using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Volunteer
{
    [DynamoDBTable("Projects")]
    public class Project
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }

        public override string ToString()
        {
            return String.Format("Name:{0}, Location:{1}, ImageUrl:{2}", Name, Location, ImageUrl);
        }
    }
}
