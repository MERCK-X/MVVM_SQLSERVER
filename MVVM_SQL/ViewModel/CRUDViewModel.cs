using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVM_SQL.Model;
using MVVM_SQL.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Net.NetworkInformation;


namespace MVVM_SQL.ViewModel
{
    public partial class CRUDViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private ObservableCollection<UserModel> users;

        [ObservableProperty]
        private UserModel selectedUser = new();

        public CRUDViewModel()
        {
            _databaseService = new DatabaseService();
            Users = new ObservableCollection<UserModel>();
            LoadUsers();
        }

        // ✅ Cargar usuarios
        private async void LoadUsers()
        {
            var userList = await _databaseService.GetUsersAsync();
            Users.Clear();
            foreach (var user in userList)
            {
                Users.Add(user);
            }
        }

        // ✅ Agregar usuario
        [RelayCommand]
        private async Task AddUser()
        {
            if (!string.IsNullOrWhiteSpace(SelectedUser.Nombre) && !string.IsNullOrWhiteSpace(SelectedUser.Correo))
            {
                if (HasInternetConnection())
                {
                    await _databaseService.AddUserToServerAsync(SelectedUser);
                }
                else
                {
                    await _databaseService.AddUserLocalAsync(SelectedUser);
                }

                LoadUsers();
                SelectedUser = new UserModel();
            }
        }

        // ✅ Sincronizar cuando haya Internet
        [RelayCommand]
        private async Task SyncData()
        {
            if (HasInternetConnection())
            {
                await _databaseService.SyncUsersToServerAsync();
                LoadUsers();
            }
        }

        // ✅ Actualizar usuario
        [RelayCommand]
        private async Task UpdateUser()
        {
            if (SelectedUser.Id > 0)
            {
                await _databaseService.UpdateUserAsync(SelectedUser);
                LoadUsers();
            }
        }

        // ✅ Eliminar usuario
        [RelayCommand]
        private async Task DeleteUser()
        {
            if (SelectedUser.Id > 0)
            {
                await _databaseService.DeleteUserAsync(SelectedUser.Id);
                LoadUsers();
                SelectedUser = new UserModel();
            }
        }

        private bool HasInternetConnection()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
    }
}
