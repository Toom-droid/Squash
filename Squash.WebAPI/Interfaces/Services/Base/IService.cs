namespace Squash.WebAPI.Interfaces.Services.Base
{
    /// <summary>
    /// Base Interface for the services.
    /// </summary>
    /// <typeparam name="T">Model</typeparam>
    public interface IService<T> where T : class
    {
        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Create entity.
        /// </summary>
        /// <param name="entity"></param>
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
