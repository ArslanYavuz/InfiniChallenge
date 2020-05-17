using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfiniDemo.Views.DialogViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddCustomerDialog : Frame
	{
		public AddCustomerDialog ()
		{
			InitializeComponent ();
		}
	}
}