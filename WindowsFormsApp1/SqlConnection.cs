using System;
using System.Collections.Generic;
using System.Collections;
using Npgsql;

class SqlConnection
{
    private static string Host = "ec2-79-125-30-28.eu-west-1.compute.amazonaws.com";
    private static string User = "altspigjyspokq";
    private static string DBname = "d9ttj1lg5b6q44";
    private static string Password = "048b6fa308eab66f87571978cf1bde69b60dc866666ba475140635b6eef11209";
    private static string Port = "5432";

    string connString = String.Format(
    "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Require;Trust Server Certificate=true",
    Host,
    User,
    DBname,
    Port,
    Password);

    private const string TABLE_NAME = "workers_table";


    public string createQuery(List<string> out_tables, List<string> queries)
    {
        // пример: out_tables = {"*"} -> "*",
        // {"salary", gender} -> "salary, gender"

        // пример: queries = {"salary >= 5000", "salary <= 99000"} -> "{table_name}.salary >= 5000 and {table_name}.salary <= 99000"

        string selectQuery = String.Join(", ", out_tables.ToArray());

        List<string> q_names = new List<string>();
        foreach (string q in queries)
            q_names.Add(TABLE_NAME + "." + q);

        string withQuery = String.Join(" and ", q_names);

        return "select " + selectQuery + " from " + TABLE_NAME + " where " + withQuery + ";";
    }

    private string convertToString(Npgsql.NpgsqlDataReader reader, int i, List<string> headers)
    {
        switch (headers[i])
        {
            case "salary":
                return reader.GetInt32(i).ToString();
                break;
            case "personal_number":
                return reader.GetInt32(i).ToString();
                break;
            case "post":
                return reader.GetString(i);
            case "b_date":
                return reader.GetDateTime(i).ToShortDateString();
                break;
            case "gender":
                return reader.GetString(i);
                break;
            case "family_status":
                return reader.GetString(i);
                break;
            case "admission_date":
                return reader.GetDateTime(i).ToShortDateString();
                break;
            case "dismissial_date":
                return reader.GetDateTime(i).ToShortDateString();
                break;
            case "absence_name":
                return reader.GetString(i);
                break;
            case "absence_days":
                return reader.GetInt32(i).ToString();
                break;
            case "city":
                return reader.GetString(i);
                break;
            case "children":
                return reader.GetInt32(i).ToString();
                break;
            case "with_mentor":
                return reader.GetInt32(i).ToString();
                break;
            case "period":
                return reader.GetDateTime(i).ToShortDateString();
                break;
            case "education":
                return reader.GetString(i);
                break;
            case "dissmissial_reason":
                return reader.GetString(i);
                break;
        }
        return "_____";
    }

    public List<string> getHeaders(string tableName)
    {
        List<string> output = new List<string>();
        using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open();
            using (var command = new NpgsqlCommand("select column_name from d9ttj1lg5b6q44.information_schema.columns where table_name = '" + tableName + "';", conn))
            {
                Npgsql.NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    for (int i = 0; i < reader.FieldCount; i++)
                        output.Add(reader.GetString(i));
            }
        }
        return output;
    }

    public List<List<string>> getNames()
    {
        List<string> raw = new List<string>();
        using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open();
            using (var command = new NpgsqlCommand("select * from unique_table;", conn))
            {
                Npgsql.NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    for (int i = 0; i < reader.FieldCount; i++)
                        raw.Add(reader.GetString(i));
            }
        }
        List<List<string>> output = new List<List<string>>();

        for (int i = 0; i < raw.Count; i++)
        {
            string[] t = raw[i].Trim('&').Split('&');
            output.Add(new List<string>(t));
        }
        return output;
    }

    public List<List<string>> StartConnect(string q, List<string> headers)
    {
        //List<string> headers = getHeaders();

        List<List<string>> result = new List<List<string>>();
        Console.WriteLine(q);
        using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open();

            List<ArrayList> output = new List<ArrayList>();
            using (var command = new NpgsqlCommand(q, conn))
            {
                Npgsql.NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    List<string> out_ = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        out_.Add(convertToString(reader, i, headers));
                    //Console.WriteLine(String.Join(" ", out_.ToArray()));
                    result.Add(out_);
                }
                reader.Close();
            }
        }
        return result;
    }
}
