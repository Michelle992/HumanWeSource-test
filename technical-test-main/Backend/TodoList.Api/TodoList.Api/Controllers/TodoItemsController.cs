using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Api.Repository;
using TodoList.Api.ViewModel;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemRepo _todoItem;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController( ILogger<TodoItemsController> logger, ITodoItemRepo todoItem)
        {
            _logger = logger;
            _todoItem = todoItem;
          
        }


        // GET: api/TodoItems
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {

            var results =  _todoItem.GetTodoItems();
            _logger.Log(LogLevel.Information, "succssfully retrieved todolist items");
            return Ok(results);
        }
      
        // GET: api/TodoItems/...
        [HttpGet("{id}")]
        public  Task<IActionResult> GetTodoItem(Guid id)
        {

            var result =  _todoItem.GetTodoItem(id);

            if (result == null)
            {
                _logger.Log(LogLevel.Error, "Todo list Item does not exist");
                return Task.FromResult<IActionResult>(NotFound());
            }
            _logger.Log(LogLevel.Information, "succssfully retrieved todolist items");
            return Task.FromResult<IActionResult>(Ok(result));
        }

        // PUT: api/TodoItems/... 
        [HttpPut()]
        [Route("PutTodoItem")]
        public async Task<ActionResult<TodoItem>> PutTodoItem(TodoItemViewModel todoItem)
        {
            var result = _todoItem.GetTodoItem(todoItem.Id);

            if (result ==null)
            {
                return (BadRequest("Todo list Item does not exist"));
            }

            try
            {
                if (TodoItemDescriptionExists(todoItem.Description))
                {
                    return BadRequest("Description already exists");
                }
               await _todoItem.UpdateTodoItem(todoItem);
                     _todoItem.Save();
                     _logger.Log(LogLevel.Error, "succssfully updated todolist items");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemIdExists(result.Id))
                {
                    _logger.Log(LogLevel.Error, "Todo item does not exist");
                    return NotFound();

                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems 
        [HttpPost]
        [Route("PostTodoItem")]
        public async Task<IActionResult> PostTodoItem(TodoItemViewModel todoItem)
        {
        
            if (ModelState.IsValid) {
                if (TodoItemDescriptionExists(todoItem.Description))
                {
                    return BadRequest("Description already exists");
                }
                  await  _todoItem.SaveTodoItem(todoItem);
                         _todoItem.Save();
                         _logger.Log(LogLevel.Information, "succssfully Saved todolist items");
            }
            
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }
 
        private bool TodoItemIdExists(Guid id)
        {
            return _todoItem.GetTodoItems().Any(x => x.Id == id);
        }

        private bool TodoItemDescriptionExists(string description)
        {
            bool found = false;
            var list = _todoItem.GetTodoItems();
            if (list !=null ) { 
                found = _todoItem.GetTodoItems()
                   .Any(x => x.Description.ToLowerInvariant() == description.ToLowerInvariant() && !x.IsCompleted);
            }
            return found; 
        }
    }
}
