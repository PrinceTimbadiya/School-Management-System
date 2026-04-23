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
    public partial class FrmShift : UserControl
    {
        public static FrmShift instance;
        SqlConnection Con;
        SqlCommand Cmd;
        SqlDataAdapter DA;
        //SqlDataReader Dtr;
        DataSet DS;
        Boolean add, edit;

        public FrmShift()
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

        private void FrmShift_Load(object sender, EventArgs e)
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

        public void resize1()
        {
            panel2.Width = global.screenWidth - 240;
            panel2.Height = global.screenHeight - 370;
        }

        private void ShowData()
        {
            //Diplay data in gridview 
            Con.Open();
            String squery = "select * from ShiftMast ORDER BY ShiftId DESC";
            DA = new SqlDataAdapter(squery, Con);
            DS = new DataSet();
            DA.Fill(DS);
            Con.Close();
            dataGridView1.DataSource = DS.Tables[0];
            //ShiftCount = DS.Tables[0].Rows.Count;
            dataGridView1.ClearSelection();
        }

        private void ShowAllData(String query)
        {
            DA = new SqlDataAdapter(query, Con);
            DS = new DataSet();
            DA.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
        }

        private void txtClear()
        {
            txtShiftID.Text = "";
            txtShiftName.Text = "";
            txtShiftIndex.Text = "";
            txtRemark.Text = "";
            cbStatus.SelectedIndex = -1;
        }

        private void txtReadOnly(Boolean ans)
        {
            TextBox[] tx = { txtShiftName, txtShiftIndex, txtRemark };
            for (int x = 0; x < 3; x++)
            {
                tx[x].ReadOnly = ans;
                tx[x].BackColor = Color.White;
            }
            cbStatus.Enabled = !ans;
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
            txtShiftName.Focus();
            txtUserID.Text = global.User;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtShiftID.Text != "")
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
                txtShiftName.Focus();
            }
            else
            {
                MessageBox.Show("Search Data To Edit...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtShiftName.Text != "" && txtShiftIndex.Text != "" && cbStatus.Text != "" && txtUserID.Text != "")
            {
                String status;
                if (cbStatus.Text == "Active")
                    status = "A";
                else
                    status = "D";

                if (add == true)
                {
                    Con.Open();
                    string inquery = "insert into ShiftMast(ShiftName, ShiftIndex, Status, Remark, UserId) values('" + txtShiftName.Text + "','" + txtShiftIndex.Text + "','" + status + "','" + txtRemark.Text + "','" + txtUserID.Text + "')";
                    Cmd = new SqlCommand(inquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Record Added...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowData();
                    //Staff.instance.AddDataInCombo();
                    //FrmAttendance.instance.AddDataInCombo();
                    FrmClassMaster.instance.AddDataInCombo();
                    btnCancel.PerformClick();
                    
                }

                if (edit == true)
                {
                    Con.Open();
                    string upquery = "update ShiftMast set ShiftName='" + txtShiftName.Text + "',ShiftIndex='" + txtShiftIndex.Text + "',Status='" + status + "',UserID='" + txtUserID.Text + "',Remark='" + txtRemark.Text + "' where ShiftID='" + txtShiftID.Text + "'";
                    Cmd = new SqlCommand(upquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    ShowData();
                    //Staff.instance.AddDataInCombo();
                    //FrmAttendance.instance.AddDataInCombo();
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
            if (txtShiftID.Text != "")
            {
                DialogResult ds = MessageBox.Show("Do You Want To Delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ds == DialogResult.Yes)
                {
                    Con.Open();
                    string dquery = "delete from ShiftMast where ShiftID='" + txtShiftID.Text + "'";
                    Cmd = new SqlCommand(dquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    ShowData();
                    //Staff.instance.AddDataInCombo();
                    //FrmAttendance.instance.AddDataInCombo();
                    FrmClassMaster.instance.AddDataInCombo();
                    txtClear();
                }
                else
                {
                    MessageBox.Show("Nothing selected...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                string squery = "select * from ShiftMast where status='A' AND (ShiftID like '%" + txtsearch.Text + "%' OR ShiftName like '%" + txtsearch.Text + "%' OR ShiftIndex like '%" + txtsearch.Text + "%' OR Status like '%" + txtsearch.Text + "%' OR UserID like '%" + txtsearch.Text + "%' OR Remark like '%" + txtsearch.Text + "%')";
                ShowAllData(squery);
            }
            else if (cbSearchAD.Text == "Deactive")
            {
                string squery = "select * from ShiftMast where status='D' AND (ShiftID like '%" + txtsearch.Text + "%' OR ShiftName like '%" + txtsearch.Text + "%' OR ShiftIndex like '%" + txtsearch.Text + "%' OR Status like '%" + txtsearch.Text + "%' OR UserID like '%" + txtsearch.Text + "%' OR Remark like '%" + txtsearch.Text + "%')";
                ShowAllData(squery);
            }
            else
            {
                string squery = "select * from ShiftMast where ShiftID like '%" + txtsearch.Text + "%' OR ShiftName like '%" + txtsearch.Text + "%' OR ShiftIndex like '%" + txtsearch.Text + "%' OR Status like '%" + txtsearch.Text + "%' OR UserID like '%" + txtsearch.Text + "%' OR Remark like '%" + txtsearch.Text + "%'";
                ShowAllData(squery);
            }
        }

        private void cbSearchAD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchAD.Text == "Active")
            {
                String squery = "select * from ShiftMast where status='A';";
                ShowAllData(squery);
            }
            else if (cbSearchAD.Text == "Deactive")
            {
                String squery = "select * from ShiftMast where status='D';";
                ShowAllData(squery);
            }
            else
            {
                String squery = "select * from ShiftMast;";
                ShowAllData(squery);
            }
        }

        private void SetGridColumnHeaders()
        {
            dataGridView1.Columns[0].HeaderText = "Shift ID";
            dataGridView1.Columns[1].HeaderText = "Shift Name";
            dataGridView1.Columns[2].HeaderText = "Shift Index";
            dataGridView1.Columns[3].HeaderText = "Status";
            dataGridView1.Columns[4].HeaderText = "Remark";
            dataGridView1.Columns[5].HeaderText = "User ID";
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
                    txtShiftID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txtShiftName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txtShiftIndex.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    if (dataGridView1.CurrentRow.Cells[3].Value.ToString() == "A")
                        cbStatus.Text = "Active";
                    else
                        cbStatus.Text = "Deactive";
                    txtRemark.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    txtUserID.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
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

