using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static NpgsqlConnection connection = new NpgsqlConnection("Host = localhost; User Id = postgres; Database = attestation; Port = 5432; Password = a;");
        public static DataSet data = new DataSet();
        private void ToolStripMenuItem14_Click(object sender, System.EventArgs e)
        {
            new Attestation().Show();
        }
        private void ToolStripMenuItem13_Click(object sender, System.EventArgs e)
        {
            new Qualification().Show();
        }
        private void ToolStripMenuItem12_Click(object sender, System.EventArgs e)
        {
            new Education().Show();
        }
        private void ToolStripMenuItem11_Click(object sender, System.EventArgs e)
        {
            new ListOfEmployees().Show();
        }
        private void ToolStripMenuItem10_Click(object sender, System.EventArgs e)
        {
            new ListOfSchools().Show();
        }
        private void ToolStripMenuItem9_Click(object sender, System.EventArgs e)
        {
            new Employees().Show();
        }
        private void ToolStripMenuItem8_Click(object sender, System.EventArgs e)
        {
            new Prikazs().Show();
        }
        private void ToolStripMenuItem7_Click(object sender, System.EventArgs e)
        {
            new Doljnosts().Show();
        }
        private void ToolStripMenuItem6_Click(object sender, System.EventArgs e)
        {
            new Companies().Show();
        }
        private void ToolStripMenuItem5_Click(object sender, System.EventArgs e)
        {
            new Schools().Show();
        }
        private void ToolStripMenuItem4_Click(object sender, System.EventArgs e)
        {
            new AboutBox1().Show();
        }
    }
}
