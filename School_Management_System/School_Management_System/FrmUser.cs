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
using System.IO;

namespace School_Management_System
{
    public partial class FrmUser : UserControl
    {
        public static FrmUser instance;
        SqlConnection Con;
        SqlCommand Cmd;
        SqlDataAdapter DA;
        DataSet DS;
        SqlDataReader Dtr;
        //DataTable dt;
        Boolean add, edit;
        string EditUserName;

        public FrmUser()
        {
            InitializeComponent();
            instance = this;
            Con = new SqlConnection(global.ConnectionString);
            //string connectionStr = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Project\School_Management_System\School_Management_System\school.mdf;Integrated Security=True";
            //Con = new SqlConnection(connectionStr);
            add = false;
            edit = false;
            FillStaffCombo();
            ShowData();
        }

        private void FrmUser_Load(object sender, EventArgs e)
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
            //ComboBox Control
            cbSearch.Enabled = true;
            txtSearch.Enabled = true;
            //Gridview control
            dataGridView1.Enabled = true;
            dataGridView1.ClearSelection();
            ShowData();
            btnAdd.Focus();
            //For Gridview_header
            SetGridColumnHeaders();
            FillStaffCombo();
        }

        public void resize1()
        {
            panel2.Width = global.screenWidth - 240;
            panel2.Height = global.screenHeight - 333;
        }

        private void FillStaffCombo()
        {
            cbStaffId.Items.Clear();
            Con.Open();
            string staffQuery = "SELECT StaffId, Name FROM StaffMaster";
            Cmd = new SqlCommand(staffQuery, Con);
            Dtr = Cmd.ExecuteReader();
            while (Dtr.Read())
            {
                string itemText = Dtr["StaffId"].ToString() + " - " + Dtr["Name"].ToString();
                cbStaffId.Items.Add(itemText);
            }
            Con.Close();

            //string query = "SELECT StaffId, CAST(StaffId AS VARCHAR) + ' - ' + Name AS DisplayName FROM StaffMaster WHERE status = 'A' ORDER BY StaffId";
            //Cmd = new SqlCommand(query, Con);
            //DA = new SqlDataAdapter(Cmd);
            //dt = new DataTable();
            //DA.Fill(dt);

            //cbStaffId.DataSource = dt;
            //cbStaffId.DisplayMember = "DisplayName"; // ComboBox value
            //cbStaffId.ValueMember = "StaffId";       // Actual value je database me jashe
        }


        private void ShowData()
        {
            Con.Open();
            //Display data in gridview
            String squery = "select userId, userName, userPass, userType, creationDt, validTillDt, createdBy, staffId, status FROM LoginMaster order by userId DESC";
            DA = new SqlDataAdapter(squery, Con);
            DS = new DataSet();
            DA.Fill(DS);
            Con.Close();

            dataGridView1.DataSource = DS.Tables[0];

            //hide userPass column in gridview
            if (dataGridView1.Columns.Contains("userPass"))
            {
                dataGridView1.Columns["userPass"].Visible = false;
            }

            dataGridView1.ClearSelection();
        }

        private void ShowAllData(String query)
        {
            DA = new SqlDataAdapter(query, Con);
            DS = new DataSet();
            DA.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];

            //Always hide userPass column
            if (dataGridView1.Columns.Contains("userPass"))
            {
                dataGridView1.Columns["userPass"].Visible = false;
            }

