using TodoList.Api.ViewModel;

namespace TodoList.Api.Mapper
{
    public class MapTodoItem
    {

        public TodoItem MapTodoItemToEntity(TodoItemViewModel model)
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