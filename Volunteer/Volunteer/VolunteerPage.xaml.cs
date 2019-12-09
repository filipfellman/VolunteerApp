using System;
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
        public VolunteerPage()

        {
            InitializeComponent();
            Console.WriteLine("Volunteer Page reached!");
        }
    }
}