using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoList.Api.Mapper;
using TodoList.Api.Repository;
using TodoList.Api.ViewModel;

namespace TodoList.Api.Service
{
    public class TodoItemRepo : ITodoItem
    {
        private TodoContext _context;
        private MapTodoItem _mapper;
    

        public TodoItemRepo(TodoContext context) {
            _context = context;
            //_mapper = mapper;
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

        public void Save()
        {
           _context.SaveChanges();
        }

        public void SaveTodoItem(TodoItemViewModel todoItem)
        {
            var mapped = _mapper.mapTodoItem(todoItem);
            _context.TodoItems.Add(mapped);
        }

        public void UpdateTodoItem(TodoItemViewModel todoItem)
        {
            var mapped = _mapper.mapTodoItem(todoItem);
            _context.Entry(mapped).State = EntityState.Modified;
           // _context.SaveChangesAsync();
        }
    }
}
