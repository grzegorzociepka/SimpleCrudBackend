using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdEngine.API.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }  
        public string firstName { get; set; }   
        public string secondName { get; set; }
        public string password { get; set; }
    }
}
