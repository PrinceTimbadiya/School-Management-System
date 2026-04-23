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
    public partial class FrmClassDefault : UserControl
    {
        public static FrmShift instance;
        SqlConnection Con;
        SqlCommand Cmd;
        SqlDataAdapter DA;
        DataSet DS;
        Boolean add, edit;

        public FrmClassDefault()
        {
            InitializeComponent();
            Con = new SqlConnection(global.ConnectionString);
            add = false;
            edit = false;
            txtReadOnly(true);
            ShowData();
            dataGridView1.ClearSelection();
            dtpDate.Value = DateTime.Today;
            LoadClassNames(); 
        }

        private void FrmClassDefault_Load(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            dataGridView1.Enabled = true;
            btnAdd.Focus();
        }

        private void txtReadOnly(Boolean ans)
        {
            cbClassName.Enabled = !ans;
            txtTotalStudents.ReadOnly = ans;
            dtpDate.Enabled = !ans;
        }

        private void txtClear()
        {
            txtDefaultId.Text = "";
            cbClassName.SelectedIndex = -1;
            txtTotalStudents.Text = "";
            dtpDate.Value = DateTime.Today;
        }

        private void LoadClassNames()
        {
            try
            {
                cbClassName.Items.Clear();
                Con.Open();
                string q = "SELECT DISTINCT ClassName FROM ClassDefaults ORDER BY ClassName";
                Cmd = new SqlCommand(q, Con);
                SqlDataReader dr = Cmd.ExecuteReader();
                while (dr.Read())
                {
                    cbClassName.Items.Add(dr["ClassName"].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading classes: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void ShowData()
        {
            try
            {
                string squery = "SELECT DefaultId, ClassName, TotalStudents, DefaultDate FROM ClassDefaults ";

                // Agar search box me kuch likha hai
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    squery += "WHERE (ClassName LIKE '%" + txtSearch.Text + "%' " +
                              "OR TotalStudents LIKE '%" + txtSearch.Text + "%' " +
                              "OR CONVERT(VARCHAR, DefaultDate, 105) LIKE '%" + txtSearch.Text + "%') ";
                    // 105 = dd-mm-yyyy format, agar aapko yyyy-mm-dd chahiye to 23 use karo
                }

                squery += "ORDER BY DefaultId DESC";

                DA = new SqlDataAdapter(squery, Con);
                DS = new DataSet();
                DA.Fill(DS);
                dataGridView1.DataSource = DS.Tables[0];
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            add = true;
            edit = false;
            txtReadOnly(false);
            dtpDate.Value = DateTime.Today;
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            dataGridView1.Enabled = false;
            txtClear();
            cbClassName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            add = false;
            edit = true;
            txtReadOnly(false);

            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            dataGridView1.Enabled = false;
            txtClear();
            cbClassName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbClassName.Text.Trim() != "" && txtTotalStudents.Text.Trim() != "")
            {
                Con.Open();
                if (add == true)
                {
                    string inquery = "INSERT INTO ClassDefaults(ClassName,TotalStudents,DefaultDate) VALUES('" + cbClassName.Text + "'," + txtTotalStudents.Text + ",'" + dtpDate.Value.ToString("yyyy-MM-dd") + "')";
                    Cmd = new SqlCommand(inquery, Con);
                    Cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Added...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (edit == true)
                {
                    string upquery = "UPDATE ClassDefaults SET ClassName='" + cbClassName.Text + "',TotalStudents=" + txtTotalStudents.Text + ",DefaultDate='" + dtpDate.Value.ToString("yyyy-MM-dd") + "' WHERE DefaultId=" + txtDefaultId.Text;
                    Cmd = new SqlCommand(upquery, Con);
                    Cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Updated...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Con.Close();
                ShowData();
                LoadClassNames();
                txtClear();
                btnCancel.PerformClick();
            }
            else
            {
                MessageBox.Show("Fill all fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtReadOnly(true);
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            dataGridView1.Enabled = true;
            dataGridView1.ClearSelection();
            txtClear();
            btnAdd.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtDefaultId.Text != "")
            {
                if (MessageBox.Show("Do You Want To Delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Con.Open();
                    Cmd = new SqlCommand("DELETE FROM ClassDefaults WHERE DefaultId=" + txtDefaultId.Text, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    ShowData();
                    LoadClassNames();
                    txtClear();
                    btnCancel.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("Nothing selected...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {
                txtReadOnly(true);
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;

                try
                {
                    txtDefaultId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    cbClassName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txtTotalStudents.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    dtpDate.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[3].Value);
                }
                catch { }
            }
        }

        private void cbClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbClassName.SelectedIndex >= 0)
            {
                try
                {
                    Con.Open();
                    string q = "SELECT TOP 1 * FROM ClassDefaults WHERE ClassName='" + cbClassName.Text + "' ORDER BY DefaultId DESC";
                    Cmd = new SqlCommand(q, Con);
                    SqlDataReader dr = Cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtDefaultId.Text = dr["DefaultId"].ToString();
                        txtTotalStudents.Text = dr["TotalStudents"].ToString();
                        dtpDate.Value = Convert.ToDateTime(dr["DefaultDate"]);
                    }
                    dr.Close();
                }
                catch { }
                finally
                {
                    Con.Close();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ShowData();
        }

    }
}

