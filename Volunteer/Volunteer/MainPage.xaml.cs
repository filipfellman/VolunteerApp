using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
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
            var loginProvider = DependencyService.Get<ILoginProvider>();
            IsBusy = true;
            var authInfo = await loginProvider.LoginAsync();
            IsBusy = false;
            if (string.IsNullOrWhiteSpace(authInfo.AccessToken) || !authInfo.IsAuthorized)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Error", "The app can't authenticate you", "OK");
                });
            }
            else
            {
                //TODO: Save the access and refresh tokens somewhere secure

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(authInfo.IdToken);
                var claims = jsonToken?.Payload?.Claims;

                var name = claims?.FirstOrDefault(x => x.Type == "name")?.Value;
                var email = claims?.FirstOrDefault(x => x.Type == "email")?.Value;
                var preferredUsername = claims?
                    .FirstOrDefault(x => x.Type == "preferred_username")?.Value;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushAsync(new AuthInfoPage(name, email, preferredUsername));
                });
            }
        }

        private async void LogoutClicked(object sender, EventArgs args)
        {
            try
            {
                Console.WriteLine("LOGOUT clicked!");
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
