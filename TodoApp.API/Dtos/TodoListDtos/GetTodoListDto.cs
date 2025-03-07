using System;

namespace TodoApp.API.Dtos.TodoListDtos
{
    public class GetTodoListDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool IsHidden { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
    }
} 