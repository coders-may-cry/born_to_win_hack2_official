using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;


namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private readonly List<string> rowNameList = new List<string>() { "personal_number", "gender", "salary", "education", "period" };
        private readonly Form1 parent;
        public Form2(Form1 parent)
        {
            InitializeComponent();
            chart1.Series.Clear();

            this.parent = parent;
        }

        public string Name_ = "";
        public string AxisX = "";
        public string AxisY = "";
        public string Time_ = "";
        public bool isChrat = true;

        private void button1_Click(object sender, EventArgs e)
        {
            string startDate = dateTimePicker1.Value.ToShortDateString();
            string endDate = dateTimePicker2.Value.ToShortDateString();

            Diagram diagram = parsData();
            diagram.xAxisArray = GetFromDB(diagram.xAxisName, startDate, endDate);
            diagram.yAxisArray = GetFromDB(diagram.yAxisName, startDate, endDate);

            parent.setNewDiagram(diagram);

            this.Close();
        }

        private List<string> GetFromDB(string field, string startDate, string endDate)  // field = зарплата
        {
            string[] d1 = startDate.Split('.');
            Array.Reverse(d1);
            string[] d2 = endDate.Split('.');
            Array.Reverse(d2);

            SqlConnection sqlConnection = new SqlConnection();

            List<string> out_tables = new List<string>() { field };
            List<string> queries = new List<string>() { "period >= \'" + String.Join("-", d1) + "\'", "period <= \'" + String.Join("-", d2) + "\'" };

            List<string> result = new List<string>();
            List <List<string>> raw = sqlConnection.StartConnect(sqlConnection.createQuery(out_tables, queries), out_tables);

            foreach (List<string> r in raw)
                result.Add(r[0]);

            return result;
        }

        private Diagram parsData()
        {
            string xAxisName = comboBox_x.Text;
            string yAxisName = comboBox_y.Text;

            graph_name.Text = xAxisName + "-" + yAxisName;
            save_button.Enabled = xAxisName != "" && yAxisName != "";

            return new Diagram(
                parent.getAndIncrementCounter(),
                graph_name.Text,
                xAxisName,
                yAxisName,
                (chart_radioButton.Enabled) ? 1 : 2,
                true,
                new List<string>(),
                new List<string>()
                );
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox_x.Items.AddRange(rowNameList.ToArray());
            comboBox_y.Items.AddRange(rowNameList.ToArray());
        }

        private void XList_SelectedIndexChanged(object sender, EventArgs e)
        {
            parsData();
        }

        private void YList_SelectedIndexChanged(object sender, EventArgs e)
        {
            parsData();
        }
        private void diagramTypeChanged()  // TODO
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            diagramTypeChanged();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Math1 math1 = new Math1();
            string startDate = dateTimePicker1.Value.ToShortDateString();
            string endDate = dateTimePicker2.Value.ToShortDateString();

            List<string> t1 = new List<string>();
            List<string> t2 = new List<string>();
            t2 = GetFromDB("dismissial_date", startDate, endDate);
            t1 = GetFromDB("admission_date", startDate, endDate);
            int l = 0;
            chart1.Series.Add(l.ToString()).Name = l.ToString();
            chart1.Series[l.ToString()].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            l++;
            ArrayList kk = new ArrayList();
            kk = math1.get_month_from_data(t1, t2);
            kk.Sort();
            int b = 0;
            ArrayList b_arr = new ArrayList();
            for (int n = 0; n < kk.Count -1 ; n++)
            {
                if (Convert.ToInt32(kk[n]) == Convert.ToInt32(kk[n + 1]))
                {
                    b++;
                }
                else
                {
                    b++;
                    b_arr.Add(b);
                    b = 0;
                }
            }
            //Console.WriteLine(b_arr.Count);
            double k = 0;
            for (int i = 0; i < 120; i++)
            {
                chart1.Series[(l - 1).ToString()].Points.AddXY(Convert.ToInt32(k), b_arr[i]);
                k += 0.28;
            }
            button1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Math1 math1 = new Math1();
            string startDate = dateTimePicker1.Value.ToShortDateString();
            string endDate = dateTimePicker2.Value.ToShortDateString();

            List<string> t1 = new List<string>();
            t1 = GetFromDB("salary", startDate, endDate);
            int l = 1;
            chart1.Series.Add(l.ToString()).Name = l.ToString();
            chart1.Series[l.ToString()].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            l++;
            ArrayList kk = new ArrayList();
            kk = math1.get_middle_salary(t1);
            double k = 0;
            for (int i = 0; i < 84; i++)
            {
                chart1.Series[(l - 1).ToString()].Points.AddXY(Convert.ToInt32(k), kk[i]);
                k += 1.5;
            }
            button3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Math1 math1 = new Math1();
            string startDate = dateTimePicker1.Value.ToShortDateString();
            string endDate = dateTimePicker2.Value.ToShortDateString();

            List<string> t1 = new List<string>();
            t1 = GetFromDB("dismissial_date", startDate, endDate);
            int l = 2;
            chart1.Series.Add(l.ToString()).Name = l.ToString();
            chart1.Series[l.ToString()].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            l++;
            ArrayList kk = new ArrayList();
            kk = math1.where_out(t1);
            double k = 0;
            for (int i = 0; i < 4; i++)
            {
                chart1.Series[(l - 1).ToString()].Points.AddXY(Convert.ToInt32(k) * 30, Convert.ToDouble(kk[i]) * 10);
                k += 1;
            }
            button4.Enabled = false;
        }
    }
}
