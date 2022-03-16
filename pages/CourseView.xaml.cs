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
            DG_COURSE.DataContext = database.Element.db.Select(database.Database.TABLES.courses);

            //Console.WriteLine(database.Element.db.Select(database.Database.TABLES.courses)[0].)

            // Cast frame content
            DetailsFrame.Content = new CVdetails();
        }

        private void DG_COURSE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            database.Course testcourse = (database.Course)DG_COURSE.SelectedItem;

            //todo: add some kind of check to avoid unnecessary work & fix potential bugs
            if (testcourse != null)
            {
                DG_AGROUP.DataContext = database.Element.db.Select(database.Database.TABLES.assignmentGroups, filter: string.Format("id = {0}", testcourse.Id));

                //unselect the rows of the other tables
                DG_AGROUP.UnselectAll();

                CVdetails detailsframe = (CVdetails)DetailsFrame.Content;

                detailsframe.ShowDetails(testcourse);
            }
        }

        private void DG_AGROUP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            database.AssignmentGroup testgroup = (database.AssignmentGroup)DG_AGROUP.SelectedItem;

            if (testgroup != null)
            {
                //unselect the rows of the other tables
                DG_COURSE.UnselectAll();

                CVdetails detailsframe = (CVdetails)DetailsFrame.Content;

                detailsframe.ShowDetails(testgroup);
            }
        }

        private void DG_ASSIGNMENT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ADD_Course_Click(object sender, RoutedEventArgs e)
        {
            //tests.Add(new Test()
            //{
            //    NAME = "unnamed course"
            //});

            //DG_COURSE.DataContext = tests;
        }
    }
}
