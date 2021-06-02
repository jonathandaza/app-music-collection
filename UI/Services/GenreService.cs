using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;
using DTO;

namespace UI.Services
{
    public class GenreService : IGenreService
    {
        private readonly HttpClient _httpClient;

        const string urlGenres = "/api/genres";

        public GenreService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<GenreReadDto>> GetAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<GenreReadDto>>(urlGenres);
        }
    }
}
