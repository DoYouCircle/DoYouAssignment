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
using static DoYouAssignment.scripts.CommonUtils;

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

            MainFrame.Content = new Dashboard();

            Notifier.Throw(Notifier.TYPE.WARNING, "Your house could burn!");
            Notifier.Throw(Notifier.TYPE.ERROR, "Your house is currently burning!");
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
            if (MainFrame.Content.ToString() != new About().ToString())
                MainFrame.Content = new About();
        }

        private void DASHBOARD_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content.ToString() != new Dashboard().ToString())
                MainFrame.Content = new Dashboard();
        }

        private void COURSEVIEW_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content.ToString() != new CourseView().ToString())
                MainFrame.Content = new CourseView();
        }


        private void NOTIFIER_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (CommonUtils.Notifier.Notes.Count > 0)
                CommonUtils.Notifier.Notes[Notifier.activeIndex].Remove();
        }

        public void NOTIFIER_Update(Notifier.Note activeNote)
        {
            if (activeNote == null)
            {
                NOTIFIER_Type.Text = "";

                NOTIFIER_Message.Text = "";

                return;
            }

            NOTIFIER_Type.Foreground = activeNote.GetColor();

            NOTIFIER_Type.Text = activeNote.GetType_();

            NOTIFIER_Message.Text = activeNote.Message;
        }
    }
}
