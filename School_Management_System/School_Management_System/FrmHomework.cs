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
//using ClosedXML.Excel;          // for faster performance, but basic excel file
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;


namespace School_Management_System
{
    public partial class FrmHomework : UserControl
    {
        public static FrmHomework instance;
        SqlConnection Con;
        SqlCommand Cmd;
        SqlDataAdapter DA;
        SqlDataReader Dtr;
        DataSet DS;
        Boolean add, edit;
        String SubID, ClassID, staffID;
        String User;
        //public CheckBox chkDefaultRow;
        //public CheckBox chkDynamicRows;


        public FrmHomework()
        {
            InitializeComponent();
            instance = this;
            txtHomeWork.PreviewKeyDown += FrmHomework_PreviewKeyDown;
            //global.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Project\School_Management_System\School_Management_System\school.mdf;Integrated Security=True";
            Con = new SqlConnection(global.ConnectionString);
            add = false;
            edit = false;
            txtReadOnly(true);
            btnSave.Enabled = false;
            ShowData();
            dtpHWDate.Value = DateTime.Today;
            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today;
            User = global.User;
            AddDataInCombo();
            SetGridColumnHeaders();
            //btnAdd.Focus();
        }

        private void FrmHomework_Load(object sender, EventArgs e)
        {
            resize1();

            //Button Control (Enable/Disable)
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnMargin.Enabled = false;
            btnExcel.Enabled = false;
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
            this.Width = global.screenWidth - 230;
            this.Height = global.screenHeight - 20;
            p1.Width = (this.Width * 31) / 100;
            p2.Width = (this.Width * 34) / 100;
            //p3.Width = (this.Width * 10) / 100;
            p4.Width = (this.Width * 33) / 100;

            p2.Location = new Point(p1.Width + 5, 3);
            //p3.Location = new Point(p1.Width + p2.Width + 10, 3);
            p4.Location = new Point(p1.Width + p2.Width + 12, 3);

            panel2.Width = global.screenWidth - 240;
            panel2.Height = global.screenHeight - 370;
        }

        public void AddDataInCombo()
        {

            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
            }

            // Class Name Combo fill
            cbClass.Items.Clear();
            Con.Open();
            string classQuery = "SELECT ClassName FROM ClassMaster";
            Cmd = new SqlCommand(classQuery, Con);
            Dtr = Cmd.ExecuteReader();
            while (Dtr.Read())
            {
                //string itemText = Dtr["ClassId"].ToString() + " - " + Dtr["ClassName"].ToString();
                string itemText = Dtr["ClassName"].ToString();
                cbClass.Items.Add(itemText);
            }
            Dtr.Close();
            Con.Close();

            // Subject Name Combo fill
            cbSubject.Items.Clear();
            Con.Open();
            string subjectQuery = "SELECT SubName FROM SubjectMaster";
            Cmd = new SqlCommand(subjectQuery, Con);
            Dtr = Cmd.ExecuteReader();
            while (Dtr.Read())
            {
                //string itemText = Dtr["SubId"].ToString() + " - " + Dtr["SubName"].ToString();
                string itemText = Dtr["SubName"].ToString();
                cbSubject.Items.Add(itemText);
            }
            Dtr.Close();
            Con.Close();


            //============ Search =============//
            //Search class Combo Fill
            cbClassSearch.Items.Clear();
            Con.Open();
            string classQueryS = "SELECT ClassName FROM ClassMaster";
            Cmd = new SqlCommand(classQueryS, Con);
            Dtr = Cmd.ExecuteReader();
            while (Dtr.Read())
            {
                //string itemText = Dtr["ClassId"].ToString() + " - " + Dtr["ClassName"].ToString();
                string itemText = Dtr["ClassName"].ToString();
                cbClassSearch.Items.Add(itemText);
            }
            Dtr.Close();
            Con.Close();
            // Subject Name Combo fill
            cbSubjectSearch.Items.Clear();
            Con.Open();
            string subjectQueryS = "SELECT SubName FROM SubjectMaster";
            Cmd = new SqlCommand(subjectQueryS, Con);
            Dtr = Cmd.ExecuteReader();
            while (Dtr.Read())
            {
                //string itemText = Dtr["SubId"].ToString() + " - " + Dtr["SubName"].ToString();
                string itemText = Dtr["SubName"].ToString();
                cbSubjectSearch.Items.Add(itemText);
            }
            Dtr.Close();
            Con.Close();
            // Staff surname + Name Combo fill
            cbStaffSearch.Items.Clear();
            Con.Open();
            string staffQueryS = "SELECT Surname, Name FROM StaffMaster";
            Cmd = new SqlCommand(staffQueryS, Con);
            Dtr = Cmd.ExecuteReader();
            while (Dtr.Read())
            {
                //string itemText = Dtr["SubId"].ToString() + " - " + Dtr["SubName"].ToString();
                string itemText = Dtr["Surname"].ToString() + " " + Dtr["Name"].ToString();
                cbStaffSearch.Items.Add(itemText);
            }
            Dtr.Close();
            Con.Close();


        }

