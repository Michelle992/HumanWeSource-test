using System;
using TodoList.Api.ViewModel;

namespace TodoList.Api.Mapper
{
    public class MapTodoItem
    {

        public MapTodoItem() { 
        
        }

        public TodoItem mapTodoItem(TodoItemViewModel model)
        {
            TodoItem item = new TodoItem();
            if (model != null)
            {
                //model = new TodoItemViewModel();
                item.Id = model.Id;
                item.Description = model.Description;
                item.IsCompleted = model.IsCompleted;
            }
            return item;

        }

    }
}
