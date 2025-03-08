namespace TodoApp.API.Dtos.TodoItemDtos
{
    public class UpdateTodoItemDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public bool IsCompleted { get; set; }
    }
} 