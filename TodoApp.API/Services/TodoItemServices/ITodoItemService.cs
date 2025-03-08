using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.API.Dtos.TodoItemDtos;

namespace TodoApp.API.Services.TodoItemService
{
    public interface ITodoItemService
    {
        Task<List<GetTodoItemDto>> GetAllTodoItemsAsync();
        Task<List<GetTodoItemDto>> GetAllTodoItemsByListIdAsync(string listId);
        Task<GetTodoItemDto> GetTodoItemByIdAsync(string id);
        Task<GetTodoItemDto> CreateTodoItemAsync(CreateTodoItemDto todoItem);
        Task<GetTodoItemDto> UpdateTodoItemAsync(UpdateTodoItemDto todoItem);
        Task<bool> DeleteTodoItemAsync(string id);
    }
} 