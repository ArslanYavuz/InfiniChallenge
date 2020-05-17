using System;
using System.Collections.Generic;
using System.Text;

namespace InfiniDemo.Models.RequestModels
{
    public class LoginRequestParameter
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string VerificationCode { get; set; }
    }
}
