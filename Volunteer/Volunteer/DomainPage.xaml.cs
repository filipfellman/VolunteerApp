using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Volunteer
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DomainPage : ContentPage
	{
		public DomainPage ()
		{
			InitializeComponent ();
            Console.WriteLine("DomainPage reached!");
		}
	}
}