using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Volunteer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrganizationPage : ContentPage

    {
        private const string AccessKey = "AKIAQX3PWFSAKJ5ACJXV";
        private const string SecretKey = "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";
        Project project = new Project();

        public OrganizationPage()
        {
            InitializeComponent();
            Console.WriteLine("Organization Page reached!");
        }

        void RegisterButton_Clicked(object sender, System.EventArgs e)
        {

            project.Name = nameEntry.Text;
            project.Location = locationEntry.Text;

            try
            {
                SaveProjectToDB(project);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to save project to DB. Reeason: " + ex);
                return;
            }

        }

        async void SaveProjectToDB(Project project)
        {
            AmazonDynamoDBClient dbClient = new AmazonDynamoDBClient(AccessKey, SecretKey, RegionEndpoint.EUWest1);

            DynamoDBContextConfig config = new DynamoDBContextConfig
            {
                Conversion = DynamoDBEntryConversion.V2
            };

            DynamoDBContext context = new DynamoDBContext(dbClient, config);

            Console.WriteLine("Trying to save project: " + project);
            await context.SaveAsync(project);
            Console.WriteLine("Successfully saved project: " + project);

        }

        async Task<MediaFile> PickPhotoFromGallery()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");

            MediaFile photo = await CrossMedia.Current.PickPhotoAsync();

            if (photo == null)
                await DisplayAlert("Failed to fetch photo", "please try again or restart app", "OK");

            await DisplayAlert("Failed to fetch photo", "please try again or restart app", "OK");

            Console.WriteLine("Successfully picked image: " + photo);
            return photo;
        }
    }
}