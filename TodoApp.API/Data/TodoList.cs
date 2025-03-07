using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.API.Data
{
    public class TodoList
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool isHidden { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
    }
}