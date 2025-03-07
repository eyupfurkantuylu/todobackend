namespace TodoApp.API.Dtos.TodoListDtos
{
    public class UpdateTodoListDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool IsHidden { get; set; }
    }
} 