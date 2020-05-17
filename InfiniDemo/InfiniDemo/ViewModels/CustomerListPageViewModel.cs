using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using InfiniDemo.Interfaces;
using InfiniDemo.Models.ResponseModels;
using Prism.Commands;
using Prism.Navigation;
using System.Linq;
using static InfiniDemo.Models.ResponseModels.CustomerListResult;
using Prism.Plugin.Popups;
using Prism.Services.Dialogs;
using InfiniDemo.Services;
using Xamarin.Forms;

namespace InfiniDemo.ViewModels
{
    public class CustomerListPageViewModel : ViewModelBase
    {
        #region Properties

        private List<CustomerData> _allCustomerList;
        public List<CustomerData> AllCustomerList
        {
            get { return _allCustomerList; }
            set { _allCustomerList = value; RaisePropertyChanged(); }
        }

        private IEnumerable<CustomerData> _filteredCustomers;
        public IEnumerable<CustomerData> FilteredCustomers
        {
            get { return _filteredCustomers; }
            set { _filteredCustomers = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<CustomerData> _customerCollection;
        public ObservableCollection<CustomerData> CustomerCollection
        {
            get { return _customerCollection; }
            set { _customerCollection = value; RaisePropertyChanged(); }
        }

        private string _searchText;
        public string SearchString
        {
            get { return _searchText; }
            set { _searchText = value; RaisePropertyChanged(); }
        }

        private int _totalPageCount;
        public int TotalPageCount
        {
            get { return _totalPageCount; }
            set { _totalPageCount = value; RaisePropertyChanged(); }
        }

        private int _currentPageIndex;
        public int CurrentPageIndex
        {
            get { return _currentPageIndex; }
            set { _currentPageIndex = value; RaisePropertyChanged(); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(); }
        }

        private int _currentCount;
        public int CurrentCount
        {
            get { return _currentCount; }
            set { _currentCount = value; RaisePropertyChanged(); }
        }

        #endregion

        #region Services

        private ICustomerService _customerService;
        private INavigationService _navigationService;
        private ICustomDialogService _customDialogService;
        private IDialogService _dialogService;
        #endregion

        #region Commands

        public DelegateCommand AddCustomerCommand { get; set; }
        public DelegateCommand<string> PerformSearchCommand { get; set; }
        public DelegateCommand PreviousPageCommand { get; set; }
        public DelegateCommand NextPageCommand { get; set; }
        public DelegateCommand FirstPageCommand { get; set; }
        public DelegateCommand LastPageCommand { get; set; }

        #endregion

        public CustomerListPageViewModel(INavigationService navigationService,
                                         ICustomerService customerService,
                                         ICustomDialogService customDialogService,
                                         IDialogService dialogService,
                                         ILoginService loginService) 
            : base(navigationService)
        {
            Title = "Kişiler";
            _navigationService = navigationService;
            _customerService = customerService;
            _customDialogService = customDialogService;
            _dialogService = dialogService;
            
            AddCustomerCommand = new DelegateCommand(AddCustomer);
            PerformSearchCommand = new DelegateCommand<string>(PerformSearch);
            PreviousPageCommand = new DelegateCommand(PreviousPage);
            NextPageCommand = new DelegateCommand(NextPage);
            FirstPageCommand = new DelegateCommand(FirstPage);
            LastPageCommand = new DelegateCommand(LastPage);
            Init();
        }

      

        private void AddCustomer()
        {
            _dialogService.ShowDialog("AddCustomerDialog",CloseDialogCallback);
        }

        void CloseDialogCallback(IDialogResult dialogResult)
        {
            var isSaveSuccess = dialogResult.Parameters.GetValue<bool>("Result");
            
            if (isSaveSuccess)
            {
                IsBusy = true;
                CustomerData customerData = new CustomerData();
                customerData = dialogResult.Parameters.GetValue<CustomerData>("Model");
                CurrentPageIndex = 1;
                AllCustomerList.Add(customerData);
                RefreshCustomerData(AllCustomerList);
                IsBusy = false;
            }
        }

        private void PerformSearch(string text)
        {
            if(text.Length >= 4)
            {
                IsBusy = true;

                FilteredCustomers = AllCustomerList.Where(x => x.CardName.Contains(text.ToUpper())).OrderBy(x => x.CardName);

                var pageCount = FilteredCustomers.Count() / 20;
                if (FilteredCustomers.Count() % 20 != 0)
                    pageCount = pageCount + 1;
                TotalPageCount = pageCount;

                CurrentPageIndex = 1;

                RefreshCustomerData(FilteredCustomers);

                IsBusy = false;
            }
            else if (text.Length == 0)
            {
                IsBusy = true;

                CustomerCollection.Clear();
                FilteredCustomers = AllCustomerList;
                RefreshCustomerData(AllCustomerList);

                IsBusy = false;
            }
        }

        private async void Init()
        {
            if (IsNetworkConnected)
            {
                IsBusy = true;

                var customerResult = await _customerService.GetCustomerListAsync();
                AllCustomerList = customerResult.Data;

                //Temp Storage
                if (Application.Current.Properties.ContainsKey("CustomerData"))
                {
                    var list = Application.Current.Properties["CustomerData"] as List<CustomerData>;
                    AllCustomerList.AddRange(list);
                }

                FilteredCustomers = AllCustomerList;
                CurrentPageIndex = 1;
                RefreshCustomerData(FilteredCustomers);

                IsBusy = false;
            }
            else
            {
                await _customDialogService.ShowMessageDialogAsync("Internet Bağlantınız Bulunamamıştır..");
                return;
            }

        }

        private void RefreshCustomerData(IEnumerable<CustomerData> customerList)
        {      

            var pageCount = customerList.Count() / 20;
            if (customerList.Count() % 20 != 0)
                pageCount = pageCount + 1;

            TotalPageCount = pageCount;
            //CurrentPageIndex = 1;

            if (CurrentPageIndex == TotalPageCount)
            {
                int takeCount = 0;
                if (TotalPageCount == 1)
                    takeCount = customerList.Count() % ((CurrentPageIndex) * 20);
                else
                    takeCount = customerList.Count() % ((CurrentPageIndex - 1) * 20);

                CustomerCollection = new ObservableCollection<CustomerData>(customerList.OrderBy(x => x.CardName).Skip((CurrentPageIndex - 1) * 20).Take(takeCount));

            }
            else
                CustomerCollection = new ObservableCollection<CustomerData>(customerList.OrderBy(x => x.CardName).Skip((CurrentPageIndex - 1) * 20).Take(20));


            CurrentCount = customerList.Count();
        }


        private void PreviousPage()
        {
            IsBusy = true;

            if(CurrentPageIndex >= 2)
                CurrentPageIndex = CurrentPageIndex - 1;

            RefreshCustomerData(FilteredCustomers);

            IsBusy = false;
        }

        private void NextPage()
        {
            IsBusy = true;
            if (CurrentPageIndex <= TotalPageCount -1 )
            {
                CurrentPageIndex = CurrentPageIndex + 1;
            }

            RefreshCustomerData(FilteredCustomers);

            IsBusy = false;
        }

        private void FirstPage()
        {
            IsBusy = true;

            CurrentPageIndex = 1;
            RefreshCustomerData(FilteredCustomers);

            IsBusy = false;
        }

        private void LastPage()
        {
            IsBusy = true;

            CurrentPageIndex = TotalPageCount;
            RefreshCustomerData(FilteredCustomers);

            IsBusy = false;
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
