using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiHttp.Services;

namespace MauiHttp.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IHTTPService _httpService;

        public LoginViewModel(IHTTPService service)
        {
            _httpService = service;
        }

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [RelayCommand]
        private async Task Login()
        {
            bool loginSuccess = await _httpService.Login(Email, Password);
            if (loginSuccess)
            {
                // Navigate! 
                await Shell.Current.GoToAsync("//ActorList"); 
            }
            else
            {
                // Show fail message
            }
        
        }
    }
}
