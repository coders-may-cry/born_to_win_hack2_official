using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class NamesManager
    {
        public List<string> dbHeaders = new List<string>();
        public List<List<string>> dbElements = new List<List<string>>();
        public NamesManager(SqlConnection sqlConnection)
        {
            this.dbHeaders = sqlConnection.getHeaders("unique_table");
            this.dbElements = sqlConnection.getNames();
        }

        public List<string> getElementsFromHeaders(string name)
        {
            for (int i = 0; i < this.dbHeaders.Count; i++)
                if (this.dbHeaders[i] == name)
                    return this.dbElements[i];
            return new List<string>();
        }
    }
}
