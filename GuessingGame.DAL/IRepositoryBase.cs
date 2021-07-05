using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GuessingGame.DAL
{
    public interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> GetAll();

        Task<T> GetById(int id);

        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);

        Task<Result> Create(T entity);

        Task<Result> Update(T entity);

        Task<Result> Remove(T entity);
    }
}
