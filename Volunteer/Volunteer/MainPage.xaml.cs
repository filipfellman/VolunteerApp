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
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
        //private NavigationPage navigationPage = new NavigationPage();

        private void LoginClicked(object sender, EventArgs args)
        {
            try
            {
                Console.WriteLine("LOGIN clicked!");
                DisplayAlert("TBI", "To be implemented...", "Oh, okay...");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void RegisterClicked(object sender, EventArgs args)
        {
            try
            {
                Console.WriteLine("Registed clicked!");
                DisplayAlert("TBI", "To be implemented...", "Oh, okay...");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       private void ContinueClicked(object sender, EventArgs args)
        {
            try
            {
                Console.WriteLine("Continue clicked!");
                DisplayAlert("TBI", "To be implemented...", "Oh, okay...");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
