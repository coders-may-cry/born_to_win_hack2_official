using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class HeadersManager
    {
        public List<string> dbHeaders = new List<string>();
        public List<string> ruHeaders = new List<string>();
        public HeadersManager(SqlConnection sqlConnection)
        {
            this.dbHeaders = sqlConnection.getHeaders("workers_table");
            this.ruHeaders = new List<string> {
                "id", "должность", "дата рождения", "пол", "семейный статус", "дата приёма", "дата увольнения", "наименвание отсутствия", 
                "календарные дни отсутствия", "оклад", "город", "количество детей", "наставник", "период", "образование", "причина увольнения" };
        }

        public string getDbHeaderFromRu(string name)
        {
            for (int i = 0; i < this.ruHeaders.Count; i++)
                if (this.ruHeaders[i] == name)
                    return this.dbHeaders[i];
            return "";
        }
        public string getRuHeaderFromRDb(string name)
        {
            for (int i = 0; i < this.dbHeaders.Count; i++)
                if (this.dbHeaders[i] == name)
                    return this.ruHeaders[i];
            return "";
        }
    }
}
