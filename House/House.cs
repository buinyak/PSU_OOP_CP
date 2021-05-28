using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House
{
    static class House
    {
        static private string database = "house";
        static public string constr;
        static public DataTable newHouse()
        {
            DB db = new DB(constr);
            constr += "Database=house;";
            db.createDatabase(House.database);
            string tableParams = "(id INT NOT NULL primary key AUTO_INCREMENT, surname varchar(255),name varchar(255),patronymic varchar(255),profit varchar(255),Rent varchar(255),Enegry varchar(255),Utilities varchar(255),Heat varchar(255),Water varchar(255));";
            return db.createTable(House.database,tableParams);
        }
        static public void insertOwner(string[] odata)
        {
            DB db = new DB(constr);
            string request = String.Format("INSERT INTO house (surname,name,patronymic,profit,Rent,Enegry,Utilities,Heat,Water) VALUES ('{0}', '{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", odata[0], odata[1], odata[2], odata[3], odata[4], odata[5], odata[6], odata[7], odata[8]);
            db.query(request);

        }
        static public void updateOwner(string[] odata,int id)
        {
            DB db = new DB(constr);
            string request = String.Format("UPDATE house SET surname = '{0}',name = '{1}',patronymic = '{2}',profit = '{3}',Rent = '{4}',Enegry = '{5}',Utilities = '{6}',Heat = '{7}',Water = '{8}' WHERE id =" + id, odata[0], odata[1], odata[2], odata[3], odata[4], odata[5], odata[6], odata[7], odata[8]);
            db.query(request);
        }
        static public void deleteOwner(string id)
        {
            DB db = new DB(constr);
            string request = "DELETE FROM house WHERE id =" + id + ";";
            db.query(request);

        }
        static public DataTable getOwners()
        {
            DB db = new DB(constr);
            string request = "SELECT * from house";
            return db.getDataTable(request);
        }
        static public MySqlBackup saveHouseData()
        {
            DB db = new DB(constr);
            return db.fileBackup();

        }
        static public void deleteHouse()
        {
            DB db = new DB(constr);
            constr = constr.Replace("Database=house;", "");
            db.dropDatabase("HOUSE");
        }
        static public bool isCreated()
        {
            DB db = new DB(House.constr+ "Database=house;");
            if (db.checkConnection())
            {
                House.constr += "Database=house;";
            }
            return db.checkConnection();
        }
        static public string[] takeOwner(int id)
        {
            string request = "SELECT * FROM house WHERE ID = " + id;
            string[] data = new string[9];
            DB db = new DB(constr);
            MySqlDataReader reader = db.takeById(House.database,id);
            while (reader.Read())
            {
                data[0] = reader[1].ToString();
                data[1] = reader[2].ToString();
                data[2] = reader[3].ToString();
                data[3] = reader[4].ToString();
                data[4] = reader[5].ToString();
                data[5] = reader[6].ToString();
                data[6] = reader[7].ToString();
                data[7] = reader[8].ToString();
                data[8] = reader[9].ToString();
            }
            reader.Close();
            return data;
        }
        static public DataTable filterOwners(string[] odata,string[] filters)
        {
            DB db = new DB(constr);
            string request = String.Format("SELECT * from house WHERE" + (odata[0] == "" ? "" : " surname = '{0}' AND") + (odata[1] == "" ? "" : " name = '{1}' AND") + (odata[2] == "" ? "" : " patronymic = '{2}' AND") + (odata[3] == "" ? "" : " profit " + (filters[0] == "ANY" ? "=" : filters[0]) + " {3} AND") + (odata[4] == "" ? "" : " Rent " + (filters[1] == "ANY" ? "=" : filters[1]) + " {4} AND") + (odata[5] == "" ? "" : " Enegry " + (filters[2] == "ANY" ? "=" : filters[2]) + " {5} AND") + (odata[6] == "" ? "" : " Utilities " + (filters[3] == "ANY" ? "=" : filters[3]) + " {6} AND") + (odata[7] == "" ? "" : " Heat " + (filters[4] == "ANY" ? "=" : filters[5]) + " {7} AND") + (odata[8] == "" ? "" : " Water " + (filters[6] == "ANY" ? "=" : filters[6]) + " {8} AND"), odata[0], odata[1], odata[2], odata[3], odata[4], odata[5], odata[6], odata[7], odata[8]);
            request = request.Substring(0, request.Length - 3);
            return db.getDataTable(request);

        }
        static public int searchOwner(string[] odata)
        {
            string request = String.Format("SELECT id from house WHERE" + (odata[0] == "" ? "" : " surname = '{0}' AND") + (odata[1] == "" ? "" : " name = '{1}' AND") + (odata[2] == "" ? "" : " patronymic = '{2}' AND") + (odata[3] == "" ? "" : " profit = {3} AND") + (odata[4] == "" ? "" : " Rent = {4} AND") + (odata[5] == "" ? "" : " Enegry = {5} AND") + (odata[6] == "" ? "" : " Utilities = {6} AND") + (odata[7] == "" ? "" : " Heat = {7} AND") + (odata[8] == "" ? "" : " Water = {8} AND"), odata[0], odata[1], odata[2], odata[3], odata[4], odata[5], odata[6], odata[7], odata[8]);
            request = request.Substring(0, request.Length - 3);
            DB db = new DB(constr);
            MySqlDataReader reader = db.takeByRequest(request);
            while (reader.Read())
            {
                return Convert.ToInt32(reader[0]);
            }
            return -1;
        }
    }
}
