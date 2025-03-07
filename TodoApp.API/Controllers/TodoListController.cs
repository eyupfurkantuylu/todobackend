using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.API.Dtos.TodoListDtos;
using TodoApp.API.Services.TodoListService;

namespace TodoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListService _todoListService;

        public TodoListController(ITodoListService todoListService)
        {
            _todoListService = todoListService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodoLists()
        {
            var todoLists = await _todoListService.GetAllTodoListsAsync();
            return Ok(todoLists);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoList([FromBody] CreateTodoListDto todoList)
        {
            var createdTodoList = await _todoListService.CreateTodoListAsync(todoList);
            return CreatedAtAction(nameof(GetTodoListById), new { id = createdTodoList.Id }, createdTodoList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoListById(string id)
        {
            var todoList = await _todoListService.GetTodoListByIdAsync(id);
            if (todoList == null)
                return NotFound();
                
            return Ok(todoList);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateTodoList([FromBody] UpdateTodoListDto todoList)
        {
            var updatedTodoList = await _todoListService.UpdateTodoListAsync(todoList);
            if (updatedTodoList == null)
                return NotFound();

            return Ok(updatedTodoList);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList(string id)
        {
            var result = await _todoListService.DeleteTodoListAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}