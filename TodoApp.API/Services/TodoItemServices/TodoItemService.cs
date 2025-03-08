using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using TodoApp.API.Data;
using TodoApp.API.Dtos.TodoItemDtos;

namespace TodoApp.API.Services.TodoItemService
{
    public class TodoItemService : ITodoItemService
    {
        private readonly DapperDbContext _context;

        public TodoItemService(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetTodoItemDto>> GetAllTodoItemsAsync()
        {
            const string sql = @"
                SELECT Id, Title, Description, Priority, IsCompleted, CreatedAt, TodoListId 
                FROM TodoItems";

             using( var connection = _context.CreateConnection()){
                var todoItems = await connection.QueryAsync<GetTodoItemDto>(sql);
                return todoItems.AsList();
            }
        }

        public async Task<List<GetTodoItemDto>> GetAllTodoItemsByListIdAsync(string listId)
        {
            
            const string sql = @"
                SELECT Id, Title, Description, Priority, IsCompleted, CreatedAt,  TodoListId 
                FROM TodoItems 
                WHERE TodoListId = @ListId";
            
            var parameters = new DynamicParameters();
            parameters.Add("ListId", listId);

            using( var connection = _context.CreateConnection()){
                var todoItems = await connection.QueryAsync<GetTodoItemDto>(sql, parameters);
                return todoItems.AsList();
            }
        }

        public async Task<GetTodoItemDto> GetTodoItemByIdAsync(string id)
        {
            
            const string sql = @"
                SELECT Id, Title, Description, Priority, IsCompleted, CreatedAt,  TodoListId 
                FROM TodoItems 
                WHERE Id = @Id";
            
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            using( var connection = _context.CreateConnection()){
                return await connection.QueryFirstOrDefaultAsync<GetTodoItemDto>(sql, parameters);
            }
        }

        public async Task<GetTodoItemDto> CreateTodoItemAsync(CreateTodoItemDto todoItem)
        {
           
            var id = Guid.NewGuid().ToString();
            var createdAt = DateTime.UtcNow;
            
            const string sql = @"
                INSERT INTO TodoItems (Id, Title, Description, Priority, IsCompleted, CreatedAt, TodoListId)
                VALUES (@Id, @Title, @Description, @Priority, @IsCompleted, @CreatedAt, @TodoListId);
                
                SELECT Id, Title, Description, Priority, IsCompleted, CreatedAt, TodoListId
                 FROM TodoItems
                 WHERE Id = @Id";
            
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("Title", todoItem.Title);
            parameters.Add("Description", todoItem.Description);
            parameters.Add("Priority", todoItem.Priority);
            parameters.Add("IsCompleted", todoItem.IsCompleted);
            parameters.Add("CreatedAt", createdAt);
            parameters.Add("TodoListId", todoItem.TodoListId);

            using( var connection = _context.CreateConnection()){
                return await connection.QueryFirstAsync<GetTodoItemDto>(sql, parameters);
            }
        }

        public async Task<GetTodoItemDto> UpdateTodoItemAsync(UpdateTodoItemDto todoItem)
        {
                        
            const string sql = @"
                UPDATE TodoItems 
                SET Title = @Title,
                    Description = @Description,
                    Priority = @Priority,
                    IsCompleted = @IsCompleted
                WHERE Id = @Id;
                
                SELECT Id, Title, Description, Priority, IsCompleted, CreatedAt,  TodoListId
                FROM TodoItems
                WHERE Id = @Id";
            

            var parameters = new DynamicParameters();
            parameters.Add("Id", todoItem.Id);
            parameters.Add("Title", todoItem.Title);
            parameters.Add("Description", todoItem.Description);
            parameters.Add("Priority", todoItem.Priority);
            parameters.Add("IsCompleted", todoItem.IsCompleted);
            
            using( var connection = _context.CreateConnection()){
                return await connection.QueryFirstOrDefaultAsync<GetTodoItemDto>(sql, parameters);
            }
        }

        public async Task<bool> DeleteTodoItemAsync(string id)
        {
            const string sql = "DELETE FROM TodoItems WHERE Id = @Id";
            
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            using( var connection = _context.CreateConnection()){
                var affectedRows = await connection.ExecuteAsync(sql, parameters);
                return affectedRows > 0;
            }
        }
    }
}