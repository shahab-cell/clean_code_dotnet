﻿using CleanCode.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Interface.Application
{
    public interface IUserApplication
    {
        Task<(List<User>, Exception)> RegisterUser(User user);
        Task<(List<User>, Exception)> LoginUser(Login user);
        Task<(List<User>, Exception)> GetAllUser();
    }
}
