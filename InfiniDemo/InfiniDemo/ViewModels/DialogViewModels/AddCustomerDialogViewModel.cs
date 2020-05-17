using InfiniDemo.Interfaces;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using static InfiniDemo.Models.ResponseModels.CustomerListResult;

namespace InfiniDemo.ViewModels.DialogViewModels
{
    public class AddCustomerDialogViewModel : BindableBase,IDialogAware, IAutoInitialize
    {
        #region Properties

        private string _firstname;
        public string Firstname
        {
            get { return _firstname; }
            set { _firstname = value; RaisePropertyChanged(); }
        }

        private string _lastName;
        public string Lastname
        {
            get { return _lastName; }
            set { _lastName = value; RaisePropertyChanged(); }
        }

        private string _locationName;
        public string LocationName
        {
            get { return _locationName; }
            set { _locationName = value; RaisePropertyChanged(); }
        }

        private string _professionCode;
        public string ProfessionCode
        {
            get { return _professionCode; }
            set { _professionCode = value; RaisePropertyChanged(); }
        }

        #endregion

        #region Commands

        public DelegateCommand SaveCustomerCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }

        #endregion

        #region Event Action

        public event Action<IDialogParameters> RequestClose;

        #endregion

        #region Services

        ICustomDialogService _customDialogService;

        #endregion

        public AddCustomerDialogViewModel(ICustomDialogService customDialogService)
        {
            _customDialogService = customDialogService;

            SaveCustomerCommand = new DelegateCommand(SaveCustomer);
            CloseCommand = new DelegateCommand(CloseDialog);
        }

        private void CloseDialog()
        {
            IDialogParameters parameters = new DialogParameters();
            parameters.Add("Result", false);
            parameters.Add("Model", null);
            RequestClose(parameters);
        }

        private async void SaveCustomer()
        {
            if (string.IsNullOrEmpty(Firstname))
            {
                await _customDialogService.ShowMessageDialogAsync("Isim Alanı Boş Bırakılamaz.");
                return;
            }
            if (string.IsNullOrEmpty(Lastname))
            {
                await _customDialogService.ShowMessageDialogAsync("Soyad Alanı Boş Bırakılamaz.");
                return;
            }
            if (string.IsNullOrEmpty(ProfessionCode))
            {
                await _customDialogService.ShowMessageDialogAsync("Uzmanlık Alanı Boş Bırakılamaz.");
                return;
            }
            if (string.IsNullOrEmpty(LocationName))
            {
                await _customDialogService.ShowMessageDialogAsync("Kurum Alanı Boş Bırakılamaz.");
                return;
            }

            var confResult = await _customDialogService.ShowConfirmationDialogAsync(Firstname + " " + Lastname +" Kişisini Eklemek Istediğinize Emin Misiniz ?");
            if (confResult == Models.ConfirmationDialogResult.Accepted)
            {
                
                CustomerData customerData = new CustomerData();
                customerData.CardName = Firstname + " " + Lastname;
                customerData.LocationName = LocationName;
                customerData.CardProfessionCode = ProfessionCode;

                if (Application.Current.Properties.ContainsKey("CustomerData"))
                {
                    var list = Application.Current.Properties["CustomerData"] as List<CustomerData>;

                    list.Add(customerData);
                    Application.Current.Properties["CustomerData"] = list;
                }
                else
                {
                    List<CustomerData> localCustomerList = new List<CustomerData>();
                    localCustomerList.Add(customerData);

                    Application.Current.Properties.Add("CustomerData", localCustomerList);
                }

                IDialogParameters parameters = new DialogParameters();
                parameters.Add("Result",true);
                parameters.Add("Model", customerData);

                RequestClose(parameters);
            }
            else
                return;

        }


        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
    }
}
