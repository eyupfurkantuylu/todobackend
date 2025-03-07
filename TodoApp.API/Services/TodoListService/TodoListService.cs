using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.API.Data;
using TodoApp.API.Dtos.TodoListDtos;
using Dapper;
using System.Data;

namespace TodoApp.API.Services.TodoListService
{
    public class TodoListService : ITodoListService
    {
        private readonly DapperDbContext _context;

        public TodoListService(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<GetTodoListDto> CreateTodoListAsync(CreateTodoListDto todoList)
        {
            var query = @"INSERT INTO TodoLists (Id, Title, IsHidden, CreatedAt,  UserId) 
                         VALUES (@Id, @Title, @IsHidden, @CreatedAt,  @UserId);
                         SELECT * FROM TodoLists WHERE Id = @Id";

            var parameters = new
            {
                Id = Guid.NewGuid().ToString(),
                todoList.Title,
                todoList.IsHidden,
                todoList.CreatedAt,
                todoList.UserId
            };

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetTodoListDto>(query, parameters);
            }
        }

        public async Task<bool> DeleteTodoListAsync(string id)
        {
            var query = "DELETE FROM TodoLists WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, new { Id = id });
                return result > 0;
            }
        }

        public async Task<List<GetTodoListDto>> GetAllTodoListsAsync()
        {
            var query = "SELECT * FROM TodoLists";

            using (var connection = _context.CreateConnection())
            {
                var todoLists = await connection.QueryAsync<GetTodoListDto>(query);
                return todoLists.ToList();
            }
        }

        public async Task<GetTodoListDto> GetTodoListByIdAsync(string id)
        {
            var query = "SELECT * FROM TodoLists WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetTodoListDto>(query, new { Id = id });
            }
        }

        public async Task<GetTodoListDto> UpdateTodoListAsync(UpdateTodoListDto todoList)
        {
            var query = @"UPDATE TodoLists 
                         SET Title = @Title, 
                             IsHidden = @IsHidden
                         WHERE Id = @Id;
                         SELECT * FROM TodoLists WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Title", todoList.Title);
            parameters.Add("IsHidden", todoList.IsHidden);
            parameters.Add("Id", todoList.Id);
         

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetTodoListDto>(query, parameters);
            }
        }
    }
}