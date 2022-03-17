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

        /// <summary>
        /// ID of selected course.
        /// </summary>
        private int selectedCourse = -1;
        /// <summary>
        /// ID of selected group.
        /// </summary>
        private int selectedAGroup = -1;
        /// <summary>
        /// ID of selected assignment.
        /// </summary>
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

            DG_COURSE.SelectedIndex = DG_COURSE.Items.Count - 1;
        }

        private void ADD_AGroup_Click(object sender, RoutedEventArgs e)
        {
            int course_id;

            //some kind of check if the selected item is still there?
            if (DG_AGROUP.DataContext != null)
            {
                course_id = selectedCourse;

                //Console.WriteLine(selectedCourse.ToString());

                database.Element.db.InsertAssignmentGroup(course_id, "unnamed group");

                DG_AGROUP.DataContext = database.Element.db.Select(database.Database.TABLES.assignmentGroups, filter: string.Format("course_id = {0}", course_id));

                DG_AGROUP.SelectedIndex = DG_AGROUP.Items.Count - 1;

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

                DG_ASSIGNMENT.SelectedIndex = DG_ASSIGNMENT.Items.Count - 1;

                return;
            }

            Notifier.Throw(Notifier.TYPE.WARNING, "You have to first select a group!");
        }

        private void SelectAfterDelete(DataGrid dg, int _index, database.Database.TABLES table)
        {
            if (dg.Items.Count > 0)
            {
                if (dg.Items.Count > _index)
                    dg.SelectedIndex = _index;

                else if (dg.Items.Count == _index)
                    dg.SelectedIndex = _index - 1;

                //relevant?
                else
                    dg.SelectedIndex = _index - 2;

                return;
            }

            switch (table)
            {
                case database.Database.TABLES.courses:
                    CVdetails detailsframe = (CVdetails)DetailsFrame.Content;

                    detailsframe.ShowEmpty();
                    break;
                case database.Database.TABLES.assignmentGroups:
                    DG_COURSE.SelectedIndex = GetSelectedIndex(selectedCourse, database.Database.TABLES.courses);
                    break;
                case database.Database.TABLES.assignments:
                    DG_AGROUP.SelectedIndex = GetSelectedIndex(selectedAGroup, database.Database.TABLES.assignmentGroups);
                    break;
                default:
                    break;
            }
        }

        private int GetSelectedIndex(int ID, database.Database.TABLES table)
        {
            switch (table)
            {
                case database.Database.TABLES.courses:
                    for (int i = 0; i < DG_COURSE.Items.Count; i++)
                    {
                        database.Course newCourse = (database.Course)DG_COURSE.Items[i];

                        if (newCourse.Id == ID)
                            return i;
                    }

                    //database.Course course = (database.Course)database.Element.db.Select(table, filter: String.Format("id = {0}", ID))[0];

                    //Console.WriteLine(DG_COURSE.Items.IndexOf(course).ToString());

                    return -1;
                case database.Database.TABLES.assignmentGroups:
                    for (int i = 0; i < DG_AGROUP.Items.Count; i++)
                    {
                        database.AssignmentGroup newGroup = (database.AssignmentGroup)DG_AGROUP.Items[i];

                        if (newGroup.Id == ID)
                            return i;
                    }
                    return -1;
                case database.Database.TABLES.assignments:
                    for (int i = 0; i < DG_ASSIGNMENT.Items.Count; i++)
                    {
                        database.Assignment newAssignment = (database.Assignment)DG_ASSIGNMENT.Items[i];

                        if (newAssignment.Id == ID)
                            return i;
                    }
                    return -1;
                default:
                    return -1;
            }
        }

        public void RemoveCourse(object course)
        {
            database.Course testcourse = (database.Course)course;

            database.Element.db.DeleteCourse((int)testcourse.Id);

            int _index = GetSelectedIndex((int)testcourse.Id, database.Database.TABLES.courses);

            //todo: add some kind of error if this happens (it should not)
            if (_index != -1)
            {
                DG_AGROUP.DataContext = null;
                DG_ASSIGNMENT.DataContext = null;

                selectedAGroup = -1;
                selectedAssignment = -1;

                DG_COURSE.UnselectAll();
            }

            DG_COURSE.DataContext = database.Element.db.Select(database.Database.TABLES.courses);

            SelectAfterDelete(DG_COURSE, _index, database.Database.TABLES.courses);
        }

        public void RemoveAGroup(object group)
        {
            database.AssignmentGroup testgroup = (database.AssignmentGroup)group;

            database.Element.db.DeleteAssignmentGroup((int)testgroup.Id);

            int _index = GetSelectedIndex((int)testgroup.Id, database.Database.TABLES.assignmentGroups);

            if (_index != -1)
            {
                DG_ASSIGNMENT.DataContext = null;

                selectedAssignment = -1;

                DG_AGROUP.UnselectAll();
            }

            DG_AGROUP.DataContext = database.Element.db.Select(database.Database.TABLES.assignmentGroups, filter: string.Format("course_id = {0}", selectedCourse));

            SelectAfterDelete(DG_AGROUP, _index, database.Database.TABLES.assignmentGroups);
        }

        public void RemoveAssignment(object assignment)
        {
            database.Assignment testassignment = (database.Assignment)assignment;

            database.Element.db.DeleteAssignment((int)testassignment.Id);

            int _index = GetSelectedIndex((int)testassignment.Id, database.Database.TABLES.assignments);

            if (_index != -1)
            {
                DG_ASSIGNMENT.UnselectAll();
            }

            DG_ASSIGNMENT.DataContext = database.Element.db.Select(database.Database.TABLES.assignments, filter: string.Format("assignment_group_id = {0}", selectedAGroup));

            SelectAfterDelete(DG_ASSIGNMENT, _index, database.Database.TABLES.assignments);
        }
    }
}
