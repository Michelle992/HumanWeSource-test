using System;

namespace TodoList.Api.ViewModel
{
    public class TodoItemViewModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public TodoItemViewModel(Guid Id, string Description, bool IsCompleted)
        {
            this.Description = Description;
            this.IsCompleted = IsCompleted;
            this.Id = Id;
        }

        public TodoItemViewModel()
        {
        }
    }
}
