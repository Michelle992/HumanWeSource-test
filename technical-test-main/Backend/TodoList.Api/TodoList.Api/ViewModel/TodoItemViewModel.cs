using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.ViewModel
{
    public class TodoItemViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Remote("TodoItemDescriptionExists", "TodoItems", ErrorMessage = "Description already exists")]
        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public TodoItemViewModel(Guid Id, string Description, bool IsCompleted)
        {
            this.Description = Description;
            this.IsCompleted = IsCompleted;
            this.Id = Id;
        }

    }
}
