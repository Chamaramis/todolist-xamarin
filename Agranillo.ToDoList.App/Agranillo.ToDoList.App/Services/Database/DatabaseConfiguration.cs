using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;

namespace Agranillo.ToDoList.App.Services.Database
{
    /// <summary>
    /// Constant values required to initialize a SQLite connection.
    /// </summary>
    public static class DatabaseConfiguration
    {
        /// <summary>
        /// Database file name.
        /// </summary>
        public const string DatabaseFilename = "ToDoListAppDb.db3";

        /// <summary>
        /// Flags for database creation.
        /// </summary>
        public const SQLiteOpenFlags DatabaseFlags = SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite;

        /// <summary>
        /// Location of the database.
        /// </summary>
        public static string DatabaseFilePath =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseFilename);
    }
}
