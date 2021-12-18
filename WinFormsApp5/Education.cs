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
    public partial class Education : Form
    {
        public Education()
        {
            InitializeComponent();
        }
        private void Education_Load(object sender, EventArgs e)
        {
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "select fio as Сотрудник, number as Номер_образования, school.name as Учебное_заведение, document as Документ_об_образовании, qualification as Квалификация from education,employee,school where education.id = employee.id and school.id = education.company_id order by education.id";
            Form1.connection.Open();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            if (Form1.data.Tables.Contains("Education")) Form1.data.Tables["Education"].Clear();
            adapter.Fill(Form1.data, "Education");
            Form1.connection.Close();
            dataGridView1.DataSource = Form1.data.Tables["Education"];
            dataGridView1.AutoResizeColumns();
            //dataGridView1.Columns["id"].Visible = false;
            dataGridView1.CurrentCell = null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            new NewEducation().ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Сначала выберете элемент, который надо редактировать", "Error");
                return;
            }
            new NewEducation(dataGridView1.CurrentCell.RowIndex).ShowDialog();
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
            command.CommandText = "delete from education where number = " + Form1.data.Tables["Education"].Rows[dataGridView1.CurrentCell.RowIndex]["Номер_образования"].ToString();
            Form1.connection.Open();
            command.ExecuteNonQuery();
            Form1.connection.Close();
            Form1.data.Tables["Education"].Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
            dataGridView1.CurrentCell = null;
        }
    }
}
