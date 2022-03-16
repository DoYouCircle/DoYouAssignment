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

        public void ShowDetails(object newObject)
        {
            if (newObject != null)
            {
                if (GDetails_selected.Visibility == Visibility.Collapsed)
                {
                    GDetails_empty.Visibility = Visibility.Collapsed;

                    GDetails_selected.Visibility = Visibility.Visible;
                }

                if (newObject.GetType() == typeof(CourseView.Test))
                {
                    //CourseView.Test selectedCourse = (CourseView.Test)newObject;

                    //TB_Name.Text = selectedCourse.NAME;

                    //TB_Type.Text = newObject.GetType().ToString();

                    ShowCourse((CourseView.Test)newObject);
                    
                    return;
                }

                if (newObject.GetType() == typeof(CourseView.TestGroups))
                {
                    //CourseView.TestGroups selectedGroup = (CourseView.TestGroups)newObject;

                    //TB_Name.Text = selectedGroup.NAME;

                    //TB_Type.Text = newObject.GetType().ToString();

                    ShowGroup((CourseView.TestGroups)newObject);

                    return;
                }

                if (newObject.GetType() == typeof(CourseView.TestGroups))
                {
                    CourseView.TestGroups selectedGroup = (CourseView.TestGroups)newObject;

                    TB_Name.Text = selectedGroup.NAME;

                    TB_Type.Text = newObject.GetType().ToString();

                    return;
                }

                Console.WriteLine("The type of the object could not be passed!");
            }

            void ShowCourse(CourseView.Test course)
            {
                if (GDetails_Course.Visibility == Visibility.Collapsed)
                {
                    GDetails_Group.Visibility = Visibility.Collapsed;
                    GDetails_Assignment.Visibility = Visibility.Collapsed;

                    GDetails_Course.Visibility = Visibility.Visible;
                }
            }

            void ShowGroup(CourseView.TestGroups group)
            {
                if (GDetails_Group.Visibility == Visibility.Collapsed)
                {
                    GDetails_Course.Visibility = Visibility.Collapsed;
                    GDetails_Assignment.Visibility = Visibility.Collapsed;

                    GDetails_Group.Visibility = Visibility.Visible;
                }
            }

            void ShowAssignment(/*CourseView.TestGroups assignment*/)
            {
                if (GDetails_Assignment.Visibility == Visibility.Collapsed)
                {
                    GDetails_Group.Visibility = Visibility.Collapsed;
                    GDetails_Course.Visibility = Visibility.Collapsed;

                    GDetails_Assignment.Visibility = Visibility.Visible;
                }
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
            GButtonsDefault.Visibility = Visibility.Visible;

            GDeletePopUp.Visibility = Visibility.Collapsed;

            GButtonsPopUp.Visibility = Visibility.Collapsed;
        }
    }
}
