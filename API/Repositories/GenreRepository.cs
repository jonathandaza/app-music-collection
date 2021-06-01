using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AppDbContext _appDbContext;
        public GenreRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Genre>> GetAsync()
        {
            return await _appDbContext.Genres.ToListAsync();
        }

        public async Task<Genre> GetAsync(int id)
        {
            return await _appDbContext.Genres.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
