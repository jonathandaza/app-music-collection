using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;
using DTO;

namespace UI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserService> _logger;

        private const string urlUsers = "/api/users";

        public UserService(HttpClient httpClient,
                           ILogger<UserService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<IEnumerable<UserReadDto>> GetAsync()
        {
            return await _httpClient.GetFromJsonAsync<UserReadDto[]>(urlUsers);
        }

        public async Task<UserReadDto> GetAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<UserReadDto>($"{urlUsers}/{id}");
        }

        public async Task PostAsync(UserCreateDto user)
        {
            var response = await _httpClient.PostAsJsonAsync(urlUsers, user);
            if (!response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error calling server {responseString}");
            }
        }

        public async Task PutAsync(int id, UserCreateDto user)
        {
            var response = await _httpClient.PutAsJsonAsync($"{urlUsers}/{id}", user);
            if (!response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error calling server {responseString}");
            }
        }

        public async Task DeleteAsync(int id)
        {           
            var response = await _httpClient.DeleteAsync($"{urlUsers}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error calling server {responseString}");
            }
        }
    }
}
