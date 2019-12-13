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
                ImageUrl = "https://images.unsplash.com/photo-1507842217343-583bb7270b66?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&w=1000&q=80"

            });
            Projects.Add(new Project
            {
                Name = "Språk Café",
                Location = "Rinkeby Bibliotek",
                ImageUrl = "https://images.unsplash.com/photo-1507842217343-583bb7270b66?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&w=1000&q=80"

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
