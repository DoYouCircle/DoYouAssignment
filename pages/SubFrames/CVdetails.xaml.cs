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
                if (newObject.GetType() == typeof(CourseView.Test))
                {
                    CourseView.Test selectedCourse = (CourseView.Test)newObject;

                    TB_Name.Text = selectedCourse.NAME;

                    TB_Type.Text = newObject.GetType().ToString();

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
            GButtonsDefault.Visibility = Visibility.Visible;

            GDeletePopUp.Visibility = Visibility.Collapsed;

            GButtonsPopUp.Visibility = Visibility.Collapsed;
        }
    }
}
