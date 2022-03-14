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
                    TB_Name.Text = "It is a course!";

                    return;
                }

                if (newObject.GetType() == typeof(CourseView.TestGroups))
                {
                    TB_Name.Text = "It is a group!";

                    return;
                }

                Console.WriteLine("The type of the object could not be passed!");
            }
        }
    }
}
