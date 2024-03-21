﻿using CleanCode.Domain.DTO;
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
        public UserApplication(IUserRepository userRepository) 
        {
            this.userRepository = userRepository;
        }

        public async Task<(List<User>, Exception)> PostUser(User user)
        {
            return await userRepository.PostUser(user);
        }

        public async Task<(List<User>, Exception)> GetAllUser()
        {
            return await userRepository.GetAllUser();
        }
    }
}
