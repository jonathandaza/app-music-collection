using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using UI.Services;

namespace UI.Pages
{
    public class UserDeleteBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IUserService UserService { get; set; }
        

        protected async Task Delete()
        {
            await UserService.DeleteAsync(Id);
            Return();
        }

        protected void Return() =>
            NavigationManager.NavigateTo("/");
    }
}
