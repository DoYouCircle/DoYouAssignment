using DoYouAssignment.pages.SubFrames;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DoYouAssignment.pages
{
    /// <summary>
    /// Interaktionslogik für CourseView.xaml
    /// </summary>
    public partial class CourseView : Page
    {
        public CourseView()
        {
            InitializeComponent();

            // Test Data for ui design
            DG_COURSE.DataContext = tests;

            // Cast frame content
            // todo: add empty version of CVDetails
            DetailsFrame.Content = new CVdetails();
        }

        public ObservableCollection<Test> tests = new ObservableCollection<Test>()
        {
            new Test()
            {
                NAME = "Mathe",
                PERCENTAGE = 0.5f
            },
            new Test()
            {
                NAME = "Physik",
                PERCENTAGE = 0f
            },
            new Test()
            {
                NAME = "Informatik",
                PERCENTAGE = 0.25f
            }
        };

        public class Test
        {
            public string NAME { get; set; }

            public float PERCENTAGE { get; set; }
        }

        public class TestGroups
        {
            public string NAME { get; set; }

            public string Curse { get; set; }
        }

        public ObservableCollection<TestGroups> TestGroupsList = new ObservableCollection<TestGroups>()
        {
            new TestGroups() { Curse = "Mathe", NAME = "ein Mathekurs"},
            new TestGroups() { Curse = "Mathe", NAME = "ein weiterer Mathekurs"},
            new TestGroups() { Curse = "Informatik", NAME = "ein Kurs der Informatik"}
        };

        private void DG_COURSE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Test testcourse = (Test)DG_COURSE.SelectedItem;

            //todo: add some kind of check to avoid unnecessary work & fix bug
            if (testcourse != null)
            {
                ObservableCollection<TestGroups> groups = new ObservableCollection<TestGroups>();

                foreach (var item in TestGroupsList)
                {
                    if (item.Curse == testcourse.NAME)
                    {
                        groups.Add(item);
                    }
                }

                DG_AGROUP.DataContext = groups;

                //unselect the rows of the other tables
                DG_AGROUP.UnselectAll();

                CVdetails detailsframe = (CVdetails)DetailsFrame.Content;

                detailsframe.ShowDetails(testcourse);
            }
        }

        private void DG_AGROUP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TestGroups testgroup = (TestGroups)DG_AGROUP.SelectedItem;

            //unselect the rows of the other tables
            DG_COURSE.UnselectAll();

            CVdetails detailsframe = (CVdetails)DetailsFrame.Content;

            detailsframe.ShowDetails(testgroup);
        }

        private void DG_ASSIGNMENT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ADD_Course_Click(object sender, RoutedEventArgs e)
        {
            tests.Add(new Test()
            {
                NAME = "unnamed course"
            });

            DG_COURSE.DataContext = tests;
        }
    }
}
