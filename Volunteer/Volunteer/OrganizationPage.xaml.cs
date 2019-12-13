using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;

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

            Project project = new Project()

            {
                Name = nameEntry.Text,
                Location = locationEntry.Text,
                ImageUrl = imageEntry.Text
            };

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
            AmazonDynamoDBClient dbClient = new AmazonDynamoDBClient("AKIAQX3PWFSAKJ5ACJXV", "cLWsrB2z9qMniBxx9TmTOX2Yz962mBe8SAqk0o/2", RegionEndpoint.EUWest1);

            DynamoDBContextConfig config = new DynamoDBContextConfig
            {
                Conversion = DynamoDBEntryConversion.V2
            };

            DynamoDBContext context = new DynamoDBContext(dbClient, config);

            Console.WriteLine("Trying to save project: " + project);
            await context.SaveAsync(project);
            Console.WriteLine("Successfully saved project: " + project);

        }
    }
}