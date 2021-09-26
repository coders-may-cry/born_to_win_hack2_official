using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp1.Form2;
using System.Collections;



namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private HeadersManager headersManager;
        private NamesManager namesManager;
        private SqlConnection sqlConnection;

        //private readonly List<string> rowNameList = new List<string>() { "ID", "Пол", "Зарплата", "Образование" };
        private readonly List<string> genderNames = new List<string>() { "мужской", "женский" };
        private readonly List<string> withmentorNames = new List<string>() { "есть", "нет" };
        private readonly List<string> postNames = new List<string>() { "машинист", "инженер" };
        //private readonly List<string> educationNames = new List<string>() { "Высшее неполное", "Высшее полное", "Среднее", "Общее" };

        private List<Diagram> diagramsList = new List<Diagram>();
        private static int counter = 0;

        public Form1()
        {
            InitializeComponent();
        }

        public void setNewDiagram(Diagram diagram)
        {
            diagramsList.Add(diagram);
            CreateLine(diagram.name, diagram.u_id);
        }

        public int getAndIncrementCounter()
        {
            return counter++;
        }

        private void generateContent(object sender, System.EventArgs e)
        {
            string filterName = ((ComboBox)sender).Text;
            string dbName = headersManager.getDbHeaderFromRu(filterName.ToLower());
            string[] namesList = namesManager.getElementsFromHeaders(dbName).ToArray();

            for (int i = 0; i < ((ComboBox)sender).Parent.Controls.Count; i++)
                if (((ComboBox)sender).Parent.Controls[i].Name == "timeElement")
                    ((ComboBox)sender).Parent.Controls[i].Dispose();

            switch (filterName.ToLower())
            {
                case "id":
                    ((ComboBox)sender).Parent.Name = dbName;
                    TextBox secondNameTextBox = new TextBox()
                    {
                        Parent = ((ComboBox)sender).Parent,
                        BorderStyle = BorderStyle.FixedSingle,
                        Text = "0"
                    };
                    secondNameTextBox.Name = "timeElement";
                    secondNameTextBox.Dock = DockStyle.Bottom;
                    break;
                case "пол":
                    ((ComboBox)sender).Parent.Name = dbName;
                    ComboBox genderComboBox = new ComboBox
                    {
                        Parent = ((ComboBox)sender).Parent,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    genderComboBox.Name = "timeElement";
                    genderComboBox.Dock = DockStyle.Bottom;
                    genderComboBox.Items.AddRange(namesList);
                    break;
                case "семейный статус":
                    ((ComboBox)sender).Parent.Name = dbName;
                    ComboBox comboBox = new ComboBox
                    {
                        Parent = ((ComboBox)sender).Parent,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    comboBox.Name = "timeElement";
                    comboBox.Dock = DockStyle.Bottom;
                    comboBox.Items.AddRange(namesList);
                    break;
                case "оклад":
                    ((ComboBox)sender).Parent.Name = dbName;
                    Panel thirdPanel = new Panel
                    {
                        Parent = ((ComboBox)sender).Parent,
                        AutoSize = true,
                        Dock = DockStyle.Bottom,
                        Name = "timeElement",
                    };
                    TextBox salaryTextBox1 = new TextBox()
                    {
                        Parent = thirdPanel,
                        Text = "0",
                        Dock = DockStyle.Bottom,
                        Name = "startSalary"
                    };

                    TextBox salaryTextBox2 = new TextBox()
                    {
                        Parent = thirdPanel,
                        Text = "1000000",
                        Dock = DockStyle.Bottom,
                        Name = "endSalary"
                    };
                    break;
                case "образование":
                    ((ComboBox)sender).Parent.Name = dbName;
                    ComboBox educationComboBox = new ComboBox
                    {
                        Parent = ((ComboBox)sender).Parent,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    educationComboBox.Name = "timeElement";
                    educationComboBox.Dock = DockStyle.Bottom;
                    educationComboBox.Items.AddRange(namesList);
                    break;
                case "должность":
                    ((ComboBox)sender).Parent.Name = dbName;
                    ComboBox postComboBox = new ComboBox
                    {
                        Parent = ((ComboBox)sender).Parent,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    postComboBox.Name = "timeElement";
                    postComboBox.Dock = DockStyle.Bottom;
                    postComboBox.Items.AddRange(namesList);
                    break;
                case "дата рождения":
                    ((ComboBox)sender).Parent.Name = dbName;
                    DateTimePicker borthdayNameTextBox = new DateTimePicker()
                    {
                        Parent = ((ComboBox)sender).Parent
                    };
                    borthdayNameTextBox.Name = "timeElement";
                    borthdayNameTextBox.Dock = DockStyle.Bottom;
                    break;
                case "дата приёма":
                    ((ComboBox)sender).Parent.Name = dbName;
                    DateTimePicker admissiondayNameTextBox = new DateTimePicker()
                    {
                        Parent = ((ComboBox)sender).Parent
                    };
                    admissiondayNameTextBox.Name = "timeElement";
                    admissiondayNameTextBox.Dock = DockStyle.Bottom;
                    break;
                case "дата увольнения":
                    ((ComboBox)sender).Parent.Name = dbName;
                    DateTimePicker dismissiuldayNameTextBox = new DateTimePicker()
                    {
                        Parent = ((ComboBox)sender).Parent
                    };
                    dismissiuldayNameTextBox.Name = "timeElement";
                    dismissiuldayNameTextBox.Dock = DockStyle.Bottom;
                    break;
                case "наименвание отсутствия":
                    ((ComboBox)sender).Parent.Name = dbName;
                    ComboBox absenceName = new ComboBox()
                    {
                        Parent = ((ComboBox)sender).Parent,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    absenceName.Name = "timeElement";
                    absenceName.Dock = DockStyle.Bottom;
                    absenceName.Items.AddRange(namesList);
                    break;
                case "календарные дни отсутствия":
                    ((ComboBox)sender).Parent.Name = dbName;
                    TextBox absencedaysTextBox = new TextBox()
                    {
                        Parent = ((ComboBox)sender).Parent,
                        BorderStyle = BorderStyle.FixedSingle,
                        Text = "0"
                    };
                    absencedaysTextBox.Name = "timeElement";
                    absencedaysTextBox.Dock = DockStyle.Bottom;
                    break;
                case "город":
                    ((ComboBox)sender).Parent.Name = dbName;
                    ComboBox cityTextBox = new ComboBox()
                    {
                        Parent = ((ComboBox)sender).Parent,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    cityTextBox.Name = "timeElement";
                    cityTextBox.Dock = DockStyle.Bottom;
                    cityTextBox.Items.AddRange(namesList);
                    break;
                case "количество детей":
                    ((ComboBox)sender).Parent.Name = dbName;
                    ComboBox childrenTextBox = new ComboBox()
                    {
                        Parent = ((ComboBox)sender).Parent,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    childrenTextBox.Name = "timeElement";
                    childrenTextBox.Dock = DockStyle.Bottom;
                    break;
               case "наставник":
                    ((ComboBox)sender).Parent.Name = dbName;
                    ComboBox withmentorComboBox = new ComboBox
                    {
                        Parent = ((ComboBox)sender).Parent,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    withmentorComboBox.Name = "timeElement";
                    withmentorComboBox.Dock = DockStyle.Bottom;
                    withmentorComboBox.Items.AddRange(new string[2] { "0", "1" });
                    break;
                case "причина увольнения":
                    ((ComboBox)sender).Parent.Name = dbName;
                    ComboBox dissmissial_reasonTextBox = new ComboBox()
                    {
                        Parent = ((ComboBox)sender).Parent,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    dissmissial_reasonTextBox.Name = "timeElement";
                    dissmissial_reasonTextBox.Dock = DockStyle.Bottom;
                    dissmissial_reasonTextBox.Items.AddRange(namesList);
                    break;
                case "период":
                    ((ComboBox)sender).Parent.Name = dbName;
                    Panel thirdPanel2 = new Panel
                    {
                        Parent = ((ComboBox)sender).Parent,
                        AutoSize = true,
                        Dock = DockStyle.Bottom,
                        Name = "timeElement",
                    };
                    DateTimePicker periodDateTimePicker1 = new DateTimePicker()
                    {
                        Parent = thirdPanel2,
                        Dock = DockStyle.Bottom,
                        Name = "startPeriod"
                    };

                    DateTimePicker periodDateTimePicker2 = new DateTimePicker()
                    {
                        Parent = thirdPanel2,
                        Dock = DockStyle.Bottom,
                        Name = "endSPeriod"
                    };
                    break;
                default:
                    ((ComboBox)sender).Parent.Name = "none";
                    break;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            tabPage1.Text = "Таблица";
            tabPage2.Text = "График";

            sqlConnection = new SqlConnection();
            headersManager = new HeadersManager(sqlConnection);
            namesManager = new NamesManager(sqlConnection);

            chart1.Series.Clear();
            dateTimePicker_to.Value = DateTime.Now;

            autosize_panel.Dock = DockStyle.Left;
            addButton.Dock = DockStyle.Left;
            addButton.BringToFront();
        }

        private void addGraph_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.ShowDialog();
        }

        private void RemoveLine(object sender, EventArgs e)
        {
            if (sender is Button)
                ((Button)sender).Parent.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Panel panel = new Panel
            {
                AutoSize = true,
                Parent = autosize_panel,
                Dock = DockStyle.Right,
                MaximumSize = new Size(150, 400)
            };

            ComboBox comboBox = new ComboBox
            {
                Parent = panel,
                Dock = DockStyle.Right,
                Name = "contentComboBox",
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            comboBox.Items.AddRange(headersManager.ruHeaders.ToArray());
            comboBox.SelectedIndexChanged += generateContent;

            Button button = new Button
            {
                Parent = panel,
                Text = "-",
                Dock = DockStyle.Right,
                MaximumSize = new Size(24, 24)
            };
            button.Click += RemoveLine;
        }

        //private int getInt(int i, Panel panel)
        //{
        //    for (int j = 0; j < panel.Controls[i].Controls.Count; j++)
        //        if (panel.Controls[i].Controls[j].GetType() == new TextBox().GetType())
        //            return int.Parse(panel.Controls[i].Controls[j].Text);
        //    return -1;
        //}
        //private string getStr(int i, Panel panel)
        //{
        //    for (int j = 0; j < panel.Controls[i].Controls.Count; j++)
        //    {
        //        if (panel.Controls[i].Controls[j].GetType() is TextBox)
        //            return panel.Controls[i].Controls[j].Text;
        //        if (panel.Controls[i].Controls[j].GetType() is ComboBox)
        //            return panel.Controls[i].Controls[j].Text;
        //    }
        //    return "";
        //}

        private List<List<string>> parsUserData(Panel mainPanel, string data1, string data2)
        {
            int personId = -1;
            string post = "";
            string b_date = "";
            string gender = "";
            string family_status = "";
            string admission_date = "";
            string dismissial_date = "";
            string absence_name = "";
            string absence_days = "";
            int startSalary = -1;
            int endSalary = -1;
            string city = "";
            string children = "";
            string with_mentor = "";
            string education = "";
            string dismissial_reason = "";

            for (int i = 0; i < mainPanel.Controls.Count; i++)
            {
                Console.WriteLine(mainPanel.Controls[i].Name);
                switch (mainPanel.Controls[i].Name)
                {
                    case "personal_number":
                        for (int j = 0; j < mainPanel.Controls[i].Controls.Count; j++)
                            if (mainPanel.Controls[i].Controls[j].GetType() == new TextBox().GetType())
                                personId = int.Parse(mainPanel.Controls[i].Controls[j].Text);
                        break;
                    case "post":
                        for (int j = 0; j < mainPanel.Controls[i].Controls.Count; j++)
                            if (mainPanel.Controls[i].Controls[j].GetType() == new ComboBox().GetType())
                                post = mainPanel.Controls[i].Controls[j].Text;
                        break;
                    case "b_date":
                        for (int j = 0; j < mainPanel.Controls[i].Controls.Count; j++)
                            if (mainPanel.Controls[i].Controls[j].GetType() == new DateTimePicker().GetType())
                                b_date = convertData(((DateTimePicker)mainPanel.Controls[i].Controls[j]).Value.ToShortDateString());
                        break;
                    case "gender":
                        for (int j = 0; j < mainPanel.Controls[i].Controls.Count; j++)
                            if (mainPanel.Controls[i].Controls[j].GetType() == new ComboBox().GetType())
                                gender = mainPanel.Controls[i].Controls[j].Text;
                        break;
                    case "family_status":
                        for (int j = 0; j < mainPanel.Controls[i].Controls.Count; j++)
                            if (mainPanel.Controls[i].Controls[j].GetType() == new ComboBox().GetType())
                                family_status = mainPanel.Controls[i].Controls[j].Text;
                        break;
                    case "admission_date":
                        for (int j = 0; j < mainPanel.Controls[i].Controls.Count; j++)
                            if (mainPanel.Controls[i].Controls[j].GetType() == new DateTimePicker().GetType())
                                admission_date = convertData(((DateTimePicker)mainPanel.Controls[i].Controls[j]).Value.ToShortDateString());
                        break;
                    case "dismissial_date":
                        for (int j = 0; j < mainPanel.Controls[i].Controls.Count; j++)
                            if (mainPanel.Controls[i].Controls[j].GetType() == new DateTimePicker().GetType())
                                dismissial_date = convertData(((DateTimePicker)mainPanel.Controls[i].Controls[j]).Value.ToShortDateString());
                        break;
                    case "absence_name":
                        for (int j = 0; j < mainPanel.Controls[i].Controls.Count; j++)
                            if (mainPanel.Controls[i].Controls[j].GetType() == new ComboBox().GetType())
                                absence_name = mainPanel.Controls[i].Controls[j].Text;
                        break;
                    case "absence_days":
                        for (int j = 0; j < mainPanel.Controls[i].Controls.Count; j++)
                            if (mainPanel.Controls[i].Controls[j].GetType() == new TextBox().GetType())
                                absence_days = mainPanel.Controls[i].Controls[j].Text;
                        break;
                    case "city":
                        for (int j = 0; j < mainPanel.Controls[i].Controls.Count; j++)
                            if (mainPanel.Controls[i].Controls[j].GetType() == new ComboBox().GetType())
                                city = mainPanel.Controls[i].Controls[j].Text;
                        break;
                    case "children":
                        for (int j = 0; j < mainPanel.Controls[i].Controls.Count; j++)
                            if (mainPanel.Controls[i].Controls[j].GetType() == new ComboBox().GetType())
                                children = mainPanel.Controls[i].Controls[j].Text;
                        break;
                    case "with_mentor":
                        for (int j = 0; j < mainPanel.Controls[i].Controls.Count; j++)
                            if (mainPanel.Controls[i].Controls[j].GetType() == new ComboBox().GetType())
                                with_mentor = mainPanel.Controls[i].Controls[j].Text;
                        break;
                    case "education":
                        for (int j = 0; j < mainPanel.Controls[i].Controls.Count; j++)
                            if (mainPanel.Controls[i].Controls[j].GetType() == new ComboBox().GetType())
                                education = mainPanel.Controls[i].Controls[j].Text;
                        break;
                    case "dismissial_reason":
                        for (int j = 0; j < mainPanel.Controls[i].Controls.Count; j++)
                            if (mainPanel.Controls[i].Controls[j].GetType() == new ComboBox().GetType())
                                education = mainPanel.Controls[i].Controls[j].Text;
                        break;
                    case "salary":
                        for (int j = 0; j < mainPanel.Controls[i].Controls.Count; j++)
                            if (mainPanel.Controls[i].Controls[j].GetType() == new Panel().GetType())
                                for (int l = 0; l < mainPanel.Controls[i].Controls[j].Controls.Count; l++)
                                {
                                    if (mainPanel.Controls[i].Controls[j].Controls[l].Name == "startSalary")
                                        startSalary = int.Parse(mainPanel.Controls[i].Controls[j].Controls[l].Text);
                                    if (mainPanel.Controls[i].Controls[j].Controls[l].Name == "endSalary")
                                        endSalary = int.Parse(mainPanel.Controls[i].Controls[j].Controls[l].Text);
                                }
                        break;
                }
            }

            List<string> out_tables = new List<string>() { "*" };
            List<string> queries = new List<string>();

            if (data1 != "")
                queries.Add("period >= " + convertData(data1));
            if (data2 != "")
                queries.Add("period <= " + convertData(data2));
            if (personId != -1)
                queries.Add("personal_number = " + personId.ToString());
            if (gender != "")
                queries.Add("gender = \'" + gender + "\'");
            if (education != "")
                queries.Add("education = \'" + education + "");
            if (startSalary != -1)
                queries.Add("salary >= " + startSalary);
            if (endSalary != -1)
                queries.Add("salary <= " + endSalary);
            if (city != "")
                queries.Add("city = \'" + city + "\'");
            if (children != "")
                queries.Add("children = \'" + children + "\'");
            if (with_mentor != "")
                queries.Add("with_mentor = \'" + with_mentor + "\'");
            if (absence_days != "")
                queries.Add("absence_days = " + absence_days + "");
            if (dismissial_reason != "")
                queries.Add("dismissial_reason = \'" + dismissial_reason + "\'");
            if (absence_name != "")
                queries.Add("absence_name = \'" + absence_name + "\'");
            if (dismissial_date != "")
                queries.Add("dismissial_date = " + dismissial_date);
            if (admission_date != "")
                queries.Add("admission_date = " + admission_date);
            if (family_status != "")
                queries.Add("family_status = \'" + family_status + "\'");
            if (gender != "")
                queries.Add("gender = \'" + gender + "\'");
            if (b_date != "")
                queries.Add("b_date = " + b_date);
            if (post != "")
                queries.Add("post = \'" + post + "\'");

            return sqlConnection.StartConnect(sqlConnection.createQuery(out_tables, queries), headersManager.dbHeaders);
            //Console.WriteLine(createQuery(personId, gender, education, startSalary, endSalary, data1, data2));
        }

        private string convertData(string d)
        {
            string[] d1 = d.Split('.');
            Array.Reverse(d1);
            return "\'" + String.Join("-", d1) + "\'";
        }

        private void showData(List<List<string>> data)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = headersManager.dbHeaders.Count;
            dataGridView1.RowCount = data.Count > 0 ? data.Count : 1;
            dataGridView1.RowHeadersVisible = false;

            for (int j = 0; j < headersManager.ruHeaders.Count; j++)
                dataGridView1.Columns[j].Name = headersManager.ruHeaders[j];
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data[0].Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = data[i][j];
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            string data1 = dateTimePicker_from.Value.ToShortDateString();
            string data2 = dateTimePicker_to.Value.ToShortDateString();
            List<List<string>> o = parsUserData(autosize_panel, data1, data2);
            showData(o);
        }


        //вторая вкладка работа со списком
        private void CreateLine(string name, int u_id)
        {
            string panelName = u_id.ToString();
            Panel panel = new Panel
            {
                Name = panelName,
                Parent = autosizePanel1,
                Dock = DockStyle.Top,
                MaximumSize = new Size(1000, 30),
                BorderStyle = BorderStyle.FixedSingle
            };

            CheckBox cb = new CheckBox
            {
                Parent = panel,
                Dock = DockStyle.Left,
                MaximumSize = new Size(20, 30),
                Padding = new Padding(5, 0, 0, 0),
                Checked = true
            };
            cb.CheckedChanged += changeVisibility;

            TextBox tb = new TextBox
            {
                Text = name,
                Parent = panel,
                Dock = DockStyle.Right,
                ReadOnly = true,
                MinimumSize = new Size(60, 30),
                Size = new Size(150, 30)
            };

            Button del = new Button
            {
                Parent = panel,
                Dock = DockStyle.Right,
                Text ="-",
                MaximumSize = new Size(30, 30)
            };
            del.Click += removePanel;
        }

        private int getIdByUId(int u_id)
        {
            for (int i = 0; i < diagramsList.Count; i++)
                if (diagramsList[i].u_id == u_id)
                    return i;
            return -1;
        }

        private void changeVisibility(object sender, EventArgs e)
        {
            int id = getIdByUId(int.Parse(((CheckBox)sender).Parent.Name));
            diagramsList[id].ChangeVisibility();
            Console.WriteLine(id);
        }

        private void removePanel(object sender, EventArgs e)
        {
            diagramsList.RemoveAt(getIdByUId(int.Parse(((Button)sender).Parent.Name)));
            RemoveLine(sender, e);
        }
    }
}
