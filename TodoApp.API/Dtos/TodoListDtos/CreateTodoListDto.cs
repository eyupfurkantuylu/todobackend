namespace TodoApp.API.Dtos.TodoListDtos
{
    public class CreateTodoListDto
    {
        public string Title { get; set; }
        public bool IsHidden { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? UserId { get; set; }
    }
}