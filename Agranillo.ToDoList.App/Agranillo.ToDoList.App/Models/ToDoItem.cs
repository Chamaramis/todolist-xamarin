using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Agranillo.ToDoList.App.Models
{
    /// <summary>
    /// Class representing a to-do items table.
    /// </summary>
    public class ToDoItem : EntityBase
    {
        /// <summary>
        /// Title of the item.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of the item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Whether the item has ben finished or not.
        /// </summary>
        public bool Finished { get; set; }
    }
}