            dataGridView1.ClearSelection();
        }

        private void txtReadOnly(Boolean ans)
        {
            cbUserType.Enabled = !ans;
            cbStatus.Enabled = !ans;
            dtpCreationDt.Enabled = !ans;
            dtpValidtillDt.Enabled = !ans;
            cbStaffId.Enabled = !ans;
            TextBox[] tx = { txtUserName, txtUserPass, txtVerifyPass, txtCreatedBy };
            for (int x = 0; x < tx.Length; x++)
            {
                tx[x].ReadOnly = ans;
                tx[x].BackColor = Color.White;
            }
        }

        private void txtClear()
        {
            txtUserID.Text = "";
            txtUserName.Text = "";
            txtUserPass.Text = "";
            txtVerifyPass.Text = "";
            cbUserType.SelectedIndex = -1;
            dtpCreationDt.Value = DateTime.Now;
            dtpValidtillDt.Value = DateTime.Now;
            txtCreatedBy.Text = "";
            cbStaffId.SelectedIndex = -1;
            cbStatus.SelectedIndex = -1;
    
            txtUserName.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Button Controls - [Add]
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            btnCancel.Enabled = true;
            txtClear();
            txtReadOnly(false);
            dataGridView1.Enabled = false;
            cbStatus.SelectedIndex = 0;
            add = true;
            edit = false;
            txtUserName.Focus();

            dtpCreationDt.Value = DateTime.Now;
            // Set valid till = creation + 1 year
            dtpValidtillDt.Value = dtpCreationDt.Value.AddYears(1); 
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditUserName = txtUserName.Text;
            if (txtUserID.Text != "" && txtUserName.Text != "")
            {
                //Button Controls - [Edit]
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnSave.Enabled = true;
                btnDelete.Enabled = false;
                btnCancel.Enabled = true;
                txtReadOnly(false);
                dataGridView1.Enabled = false;

                add = false;
                edit = true;
                txtUserID.ReadOnly = true;
                dtpCreationDt.Enabled = false;
                txtUserName.Focus();
            }
            else
            {
                MessageBox.Show("Search Data To Edit...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }            
     
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string status = "D";
            if (txtUserID.Text != "")
            {
                DialogResult ds = MessageBox.Show("Do You Want To Delete?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ds == DialogResult.Yes)
                {
                    Con.Open();
                    string dquery = "UPDATE LoginMaster SET status = '" + status + "' WHERE userId = " + txtUserID.Text;
                    Cmd = new SqlCommand(dquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Record Deactivated...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowData();    

                    //Button Controls - [Delete]
                    btnCancel.PerformClick();   
                }
            }
            else
            {
                MessageBox.Show("Nothing Selected...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SetGridColumnHeaders()
        {
            dataGridView1.Columns[0].HeaderText = "User ID";
            dataGridView1.Columns[1].HeaderText = "User Name";
            dataGridView1.Columns[2].HeaderText = "Password";
            dataGridView1.Columns[3].HeaderText = "User Type";
            dataGridView1.Columns[4].HeaderText = "Created On";
            dataGridView1.Columns[5].HeaderText = "Valid Till";
            dataGridView1.Columns[6].HeaderText = "Created By";
            dataGridView1.Columns[7].HeaderText = "Staff ID";
            dataGridView1.Columns[8].HeaderText = "Status";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {
                DataGridViewRow currentRow = dataGridView1.CurrentRow;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                //cbStatus.Enabled = true;
                btnAdd.Focus();
                try
                {
                    txtUserID.Text = currentRow.Cells["userId"].Value.ToString();
                    txtUserName.Text = currentRow.Cells["userName"].Value.ToString();
                    txtUserPass.Text = currentRow.Cells["userPass"].Value.ToString();
                    //txtVerifyPass.Text = txtUserPass.Text;
                    cbUserType.Text = currentRow.Cells["userType"].Value.ToString();
                    dtpCreationDt.Value = Convert.ToDateTime(currentRow.Cells["creationDt"].Value);
                    dtpValidtillDt.Value = Convert.ToDateTime(currentRow.Cells["validTillDt"].Value);
                    txtCreatedBy.Text = currentRow.Cells["createdBy"].Value.ToString();
                    //StaffId
                    string staffId = dataGridView1.CurrentRow.Cells["StaffId"].Value.ToString();
                    Con.Open();
                    string q3 = "SELECT Name FROM StaffMaster WHERE StaffId = '" + staffId + "'";
                    Cmd = new SqlCommand(q3, Con);
                    Dtr = Cmd.ExecuteReader();
                    if (Dtr.Read())
                        cbStaffId.Text = staffId + " - " + Dtr["Name"].ToString();
                    Con.Close();
                    cbStatus.Text = (currentRow.Cells["status"].Value.ToString() == "A") ? "Active" : "Deactive";
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string squery;

            string baseQuery = "SELECT * FROM LoginMaster WHERE ";
            string filterFields = "(userId LIKE '%" + txtSearch.Text + "%' OR userName LIKE '%" + txtSearch.Text +
                                  "%' OR userType LIKE '%" + txtSearch.Text + "%' OR createdBy LIKE '%" + txtSearch.Text + "%' OR staffId LIKE '%" + txtSearch.Text + "%') order by userId DESC";
            if (cbSearch.Text == "Active")
            {
                squery = baseQuery + "status = 'A' AND " + filterFields;
            }
            else if (cbSearch.Text == "Deactive")
            {
                squery = baseQuery + "status = 'D' AND " + filterFields;
            }
            else
            {
                squery = baseQuery + filterFields;
            }

            ShowAllData(squery);
        }

        private void cbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string squery;
            if (cbSearch.Text == "Active")
            {
                squery = "select userId, userName, userPass, userType, creationDt, validTillDt, createdBy, staffId, status FROM LoginMaster WHERE status = 'A' order by userId DESC";
            }
            else if (cbSearch.Text == "Deactive")
            {
                squery = "select userId, userName, userPass, userType, creationDt, validTillDt, createdBy, staffId, status FROM LoginMaster WHERE status = 'D' order by userId DESC";
            }
            else
            {
                squery = "select userId, userName, userPass, userType, creationDt, validTillDt, createdBy, staffId, status FROM LoginMaster order by userId DESC";
            }

            ShowAllData(squery);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Please fill user name...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Focus();
            }
            else if (txtUserPass.Text == "" || txtUserPass.Text != txtVerifyPass.Text)
            {
                MessageBox.Show("Please fill valid password...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (txtUserPass.Text == "")
                {
                    txtUserPass.Focus();
                }
                else
                {
                    txtVerifyPass.Focus();
                }

            }
            else if (cbUserType.Text == "" || cbStatus.Text == "")
            {
                MessageBox.Show("Please fill data from the drop-down list...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (cbUserType.Text == "") 
                {
                    cbUserType.Focus(); 
                }
                else
                {
                    cbStatus.Focus();
                }
            }
            else if (txtCreatedBy.Text == "" || cbStaffId.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill ID number properly...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (txtCreatedBy.Text == "")
                {
                    txtCreatedBy.Focus();
                }
                else
                {
                    cbStaffId.Focus();
                }
            }
            else if (dtpValidtillDt.Value.Date < dtpCreationDt.Value.Date)       //fill valid date
            {
                MessageBox.Show("Valid Till Date should not be earlier than Creation Date.", "ValidTill Date is Wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpValidtillDt.Focus();
            }
            else if (add == true)
            {
                string status = (cbStatus.Text == "Active") ? "A" : "D";
                DateTime creationDate = DateTime.Now;
                DateTime validTillDate = dtpValidtillDt.Value;
                int createdBy = Int32.Parse(txtCreatedBy.Text);
                string staffId = cbStaffId.Text.Split('-')[0].Trim();

                try
                {
                    Con.Open();
                    string inquery = "INSERT INTO LoginMaster (userName, userPass, userType, creationDt, validTillDt, createdBy, staffId, status) VALUES (" + "'" + txtUserName.Text + "', " + "'" + txtUserPass.Text + "', " + "'" + cbUserType.Text + "', " + "'" + creationDate.ToString("yyyy-MM-dd HH:mm:ss") + "', " + "'" + validTillDate.ToString("yyyy-MM-dd") + "', " + createdBy + ", " + staffId + ", " + "'" + status + "')";
                    Cmd = new SqlCommand(inquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    ShowData();
                    MessageBox.Show("Record Added...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    if (Con.State == ConnectionState.Open)
                    {
                        Con.Close();
                    }
                }
            }
            else if (edit == true)
            {
                string status = (cbStatus.Text == "Active") ? "A" : "D";
                int createdBy = Int32.Parse(txtCreatedBy.Text);
                string staffId = cbStaffId.Text.Split('-')[0].Trim();

                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a record to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    Con.Open();
                    string upquery = "UPDATE LoginMaster SET userName = '" + txtUserName.Text + "', userType = '" + cbUserType.Text + "', userPass = '" + txtUserPass.Text + "',validTillDt = '" + dtpValidtillDt.Value.ToString("yyyy-MM-dd") + "', createdBy = " + createdBy + ", staffId = " + staffId + ", status = '" + status + "' WHERE UserId = " + txtUserID.Text;
                    Cmd = new SqlCommand(upquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    ShowData();
                    MessageBox.Show("Record Updated...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            txtClear();
            txtReadOnly(true);
            dataGridView1.Enabled = true;

            add = false;
            edit = false;

            btnAdd.Focus();
        }

        private void txtUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Backspace:8
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
                e.Handled = true;
        }

        private void txtStaffID_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Backspace:8
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
                e.Handled = true;
        }

        private void txtCreatedBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Backspace:8
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
                e.Handled = true;
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            //Only check when Add or Edit mode else return
            if (!add && !edit)
                return;

            Con.Open();
            string dupquery = "SELECT COUNT(*) FROM LoginMaster WHERE userName = '" + txtUserName.Text + "'";
            Cmd = new SqlCommand(dupquery, Con);
            int count = (int)Cmd.ExecuteScalar();
            Con.Close();
            if (count != 0 && txtUserName.Text != EditUserName)
            {
                DialogResult ds1 = MessageBox.Show("User Name already exists. Please choose a different name.", "Duplicate User", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (ds1 == DialogResult.Cancel)
                {
                    txtClear();
                    btnCancel.PerformClick();
                }
                else
                {
                    txtUserName.Focus();
                }
            }
            
        }

    }
}
