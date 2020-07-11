using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Domain.Models;

namespace Domain
{
    class Storage
    {
        private static string taskFilePath = @"C:\Data\Todo\Tasks.txt";
        public static string[] GetTaskStore()
        {
            try
            {
                // Create the file if it does not exist
                if(!File.Exists(taskFilePath))
                {
                    File.CreateText(taskFilePath);
                }

                // Fetch the file contents
                return File.ReadAllLines(taskFilePath);
                
            }
            catch (Exception ex)
            {
                // Capture the exception and display the actual error message
                Console.WriteLine($"An error occurred. Reason: {ex.Message}");

                // Rethrow exception
                throw ex;
            }
            
        }
    }
}
