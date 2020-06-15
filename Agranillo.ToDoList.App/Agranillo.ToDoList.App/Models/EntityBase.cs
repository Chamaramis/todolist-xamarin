using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Agranillo.ToDoList.App.Models
{
    /// <summary>
    /// Base class for all entities that will live in the database.
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Id of the record.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
