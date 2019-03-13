﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Personen_db
{
    public class AddressDB
    {
        //SqlConnection myConnection = new SqlConnection();
        string datapath = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + "\"" + @"D:\Development\Visual Studio 2017\source\repos\C#\Personen_db\Personen_db\AddressBook.mdf" + "\"" + ";Integrated Security=True;Connect Timeout=30";

        public void Database()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "xxx";
            builder.InitialCatalog = "xxx";
            builder.UserID = "xxx";
            builder.Password = "xxx";
            builder.IntegratedSecurity = false;
            builder.NetworkLibrary = "dbmssocn"; //TCP/IP
            //string datapath = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + "\"" + @"D:\Development\Visual Studio 2013\Projects\C#\Personen_db\Personen_db\AddressBook.mdf" + "\"" + ";Integrated Security=True;Connect Timeout=30";

            //SqlConnection myConnection = new SqlConnection(builder.ToString());
            //myConnection = new SqlConnection(datapath);
        }

        //public void Open()
        //{
        //    try
        //    {
        //        myConnection.Open();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        //public void Close()
        //{
        //    myConnection.Close();
        //}

        //public string GetID(string Id, string Tabelle)
        //{
        //    string result = "Keine ID gefunden";

        //    StringBuilder sql = new StringBuilder();
        //    sql.Append("SELECT @Id FROM @Tabelle");



        //    SqlCommand cmd = new SqlCommand(sql.ToString(), myConnection);
        //    cmd.Parameters.Add(new SqlParameter("Id", Id));
        //    cmd.Parameters.Add(new SqlParameter("Tabelle", Tabelle));

        //    try
        //    {
        //        IDataReader reader = cmd.ExecuteReader();
        //        try
        //        {
        //            if (reader.Read())
        //            {
        //                result = Convert.ToString(reader["Id"]);
        //            }
        //        }
        //        finally
        //        {
        //            reader.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return result;

        //}

        public int Insert(Person person)
        {
            int index;
            using (SqlConnection connection = new SqlConnection(datapath)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    //string sql = "Insert into Address Values('" + Guid.NewGuid() + "','"
                    string sql = "Insert into Address Values('"
                        + person.FName + "','"
                        + person.LName + "','"
                        + person.Street + "','"
                        + person.Number + "','"
                        + person.Plz + "','"
                        + person.Location + "','"
                        + person.Telephone + "','"
                        + person.Email + "')";
                    command.CommandText = sql;
                    int affectedRows = command.ExecuteNonQuery();
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    index=Convert.ToInt32(command.ExecuteScalar());
                    //MessageBox.Show("After insert, affected rows: " + affectedRows, "Affected", MessageBoxButtons.OK);
                    //MessageBox.Show("After insert, Index " + index, "Affected", MessageBoxButtons.OK);
                }
                connection.Close();
            }
            return index;
        }

        public bool RemoveFromDb(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(datapath))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        string sql = "Delete from Address where id="+id.ToString();
                        command.CommandText = sql;
                        int affectedRows = command.ExecuteNonQuery();
                        //MessageBox.Show("After delete, affected rows: " + affectedRows, "Affected", MessageBoxButtons.OK);
                    }
                    connection.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Person> ReadDB()
        {
            List<Person> readAddresses = new List<Person>();
            //Person address = new Person();
            int i = 0;
            using (SqlConnection connection = new SqlConnection(datapath))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "Select * from Address order by Nachname";
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        Person address = new Person
                        {
                            Id = reader.GetInt32(0),
                            FName = reader.GetString(1),
                            LName = reader.GetString(2),
                            Street = reader.GetString(3),
                            Number = reader.GetString(4),
                            Plz = reader.GetString(5),
                            Location = reader.GetString(6),
                            Telephone = reader.GetString(7),
                            Email = reader.GetString(8)
                        };
                        readAddresses.Add(address);
                        i++;
                    }
                    connection.Close();
                }
            }
            return readAddresses;
        } 
    }
}
