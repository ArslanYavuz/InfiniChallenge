using System;
using System.Collections.Generic;
using System.Text;
using InfiniDemo.Interfaces;
using InfiniDemo.Models;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Prism.Commands;
using Prism.Navigation;

namespace InfiniDemo.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Properties

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; RaisePropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged(); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(); }
        }

        #endregion

        #region Commands 

        public DelegateCommand LoginCommand { get; set; }

        #endregion

        #region Services

        ILoginService _loginService;
        INavigationService _navigationService;
        ICustomDialogService _dialogService;

        #endregion

        public LoginPageViewModel(INavigationService navigationService,ILoginService loginService, ICustomDialogService dialogService) : base(navigationService)
        {
            _loginService = loginService;
            _navigationService = navigationService;
            _dialogService = dialogService;

            IsBusy = false;

            LoginCommand = new DelegateCommand(DoLogin);
        }


        private async void DoLogin()
        {
            #region Validation Controls

            if (!IsNetworkConnected)
            {
                await _dialogService.ShowMessageDialogAsync("Internet Bağlantınız Bulunamamıştır.");
                return;
            }

            else if (string.IsNullOrWhiteSpace(Username))
            {
                await _dialogService.ShowMessageDialogAsync("Kullanıcı Adı Alanı Boş Bırakılamaz..");
                return;
            }

            else if (string.IsNullOrWhiteSpace(Password))
            {
                await _dialogService.ShowMessageDialogAsync("Şifre Alanı Boş Bırakılamaz");
                return;
            }

            #endregion

            IsBusy = true;

            var loginResult = await _loginService.LoginAsync(Username,Password,string.Empty);

            if(loginResult != null)
            {
                if (loginResult.Success)
                {
                    GlobalAppContext.UserInfo = await _loginService.GetUserMobileAsync();
                    IsBusy = false;
                    await NavigationService.NavigateAsync(new Uri("http://www.brianlagunas.com/MasterPage/NavigationPage/CustomerListPage", UriKind.Absolute)); // Using Absolute URI Documentation
                }
                else
                {
                    await _dialogService.ShowMessageDialogAsync(loginResult.ErrorMsg);
                    IsBusy = false;
                }
            }
     
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
    }
}
