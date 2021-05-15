using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace House
{
    public partial class Form2 : Form
    {
        private String task;
        private int id;
        private string constr;
        private MySqlConnection mycon;
        private MySqlCommand mycom;
        private DataSet ds;
        public MySqlDataAdapter filter_data;
        public int getId()
        {
            return this.id;
        }
        public Form2(String constr,String task, int ident)
        {
            
            this.constr = constr;
            InitializeComponent();
            comboboxesHide();
            this.Text = "Изменение записи id = " + ident;
            this.id = ident;
            this.task = task;
            string request = "SELECT * FROM house WHERE ID = " + ident;
            mycon = new MySqlConnection(constr);
            mycon.Open();
            mycom = new MySqlCommand(request, mycon);
            MySqlDataReader reader = mycom.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = reader[1].ToString();
                textBox2.Text = reader[2].ToString();
                textBox3.Text = reader[3].ToString();
                textBox4.Text = reader[4].ToString();
                textBox5.Text = reader[5].ToString();
                textBox6.Text = reader[6].ToString();
                textBox7.Text = reader[7].ToString();
                textBox8.Text = reader[8].ToString();
                textBox9.Text = reader[9].ToString();
            }
            reader.Close();
            mycon.Close();
            button1.Text = "Изменить запись";

        }
        public Form2(String constr,String task)
        {
            
            this.constr = constr;
            this.task = task;
            InitializeComponent();

            switch (task)
            {
                case "insert":
                    comboboxesHide();
                    this.Text = "Создание записи";
                    button1.Text = "Создать запись";
                    break;
                case "search":
                    comboboxesHide();
                    this.id = -1;
                    this.Text = "Поиск записи";
                    button1.Text = "Найти запись";
                    break;
                case "filter":
                    comboboxesShow();
                    button1.Text = "Фильтрация";
                    this.Text = "Фильтрация записей";
                    break;
            }
        }
        private void comboboxesShow()
        {
            comboBox1.Show();
            comboBox2.Show();
            comboBox3.Show();
            comboBox4.Show();
            comboBox5.Show();
            comboBox6.Show();
        }
        private void comboboxesHide()
        {
            comboBox1.Hide();
            comboBox2.Hide();
            comboBox3.Hide();
            comboBox4.Hide();
            comboBox5.Hide();
            comboBox6.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string surname, name, patronymic,profit, Rent, Enegry, Utilities, Heat, Water;
            surname = textBox1.Text;
            name = textBox2.Text;
            patronymic = textBox3.Text;
            profit = textBox4.Text;
            Rent = textBox5.Text;
            Enegry = textBox6.Text;
            Utilities = textBox7.Text;
            Heat = textBox8.Text;
            Water = textBox9.Text;
            mycon = new MySqlConnection(constr);
            mycon.Open();
            try { 
                string request = "";
                switch (task)
                {
                    case "insert":
                        request = String.Format("INSERT INTO house (surname,name,patronymic,profit,Rent,Enegry,Utilities,Heat,Water) VALUES ('{0}', '{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", surname, name, patronymic, profit, Rent, Enegry, Utilities, Heat, Water);
                        mycom = new MySqlCommand(request, mycon);
                        mycom.ExecuteNonQuery();
                        break;
                    case "update":
                        request = String.Format("UPDATE house SET surname = {0},name = {1},patronymic = {2},profit = {3},Rent = {4},Enegry = {5},Utilities = {6},Heat = {7},Water = {8} WHERE ID =" + this.id, surname, name, patronymic, profit, Rent, Enegry, Utilities, Heat, Water);
                        mycom = new MySqlCommand(request, mycon);
                        mycom.ExecuteNonQuery();
                        break;
                    case "filter":
                        request = String.Format("SELECT * from house WHERE" + (surname == "" ? "" : " surname = '{0}' AND") + (name == "" ? "" : " name = '{1}' AND") + (patronymic == "" ? "" : " patronymic = '{2}' AND") + (profit == "" ? "" : " profit "+ (comboBox1.SelectedItem == "ANY" ? "=" : comboBox1.SelectedItem) + " {3} AND") + (Rent == "" ? "" : " Rent " + (comboBox2.SelectedItem == "ANY" ? "=" : comboBox2.SelectedItem) + " {4} AND") + (Enegry == "" ? "" : " Enegry " + (comboBox3.SelectedItem == "ANY" ? "=" : comboBox3.SelectedItem) + " {5} AND") + (Utilities == "" ? "" : " Utilities " + (comboBox4.SelectedItem == "ANY" ? "=" : comboBox4.SelectedItem) + " {6} AND") + (Heat == "" ? "" : " Heat " + (comboBox5.SelectedItem == "ANY" ? "=" : comboBox5.SelectedItem) + " {7} AND") + (Water == "" ? "" : " Water " + (comboBox6.SelectedItem == "ANY" ? "=" : comboBox6.SelectedItem) + " {8} AND"), surname, name, patronymic, profit, Rent, Enegry, Utilities, Heat, Water);
                        request = request.Substring(0, request.Length - 3);
                        filter_data = new MySqlDataAdapter(request, constr);
                        break;
                    case "search":
                        request = String.Format("SELECT id from house WHERE" + (surname == "" ? "" : " surname = '{0}' AND") + (name == "" ? "" : " name = '{1}' AND") + (patronymic == "" ? "" : " patronymic = '{2}' AND") + (profit == "" ? "" : " profit = {3} AND") + (Rent == "" ? "" : " Rent = {4} AND") + (Enegry == "" ? "" : " Enegry = {5} AND") + (Utilities == "" ? "" : " Utilities = {6} AND") + (Heat == "" ? "" : " Heat = {7} AND") + (Water == "" ? "" : " Water = {8} AND"), surname, name, patronymic, profit, Rent, Enegry, Utilities, Heat, Water);
                        request = request.Substring(0, request.Length - 3);
                        mycom = new MySqlCommand(request, mycon);
                        MySqlDataReader reader = mycom.ExecuteReader();
                        if (reader.Read())
                        {
                            this.id = (int)reader[0];
                        }
                        reader.Close();
                        break;
                }
                mycon.Close();
            }
            
            catch
            {
                MessageBox.Show("Database connection lost");  
            }
            Close();
}
    }
}
