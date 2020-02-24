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

        private async void LoginClicked(object sender, EventArgs args)
        {
            try
            {
                Console.WriteLine("LOGIN clicked!");
                await Navigation.PushAsync(new LoginPage());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async void RegisterClicked(object sender, EventArgs args)
        {
            try
            {
                Console.WriteLine("Registed clicked!");
                await Navigation.PushAsync(new RegisterPage());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       private async void ContinueClicked(object sender, EventArgs args)
        {
            try
            {
                Console.WriteLine("Continue clicked!");
                await Navigation.PushAsync(new VolunteerPage());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
