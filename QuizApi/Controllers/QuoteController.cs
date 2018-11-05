using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Data.Repositories.Contracts;

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        //Repo as private field and initiation in the constructor
        private IQuoteRepository quoteRepository;

        public QuoteController(IQuoteRepository quoteRepository)
        {
            this.quoteRepository = quoteRepository;
        }

        // GET: api/Quote
        [HttpGet]
        public async Task<IActionResult> GetAllQuotesAsync()
        {
            var data = await this.quoteRepository.GetAllAsync();
            return Ok(data);
        }

        // GET: api/Quote/5
        [HttpGet("{id}", Name = "GetQuoteById")]
        public IActionResult GetQuoteById(int id)
        {
            var quote = this.quoteRepository.GetById(id);

            if (quote == null)
            {
                return NotFound();
            }

            return Ok(quote);
        }

        // POST: api/Quote
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Quote/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
