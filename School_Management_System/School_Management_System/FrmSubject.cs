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
    public partial class FrmSubject : UserControl
    {
        public static FrmSubject instance;
        SqlConnection Con;
        SqlCommand Cmd;
        SqlDataAdapter DA;
        DataSet DS;
        Boolean add, edit;


        public FrmSubject()
        {
            InitializeComponent();
            instance = this;
            Con = new SqlConnection(global.ConnectionString);
            add = false;
            edit = false;
            txtReadOnly(true);
            btnSave.Enabled = false;
            txtUserID.ReadOnly = true;
            txtUserID.BackColor = Color.White;
            ShowData();
            txtUserID.Text = global.User;
            AddDataInCombo();
            SetGridColumnHeaders();
            dataGridView1.ClearSelection();
        }

        public void resize1()
        {
            panel2.Width = global.screenWidth - 240;
            panel2.Height = global.screenHeight - 405;
        }

        private void ShowData()
        {   //Diplay data in gridview 
            String squery = "select * from SubjectMaster ORDER BY SubId DESC";
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
            TextBox[] tx = { txtSubjectName, txtGSubjectName, txtRemark, txtSubInd};
            for (int x = 0; x < 4; x++)
            {
                tx[x].ReadOnly = ans;
                tx[x].BackColor = Color.White;
            }
        }

        private void txtClear()
        {
            txtSubjectID.Text = "";
            txtSubjectName.Text = "";
            txtGSubjectName.Text = "";
            txtRemark.Text = "";
            cbStatus.SelectedIndex = -1;
            txtSubInd.Text = "";
        }

        public void AddDataInCombo()
        {
            //classId pela define kareli hti
        }

        private void FrmSubject_Load(object sender, EventArgs e)
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
            txtSubjectName.Focus();
            txtUserID.Text = global.User;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtSubjectID.Text != "")
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
                txtSubjectName.Focus();
            }
            else
            {
                MessageBox.Show("Search Data To Edit...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSubjectName.Text != "" && txtGSubjectName.Text != "" && txtRemark.Text != "" && cbStatus.Text != "" && txtUserID.Text != "")
            {
                if (add == true)
                {
                    String status;
                    if (cbStatus.Text == "Active")
                        status = "A";
                    else
                        status = "D";
                    
                    Con.Open();
                    string inquery = "insert into SubjectMaster(SubName, SubGName, Remark, Status, UserId, SubIndex) values('" + txtSubjectName.Text + "',N'" + txtGSubjectName.Text + "', '" + txtRemark.Text + "', '" + status + "', '" + txtUserID.Text + "', '"+txtSubInd.Text+"');";
                    Cmd = new SqlCommand(inquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Record Added...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowData();
                    btnCancel.PerformClick();
                }

                else if (edit == true)
                {
                    String status;
                    if (cbStatus.Text == "Active")
                        status = "A";
                    else
                        status = "D";
                    Con.Open();
                    string upquery = "update SubjectMaster set SubName='" + txtSubjectName.Text + "', SubGName=N'" + txtGSubjectName.Text + "', Remark='" + txtRemark.Text + "', Status='" + status + "', UserID='" + txtUserID.Text + "', SubIndex='" + txtSubInd.Text + "' where SubID='" + txtSubjectID.Text + "'";
                    Cmd = new SqlCommand(upquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    ShowData();
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
            if (txtSubjectID.Text != "")
            {
                DialogResult ds = MessageBox.Show("Do You Want To Delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ds == DialogResult.Yes)
                {
                    Con.Open();
                    string dquery = "delete from SubjectMaster where SubID='" + txtSubjectID.Text + "'";
                    Cmd = new SqlCommand(dquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
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
            dataGridView1.Columns[0].HeaderText = "Subject ID";
            dataGridView1.Columns[1].HeaderText = "Subject Name";
            dataGridView1.Columns[2].HeaderText = "વિષય";
            dataGridView1.Columns[3].HeaderText = "Remark";
            dataGridView1.Columns[4].HeaderText = "Status";
            dataGridView1.Columns[5].HeaderText = "User Id";
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
                    txtSubjectID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txtSubjectName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txtGSubjectName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    txtRemark.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "A")
                        cbStatus.Text = "Active";
                    else
                        cbStatus.Text = "Deactive";
                    txtUserID.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    txtSubInd.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
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
            String squery;
            string baseQuery = "SELECT * FROM SubjectMaster WHERE ";
            string filterFields = "(SubID like '%" + txtSearch.Text + "%' OR SubName like '%" + txtSearch.Text + "%' OR SubGName like N'%" + txtSearch.Text + "%' OR Remark like '%" + txtSearch.Text + "%' OR Status like '%" + txtSearch.Text + "%' OR UserId like '%" + txtSearch.Text + "%' OR SubIndex like '%" + txtSearch.Text + "%') order by SubId DESC";
            if (cbStatusSearch.Text == "Active")
            {
                squery = baseQuery + "Status = 'A' AND " + filterFields;
            }
            else if (cbStatusSearch.Text == "Deactive")
            {
                squery = baseQuery + "Status = 'D' AND " + filterFields;
            }
            else
            {
                squery = baseQuery + filterFields;
            }
            ShowAllData(squery);
        }

        private void cbStatusSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStatusSearch.Text == "All")
            {
                String squery = "select * from SubjectMaster where SubId like '%" + txtSearch.Text + "%' OR SubName like '" + txtSearch.Text + "%' OR SubGName like N'" + txtSearch.Text + "%' OR Remark like '" + txtSearch.Text + "%' OR Status like '" + txtSearch.Text + "%' OR UserID like '" + txtSearch.Text + "%' OR SubIndex like '%" + txtSubInd.Text + "%' order by SubId DESC";
                ShowAllData(squery);
            }
            else if (cbStatusSearch.Text == "Active")
            {
                String squery = "select * from SubjectMaster where (Status='A') AND (SubId like '%" + txtSearch.Text + "%' OR SubName like '" + txtSearch.Text + "%' OR SubGName like N'" + txtSearch.Text + "%' OR Remark like '" + txtSearch.Text + "%' OR Status like '" + txtSearch.Text + "%' OR UserID like '" + txtSearch.Text + "%'  OR SubIndex like '%" + txtSearch.Text + "%') order by SubId DESC";
                ShowAllData(squery);
            }
            else if (cbStatusSearch.Text == "Deactive")
            {
                String squery = "select * from SubjectMaster where (Status='D') AND (SubID like '%" + txtSearch.Text + "%' OR SubName like '" + txtSearch.Text + "%' OR SubGName like N'" + txtSearch.Text + "%' OR Remark like '" + txtSearch.Text + "%' OR Status like '" + txtSearch.Text + "%' OR UserID like '" + txtSearch.Text + "%'  OR SubIndex like '%" + txtSearch.Text + "%') order by SubId DESC";
                ShowAllData(squery);
            }
            else
            {
                String squery = "select * from SubjectMaster where (Status='A') AND (SubID like '%" + txtSearch.Text + "%' OR SubName like '" + txtSearch.Text + "%' OR SubGName like N'" + txtSearch.Text + "%' OR Remark like '" + txtSearch.Text + "%' OR Status like '" + txtSearch.Text + "%' OR UserID like '" + txtSearch.Text + "%'  OR SubIndex like '%" + txtSearch.Text + "%') order by SubId DESC";
                ShowAllData(squery);
            }
        }

        private void txtSubjectName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Backspace:8 space:32 dash:45 comma:44 dot:46
            char ch = e.KeyChar;
            if (!char.IsLetter(ch) && !char.IsDigit(ch) && ch != 45 && ch != 8 && ch!=32)
                e.Handled = true;
        }
    }
}
