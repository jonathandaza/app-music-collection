﻿using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> SearchAsync(string name, string genre, int? age);
        Task<IEnumerable<User>> GetAsync();
        Task<User> GetAsync(int id);
        Task<User> GetByNameAsync(string name);
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);

    }
}
