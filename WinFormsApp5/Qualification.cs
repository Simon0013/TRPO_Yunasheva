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
    public partial class Qualification : Form
    {
        public Qualification()
        {
            InitializeComponent();
        }
        private void Qualification_Load(object sender, EventArgs e)
        {
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "select fio as Сотрудник, number as Номер_повышения, school.name as Учебное_заведение, start_date as Дата_начала, end_date as Дата_конца from high_qualification,employee,school where high_qualification.id = employee.id and school.id = high_qualification.company_id order by high_qualification.id";
            Form1.connection.Open();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            if (Form1.data.Tables.Contains("Qualification")) Form1.data.Tables["Qualification"].Clear();
            adapter.Fill(Form1.data, "Qualification");
            Form1.connection.Close();
            dataGridView1.DataSource = Form1.data.Tables["Qualification"];
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            new NewQualification().ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Сначала выберете элемент, который надо редактировать", "Error");
                return;
            }
            new NewQualification(dataGridView1.CurrentCell.RowIndex).ShowDialog();
            dataGridView1.CurrentCell = null;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Сначала выберете элемент, который надо удалить", "Error");
                return;
            }
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "delete from high_qualification where number = " + Form1.data.Tables["Qualification"].Rows[dataGridView1.CurrentCell.RowIndex]["Номер_повышения"].ToString();
            Form1.connection.Open();
            command.ExecuteNonQuery();
            Form1.connection.Close();
            Form1.data.Tables["Qualification"].Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
            dataGridView1.CurrentCell = null;
        }
    }
}
