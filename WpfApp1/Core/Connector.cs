using System;
using MySql.Data.MySqlClient;
using System.Collections;
using System.IO;

namespace WpfApp1
{
    public class Connector
    {
        MySqlConnection Conn;
        public Core.Account acc;


        public Connector()
        {
            /**
             * SQL connections requires that ssl certificate was in the same folder as executable 
             * 
             */

            string connString;
            string path = Environment.CurrentDirectory;
            path.Replace('/', '\\');
            connString = "Server=192.168.1.247;" + "CertificateFile=" + path + "\\client.pfx;" +
  "CertificatePassword=pass;" + " database=mydb; Uid=sslclient; Pwd=pass; SSL Mode=Required";
            Conn = new MySqlConnection(connString);
        }
        //    Certificate Store Location=CurrentUser;


        //Login with ssl and password
        //TODO check escaping characters to prevent sql injection
        public bool Login(string username, string pass)
        {
            try
            {
                MySqlCommand command = new MySqlCommand("select * from mydb.users where username = @username;", Conn);
                command.Parameters.AddWithValue("@username", username);
                MySqlDataReader reader;
                Conn.Open();

                reader = command.ExecuteReader();

                string[] readable = { "", "", "","" };
                while (reader.Read())
                {
                    for (int i = 0; i < 4; i++) {
                        if (!reader.IsDBNull(i))
                        {
                            readable[i] = (string)reader.GetValue(i);
                        }
                    }
                }
                Conn.Close();
                acc = new Core.Account(username, readable[3], readable[2]);

                return HashnSalt.Verify(pass, readable[1]);
            }
            catch (Exception e)
            {
                Conn.Close();
                throw e;
                //return false;
            }
        }


        //Read all values from mydb.Table
        public ArrayList[] ReadAll(string query, string[] colums)
        {
            ArrayList[] returnable = new ArrayList[colums.Length];
            try
            {
                for (int i = 0; i < colums.Length; i++)
                {
                    returnable[i] = new ArrayList();
                }
                MySqlCommand command = new MySqlCommand(query, Conn);
                MySqlDataReader reader;
                Conn.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < colums.Length; i++)
                    {
                        if (!reader.IsDBNull(i))
                        {
                            returnable[i].Add(reader[colums[i]].ToString());
                        }
                    }
                }
                Conn.Close();
                return returnable;
            }
            catch (Exception e)
            {
                string contents = "";
                foreach (string s in colums)
                {
                    contents += s + " ";
                }
                contents += "\n" + e.Message;
                WriteError(contents);
                Conn.Close();
                throw e;
            }
        }


        // Update, Delete, Insert so, on.
        public bool NonQuery(string[] col, string[] values, string query)
        {
            MySqlCommand command = new MySqlCommand(query, Conn);
            for (int i = 0; i < col.Length; i++)
            {
                command.Parameters.AddWithValue(col[i], values[i]);
            }
            try
            {
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry"))
                {
                    command.Connection.Close();
                    return false;
                }
                string contents = query;
                foreach (string s in values)
                {
                    contents += s + " ";
                }
                contents += "\n" + ex.Message;
                WriteError(contents);

                command.Connection.Close();
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        //Add an account to mydb.users
        public bool Register(string[] message)
        {
            string Query = "insert into mydb.users(username, pass, elev) values(@username ,@pass ,@elev);";

            MySqlCommand MyCommand2 = new MySqlCommand(Query, Conn);
            MyCommand2.Parameters.AddWithValue("@username", message[0]);
            MyCommand2.Parameters.AddWithValue("@pass", message[1]);
            MyCommand2.Parameters.AddWithValue("@elev", message[2]);

            try
            {

                MyCommand2.Connection.Open();
                MyCommand2.ExecuteNonQuery();
                MyCommand2.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                WriteError(Query + "\n" + message[0] + " " + message[1] + " " + message[2] + "\n" + ex.Message);
                MyCommand2.Connection.Close();
                throw ex;
                //return false;
            }
        }


        public void WriteError(string error)
        {
            Directory.CreateDirectory("Logs");
            string path = @"Logs\ErrorLog.txt";
            error = DateTime.Now.ToString() + "\n" + error + "\n---------------------------------------------------------------------------------------\n";

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(error);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(error);
                }
            }
        }
    }
}