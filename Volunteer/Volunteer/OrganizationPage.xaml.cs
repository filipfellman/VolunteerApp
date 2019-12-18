using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.S3;
using Amazon.S3.Model;
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

        async void UploadButton_Clicked(object sender, System.EventArgs e)
        {
            MediaFile photo = await PickPhotoFromGallery();
            await UploadPhotoToS3Async(photo);
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

        private async Task UploadPhotoToS3Async(MediaFile photo)
        {
            IAmazonS3 s3Client = new AmazonS3Client(AccessKey, SecretKey, RegionEndpoint.EUWest1);
            try
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = "lol",
                    Key = "lol",
                    FilePath = photo.Path,
                    ContentType = "text/plain"

                };

                await s3Client.PutObjectAsync(putRequest);

            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine(
                        "Error encountered ***. Message:'{0}' when writing an object"
                        , e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    "Unknown encountered on server. Message:'{0}' when writing an object"
                    , e.Message);
            }
        }
    }
}