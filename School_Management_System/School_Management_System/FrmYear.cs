using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School_Management_System
{
    public partial class FrmYear : UserControl
    {
        public static FrmYear instance;
        SqlConnection Con;
        SqlCommand Cmd;
        SqlDataAdapter DA;
        //SqlDataReader Dtr;
        DataSet DS;
        Boolean add, edit;

        public FrmYear()
        {
            InitializeComponent();
            instance = this;

            Con = new SqlConnection(global.ConnectionString);
            add = false;
            edit = false;
            ShowData();
            txtReadOnly(true);
            btnSave.Enabled = false;
            txtUserID.Text = global.User;
            btnAdd.Focus();
        }

        private void FrmYear_Load(object sender, EventArgs e)
        {
            resize1();
            //Button Control (Enable/Disable)
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            txtClear();
            txtReadOnly(true);
            dataGridView1.Enabled = true;
            dataGridView1.ClearSelection();
            ShowData();
            btnAdd.Focus();
        }

        public void resize1()
        {
            panel1.Width = (this.Width * 50) / 100;
            panel3.Width = (this.Width * 20) / 100;

            panel1.Height = (this.Height * 40) / 100;
            panel3.Height = (this.Height * 40) / 100;

            panel3.Location = new Point(panel1.Width, 3);

            panel2.Width = global.screenWidth - 240;
            panel2.Height = global.screenHeight - 380;
        }

        private void ShowData()
        {   //Diplay data in gridview 
            String squery = "select * from YearMaster order by YearId DESC";
            DA = new SqlDataAdapter(squery, Con);
            DS = new DataSet();
            DA.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            dataGridView1.ClearSelection();
        }

        private void ShowAllData(String query)
        {
            DA = new SqlDataAdapter(query, Con);
            DS = new DataSet();
            DA.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            dataGridView1.ClearSelection();
        }

        private void txtReadOnly(Boolean ans)
        {
            cbStatus.Enabled = !ans;
            TextBox[] tx = { txtYearDetail };
            for (int x = 0; x < 1; x++)
            {
                tx[x].ReadOnly = ans;
                tx[x].BackColor = Color.White;
            }
        }

        private void txtClear()
        {
            txtYearID.Text = "";
            txtYearDetail.Text = "";
            cbStatus.SelectedIndex = -1;
            lblYearDetail.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtReadOnly(false);
            btnAdd.Enabled = false;
            btnSave.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnCancel.Enabled = true;
            dataGridView1.Enabled = false;
            txtClear();
            add = true;
            edit = false;
            cbStatus.SelectedIndex = 0;
            txtYearDetail.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtYearID.Text != "")
            {
                txtReadOnly(false);
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                btnDelete.Enabled = false;
                add = false;
                edit = true;
                dataGridView1.Enabled = false;
                txtYearDetail.Focus();
            }
            else
            {
                MessageBox.Show("Search Data To Edit...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtYearDetail.Text != "" && txtUserID.Text != "" && cbStatus.Text != "")
            {
                // Duplicate check
                Con.Open();
                string checkQuery = "";
                if (add == true)
                {
                    checkQuery = "select count(*) from YearMaster where YearDetails='" + txtYearDetail.Text + "'";
                }
                else if (edit == true && txtYearID.Text != "")
                {
                    checkQuery = "select count(*) from YearMaster where YearDetails='" + txtYearDetail.Text + "' and YearId<>'" + txtYearID.Text + "'";
                }

                Cmd = new SqlCommand(checkQuery, Con);
                int count = Convert.ToInt32(Cmd.ExecuteScalar());
                Con.Close();

                if (count > 0)
                {
                    MessageBox.Show("This Year already exist, please don't enter duplicate year!", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtYearDetail.Text = "";
                    txtYearDetail.Focus();
                    return;
                }

                // Insert karna hai
                if (add == true)
                {
                    string status = "D";
                    if (cbStatus.Text == "Active")
                        status = "A";

                    Con.Open();
                    string inquery = "insert into YearMaster values('" + txtYearDetail.Text + "','" + status + "','" + txtUserID.Text + "')";
                    Cmd = new SqlCommand(inquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();

                    MessageBox.Show("Record Added...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowData();
                    btnCancel.PerformClick();
                }

                // Update karna hai
                if (edit == true)
                {
                    string status = "D";
                    if (cbStatus.Text == "Active")
                        status = "A";

                    Con.Open();
                    string upquery = "update YearMaster set YearDetails='" + txtYearDetail.Text + "', UserID='" + txtUserID.Text + "', Status='" + status + "' where YearId='" + txtYearID.Text + "'";
                    Cmd = new SqlCommand(upquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();

                    MessageBox.Show("Record Updated...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowData();
                    btnCancel.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("Fill the Data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtYearID.Text != "")
            {
                DialogResult ds = MessageBox.Show("Do You Want To Delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ds == DialogResult.Yes)
                {
                    Con.Open();
                    string dquery = "delete from YearMaster where YearID='" + txtYearID.Text + "'";
                    Cmd = new SqlCommand(dquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    lblYearDetail.Text = "";
                    ShowData();
                    txtClear();
                    btnCancel.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("Nothing selected...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtReadOnly(true);
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            dataGridView1.Enabled = true;
            txtClear();
            txtUserID.Text = global.User;
            lblYearDetail.Text = "";
            btnAdd.Focus();
        }

        private void txtYearDetail_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Backspace:8 dash:45 Underscore:45
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 45 && ch != 8)
            {
                lblYearDetail.Text = "Only NUMBER And Hyphen( - ) Allow";
                e.Handled = true;
            }
            else
            {
                lblYearDetail.Text = "";
                e.Handled = false;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (cbStatusSearch.Text == "Active")
            {
                String squery = "select * from YearMaster where (Status='A') AND (YearID like '%" + txtSearch.Text + "%' OR YearDetails like '" + txtSearch.Text + "%' OR Status like '" + txtSearch.Text + "%' OR UserID like '" + txtSearch.Text + "%' ) order by YearId DESC";
                ShowAllData(squery);
            }
            else if (cbStatusSearch.Text == "Deactive")
            {
                String squery = "select * from YearMaster where (Status='D') AND (YearID like '%" + txtSearch.Text + "%' OR YearDetails like '" + txtSearch.Text + "%' OR Status like '" + txtSearch.Text + "%' OR UserID like '" + txtSearch.Text + "%' ) order by YearId DESC";
                ShowAllData(squery);
            }
            else
            {
                String squery = "select * from YearMaster where YearID like '%" + txtSearch.Text + "%' OR YearDetails like '" + txtSearch.Text + "%' OR Status like '" + txtSearch.Text + "%' OR UserID like '" + txtSearch.Text + "%' order by YearId DESC";
                ShowAllData(squery);
            }
        }

        private void cbStatusSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch_TextChanged(sender, e);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {

                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                //cbStatus.Enabled = true;
                btnAdd.Focus();
                try
                {

                    txtYearID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txtYearDetail.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == "A")
                        cbStatus.Text = "Active";
                    else
                        cbStatus.Text = "Deactive";
                    txtUserID.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error filling form: " + ex.Message);
                }
                finally
                {
                    if (Con.State == ConnectionState.Open)
                    {
                        Con.Close();
                    }
                }
            }
        }
    }
}
