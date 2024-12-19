namespace Squash.WebAPI.Interfaces.Repositories.Base
{
    /// <summary>
    /// Base Interface for the repositories.
    /// </summary>
    /// <typeparam name="T">Service</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Create entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns></returns>
        Task<bool> CreateAsync(T entity);

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);
    }
}
