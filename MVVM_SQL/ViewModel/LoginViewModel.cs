using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVM_SQL.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_SQL.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly AuthService _authService;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            _authService = new AuthService();
            LoginCommand = new RelayCommand(async () => await Login());
        }

        private async Task Login()
        {
            var user = await _authService.LoginUser(Email, Password);
            if (user != null)
            {
                // Navegar a MainPage
            }
            else
            {
                // Mostrar error
            }
        }
    }
}
