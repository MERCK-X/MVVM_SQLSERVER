using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_SQL.Data;
using MVVM_SQL.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MVVM_SQL.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService()
        {
            _context = new AppDbContext();
            _context.Database.EnsureCreated();
        }

        public async Task<bool> RegisterUser(UserModel user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserModel> LoginUser(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Correo == email && u.Password == password);
        }
    }
}
