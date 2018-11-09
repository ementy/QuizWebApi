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
	/// <summary>
	/// Offers the basic CRUD operations with the Author entity type
	/// </summary>
    [Route("QuizApi/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository repository;

		/// <summary>
		/// The constructor of the author contoller
		/// </summary>
		/// <param name="repository">Receives the Author Repository.</param>
        public AuthorController(IAuthorRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
		/// Gets all values
		/// </summary>
		/// <returns></returns>
        [HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<IQueryable<AuthorViewModel>>> GetAllAuthors()
        {
            IQueryable<Author> authorsResult = await this.repository.GetAllAsync();

            IQueryable<AuthorViewModel>  authors = authorsResult.Select(x => new AuthorViewModel
            {
                FullName = x.FullName
            });

            if (!authors.Any())
            {
                return NotFound();
            }

            return Ok(authors);
        }

        /// <summary>
		/// Gets a value by Id.
		/// </summary>
		/// <param name="id">The Id of the entity you wish to get.</param>
		/// <returns></returns>
        [HttpGet("{id}", Name = "GetAuthorById")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
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

        /// <summary>
		/// Creates a new entity.
		/// </summary>
		/// <param name="model">requires model</param>
		/// <returns></returns>
        [HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
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

			//the server returns 500 Internal Server Error if an author with the same name already exists 
			//Check if there is already an author with the same Full Name
			Author authorWithNameExists = this.repository.GetAll().FirstOrDefault(x => x.FullName == model.FullName);

			if (authorWithNameExists != null)
			{
				//TODO: move text to consts file
				return BadRequest("Author already exists.");
			}
            await this.repository.AddAsync(newAuthor);

            return CreatedAtAction(nameof(GetAuthorById), new { id = newAuthor.Id }, newAuthor);
        }

        /// <summary>
		/// Updates an item with the provided id.
		/// </summary>
		/// <param name="id">The id of the entity you wish to update/modify.</param>
		/// <param name="model">The content you wish to change.</param>
		/// <returns></returns>
        [HttpPut("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorUpdateViewModel model)
        {
            //TODO: Add validation for Id > 0
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Done: add check if the author already exists

            var author = await this.repository.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            author.FullName = model.FullName;

            await this.repository.UpdateAsync(author);

            return NoContent();

        }

        /// <summary>
		/// Deletes an entity with the provided Id.
		/// </summary>
		/// <param name="id">The id of the entity you wish to delete.</param>
		/// <returns></returns>
        [HttpDelete("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
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

        /// <summary>
		/// Gets a random entity.
		/// </summary>
		/// <returns>Returns the Full Name property of a random author.</returns>
        [HttpGet("Random")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<ActionResult<AuthorRandomViewModel>> Random()
        {
            //TODO: Add validation for ID > 0

            IQueryable<int> authorsIds = this.repository.GetAll().Select(x => x.Id);
            var listAuthorIds = authorsIds.ToList();
            
            Random rnd = new Random();
            int randomIndex = rnd.Next(listAuthorIds.Count);

            var id = listAuthorIds[randomIndex];

            Author author = await this.repository.GetByIdAsync(id);

            if (author == null)
            {
                return BadRequest();
            }

            var resultAuthor = new AuthorRandomViewModel
            {
                Id = author.Id,
                FullName = author.FullName
            };

            return Ok(resultAuthor);
        }
    }
}
