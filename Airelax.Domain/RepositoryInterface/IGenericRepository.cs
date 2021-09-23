using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Airelax.Domain.DomainObject;

namespace Airelax.Domain.RepositoryInterface
{
    public interface IGenericRepository<in TId, TEntity> where TEntity : Entity<TId>
    {
        /// <summary>
        ///     取得Entity全部筆數的IQueryable。
        /// </summary>
        /// <returns>Entity全部筆數的IQueryable。</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        ///     透過指定條件選取第一個符合的Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns>符合條件的第一個Entity，若無則回傳default</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp);

        /// <summary>
        ///     建立一個Entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task CreateAsync(TEntity item);

        /// <summary>
        ///     更新Entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity item);

        /// <summary>
        ///     刪除Entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity item);

        /// <summary>
        ///     儲存變更
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}