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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PracticalPassword
{
    
    public partial class Form6 : Form
    { 
        public string UpdatedUsername { get; private set; }
        public string UpdatedPassword { get; private set; }

        string con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\solis\\OneDrive\\Documents\\PasswordF.accdb";
        private OleDbConnection connection;
        private DataGridViewRow _selectedRow;
        public Form6(DataGridViewRow selectedRow)
        {
            InitializeComponent();
            connection = new OleDbConnection(con);

            _selectedRow = selectedRow;

            // get the username and password from the selected row
            _selectedRow = selectedRow;
            textBox1.Text = selectedRow.Cells["username"].Value.ToString();
            textBox2.Text = selectedRow.Cells["password"].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newUsername = textBox1.Text;
            string newPassword = textBox2.Text;
            int id = (int)_selectedRow.Cells["id"].Value;

            string commandText = "UPDATE thePassword SET username=@username, [password]=@password WHERE id=@id";
            OleDbCommand command = new OleDbCommand(commandText, connection);
            command.Parameters.AddWithValue("@username", newUsername);
            command.Parameters.AddWithValue("@password", newPassword);
            command.Parameters.AddWithValue("@id", id);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            // set the updated values
            UpdatedUsername = newUsername;
            UpdatedPassword = newPassword;

            Close();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox2.PasswordChar = '\0';
            else
                textBox2.PasswordChar = '*';
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox2.PasswordChar = '\0';
            else
                textBox2.PasswordChar = '*';
        }

       
    }
}
