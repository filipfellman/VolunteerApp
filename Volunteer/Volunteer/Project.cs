using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Volunteer
{
    public class Project
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
