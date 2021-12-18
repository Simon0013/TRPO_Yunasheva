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
    public partial class Attestation : Form
    {
        public Attestation()
        {
            InitializeComponent();
        }
        private void Attestation_Load(object sender, EventArgs e)
        {
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "select fio as Сотрудник, attestation_date as Дата, count_questions as Количество_вопросов, count_answers as Количество_ответов, grade as Оценка_соответствия, (select fio from employee where employee.id = comiss_member1) as Член_комиссии_1, (select fio from employee where employee.id = comiss_member2) as Член_комиссии_2, (select fio from employee where employee.id = comiss_member3) as Член_комиссии_3, (select fio from employee where employee.id = comiss_member4) as Член_комиссии_4, (select fio from employee where employee.id = comiss_member5) as Член_комиссии_5 from attestation,employee where employee_id = employee.id order by attestation.employee_id";
            Form1.connection.Open();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            if (Form1.data.Tables.Contains("Attestation")) Form1.data.Tables["Attestation"].Clear();
            adapter.Fill(Form1.data, "Attestation");
            Form1.connection.Close();
            dataGridView1.DataSource = Form1.data.Tables["Attestation"];
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            new NewAttestation().ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Сначала выберете элемент, который надо редактировать", "Error");
                return;
            }
            new NewAttestation(dataGridView1.CurrentCell.RowIndex).ShowDialog();
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
            command.CommandText = "delete from attestation where attestation_date = '" + Form1.data.Tables["Attestation"].Rows[dataGridView1.CurrentCell.RowIndex]["Дата"].ToString() + "'";
            Form1.connection.Open();
            command.ExecuteNonQuery();
            Form1.connection.Close();
            Form1.data.Tables["Attestation"].Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
            dataGridView1.CurrentCell = null;
        }
    }
}
