using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.ViewModels.Auth
{
    public partial class AuthSelectionViewModel : ObservableObject
    {
        [RelayCommand]
        private async Task GoToLogin()
        {
            await Shell.Current.GoToAsync("//Login");
        }

        [RelayCommand]
        private async Task GoToRegister()
        {
            await Shell.Current.GoToAsync("//Register");
        }
    }

}
