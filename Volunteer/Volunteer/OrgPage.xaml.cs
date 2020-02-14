using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
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
	public partial class OrgPage : ContentPage
	{
        public IList<Organization> Organizations { get; private set; }
        public OrgPage ()
		{
			InitializeComponent ();

            ListOrganizations();

        }

        private async void ListOrganizations()
        {
            Organizations = new List<Organization>();
            Organizations.Add(new Organization
            {
                Name = "Hope Sweden",
                Location = "Rinkeby Bibliotek",
                Image = ImageSource.FromUri(new Uri("https://volunteer-application.s3-eu-west-1.amazonaws.com/organizations/images/hopewwsweden.jpeg"))

            });
            Organizations.Add(new Organization
            {
                Name = "Rädda Barnen",
                Location = "Central station",
                Image = ImageSource.FromUri(new Uri("https://volunteer-application.s3-eu-west-1.amazonaws.com/organizations/images/raddabarnen.png"))
            });


            IEnumerable<Organization> allOrganizations;

            try
            {
                allOrganizations = await GetAllOrganizationsFromDB();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to scan organizations from DB. Reeason: " + ex);
                throw ex;
            }

            foreach (Organization organization in allOrganizations)
            {
                if (organization.Image == null)
                {
                    organization.Image = ImageSource.FromFile("volunteerimage.png");

                }
                Organizations.Add(organization);
            }
            BindingContext = this;
        }

        private async Task<IEnumerable<Organization>> GetAllOrganizationsFromDB()
        {
            AmazonDynamoDBClient dbClient = new AmazonDynamoDBClient("AKIAQX3PWFSAKJ5ACJXV", "cLWsrB2z9qMniBxx9TmTOX2Yz962mBe8SAqk0o/2", RegionEndpoint.EUWest1);

            DynamoDBContextConfig config = new DynamoDBContextConfig
            {
                Conversion = DynamoDBEntryConversion.V2
            };

            DynamoDBContext context = new DynamoDBContext(dbClient, config);

            Console.WriteLine("Trying to scan all organizations");

            var conditions = new List<ScanCondition>();
            IEnumerable<Organization> result = await context.ScanAsync<Organization>(conditions).GetRemainingAsync();
            Console.WriteLine("Successfully scanned all organizations");

            return result;
        }

        private void HopeSwedenClicked(object sender, EventArgs e)
        {
            Console.WriteLine("HopeWWSweden clicked!");
        }
    }
}