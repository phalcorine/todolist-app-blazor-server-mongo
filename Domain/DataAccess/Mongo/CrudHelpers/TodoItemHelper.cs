using Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DataAccess.Mongo.CrudHelpers
{
    public class TodoItemHelper
    {
        private CRUD crudHelper;
        private string _tableName = "todo-items";
        

        public TodoItemHelper()
        {
            // Get CRUD Instance
            crudHelper = CRUD.GetInstance();

            // Initialize the collection with new items if not existing
            this.Init();
        }

        /// <summary>
        /// Gets a reference to the db collection
        /// </summary>
        /// <returns></returns>
        public IMongoCollection<TodoItem> GetCollection()
        {
            // Get the collection
            var collection = crudHelper.GetCollection<TodoItem>(_tableName);

            return collection;
        }

        public List<TodoItem> GetTodoItems()
        {
            // Fetches a list of items from the collection
            List<TodoItem> todoItems = crudHelper.Find<TodoItem>(_tableName);

            // Returns the items
            return todoItems;
        }

        /// <summary>
        /// Creates a new record into the collection
        /// </summary>
        /// <param name="record"></param>
        public void Create(TodoItem record)
        {
            // Insert the record
            crudHelper.InsertOne<TodoItem>(_tableName, record);
        }

        /// <summary>
        /// Initializes the collection with sample data, if none exists
        /// </summary>
        private void Init()
        {
            var collection = this.GetCollection();

            if(collection.EstimatedDocumentCount() < 1)
            {
                // Create a list of new dummy records
                List<TodoItem> todoItems = new List<TodoItem>
                {
                    new TodoItem
                    {
                        Id = Guid.NewGuid(),
                        Title = "First Task",
                        Description = "This is the description of the first task"
                    },
                    new TodoItem
                    {
                        Id = Guid.NewGuid(),
                        Title = "Second Task",
                        Description = "This is the description of the second task. It is basically after the first task"
                    },
                };

                // Insert each record into the collection
                todoItems.ForEach(todoItem => this.Create(todoItem));
            }
        }
    }
}
