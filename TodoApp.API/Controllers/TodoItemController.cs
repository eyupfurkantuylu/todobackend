using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.API.Dtos.TodoItemDtos;
using TodoApp.API.Services.TodoItemService;

namespace TodoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodoItems()
        {
            var todoItems = await _todoItemService.GetAllTodoItemsAsync();
            return Ok(todoItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItemById(string id)
        {
            var todoItem = await _todoItemService.GetTodoItemByIdAsync(id);
            return Ok(todoItem);
        }


        [HttpGet("list/{listId}")]
        public async Task<IActionResult> GetAllTodoItemsByListId(string listId)
        {
            var todoItems = await _todoItemService.GetAllTodoItemsByListIdAsync(listId);
            return Ok(todoItems);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoItem(CreateTodoItemDto todoItem)
        {
            var createdTodoItem = await _todoItemService.CreateTodoItemAsync(todoItem);
            return CreatedAtAction(nameof(GetTodoItemById), new { id = createdTodoItem.Id }, createdTodoItem);
        }   

        [HttpPut()]
        public async Task<IActionResult> UpdateTodoItem( UpdateTodoItemDto todoItem)
        {
            var updatedTodoItem = await _todoItemService.UpdateTodoItemAsync(todoItem);
            return Ok(updatedTodoItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(string id)
        {
            var result = await _todoItemService.DeleteTodoItemAsync(id);
            return NoContent();
        }
    }
}