using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public sealed class SQLTools
    {
        public static void CreateUDL(string datastring, string archivoUDL)
        {
            StreamWriter str = new StreamWriter(archivoUDL, false, Encoding.Unicode);

            str.WriteLine("[oledb]");
            str.WriteLine("; Everything after this line is an OLE DB initstring");
            str.WriteLine(datastring);
            str.Close();
        }
    }
}
