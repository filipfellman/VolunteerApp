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
        Organization organization = new Organization();

        public OrganizationPage()
        {
            InitializeComponent();
            Console.WriteLine("Organization Page reached!");
        }

        async void RegisterButton_Clicked(object sender, System.EventArgs e)
        {

            organization.Name = nameEntry.Text;
            organization.Location = locationEntry.Text;
            if (string.IsNullOrEmpty(nameEntry.Text))
            {
                await DisplayAlert("Error", "Please fill in required fields", "OK");
                //TODO: Change border on fields to red
                return;
            }

            if (string.IsNullOrEmpty(locationEntry.Text))
            {
                await DisplayAlert("Error", "Please fill in required fields", "OK");
                //TODO: Change border on fields to red
                return;
            }

            //TODO: Check for image?

            //organization.Domain = domainPicker.SelectedItem.ToString();

            try
            {
                SaveOrganizationToDB(organization);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to save organization to DB. Reeason: " + ex);
                return;
            }

        }

        async void UploadButton_Clicked(object sender, System.EventArgs e)
        {
            MediaFile photo = await PickPhotoFromGallery();

            //TODO: How to upload to s3 from here and then store link in dynamodb field

            //organization.Image = photo;
        }


        async void SaveOrganizationToDB(Organization organization)
        {
            AmazonDynamoDBClient dbClient = new AmazonDynamoDBClient(AccessKey, SecretKey, RegionEndpoint.EUWest1);

            DynamoDBContextConfig config = new DynamoDBContextConfig
            {
                Conversion = DynamoDBEntryConversion.V2
            };

            DynamoDBContext context = new DynamoDBContext(dbClient, config);

            Console.WriteLine("Trying to save organization: " + organization);
            await context.SaveAsync(organization);
            Console.WriteLine("Successfully saved organization: " + organization);
            await DisplayAlert("Success", "Successfully registered organization \"" + organization.Name + "\"", "OK");

        }

        async Task<MediaFile> PickPhotoFromGallery()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");

            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Small
            };

            MediaFile pickedPhotoFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            if (pickedPhotoFile == null)
                await DisplayAlert("Failed to fetch photo", "please try again or restart app", "OK");

            Console.WriteLine("Successfully picked image: " + pickedPhotoFile);

            selectedImage.Source = ImageSource.FromStream(() => pickedPhotoFile.GetStream());

            return pickedPhotoFile;
        }
    }
}