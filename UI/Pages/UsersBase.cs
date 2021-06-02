using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Services;
using DTO;

namespace UI.Pages
{
    public class UsersBase : ComponentBase
    {
        [Inject]
        private IUserService UserService { get; set; }


        protected IEnumerable<UserReadDto> users;

        protected override async Task OnInitializedAsync()
        {
            users = await UserService.GetAsync();
        }
    }
}
