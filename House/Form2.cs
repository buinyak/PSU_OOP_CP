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
        public DataTable filter_data;
        public int getId()
        {
            return this.id;
        }
        public Form2(String task, int ident)
        {
            
            InitializeComponent();
            comboboxesHide();
            this.Text = "Изменение записи id = " + ident;
            this.id = ident;
            this.task = task;
            string[] data = House.takeOwner(ident);
            textBox1.Text = data[0];
            textBox2.Text = data[1];
            textBox3.Text = data[2];
            textBox4.Text = data[3];
            textBox5.Text = data[4];
            textBox6.Text = data[5];
            textBox7.Text = data[6];
            textBox8.Text = data[7];
            textBox9.Text = data[8];
            button1.Text = "Изменить запись";

        }
        public Form2(String task)
        {
            
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

            string[] data = { textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text};
            try { 
                switch (task)
                {
                    case "insert":
                        House.insertOwner(data);
                        break;
                    case "update":
                        House.updateOwner(data,this.id);
                        break;
                    case "filter":
                        string[] filters = { comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), comboBox3.SelectedItem.ToString(), comboBox4.SelectedItem.ToString(), comboBox5.SelectedItem.ToString(), comboBox6.SelectedItem.ToString() };
                        filter_data = House.filterOwners(data, filters);
                        break;
                    case "search":
                        this.id = House.searchOwner(data);
                        break;
                }
            }
            
                catch
            {
                MessageBox.Show("Database connection lost");  
            }
            Close();
}
    }
}
