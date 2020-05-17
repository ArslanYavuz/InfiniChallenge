using InfiniDemo.Interfaces;
using InfiniDemo.Models.RequestModels;
using InfiniDemo.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;

namespace InfiniDemo.Services
{
    public class LoginService : ILoginService
    {
        private IBaseService _baseService;
        public LoginService(IBaseService baseService)
        {
            _baseService = baseService;
        }

       
        public async Task<LoginResult> LoginAsync(string username, string password, string verificationCode)
        {
            LoginResult loginResult = new LoginResult();

            LoginRequestParameter loginParams = new LoginRequestParameter();
            loginParams.Username = username;
            loginParams.Password = password;
            loginParams.VerificationCode = verificationCode;

            loginResult = await _baseService.PostAsync<LoginResult>("/Account/Login/", loginParams);
                       
            return loginResult;
        }

        public async Task<UserMobileResult> GetUserMobileAsync()
        {
            UserMobileResult userMobileResult = new UserMobileResult();
            userMobileResult =  await _baseService.GetAsync<UserMobileResult>("/Account/GetUserMobile");

            return userMobileResult;
        }


    }
}
