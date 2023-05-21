using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticalPassword
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\solis\\OneDrive\\Documents\\PasswordF.accdb";
            OleDbConnection connection = new OleDbConnection(con);

            string username = textBox1.Text;
            string password = textBox2.Text;

            // create a command to insert the data into the database
            string commandText = "INSERT INTO thePassword (username, [password]) VALUES (?, ?)";
            OleDbCommand command = new OleDbCommand(commandText, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            // open the connection, execute the command, and close the connection
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            // clear the textboxes and show a message box to confirm the registration
            textBox1.Clear();
            textBox2.Clear();
            MessageBox.Show("Registration successful!");
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox2.PasswordChar = '\0';
            else
                textBox2.PasswordChar = '*';
        }

     }
}

