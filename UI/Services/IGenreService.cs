using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;

namespace UI.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreReadDto>> GetAsync();
    }
}
