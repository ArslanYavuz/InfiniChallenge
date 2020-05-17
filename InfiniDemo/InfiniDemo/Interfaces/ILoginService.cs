using InfiniDemo.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfiniDemo.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResult> LoginAsync(string username,string password,string verificationCode);
        Task<UserMobileResult> GetUserMobileAsync();
    }
}
