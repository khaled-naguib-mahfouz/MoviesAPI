
using Microsoft.EntityFrameworkCore;

namespace Movies.Services
{
    public class categoryService : IcategoryService
    {
        private readonly Context _context;

        public categoryService(Context context)
        {
            _context = context;
        }

        public async Task<Category> Add(Category category)
        {
            await _context.AddAsync(category);
            _context.SaveChanges();

            return category;
        }

        public async Task<Category> Delete(Category category)
        {
            _context.Remove(category);
            _context.SaveChanges();

            return category;
        }

        public async Task<Category> Get(int id)
        {
            return await _context.categories.SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.categories.OrderBy(g => g.Name).ToListAsync();
        }

        public Task<bool> IsValidCategory(int id)
        {
            return _context.categories.AnyAsync(g => g.Id == id);
        }

        public async Task<Category> Update(Category category)
        {
            _context.Update(category);
            _context.SaveChanges();
            return category;
        }
    }
}
