using Microsoft.EntityFrameworkCore;
using MVVM.Data;
using MVVM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Services
{
    public class AuthService : IAuthService
    {
        private readonly IApiService _apiService;
        private readonly LocalDbContext _localDb;

        public AuthService(IApiService apiService, LocalDbContext localDb)
        {
            _apiService = apiService;
            _localDb = localDb;
        }

        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                return await _apiService.LoginAsync(email, password);
            }
            else
            {
                var user = await _localDb.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
                return user != null ?
                    AuthResult.Success("Offline login") :
                    AuthResult.Fail("Credenciales incorrectas");
            }
        }
    }
}
