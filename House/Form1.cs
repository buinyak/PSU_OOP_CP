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
using MySqlX.XDevAPI.Relational;

namespace House
{
    public partial class Form1 : Form
    {
        public Form1(String scon)
        {
            
            InitializeComponent();
            database = scon;
            constr = database + "Database=house;";
            Text = "ООП КР Буйняков Г.Э. 19ВП2";
            progressBar1.Hide();
            checkDB();
        }

        private string database;
        private string constr;
        private MySqlConnection mycon;
        private MySqlCommand mycom;
        public DataSet ds;
        private string checked_column;
        public void checkDB()
        {
            try
            {
                string request = "SHOW DATABASES LIKE 'house';";
                mycon = new MySqlConnection(constr);
                mycon.Open();
                MySqlCommand cmd = new MySqlCommand(request, mycon);
                cmd.ExecuteNonQuery();
                mycon.Close();
                settingsShow();
                table_load();
            }catch {
                settingsHide();
            }
        }
        private void fillDB()
        {
            string[] surnames = { "Смирнов", "Иванов", "Кузнецов", "Попов", "Соколов", "Лебедев", "Козлов", "Новиков", "Морозов", "Петров", "Волков", "Соловьев", "Васильев", "Зайцев", "Павлов", "Семенов", "Голубев", "Виноградов", "Богданов", "Воробьев", "Федоров", "Михайлов", "Беляев", "Тарасов", "Белов", "Комаров", "Орлов", "Киселев", "Макаров", "Андреев", "Ковалев", "Ильин", "Гусев", "Титов", "Кузьмин", "Кудрявцев", "Баранов", "Куликов", "Алексеев", "Степанов", "Яковлев", "Сорокин", "Сергеев", "Романов", "Захаров", "Борисов", "Королев", "Герасимов", "Пономарев", "Григорьев", "Лазарев", "Медведев", "Ершов", "Никитин", "Соболев", "Рябов", "Поляков", "Цветков", "Жуков", "Фролов", "Журавлев", "Николаев", "Крылов", "Максимов", "Сидоров", "Осипов", "Белоусов", "Федотов", "Дорофеев", "Егоров", "Матвеев", "Бобров", "Дмитриев", "Калинин", "Анисимов", "Петухов", "Антонов", "Тимофеев", "Никифоров", "Веселов", "Филиппов", "Марков", "Большаков", "Суханов", "Миронов", "Ширяев", "Александров", "Коновалов", "Шестаков", "Казаков", "Ефимов", "Денисов", "Громов", "Фомин", "Давыдов", "Мельников", "Щербаков", "Блинов", "Колесников" };
            string[] names = { "Александр", "Михаил", "Максим", "Даниил", "Дмитрий", "Иван", "Роман", "Матвей", "Кирилл", "Егор", "Илья", "Тимофей", "Сергей", "Павел", "Владимир", "Никита", "Ярослав", "Марк", "Алексей", "Константин", "Николай", "Арсений", "Артем", "Андрей", "Евгений", "Лев", "Богдан", "Федор", "Мирон", "Виктор", "Владислав", "Георгий", "Степан", "Макар", "Глеб", "Давид", "Платон", "Захар", "Вячеслав", "Тимур", "Олег", "Святослав", "Юрий", "Дамир", "Мирослав", "Роберт", "Артемий", "Назар", "Геннадий", "Герман"};
            string[] patronymices = { "Адамович", "Александрович", "Алексеевич", "Анатольевич", "Андреевич", "Антонович", "Аркадьевич", "Артемович", "Борисович", "Брониславович", "Вадимович", "Валентинович", "Валерьевич", "Васильевич", "Викторович", "Витальевич", "Владимирович", "Владиславович", "Всеволодович", "Вячеславович:", "Гавриилович", "Геннадьевич", "Георгиевич", "Германович", "Глебович", "Григорьевич", "Даниилович", "Денисович", "Дмитриевич", "Евгеньевич", "Егорович", "Ефимович" };
            int[] profits = new int[50];
            int[] rents = new int[50];
            int[] energys = new int[50];
            int[] utilitiess = new int[50];
            int[] heats = new int[50];
            int[] waters = new int[50];
            Random rnd = new Random();
            for (int i = 0; i < profits.Length; i++)
            {
                profits[i] = rnd.Next(18000, 50000);
                rents[i] = rnd.Next(6000, 10000);
                energys[i] = rnd.Next(100, 600);
                utilitiess[i] = rnd.Next(500, 1500);
                heats[i] = rnd.Next(2000, 5000);
                waters[i] = rnd.Next(200, 1000);
            }
            mycon = new MySqlConnection(constr);
            mycon.Open();
            string request, name, surname, patronymic;
            int profit, utilities,rent, energy, heat, water;
            for (int i = 0; i < surnames.Length; i++)
            {
                
                name = names[rnd.Next(0, names.Length - 1)];
                surname = surnames[rnd.Next(0, surnames.Length - 1)];
                patronymic = patronymices[rnd.Next(0, patronymices.Length - 1)];
                profit = profits[rnd.Next(0, profits.Length - 1)];
                rent = rents[rnd.Next(0, rents.Length - 1)];
                energy = energys[rnd.Next(0, energys.Length - 1)];
                utilities = utilitiess[rnd.Next(0, utilitiess.Length - 1)];
                heat = heats[rnd.Next(0, heats.Length - 1)];
                water = waters[rnd.Next(0, waters.Length - 1)];
                request = String.Format("INSERT INTO house (surname,name,patronymic,profit,Rent,Enegry,Utilities,Heat,Water) VALUES ('{0}', '{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", surname, name, patronymic, profit, rent, energy, utilities, heat, water);
                mycom = new MySqlCommand(request, mycon);
                mycom.ExecuteNonQuery();
                progressBar1.PerformStep();
            } 
            
            mycon.Close();

        }
        public void settingsHide()
        {
            button1.Show();
            button2.Hide();
            button3.Hide();
            button4.Hide();
            button5.Hide();
            button6.Hide();
            button7.Hide();
            button8.Hide();
            button9.Hide();
            button10.Hide();
        }
        public void settingsShow()
        {
            button1.Hide();
            button2.Show();
            button3.Show();
            button4.Show();
            button5.Show();
            button6.Show();
            button7.Show();
            button8.Show();
            button9.Show();
            button10.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                string request = "CREATE DATABASE HOUSE CHARACTER SET 'cp1251'";
                mycon = new MySqlConnection(database);
                mycon.Open();
                mycom = new MySqlCommand(request, mycon);
                mycom.ExecuteNonQuery();
                mycon.Close();

                request = "CREATE TABLE House(id INT NOT NULL primary key AUTO_INCREMENT,surname varchar(255),name varchar(255),patronymic varchar(255),profit varchar(255),Rent varchar(255),Enegry varchar(255),Utilities varchar(255),Heat varchar(255),Water varchar(255)); ";
                mycon = new MySqlConnection(constr);
                mycon.Open();
                MySqlDataAdapter ms_data = new MySqlDataAdapter(request, constr);
                DataTable table = new DataTable();
                ms_data.Fill(table);
                dataGridView1.DataSource = table;
                mycon.Close();
                table_load();
                settingsShow();
            }
            catch
            {
                
                MessageBox.Show("Подключение отсутствует");
            }
            
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null) { MessageBox.Show("Не создана база данных"); return; }
            if (dataGridView1.Rows.Count == 1) { MessageBox.Show("В базе данных нет записей"); return; }
            if (dataGridView1.SelectedCells[0].RowIndex == dataGridView1.Rows.Count - 1) return;
            string delete = dataGridView1[0, dataGridView1.SelectedCells[0].RowIndex].Value.ToString();
            DialogResult dialogResult = MessageBox.Show("Вы действительно хотите удалить запись c id = "+delete, "Удаление", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string request = "DELETE FROM house WHERE id =" + delete + ";";
                mycon = new MySqlConnection(constr);
                mycon.Open();
                mycom = new MySqlCommand(request, mycon);
                mycom.ExecuteNonQuery();
                mycon.Close();
                table_load();
            }
            
            
        }
        private void table_load()
        {
            string request;
            try { 
                request = "SELECT * from house";
                mycon = new MySqlConnection(constr);
                mycon.Open();
                MySqlDataAdapter ms_data = new MySqlDataAdapter(request, constr);
                DataTable table = new DataTable();
                ms_data.Fill(table);
                dataGridView1.DataSource = table;
                mycon.Close();
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[1].HeaderText = "Фамилия";
                dataGridView1.Columns[2].HeaderText = "Имя";
                dataGridView1.Columns[3].HeaderText = "Отчество";
                dataGridView1.Columns[4].HeaderText = "Доходы";
                dataGridView1.Columns[5].HeaderText = "Аренда";
                dataGridView1.Columns[6].HeaderText = "Электроэнергия";
                dataGridView1.Columns[7].HeaderText = "Ком. Услуги";
                dataGridView1.Columns[8].HeaderText = "Отопление";
                dataGridView1.Columns[9].HeaderText = "Водоснабжение";
            }
            catch{
                MessageBox.Show("Подключение отсутствует");
            }
            

        }
        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(constr,"insert");
            form2.ShowDialog();
            table_load();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
             {
                 string request = "DROP DATABASE HOUSE";
                 mycon = new MySqlConnection(constr);
                 mycon.Open();
                 mycom = new MySqlCommand(request, mycon);
                 mycom.ExecuteNonQuery();
                 mycon.Close();
                 dataGridView1.DataSource = null;
                 settingsHide();
             }
             catch
             {
                 MessageBox.Show("База данных не найден");
             }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void button8_Click(object sender, EventArgs e)
        {
            string request = "SELECT * FROM house ORDER BY "+this.checked_column;
            mycon = new MySqlConnection(constr);
            mycon.Open();
            mycom = new MySqlCommand(request, mycon);
            mycom.ExecuteNonQuery();
            mycon.Close();

        }
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioBtn = (RadioButton)sender;
            this.checked_column = radioBtn.Text;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 1) { MessageBox.Show("В базе данных нет записей"); return; }
            int id = Convert.ToInt32(dataGridView1[0, dataGridView1.SelectedCells[0].RowIndex].Value);
            Form2 form2 = new Form2(constr,"update",id);
            form2.ShowDialog();
            table_load();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            table_load();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(constr, "search");
            form2.ShowDialog();
            int rowIndex = -1;
            if (form2.getId() != -1)
            {
                dataGridView1.ClearSelection();
                DataGridViewRow row = dataGridView1.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells["ID"].Value.ToString().Equals(form2.getId().ToString()))
                    .First();

                rowIndex = row.Index;
                dataGridView1.Rows[rowIndex].Selected = true;
            }
            else
            {
                MessageBox.Show("Запись не найдена");
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(constr, "filter");
            form2.ShowDialog();
            if(form2.filter_data != null)
            {
                DataTable table = new DataTable();
                form2.filter_data.Fill(table);
                dataGridView1.DataSource = table;
            }
            
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            progressBar1.Show();
            progressBar1.Minimum = 0; 
            progressBar1.Maximum = 50;
            progressBar1.Step = 1; 
            fillDB();
            progressBar1.Hide();
            progressBar1.Value = 0;
            table_load();

        }

        private void button3_Click(object sender, EventArgs e)
        {
                
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(constr);
            mycon.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = mycon;
            MySqlBackup mb = new MySqlBackup(cmd);
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                if(fbd.SelectedPath.Substring(fbd.SelectedPath.Length - 2) == "\\") { fbd.SelectedPath = fbd.SelectedPath.Substring(0, fbd.SelectedPath.Length - 2); }
                mb.ExportToFile(fbd.SelectedPath+"\\house.sql");
                MessageBox.Show("Файл успешно сохранен");
            }
            
            mycon.Close();
        }
    }
}
