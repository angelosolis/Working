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
    public partial class Form4 : Form
    {
        private OleDbConnection con;
        public Form4()
        {
            InitializeComponent();
            con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\solis\\OneDrive\\Documents\\LibSys.mdb;Persist Security Info=False;");
            loadDatagrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtno.Text = grid1.Rows[e.RowIndex].Cells["Ascension Number"].Value.ToString();
            txttitle.Text = grid1.Rows[e.RowIndex].Cells["Title"].Value.ToString();
            txtauthor.Text = grid1.Rows[e.RowIndex].Cells["Author"].Value.ToString();
        }
        private void loadDatagrid()
        {
            con.Open();
            OleDbCommand com = new OleDbCommand("SELECT * FROM book ORDER BY [Ascension Number] ASC", con);
            com.ExecuteNonQuery();
            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            grid1.DataSource = tab;

            con.Close();

        }

        private void btnadd_click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand com = new OleDbCommand("Insert into book values ('" + txtno.Text + "', '" + txttitle.Text + "', '" + txtauthor.Text + "')", con);
            com.ExecuteNonQuery();

            MessageBox.Show("Successfully Saved!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            loadDatagrid();
            txtno.Clear();
            txttitle.Clear();
            txtauthor.Clear();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtno.Text))
            {
                string ascensionNumber = txtno.Text;
                string title = txttitle.Text;
                string author = txtauthor.Text;

                string con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\solis\\OneDrive\\Documents\\LibSys.mdb;Persist Security Info=False;";
                string commandText = "UPDATE book SET Title=@title, Author=@author WHERE [Ascension Number]=@ascension";

                using (OleDbConnection connection = new OleDbConnection(con))
                {
                    using (OleDbCommand command = new OleDbCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("@title", title);
                        command.Parameters.AddWithValue("@author", author);
                        command.Parameters.AddWithValue("@ascension", ascensionNumber);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }

                // Refresh the DataGridView to reflect the changes
                loadDatagrid();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string searchValue = textBox4.Text.Trim();
            if (!string.IsNullOrEmpty(searchValue))
            {
                con.Open();
                OleDbCommand com = new OleDbCommand("SELECT * FROM book WHERE [Ascension Number] LIKE @searchValue OR title LIKE @searchValue OR author LIKE @searchValue", con);
                com.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%");
                OleDbDataAdapter adap = new OleDbDataAdapter(com);
                DataTable tab = new DataTable();

                adap.Fill(tab);
                grid1.DataSource = tab;

                con.Close();
            }
            else
            {
                loadDatagrid();
            }
        }

        private void btnDelete_click(object sender, EventArgs e)
        {
            con.Open();
            string num = txtno.Text;
            DialogResult dr = MessageBox.Show("Are you sure you want to delete this?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                int ascensionNumber;
                if (int.TryParse(num, out ascensionNumber))
                {
                    OleDbCommand com = new OleDbCommand("DELETE FROM book WHERE [Ascension Number] = " + ascensionNumber, con);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Successfully DELETED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Invalid input. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("CANCELLED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            con.Close();
            loadDatagrid();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.ShowDialog();
        }
    }
}
