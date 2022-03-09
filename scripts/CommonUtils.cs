using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DoYouAssignment.scripts
{
    /// <summary>
    /// This class holds common functions that do not belong to a specific part of the application.
    /// </summary>
    public static class CommonUtils
    {
        /// <summary>
        /// Reference to the main window.
        /// </summary>
        private static readonly MainWindow mw = (MainWindow)Application.Current.MainWindow;

        /// <summary>
        /// This opens the webpage from the given URL with the standard browser.
        /// </summary>
        /// <param name="url">The URL of the webpage.</param>
        public static void OpenLink(string url)
        {
            System.Diagnostics.Process.Start(url);
        }

        /// <summary>
        /// This class handles everything related to the Notifier. 
        /// </summary>
        public class Notifier
        {
            /// <summary>
            /// This is the type of the notification.
            /// </summary>
            public enum TYPE
            {
                /// <summary>
                /// Useful information
                /// </summary>
                INFO,
                /// <summary>
                /// Important information that the user should be aware of.
                /// </summary>
                WARNING,
                /// <summary>
                /// Fatal error that appeared.
                /// </summary>
                ERROR
            }

            /// <summary>
            /// This object holds all relevant information for the notification.
            /// </summary>
            public class Note
            {
                /// <summary>
                /// Type of the notification.
                /// </summary>
                public TYPE Type { get; set; }

                /// <summary>
                /// This message will be shown to the user.
                /// </summary>
                public string Message { get; set;  }

                /// <summary>
                /// Adds the notification to the list, changes the index and activates it on the user interface.
                /// </summary>
                public void Add()
                {
                    Notes.Add(this);

                    activeIndex = Notes.Count - 1;

                    mw.NOTIFIER_Update(this);
                }

                /// <summary>
                /// Removes the notification from the list and updates the user interface.
                /// </summary>
                public void Remove()
                {
                    Notes.Remove(this);
                    
                    Update();
                }

                /// <summary>
                /// Returns the colour for the corresponding type of the notification.
                /// </summary>
                /// <returns>SolidColorBrush for the type.</returns>
                public Brush GetColor()
                {
                    switch (Type)
                    {
                        case TYPE.INFO:
                            return Brushes.Black;
                        case TYPE.WARNING:
                            return Brushes.Orange;
                        case TYPE.ERROR:
                            return Brushes.Red;
                        default:
                            return Brushes.Black;
                    }
                }

                /// <summary>
                /// Returns the shown string of the corresponding type of the notification.
                /// </summary>
                /// <returns>String for the type.</returns>
                public string GetType_()
                {
                    switch (Type)
                    {
                        case TYPE.INFO:
                            return "INFO";
                        case TYPE.WARNING:
                            return "WARNING";
                        case TYPE.ERROR:
                            return "ERROR";
                        default:
                            return "";
                    }
                }
            }

            /// <summary>
            /// A list of all active notifications.
            /// </summary>
            public static List<Note> Notes = new List<Note>();

            /// <summary>
            /// This is the index of the currenty shown notification.
            /// </summary>
            public static int activeIndex = 0;

            /// <summary>
            /// This function can be used to display notifications for the user.
            /// </summary>
            /// <param name="Type">Type of the notification.</param>
            /// <param name="Message">Message will be shown to the user.</param>
            public static void Throw(TYPE Type, string Message)
            {
                new Note() {
                    Type = Type,
                    Message = Message
                }.Add();
            }

            /// <summary>
            /// Currently updates the user interface to either show the latest entry of the notifications list or to be blank.
            /// </summary>
            public static void Update()
            {
                activeIndex = Notes.Count - 1;

                if (Notes.Count > 0)
                    mw.NOTIFIER_Update(Notes[activeIndex]);

                else
                    mw.NOTIFIER_Update(null);
            }
        }
    }
}
