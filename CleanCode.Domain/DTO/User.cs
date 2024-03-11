using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Domain.DTO
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
