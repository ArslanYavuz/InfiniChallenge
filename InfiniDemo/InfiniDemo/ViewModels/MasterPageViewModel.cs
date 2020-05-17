using System;
using System.Collections.Generic;
using System.Text;
using InfiniDemo.Interfaces;
using InfiniDemo.Models;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace InfiniDemo.ViewModels
{
    public class MasterPageViewModel : ViewModelBase
    {
        #region Properties

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; RaisePropertyChanged(); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; RaisePropertyChanged(); }
        }

        private string _positionDesc;
        public string PositionDesc
        {
            get { return _positionDesc; }
            set { _positionDesc = value; RaisePropertyChanged(); }
        }

        #endregion 


        #region Services

        INavigationService _navigationService;
        ILoginService _loginService;
        ICustomDialogService _dialogService;

        #endregion

        #region Commands

        public DelegateCommand<string> NavigateCommand { get; set; }

        #endregion

        public MasterPageViewModel(INavigationService navigationService, ILoginService loginService, ICustomDialogService dialogService) : base(navigationService)
        {
            _navigationService = navigationService;
            _loginService = loginService;
            _dialogService = dialogService;

            NavigateCommand = new DelegateCommand<string>(Navigate);

            GetUserMobile();
        }

        public async void GetUserMobile()
        {
            if (!IsNetworkConnected)
            {
                await _dialogService.ShowMessageDialogAsync("Internet Bağlantınız Bulunamamıştır.");
                return;
            }


            FullName = GlobalAppContext.UserInfo.Data.FullName;
            Email = GlobalAppContext.UserInfo.Data.EMail;
            PositionDesc = GlobalAppContext.UserInfo.Data.PositionDescription;
        }

        private async void Navigate(string page)
        {
            if (page.Contains("Login"))
            {
                var confResult = await _dialogService.ShowConfirmationDialogAsync("Çıkış Yapmak Istediğinize Emin Misiniz ? ");

                if(confResult == ConfirmationDialogResult.Accepted)
                {
                    Application.Current.Properties.Clear();
                    await _navigationService.NavigateAsync(new Uri(page, UriKind.Relative));
                }
            }
            else
                await _navigationService.NavigateAsync(new Uri(page, UriKind.Relative));
        }
    }
}
