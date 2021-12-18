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
    public partial class ListOfSchools : Form
    {
        public ListOfSchools()
        {
            InitializeComponent();
        }
        private void ListOfSchools_Load(object sender, EventArgs e)
        {
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "select id, name as Название, address as Адрес from school order by id";
            Form1.connection.Open();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            if (Form1.data.Tables.Contains("SchoolList")) Form1.data.Tables["SchoolList"].Clear();
            adapter.Fill(Form1.data, "SchoolList");
            Form1.connection.Close();
            dataGridView1.DataSource = Form1.data.Tables["SchoolList"];
            dataGridView1.AutoResizeColumns();
            dataGridView1.Columns["id"].Visible = false;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = 0; i < Form1.data.Tables["SchoolList"].Rows.Count; i++)
            {
                if (!Form1.data.Tables["SchoolList"].Rows[i]["Адрес"].ToString().Contains(textBox1.Text))
                {
                    if (radioButton1.Checked) dataGridView1.Rows[i].Visible = false;
                    else dataGridView1.Rows[i].Visible = true;
                }
                if (checkBox1.Checked)
                {
                    NpgsqlCommand command = Form1.connection.CreateCommand();
                    command.CommandText = "select count(id) from high_qualification where company_id = " + Form1.data.Tables["SchoolList"].Rows[i]["id"].ToString();
                    Form1.connection.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader[0].ToString() == "0")
                        dataGridView1.Rows[i].Visible = false;
                    else
                        dataGridView1.Rows[i].Visible = true;
                    Form1.connection.Close();
                }
                if (dataGridView1.Rows[i].Visible) ++count;
            }
            label4.Text = "Найдено: " + count + " строк";
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = 0; i < Form1.data.Tables["SchoolList"].Rows.Count; i++)
            {
                if (!Form1.data.Tables["SchoolList"].Rows[i]["Название"].ToString().Contains(textBox2.Text))
                {
                    if (radioButton2.Checked) dataGridView1.Rows[i].Visible = false;
                    else dataGridView1.Rows[i].Visible = true;
                }
                if (checkBox1.Checked)
                {
                    NpgsqlCommand command = Form1.connection.CreateCommand();
                    command.CommandText = "select count(id) from high_qualification where company_id = " + Form1.data.Tables["SchoolList"].Rows[i]["id"].ToString();
                    Form1.connection.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader[0].ToString() == "0")
                        dataGridView1.Rows[i].Visible = false;
                    else
                        dataGridView1.Rows[i].Visible = true;
                    Form1.connection.Close();
                }
                if (dataGridView1.Rows[i].Visible) ++count;
            }
            label4.Text = "Найдено: " + count + " строк";
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = 0; i < Form1.data.Tables["SchoolList"].Rows.Count; i++)
            {
                dataGridView1.Rows[i].Visible = true;
                if (checkBox1.Checked)
                {
                    NpgsqlCommand command = Form1.connection.CreateCommand();
                    command.CommandText = "select count(id) from high_qualification where company_id = " + Form1.data.Tables["SchoolList"].Rows[i]["id"].ToString();
                    Form1.connection.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader[0].ToString() == "0")
                        dataGridView1.Rows[i].Visible = false;
                    else
                        dataGridView1.Rows[i].Visible = true;
                    Form1.connection.Close();
                }
                if (!Form1.data.Tables["SchoolList"].Rows[i]["Адрес"].ToString().Contains(textBox1.Text))
                {
                    if (radioButton1.Checked) dataGridView1.Rows[i].Visible = false;
                }
                if (!Form1.data.Tables["SchoolList"].Rows[i]["Название"].ToString().Contains(textBox2.Text))
                {
                    if (radioButton2.Checked) dataGridView1.Rows[i].Visible = false;
                }
                if (dataGridView1.Rows[i].Visible) ++count;
            }
            label4.Text = "Найдено: " + count + " строк";
        }
    }
}
