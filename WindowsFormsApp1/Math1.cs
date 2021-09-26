using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace WindowsFormsApp1
{
    class Math1
    {
        public ArrayList get_month_from_data(List<string> start, List<string> finish)
        {
            string[] start_ = new string[start.Count];
            start_ = start.ToArray();


            string[] finish_ = new string[finish.Count];
            finish_ = finish.ToArray();


            int diff = 0;


            ArrayList arr_diff = new ArrayList();

            for (int i = 0; i < start.Count; i++)
            {
                string[] start_mount = start_[i].Split('.');
                if (finish_[i].ToString() != "01.01.1970" || finish_[i].ToString() == "")
                {
                    string[] end_mount = finish_[i].Split('.');
                    if (int.Parse(end_mount[2]) == int.Parse(start_mount[2]))
                    {
                        diff = int.Parse(end_mount[1]) - int.Parse(start_mount[1]);  /*# если месяц это 2 позиция*/
                    }

                    else
                    {
                        diff = (int.Parse(end_mount[2]) - int.Parse(start_mount[2].Substring(0, 4))) * 12 - int.Parse(start_mount[1]) + int.Parse(end_mount[1]);  // 12 month in year высчитывает месяцы.
                    }
                    arr_diff.Add(diff);
                }

            }

            return arr_diff;
            Console.WriteLine(arr_diff);
        }

        public ArrayList get_middle_salary(List<string> raw)
        {

            int max_sal = int.Parse(raw[0]);
            int min_sal = int.Parse(raw[0]);
            int middle_sal = 0;

            ArrayList arra = new ArrayList();

            for (int i = 0; i < raw.Count; i++)
            {  // цикл до конца
                middle_sal += (int.Parse(raw[i]));  // копит зарплату
                if (int.Parse(raw[i]) < min_sal)
                {  // сравнивает минимум зарплату
                    min_sal = int.Parse(raw[i]);
                }
                if (int.Parse(raw[i]) > max_sal)
                {  // сравнивает максимум зарплату
                    max_sal = int.Parse(raw[i]);
                }
                if (i % 120 == 0)
                {
                    arra.Add(middle_sal / 120);
                    middle_sal = 0;
                }
            }

            middle_sal /= raw.Count;
            Console.WriteLine(middle_sal.ToString(), "middle salary\n", max_sal.ToString(), "max salary\n", min_sal.ToString(), "min salary\n");
            return arra;
        }


        public ArrayList where_out(List<string> raw1)
        {
            int winter_out = 0;
            int spring_out = 0;
            int summer_out = 0;
            int autumn_out = 0;

            string[] raw;

            ArrayList arr = new ArrayList();


            for (int i = 0; i < raw1.Count; i++)
            {
                if (raw1[i].ToString() != "01.01.1970" || raw1[i].ToString() == "")
                {
                    raw = raw1[i].Split('.');
                    if (raw[1].ToString() == "01" || raw[1].ToString() == "02" || raw[1].ToString() == "03")  // if month eqwls
                    {
                        winter_out++;
                    }
                    else if (raw[1].ToString() == "04" || raw[1].ToString() == "05" || raw[1].ToString() == "06")  // if month eqwls
                    {
                        spring_out++;
                    }
                    else if (raw[1].ToString() == "07" || raw[1].ToString() == "08" || raw[1].ToString() == "09")  // if month eqwls
                    {
                        summer_out++;
                    }
                    else
                    {
                        autumn_out++;
                    }
                }


                Console.WriteLine("winter_out: ", winter_out, "\nspring_out ", spring_out, "\nsummer_out ", summer_out, "\nautumn_out ",
                    autumn_out, "\n");

            }
            arr.Add(winter_out);
            arr.Add(spring_out);
            arr.Add(summer_out);
            arr.Add(autumn_out);
            return arr;
        }

    }
}
