using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Migrations;
using Movies.Models;
using Movies.Services;

namespace Movies.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MoviesController : ControllerBase
	{
		private readonly IMoviesService _moviesService;
		private readonly IcategoryService _categoryService;
		private new List<string> _AllowedExtensions =
			new List<string> { ".jpg", ".png" };

		public MoviesController(IMoviesService movies, IcategoryService icategoryService )
		{
			_moviesService = movies;
			_categoryService = icategoryService;
			
		}
		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var Movies = await _moviesService.GetAll();
			return Ok(Movies);
		}
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{

            var movie = await _moviesService.GetById(id);
			if(movie == null) { return NotFound(); }
		
            return Ok(movie);
        }
		[HttpGet("Category/{categoryId:int}")]
		public async Task<IActionResult> GetByCategoryId(int categoryId)
		{
			var movies = await _moviesService.GetAll(categoryId);
			return Ok(movies);
        }


		[HttpPost]
		public async Task<IActionResult> CreateAsync([FromForm] MovieDto dto)
		{
			if (!_AllowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower())) {
				return BadRequest("Only .jpg and .png are allowed");
			}
            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);
            var isValidCategory = await _categoryService.IsValidCategory(dto.CategoryId);
			if (!isValidCategory) { return BadRequest("not Valid Category"); }
			var movie = new Movie
			{
				CategoryId = dto.CategoryId,
				Title = dto.Title,
				Poster = dataStream.ToArray(),
				Rate = dto.Rate,
				StoryLine = dto.StoryLine,



			};
			await _moviesService.Add(movie);
			return Ok(movie);
		}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] MovieDto dto)
        {
            var movie = await _moviesService.GetById(id);

            if (movie == null)
                return NotFound($"No movie was found with ID {id}");

            var isValidGenre = await _categoryService.IsValidCategory(dto.CategoryId);

            if (!isValidGenre)
                return BadRequest("Invalid genere ID!");

            if (dto.Poster != null)
            {
                if (!_AllowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                {
                    return BadRequest("Only .jpg and .png are allowed");
                }


                using var dataStream = new MemoryStream();

                await dto.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();
            }

            movie.Title = dto.Title;
            movie.CategoryId = dto.CategoryId;
            movie.Year = dto.Year;
            movie.StoryLine = dto.StoryLine;
            movie.Rate = dto.Rate;

            _moviesService.Update(movie);

            return Ok(movie);
        }
        [HttpDelete("Delete/{id:int}")]
		public async Task<IActionResult> DeleteMovieAsync(int id)
		{
			var movie = await _moviesService.GetById(id);
			if (movie == null)
			{
				return NotFound();
			}

			_moviesService.Delete(movie);


			return Ok(movie);
		}


	}
}
