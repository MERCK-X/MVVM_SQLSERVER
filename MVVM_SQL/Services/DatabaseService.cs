using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_SQL.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVVM_SQL.Data;
using System.Net.Http.Json;

namespace MVVM_SQL.Services
{
    public class DatabaseService
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly string apiUrl = "https://tudominio.com/api/usuarios"; // Reemplaza con tu URL


        public DatabaseService()
        {
            _context = new AppDbContext();
            _context.Database.EnsureCreated();
            _httpClient = new HttpClient();
        }

        // ✅ Obtener todos los usuarios
        public async Task<List<UserModel>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }


        public async Task SyncToServerAsync()
        {
            var users = await GetUsersAsync();
            foreach (var user in users)
            {
                // Enviar datos a SQL Server
            }
        }

        // ✅ Agregar usuario
        public async Task<bool> AddUserLocalAsync(UserModel user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
        // ✅ Enviar usuario a SQL Server
        public async Task<bool> AddUserToServerAsync(UserModel user)
        {
            var response = await _httpClient.PostAsJsonAsync(apiUrl, user);
            return response.IsSuccessStatusCode;
        }


        // ✅ Actualizar usuario
        public async Task<bool> UpdateUserAsync(UserModel user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ Eliminar usuario
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // ✅ Sincronizar datos: Manda SQLite → SQL Server
        public async Task SyncUsersToServerAsync()
        {
            var localUsers = await GetUsersAsync();
            foreach (var user in localUsers)
            {
                bool success = await AddUserToServerAsync(user);
                if (success)
                {
                    _context.Users.Remove(user);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
