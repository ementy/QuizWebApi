using Data.Repositories.Contracts;
using Moq;
using NUnit.Framework;
using QuizApi.Controllers;
using QuizApi.ViewModels.Quotes;
using Microsoft.AspNetCore.Mvc;
using Entities.DbModels;
using System.Threading.Tasks;
using System.Net;

namespace QuizTask.Tests
{
    [TestFixture]
    public class TestQuoteContoller
    {
        [Test]
        public async Task CreateAsync_ReturnsCorrectResponse_WhenModelStateIsValid()
        {
            //ARRANGE
            var quoteVM = new QuoteCreateViewModel();
            quoteVM.Content = "some content";
            quoteVM.AuthorId = 3;

            var quote = new Quote
            {
                Content = quoteVM.Content,
                AuthorId = quoteVM.AuthorId
            };

            var author = new Author()
            {
                FullName = "some author",
                Id = 3
            };

            var fakeRepo = new Mock<IQuoteRepository>();
            fakeRepo.Setup(x => x.AddAsync(quote)).Returns(Task.CompletedTask);
            var fakeAuthorRepo = new Mock<IAuthorRepository>();
            fakeAuthorRepo.Setup(x => x.GetById(quoteVM.AuthorId)).Returns(author);


            var controller = new QuoteController(fakeRepo.Object, fakeAuthorRepo.Object);

            //ACT
            var taskResult = await controller.CreateAsync(quoteVM);


            //ASSERT
            var taskResultContent = taskResult as CreatedAtActionResult;
            var expectedStatusCode = (int)HttpStatusCode.Created;

            var routeValue = taskResultContent.RouteValues;
            var value = taskResultContent.Value;

            //var createdAtActionResult = Xunit.Assert.IsType<CreatedAtActionResult>(taskResult);
            //Xunit.Assert.Null(createdAtActionResult.ControllerName);
            //Xunit.Assert.Equal(expectedStatusCode, createdAtActionResult.StatusCode);
            //Xunit.Assert.Equal("GetQuoteByIdAsync", createdAtActionResult.ActionName);
            //fakeRepo.Verify();

            //check if the task result of the controller method is CreatedAtActionResult
            Assert.IsInstanceOf<CreatedAtActionResult>(taskResult);
            //Assert that the response in not null
            Assert.IsNotNull(taskResult);

            //check if the response code for successfull complete is equal to HttpStatusCode.Created (201)
            Assert.AreEqual(expectedStatusCode, taskResultContent.StatusCode);
            //check if returns status
            Assert.IsNotNull(taskResultContent.StatusCode);

            //check if the return routeValue is generated 
            Assert.IsNotNull(taskResultContent.RouteValues["id"]);

            //check if there is a return object value in the CreateAtAction response
            Assert.IsNotNull(taskResultContent.Value);
            //check if the return object of the CreateAtAction is Quote class
            Assert.IsInstanceOf<Quote>(taskResultContent.Value);

            //fakeRepo.Verify();

        }

        [Test]
        public async Task CreateAsync_ReturnsCorrectResponse_WhenModelStateIsNotValid()
        {
            //arrange
            var mystring = new string('*', 600);

            //add case with invalid author id
            var invalidQuoteVM = new QuoteCreateViewModel()
            {
                AuthorId = 1,
                Content = mystring
            };

            var invalidQuote = new Quote()
            {
                AuthorId = invalidQuoteVM.AuthorId,
                Content = invalidQuoteVM.Content
            };

            var fakeQuoteRepo = new Mock<IQuoteRepository>();
            var fakeAuthorRepo = new Mock<IAuthorRepository>();
            var controller = new QuoteController(fakeQuoteRepo.Object, fakeAuthorRepo.Object);
            controller.ModelState.AddModelError("error", "invalid name");

            //act
            //var result = await controller.CreateAsync(model: null);
            var result = await controller.CreateAsync(invalidQuoteVM);

            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        //if author with the provided id doesn't exist?
        [Test]
        public async Task CreateAsync_ReturnsCorrectResponse_WhenModelStateIsValid_WithInvalidAuthorId()
        {
            //arrange
            var quoteVM = new QuoteCreateViewModel();
            quoteVM.Content = "some content";
            quoteVM.AuthorId = 3;

            var quote = new Quote
            {
                Content = quoteVM.Content,
                AuthorId = quoteVM.AuthorId
            };

            Author nonexistentAuthor = null;

            var fakeRepo = new Mock<IQuoteRepository>();
            var fakeAuthorRepo = new Mock<IAuthorRepository>();
            fakeAuthorRepo.Setup(x => x.GetById(quoteVM.AuthorId)).Returns(nonexistentAuthor);
            var controller = new QuoteController(fakeRepo.Object, fakeAuthorRepo.Object);

            //act
            var result = await controller.CreateAsync(quoteVM);
            var badRequestResult = result as BadRequestObjectResult;

            var neededStatusCode = (int)HttpStatusCode.BadRequest;
            var neededMessage = "Invalid author.";

            //assert that retunrs bad request result
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            //assert that returns the correct bad result code 400
            Assert.AreEqual(neededStatusCode, badRequestResult.StatusCode);
            //assert that returns the correct failure message
            Assert.AreEqual(neededMessage, badRequestResult.Value);
        }
    }
}
