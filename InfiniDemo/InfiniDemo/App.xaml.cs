using Prism;
using Prism.Ioc;
using InfiniDemo.ViewModels;
using InfiniDemo.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using InfiniDemo.Interfaces;
using InfiniDemo.Services;
using Prism.Services;
using Prism.Services.Dialogs;
using InfiniDemo.Views.DialogViews;
using InfiniDemo.ViewModels.DialogViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace InfiniDemo
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var pageDialogService = Container.Resolve<IPageDialogService>();

            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterInstance(typeof(IBaseService), new BaseService());
            containerRegistry.RegisterInstance(typeof(ILoginService), new LoginService(new BaseService()));
            containerRegistry.RegisterInstance(typeof(ICustomDialogService), new CustomDialogService(pageDialogService));
            containerRegistry.RegisterInstance(typeof(ICustomerService), new CustomerService(new BaseService()));

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterDialog<AddCustomerDialog, AddCustomerDialogViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<MasterPage, MasterPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<CustomerListPage, CustomerListPageViewModel>();
            
        }
    }
}
