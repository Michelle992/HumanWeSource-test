using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Api.Mapper;
using TodoList.Api.ViewModel;

namespace TodoList.Api.Repository
{
    public interface ITodoItemRepo
    {
        List<TodoItem> GetTodoItems();
        TodoItem GetTodoItem(Guid id);

        Task SaveTodoItem(TodoItemViewModel todoItem);
        Task UpdateTodoItem(TodoItemViewModel todoItem);

        void Save();
    }
}
