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
    public partial class NewQualification : Form
    {
        public NewQualification(): this(-1)
        {}
        public NewQualification(int n)
        {
            InitializeComponent();
            this.n = n;
        }
        int n;
        private void NewQualification_Load(object sender, EventArgs e)
        {
            if (n > -1)
            {
                comboBox1.Text = Form1.data.Tables["Qualification"].Rows[n]["Сотрудник"].ToString();
                comboBox2.Text = Form1.data.Tables["Qualification"].Rows[n]["Учебное_заведение"].ToString();
                dateTimePicker1.Text = Form1.data.Tables["Qualification"].Rows[n]["Дата_начала"].ToString();
                dateTimePicker2.Text = Form1.data.Tables["Qualification"].Rows[n]["Дата_конца"].ToString();
                label1.Text = "Сведения о повышении квалификации №" + Form1.data.Tables["Qualification"].Rows[n]["Номер_повышения"].ToString();
            }
            Form1.connection.Open();
            string sql = "select fio from employee order by id";
            NpgsqlCommand command = new NpgsqlCommand(sql, Form1.connection);
            NpgsqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox1.Items.Add(dataReader["fio"]);
            Form1.connection.Close();
            Form1.connection.Open();
            sql = "select name from school order by id";
            command = new NpgsqlCommand(sql, Form1.connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
                comboBox2.Items.Add(dataReader["name"]);
            Form1.connection.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlCommand npgsql = Form1.connection.CreateCommand();
            npgsql.CommandText = "select id from employee where fio = '" + comboBox1.Text + "'";
            Form1.connection.Open();
            NpgsqlDataReader reader = npgsql.ExecuteReader();
            reader.Read();
            string employee_id = reader[0].ToString();
            Form1.connection.Close();
            npgsql = Form1.connection.CreateCommand();
            npgsql.CommandText = "select id from school where name = '" + comboBox2.Text + "'";
            Form1.connection.Open();
            reader = npgsql.ExecuteReader();
            reader.Read();
            string company_id = reader[0].ToString();
            Form1.connection.Close();
            if (n == -1)
            {
                npgsql = Form1.connection.CreateCommand();
                npgsql.CommandText = "select max(number) + 1 from high_qualification";
                Form1.connection.Open();
                reader = npgsql.ExecuteReader();
                reader.Read();
                string id = reader[0].ToString();
                Form1.connection.Close();
                string sql = "insert into high_qualification values (" + employee_id + ", " + id + ", " + company_id + ", '" + dateTimePicker1.Value.ToShortDateString() + "', '" + dateTimePicker2.Value.ToShortDateString() + "')";
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
                label1.Text = "Сведения об образовании №" + id;
                Form1.data.Tables["Qualification"].Rows.Add(new object[] { comboBox1.Text, id, comboBox2.Text, dateTimePicker1.Text, dateTimePicker2.Text });
            }
            else
            {
                string sql = "update high_qualification set id = " + employee_id + ", start_date = '" + dateTimePicker1.Value.ToShortDateString() + "', company_id = " + company_id + ", end_date = '" + dateTimePicker2.Value.ToShortDateString() + "' where number = " + Form1.data.Tables["Qualification"].Rows[n]["Номер_повышения"].ToString();
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
                Form1.data.Tables["Qualification"].Rows[n]["Сотрудник"] = comboBox1.Text;
                Form1.data.Tables["Qualification"].Rows[n]["Учебное_заведение"] = comboBox2.Text;
                Form1.data.Tables["Qualification"].Rows[n]["Дата_начала"] = dateTimePicker1.Text;
                Form1.data.Tables["Qualification"].Rows[n]["Дата_конца"] = dateTimePicker2.Text;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
