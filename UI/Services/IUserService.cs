using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;

namespace UI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAsync();
        Task<UserReadDto> GetAsync(int id);
        Task PostAsync(UserCreateDto user);
        Task PutAsync(int id, UserCreateDto user);
        Task DeleteAsync(int id);
    }
}
