using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfiniDemo.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isNetworkConnected;
        public bool IsNetworkConnected
        {
            get { return _isNetworkConnected; }
            set { _isNetworkConnected = value; RaisePropertyChanged(); }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;

            IsNetworkConnected = CrossConnectivity.Current.IsConnected;
            CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            CrossConnectivity.Current.ConnectivityChanged -= ConnectivityChanged;
        }

        private void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (IsNetworkConnected != e.IsConnected)
                IsNetworkConnected = e.IsConnected;
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
