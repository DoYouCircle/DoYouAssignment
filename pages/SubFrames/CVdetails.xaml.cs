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

namespace DoYouAssignment.pages.SubFrames
{
    /// <summary>
    /// Interaction logic for CVdetails.xaml
    /// </summary>
    public partial class CVdetails : Page
    {
        public CVdetails()
        {
            InitializeComponent();
        }

        private object selectedObject = new object();

        private enum Type
        {
            COURSE,
            AGROUP,
            ASSIGNMENT,
            NULL
        }

        private Type GetType(object newObject)
        {
            if (newObject.GetType() == typeof(database.Course))
                return Type.COURSE;

            if (newObject.GetType() == typeof(database.AssignmentGroup))
                return Type.AGROUP;

            if (newObject.GetType() == typeof(database.Assignment))
                return Type.ASSIGNMENT;

            return Type.NULL;
        }

        /// <summary>
        /// Present the highlighted item in the details frame.
        /// </summary>
        /// <param name="newObject">The highlighted item.</param>
        public void ShowDetails(object newObject)
        {
            if (GDetails_selected.Visibility == Visibility.Collapsed)
            {
                GDetails_empty.Visibility = Visibility.Collapsed;

                GDetails_selected.Visibility = Visibility.Visible;
            }

            //if (newObject.GetType() == typeof(database.Course))
            //{
            //    ShowCourse((database.Course)newObject);
                    
            //    return;
            //}

            //if (newObject.GetType() == typeof(database.AssignmentGroup))
            //{
            //    ShowGroup((database.AssignmentGroup)newObject);

            //    return;
            //}

            //if (newObject.GetType() == typeof(database.Assignment))
            //{
            //    ShowAssignment((database.Assignment)newObject);

            //    return;
            //}

            switch (GetType(newObject))
            {
                case Type.COURSE:
                    ShowCourse((database.Course)newObject);
                    break;
                case Type.AGROUP:
                    ShowGroup((database.AssignmentGroup)newObject);
                    break;
                case Type.ASSIGNMENT:
                    ShowAssignment((database.Assignment)newObject);
                    break;
                case Type.NULL:
                    Console.WriteLine("The type of the object could not be passed!");
                    break;
                default:
                    break;
            }

            //Console.WriteLine("The type of the object could not be passed!");

            void ShowCourse(database.Course course)
            {
                if (GDetails_Course.Visibility == Visibility.Collapsed)
                {
                    GDetails_Group.Visibility = Visibility.Collapsed;
                    GDetails_Assignment.Visibility = Visibility.Collapsed;

                    GDetails_Course.Visibility = Visibility.Visible;
                }

                GDetails_Course.DataContext = course;

                selectedObject = course;
            }

            void ShowGroup(database.AssignmentGroup group)
            {
                if (GDetails_Group.Visibility == Visibility.Collapsed)
                {
                    GDetails_Course.Visibility = Visibility.Collapsed;
                    GDetails_Assignment.Visibility = Visibility.Collapsed;

                    GDetails_Group.Visibility = Visibility.Visible;
                }

                selectedObject = group;
            }

            void ShowAssignment(database.Assignment assignment)
            {
                if (GDetails_Assignment.Visibility == Visibility.Collapsed)
                {
                    GDetails_Group.Visibility = Visibility.Collapsed;
                    GDetails_Course.Visibility = Visibility.Collapsed;

                    GDetails_Assignment.Visibility = Visibility.Visible;
                }

                selectedObject = assignment;
            }
        }

        private void DELETE_Click(object sender, RoutedEventArgs e)
        {
            GButtonsDefault.Visibility = Visibility.Collapsed;

            GDeletePopUp.Visibility = Visibility.Visible;

            GButtonsPopUp.Visibility = Visibility.Visible;
        }

        private void DEL_Cancel_Click(object sender, RoutedEventArgs e)
        {
            GButtonsDefault.Visibility = Visibility.Visible;

            GDeletePopUp.Visibility = Visibility.Collapsed;

            GButtonsPopUp.Visibility = Visibility.Collapsed;
        }

        private void DEL_Okay_Click(object sender, RoutedEventArgs e)
        {
            //todo:
            // implement deletion
            // reset detail frame
            
            MainWindow mw = (MainWindow)App.Current.MainWindow;

            CourseView cv = (CourseView)mw.MainFrame.Content;

            switch (GetType(selectedObject))
            {
                case Type.COURSE:
                    cv.RemoveCourse(selectedObject);
                    break;
                case Type.AGROUP:
                    break;
                case Type.ASSIGNMENT:
                    break;
                case Type.NULL:
                    break;
                default:
                    
                    break;
            }

            GButtonsDefault.Visibility = Visibility.Visible;

            GDeletePopUp.Visibility = Visibility.Collapsed;

            GButtonsPopUp.Visibility = Visibility.Collapsed;
        }

        public void ShowEmpty()
        {
            GDetails_selected.Visibility = Visibility.Collapsed;

            GDetails_Course.Visibility = Visibility.Collapsed;
            GDetails_Group.Visibility = Visibility.Collapsed;
            GDetails_Assignment.Visibility = Visibility.Collapsed;
            
            GDetails_empty.Visibility = Visibility.Visible;
        }
    }
}
