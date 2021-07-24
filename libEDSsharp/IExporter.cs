using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libEDSsharp
{
    public abstract class IExporter
    {
        private string _fileName;
        private EDSsharp _eds;
        private String _gitVersion;
        private String lastExportedFile;
   


        public IExporter(string fileName, EDSsharp eds, string gitVersion)
        {
            FileName = fileName;
            Eds = eds;
            GitVersion = gitVersion;
            //OdName = odName;
        }


        
        public string LastExportedFile { get => lastExportedFile; protected set => lastExportedFile = value; }
        //protected string OdName { get => _odName; private set => _odName = value; }
        protected string GitVersion { get => _gitVersion; private set => _gitVersion = value; }
        protected string FileName { get => _fileName; private set => _fileName = value; }
        protected EDSsharp Eds { get => _eds; private set => _eds = value; }

        public abstract void export();


        protected string GetBaseDirectory()
        {
            return Path.GetDirectoryName(_fileName);
        }

        protected string GetBaseFileName()
        {
            return Path.GetFileNameWithoutExtension(_fileName);
        }

        protected string GetFileNameWithExtension(string extension)
        {
            return GetBaseDirectory() + Path.DirectorySeparatorChar + GetBaseFileName() + "." + extension;
        }


        protected string GetDefinitionName()
        {
            return GetBaseFileName().ToUpper();
        }
    }
}
