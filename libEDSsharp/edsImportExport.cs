/*
    This file is part of libEDSsharp.

    libEDSsharp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    libEDSsharp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with libEDSsharp.  If not, see <http://www.gnu.org/licenses/>.
 
    Copyright(c) 2016 - 2019 Robin Cornelius <robin.cornelius@gmail.com>
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace libEDSsharp
{
    public partial class InfoSection
    {
        public virtual void Parse(Dictionary<string, string> section, string sectionname)
        {
            this.section = section;

            FieldInfo[] fields = this.GetType().GetFields();

            foreach (FieldInfo f in fields)
            {
                if (Attribute.IsDefined(f, typeof(EdsExport)))
                    GetField(f.Name, f.Name);

                if (Attribute.IsDefined(f, typeof(DcfExport)))
                    GetField(f.Name, f.Name);
            }
        }

        /// <summary>
        /// Write object to stream
        /// </summary>
        /// <param name="writer">stream to write the data to</param>
        /// <param name="ft">file type</param>
        public void Write(StreamWriter writer, Filetype ft)
        {
            writer.WriteLine("[" + edssection + "]");
            Type tx = this.GetType();
            FieldInfo[] fields = this.GetType().GetFields();

            foreach (FieldInfo f in fields)
            {
                if ((ft == Filetype.File_EDS) && (!Attribute.IsDefined(f, typeof(EdsExport))))
                    continue;

                if ((ft == Filetype.File_DCF) && (!(Attribute.IsDefined(f, typeof(DcfExport)) || Attribute.IsDefined(f, typeof(EdsExport)))))
                    continue;

                if (f.GetValue(this) == null)
                    continue;

                EdsExport ex = (EdsExport)f.GetCustomAttribute(typeof(EdsExport));

                bool comment = ex.IsReadOnly();

                if (f.FieldType.Name == "Boolean")
                {
                    writer.WriteLine(string.Format("{2}{0}={1}", f.Name, ((bool)f.GetValue(this)) == true ? 1 : 0, comment == true ? ";" : ""));
                }
                else
                {
                    writer.WriteLine(string.Format("{2}{0}={1}", f.Name, f.GetValue(this).ToString(), comment == true ? ";" : ""));
                }
            }

            writer.WriteLine("");
        }
    }

    public partial class MandatoryObjects : SupportedObjects
    {
        public MandatoryObjects(Dictionary<string, string> section)
        : this()
        {
            Parse(section);
        }
    }

    public partial class OptionalObjects : SupportedObjects
    {
        public OptionalObjects(Dictionary<string, string> section)
            : this()
        {
            Parse(section);
        }
    }
}
