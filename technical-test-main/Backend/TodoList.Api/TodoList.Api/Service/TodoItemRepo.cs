using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Api.Mapper;
using TodoList.Api.Repository;
using TodoList.Api.ViewModel;

namespace TodoList.Api.Service
{
    public class TodoItemRepo : ITodoItemRepo
    {
        private TodoContext _context;
        private MapTodoItem _mapper;
    

        public TodoItemRepo(TodoContext context) {
            _context = context;
            _mapper = new MapTodoItem();
        }
        public TodoItem GetTodoItem(Guid id)
        {
            return _context.TodoItems.AsNoTracking().Where(x=>x.Id.Equals(id)).FirstOrDefault();
        }

        public List<TodoItem> GetTodoItems()
        {
         
            return _context.TodoItems.ToList();
        }

        public  async void Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task SaveTodoItem(TodoItemViewModel todoItem)
        {
            var mapped = _mapper.MapTodoItemToEntity(todoItem);
           await _context.TodoItems.AddAsync(mapped);
        }

        public async Task UpdateTodoItem(TodoItemViewModel todoItem)
        {
            var mapped = _mapper.MapTodoItemToEntity(todoItem);
            _context.Entry(mapped).State = EntityState.Modified;

        }
    }
}