        private void ShowData()
        {
            try
            {
                string squery = "SELECT HWMaster.HwId, HWMaster.HwDetails, ClassMaster.ClassName AS Class, " +
                                "SubjectMaster.SubName AS Subject, HWMaster.HwDt, " +
                                "(StaffMaster.Surname + ' ' + StaffMaster.Name) AS Staff, HWMaster.Remark " +
                                "FROM HWMaster " +
                                "INNER JOIN ClassMaster ON HWMaster.ClassId = ClassMaster.ClassId " +
                                "INNER JOIN SubjectMaster ON HWMaster.SubId = SubjectMaster.SubId " +
                                "LEFT JOIN StaffMaster ON HWMaster.StaffId = StaffMaster.StaffId " +
                                "WHERE 1=1";

                if (cbClassSearch.Text.Trim() != "")
                    squery += " AND ClassMaster.ClassName LIKE '%" + cbClassSearch.Text + "%'";

                if (cbSubjectSearch.Text.Trim() != "")
                    squery += " AND SubjectMaster.SubName LIKE '%" + cbSubjectSearch.Text + "%'";

                if (cbStaffSearch.Text.Trim() != "")
                    squery += " AND (StaffMaster.Surname + ' ' + StaffMaster.Name) LIKE '%" + cbStaffSearch.Text + "%'";

                if (!chkAllDates.Checked)
                    squery += " AND HWMaster.HwDt BETWEEN '" + dtpFrom.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpTo.Value.ToString("yyyy-MM-dd") + "'";

                if (txtSearch.Text.Trim() != "")
                    squery += " AND (HWMaster.HwId LIKE '%" + txtSearch.Text + "%' OR " +
                              "ClassMaster.ClassName LIKE '%" + txtSearch.Text + "%' OR " +
                              "SubjectMaster.SubName LIKE '%" + txtSearch.Text + "%' OR " +
                              "StaffMaster.Surname LIKE '%" + txtSearch.Text + "%' OR " +
                              "StaffMaster.Name LIKE '%" + txtSearch.Text + "%' OR " +
                              "HWMaster.HwDetails LIKE '%" + txtSearch.Text + "%' OR " +
                              "HWMaster.Remark LIKE '%" + txtSearch.Text + "%')";

                // sorting rule: agar class search hui hai to date desc, warna id desc
                if (cbClassSearch.Text.Trim() != "")
                    squery += " ORDER BY HWMaster.HwDt DESC";
                else
                    squery += " ORDER BY HWMaster.HwId DESC";

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

        private double GetPaperWidth(string paperName)
        {
            // Inches me width (portrait orientation)
            switch (paperName)
            {
                case "A5": return 5.83;
                case "A4": return 8.27;
                case "A3": return 11.69;
                case "Letter": return 8.50;
                case "Legal": return 8.50;
                default: return 8.27; // Default A4
            }
        }

        private double ColumnWidthToRowHeight(double colWidth)
        {
            // Excel me column width 1 = approx 7.5 points row height
            return colWidth * 7.5;
        }

        private void txtReadOnly(Boolean ans)
        {
            cbClass.Enabled = !ans;
            cbSubject.Enabled = !ans;
            dtpHWDate.Enabled = !ans;

            TextBox[] tx = { txtHomeWork, txtRemark };
            for (int x = 0; x < tx.Length; x++)
            {
                tx[x].ReadOnly = ans;
                tx[x].BackColor = Color.White;
            }
        }

        private void txtClear()
        {
            txtHomeWorkID.Text = "";
            //cbClass.SelectedIndex = -1;
            cbSubject.SelectedIndex = -1;
            txtStaffID.Text = "";
            txtHomeWork.Text = "";
            txtRemark.Text = "";
            //txtHWDispaly.Text = "";
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
            add = true;
            edit = false;
            //dtpHWDate.Value = DateTime.Now;
            txtRemark.Text = "-";
            cbClass.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtHomeWorkID.Text != "")
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
                cbClass.Focus();
            }
            else
            {
                MessageBox.Show("Search Data To Edit...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String Class = " ", Subject = " ", Staff = " ";
            if (cbClass.Text != "" && cbSubject.Text != "" && txtStaffID.Text != "" && txtHomeWork.Text != "")
            {

                //DateTime HwDate = DateTime.Now;
                

                //ClassName to ClassIC

                Con.Open();
                string nquery1 = "select ClassID from ClassMaster where ClassName='" + cbClass.Text + "';";
                Cmd = new SqlCommand(nquery1, Con);
                Dtr = Cmd.ExecuteReader();
                //To see record is not null
                try
                {
                    Dtr.Read();
                    Class = Dtr[0].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: ", ex.Message);
                }
                Con.Close();


                Con.Open();
                string query1 = "select SubID from SubjectMaster where SubName='" + cbSubject.Text + "' ";
                Cmd = new SqlCommand(query1, Con);
                Dtr = Cmd.ExecuteReader();
                //To see record is not null
                try
                {
                    Dtr.Read();
                    Subject = Dtr[0].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: ", ex.Message);
                }
                Con.Close();


                Con.Open();
                string nquery2 = "select StaffID from SubjectAllotment where SubID='" + Subject + "'";
                Cmd = new SqlCommand(nquery2, Con);
                Dtr = Cmd.ExecuteReader();
                try
                {
                    Dtr.Read();
                    Staff = Dtr[0].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: ", ex.Message);
                }
                Con.Close();


                // Duplicate Check - Desi Tarika
                //Con.Open();
                //string dupQuery = "SELECT COUNT(*) FROM HWMaster WHERE ClassID = '" + Class + "' AND SubID = '" + Subject + "' AND HWDt = '" + dtpHWDate.Value.ToString("yyyy-MM-dd") + "'";
                //Cmd = new SqlCommand(dupQuery, Con);
                //int exists = Convert.ToInt32(Cmd.ExecuteScalar());
                //Con.Close();

                //if (exists > 0 && add == true)
                //{
                //    MessageBox.Show("Homework for this Class, Subject and Date already exists!", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                if (add == true)
                {
                    Con.Open();
                    string inquery = "insert into HWMaster(HwDetails, ClassId, SubId, HwDt, StaffId, Remark, UserId) values(N'" + txtHomeWork.Text + "', '" + Class + "','" + Subject + "','" + dtpHWDate.Value.ToString("yyyy-MM-dd") + "','" + Staff + "','" + txtRemark.Text + "','" + User + "')";
                    Cmd = new SqlCommand(inquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Record Added...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel.PerformClick();
                    ShowData();
                }

                if (edit == true)
                {
                    Con.Open();
                    string upquery = "update HWMaster set HwDetails=N'" + txtHomeWork.Text + "', ClassID='" + Class + "', SubID='" + Subject + "', HWDt='" + dtpHWDate.Value.ToString("yyyy-MM-dd") + "', StaffID='" + Staff + "', Remark='" + txtRemark.Text + "', UserID='" + User + "' where HwID='" + txtHomeWorkID.Text + "'";
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

        private void SetGridColumnHeaders()
        {
            dataGridView1.Columns[0].HeaderText = "Homework ID";
            dataGridView1.Columns[1].HeaderText = "Homework Details";
            dataGridView1.Columns[2].HeaderText = "Class";
            dataGridView1.Columns[3].HeaderText = "Subject";
            dataGridView1.Columns[4].HeaderText = "Homework Date";
            dataGridView1.Columns[5].HeaderText = "Staff Name";
            dataGridView1.Columns[6].HeaderText = "Remark";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Con.State == ConnectionState.Open)
                Con.Close();


            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index != -1)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnMargin.Enabled = true;
                btnExcel.Enabled = true;
                btnAdd.Focus();

                try
                {

                    txtHomeWorkID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txtHomeWork.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

                    // Class Name se ID nikalna
                    string className = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    Con.Open();
                    Cmd = new SqlCommand("SELECT ClassId FROM ClassMaster WHERE ClassName='" + className + "'", Con);
                    object cid = Cmd.ExecuteScalar();
                    if (cid != null)
                        ClassID = cid.ToString();
                    cbClass.Text = className;
                    Con.Close();

                    // Subject Name se ID nikalna
                    string subName = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    Con.Open();
                    Cmd = new SqlCommand("SELECT SubId FROM SubjectMaster WHERE SubName='" + subName + "'", Con);
                    object sid = Cmd.ExecuteScalar();
                    if (sid != null)
                        SubID = sid.ToString();
                    cbSubject.Text = subName;
                    Con.Close();

                    dtpHWDate.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[4].Value.ToString());

                    // Staff Name se ID nikalna
                    //string staffName = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    //if (!string.IsNullOrEmpty(staffName))
                    //{
                    //    Con.Open();
                    //    Cmd = new SqlCommand("SELECT StaffId FROM StaffMaster WHERE (Surname + ' ' + Name)='" + staffName + "'", Con);
                    //    object stid = Cmd.ExecuteScalar();
                    //    if (stid != null) 
                    //        staffID = stid.ToString();
                    //    txtStaffID.Text = staffName;
                    //    Con.Close();
                    //}


                    // Staff Name direct grid se le
                    string staffName = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    txtStaffID.Text = staffName;

                    // Agar staffID chahiye toh subject se nikalo
                    if (!string.IsNullOrEmpty(SubID) && !string.IsNullOrEmpty(ClassID))
                    {
                        Con.Open();
                        Cmd = new SqlCommand("SELECT StaffID FROM SubjectAllotment WHERE SubID='" + SubID + "' AND ClassID='" + ClassID + "'", Con);
                        object stid = Cmd.ExecuteScalar();
                        if (stid != null)
                            staffID = stid.ToString();
                        Con.Close();
                    }

                    txtRemark.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

                    // homework display text update
                    cbClass_SelectedIndexChanged(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error filling form: " + ex.Message);
                }
                finally
                {
                    if (Con.State == ConnectionState.Open)
                        Con.Close();
                }
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtHomeWorkID.Text != "")
            {
                DialogResult ds = MessageBox.Show("Do You Want To Delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ds == DialogResult.Yes)
                {
                    Con.Open();
                    string dquery = "delete from HWMaster where HWID='" + txtHomeWorkID.Text + "'";
                    Cmd = new SqlCommand(dquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    btnCancel.PerformClick();
                    ShowData();
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
            //dtpHWDate.Value = DateTime.Today;
        }

        private void cbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbClass.Text))
            {
                string HW = "";
                if (Con.State == ConnectionState.Open) Con.Close();
                Con.Open();
                string query2 = "SELECT HWMaster.HwDt, SubjectMaster.SubName, HWMaster.HwDetails " +
                                "FROM HWMaster " +
                                "INNER JOIN SubjectMaster ON HWMaster.SubId = SubjectMaster.SubId " +
                                "WHERE HWMaster.ClassId = '" + ClassID + "' " +
                                "AND HWMaster.HwDt = '" + dtpHWDate.Value.ToString("yyyy-MM-dd") + "'";
                Cmd = new SqlCommand(query2, Con);
                Dtr = Cmd.ExecuteReader();

                // Format cbClass.Text like "1A" -> "Std.1(A)"
                string classText = cbClass.Text.Trim();
                string digits = "";
                string letters = "";

                for (int i = 0; i < classText.Length; i++)
                {
                    if (char.IsDigit(classText[i]))
                        digits += classText[i];
                    else
                        letters += classText[i];
                }

                string classNameFormatted = "Std." + digits + "(" + letters + ")";
                string hwDate = dtpHWDate.Value.ToString("dd/MM");

                HW = classNameFormatted + " [હોમવર્ક Dt." + hwDate + "]";

                while (Dtr.Read())
                {
                    HW += " (" + Dtr["SubName"].ToString() + ") " + Dtr["HwDetails"].ToString() + ",";
                }

                txtHWDispaly.Text = HW.TrimEnd(',');
                Dtr.Close();
                Con.Close();
            }


        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ShowData();
        }

        private void cbSubject_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Con.State == ConnectionState.Open)
                Con.Close();

            int a = 1;
            Con.Open();
            string query1 = "select SubID from SubjectMaster where SubName='" + cbSubject.Text + "' ";
            Cmd = new SqlCommand(query1, Con);
            Dtr = Cmd.ExecuteReader();
            //To see record is not null
            while (a != 0)
            {
                try
                {
                    Dtr.Read();
                    SubID = Dtr[0].ToString();
                }
                catch
                {
                    a = 0;
                }
            }
            Con.Close();

            a = 1;
            Con.Open();
            string nquery1 = "select StaffID from SubjectAllotment where SubID='" + SubID + "'";
            Cmd = new SqlCommand(nquery1, Con);
            Dtr = Cmd.ExecuteReader();
            while (a != 0)
            {
                try
                {
                    Dtr.Read();
                    staffID = Dtr[0].ToString();
                }
                catch
                {
                    a = 0;
                }
            }
            Con.Close();

            a = 1;
            Con.Open();
            string nquery2 = "select SurName,Name from StaffMaster where StaffID='" + staffID + "'";
            Cmd = new SqlCommand(nquery2, Con);
            Dtr = Cmd.ExecuteReader();
            while (a != 0)
            {
                try
                {
                    Dtr.Read();
                    String nm = Dtr[0].ToString();
                    nm += " " + Dtr[1].ToString();
                    txtStaffID.Text = nm;
                }
                catch
                {
                    a = 0;
                }
            }
            Con.Close();


        }

        private void FrmHomework_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // check karo kya key Function wali hai
            if (e.KeyCode.ToString().StartsWith("F"))
            {
                string pressedKey = e.KeyCode.ToString(); // e.g. "F2"
                ShowFunctionData(pressedKey);
            }
        }

        private void ShowFunctionData(string functionKey)
        {
            try
            {
                Con.Open();
                Cmd = new SqlCommand("SELECT DisplayData FROM FunctionMaster WHERE FunctionKey='" + functionKey + "'", Con);
                object result = Cmd.ExecuteScalar();
                if (result != null)
                {
                    // Append mode
                    if (txtHomeWork.Text.Length > 0)
                        txtHomeWork.AppendText(" " + result.ToString());
                    else
                        txtHomeWork.Text = result.ToString();
                }
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                if (Con.State == System.Data.ConnectionState.Open)
                    Con.Close();
            }
        }

        private void btnMargin_Click(object sender, EventArgs e)
        {
            //FrmHwMargin hm = new FrmHwMargin();
            //hm.Show();

            if (string.IsNullOrWhiteSpace(txtHWDispaly.Text))
            {
                MessageBox.Show("Please select homework first!");
                return;
            }

            FrmHwMargin marginForm = new FrmHwMargin(txtHWDispaly.Text, cbClass.Text);
            marginForm.ShowDialog();
        }


        // ------------------------------
        // Helper function (inside FrmHomework)
        //private int GetRowsForOutput(int cols)
        //{
        //    if (cols <= 0) return 1;

        //    if (FrmHwMargin.IsDefaultChecked)         // Default mode
        //    {
        //        int totalStudents = GetDefaultStudentCount(cbClass.Text);
        //        if (totalStudents <= 0) 
        //            totalStudents = 1;
        //        return (int)Math.Ceiling((double)totalStudents / cols);
        //    }
        //    else if (FrmHwMargin.IsDynamicChecked)   // Dynamic mode
        //    {
        //        int totalStudents = GetStudentCount(cbClass.Text);
        //        if (totalStudents <= 0) 
        //            totalStudents = 1;
        //        return (int)Math.Ceiling((double)totalStudents / cols);
        //    }
        //    else
        //    {
        //        // Agar manual mode ka option nahi hai, to fallback 1 row
        //        return 1;
        //    }
        //}

        //private int GetDefaultStudentCount(string className)
        //{
        //    int count = 0;
        //    try
        //    {
        //        Con.Open();
        //        string q = "SELECT TOP 1 TotalStudents FROM ClassDefaults WHERE ClassName='" + className + "' ORDER BY DefaultDate DESC, DefaultId DESC";
        //        Cmd = new SqlCommand(q, Con);
        //        object obj = Cmd.ExecuteScalar();
        //        if (obj != null) count = Convert.ToInt32(obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error fetching default student count: " + ex.Message);
        //    }
        //    finally
        //    {
        //        if (Con.State == ConnectionState.Open) Con.Close();
        //    }
        //    return count;
        //}

        //private int GetStudentCount(string className)
        //{
        //    int count = 0;
        //    try
        //    {
        //        Con.Open();
        //        string q = "SELECT COUNT(*) FROM StudentMaster WHERE ClassName='" + className + "'";
        //        Cmd = new SqlCommand(q, Con);
        //        count = Convert.ToInt32(Cmd.ExecuteScalar());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error fetching student count: " + ex.Message);
        //    }
        //    finally
        //    {
        //        if (Con.State == ConnectionState.Open) Con.Close();
        //    }
        //    return count;
        //}

        private void btnExcel_Click(object sender, EventArgs e)
        {
            String SelectedClassName = cbClass.Text;

            if (string.IsNullOrWhiteSpace(txtHWDispaly.Text))
            {
                MessageBox.Show("Homework text is empty!");
                return;
            }

            decimal fontSize = 0, mTop = 0, mBottom = 0, mLeft = 0, mRight = 0, separatorWidth = 0, separatorHeight = 0;
            string type = "";
            int rows = 0, cols = 0;
            string paper = "";

            // --- Fetch HW margin ---
            try
            {
                Con.Open();
                Cmd = new SqlCommand("SELECT TOP 1 * FROM HomeworkMargin WHERE IsDefault='Y' ORDER BY HwMarginId DESC", Con);
                Dtr = Cmd.ExecuteReader();
                if (Dtr.Read())
                {
                    fontSize = Convert.ToDecimal(Dtr["Size"]);
                    mTop = Convert.ToDecimal(Dtr["MTop"]);
                    mBottom = Convert.ToDecimal(Dtr["MBottom"]);
                    mLeft = Convert.ToDecimal(Dtr["MLeft"]);
                    mRight = Convert.ToDecimal(Dtr["MRight"]);
                    cols = Convert.ToInt32(Dtr["Columns"]);
                    rows = Convert.ToInt32(Dtr["Rows"]);    // Default fallback
                    type = Dtr["RowType"].ToString(); // default
                    paper = Dtr["PaperSize"].ToString();
                    separatorWidth = Convert.ToDecimal(Dtr["HorizontalSpace"]);
                    separatorHeight = Convert.ToDecimal(Dtr["VerticalSpace"]);
                }
                Dtr.Close();


                if (type == "Static") // ClassDefaults
                {
                    string q = "SELECT TOP 1 TotalStudents FROM ClassDefaults WHERE ClassName='" + SelectedClassName + "' ORDER BY DefaultDate DESC, DefaultId DESC";
                    Cmd = new SqlCommand(q, Con);
                    object val = Cmd.ExecuteScalar();
                    int totalStudents = (val != null && val != DBNull.Value) ? Convert.ToInt32(val) : 1;
                    rows = (int)Math.Ceiling((double)totalStudents / cols);
                }
                else if (type == "ByStudent") // StudentMaster
                {
                    string q = "SELECT COUNT(*) FROM StudentMaster WHERE CurrentClass='" + SelectedClassName + "'";
                    Cmd = new SqlCommand(q, Con);
                    int totalStudents = Convert.ToInt32(Cmd.ExecuteScalar());
                    if (totalStudents <= 0) totalStudents = 1;
                    rows = (int)Math.Ceiling((double)totalStudents / cols);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching HW margin: " + ex.Message);
            }
            finally 
            { 
                if (Con.State == ConnectionState.Open) 
                    Con.Close(); 
            }


            if (cols <= 0 || rows <= 0)
            {
                MessageBox.Show("Rows/Columns must be greater than 0.");
                return;
            }

            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel._Worksheet worksheet = null;

            try
            {
                excelApp = new Excel.Application();
                excelApp.Visible = true;
                excelApp.DisplayAlerts = false;

                workbook = excelApp.Workbooks.Add();
                worksheet = (Excel._Worksheet)workbook.ActiveSheet;

                double paperWidthInches = GetPaperWidth(paper);
                double charsPerInch = 12.8;
                double usableInches = paperWidthInches;
                double totalChars = usableInches * charsPerInch;
                double contentWidth = (totalChars - ((double)separatorWidth * (cols - 1))) / (double)cols;
                if (contentWidth <= 0) contentWidth = 5;

                int startRow = 1;

                // Fill cells
                for (int r = 0; r < rows; r++)
                {
                    int actualRow = startRow + (r * 2);
                    for (int b = 0; b < cols; b++)
                    {
                        int col = (b * 2) + 1;
                        worksheet.Cells[actualRow, col] = txtHWDispaly.Text;
                    }
                }

                // Formatting
                worksheet.Cells.Font.Size = (int)fontSize;
                worksheet.Cells.WrapText = true;
                worksheet.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                worksheet.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                // Column widths
                for (int c = 1; c <= (cols * 2) - 1; c++)
                    worksheet.Columns[c].ColumnWidth = (c % 2 == 1) ? contentWidth : (double)separatorWidth;

                // Row heights with content autofit
                for (int r = 1; r <= (rows * 2) - 1; r++)
                {
                    if (r % 2 == 1) // content row
                    {
                        Excel.Range rowRange = worksheet.Rows[r];
                        rowRange.WrapText = true;
                        rowRange.AutoFit(); // ✅ AutoFit content
                    }
                    else // separator row
                    {
                        worksheet.Rows[r].RowHeight = ColumnWidthToRowHeight((double)separatorHeight);
                    }
                }

                // Margins
                worksheet.PageSetup.LeftMargin = excelApp.InchesToPoints((double)mLeft / 10);
                worksheet.PageSetup.RightMargin = excelApp.InchesToPoints((double)mRight / 10);
                worksheet.PageSetup.TopMargin = excelApp.InchesToPoints((double)mTop / 10);
                worksheet.PageSetup.BottomMargin = excelApp.InchesToPoints((double)mBottom / 10);
                worksheet.PageSetup.Zoom = false;
                worksheet.PageSetup.FitToPagesWide = 1;
                worksheet.PageSetup.FitToPagesTall = false;

                // Save
                string defaultFileName = "Hw_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel Workbook|*.xlsx";
                saveDialog.Title = "Save Homework File";
                saveDialog.FileName = defaultFileName;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveDialog.FileName;
                    if (File.Exists(filePath)) File.Delete(filePath);
                    workbook.SaveAs(filePath);
                    MessageBox.Show("Excel file saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                ReleaseObject(worksheet);
                ReleaseObject(workbook);
                ReleaseObject(excelApp);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }



        private void ReleaseObject(object obj)
        {
            try
            {
                if (obj != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            }
            catch { }
            finally { obj = null; }
        }

        //private void dtpHwDtSearch_ValueChanged(object sender, EventArgs e)
        //{
        //    ShowData();
        //}

        private void cbClassSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowData();
        }

        private void cbSubjectSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowData();
        }

        private void cbSubject_Leave(object sender, EventArgs e)
        {
            if (Con.State == ConnectionState.Open)
                Con.Close();

            string Class = "", Subject = "", Staff = "";
            DateTime HwDate = dtpHWDate.Value.Date;

            // Get ClassID
            try
            {
                Con.Open();
                string nquery1 = "SELECT ClassID FROM ClassMaster WHERE ClassName = '" + cbClass.Text + "'";
                Cmd = new SqlCommand(nquery1, Con);
                Dtr = Cmd.ExecuteReader();
                if (Dtr.Read())
                {
                    Class = Dtr[0].ToString();
                }
                Dtr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Class fetch: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }

            // Get SubID
            try
            {
                Con.Open();
                string query1 = "SELECT SubID FROM SubjectMaster WHERE SubName = '" + cbSubject.Text + "'";
                Cmd = new SqlCommand(query1, Con);
                Dtr = Cmd.ExecuteReader();
                if (Dtr.Read())
                {
                    Subject = Dtr[0].ToString();
                }
                Dtr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Subject fetch: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }

            // Get StaffID
            try
            {
                Con.Open();
                string nquery2 = "SELECT StaffID FROM SubjectAllotment WHERE SubID = '" + Subject + "'";
                Cmd = new SqlCommand(nquery2, Con);
                Dtr = Cmd.ExecuteReader();
                if (Dtr.Read())
                {
                    Staff = Dtr[0].ToString();
                }
                Dtr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Staff fetch: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }


            // Check for duplicate HW entry
            int exists = 0;
            try
            {
                Con.Open();
                string dupQuery = "SELECT COUNT(*) FROM HWMaster WHERE ClassId = '" + Class + "' AND SubId = '" + Subject + "' AND HwDt = '" + dtpHWDate.Value.ToString("yyyy-MM-dd") + "'";
                Cmd = new SqlCommand(dupQuery, Con);
                exists = Convert.ToInt32(Cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in duplicate check: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }

            //// Duplicate exists & user in ADD mode
            //if (exists > 0 || add == true)
            //{
            //    DialogResult ds = MessageBox.Show("Homework for this Class, Subject and Date already exists!\nDo you want to edit it?","Duplicate Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            //    if (ds == DialogResult.Yes)
            //    {
            //        // Load the existing record to edit
            //        try
            //        {
            //            Con.Open();
            //            string loadQuery = "SELECT * FROM HWMaster WHERE ClassID = '" + Class + "' AND SubID = '" + Subject + "' AND HWDt = '" + dtpHWDate.Value.ToString("yyyy-MM-dd") + "'";
            //            Cmd = new SqlCommand(loadQuery, Con);
            //            Dtr = Cmd.ExecuteReader();
            //            if (Dtr.Read())
            //            {
            //                txtHomeWork.Text = Dtr["HwDetails"].ToString();
            //                txtRemark.Text = Dtr["Remark"].ToString();
            //                txtHomeWorkID.Text = Dtr["HwID"].ToString();  // Required for update
            //                txtHomeWork.Focus();
            //            }
            //            Dtr.Close();

            //            add = false;
            //            edit = true;
            //            ShowData();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Error loading record: " + ex.Message);
            //        }
            //        finally
            //        {
            //            Con.Close();
            //        }
            //    }
            //    else
            //    {
            //        cbSubject.Text = "";
            //        cbSubject.Focus();
            //    }

            //    return;
            //}



            // Duplicate exists & user in ADD mode
            if (exists > 0 && add == true)
            {
                DialogResult ds = MessageBox.Show("Homework for this Class, Subject and Date already exists!\nDo you want to edit it?", "Duplicate Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (ds == DialogResult.Yes)
                {
                    // Load the existing record to edit
                    try
                    {
                        Con.Open();
                        string loadQuery = "SELECT * FROM HWMaster WHERE ClassID = '" + Class + "' AND SubID = '" + Subject + "' AND HWDt = '" + dtpHWDate.Value.ToString("yyyy-MM-dd") + "'";
                        Cmd = new SqlCommand(loadQuery, Con);
                        Dtr = Cmd.ExecuteReader();
                        if (Dtr.Read())
                        {
                            txtHomeWork.Text = Dtr["HwDetails"].ToString();
                            txtRemark.Text = Dtr["Remark"].ToString();
                            txtHomeWorkID.Text = Dtr["HwID"].ToString();  // Required for update
                            txtHomeWork.Focus();
                        }
                        Dtr.Close();

                        add = false;
                        edit = true;
                        ShowData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading record: " + ex.Message);
                    }
                    finally
                    {
                        Con.Close();
                    }
                }
                else
                {
                    cbSubject.Text = "";
                    cbSubject.Focus();
                }

                return;
            }

        }

        private void cbStaffSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No data to export!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel Workbook|*.xlsx";
            saveDialog.Title = "Save Homework Excel File";
            saveDialog.FileName = "Homework_Report_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";

            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;

            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                excelApp = new Excel.Application();
                workbook = excelApp.Workbooks.Add(Type.Missing);
                worksheet = (Excel.Worksheet)workbook.Sheets[1];
                worksheet.Name = "Homework Report";

                // Column headers
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    Excel.Range headerRange = (Excel.Range)worksheet.Cells[1, i + 1];
                    headerRange.Value2 = dataGridView1.Columns[i].HeaderText;
                    headerRange.Font.Bold = true;
                    headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    headerRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                }

                // Data rows
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        object cellValue = dataGridView1.Rows[i].Cells[j].Value;
                        Excel.Range cell = (Excel.Range)worksheet.Cells[i + 2, j + 1];

                        if (cellValue != null)
                        {
                            // Check if column is HwDt (you can also use column index if it's fixed, e.g., if j == 4)
                            string columnName = dataGridView1.Columns[j].HeaderText.ToLower();

                            if (columnName == "hwdt" || columnName.Contains("date"))
                            {
                                DateTime dt;
                                if (DateTime.TryParse(cellValue.ToString(), out dt))
                                    cell.Value2 = dt.ToString("dd/MM/yyyy");  // or "yyyy-MM-dd"
                                else
                                    cell.Value2 = cellValue.ToString(); // fallback
                            }
                            else
                            {
                                cell.Value2 = cellValue.ToString();
                            }
                        }
                        else
                        {
                            cell.Value2 = "";
                        }

                        cell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    }
                }


                worksheet.Columns.AutoFit();
                if (File.Exists(saveDialog.FileName))
                {
                    File.Delete(saveDialog.FileName);
                }

                workbook.SaveAs(saveDialog.FileName);

                excelApp.Visible = true;
                excelApp.Workbooks.Open(saveDialog.FileName);

                //MessageBox.Show("Excel exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // workbook aur worksheet ko release karne ki zarurat nahi agar file open karke dikhani ho
                if (excelApp != null)
                {
                    // Don't quit the app because we're showing it to the user
                    ReleaseObject(excelApp);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            ShowData();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            ShowData();
        }

        private void chkAllDates_CheckedChanged(object sender, EventArgs e)
        {
            ShowData();
        }


 
 

        
    }
}