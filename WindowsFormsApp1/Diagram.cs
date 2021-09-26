using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Diagram
    {
        public int u_id;
        public string name;
        public string xAxisName;
        public string yAxisName;
        public int diagramType;  // 1 - chart, 2 - graph
        public bool isVisible = true;
        public List<string> xAxisArray;
        public List<string> yAxisArray;

        public Diagram(int u_id, string name, string xAxisName, string yAxisName, int diagramType, bool isVisible, List<string> xAxisArray, List<string> yAxisArray)
        {
            this.u_id = u_id;
            this.name = name;
            this.xAxisName = xAxisName;
            this.yAxisName = yAxisName;
            this.diagramType = diagramType;
            this.isVisible = isVisible;
            this.xAxisArray = xAxisArray;
            this.yAxisArray = yAxisArray;
        }

        public void ChangeVisibility()
        {
            this.isVisible = !this.isVisible;
        }
    }
}
