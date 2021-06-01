using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAsync();
        Task<Genre> GetAsync(int id);
    }
}
