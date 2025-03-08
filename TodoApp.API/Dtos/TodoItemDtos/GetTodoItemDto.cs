using System;

namespace TodoApp.API.Dtos.TodoItemDtos
{
    public class GetTodoItemDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TodoListId { get; set; }
    }
} 