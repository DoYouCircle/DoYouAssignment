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
        private static readonly MainWindow mw = (MainWindow)Application.Current.MainWindow;

        /// <summary>
        /// This opens the webpage from the given URL with the standard browser.
        /// </summary>
        /// <param name="url">The URL of the webpage.</param>
        public static void OpenLink(string url)
        {
            System.Diagnostics.Process.Start(url);
        }

        public class Notifier
        {
            public enum TYPE
            {
                INFO,
                WARNING,
                ERROR
            }

            public class Note
            {
                public TYPE Type { get; set; }
                public string Message { get; set;  }

                public void Add()
                {
                    Notes.Add(this);

                    activeIndex = Notes.Count - 1;

                    mw.NOTIFIER_Update(this);
                }

                public void Remove()
                {
                    Notes.Remove(this);
                    
                    Update();
                }

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

            public static List<Note> Notes = new List<Note>();

            public static int activeIndex = 0;

            /// <summary>
            /// Will be called from different places.
            /// </summary>
            /// <param name="Type"></param>
            /// <param name="Message"></param>
            public static void Throw(TYPE Type, string Message)
            {
                new Note() {
                    Type = Type,
                    Message = Message
                }.Add();
            }

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
