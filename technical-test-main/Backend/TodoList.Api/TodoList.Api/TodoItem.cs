using System;

namespace TodoList.Api
{
    public class TodoItem
    {
        
        public Guid Id { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public TodoItem(Guid Id, string Description, bool IsCompleted)
        {
            this.Description = Description;
            this.IsCompleted = IsCompleted;
            this.Id = Id;
        }

        public TodoItem()
        {
        }
    }
}
