using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.API.Dtos.TodoListDtos;

namespace TodoApp.API.Services.TodoListService
{
    public interface ITodoListService
    {
        Task<List<GetTodoListDto>> GetAllTodoListsAsync();
        Task<GetTodoListDto> GetTodoListByIdAsync(string id);
        Task<GetTodoListDto> CreateTodoListAsync(CreateTodoListDto todoList);
        Task<GetTodoListDto> UpdateTodoListAsync(UpdateTodoListDto todoList);
        Task<bool> DeleteTodoListAsync(string id);
        Task<List<GetTodoListDto>> GetTodoListWithUserIdAsync(string userId);
    }
}
