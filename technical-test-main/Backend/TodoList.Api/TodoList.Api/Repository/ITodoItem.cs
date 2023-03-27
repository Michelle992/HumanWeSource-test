using System;
using System.Collections.Generic;
using TodoList.Api.Mapper;
using TodoList.Api.ViewModel;

namespace TodoList.Api.Repository
{
    public interface ITodoItem
    {
        List<TodoItem> GetTodoItems();
        TodoItem GetTodoItem(Guid id);

        void SaveTodoItem(TodoItemViewModel todoItem);
        void UpdateTodoItem(TodoItemViewModel todoItem);

        void Save();
    }
}
