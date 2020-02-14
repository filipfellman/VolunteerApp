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
	public partial class ProjectPage : ContentPage
	{
        public IList<Project> Projects { get; private set; }
        public ProjectPage ()
		{
			InitializeComponent ();

            ListProjects();

        }

        private async void ListProjects()
        {
            Projects = new List<Project>();
            Projects.Add(new Project
            {
                Name = "Språk Café",
                Organization = "Hope Sweden",
                Domain = "Food",
                Image = ImageSource.FromUri(new Uri("https://volunteer-application.s3-eu-west-1.amazonaws.com/projects/images/redcross.jpg"))

            });
            Projects.Add(new Project
            {
                Name = "Asylum Kids",
                Organization = "Hope Sweden",
                Domain = "Kids",
                Image = ImageSource.FromUri(new Uri("https://volunteer-application.s3-eu-west-1.amazonaws.com/projects/images/volunteerimage.png"))
            });


            IEnumerable<Project> allProjects;

            try
            {
                allProjects = await GetAllProjectsFromDB();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to scan organizations from DB. Reeason: " + ex);
                throw ex;
            }

            foreach (Project project in allProjects)
            {
                //organization.Image = ImageSource.FromFile("volunteerimage.png");
                Projects.Add(project);
            }
            BindingContext = this;
        }

        private async Task<IEnumerable<Project>> GetAllProjectsFromDB()
        {
            AmazonDynamoDBClient dbClient = new AmazonDynamoDBClient("AKIAQX3PWFSAKJ5ACJXV", "cLWsrB2z9qMniBxx9TmTOX2Yz962mBe8SAqk0o/2", RegionEndpoint.EUWest1);

            DynamoDBContextConfig config = new DynamoDBContextConfig
            {
                Conversion = DynamoDBEntryConversion.V2
            };

            DynamoDBContext context = new DynamoDBContext(dbClient, config);

            Console.WriteLine("Trying to scan all organizations");

            var conditions = new List<ScanCondition>();
            IEnumerable<Project> result = await context.ScanAsync<Project>(conditions).GetRemainingAsync();
            Console.WriteLine("Successfully scanned all organizations");

            return result;
        }
    }
}