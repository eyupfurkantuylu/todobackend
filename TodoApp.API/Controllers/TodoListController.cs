using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApp.API.Dtos.TodoListDtos;
using TodoApp.API.Models.Identity;
using TodoApp.API.Services.TodoListService;

namespace TodoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITodoListService _todoListService;

        public TodoListController(UserManager<ApplicationUser> userManager, ITodoListService todoListService)
        {
            _userManager = userManager;
            _todoListService = todoListService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllTodoLists()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

            if (string.IsNullOrEmpty(email))
                return Unauthorized(new { message = "Email claim bulunamadı." });

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return NotFound(new { message = "Kullanıcı bulunamadı." });

            var todoLists = await _todoListService.GetAllTodoListsAsync();
            return Ok(todoLists);
        }

        [Authorize]
        [HttpGet("GetMyTodoLists")]
        public async Task<IActionResult> GetTodoListsByUserId()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

            if (string.IsNullOrEmpty(email))
                return Unauthorized(new { message = "Email claim bulunamadı." });

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return NotFound(new { message = "Kullanıcı bulunamadı." });

            var todoLists = await _todoListService.GetTodoListWithUserIdAsync(user.Id);
            return Ok(todoLists);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTodoList([FromBody] CreateTodoListDto todoList)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

            if (string.IsNullOrEmpty(email))
                return Unauthorized(new { message = "Email claim bulunamadı." });

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return NotFound(new { message = "Kullanıcı bulunamadı." });
            todoList.UserId = user.Id;
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