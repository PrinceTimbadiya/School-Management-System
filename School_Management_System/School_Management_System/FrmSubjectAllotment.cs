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
    public partial class FrmSubjectAllotment : UserControl
    {
        public static FrmSubjectAllotment instance;
        SqlConnection Con;
        SqlCommand Cmd;
        SqlDataAdapter DA;
        SqlDataReader Dtr;
        DataSet DS;
        Boolean add, edit;
        String SubID;

        public FrmSubjectAllotment()
        {
            InitializeComponent();
            instance = this;
            Con = new SqlConnection(global.ConnectionString);
            //global.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Project\School_Management_System\School_Management_System\school.mdf;Integrated Security=True";
            //Con = new SqlConnection(global.ConnectionString);
            add = false;
            edit = false;
            txtReadOnly(true);
            txtUserID.ReadOnly = true;
            txtUserID.BackColor = Color.White;
            ShowData();
            txtUserID.Text = global.User;
            AddDataInCombo();
            txtSearch.Text = global.projectpath;
            SetGridColumnHeaders();
            dataGridView1.ClearSelection();
        }

        private void ShowData()
        {   //Diplay data in gridview 
            String squery = "select * from SubjectAllotment order by AllotmentId DESC";
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
            cbDeptID.Enabled = !ans;
            cbClassID.Enabled = !ans;
            cbSubject.Enabled = !ans;
            cbStaffID.Enabled = !ans;
            cbStatus.Enabled = !ans;
            cbShiftID.Enabled = !ans;

            TextBox[] tx = { txtSubIndex, txtRemark};
            for (int x = 0; x < tx.Length; x++)
            {
                tx[x].ReadOnly = ans;
                tx[x].BackColor = Color.White;
            }
        }

        private void txtClear()
        {
            txtAllotmentID.Text = "";
            cbDeptID.SelectedIndex = -1;
            cbClassID.SelectedIndex = -1;
            cbSubject.SelectedIndex = -1;
            txtSubIndex.Text = "";
            cbShiftID.SelectedIndex = -1;
            cbStaffID.SelectedIndex = -1;
            cbStatus.SelectedIndex = -1;
            txtRemark.Text = "";
        }

        public void AddDataInCombo()
        {
            // Class ID + Class Name Combo fill
            cbClassID.Items.Clear();
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
            }

            Con.Open();
            string classQuery = "SELECT ClassId, ClassName FROM ClassMaster";
            Cmd = new SqlCommand(classQuery, Con);
            Dtr = Cmd.ExecuteReader();
            while (Dtr.Read())
            {
                string itemText = Dtr["ClassId"].ToString() + " - " + Dtr["ClassName"].ToString();
                cbClassID.Items.Add(itemText);
            }
            Dtr.Close();
            Con.Close();

            // Staff ID + Name Combo fill
            cbStaffID.Items.Clear();
            Con.Open();
            string staffQuery = "SELECT StaffId, Name FROM StaffMaster";
            Cmd = new SqlCommand(staffQuery, Con);
            Dtr = Cmd.ExecuteReader();
            while (Dtr.Read())
            {
                string itemText = Dtr["StaffId"].ToString() + " - " + Dtr["Name"].ToString();
                cbStaffID.Items.Add(itemText);
            }
            Dtr.Close();
            Con.Close();

            // Dept ID + Name Combo fill
            cbDeptID.Items.Clear();
            Con.Open();
            string deptQuery = "SELECT DeptId, DeptName FROM DepartmentMaster";
            Cmd = new SqlCommand(deptQuery, Con);
            Dtr = Cmd.ExecuteReader();
            while (Dtr.Read())
            {
                string itemText = Dtr["DeptId"].ToString() + " - " + Dtr["DeptName"].ToString();
                cbDeptID.Items.Add(itemText);
            }
            Dtr.Close();
            Con.Close();

            // Shift ID + Name Combo fill
            cbShiftID.Items.Clear();
            Con.Open();
            string shiftQuery = "SELECT ShiftId, ShiftName FROM ShiftMast";
            Cmd = new SqlCommand(shiftQuery, Con);
            Dtr = Cmd.ExecuteReader();
            while (Dtr.Read())
            {
                string itemText = Dtr["ShiftId"].ToString() + " - " + Dtr["ShiftName"].ToString();
                cbShiftID.Items.Add(itemText);
            }
            Dtr.Close();
            Con.Close();

            // Subject ID + Name Combo fill
            cbSubject.Items.Clear();
            Con.Open();
            string subjectQuery = "SELECT SubId, SubName FROM SubjectMaster";
            Cmd = new SqlCommand(subjectQuery, Con);
            Dtr = Cmd.ExecuteReader();
            while (Dtr.Read())
            {
                string itemText = Dtr["SubId"].ToString() + " - " + Dtr["SubName"].ToString();
                cbSubject.Items.Add(itemText);
            }
            Dtr.Close();
            Con.Close();
        }

        private void FrmSubjectAllotment_Load(object sender, EventArgs e)
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
            txtSearch.Enabled = true;
            //Gridview control
            dataGridView1.Enabled = true;
            dataGridView1.ClearSelection();
            ShowData();
            btnAdd.Focus();
        }

        public void resize1()
        {
            panel2.Width = global.screenWidth - 240;
            panel2.Height = global.screenHeight - 405;
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
            txtRemark.Text = "-";
            cbStatus.SelectedIndex = 0;
            add = true;
            edit = false;
            cbDeptID.Focus();
            txtUserID.Text = global.User;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtAllotmentID.Text != "")
            {
                txtReadOnly(false);
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnSave.Enabled = true;
                btnDelete.Enabled = false;
                btnCancel.Enabled = true;
                dataGridView1.Enabled = false;
                add = false;
                edit = true;
                cbDeptID.Focus();
            }
            else
            {
                MessageBox.Show("Search Data To Edit...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtSubIndex.Text != "") //(cbClassID.Text != "" && cbSubject.Text != "" && cbStaffID.Text != "" && txtUserID.Text != "")
            {
                Con.Open();
                string query1 = "select SubID from SubjectMaster where SubName='" + cbSubject.Text + "'";
                Cmd = new SqlCommand(query1, Con);
                Dtr = Cmd.ExecuteReader();
                //To see record is not null
                try
                {
                    Dtr.Read();
                    SubID = Dtr[0].ToString();
                }
                catch
                {
                    SubID = "0";
                }
                Con.Close();

                String status;
                if (cbStatus.Text == "Active")
                    status = "A";
                else
                    status = "D";

                if (add == true)
                {
                    Con.Open(); 
                    string deptId = cbDeptID.Text.Split('-')[0].Trim();
                    string classId = cbClassID.Text.Split('-')[0].Trim();
                    string staffId = cbStaffID.Text.Split('-')[0].Trim();
                    string shiftId = cbShiftID.Text.Split('-')[0].Trim();
                    string subId = cbSubject.Text.Split('-')[0].Trim();

                    string inquery = "insert into SubjectAllotment(DeptId, ClassId, SubId, SubIndex, StaffId, ShiftId, Status, Remark, UserId) values('" + deptId + "','" + classId + "','" + subId + "','" + txtSubIndex.Text + "','" + staffId + "','" + shiftId + "','" + status + "' ,'" + txtRemark.Text + "','" + txtUserID.Text + "');";
                    Cmd = new SqlCommand(inquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Record Added...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowData();
                    btnCancel.PerformClick();
                }

                else if (edit == true)
                {
                    Con.Open();
                    string deptId = cbDeptID.Text.Split('-')[0].Trim();
                    string classId = cbClassID.Text.Split('-')[0].Trim();
                    string staffId = cbStaffID.Text.Split('-')[0].Trim();
                    string shiftId = cbShiftID.Text.Split('-')[0].Trim();
                    string subId = cbSubject.Text.Split('-')[0].Trim();

                    string upquery = "update SubjectAllotment set DeptId = '" + deptId + "' ,ClassId='" + classId + "',SubId='" + subId + "', SubIndex = '" + txtSubIndex.Text + "',StaffId='" + staffId + "',ShiftId='" + shiftId + "',Status='" + status + "',Remark='" + txtRemark.Text + "',UserID='" + txtUserID.Text + "' where AllotmentID='" + txtAllotmentID.Text + "'";
                    Cmd = new SqlCommand(upquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    ShowData();
                    MessageBox.Show("Record Updated...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (txtAllotmentID.Text != "")
            {
                DialogResult ds = MessageBox.Show("Do You Want To Delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ds == DialogResult.Yes)
                {
                    Con.Open();
                    string dquery = "delete from SubjectAllotment where AllotmentID='" + txtAllotmentID.Text + "'";
                    Cmd = new SqlCommand(dquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    ShowData();
                    txtClear();
                }
            }
            else
            {
                MessageBox.Show("Nothing selected...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            txtUserID.Text = global.User;
        }

        private void SetGridColumnHeaders()
        {
            dataGridView1.Columns[0].HeaderText = "Allotment ID";
            dataGridView1.Columns[1].HeaderText = "Department";
            dataGridView1.Columns[2].HeaderText = "Class";
            dataGridView1.Columns[3].HeaderText = "Subject";
            dataGridView1.Columns[4].HeaderText = "Sub Index";
            dataGridView1.Columns[5].HeaderText = "Staff";
            dataGridView1.Columns[6].HeaderText = "Shift";
            dataGridView1.Columns[7].HeaderText = "Status";
            dataGridView1.Columns[8].HeaderText = "Remark";
            dataGridView1.Columns[9].HeaderText = "User ID";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnAdd.Focus();

                try
                {
                    //AllotmentId
                    txtAllotmentID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                    //DeptId
                    string deptId = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    Con.Open();
                    string q1 = "SELECT DeptName FROM DepartmentMaster WHERE DeptId = '" + deptId + "'";
                    Cmd = new SqlCommand(q1, Con);
                    Dtr = Cmd.ExecuteReader();
                    if (Dtr.Read())
                        cbDeptID.Text = deptId + " - " + Dtr["DeptName"].ToString();
                    Dtr.Close();
                    Con.Close();

                    //ClassId
                    string classId = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    Con.Open();
                    string q4 = "SELECT ClassName FROM ClassMaster WHERE ClassId = '" + classId + "'";
                    Cmd = new SqlCommand(q4, Con);
                    Dtr = Cmd.ExecuteReader();
                    if (Dtr.Read())
                        cbClassID.Text = classId + " - " + Dtr["ClassName"].ToString();
                    Dtr.Close();
                    Con.Close();

                    //SubId
                    string subId = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    Con.Open();
                    string query1 = "select SubName from SubjectMaster where SubId='" + subId + "'";
                    Cmd = new SqlCommand(query1, Con);
                    Dtr = Cmd.ExecuteReader();
                    if (Dtr.Read())
                        cbSubject.Text = subId + " - " + Dtr["SubName"].ToString();
                    Dtr.Close();
                    Con.Close();

                    //SubIndex
                    txtSubIndex.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

                    //StaffId
                    string staffId = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    Con.Open();
                    string q3 = "SELECT Name FROM StaffMaster WHERE StaffId = '" + staffId + "'";
                    Cmd = new SqlCommand(q3, Con);
                    Dtr = Cmd.ExecuteReader();
                    if (Dtr.Read())
                        cbStaffID.Text = staffId + " - " + Dtr["Name"].ToString();
                    Dtr.Close();
                    Con.Close();

                    //ShiftId
                    string shiftId = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                    Con.Open();
                    string q2 = "SELECT ShiftName FROM ShiftMast WHERE ShiftId = '" + shiftId + "'";
                    Cmd = new SqlCommand(q2, Con);
                    Dtr = Cmd.ExecuteReader();
                    if (Dtr.Read())
                        cbShiftID.Text = shiftId + " - " + Dtr["ShiftName"].ToString();
                    Dtr.Close();
                    Con.Close();

                    //Status
                    if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == "A")
                        cbStatus.Text = "Active";
                    else
                        cbStatus.Text = "Deactive";

                    //Remark
                    txtRemark.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();

                    //UserId
                    txtUserID.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
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
            string squery = "select * from SubjectAllotment where AllotmentID like '" + txtSearch.Text + "%' OR DeptId like '" + txtSearch.Text + "%' OR ClassId like '" + txtSearch.Text + "%' OR SubID like '" + txtSearch.Text + "%' OR SubIndex like '" + txtSearch.Text + "%' OR StaffId like '" + txtSearch.Text + "%' OR Status like '" + txtSearch.Text + "%' OR UserID like '" + txtSearch.Text + "%' OR Remark like '" + txtSearch.Text + "%' ";
            ShowAllData(squery);
        }

        private void cbClassID_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cbSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int a = 1;
            //Con.Open();
            //string query1 = "select SubName from SubjectMaster where SubId='" + cbSubject.Text + "';";
            //Cmd = new SqlCommand(query1, Con);
            //Dtr = Cmd.ExecuteReader();
            //cbSubject.Items.Clear();
            ////To see record is not null
            //while (a != 0)
            //{
            //    try
            //    {
            //        Dtr.Read();
            //        cbSubject.Items.Add(Dtr[0].ToString());
            //    }
            //    catch
            //    {
            //        a = 0;
            //    }
            //}
            //Con.Close();
        }
    }
}
