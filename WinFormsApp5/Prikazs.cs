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
    public partial class Prikazs : Form
    {
        public Prikazs()
        {
            InitializeComponent();
        }
        private void Prikazs_Load(object sender, EventArgs e)
        {
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "select * from prikaz order by id";
            Form1.connection.Open();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            if (Form1.data.Tables.Contains("Prikaz")) Form1.data.Tables["Prikaz"].Clear();
            adapter.Fill(Form1.data, "Prikaz");
            Form1.connection.Close();
            if (Form1.data.Tables["Prikaz"].Rows.Count > n)
                FiledsForm_Fill();
        }
        int n = 0;
        public void FiledsForm_Fill()
        {
            label1.Text = "Приказ №" + Form1.data.Tables["Prikaz"].Rows[n]["id"].ToString();
            dateTimePicker1.Text = Form1.data.Tables["Prikaz"].Rows[n]["create_date"].ToString();
            textBox1.Text = Form1.data.Tables["Prikaz"].Rows[n]["name"].ToString();
        }
        public void FiledsForm_Clear()
        {
            label1.Text = "Приказ №";
            dateTimePicker1.Value = DateTime.Now;
            textBox1.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (n == Form1.data.Tables["Prikaz"].Rows.Count)
            {
                NpgsqlCommand npgsql = Form1.connection.CreateCommand();
                npgsql.CommandText = "select max(id) + 1 from prikaz";
                Form1.connection.Open();
                NpgsqlDataReader reader = npgsql.ExecuteReader();
                reader.Read();
                string id = reader[0].ToString();
                Form1.connection.Close();
                string sql = "insert into prikaz values (" + id + ", '" + dateTimePicker1.Text + "', '" + textBox1.Text + "')";
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
                label1.Text = "Приказ №" + id;
                Form1.data.Tables["Prikaz"].Rows.Add(new object[] { id, dateTimePicker1.Text, textBox1.Text });
            }
            else
            {
                string sql = "update prikaz set create_date = '" + dateTimePicker1.Text + "', name = '" + textBox1.Text + "' where id = " + Form1.data.Tables["Prikaz"].Rows[n]["id"].ToString();
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
                Form1.data.Tables["Prikaz"].Rows[n]["create_date"] = dateTimePicker1.Text;
                Form1.data.Tables["Prikaz"].Rows[n]["name"] = textBox1.Text;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string message = "Удалить приказ с идентификатором " + Form1.data.Tables["Prikaz"].Rows[n]["id"].ToString() + "?";
            string caption = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No) { return; }
            string sql = "delete from prikaz where id = " + Form1.data.Tables["Prikaz"].Rows[n]["id"].ToString();
            NpgsqlCommand command = new NpgsqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            command.ExecuteNonQuery();
            Form1.connection.Close();
            try
            {
                Form1.data.Tables["Prikaz"].Rows.RemoveAt(n);
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Удаление не было выполнено из-за указания несуществующего экземпляра", "Ошибка");
                return;
            }
            if (Form1.data.Tables["Prikaz"].Rows.Count > n)
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
            n = Form1.data.Tables["Prikaz"].Rows.Count;
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
            if (Form1.data.Tables["Prikaz"].Rows.Count > (n + 1))
            {
                ++n;
                FiledsForm_Fill();
            }
        }
    }
}
