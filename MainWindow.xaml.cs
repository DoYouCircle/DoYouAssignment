using DoYouAssignment.pages;
using DoYouAssignment.scripts;
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

namespace DoYouAssignment
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GITHUB_Click(object sender, RoutedEventArgs e)
        {
            CommonUtils.OpenLink(Constants.Links.github);
        }

        private void ISSUES_Click(object sender, RoutedEventArgs e)
        {
            CommonUtils.OpenLink(Constants.Links.issues);
        }

        private void ABOUT_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new About();
        }

        private void DASHBOARD_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Dashboard();
        }

        private void COURSEVIEW_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new CourseView();
        }
    }
}
