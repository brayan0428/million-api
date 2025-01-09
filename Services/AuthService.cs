using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitWork;

        public AuthService(IUnitOfWork unitWork)
        {
            _unitWork = unitWork;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            try
            {
                var user = (await _unitWork.UserRepository.GetAsync(u => u.Email == email)).FirstOrDefault();
                if(user == null)
                {
                    throw new Exception("User not found");
                }
                if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    throw new UnauthorizedAccessException("Invalid credentials");
                }
                return user;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
