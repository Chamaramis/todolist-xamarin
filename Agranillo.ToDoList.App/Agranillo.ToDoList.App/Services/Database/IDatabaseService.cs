using System.Collections.Generic;
using System.Threading.Tasks;
using Agranillo.ToDoList.App.Models;

namespace Agranillo.ToDoList.App.Services.Database
{
    /// <summary>
    /// Grants access to local data.
    /// </summary>
    public interface IDatabaseService
    {
        /// <summary>
        /// Gets all the records in the database for that table.
        /// </summary>
        /// <typeparam name="T">Table.</typeparam>
        /// <returns>List of records for that table.</returns>
        Task<List<T>> Get<T>()
            where T : EntityBase, new();

        /// <summary>
        /// Inserts a new record in the database for that table.
        /// </summary>
        /// <typeparam name="T">Table.</typeparam>
        /// <param name="entity">Entity to insert.</param>
        /// <returns>Flag indicating if the operation was successful</returns>
        Task<bool> Create<T>(T entity)
            where T : EntityBase, new();

        /// <summary>
        /// Updates the specified record in the database for that table.
        /// </summary>
        /// <typeparam name="T">Table.</typeparam>
        /// <param name="entity">Entity to modify.</param>
        /// <returns>Flag indicating if the operation was successful</returns>
        Task<bool> Update<T>(T entity)
            where T : EntityBase, new();

        /// <summary>
        /// Deletes the specified record in the database for that table.
        /// </summary>
        /// <typeparam name="T">Table.</typeparam>
        /// <param name="entity">Entity to delete.</param>
        /// <returns>Flag indicating if the operation was successful</returns>
        Task<bool> Delete<T>(T entity)
            where T : EntityBase, new();
    }
}