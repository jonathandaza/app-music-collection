using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;
using UI.Services;
using DTO;

namespace UI.Pages
{
    public class UserBase : ComponentBase
    {
        const string pageHeaderEditUser = "Edit User";
        const string pageHeaderCreateUser = "Create User";
        const int ageByDefault = 1;
        const int genreByDefault = 1;

        [Parameter]
        public int Id { get; set; }
                
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        
        [Inject]
        private ILogger<User> Logger { get; set; }

        [Inject]
        public IGenreService GenreService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }


        private bool IsNew { get { return Id == 0; } }
        protected string PageHeaderText { get { return IsNew ? pageHeaderCreateUser : pageHeaderEditUser; } }

        protected UserCreateDto user = new(ageByDefault, genreByDefault);
        protected IEnumerable<GenreReadDto> genres = new List<GenreReadDto>();


        protected override async Task OnInitializedAsync()
        {
            genres = await GenreService.GetAsync();

            if (!IsNew)            
                user = await UserService.GetAsync(Id);            
        }

        protected async Task FormSubmitted(EditContext editContext)
        {
            bool formIsValid = editContext.Validate();
            if (formIsValid)            
                await SaveOrEdit();            
            else
                Logger.LogError($"Error in validations");            
        }

        private async Task SaveOrEdit()
        {
            if (IsNew)
                await UserService.PostAsync(user);            
            else        
                await UserService.PutAsync(Id, user);

            Return();
        }

        protected void Return() => NavigationManager.NavigateTo("/");
    }
}
