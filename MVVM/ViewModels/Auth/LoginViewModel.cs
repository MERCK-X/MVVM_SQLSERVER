using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.ViewModels.Auth
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string email;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string password;

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task Login()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                var result = await _authService.LoginAsync(Email, Password);

                if (result.Success)
                {
                    await Shell.Current.GoToAsync("//Dashboard");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", result.Message, "OK");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool CanLogin() => !string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password);

        [RelayCommand]
        private async Task ForgotPassword()
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                await Shell.Current.DisplayAlert("Error", "Ingrese su correo electrónico", "OK");
                return;
            }

            var result = await _authService.ForgotPasswordAsync(email);
            await Shell.Current.DisplayAlert(
                result.Success ? "Éxito" : "Error",
                result.Message,
                "OK");
        }

        [RelayCommand]
        private async Task GoToRegister()
        {
            await Shell.Current.GoToAsync("//Register");
        }
    }
}
