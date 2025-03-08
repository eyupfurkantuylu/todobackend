namespace TodoApp.API.Dtos.TodoItemDtos
{
    public class CreateTodoItemDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public bool IsCompleted { get; set; }
        public string TodoListId { get; set; }
    }
} 