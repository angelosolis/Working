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
    public partial class Form3 : Form
    {
        string con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\solis\\OneDrive\\Documents\\PasswordF.accdb";
        public Form3()
        {
            InitializeComponent();
            loadDatagrid();
        }
        public void loadDatagrid()
        {
            string con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\solis\\OneDrive\\Documents\\PasswordF.accdb";
            OleDbConnection connection = new OleDbConnection(con);

            connection.Open();

            string commandText = "SELECT username, password, id FROM thePassword";
            OleDbCommand command = new OleDbCommand(commandText, connection);

            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;

            dataGridView1.Columns["username"].HeaderText = "Username";
            dataGridView1.Columns["username"].DataPropertyName = "username";
            dataGridView1.Columns["password"].HeaderText = "Password";
            dataGridView1.Columns["password"].DataPropertyName = "password";
            dataGridView1.Columns["id"].HeaderText = "Id";
            dataGridView1.Columns["id"].DataPropertyName = "id";
            dataGridView1.Columns["id"].Visible = false;

            connection.Close();
        }

        private void loader(object sender, EventArgs e)
        {
            // load the data from the database into the DataGridView
            string commandText = "SELECT * FROM thePassword";
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(commandText, con);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            
            dataGridView1.Columns["username"].HeaderText = "Username";
            dataGridView1.Columns["password"].HeaderText = "Password";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            loadDatagrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string username = selectedRow.Cells["Username"].Value.ToString();
                string password = selectedRow.Cells["Password"].Value.ToString();

                Form6 form6 = new Form6(selectedRow);
                form6.ShowDialog();

                // update the selected row in the DataGridView
                selectedRow.Cells["Username"].Value = form6.UpdatedUsername;
                selectedRow.Cells["Password"].Value = form6.UpdatedPassword;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int id = (int)selectedRow.Cells["id"].Value;

                string con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\solis\\OneDrive\\Documents\\PasswordF.accdb";
                OleDbConnection connection = new OleDbConnection(con);

                string commandText = "DELETE FROM thePassword WHERE id=@id";
                OleDbCommand command = new OleDbCommand(commandText, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                dataGridView1.Rows.Remove(selectedRow);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.ShowDialog();
        }
    }
}
