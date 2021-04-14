using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TradeCategories.Domain;
using TradeCategories.Domain.Enums;
using TradeCategories.Infra.Interfaces;

namespace TradeCategories.Infra.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly IDbConnection _connection;
        private readonly string _tableName;

        public CategoryRepository(IDbConnection connection) : base(tableName: "Category")
        {
            _connection = connection;
            _tableName = "Category";
        }

        public Task<List<Category>> GetBySector(ESectorClient sectorClient)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Category>> SearchByName(string name)
        {
            using (var connection = CreateConnection())
            {
                try
                {
                    var result = await connection.QueryAsync<Category>($"SELECT * FROM {_tableName} Where Name LIKE :Name ", new { Name = name + "%" });

                    if (result == null)
                        throw new KeyNotFoundException($"{_tableName} with Name [{name}] could not be found.");

                    return (List<Category>)result;
                }
                catch (Exception EX)
                {

                    throw;
                }
                
               
            }
        }
    }
}
