using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.DataAccess.CSV
{
    public class DataAccess
    {
        public List<TodoItem> LoadTaskData()
        {
            // CSV Limit
            int csvLimit = 3;

            // Fetch all tasks
            List<string> rows = Storage.GetTaskStore().ToList();

            // Create a List of Tasks
            List<TodoItem> tasks = new List<TodoItem>();

            // Fetch each task and store it in the list
            foreach(var row in rows)
            {
                string[] values = row.Split(',');

                if(values.Length != csvLimit)
                {
                    throw new Exception("An error occurred while attempting to read values from file store. Check the CSV");
                }

                tasks.Add(new TodoItem
                {
                    Id = Guid.NewGuid(),
                    Title = values[1],
                    Description = values[2]
                });
            }

            return tasks;
        }

    }
}
