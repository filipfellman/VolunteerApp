using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Volunteer
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private NavigationPage navigationPage = new NavigationPage();

        private async void VolunteerClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VolunteerPage());
            Console.WriteLine("Volunteer clicked!");
        }

        private async void OrganizationClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Voluntary Organization clicked!");
            await Navigation.PushAsync(new OrganizationPage());
        }
    }
}
