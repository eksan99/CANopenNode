using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libEDSsharp
{
    /// <summary>
    /// Interface for exporting CanOpenNode OD files
    /// </summary>
    public interface IExporter
    {
        /// <summary>
        /// Export file(s)
        /// </summary>
        /// <param name="folderpath">Path to the folder that will contain the new files</param>
        /// <param name="filename">base filename for the new files</param>
        /// <param name="gitVersion">version that will be saved to the file</param>
        /// <param name="eds">The eds that will be exported</param>
        /// <param name="odname">The object dictionary name</param>
        void export(string folderpath, string filename, string gitVersion, EDSsharp eds , string odname="OD");
    }
}
