using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class TodoItem
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
