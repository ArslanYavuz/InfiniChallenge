using InfiniDemo.Interfaces;
using InfiniDemo.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniDemo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        #region Services

        ILoginService _loginService;

        #endregion


        #region Commands

        public DelegateCommand LoginCommand { get; set; }

        #endregion


        public MainPageViewModel(INavigationService navigationService,ILoginService loginService) : base(navigationService)
        {
            Title = "Ana Sayfa";

            LoginCommand = new DelegateCommand(DoLogin);
            _loginService = loginService;
        }

        private async void DoLogin()
        {
            //ILoginService loginService = new LoginService();
            //_loginService.LoginAsync("demo607", "1234567",string.Empty);
        }
    }
}
