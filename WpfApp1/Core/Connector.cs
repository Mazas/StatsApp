﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data;
using System.IO;

namespace WpfApp1
{
    public class Connector
    {
        MySqlConnection Conn;
        public bool Admin { get; private set; }


        public Connector()
        {
            /**
             * SQL connections requires that ssl certificate was in the same folder as executable 
             * 
             */

            string connString;
            string path = Environment.CurrentDirectory;
            path.Replace('/','\\');
            connString = "Server=192.168.1.247;"+ "CertificateFile="+path+"\\client.pfx;" +
  "CertificatePassword=pass;"+" database=mydb; Uid=sslclient; Pwd=pass; SSL Mode=Required";
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
                string readable="";
                string elev = "";
                while (reader.Read())
                {
                    readable = reader.GetString("pass");
                    elev = reader.GetString("elev");
                }
                Conn.Close();
                
                if (elev.Equals("all"))
                {
                    Admin = true;
                }
                else
                {
                    Admin = false;
                }

                if (!HashnSalt.Verify(pass,readable)) {
                    Console.WriteLine(pass);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Conn.Close();
                throw e;
                //return false;
            }
        }

        // Load the last value from the database
        public string Read(string commandString)
        {
            string returnString = "";
            try
            {
                MySqlCommand command = new MySqlCommand(commandString, Conn);
                MySqlDataReader reader;
                Conn.Open();
                reader = command.ExecuteReader();
                int counter = 0;
                while (reader.Read())
                {
                    returnString = returnString + reader[counter++];
                }
            }
            catch (Exception e)
            {
                returnString = e.Message;
                WriteError(DateTime.Now.ToString() + "\n"+commandString+"\n"+e.Message+"\n---------------------------------------------------------------------------------------\n");
            }
            Conn.Close();
            return returnString;
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
                    for (int i=0;i<colums.Length;i++)
                    {
                        if (!reader.IsDBNull(i))
                        { 
                            returnable[i].Add(reader[colums[i]].ToString());
                        }
                    }
                }
                    /*ArrayList date = new ArrayList();
                    ArrayList Android = new ArrayList();
                    ArrayList Java = new ArrayList();
                    ArrayList net = new ArrayList();
                    ArrayList php = new ArrayList();
                    ArrayList cpp = new ArrayList();
                    ArrayList script = new ArrayList();
                    ArrayList ruby = new ArrayList();
                    ArrayList ios = new ArrayList();
                    MySqlCommand command = new MySqlCommand(query, Conn);
                    MySqlDataReader reader;
                    Conn.Open();
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        date.Add(reader.GetString("Date"));
                        Android.Add(reader.GetString("Android"));
                        Java.Add(reader.GetString("Java"));
                        net.Add(reader.GetString("C#.NET"));
                        php.Add(reader.GetString("PHP"));
                        cpp.Add(reader.GetString("Cpp"));
                        script.Add(reader.GetString("JavaScript"));
                        ruby.Add(reader.GetString("Ruby"));
                        ios.Add(reader.GetString("IOS"));
                    }
                    returnable[0] = date;
                    returnable[1] = Android;
                    returnable[2] = Java;
                    returnable[3] = net;
                    returnable[4] = php;
                    returnable[5] = cpp;
                    returnable[6] = script;
                    returnable[7] = ruby;
                    returnable[8] = ios;
                    */
                    Conn.Close();
                return returnable;
            }
            catch (Exception e)
            {
                string contents = DateTime.Now.ToString()+"\n";
                foreach (string s in colums)
                {
                    contents += s + " ";
                }
                contents+="\n" + e.Message + "\n---------------------------------------------------------------------------------------\n";
                WriteError(contents);
                Conn.Close();
                throw e;
            }
        }
        //Delete entry
        //public bool DeleteItem(string query)
        public bool NonQuery(string[] col, string[] values,string query)
        {
            MySqlCommand command = new MySqlCommand(query, Conn);
            for (int i=0;i<col.Length;i++)
            {
                command.Parameters.AddWithValue(col[i],values[i]);
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
                string contents = DateTime.Now.ToString()+"\n"+query+"\n";
                foreach(string s in values)
                {
                    contents += s + " ";
                }
                contents += "\n"+ex.Message+"\n---------------------------------------------------------------------------------------\n";
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
            MyCommand2.Parameters.AddWithValue("@username",message[0]);
            MyCommand2.Parameters.AddWithValue("@pass",message[1]);
            MyCommand2.Parameters.AddWithValue("@elev",message[2]);

            try
            {
                
                MyCommand2.Connection.Open();
                MyCommand2.ExecuteNonQuery();
                /*MySqlDataReader MyReader2;
                Conn.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                while (MyReader2.Read())
                {

                }
                */
                MyCommand2.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                WriteError(DateTime.Now.ToString() + "\n" + Query + "\n"+message[0]+ " "+message[1]+" "+message[2]+"\n" + ex.Message + "\n---------------------------------------------------------------------------------------\n");
                MyCommand2.Connection.Close();
                throw ex;
                //return false;
            }
        }
        private void WriteError(string error)
        {
            System.IO.Directory.CreateDirectory("Logs");
            string path = @"Logs\ErrorLog.txt";

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