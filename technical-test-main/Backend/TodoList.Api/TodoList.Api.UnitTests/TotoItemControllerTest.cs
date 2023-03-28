using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Api.Controllers;
using TodoList.Api.Repository;
using TodoList.Api.Service;
using TodoList.Api.ViewModel;
using Xunit;
using Xunit.Sdk;

namespace TodoList.Api.UnitTests
{
    public class TotoItemControllerTest
    {
     
        private readonly Mock<ITodoItemRepo> _mockRepo;
        private readonly TodoItemsController _controller;

        public TotoItemControllerTest() {
            _mockRepo = new Mock<ITodoItemRepo>();
           var  logger = Mock.Of<ILogger<TodoItemsController>>();
            _controller = new TodoItemsController(logger, _mockRepo.Object);
        }

     
        private List<TodoItem> TodoItemList()
        {
       
            List<TodoItem> items = new List<TodoItem>()
            {
                new TodoItem()
                {
                    Id =new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"),
                    Description ="Test",
                    IsCompleted = false,
                  
                }, new TodoItem {
                       Id =new Guid("11223344-5566-7788-99AA-BBCCDDEEFF09"),
                    Description ="Test3",
                    IsCompleted = false,
                }         
            };

            return items;
        }

        [Fact]
        public async Task returnListOfTodoItems()
        {
           
            _mockRepo
          .Setup(p => p.GetTodoItems())
         .Returns(TodoItemList())
         .Verifiable();

            var response = await _controller.GetTodoItems() as OkObjectResult;
            // Problem is here, 
            var li = response.Value as List<TodoItem>;
        }
        [Fact]
        public async void GetTodoItemDetails()
        {
            var item = new TodoItem()
            {
                Id = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF22"),
                Description = "Submit the test",
                IsCompleted = false,
            };
            _mockRepo.Setup(p => p.GetTodoItem(Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF22"))).Returns(item);
            var result = await _controller.GetTodoItem(item.Id);
            try
            {
                Assert.False(item.Equals(result));
            }
            catch (EqualException ex)
            {
                throw new XunitException($"{"Testing message"}\n{ex}");
            }
            
        }

        [Fact]
        public void AddItem()
        {
            var item = new TodoItemViewModel()
            {
                Id = Guid.NewGuid(),
                Description = "Mpho",
                IsCompleted = false,
            };
            _controller.PostTodoItem(item);

            var blog = _controller.GetTodoItem(item.Id);
            string msg = "";
            if (blog != null) {
                msg = "Saved";
            }
            else {
                msg = "Failed";
            }
            Assert.Equal("Saved",msg);

        }
    }
}
