using System;
using System.Collections.Generic;
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
            DG_COURSE.DataContext = new List<Test>()
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

        }

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

        public List<TestGroups> TestGroupsList = new List<TestGroups>()
        {
            new TestGroups() { Curse = "Mathe", NAME = "ein Mathekurs"},
            new TestGroups() { Curse = "Mathe", NAME = "ein weiterer Mathekurs"},
            new TestGroups() { Curse = "Informatik", NAME = "ein Kurs der Informatik"}
        };

        private void DG_COURSE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Test testcourse = (Test)DG_COURSE.SelectedItem;

            List<TestGroups> groups = new List<TestGroups>();

            foreach (var item in TestGroupsList)
            {
                if (item.Curse == testcourse.NAME)
                {
                    groups.Add(item);
                }
            }

            DG_AGROUP.DataContext = groups;
        }
    }
}
