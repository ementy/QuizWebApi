using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories.Contracts;
using Entities.DbModels;
using Microsoft.AspNetCore.Mvc;
using QuizApi.ViewModels.Author;

namespace QuizApi.Controllers
{
    [Route("QuizApi/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        //Repo as private field and initiation in the constructor
        private readonly IAuthorRepository repository;

        public AuthorController(IAuthorRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorViewModel>>> GetAllAuthors()
        {
            IEnumerable<AuthorViewModel> authors = null;

            var authorsResult = await this.repository.GetAllAsync();

            authors = authorsResult.Select(x => new AuthorViewModel
            {
                FullName = x.FullName
            });

            if (!authors.Any())
            {
                return NotFound();
            }

            return Ok(authors);
        }

        // GET: api/Author/5
        [HttpGet("{id}", Name = "GetAuthorById")]
        public async Task<ActionResult<AuthorViewModel>> GetAuthorById(int id)
        {
            //TODO: Add validation for ID > 0
            var author = await this.repository.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            var resultAuthor = new AuthorViewModel
            {
                FullName = author.FullName
            };

            return Ok(resultAuthor);
        }

        // POST: api/Author
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newAuthor = new Author
            {
                FullName = model.FullName
            };

            await this.repository.AddAsync(newAuthor);

            //the server returns 500 Internal Server Error if an author with the same name already exists 

            return CreatedAtAction(nameof(GetAuthorById), new { id = newAuthor.Id }, newAuthor);
        }

        // PUT: api/Author/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorUpdateViewModel model)
        {
            //TODO: Add validation for Id > 0
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //TODO: add check if the author already exists

            var author = await this.repository.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            author.FullName = model.FullName;

            await this.repository.UpdateAsync(author);

            return NoContent();

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //TODO: Add validation for Id < 0
            var author = await this.repository.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            await this.repository.DeleteAsync(author);

            return NoContent();
        }

        // GET: api/Author/Random
        [HttpGet("Random")]
        public async Task<ActionResult<AuthorRandomViewModel>> Random()
        {
            //TODO: Add validation for ID > 0

            //Move this logic to the IAuthor repository?
            IEnumerable<Author> authors = await this.repository.GetAllAsync();
            IEnumerable<int> authorIds = authors.Select(x => x.Id);
            
            Random rnd = new Random();
            int randomIndex = rnd.Next(authorIds.Count());

            Author author = authors.ElementAt(randomIndex);

            var resultAuthor = new AuthorRandomViewModel
            {
                Id = author.Id,
                FullName = author.FullName
            };

            return Ok(resultAuthor);
        }
    }
}
