using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouAssignment.scripts
{
    /// <summary>
    /// This class holds common functions that do not belong to a specific part of the application.
    /// </summary>
    public static class CommonUtils
    {
        /// <summary>
        /// This opens the webpage from the given URL with the standard browser.
        /// </summary>
        /// <param name="url">The URL of the webpage.</param>
        public static void OpenLink(string url)
        {
            System.Diagnostics.Process.Start(url);
        }
    }
}
