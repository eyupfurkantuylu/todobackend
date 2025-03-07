using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.API.Data
{
    public class TodoItems
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool isCompleted { get; set; }
        public string Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TodoListId { get; set; }
     
    }
}