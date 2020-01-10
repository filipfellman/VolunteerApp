using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Volunteer
{
    [DynamoDBTable("Projects")]
    public class Project
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public ImageSource Image { get; set; }
        //private static ImageSource image = ImageSource.FromFile("volunteerimage.png");
        //public ImageSource StandardImage
        //{
        //    get { return image; }
        //}

        public override string ToString()
        {
            return String.Format("Name:{0}, Location:{1}", Name, Location);
        }
    }
}
