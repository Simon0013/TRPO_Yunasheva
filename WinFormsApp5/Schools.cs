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
    public partial class Schools : Form
    {
        public Schools()
        {
            InitializeComponent();
        }
        private void Schools_Load(object sender, EventArgs e)
        {
            NpgsqlCommand command = Form1.connection.CreateCommand();
            command.CommandText = "select * from school order by id";
            Form1.connection.Open();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            if (Form1.data.Tables.Contains("School")) Form1.data.Tables["School"].Clear();
            adapter.Fill(Form1.data, "School");
            Form1.connection.Close();
            if (Form1.data.Tables["School"].Rows.Count > n)
                FiledsForm_Fill();
        }
        int n = 0;
        public void FiledsForm_Fill()
        {
            label1.Text = "Учебное заведение №" + Form1.data.Tables["School"].Rows[n]["id"].ToString();
            textBox1.Text = Form1.data.Tables["School"].Rows[n]["name"].ToString();
            textBox2.Text = Form1.data.Tables["School"].Rows[n]["address"].ToString();
        }
        public void FiledsForm_Clear()
        {
            label1.Text = "Учебное заведение №";
            textBox1.Text = "";
            textBox2.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (n == Form1.data.Tables["School"].Rows.Count)
            {
                NpgsqlCommand npgsql = Form1.connection.CreateCommand();
                npgsql.CommandText = "select max(id) + 1 from school";
                Form1.connection.Open();
                NpgsqlDataReader reader = npgsql.ExecuteReader();
                reader.Read();
                string id = reader[0].ToString();
                Form1.connection.Close();
                string sql = "insert into school values (" + id + ", '" + textBox1.Text + "', '" + textBox2.Text + "')";
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
                label1.Text = "Учебное заведение №" + id;
                Form1.data.Tables["School"].Rows.Add(new object[] { id, textBox1.Text, textBox2.Text });
            }
            else
            {
                string sql = "update school set name = '" + textBox1.Text + "', address = '" + textBox2.Text + "' where id = " + Form1.data.Tables["School"].Rows[n]["id"].ToString();
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
                Form1.data.Tables["School"].Rows[n]["name"] = textBox1.Text;
                Form1.data.Tables["School"].Rows[n]["address"] = textBox2.Text;
                FiledsForm_Fill();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string message = "Удалить учебное заведение с идентификатором " + Form1.data.Tables["School"].Rows[n]["id"].ToString() + "?";
            string caption = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No) { return; }
            string sql = "delete from school where id = " + Form1.data.Tables["School"].Rows[n]["id"].ToString();
            NpgsqlCommand command = new NpgsqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            command.ExecuteNonQuery();
            Form1.connection.Close();
            try
            {
                Form1.data.Tables["School"].Rows.RemoveAt(n);
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Удаление не было выполнено из-за указания несуществующего экземпляра", "Ошибка");
                return;
            }
            if (Form1.data.Tables["School"].Rows.Count > n)
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
            n = Form1.data.Tables["School"].Rows.Count;
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
            if (Form1.data.Tables["School"].Rows.Count > (n+1))
            {
                ++n;
                FiledsForm_Fill();
            }
        }
    }
}
