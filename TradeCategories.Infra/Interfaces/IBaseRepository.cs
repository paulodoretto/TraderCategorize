using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeCategories.Domain;

namespace TradeCategories.Infra.Interfaces
{
    public interface IBaseRepository<T> where T : Base
    {
        Task<T> Create(T obj);

        Task<T> Update(T obj);

        Task Remove(long id);

        Task<T> GetById(long id);

        Task<List<T>> Get();
    }
}
