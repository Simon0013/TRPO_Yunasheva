using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp5
{
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
            maskedTextBox1.ValidatingType = typeof(double);
        }
        int n = 0;
        private void Employees_Load(object sender, EventArgs e)
        {
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "select employee.id as id, fio, birthday, company.name as company, block, doljnost.name as doljnost, prikaz_id, start_work_date, end_work_date, salary, start_experience, start_special from employee,company,doljnost where company.id = company_id and doljnost.id = doljnost_id order by employee.id";
            Form1.connection.Open();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            if (Form1.data.Tables.Contains("Employee")) Form1.data.Tables["Employee"].Clear();
            adapter.Fill(Form1.data, "Employee");
            Form1.connection.Close();
            if (Form1.data.Tables["Employee"].Rows.Count > n)
                FiledsForm_Fill();
            Form1.connection.Open();
            string sql = "select name from company order by id";
            command = new NpgsqlCommand(sql, Form1.connection);
            NpgsqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox1.Items.Add(dataReader["name"]);
            Form1.connection.Close();
            Form1.connection.Open();
            sql = "select name from doljnost order by id";
            command = new NpgsqlCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox2.Items.Add(dataReader["name"]);
            Form1.connection.Close();
            Form1.connection.Open();
            sql = "select id from prikaz order by id";
            command = new NpgsqlCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox3.Items.Add(dataReader["id"]);
            Form1.connection.Close();
        }
        public void FiledsForm_Fill()
        {
            label1.Text = "Сотрудник №" + Form1.data.Tables["Employee"].Rows[n]["id"].ToString();
            textBox1.Text = Form1.data.Tables["Employee"].Rows[n]["fio"].ToString();
            dateTimePicker1.Text = Form1.data.Tables["Employee"].Rows[n]["birthday"].ToString();
            comboBox1.Text = Form1.data.Tables["Employee"].Rows[n]["company"].ToString();
            textBox2.Text = Form1.data.Tables["Employee"].Rows[n]["block"].ToString();
            comboBox2.Text = Form1.data.Tables["Employee"].Rows[n]["doljnost"].ToString();
            comboBox3.Text = Form1.data.Tables["Employee"].Rows[n]["prikaz_id"].ToString();
            dateTimePicker2.Text = Form1.data.Tables["Employee"].Rows[n]["start_work_date"].ToString();
            dateTimePicker3.Text = Form1.data.Tables["Employee"].Rows[n]["end_work_date"].ToString();
            maskedTextBox1.Text = Form1.data.Tables["Employee"].Rows[n]["salary"].ToString();
            dateTimePicker4.Text = Form1.data.Tables["Employee"].Rows[n]["start_experience"].ToString();
            dateTimePicker5.Text = Form1.data.Tables["Employee"].Rows[n]["start_special"].ToString();
        }
        public void FiledsForm_Clear()
        {
            label1.Text = "Сотрудник №";
            textBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            comboBox1.SelectedIndex = -1;
            textBox2.Text = "";
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            maskedTextBox1.Text = "0.00";
            dateTimePicker4.Value = DateTime.Now;
            dateTimePicker5.Value = DateTime.Now;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlCommand npgsql = Form1.connection.CreateCommand();
            npgsql.CommandText = "select id from company where name = '" + comboBox1.Text + "'";
            Form1.connection.Open();
            NpgsqlDataReader reader = npgsql.ExecuteReader();
            reader.Read();
            string company_id = reader[0].ToString();
            Form1.connection.Close();
            npgsql = Form1.connection.CreateCommand();
            npgsql.CommandText = "select id from doljnost where name = '" + comboBox2.Text + "'";
            Form1.connection.Open();
            reader = npgsql.ExecuteReader();
            reader.Read();
            string doljnost_id = reader[0].ToString();
            Form1.connection.Close();
            if (n == Form1.data.Tables["Employee"].Rows.Count)
            {
                npgsql = Form1.connection.CreateCommand();
                npgsql.CommandText = "select max(id) + 1 from employee";
                Form1.connection.Open();
                reader = npgsql.ExecuteReader();
                reader.Read();
                string id = reader[0].ToString();
                Form1.connection.Close();
                string sql = "insert into employee values (" + id + ", '" + textBox1.Text + "', '" + dateTimePicker1.Value.ToShortDateString() + "', " + company_id + ", '" + textBox2.Text + "', " + doljnost_id + ", " + comboBox3.Text + ", '" + dateTimePicker2.Value.ToShortDateString() + "', '" + dateTimePicker3.Value.ToShortDateString() + "', " + maskedTextBox1.Text.Replace(",", ".") + ", '" + dateTimePicker4.Value.ToShortDateString() + "', '" + dateTimePicker5.Value.ToShortDateString() + "')";
                NpgsqlCommand command = new NpgsqlCommand(sql, Form1.connection);
                Form1.connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (NpgsqlException)
                {
                    MessageBox.Show("Добавление экземпляра не было успешно проведено из-за неуказания его данных или несоответствия их типов или попытки добавить экземпляр с уже используемым кодом!!!", "Ошибка");
                    Form1.connection.Close();
                    return;
                }
                Form1.connection.Close();
                label1.Text = "Сотрудник №" + id;
                Form1.data.Tables["Employee"].Rows.Add(new object[] { id, textBox1.Text, dateTimePicker1.Text, comboBox1.Text, textBox2.Text, comboBox2.Text, comboBox3.Text, dateTimePicker2.Text, dateTimePicker3.Text, maskedTextBox1.Text, dateTimePicker4.Text, dateTimePicker5.Text });
            }
            else
            {
                string sql = "update employee set fio = '" + textBox1.Text + "', birthday = '" + dateTimePicker1.Value.ToShortDateString() + "', company_id = " + company_id + ", block = '" + textBox2.Text + "', doljnost_id = " + doljnost_id + ", prikaz_id = " + comboBox3.Text + ", start_work_date = '" + dateTimePicker2.Value.ToShortDateString() + "', end_work_date = '" + dateTimePicker3.Value.ToShortDateString() + "', salary = " + maskedTextBox1.Text.Replace(",", ".") + ", start_experience = '" + dateTimePicker4.Value.ToShortDateString() + "', start_special = '" + dateTimePicker5.Value.ToShortDateString() + "' where id = " + Form1.data.Tables["Employee"].Rows[n]["id"].ToString();
                NpgsqlCommand command = Form1.connection.CreateCommand();
                command.CommandText = sql;
                Form1.connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (NpgsqlException)
                {
                    MessageBox.Show("Изменения не были успешно сохранены из-за несовпадения типов значений!!!", "Ошибка");
                    Form1.connection.Close();
                    return;
                }
                Form1.connection.Close();
                Form1.data.Tables["Employee"].Rows[n]["fio"] = textBox1.Text;
                Form1.data.Tables["Employee"].Rows[n]["birthday"] = dateTimePicker1.Text;
                Form1.data.Tables["Employee"].Rows[n]["company"] = comboBox1.Text;
                Form1.data.Tables["Employee"].Rows[n]["block"] = textBox2.Text;
                Form1.data.Tables["Employee"].Rows[n]["doljnost"] = comboBox2.Text;
                Form1.data.Tables["Employee"].Rows[n]["prikaz_id"] = comboBox3.Text;
                Form1.data.Tables["Employee"].Rows[n]["start_work_date"] = dateTimePicker2.Text;
                Form1.data.Tables["Employee"].Rows[n]["end_work_date"] = dateTimePicker3.Text;
                Form1.data.Tables["Employee"].Rows[n]["salary"] = maskedTextBox1.Text;
                Form1.data.Tables["Employee"].Rows[n]["start_experience"] = dateTimePicker4.Text;
                Form1.data.Tables["Employee"].Rows[n]["start_special"] = dateTimePicker5.Text;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string message = "Удалить сотрудника с идентификатором " + Form1.data.Tables["Employee"].Rows[n]["id"].ToString() + "?";
            string caption = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No) { return; }
            string sql = "delete from employee where id = " + Form1.data.Tables["Employee"].Rows[n]["id"].ToString();
            NpgsqlCommand command = new NpgsqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            command.ExecuteNonQuery();
            Form1.connection.Close();
            try
            {
                Form1.data.Tables["Employee"].Rows.RemoveAt(n);
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Удаление не было выполнено из-за указания несуществующего экземпляра", "Ошибка");
                return;
            }
            if (Form1.data.Tables["Employee"].Rows.Count > n)
            {
                FiledsForm_Fill();
            }
            else
            {
                --n;
                FiledsForm_Fill();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            n = Form1.data.Tables["Employee"].Rows.Count;
            FiledsForm_Clear();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (n > 0)
            {
                --n;
                FiledsForm_Fill();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (Form1.data.Tables["Employee"].Rows.Count > (n + 1))
            {
                ++n;
                FiledsForm_Fill();
            }
        }
    }
}
