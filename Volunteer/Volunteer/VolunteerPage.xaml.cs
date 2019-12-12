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
    public partial class VolunteerPage : ContentPage
    {
        public IList<Project> Projects { get; private set; }
        public VolunteerPage()

        {
            InitializeComponent();
            Console.WriteLine("Volunteer Page reached!");

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
            Projects.Add(new Project
            {
                Name = "Språk Café",
                Location = "Rinkeby Bibliotek",
                ImageUrl = "https://images.unsplash.com/photo-1507842217343-583bb7270b66?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&w=1000&q=80"

            });
            BindingContext = this;
        }
    }
}
