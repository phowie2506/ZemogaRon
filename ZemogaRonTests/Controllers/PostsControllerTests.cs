using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZemogaRon.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ZemogaRon.Models;

namespace ZemogaRon.Controllers.Tests
{
    [TestClass()]
    public class PostsControllerTests
    {
        [TestMethod()]
        public void ValidateModel()
        {
            //validate if the model is not valid , is required UserId
            Post post = new Post()
            {
                Id = 1,
                Title = "title of the post"
            };
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(post, serviceProvider: null, items: null);
            bool isValid = Validator.TryValidateObject(post,context, validationResults, true);
            Assert.IsFalse(isValid);
        }
    }
}