using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouAssignment.scripts
{
    public static class CommonUtils
    {
        public static void OpenLink(string link)
        {
            System.Diagnostics.Process.Start(link);
        }
    }
}
