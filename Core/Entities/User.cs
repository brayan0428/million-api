using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty; 
        public string PasswordHash { get; set; } = string.Empty;
    }
}
