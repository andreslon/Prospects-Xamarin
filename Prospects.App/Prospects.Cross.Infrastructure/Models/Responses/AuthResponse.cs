using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospects.Cross.Infrastructure.Models.Responses
{
    public class AuthResponse
    {
        public bool success { get; set; }
        public string authToken { get; set; }
        public string email { get; set; }
        public string zone { get; set; }
        public int code { get; set; }
        public string error { get; set; }  
    }
}
