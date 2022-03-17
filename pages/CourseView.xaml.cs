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
using static DoYouAssignment.scripts.CommonUtils;

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

        private int selectedCourse = -1;
        private int selectedAGroup = -1;
        private int selectedAssignment = -1;

        private void DG_COURSE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            database.Course testcourse = (database.Course)DG_COURSE.SelectedItem;

            //todo: add some kind of check to avoid unnecessary work & fix potential bugs
            if (testcourse != null)
            {
                selectedCourse = (int)testcourse.Id;
                selectedAGroup = -1;
                selectedAssignment = -1;

                DG_ASSIGNMENT.DataContext = null;
                DG_AGROUP.DataContext = database.Element.db.Select(database.Database.TABLES.assignmentGroups, filter: string.Format("course_id = {0}", testcourse.Id));

                //unselect the rows of the other tables
                DG_AGROUP.UnselectAll();
                DG_ASSIGNMENT.UnselectAll();

                CVdetails detailsframe = (CVdetails)DetailsFrame.Content;

                detailsframe.ShowDetails(testcourse);
            }
        }

        private void DG_AGROUP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            database.AssignmentGroup testgroup = (database.AssignmentGroup)DG_AGROUP.SelectedItem;

            if (testgroup != null)
            {
                selectedAGroup = (int)testgroup.Id;
                selectedAssignment = -1;

                DG_ASSIGNMENT.DataContext = database.Element.db.Select(database.Database.TABLES.assignments, filter: string.Format("assignment_group_id = {0}", testgroup.Id));

                //unselect the rows of the other tables
                DG_COURSE.UnselectAll();
                DG_ASSIGNMENT.UnselectAll();

                CVdetails detailsframe = (CVdetails)DetailsFrame.Content;

                detailsframe.ShowDetails(testgroup);
            }
        }

        private void DG_ASSIGNMENT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            database.Assignment testassignment = (database.Assignment)DG_ASSIGNMENT.SelectedItem;

            if (testassignment != null)
            {
                selectedAssignment = (int)testassignment.Id;

                //unselect the rows of the other tables
                DG_COURSE.UnselectAll();
                DG_AGROUP.UnselectAll();

                CVdetails detailsframe = (CVdetails)DetailsFrame.Content;

                detailsframe.ShowDetails(testassignment);
            }
        }

        private void ADD_Course_Click(object sender, RoutedEventArgs e)
        {
            database.Element.db.InsertCourse("unnamed course");

            DG_COURSE.DataContext = database.Element.db.Select(database.Database.TABLES.courses);
        }

        private void ADD_AGroup_Click(object sender, RoutedEventArgs e)
        {
            int course_id;

            // todo: make it nicer - I beg you!

            //if (DG_COURSE.SelectedIndex != -1 | DG_COURSE.SelectedIndex != -1) 
            //{
            //    if (DG_COURSE.SelectedIndex != -1)
            //    {
            //        database.Course testcourse = (database.Course)DG_COURSE.SelectedItem;

            //        course_id = (int)testcourse.Id;
            //    }

            //    else
            //    {
            //        database.AssignmentGroup testgroup = (database.AssignmentGroup)DG_AGROUP.SelectedItem;

            //        course_id = (int)testgroup.Id;
            //    }

            //    database.Element.db.InsertAssignmentGroup(course_id, "unnamed group");

            //    DG_AGROUP.DataContext = database.Element.db.Select(database.Database.TABLES.assignmentGroups, filter: string.Format("id = {0}", course_id));
            //}

            //some kind of check if the selected item is still there?
            if (DG_AGROUP.DataContext != null)
            {
                course_id = selectedCourse;

                //Console.WriteLine(selectedCourse.ToString());

                database.Element.db.InsertAssignmentGroup(course_id, "unnamed group");

                DG_AGROUP.DataContext = database.Element.db.Select(database.Database.TABLES.assignmentGroups, filter: string.Format("course_id = {0}", course_id));

                return;
            }

            Notifier.Throw(Notifier.TYPE.WARNING, "You have to first select a course!");
        }

        private void ADD_Assignment_Click(object sender, RoutedEventArgs e)
        {
            int agroup_id;

            // todo: make it nicer - I beg you!

            //some kind of check if the selected item is still there?
            if (DG_ASSIGNMENT.DataContext != null)
            {
                agroup_id = selectedAGroup;

                database.Element.db.InsertAssignment(agroup_id, "unnamed assignment");

                DG_ASSIGNMENT.DataContext = database.Element.db.Select(database.Database.TABLES.assignments, filter: string.Format("assignment_group_id = {0}", agroup_id));

                return;
            }

            Notifier.Throw(Notifier.TYPE.WARNING, "You have to first select a group!");
        }

        public void RemoveCourse(object course)
        {
            
            database.Course testcourse = (database.Course)course;

            database.Element.db.DeleteCourse((int)testcourse.Id);

            int _index = -1;

            for (int i = 0; i < DG_COURSE.Items.Count; i++)
            {
                database.Course newCourse = (database.Course)DG_COURSE.Items[i];

                if (newCourse.Id == testcourse.Id)
                {
                    _index = i;

                    break;
                }
            }

            if (_index != -1)
            {
                DG_AGROUP.DataContext = null;
                DG_ASSIGNMENT.DataContext = null;

                selectedAGroup = -1;
                selectedAssignment = -1;

                DG_COURSE.UnselectAll();
            }

            DG_COURSE.DataContext = database.Element.db.Select(database.Database.TABLES.courses);

            if (DG_COURSE.Items.Count > 0 )
            {
                if (DG_COURSE.Items.Count >= _index)
                    DG_COURSE.SelectedIndex = _index;

                else
                    DG_COURSE.SelectedIndex = _index - 1;
            } else
            {
                //Console.WriteLine("ehhhh");
                CVdetails detailsframe = (CVdetails)DetailsFrame.Content;

                detailsframe.ShowEmpty();
            }

            Console.WriteLine(testcourse.Name);
            
        }
    }
}
