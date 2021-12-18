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
    public partial class NewAttestation : Form
    {
        public NewAttestation(): this(-1)
        {}
        public NewAttestation(int n)
        {
            InitializeComponent();
            this.n = n;
        }
        int n;
        private void NewAttestation_Load(object sender, EventArgs e)
        {
            if (n > -1)
            {
                comboBox1.Text = Form1.data.Tables["Attestation"].Rows[n]["Сотрудник"].ToString();
                textBox1.Text = Form1.data.Tables["Attestation"].Rows[n]["Количество_вопросов"].ToString();
                textBox2.Text = Form1.data.Tables["Attestation"].Rows[n]["Количество_ответов"].ToString();
                if (Form1.data.Tables["Attestation"].Rows[n]["Оценка_соответствия"].ToString() == "True")
                    comboBox2.SelectedIndex = 0;
                else
                    comboBox2.SelectedIndex = 1;
                comboBox3.Text = Form1.data.Tables["Attestation"].Rows[n]["Член_комиссии_1"].ToString();
                comboBox4.Text = Form1.data.Tables["Attestation"].Rows[n]["Член_комиссии_2"].ToString();
                comboBox5.Text = Form1.data.Tables["Attestation"].Rows[n]["Член_комиссии_3"].ToString();
                comboBox6.Text = Form1.data.Tables["Attestation"].Rows[n]["Член_комиссии_4"].ToString();
                comboBox7.Text = Form1.data.Tables["Attestation"].Rows[n]["Член_комиссии_5"].ToString();
                dateTimePicker1.Text = Form1.data.Tables["Attestation"].Rows[n]["Дата"].ToString();
                label1.Text = "Сведения об аттестации от " + Form1.data.Tables["Attestation"].Rows[n]["Дата"].ToString().Replace("0:00:00", "");
                dateTimePicker1.Visible = false;
                label3.Visible = false;
            }
            Form1.connection.Open();
            string sql = "select fio from employee order by id";
            NpgsqlCommand command = new NpgsqlCommand(sql, Form1.connection);
            NpgsqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                comboBox1.Items.Add(dataReader["fio"]);
                comboBox3.Items.Add(dataReader["fio"]);
                comboBox4.Items.Add(dataReader["fio"]);
                comboBox5.Items.Add(dataReader["fio"]);
                comboBox6.Items.Add(dataReader["fio"]);
                comboBox7.Items.Add(dataReader["fio"]);
            }
            Form1.connection.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlCommand npgsql = Form1.connection.CreateCommand();
            npgsql.CommandText = "select id, (select id from employee where fio = '" + comboBox3.Text + "') as m1, (select id from employee where fio = '" + comboBox4.Text + "') as m2, (select id from employee where fio = '" + comboBox5.Text + "') as m3, (select id from employee where fio = '" + comboBox6.Text + "') as m4, (select id from employee where fio = '" + comboBox7.Text + "') as m5 from employee where fio = '" + comboBox1.Text + "'";
            Form1.connection.Open();
            NpgsqlDataReader reader = npgsql.ExecuteReader();
            reader.Read();
            string employee_id = reader[0].ToString();
            string member1_id = reader[1].ToString();
            string member2_id = reader[2].ToString();
            string member3_id = reader[3].ToString();
            string member4_id = reader[4].ToString();
            string member5_id = reader[5].ToString();
            Form1.connection.Close();
            string grade = "false";
            if (comboBox2.SelectedIndex == 0)
                grade = "true";
            if (n == -1)
            {
                string sql = "insert into attestation values (" + employee_id + ", '" + dateTimePicker1.Value.ToShortDateString() + "', " + textBox1.Text + ", " + textBox2.Text + ", " + grade + ", " + member1_id + ", " + member2_id + ", " + member3_id + ", " + member4_id + ", " + member5_id + ")";
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
                label1.Text = "Сведения об аттестации от " + dateTimePicker1.Text.Replace("0:00:00", "");
                Form1.data.Tables["Attestation"].Rows.Add(new object[] { comboBox1.Text, dateTimePicker1.Text, textBox1.Text, textBox2.Text, comboBox2.SelectedIndex == 0, comboBox3.Text, comboBox4.Text, comboBox5.Text, comboBox6.Text, comboBox7.Text });
            }
            else
            {
                string sql = "update attestation set employee_id = " + employee_id + ", count_questions = " + textBox1.Text + ", count_answers = " + textBox2.Text + ", grade = " + grade + ", comiss_member1 = " + member1_id + ", comiss_member2 = " + member2_id + ", comiss_member3 = " + member3_id + ", comiss_member4 = " + member4_id + ", comiss_member5 = " + member5_id + " where attestation_date = '" + Form1.data.Tables["Attestation"].Rows[n]["Дата"].ToString() + "'";
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
                Form1.data.Tables["Attestation"].Rows[n]["Сотрудник"] = comboBox1.Text;
                Form1.data.Tables["Attestation"].Rows[n]["Количество_вопросов"] = textBox1.Text;
                Form1.data.Tables["Attestation"].Rows[n]["Количество_ответов"] = textBox2.Text;
                Form1.data.Tables["Attestation"].Rows[n]["Оценка_соответствия"] = comboBox2.SelectedIndex == 0;
                Form1.data.Tables["Attestation"].Rows[n]["Член_комиссии_1"] = comboBox3.Text;
                Form1.data.Tables["Attestation"].Rows[n]["Член_комиссии_2"] = comboBox4.Text;
                Form1.data.Tables["Attestation"].Rows[n]["Член_комиссии_3"] = comboBox5.Text;
                Form1.data.Tables["Attestation"].Rows[n]["Член_комиссии_4"] = comboBox6.Text;
                Form1.data.Tables["Attestation"].Rows[n]["Член_комиссии_5"] = comboBox7.Text;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
