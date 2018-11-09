using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories.Contracts;
using Entities.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Data.Repositories.Contracts;
using QuizApi.ViewModels.Quotes;

namespace QuizApi.Controllers
{
    [Route("QuizApi/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        //Repo as private field and initiation in the constructor
        private readonly IQuoteRepository quoteRepository;
        private readonly IAuthorRepository authorRepository;

        public QuoteController(IQuoteRepository quoteRepository, IAuthorRepository authorRepository)
        {
            this.quoteRepository = quoteRepository;
            this.authorRepository = authorRepository;
        }

        /// <summary>
		/// Gets all Quotes
		/// </summary>
		/// <returns>Returns all quotes or Status Code 404 Not Found if there are no quotes</returns>
        [HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<IQueryable<QuoteViewModel>>> GetAllQuotesAsync()
        {
            IQueryable<Quote> quotesResult = await this.quoteRepository.GetAllAsync();

            IQueryable<QuoteViewModel> quotes = quotesResult.Select(x => new QuoteViewModel
            {
                Content = x.Content
            });

            if (!quotes.Any())
            {
                return NotFound();
            }

            return Ok(quotes);
        }

		/// <summary>
		/// Gets a Quote by provided Id
		/// </summary>
		/// <param name="id">The Id of the quoute you want to receive</param>
		/// <returns>Returns the content of the quote with the provided id or Status Code 404 Not Found if there is no quote with this Id</returns>
		[HttpGet("{id}", Name = "GetQuoteByIdAsync")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<QuoteViewModel>> GetQuoteByIdAsync(int id)
        {
            //check for the Id?

            var quote = await this.quoteRepository.GetByIdAsync(id);

            if (quote == null)
            {
                return NotFound();
            }

            var newQuote = new QuoteViewModel
            {
                Content = quote.Content
            };

            return Ok(newQuote);
        }

        /// <summary>
		/// Creates a new quote
		/// </summary>
		/// <param name="model">Creates a new quote with the provided string Content and int AuthorId</param>
		/// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAsync([FromBody] QuoteCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Check if the author exists
            var author = this.authorRepository.GetById(model.AuthorId);

            if (author == null)
            {
                //TODO: errors string in constants class
                return BadRequest("Invalid author.");
            }

            var newQuote = new Quote
            {
                Content = model.Content,
                AuthorId = model.AuthorId
            };

            await this.quoteRepository.AddAsync(newQuote);

            return CreatedAtAction(nameof(GetQuoteByIdAsync), new { id = newQuote.Id }, newQuote);
        }

        /// <summary>
		/// Updates the quote with the provided Id
		/// </summary>
		/// <param name="id">The Id of the quote you wish to modify.</param>
		/// <param name="model">The content and the author of the quote entity.</param>
		/// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] QuoteUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var quote = await this.quoteRepository.GetByIdAsync(id);

            if (quote == null)
            {
                return NotFound();
            }

            quote.Content = model.Content;
            quote.AuthorId = model.AuthorId;

            await this.quoteRepository.UpdateAsync(quote);

            return NoContent();
        }

        /// <summary>
		/// Deletes an quote with the provided Id.
		/// </summary>
		/// <param name="id">The id of the quote you wish to delete.</param>
		/// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            Quote quote = await this.quoteRepository.GetByIdAsync(id);

            if (quote == null)
            {
                return NotFound();
            }

            await this.quoteRepository.DeleteAsync(quote);

            return NoContent();
        }

        /// <summary>
		/// Gets a random quote.
		/// </summary>
		/// <returns></returns>
        [HttpGet("Random")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<QuoteRandomViewModel>> Random()
        {
            //TODO: Add validation for ID > 0

            IQueryable<int> quoteIds = this.quoteRepository.GetAll().Select(x => x.Id);
            var listIds = quoteIds.ToList();

            Random rnd = new Random();
            int randomIndex = rnd.Next(listIds.Count);

            var id = listIds[randomIndex];

            Quote quote = await this.quoteRepository.GetByIdAsync(id);

            if (quote == null)
            {
                return BadRequest();
            }

            var resultQuote = new QuoteRandomViewModel
            {
                Id = quote.Id,
                Content = quote.Content,
            };

            return Ok(resultQuote);
        }


        /// <summary>
		/// Check if the quote with the provided Id belongs to the author with the provided Id.
		/// </summary>
		/// <param name="quoteId">The id of the quote you wish to check.</param>
		/// <param name="authorId">The Id of the author you wish to check.</param>
		/// <returns>Returns true/false if the quote belonges to the author OR 404 NotFound if theres no such quote.</returns>

        //[HttpGet(Name = "GetQuoteAndAuthorByIds")]
        //[Route("{quoteId}/author/{authorId}")]
        [HttpGet("{quoteId}/author/{authorId}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<ActionResult<bool>> GetQuoteAndAuthorByIds(int quoteId, int authorId)
        {
            var quote = await this.quoteRepository.GetByIdAsync(quoteId);

            if (quote == null)
            {
                return NotFound();
            }

            if (quote.AuthorId == authorId)
            {
                return Ok(true);
            }

            return Ok(false);
        }
    }
}
