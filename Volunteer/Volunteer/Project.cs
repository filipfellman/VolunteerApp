//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Text;

//namespace Volunteer
//{
//    class Project
//    {
//        public string DisplayProject { get; set; }
//    }

//    ObservableCollection<Project> projects = new ObservableCollection<Project>();
//    public ObservableCollection<Project> Projects { get { return projects; } }

//    public ProjectListPage()
//    {
//        ProjecList.ItemsSource = projects;

//        // ObservableCollection allows items to be added after ItemsSource
//        // is set and the UI will react to changes
//        employees.Add(new Employee { DisplayName = "Rob Finnerty" });
//        employees.Add(new Employee { DisplayName = "Bill Wrestler" });
//        employees.Add(new Employee { DisplayName = "Dr. Geri-Beth Hooper" });
//        employees.Add(new Employee { DisplayName = "Dr. Keith Joyce-Purdy" });
//        employees.Add(new Employee { DisplayName = "Sheri Spruce" });
//        employees.Add(new Employee { DisplayName = "Burt Indybrick" });
//    }
//}
