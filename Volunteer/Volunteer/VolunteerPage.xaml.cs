using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Volunteer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VolunteerPage : ContentPage
    {
        public VolunteerPage()

        {
            InitializeComponent();
            Console.WriteLine("Volunteer Page reached!");
        }

        private async void ProjectsClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Projects clicked!");
            await Navigation.PushAsync(new ProjectPage());

        }

        private async void OrganizationsClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Organizations clicked!");
            await Navigation.PushAsync(new OrgPage());

        }

        private async void DomainClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Domain clicked!");
            await Navigation.PushAsync(new DomainPage());

        }

    }
}
