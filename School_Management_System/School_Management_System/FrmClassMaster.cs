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
    public partial class FrmClassMaster : UserControl
    {
        public static FrmClassMaster instance;
        SqlConnection Con;
        SqlCommand Cmd;
        SqlDataAdapter DA;
        SqlDataReader Dtr;
        DataSet DS;
        Boolean add, edit;

        public FrmClassMaster()
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
            SetGridColumnHeaders();
        }

        private void FrmClassMaster_Load(object sender, EventArgs e)
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
            cbStatusSearch.Enabled = true;
            txtSearch.Enabled = true;
            //Gridview control
            dataGridView1.Enabled = true;
            dataGridView1.ClearSelection();
            ShowData();
            btnAdd.Focus();
            //For Gridview_header
            SetGridColumnHeaders();
        }

        public void resize1()
        {
            panel2.Width = global.screenWidth - 240;
            panel2.Height = global.screenHeight - 350;
        }

        public void AddDataInCombo()
        {
            if (Con.State == ConnectionState.Open) Con.Close();

            // Dept Combo Fill (ID + Name)
            //cbDeptID.Items.Clear();
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

            // Shift Combo Fill (ID + Name)
            //cbShift.Items.Clear();
            Con.Open();
            string shiftQuery = "SELECT ShiftId, ShiftName FROM ShiftMast";
            Cmd = new SqlCommand(shiftQuery, Con);
            Dtr = Cmd.ExecuteReader();
            while (Dtr.Read())
            {
                string itemText = Dtr["ShiftId"].ToString() + " - " + Dtr["ShiftName"].ToString();
                cbShift.Items.Add(itemText);
            }
            Dtr.Close();
            Con.Close();
        }


        private void ShowData()
        {   //Diplay data in gridview 
            String squery = "select * from ClassMaster order by ClassId DESC";
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
            TextBox[] tx = { txtClassName, txtClassIndex, txtMinAge };
            for (int x = 0; x < 3; x++)
            {
                tx[x].ReadOnly = ans;
                tx[x].BackColor = Color.White;
            }
            cbClassHw.Enabled = !ans;
            cbShift.Enabled = !ans;
            cbMedium.Enabled = !ans;
            cbStatus.Enabled = !ans;
            cbDeptID.Enabled = !ans;
        }

        //private void cbEnable(Boolean a)
        //{
        //    cbDeptID.Enabled = a;
        //    cbMedium.Enabled = a;
        //    cbShift.Enabled = a;
        //    cbStatus.Enabled = a;
        //}

        private void txtClear()
        {
            txtClassID.Text = "";
            cbDeptID.SelectedIndex = -1;
            txtClassName.Text = "";
            txtClassIndex.Text = "";
            cbClassHw.SelectedIndex = -1;
            txtMinAge.Text = "";
            cbShift.SelectedIndex = -1;
            cbMedium.SelectedIndex = -1;
            cbStatus.SelectedIndex = -1;
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
            //AddDataInCombo();
            cbStatus.SelectedIndex = 0;
            txtUserID.Text = global.User;
            add = true;
            edit = false;
            cbDeptID.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtClassID.Text != "")
            {
                //Button Controls - [Edit]
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnSave.Enabled = true;
                btnDelete.Enabled = false;
                btnCancel.Enabled = true;
                txtReadOnly(false);
                dataGridView1.Enabled = false;
                //AddDataInCombo();
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
            if(txtMinAge.Text != "") //(cbDeptID.Text != "" && txtClassName.Text != "" && txtClassIndex.Text != "" && txtMinAge.Text != "" && cbShift.Text != "" && cbMedium.Text != "" && cbStatus.Text != "" && txtUserID.Text != "")
            {
                String status, HwStatus;
                if (cbStatus.Text == "Active")
                    status = "A";
                else
                    status = "D";
                if (cbClassHw.Text == "Active")
                    HwStatus = "A";
                else
                    HwStatus = "D";
                if (add == true)
                {
                    string deptIdOnly = cbDeptID.Text.Split('-')[0].Trim();
                    string shiftIdOnly = cbShift.Text.Split('-')[0].Trim();

                    Con.Open();
                    string inquery = "INSERT INTO ClassMaster(DeptId, ClassName, ClassIndex, ClassHw, MinAge, Shift, Medium, Status, UserId) VALUES('" + deptIdOnly + "','" + txtClassName.Text + "','" + txtClassIndex.Text + "', '" + HwStatus + "' ,'" + txtMinAge.Text + "','" + shiftIdOnly + "','" + cbMedium.Text + "', '" + status + "', '" + global.User + "')";
                    Cmd = new SqlCommand(inquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Record Added...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowData();
                    //FrmSubjectAllotment.instance.AddDataInCombo();
                    //FrmSubject.instance.AddDataInCombo();
                    //FrmStructure.instance.AddDataInCombo();
                    //FrmHomework.instance.AddDataInCombo();
                    btnCancel.PerformClick();
                }

                if (edit == true)
                {
                    string deptIdOnly = cbDeptID.Text.Split('-')[0].Trim();
                    string shiftIdOnly = cbShift.Text.Split('-')[0].Trim();

                    Con.Open();
                    string upquery = "UPDATE ClassMaster SET DeptId='" + deptIdOnly + "', ClassName='" + txtClassName.Text + "', ClassIndex='" + txtClassIndex.Text + "', ClassHw = '" + HwStatus + "' , MinAge='" + txtMinAge.Text + "', Shift='" + shiftIdOnly + "', Medium='" + cbMedium.Text + "', Status='" + status + "' WHERE ClassID='" + txtClassID.Text + "'";
                    Cmd = new SqlCommand(upquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    ShowData();
                    //FrmSubjectAllotment.instance.AddDataInCombo();
                    //FrmSubject.instance.AddDataInCombo();
                    //FrmHomework.instance.AddDataInCombo();
                    //FrmStructure.instance.AddDataInCombo();
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
            if (txtClassID.Text != "")
            {
                DialogResult ds = MessageBox.Show("Do You Want To Delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ds == DialogResult.Yes)
                {
                    Con.Open();
                    string dquery = "delete from ClassMaster where ClassId='" + txtClassID.Text + "'";
                    Cmd = new SqlCommand(dquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    //FrmSubjectAllotment.instance.AddDataInCombo();
                    FrmSubject.instance.AddDataInCombo();
                    FrmHomework.instance.AddDataInCombo();
                    //FrmStructure.instance.AddDataInCombo();
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
            ShowData();
            add = false;
            edit = false;
            txtUserID.Text = global.User;
            btnAdd.Focus();
        }

        private void SetGridColumnHeaders()
        {
            dataGridView1.Columns[0].HeaderText = "Class ID";
            dataGridView1.Columns[1].HeaderText = "Dept ID";
            dataGridView1.Columns[2].HeaderText = "Class Name";
            dataGridView1.Columns[3].HeaderText = "Class Index";
            dataGridView1.Columns[4].HeaderText = "Homework Status";
            dataGridView1.Columns[5].HeaderText = "Minimum Age";
            dataGridView1.Columns[6].HeaderText = "Shift";
            dataGridView1.Columns[7].HeaderText = "Medium";
            dataGridView1.Columns[8].HeaderText = "Status";
            dataGridView1.Columns[9].HeaderText = "User ID";
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
                    txtClassID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                    // Dept ID + Name
                    string deptId = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    Con.Open();
                    Cmd = new SqlCommand("SELECT DeptName FROM DepartmentMaster WHERE DeptId='" + deptId + "'", Con);
                    Dtr = Cmd.ExecuteReader();
                    if (Dtr.Read()) 
                        cbDeptID.Text = deptId + " - " + Dtr["DeptName"].ToString();
                    Dtr.Close();
                    Con.Close();

                    txtClassName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    txtClassIndex.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "A")
                        cbClassHw.Text = "Active";
                    else
                        cbClassHw.Text = "Deactive";
                    txtMinAge.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();

                    // Shift ID + Name
                    string shiftId = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                    Con.Open();
                    Cmd = new SqlCommand("SELECT ShiftName FROM ShiftMast WHERE ShiftId='" + shiftId + "'", Con);
                    Dtr = Cmd.ExecuteReader();
                    if (Dtr.Read()) cbShift.Text = shiftId + " - " + Dtr["ShiftName"].ToString();
                    Dtr.Close();
                    Con.Close();

                    cbMedium.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                    if (dataGridView1.CurrentRow.Cells[8].Value.ToString() == "A")
                        cbStatus.Text = "Active";
                    else
                        cbStatus.Text = "Deactive";
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

        private void txtMinAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Backspace:8
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
                e.Handled = true;
        }

        private void txtMinAge_Leave(object sender, EventArgs e)
        {
            if (txtMinAge.Text != "")
            {
                int age = Int32.Parse(txtMinAge.Text);
                if (age <= 0)
                {
                    MessageBox.Show("Invalid Age");
                    txtMinAge.Focus();
                }
            }
        }

        private void txtClassName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Backspace:8 space:32 dash:45 dot:46
            char ch = e.KeyChar;
            if (!char.IsLetter(ch) && !char.IsDigit(ch) && ch != 45 && ch != 8 && ch != 32 && ch != 46)
                e.Handled = true;
        }

        private void cbStatusSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStatusSearch.Text == "Active")
            {
                String squery = "select * from ClassMaster where status='A'";
                ShowAllData(squery);
            }
            else if (cbStatusSearch.Text == "Deactive")
            {
                String squery = "select * from ClassMaster where status='D'";
                ShowAllData(squery);
            }
            else
            {
                String squery = "select * from ClassMaster";
                ShowAllData(squery);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (cbStatusSearch.Text == "Active")
            {
                string squery = "select * from ClassMaster where Status='A' AND ClassId like '%" + txtSearch.Text + "%' OR DeptId like '" + txtSearch.Text + "%' OR ClassName like '" + txtSearch.Text + "%' OR classIndex like '" + txtSearch.Text + "%' OR MinAge like '" + txtSearch.Text + "%' OR Shift like '" + txtSearch.Text + "%' OR Medium like '" + txtSearch.Text + "%' OR UserID like '" + txtSearch.Text + "%' OR Status like '" + txtSearch.Text + "%' order by ClassId DESC";
                ShowAllData(squery);
            }
            else if (cbStatusSearch.Text == "Deactive")
            {
                string squery = "select * from ClassMaster where Status='D' AND ClassId like '%" + txtSearch.Text + "%' OR DeptId like '" + txtSearch.Text + "%' OR ClassName like '" + txtSearch.Text + "%' OR classIndex like '" + txtSearch.Text + "%' OR MinAge like '" + txtSearch.Text + "%' OR Shift like '" + txtSearch.Text + "%' OR Medium like '" + txtSearch.Text + "%' OR UserID like '" + txtSearch.Text + "%' OR Status like '" + txtSearch.Text + "%' order by ClassId DESC";
                ShowAllData(squery);
            }
            else
            {
                string squery = "select * from ClassMaster where  ClassId like '%" + txtSearch.Text + "%' OR DeptId like '" + txtSearch.Text + "%' OR ClassName like '" + txtSearch.Text + "%' OR classIndex like '" + txtSearch.Text + "%' OR MinAge like '" + txtSearch.Text + "%' OR Shift like '" + txtSearch.Text + "%' OR Medium like '" + txtSearch.Text + "%' OR UserID like '" + txtSearch.Text + "%' OR Status like '" + txtSearch.Text + "%' order by ClassId DESC";
                ShowAllData(squery);
            }
        }

        public ComboBoxStyle DropDown { get; set; }

    }
}
