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
    public partial class OrganizationPage : ContentPage
    {
        public OrganizationPage()
        {
            InitializeComponent();
            Console.WriteLine("Organization Page reached!");
        }
        void RegisterButton_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}