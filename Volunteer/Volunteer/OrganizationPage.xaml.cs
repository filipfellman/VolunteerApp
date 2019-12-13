using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
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
        string tableName = "Projects";

        public OrganizationPage()
        {
            InitializeComponent();
            Console.WriteLine("Organization Page reached!");
        }
        async void RegisterButton_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                AmazonDynamoDBClient dbClient = new AmazonDynamoDBClient("AKIAQX3PWFSAKJ5ACJXV", "cLWsrB2z9qMniBxx9TmTOX2Yz962mBe8SAqk0o/2", RegionEndpoint.EUWest1);
                Console.WriteLine("Successfully created client");

                Project project = new Project()

                {
                    Name = nameEntry.Text,
                    Location = locationEntry.Text,
                    ImageUrl = imageEntry.Text
                };

                Console.WriteLine("Trying to put item");

                var request = new PutItemRequest
                {
                    TableName = tableName,
                    Item = new Dictionary<string, AttributeValue>()
                {
                    {"Name" , new AttributeValue { S = project.Name} },
                    {"Location" , new AttributeValue { S = project.Location} },
                    {"ImageUrl" , new AttributeValue { S = project.ImageUrl} }

                }
                };
                await dbClient.PutItemAsync(request);
                Console.WriteLine("Successfully PUT ITEM!");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong. Reeason: " + ex);
                return;
            }

        }
    }
}