using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House
{
    class DB
    {
        private string constr;
        private MySqlConnection mycon;
        private MySqlCommand mycom;
        public DB(string constr)
        {
            this.constr = constr;

        }
        public bool checkConnection()
        {
            try
            {
                string request = "SHOW DATABASES LIKE 'house';";
                query(request);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private MySqlConnection Connection()
        {
            mycon = new MySqlConnection(constr);
            mycon.Open();
            return mycon;
        }
        public void query(string request)
        {
            Connection();
            mycom = new MySqlCommand(request, mycon);
            mycom.ExecuteNonQuery();
            mycon.Close();
        }
        public DataTable getDataTable(string request)
        {
            Connection();
            MySqlDataAdapter ms_data = new MySqlDataAdapter(request, constr);
            DataTable table = new DataTable();
            ms_data.Fill(table);
            mycon.Close();
            return table;
        }
        public void dropDatabase(string database)
        {
            string request = "DROP DATABASE "+database;
            query(request);
        }
        public MySqlBackup fileBackup()
        {
            Connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mycon;
            return new MySqlBackup(cmd);

        }
        public void createDatabase(string dbName)
        {
            string request = "CREATE DATABASE " + dbName + " CHARACTER SET 'cp1251'";
            query(request);
        }
        public DataTable createTable(string dbName,string tparams)
        {
            string request = "CREATE TABLE "+dbName +tparams;
            constr += "Database=" + dbName + ";";
            return getDataTable(request);
        }
        public MySqlDataReader takeById(string dbName , int id)
        {
            string request = "SELECT * FROM "+dbName+" WHERE ID = " + id;
            Connection();
            mycom = new MySqlCommand(request, mycon);
            return mycom.ExecuteReader();
        }
        public MySqlDataReader takeByRequest(string request)
        {
            Connection();
            mycom = new MySqlCommand(request, mycon);
            return mycom.ExecuteReader();
        }
    }
}
