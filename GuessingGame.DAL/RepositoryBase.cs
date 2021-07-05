using GuessingGame.Model.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame.DAL
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly GuessingGameDb dbContext;

        public RepositoryBase(GuessingGameDb dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<T> GetAll()
        {
            return dbContext.Set<T>();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return dbContext.Set<T>().Where(expression);
        }

        public async Task<T> GetById(int id)
        {
            return await dbContext.Set<T>().FindAsync(id).ConfigureAwait(false);
        }

        public async Task<Result> Remove(T entity)
        {
            try
            {
                dbContext.Set<T>().Remove(entity);
                await SaveChanges().ConfigureAwait(false);

                return new Result(succeed: true);
            }
            catch (Exception ex)
            {
                return new Result(succeed: false, error: ex.Message);
            }
        }

        public async Task<Result> Create(T entity)
        {
            try
            {
                dbContext.Set<T>().Add(entity);
                await SaveChanges().ConfigureAwait(false);

                return new Result(succeed: true);
            }
            catch (Exception ex)
            {
                return new Result(succeed: false, error: ex.Message);
            }
        }

        public async Task<Result> Update(T entity)
        {
            try
            {
                dbContext.Set<T>().Update(entity);
                await SaveChanges().ConfigureAwait(false);

                return new Result(succeed: true);
            }
            catch (Exception ex)
            {
                return new Result(succeed: false, error: ex.Message);
            }
        }

        private async Task SaveChanges()
        {
            try
            {
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }
    }
}
