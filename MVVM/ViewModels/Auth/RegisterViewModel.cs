using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;

namespace MVVM_SQLSERVER.ViewModels.Auth
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        // Propiedades con validación
        [ObservableProperty]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MinLength(3, ErrorMessage = "Mínimo 3 caracteres")]
        private string nombre;

        [ObservableProperty]
        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        private string apellidoPaterno;

        [ObservableProperty]
        private string apellidoMaterno;

        [ObservableProperty]
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        private DateTime fechaNacimiento = DateTime.Now.AddYears(-18);

        [ObservableProperty]
        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "Teléfono inválido")]
        private string telefono;

        [ObservableProperty]
        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        private string email;

        [ObservableProperty]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "Mínimo 6 caracteres")]
        private string password;

        [ObservableProperty]
        [Required(ErrorMessage = "Confirma tu contraseña")]
        private string confirmPassword;

        public DateTime MaxDate => DateTime.Now.AddYears(-18);

        public RegisterViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private async Task Register()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                await ShowValidationErrors();
                return;
            }

            if (password != ConfirmPassword)
            {
                await Shell.Current.DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
                return;
            }

            if (IsBusy) return;

            try
            {
                IsBusy = true;

                var user = new User
                {
                    Nombre = nombre,
                    ApellidoPaterno = apellidoPaterno,
                    ApellidoMaterno = apellidoMaterno,
                    FechaNacimiento = fechaNacimiento,
                    Telefono = telefono,
                    Email = email,
                    Password = password
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

        private async Task ShowValidationErrors()
        {
            var errors = this.GetErrors()
                .Select(e => e.ErrorMessage)
                .Where(m => !string.IsNullOrEmpty(m));

            await Shell.Current.DisplayAlert("Error", string.Join("\n", errors), "OK");
        }
    }
}