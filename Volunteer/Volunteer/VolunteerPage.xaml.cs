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
        public IList<Project> Projects { get; private set; }
        public VolunteerPage()

        {
            InitializeComponent();
            Console.WriteLine("Volunteer Page reached!");

            ListProjects();

        }

        private async void ListProjects()
        {
            Projects = new List<Project>();
            Projects.Add(new Project
            {
                Name = "Språk Café",
                Location = "Rinkeby Bibliotek",
                Image = ImageSource.FromFile("volunteerimage.png")

            });
            Projects.Add(new Project
            {
                Name = "Språk Café",
                Location = "Rinkeby Bibliotek",
                Image = ImageSource.FromFile("volunteerimage.png")

            });


            IEnumerable<Project> allProjects;

            try
            {
                allProjects = await GetAllProjectsFromDB();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to scan projects from DB. Reeason: " + ex);
                throw ex;
            }

            foreach (Project project in allProjects)
            {
                project.Image = ImageSource.FromFile("volunteerimage.png");
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

            Console.WriteLine("Trying to scan all projects");

            var conditions = new List<ScanCondition>();
            IEnumerable<Project> result = await context.ScanAsync<Project>(conditions).GetRemainingAsync();
            Console.WriteLine("Successfully scanned all projects");

            return result;
        }
    }
}
