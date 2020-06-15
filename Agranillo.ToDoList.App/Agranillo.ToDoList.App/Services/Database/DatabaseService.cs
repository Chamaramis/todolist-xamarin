using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Agranillo.ToDoList.App.Models;
using SQLite;

namespace Agranillo.ToDoList.App.Services.Database
{
    public class DatabaseService : IDatabaseService
    {
        /// <summary>
        /// Our private connection
        /// </summary>
        private SQLiteAsyncConnection connection;

        /// <summary>
        /// Initializes a new SQLite connection with the specified parameters in DatabaseConfiguration and creates the tables if they don't exist.
        /// </summary>
        public DatabaseService()
        {
            this.connection = new SQLiteAsyncConnection(DatabaseConfiguration.DatabaseFilePath, DatabaseConfiguration.DatabaseFlags);
            this.InitializeDatabase();
        }

        /// <summary>
        /// Creates the specified tables if they don't exist.
        /// </summary>
        private void InitializeDatabase()
        {
            try
            {
                this.connection?.CreateTableAsync<ToDoItem>().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }
        }

        public Task<List<T>> Get<T>()
            where T : EntityBase, new()
        {
            var records = this.connection.Table<T>().ToListAsync();
            return records;
        }

        public async Task<bool> Create<T>(T entity)
            where T : EntityBase, new()
        {
            try
            {
                int affectedRows = await this.connection.InsertAsync(entity);
                return affectedRows == 1;
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                return false;
            }
        }

        public async Task<bool> Update<T>(T entity)
            where T : EntityBase, new()
        {
            try
            {
                int affectedRows = await this.connection.UpdateAsync(entity);
                return affectedRows == 1;
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                return false;
            }
        }

        public async Task<bool> Delete<T>(T entity)
            where T : EntityBase, new()
        {
            try
            {
                int deletedRows = await this.connection.DeleteAsync<T>(entity.Id);
                return deletedRows == 1;
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                return false;
            }
        }
    }
}
