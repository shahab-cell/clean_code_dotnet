using CleanCode.Application.Services.ServiceInterface;
using CleanCode.Domain.DTO;
using CleanCode.Interface.Application;
using CleanCode.Interface.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Application.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordCoder passwordCoder;
        public UserApplication(IUserRepository userRepository, IPasswordCoder passwordCoder) 
        {
            this.userRepository = userRepository;
            this.passwordCoder = passwordCoder;
        }

        public async Task<(List<User>, Exception)> RegisterUser(User user)
        {
            // Encrypt Password
            user.Password = passwordCoder.EncryptPassword(user.Password);

            return await userRepository.RegisterUser(user);
        }

        public async Task<(List<User>, Exception)> LoginUser(Login user)
        {
            // Verify Password
            string encPass = passwordCoder.EncryptPassword(user.Password);
            bool IsValid = passwordCoder.VerifyPassword(user.Password, encPass);
            if (IsValid)
            {
                return await userRepository.LoginUser(user, encPass);
            }
            else
            {
                return (null, null);
            }
        }

        public async Task<(List<User>, Exception)> GetAllUser()
        {
            return await userRepository.GetAllUser();
        }
    }
}
