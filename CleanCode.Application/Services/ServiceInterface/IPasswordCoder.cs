using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Application.Services.ServiceInterface
{
    public interface IPasswordCoder
    {
        string EncryptPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
