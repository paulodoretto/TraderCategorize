using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TradeCategories.Domain;
using TradeCategories.Infra.Interfaces;

namespace TradeCategories.Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base    
    {
        private readonly string _tableName;

        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();

        protected BaseRepository(string tableName)
        {
            _tableName = tableName;
        }

        private OracleConnection OracleConnection()
        {
            return new OracleConnection(ConfigurationManager.ConnectionStrings["ORACLEDBSTRING"].ConnectionString);
        }

        public IDbConnection CreateConnection()
        {
            var conection = OracleConnection();
            conection.Open();
            return conection;
        }

        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }

        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_tableName} ");

            insertQuery.Append("(");

            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(prop => { insertQuery.Append($"{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($":{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")");

            return insertQuery.ToString();
        }

        public async Task<T> Create(T obj)
        {
            var insertQuery = GenerateInsertQuery();

            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(insertQuery, obj);
            }

            return obj;
        }

        private string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");
            var properties = GenerateListOfProperties(GetProperties);

            properties.ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    updateQuery.Append($"{property}=:{property},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append(" WHERE Id=:Id");

            return updateQuery.ToString();
        }

        public async Task<T> Update(T obj)
        {
            {
                var updateQuery = GenerateUpdateQuery();

                using (var connection = CreateConnection())
                {
                    await connection.ExecuteAsync(updateQuery, obj);
                }

                return obj;
            }
        }

        public async Task Remove(long id)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync($"DELETE FROM {_tableName} WHERE Id=:Id", new { Id = id });
            }
        }

        public async Task<List<T>> Get()
        {
            using (var connection = CreateConnection())
            {
                return (List<T>)await connection.QueryAsync<T>($"SELECT * FROM {_tableName}");
            }
        }

        public async Task<T> GetById(long id)
        {
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {_tableName} WHERE Id=:Id", new { Id = id });
                if (result == null)
                    throw new KeyNotFoundException($"{_tableName} with id [{id}] could not be found.");

                return result;
            }
        }

        

       
    }
}
