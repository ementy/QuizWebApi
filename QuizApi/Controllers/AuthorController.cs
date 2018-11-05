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

            var authorsTaskResult = await this.repository.GetAllAsync();

            authors = authorsTaskResult.Select(x => new AuthorViewModel
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

            return CreatedAtAction(nameof(GetAuthorById), new { id = newAuthor.Id }, newAuthor);
        }

        // PUT: api/Author/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AuthorUpdateViewModel model)
        {
            //TODO: Add validation for Id > 0
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
    }
}
