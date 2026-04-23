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
    public partial class FrmDepartment : UserControl
    {
        public static FrmDepartment instance;
        SqlConnection Con;
        SqlCommand Cmd;
        SqlDataAdapter DA;
        //SqlDataReader Dtr;
        DataSet DS;
        Boolean add, edit;

        public FrmDepartment()
        {
            InitializeComponent();
            instance = this;

            Con = new SqlConnection(global.ConnectionString);
            //global.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Project\School_Management_System\School_Management_System\school.mdf;Integrated Security=True";
            //Con = new SqlConnection(global.ConnectionString);
            add = false;
            edit = false;
            txtReadOnly(true);
            btnSave.Enabled = false;
            txtUserID.ReadOnly = true;
            txtUserID.BackColor = Color.White;
            ShowData();
            SetGridColumnHeaders();
            txtUserID.Text = global.User;
            dataGridView1.ClearSelection();
        }

        public void resize1()
        {
            panel2.Width = global.screenWidth - 240;
            panel2.Height = global.screenHeight - 395;
        }

        private void ShowData()
        {   //Diplay data in gridview 
            String squery = "select * from DepartmentMaster order by DeptId DESC";
            DA = new SqlDataAdapter(squery, Con);
            DS = new DataSet();
            DA.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            //deptCount = DS.Tables[0].Rows.Count;
            dataGridView1.ClearSelection();
            dataGridView1.ClearSelection();
        }

        private void ShowAllData(String query)
        {
            DA = new SqlDataAdapter(query, Con);
            DS = new DataSet();
            DA.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            dataGridView1.ClearSelection();
            dataGridView1.ClearSelection();
        }

        private void txtClear()
        {
            txtDeptID.Text = "";
            txtDeptName.Text = "";
            txtDeptIndex.Text = "";
            txtDeptNameGJ.Text = "";
            txtGOVTDiseNo.Text = "";
            txtGOVTReDeNo.Text = "";
            txtRedgUnder.Text = "";
            cbMedium.SelectedIndex = -1;
            cbStatus.SelectedIndex = -1;
        }

        private void txtReadOnly(Boolean ans)
        {
            TextBox[] tx = { txtDeptName, txtDeptNameGJ, txtGOVTReDeNo, txtGOVTDiseNo, txtDeptIndex, txtRedgUnder };
            for (int x = 0; x < 5; x++)
            {
                tx[x].ReadOnly = ans;
                tx[x].BackColor = Color.White;
            }
            cbStatus.Enabled = !ans;
            cbMedium.Enabled = !ans;
        }


        private void FrmDepartment_Load(object sender, EventArgs e)
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
            txtsearch.Enabled = true;
            //Gridview control
            dataGridView1.Enabled = true;
            dataGridView1.ClearSelection();
            ShowData();
            btnAdd.Focus();
        }

        private void btAdd_Click(object sender, EventArgs e)
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
            txtDeptName.Focus();
            txtUserID.Text = global.User;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtDeptID.Text != "")
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
                txtDeptName.Focus();
            }
            else
            {
                MessageBox.Show("Search Data To Edit...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDeptName.Text != "" && txtDeptNameGJ.Text != "" && txtDeptIndex.Text != "" && txtGOVTDiseNo.Text != "" && txtGOVTReDeNo.Text != "")
            {
                String status;
                if (cbStatus.Text == "Active")
                    status = "A";
                else
                    status = "D";

                if (add == true)
                {
                    Con.Open();
                    string inquery = "insert into DepartmentMaster(eduMedium, DeptName, G_DeptName, DeptINDEX, GOVTRedgNo, GOVTDiseNo, RedgUnder, UserId, Status) values('" + cbMedium.Text + "','" + txtDeptName.Text + "',N'" + txtDeptNameGJ.Text + "','" + txtDeptIndex.Text + "','" + txtGOVTReDeNo.Text + "','" + txtGOVTDiseNo.Text + "','"+txtRedgUnder.Text+"','" + global.User + "','" + status + "')";
                    Cmd = new SqlCommand(inquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    ShowData();
                    FrmSubjectAllotment.instance.AddDataInCombo();
                    //FrmStudent.instance.AddDataInCombo();
                    //FrmStructure.instance.AddDataInCombo();
                    FrmClassMaster.instance.AddDataInCombo();
                    MessageBox.Show("Record Added...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel.PerformClick();
                }

                if (edit == true)
                {
                    Con.Open();
                    string upquery = "update DepartmentMaster set DeptName='" + txtDeptName.Text + "',eduMedium='" + cbMedium.Text + "',G_DeptName=N'" + txtDeptNameGJ.Text + "',GOVTRedgNo='" + txtGOVTReDeNo.Text + "',GOVTDiseNo='" + txtGOVTDiseNo.Text + "',RedgUnder='" + txtRedgUnder.Text + "',Status='" + status + "' where userID='" + txtDeptID.Text + "'";
                    Cmd = new SqlCommand(upquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    ShowData();
                    FrmSubjectAllotment.instance.AddDataInCombo();
                    //FrmStudent.instance.AddDataInCombo();
                    //FrmStructure.instance.AddDataInCombo();
                    FrmClassMaster.instance.AddDataInCombo();
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
            if (txtDeptID.Text != "")
            {
                DialogResult ds = MessageBox.Show("Do You Want To Delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ds == DialogResult.Yes)
                {
                    Con.Open();
                    string dquery = "delete from DepartmentMaster where DeptID='" + txtDeptID.Text + "'";
                    Cmd = new SqlCommand(dquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    ShowData();
                    FrmSubjectAllotment.instance.AddDataInCombo();
                    //FrmStudent.instance.AddDataInCombo();
                    //FrmStructure.instance.AddDataInCombo();
                    FrmClassMaster.instance.AddDataInCombo();
                    txtClear();
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
            txtUserID.Text = global.User;
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (cbSearchAD.Text == "Active")
            {
                string squery = "select * from DepartmentMaster where status='A' AND (DeptID like '%" + txtsearch.Text + "%' OR DeptName like '%" + txtsearch.Text + "%' OR G_DeptName like '%" + txtsearch.Text + "%' OR eduMedium like '%" + txtsearch.Text + "%' OR DeptINDEX like '%" + txtsearch.Text + "%' OR GOVTRedgNo like '%" + txtsearch.Text + "%' OR GOVTDiseNo like '%" + txtsearch.Text + "%' OR UserID like '%" + txtsearch.Text + "%' )";
                ShowAllData(squery);
            }
            else if (cbSearchAD.Text == "Deactive")
            {
                string squery = "select * from DepartmentMaster where status='D' AND (DeptID like '%" + txtsearch.Text + "%' OR DeptName like '%" + txtsearch.Text + "%' OR G_DeptName like '%" + txtsearch.Text + "%' OR eduMedium like '%" + txtsearch.Text + "%' OR DeptINDEX like '%" + txtsearch.Text + "%' OR GOVTRedgNo like '%" + txtsearch.Text + "%' OR GOVTDiseNo like '%" + txtsearch.Text + "%' OR UserID like '%" + txtsearch.Text + "%' )";
                ShowAllData(squery);
            }
            else
            {
                string squery = "select * from DepartmentMaster where DeptID like '%" + txtsearch.Text + "%' OR DeptName like '%" + txtsearch.Text + "%' OR G_DeptName like '%" + txtsearch.Text + "%' OR eduMedium like '%" + txtsearch.Text + "%' OR DeptINDEX like '%" + txtsearch.Text + "%' OR GOVTRedgNo like '%" + txtsearch.Text + "%' OR GOVTDiseNo like '%" + txtsearch.Text + "%' OR UserID like '%" + txtsearch.Text + "%'";
                ShowAllData(squery);
            }
        }

        private void cbSearchAD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchAD.Text == "Active")
            {
                String squery = "select * from DepartmentMaster where status='A'";
                ShowAllData(squery);
            }
            else if (cbSearchAD.Text == "Deactive")
            {
                String squery = "select * from DepartmentMaster where status='D'";
                ShowAllData(squery);
            }
            else
            {
                String squery = "select * from DepartmentMaster";
                ShowAllData(squery);
            }
        }

        private void SetGridColumnHeaders()
        {
            dataGridView1.Columns[0].HeaderText = "Dept ID";
            dataGridView1.Columns[1].HeaderText = "Edu. Medium";
            dataGridView1.Columns[2].HeaderText = "Dept Name";
            dataGridView1.Columns[3].HeaderText = "એકમનું નામ";
            dataGridView1.Columns[4].HeaderText = "Dept Index";
            dataGridView1.Columns[5].HeaderText = "GOVT RedgNo";
            dataGridView1.Columns[6].HeaderText = "GOVT DiseNo";
            dataGridView1.Columns[7].HeaderText = "Redg Under";
            dataGridView1.Columns[8].HeaderText = "UserId";
            dataGridView1.Columns[9].HeaderText = "Status";
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
                    txtDeptID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    cbMedium.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txtDeptName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    txtDeptNameGJ.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    txtDeptIndex.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    txtGOVTReDeNo.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    txtGOVTDiseNo.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                    txtRedgUnder.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                    txtUserID.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                    if (dataGridView1.CurrentRow.Cells[9].Value.ToString() == "A")
                        cbStatus.Text = "Active";
                    else
                        cbStatus.Text = "Deactive";
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

        private void txtDeptID_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Backspace:8
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
                e.Handled = true;
        }

        private void txtDeptNameGJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Backspace:8 space:32 
            char ch = e.KeyChar;
            if (!char.IsLetter(ch) && ch != 8 && ch != 32)
                e.Handled = true;
        }
    }
}

