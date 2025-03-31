using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.ViewModels.Auth
{
    public partial class RegisterViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string nombre;

        [ObservableProperty]
        private string apellidoPaterno;

        [ObservableProperty]
        private string apellidoMaterno;

        [ObservableProperty]
        private DateTime fechaNacimiento = DateTime.Now.AddYears(-18);

        [ObservableProperty]
        private string telefono;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string confirmPassword;

        public DateTime MaxDate => DateTime.Now.AddYears(-18);

        public RegisterViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private async Task Register()
        {
            if (Password != confirmPassword)
            {
                await Shell.Current.DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
                return;
            }

            try
            {
                IsBusy = true;

                var user = new User
                {
                    Nombre = Nombre,
                    ApellidoPaterno = ApellidoPaterno,
                    ApellidoMaterno = ApellidoMaterno,
                    FechaNacimiento = FechaNacimiento,
                    Telefono = Telefono,
                    Email = Email,
                    Password = Password
                };

                var result = await _authService.RegisterAsync(user);

                if (result.Success)
                {
                    await Shell.Current.DisplayAlert("Éxito", "Cuenta creada correctamente", "OK");
                    await Shell.Current.GoToAsync("//Login");
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

        [RelayCommand]
        private async Task GoToLogin()
        {
            await Shell.Current.GoToAsync("//Login");
        }
    }
}
