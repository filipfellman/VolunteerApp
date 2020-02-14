using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Volunteer
{
    [DynamoDBTable("Projects")]
    public class Project
    {
        public string Name { get; set; }
        public string Organization { get; set; }
        public string Domain { get; set; }
        public ImageSource Image { get; set; }

        public override string ToString()
        {
            return String.Format("Name:{0}, Organization:{1}, Domain:{2}", Name, Organization, Domain);
        }
    }
}
