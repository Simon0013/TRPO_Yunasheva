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
    public partial class ListOfEmployees : Form
    {
        public ListOfEmployees()
        {
            InitializeComponent();
        }
        private void ListOfEmployees_Load(object sender, EventArgs e)
        {
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "select employee.id as id, fio as ФИО, birthday as Дата_рождения, company.name as Предприятие, block as Подразделение, doljnost.name as Должность, prikaz_id as Приказ, start_work_date as Начало_работы, end_work_date as Конец_контракта, salary as Ставка, start_experience as Начало_стажа, start_special as Начало_работы_по_специальности from employee,company,doljnost where company.id = company_id and doljnost.id = doljnost_id order by employee.id";
            Form1.connection.Open();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            if (Form1.data.Tables.Contains("EmployeeList")) Form1.data.Tables["EmployeeList"].Clear();
            adapter.Fill(Form1.data, "EmployeeList");
            Form1.connection.Close();
            dataGridView1.DataSource = Form1.data.Tables["EmployeeList"];
            dataGridView1.AutoResizeColumns();
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.CurrentCell = null;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            int count = 0;
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "select employee.id as id, fio as ФИО, birthday as Дата_рождения, company.name as Предприятие, block as Подразделение, doljnost.name as Должность, prikaz_id as Приказ, start_work_date as Начало_работы, end_work_date as Конец_контракта, salary as Ставка, start_experience as Начало_стажа, start_special as Начало_работы_по_специальности from employee,company,doljnost,attestation where company.id = company_id and doljnost.id = doljnost_id and attestation_date <= '" + dateTimePicker1.Value.ToShortDateString() + "' and employee_id = employee.id order by employee.id";
            Form1.connection.Open();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            if (Form1.data.Tables.Contains("EmployeeListAtt")) Form1.data.Tables["EmployeeListAtt"].Clear();
            adapter.Fill(Form1.data, "EmployeeListAtt");
            Form1.connection.Close();
            dataGridView1.DataSource = Form1.data.Tables["EmployeeListAtt"];
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
            for (int i = 0; i < Form1.data.Tables["EmployeeListAtt"].Rows.Count; i++)
            {
                if (!Form1.data.Tables["EmployeeListAtt"].Rows[i]["ФИО"].ToString().Contains(textBox1.Text))
                    dataGridView1.Rows[i].Visible = false;
                else
                {
                    ++count;
                    dataGridView1.Rows[i].Visible = true;
                }
            }
            label8.Text = "Найдено: " + count + " строк";
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            int count = 0;
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "select employee.id as id, fio as ФИО, birthday as Дата_рождения, company.name as Предприятие, block as Подразделение, doljnost.name as Должность, prikaz_id as Приказ, start_work_date as Начало_работы, end_work_date as Конец_контракта, salary as Ставка, start_experience as Начало_стажа, start_special as Начало_работы_по_специальности from employee,company,doljnost,attestation where company.id = company_id and doljnost.id = doljnost_id and attestation_date <= '" + dateTimePicker1.Value.ToShortDateString() + "' and employee_id = employee.id order by employee.id";
            Form1.connection.Open();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            if (Form1.data.Tables.Contains("EmployeeListAtt")) Form1.data.Tables["EmployeeListAtt"].Clear();
            adapter.Fill(Form1.data, "EmployeeListAtt");
            Form1.connection.Close();
            dataGridView1.DataSource = Form1.data.Tables["EmployeeListAtt"];
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
            for (int i = 0; i < Form1.data.Tables["EmployeeListAtt"].Rows.Count; i++)
            {
                if (!Form1.data.Tables["EmployeeListAtt"].Rows[i]["Предприятие"].ToString().Contains(textBox2.Text))
                    dataGridView1.Rows[i].Visible = false;
                else
                {
                    ++count;
                    dataGridView1.Rows[i].Visible = true;
                }
            }
            label8.Text = "Найдено: " + count + " строк";
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            int count = 0;
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "select employee.id as id, fio as ФИО, birthday as Дата_рождения, company.name as Предприятие, block as Подразделение, doljnost.name as Должность, prikaz_id as Приказ, start_work_date as Начало_работы, end_work_date as Конец_контракта, salary as Ставка, start_experience as Начало_стажа, start_special as Начало_работы_по_специальности from employee,company,doljnost,attestation where company.id = company_id and doljnost.id = doljnost_id and attestation_date <= '" + dateTimePicker1.Value.ToShortDateString() + "' and employee_id = employee.id order by employee.id";
            Form1.connection.Open();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            if (Form1.data.Tables.Contains("EmployeeListAtt")) Form1.data.Tables["EmployeeListAtt"].Clear();
            adapter.Fill(Form1.data, "EmployeeListAtt");
            Form1.connection.Close();
            dataGridView1.DataSource = Form1.data.Tables["EmployeeListAtt"];
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
            for (int i = 0; i < Form1.data.Tables["EmployeeListAtt"].Rows.Count; i++)
            {
                if (!Form1.data.Tables["EmployeeListAtt"].Rows[i]["Должность"].ToString().Contains(textBox3.Text))
                    dataGridView1.Rows[i].Visible = false;
                else
                {
                    ++count;
                    dataGridView1.Rows[i].Visible = true;
                }
            }
            label8.Text = "Найдено: " + count + " строк";
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            int count = 0;
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "select employee.id as id, fio as ФИО, birthday as Дата_рождения, company.name as Предприятие, block as Подразделение, doljnost.name as Должность, prikaz_id as Приказ, start_work_date as Начало_работы, end_work_date as Конец_контракта, salary as Ставка, start_experience as Начало_стажа, start_special as Начало_работы_по_специальности from employee,company,doljnost,attestation where company.id = company_id and doljnost.id = doljnost_id and attestation_date <= '" + dateTimePicker1.Value.ToShortDateString() + "' and employee_id = employee.id order by employee.id";
            Form1.connection.Open();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            if (Form1.data.Tables.Contains("EmployeeListAtt")) Form1.data.Tables["EmployeeListAtt"].Clear();
            adapter.Fill(Form1.data, "EmployeeListAtt");
            Form1.connection.Close();
            dataGridView1.DataSource = Form1.data.Tables["EmployeeListAtt"];
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
            for (int i = 0; i < Form1.data.Tables["EmployeeListAtt"].Rows.Count; i++)
            {
                if (!Form1.data.Tables["EmployeeListAtt"].Rows[i]["Подразделение"].ToString().Contains(textBox4.Text))
                    dataGridView1.Rows[i].Visible = false;
                else
                {
                    ++count;
                    dataGridView1.Rows[i].Visible = true;
                }
            }
            label8.Text = "Найдено: " + count + " строк";
        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Нужно выбрать оценку соответствия должности", "Error");
                return;
            }
            string grade = (comboBox1.SelectedIndex == 0) ? "true" : "false";
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "select employee.id as id, fio as ФИО, birthday as Дата_рождения, company.name as Предприятие, block as Подразделение, doljnost.name as Должность, prikaz_id as Приказ, start_work_date as Начало_работы, end_work_date as Конец_контракта, salary as Ставка, start_experience as Начало_стажа, start_special as Начало_работы_по_специальности from employee,company,doljnost,attestation where company.id = company_id and doljnost.id = doljnost_id and grade = " + grade + " and attestation_date <= '" + dateTimePicker1.Value.ToShortDateString() + "' and employee_id = employee.id order by employee.id";
            Form1.connection.Open();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            if (Form1.data.Tables.Contains("EmployeeListAtt")) Form1.data.Tables["EmployeeListAtt"].Clear();
            adapter.Fill(Form1.data, "EmployeeListAtt");
            Form1.connection.Close();
            dataGridView1.DataSource = Form1.data.Tables["EmployeeListAtt"];
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Visible = true;
            }
            label8.Text = "Найдено: " + Form1.data.Tables["EmployeeListAtt"].Rows.Count + " строк";
        }
    }
}
