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
    public partial class FrmStructure : UserControl
    {
        public static FrmStructure instance;

        SqlConnection Con;
        SqlCommand Cmd;
        SqlDataAdapter DA;
        DataSet DS;
        Boolean add, edit;

        public FrmStructure()
        {
            InitializeComponent();
            instance = this;
            Con = new SqlConnection(global.ConnectionString);
            add = false;
            edit = false;
        }

        private void FrmStructure_Load(object sender, EventArgs e)
        {
            resize1();
            LoadFeeTypes();
            LoadDept();
            LoadClass();
            LoadYears();
            LoadSearchDropdowns();
            ShowData();
            txtUserID.Text = global.User; // auto user
            txtFeeStructID.ReadOnly = true; // always readonly
            btnAdd.Focus();
            setEnableDisable(true, false, false, false, false);
        }

        public void resize1()
        {  
            panel2.Width = global.screenWidth - 240;
            panel2.Height = global.screenHeight - 385;
        }

        private void setEnableDisable(bool addBtn, bool editBtn, bool saveBtn, bool deleteBtn, bool cancelBtn)
        {
            btnAdd.Enabled = addBtn;
            btnEdit.Enabled = editBtn;
            btnSave.Enabled = saveBtn;
            btnDelete.Enabled = deleteBtn;
            btnCancel.Enabled = cancelBtn;
        }

        private void txtClear()
        {
            txtFeeStructID.Clear();
            txtFeeYear.Clear();
            cbDeptCode.SelectedIndex = -1;
            cbClassID.SelectedIndex = -1;
            cbFeeDetail.SelectedIndex = -1;
            cbFeeType.SelectedIndex = -1;
            cbFeeAmount.SelectedIndex = -1;
            cbGovtApprovedFee.SelectedIndex = -1;
            cbStatus.SelectedIndex = -1;
            txtUserID.Text = global.User;
        }

        private void txtReadOnly(bool flag)
        {
            txtFeeYear.ReadOnly = flag;
            cbDeptCode.Enabled = !flag;
            cbClassID.Enabled = !flag;
            cbFeeDetail.Enabled = !flag;
            cbFeeType.Enabled = !flag;
            cbFeeAmount.Enabled = !flag;
            cbGovtApprovedFee.Enabled = !flag;
            cbStatus.Enabled = !flag;
        }

        private void ShowData()
        {
            string query = "SELECT * FROM FeesStructure ORDER BY FeestructuredId";
            DA = new SqlDataAdapter(query, Con);
            DS = new DataSet();
            DA.Fill(DS, "FeesStructure");
            dataGridView1.DataSource = DS.Tables["FeesStructure"];
        }

        // ---------------------- LOAD DROPDOWNS ----------------------
        // Load distinct FeeTypes in cbFeeType
        private void LoadFeeTypes()
        {
            cbFeeType.Items.Clear();
            string query = "SELECT DISTINCT FeesType FROM FeesStructure ORDER BY FeesType";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
                cbFeeType.Items.Add(dr["FeesType"].ToString());
        }

        private void LoadDept()
        {
            string query = "SELECT DeptId, DeptName FROM DepartmentMaster ORDER BY DeptName";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbDeptCode.DataSource = dt;
            cbDeptCode.DisplayMember = "DeptName";
            cbDeptCode.ValueMember = "DeptId";
            cbDeptCode.SelectedIndex = -1;
        }

        private void LoadClass()
        {
            string query = "SELECT ClassId, ClassName FROM ClassMaster ORDER BY ClassName";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbClassID.DataSource = dt;
            cbClassID.DisplayMember = "ClassName";
            cbClassID.ValueMember = "ClassId";
            cbClassID.SelectedIndex = -1;
        }

        private void LoadYears()
        {
            string query = "SELECT DISTINCT FeesYear FROM FeesStructure ORDER BY FeesYear";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cbFromYear.Items.Clear();
            cbToYear.Items.Clear();
            cbDeleteYear.Items.Clear();
            cbYearSearch.Items.Clear();
            cbYearSearch.Items.Add("All");

            foreach (DataRow dr in dt.Rows)
            {
                string yr = dr["FeesYear"].ToString();
                cbFromYear.Items.Add(yr);
                cbToYear.Items.Add(yr);
                cbDeleteYear.Items.Add(yr);
                cbYearSearch.Items.Add(yr);
            }
        }

        private void LoadSearchDropdowns()
        {
            // Class Search
            string query = "SELECT ClassId, ClassName FROM ClassMaster ORDER BY ClassName";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbClassSearch.DataSource = dt;
            cbClassSearch.DisplayMember = "ClassName";
            cbClassSearch.ValueMember = "ClassId";
            cbClassSearch.SelectedIndex = -1;

            // Status Search
            cbStatusSearch.Items.Clear();
            cbStatusSearch.Items.Add("All");
            cbStatusSearch.Items.Add("A");
            cbStatusSearch.Items.Add("D");
            cbStatusSearch.SelectedIndex = 0;
        }



        // ---------------------- BUTTON LOGIC ----------------------
        private void btnadd_Click(object sender, EventArgs e)
        {
            add = true; edit = false;
            setEnableDisable(false, false, true, false, true);
            txtClear();
            txtReadOnly(false);
            txtFeeYear.Focus();
        }

        

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (txtFeeStructID.Text == "")
            {
                MessageBox.Show("Select a record to edit.");
                return;
            }
            add = false; 
            edit = true;
            setEnableDisable(false, false, true, false, true);
            txtReadOnly(false);
            txtFeeYear.Focus();
        }

        private void btnCalcel_Click(object sender, EventArgs e)
        {
            add = false;
            edit = false;
            txtClear();
            txtReadOnly(true);
            setEnableDisable(true, false, false, false, false);
            btnAdd.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbFeeType.Text == "" || cbFeeDetail.Text == "" || txtFeeYear.Text == "")
            {
                MessageBox.Show("Please enter all required fields.");
                return;
            }
          
            if (add)
            {
                string status = (cbStatus.Text == "Active") ? "A" : "D";

                string query = "INSERT INTO FeesStructure (FeesYear, DepartmentId, ClassId, FeesDetail, FeesType, FeesAmount, GovtFees, UserId, Status) " +
                               "VALUES ('" + txtFeeYear.Text + "'," + cbDeptCode.SelectedValue + "," + cbClassID.SelectedValue + ",'" + cbFeeDetail.Text + "','" + cbFeeType.Text + "'," + cbFeeAmount.Text + "," + cbGovtApprovedFee.Text + "," + txtUserID.Text + ",'" + status + "')";
                Cmd = new SqlCommand(query, Con);
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                MessageBox.Show("Record Added Successfully!");
                setEnableDisable(true, false, false, false, false);
                txtClear();
                btnAdd.Focus();
            }
            else if (edit)
            {
                string status = (cbStatus.Text == "Active") ? "A" : "D";

                string query = "UPDATE FeesStructure SET FeesYear='" + txtFeeYear.Text + "', DepartmentId='" + cbDeptCode.SelectedValue + "', ClassId='" + cbClassID.SelectedValue + "', FeesDetail='" + cbFeeDetail.Text + "', FeesType='" + cbFeeType.Text + "', FeesAmount='" + cbFeeAmount.Text + "', GovtFees='" + cbGovtApprovedFee.Text + "', UserId='" + txtUserID.Text + "', Status='" + status + "' WHERE FeestructuredId='" + txtFeeStructID.Text + "'";
                Cmd = new SqlCommand(query, Con);
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                MessageBox.Show("Record Updated Successfully!");
                setEnableDisable(true, false, false, false, false);
                txtClear();
                btnAdd.Focus();
            }

            add = false; edit = false;
            ShowData();
            LoadFeeTypes();
            LoadYears();
            //txtClear();
            txtReadOnly(true);
            //setEnableDisable(true, true, false, true, false);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtFeeStructID.Text == "")
            {
                MessageBox.Show("Select a record to delete.");
                return;
            }

            DialogResult dr = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string query = "DELETE FROM FeesStructure WHERE FeestructuredId='" + txtFeeStructID.Text + "'";
                Cmd = new SqlCommand(query, Con);
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                ShowData();
                LoadFeeTypes();
                LoadYears();
                txtClear();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (add || edit) return;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtFeeStructID.Text = row.Cells["FeestructuredId"].Value.ToString();
                txtFeeYear.Text = row.Cells["FeesYear"].Value.ToString();
                cbDeptCode.SelectedValue = row.Cells["DepartmentId"].Value;
                cbClassID.SelectedValue = row.Cells["ClassId"].Value;
                cbFeeDetail.Text = row.Cells["FeesDetail"].Value.ToString();
                cbFeeType.Text = row.Cells["FeesType"].Value.ToString();
                cbFeeAmount.Text = row.Cells["FeesAmount"].Value.ToString();
                cbGovtApprovedFee.Text = row.Cells["GovtFees"].Value.ToString();
                txtUserID.Text = row.Cells["UserId"].Value.ToString();
                cbStatus.Text = (row.Cells["Status"].Value.ToString() == "A") ? "Active" : "Deactive"; ;

                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnAdd.Focus();
            }
        }



        // ---------------------- COPY YEAR ----------------------
        private void cbFromYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableCopyYearButton();
        }

        private void cbToYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableCopyYearButton();
        }

        private void EnableCopyYearButton()
        {
            if (!string.IsNullOrEmpty(cbFromYear.Text) && !string.IsNullOrEmpty(cbToYear.Text))
                btnCopyYear.Enabled = true;
            else
                btnCopyYear.Enabled = false;
        }

        private void btnCopyYear_Click(object sender, EventArgs e)
        {
            if (cbFromYear.Text == cbToYear.Text)
            {
                MessageBox.Show("From Year and To Year cannot be same!");
                return;
            }

            string query = "INSERT INTO FeesStructure (FeesYear, DepartmentId, ClassId, FeesDetail, FeesType, FeesAmount, GovtFees, UserId, Status) " +
                           "SELECT '" + cbToYear.Text + "', DepartmentId, ClassId, FeesDetail, FeesType, FeesAmount, GovtFees, UserId, Status " +
                           "FROM FeesStructure WHERE FeesYear='" + cbFromYear.Text + "'";
            Cmd = new SqlCommand(query, Con);
            Con.Open();
            int rows = Cmd.ExecuteNonQuery();
            Con.Close();
            MessageBox.Show(rows + " records copied from " + cbFromYear.Text + " to " + cbToYear.Text);
            ShowData();
            LoadYears();

        }

        // ---------------------- DELETE YEAR ----------------------
        private void cbDeleteYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDeleteYear.Enabled = !string.IsNullOrEmpty(cbDeleteYear.Text);
        }

        private void btnDeleteYear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbDeleteYear.Text))
            {
                MessageBox.Show("Please select a year to delete.");
                return;
            }

            DialogResult dr = MessageBox.Show("Are you sure you want to delete all records of year " + cbDeleteYear.Text + "?\n\nDid you copy this year to next year before deleting?",
                                              "Confirm Delete Year", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                string query = "DELETE FROM FeesStructure WHERE FeesYear='" + cbDeleteYear.Text + "'";
                Cmd = new SqlCommand(query, Con);
                Con.Open();
                int rows = Cmd.ExecuteNonQuery();
                Con.Close();

                MessageBox.Show(rows + " records deleted for year " + cbDeleteYear.Text);
                ShowData();
                LoadFeeTypes();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string query = "SELECT * FROM FeesStructure WHERE 1=1";
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                query += " AND (CAST(FeestructuredId AS VARCHAR) LIKE '%" + txtSearch.Text + "%' " +
                         "OR FeesDetail LIKE '%" + txtSearch.Text + "%' " +
                         "OR FeesType LIKE '%" + txtSearch.Text + "%')";
            }
            if (!string.IsNullOrEmpty(cbYearSearch.Text) && cbYearSearch.Text != "All")
                query += " AND FeesYear='" + cbYearSearch.Text + "'";
            if (cbClassSearch.SelectedIndex >= 0)
                query += " AND ClassId='" + cbClassSearch.SelectedValue + "'";
            if (!string.IsNullOrEmpty(cbStatusSearch.Text) && cbStatusSearch.Text != "All")
                query += " AND Status='" + cbStatusSearch.Text + "'";

            DA = new SqlDataAdapter(query, Con);
            DS = new DataSet();
            DA.Fill(DS, "FeesStructure");
            dataGridView1.DataSource = DS.Tables["FeesStructure"];
        }


        // ---------------------- FEE TYPE / DETAIL / AMOUNT ----------------------
        // Load FeeDetails as per selected FeeType
        private void cbFeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbFeeDetail.Items.Clear();
            cbFeeAmount.Items.Clear();

            string q1 = "SELECT DISTINCT FeesDetail FROM FeesStructure WHERE FeesType='" + cbFeeType.Text + "' ORDER BY FeesDetail";
            SqlDataAdapter da1 = new SqlDataAdapter(q1, Con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            foreach (DataRow dr in dt1.Rows)
                cbFeeDetail.Items.Add(dr["FeesDetail"].ToString());

            string q2 = "SELECT DISTINCT FeesAmount FROM FeesStructure WHERE FeesType='" + cbFeeType.Text + "' ORDER BY FeesAmount";
            SqlDataAdapter da2 = new SqlDataAdapter(q2, Con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            foreach (DataRow dr in dt2.Rows)
                cbFeeAmount.Items.Add(dr["FeesAmount"].ToString());
        }

        




    }
}
