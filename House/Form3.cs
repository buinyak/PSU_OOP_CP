using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace House
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            Form4 form4 = new Form4();
            InitializeComponent();

        }
        public string constr;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                constr = "datasource="+textBox1.Text+";port="+textBox2.Text + ";username="+ textBox3.Text + ";password="+ textBox4.Text + ";CharSet=utf8;";
                MySqlConnection con = new MySqlConnection(constr);
                con.Open();
                con.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
            }
            this.Hide();
            Form1 form1 = new Form1(constr);
            form1.ShowDialog();
            this.Close();
        }
    }
}
