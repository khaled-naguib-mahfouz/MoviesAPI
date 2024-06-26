﻿using Microsoft.EntityFrameworkCore;

namespace Movies.Services
{
    public class MoviesService:IMoviesService
    {
        private readonly Context _context;

        public MoviesService(Context context)
        {
            _context = context;
        }


        public async Task<Movie> Add(Movie movie)
        {
            await _context.AddAsync(movie);
            _context.SaveChanges();

            return movie;
        }

        public Movie Delete(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(int CategoryId=0)
        {
            return await _context.movies
                .Where(m => m.CategoryId == CategoryId || CategoryId == 0)
                .OrderByDescending(m => m.Rate)
                .Include(m => m.Category)
                .ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.movies.Include(m => m.Category).SingleOrDefaultAsync(m => m.Id == id);
        }

        public Movie Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();

            return movie;
        }
    }
}
