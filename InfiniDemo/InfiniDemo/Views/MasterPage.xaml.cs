using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniDemo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : MasterDetailPage,IMasterDetailPageOptions
	{
		public MasterPage ()
		{
			InitializeComponent ();
        }

        public bool IsPresentedAfterNavigation => false;
    }
}