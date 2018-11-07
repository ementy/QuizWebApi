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

        // GET: api/Quote
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuoteViewModel>>> GetAllQuotesAsync()
        {
            IEnumerable<Quote> quotesResult = await this.quoteRepository.GetAllAsync();

            if (!quotesResult.Any())
            {
                return NotFound();
            }

            IEnumerable<QuoteViewModel> quotes = quotesResult.Select(x => new QuoteViewModel
            {
                Content = x.Content
            });

            return Ok(quotes);
        }

        // GET: api/Quote/5
        [HttpGet("{id}", Name = "GetQuoteByIdAsync")]
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

        // POST: api/Quote
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

        // PUT: api/Quote/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] QuoteUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var quote = await this.quoteRepository.GetByIdAsync(id);

            quote.Content = model.Content;
            quote.AuthorId = model.AuthorId;

            await this.quoteRepository.UpdateAsync(quote);

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(201)]
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

        //GET: api/quote/random
        [HttpGet("Random")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<QuoteRandomViewModel>> Random()
        {
            //TODO: Add validation for ID > 0

            IQueryable<int> quoteIds = this.quoteRepository.GetAll().Select(x => x.Id);
            
            Random rnd = new Random();
            int randomIndex = rnd.Next(quoteIds.Count());

            Quote quote = await this.quoteRepository.GetByIdAsync(randomIndex);

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


        //Get: api/quote/{id}/author/{id}

        //[HttpGet(Name = "GetQuoteAndAuthorByIds")]
        //[Route("{quoteId}/author/{authorId}")]
        [HttpGet("{quoteId}/author/{authorId}")]
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
