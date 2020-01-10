using Amazon.DynamoDBv2.DataModel;
using System;
using Xamarin.Forms;

namespace Volunteer
{
    [DynamoDBTable("Projects")]
    public class Project
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public ImageSource Image { get; set; }

        public override string ToString()
        {
            return String.Format("Name:{0}, Location:{1}", Name, Location);
        }
    }
}
