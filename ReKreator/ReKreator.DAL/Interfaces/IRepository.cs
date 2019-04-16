using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReKreator.DAL.Interfaces
{
    /// <summary>
    /// Generic Repository for CRUD operations and methods to locate entities within your store.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <typeparam name="TDomain">The type of the domain entity.</typeparam>
    public interface IRepository<in TId, TDomain> where TDomain : class
    {
        /// <summary>
        /// Gets a collection of all entities asynchronous.
        /// </summary>
        /// <returns>Task of entities IEnumerable collection</returns>
        Task<IEnumerable<TDomain>> GetAllAsync();

        /// <summary>
        /// Gets a collection of entities using filters asynchronous.
        /// </summary>
        /// <param name="filter">LINQ lambda expressions filter.</param>
        /// <param name="orderBy">sorting by LINQ lambda expression.</param>
        /// <param name="skip">number of elements to skip from the beginning.</param>
        /// <param name="take">number of elements to take after the skipping</param>
        /// <param name="includes">additional LINQ lambda expressions for including dependent entities.</param>
        /// <returns>Task of entities IEnumerable collection</returns>
        Task<IEnumerable<TDomain>> GetAllAsync(
            Expression<Func<TDomain, bool>> filter,
            Func<IQueryable<TDomain>, IOrderedQueryable<TDomain>> orderBy = null,
            int skip = 0,
            int? take = null,
            params Expression<Func<TDomain, object>>[] includes);

        /// <summary>
        /// Gets one entity by id asynchronous.
        /// </summary>
        /// <param name="id">The identifier of entity.</param>
        /// <returns>Task of one entity</returns>
        Task<TDomain> GetAsync(TId id);

        /// <summary>
        /// Gets one entity using filter asynchronous.
        /// </summary>
        /// <param name="expression">LINQ lambda expressions filter.</param>
        /// <param name="includes">additional LINQ lambda expressions for including dependent entities.</param>
        /// <returns>Task of IEnumerable collection of entities</returns>
        Task<TDomain> GetAsync(Expression<Func<TDomain, bool>> expression, params Expression<Func<TDomain, object>>[] includes);

        /// <summary>
        /// Used to create a new entity into the database.
        /// </summary>
        /// /<param name="item">Entity to create.</param>
        void Create(TDomain item);

        /// <summary>
        /// Used to update an entity that already exists in the database.
        /// </summary>
        /// <param name="item">Entity to update.</param>
        void Update(TDomain item);

        /// <summary>
        /// Used to delete an entity from the database.
        /// </summary>
        /// <param name="id">The identifier of entity to delete.</param>
        void Delete(TId id);

        /// <summary>
        /// Used to delete an entity from the database.
        /// </summary>
        /// <param name="item">Entity to delete.</param>
        void Delete(TDomain item);
    }
}
