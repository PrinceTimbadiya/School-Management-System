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
using System.Globalization;

namespace School_Management_System
{
    public partial class FrmStudent : UserControl
    {
        public static FrmStudent instance;

        SqlConnection Con;
        SqlCommand Cmd;
        SqlDataAdapter DA;
        SqlDataReader Dtr;
        DataSet DS;
        int MaxNum;
        Boolean add, edit;
        String photopath;
        Boolean photochange;
        //static String[] DeptName;
        String CityID;
        String cl;

        public FrmStudent()
        {
            InitializeComponent();
            instance = this;
            Con = new SqlConnection(global.ConnectionString);
            add = false;
            edit = false;
            PBPhoto.Enabled = false;
            txtReadOnly(true);
            btnsave.Enabled = false;
            txtLeavingReason.ReadOnly = true;
            dtpBirthDate.MaxDate = DateTime.Today;
            dtpBirthDate.Value = DateTime.Today;
            dtpJoinDate.MaxDate = DateTime.Today;
            dtpJoinDate.Value = DateTime.Today;
            AddDataInCombo();
        }

        public void resize1()
        {
            this.Width = global.screenWidth - 230;
            this.Height = global.screenHeight - 20;
        }

        private void txtReadOnly(Boolean a)
        {
            txtSName.ReadOnly = true;
            cbGender.Enabled = !a;
            cbStatus.Enabled = !a;
            dtpBirthDate.Enabled = !a;
            dtpJoinDate.Enabled = !a;
            cbDeptCode.Enabled = !a;
            cbCurrentClass.Enabled = !a;
            cbTmp_Class.Enabled = !a;
            cbTmp_Dept.Enabled = !a;
            cbType.Enabled = !a;
            cbCategory.Enabled = !a;

            TextBox[] tx = { txtSName, txtName, txtFName, txtGFName, txtMName, txtAddress, txtCity, txtTaluko, txtDistrict, txtLastSchool, txtBirthPlace, txtBirthDistrict, txtReligious, txtCast, txtSubCast, txtSNameG, txtNameG, txtFNameG, txtGFNameG, txtMNameG, txtAddressG, txtCityG, txtTalukoG, txtDistrictG, txtLastSchoolG, txtBirthPlaceG, txtBirthDistrictG, txtReligiousG, txtCastG, txtSubCastG, txtHPhone, txtFPhone, txtPPhone, txtAdhar, txtDiseCode, txtGRNo, txtRollNo, txtJoiningStarted };
            for (int x = 0; x < 38; x++)
            {
                tx[x].ReadOnly = a;
                tx[x].BackColor = Color.White;
            }
        }

        private void clear()
        {
            TextBox[] tx = { txtSName, txtName, txtFName, txtGFName, txtMName, txtAddress, txtCity, txtTaluko, txtDistrict, txtLastSchool, txtBirthPlace, txtBirthDistrict, txtReligious, txtCast, txtSubCast, txtSNameG, txtNameG, txtFNameG, txtGFNameG, txtMNameG, txtAddressG, txtCityG, txtTalukoG, txtDistrictG, txtLastSchoolG, txtBirthPlaceG, txtBirthDistrictG, txtReligiousG, txtCastG, txtSubCastG, txtHPhone, txtFPhone, txtPPhone, txtAdhar, txtDiseCode, txtReDgNo, txtDivision, txtGRNo, txtRollNo, txtJoiningStarted, txtLeaveDate, txtLeavingReason };
            for (int x = 0; x < 42; x++)
            {
                tx[x].Text = "";
            }
            dtpJoinDate.Value = DateTime.Today;
            dtpBirthDate.Value = DateTime.Today;
            cbDeptCode.SelectedIndex = -1;
            cbGender.SelectedIndex = -1;
            cbStatus.SelectedIndex = -1;
            cbCurrentClass.SelectedIndex = -1;
            cbType.SelectedIndex = -1;
            cbTmp_Class.SelectedIndex = -1;
            cbTmp_Dept.SelectedIndex = -1;
            cbCategory.SelectedIndex = -1;
            //dtpBirthDate.Value = DateTime.Today;
            PBPhoto.Image = null;
        }

        //private bool ValidatePersonalDetails()
        //{
        //    // English aur Gujarati dono controls + unke naam
        //    Control[] ctrls = { 
        //        txtSName, txtName, txtFName, txtMName, txtAddress, txtCity, txtBirthPlace, txtAdhar, cbGender, txtHPhone,
        //        txtSNameG, txtNameG, txtFNameG, txtMNameG, txtAddressG, txtCityG, txtBirthPlaceG
        //    };

        //    string[] names = { 
        //        "Surname (Personal)", "Name (Personal)", "Father's Name", "Mother's Name", "Address", "City", "Birth Place", "Adhar", "Gender", "Home Phone",
        //        "Gujarati Surname", "Gujarati Name", "Gujarati Father's Name", "Gujarati Mother's Name", "Gujarati Address", "Gujarati City", "Gujarati Birth Place"
        //    };

        //    for (int i = 0; i < ctrls.Length; i++)
        //    {
        //        if (string.IsNullOrWhiteSpace(ctrls[i].Text))
        //        {
        //            MessageBox.Show(names[i] + " is required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            ctrls[i].Focus();
        //            return false;
        //        }
        //    }

        //    // Date check (alag se)
        //    if (string.IsNullOrWhiteSpace(dtpBirthDate.Text))
        //    {
        //        MessageBox.Show("Birth Date is required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        dtpBirthDate.Focus();
        //        return false;
        //    }

        //    return true;
        //}

        private bool ValidatePersonalDetails()
        {
            // English aur Gujarati dono controls + unke naam
            Control[] ctrls = { 
                txtSName, txtName, txtFName, txtGFName, txtMName, txtAddress, txtCity, txtTaluko, txtDistrict,
                txtLastSchool, txtBirthPlace, txtBirthDistrict, txtReligious, txtCast, txtSubCast,

                txtSNameG, txtNameG, txtFNameG, txtGFNameG, txtMNameG, txtAddressG, txtCityG, txtTalukoG, txtDistrictG,
                txtLastSchoolG, txtBirthPlaceG, txtBirthDistrictG, txtReligiousG, txtCastG, txtSubCastG, cbCategory
            };

            string[] names = { 
                "Surname", "Name", "Father's Name", "Grandfather's Name", "Mother's Name", "Address", "City", "Taluko", "District",
                "Last School", "Birth Place", "Birth District", "Religion", "Caste", "Sub Caste",

                "Gujarati Surname", "Gujarati Name", "Gujarati Father's Name", "Gujarati Grandfather's Name", "Gujarati Mother's Name",
                "Gujarati Address", "Gujarati City", "Gujarati Taluko", "Gujarati District",
                "Gujarati Last School", "Gujarati Birth Place", "Gujarati Birth District", "Gujarati Religion", "Gujarati Caste", "Gujarati Sub Caste", "Category"
            };

            for (int i = 0; i < ctrls.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(ctrls[i].Text))
                {
                    MessageBox.Show(names[i] + " is required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ctrls[i].Focus();
                    return false;
                }
            }

            // Date check (alag se)
            if (string.IsNullOrWhiteSpace(dtpBirthDate.Text))
            {
                MessageBox.Show("Birth Date is required!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpBirthDate.Focus();
                return false;
            }

            return true;
        }

        private void UpdateFullNameLabel()
        {
            lblFullName.Text = txtSName.Text.Trim() + " " + txtName.Text.Trim() + " " + txtFName.Text.Trim();
            lblFullName1.Text = txtSName.Text.Trim() + " " + txtName.Text.Trim() + " " + txtFName.Text.Trim();
        }



        private void FrmStudent_Load(object sender, EventArgs e)
        {
            resize1();

            //Button Control (Enable/Disable)
            btnadd.Enabled = true;
            btnedit.Enabled = false;
            btndelete.Enabled = false;
            btnsave.Enabled = false;
            btncancel.Enabled = false;
            clear();
            txtReadOnly(true);
            btnadd.Focus();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            clear();
            add = true;
            edit = false;
            PBPhoto.Enabled = true;
            PBPhoto.Image = null;
            btnadd.Enabled = false;
            btnsave.Enabled = true;
            btnedit.Enabled = false;
            btndelete.Enabled = false;
            btncancel.Enabled = true;
            btnSearch.Enabled = false;
            btnDocument.Enabled = false;
            txtReadOnly(false);
            cbStatus.SelectedIndex = 0;
            dtpJoinDate.Value = DateTime.Today;
            dtpBirthDate.Value = DateTime.Today;   
            txtSName.Focus();
        }

        private void StoreEToG()
        {

            TextBox[] txtb = { txtSName, txtName, txtFName, txtGFName, txtMName, txtAddress, txtCity, txtTaluko, txtDistrict, txtLastSchool, txtBirthPlace, txtBirthDistrict, txtReligious, txtCast, txtSubCast }; ;
            TextBox[] txtbgj = { txtSNameG, txtNameG, txtFNameG, txtGFNameG, txtMNameG, txtAddressG, txtCityG, txtTalukoG, txtDistrictG, txtLastSchoolG, txtBirthPlaceG, txtBirthDistrictG, txtReligiousG, txtCastG, txtSubCastG };
            Boolean flag = true;
            for (int x = 0; x < 15; x++)
            {
                flag = true;
                Con.Open();
                string nquery = "select * from EnglishToGujarati where English='" + txtb[x].Text + "';";
                Cmd = new SqlCommand(nquery, Con);
                Dtr = Cmd.ExecuteReader();
                Dtr.Read();
                //To see record is not null
                try
                {
                    String nm = Dtr[0].ToString();
                }
                catch
                {
                    flag = false;
                }
                Con.Close();
                if (flag == false)
                {
                    Con.Open();
                    string inquery = "insert into EnglishToGujarati values('" + txtb[x].Text + "',N'" + txtbgj[x].Text + "')";
                    Cmd = new SqlCommand(inquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                }
            }
        }

        private void cityLeave()
        {
            Con.Open();
            string nquery = "select GCity,Taluko,GTaluko,District,GDistrict from CityMaster where city='" + txtCity.Text + "'";
            Cmd = new SqlCommand(nquery, Con);
            Dtr = Cmd.ExecuteReader();
            Dtr.Read();
            //To see record is not null
            try
            {
                txtCityG.Text = Dtr[0].ToString();
                txtTaluko.Text = Dtr[1].ToString();
                txtTalukoG.Text = Dtr[2].ToString();
                txtDistrict.Text = Dtr[3].ToString();
                txtDistrictG.Text = Dtr[4].ToString();
            }
            catch
            {
                MaxNum = 0;
            }
            Con.Close();
        }

        public void AddDataInCombo()
        {
            // Class ID + Class Name Combo fill
            cbTmp_Class.Items.Clear();
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
                cbTmp_Class.Items.Add(itemText);
            }
            Dtr.Close();
            Con.Close();


            // Dept ID + Name Combo fill
            cbTmp_Dept.Items.Clear();
            Con.Open();
            string deptQuery = "SELECT DeptId, DeptName FROM DepartmentMaster";
            Cmd = new SqlCommand(deptQuery, Con);
            Dtr = Cmd.ExecuteReader();
            while (Dtr.Read())
            {
                string itemText = Dtr["DeptId"].ToString() + " - " + Dtr["DeptName"].ToString();
                cbTmp_Dept.Items.Add(itemText);
            }
            Dtr.Close();
            Con.Close();


            Con.Open();
            //String dept;
            string nquery = "select DeptID from DepartmentMaster";
            Cmd = new SqlCommand(nquery, Con);
            Dtr = Cmd.ExecuteReader();
            cbDeptCode.Items.Clear();
            //To see record is not null
            while (Dtr.Read())
            {
                cbDeptCode.Items.Add(Dtr[0].ToString());
            }
            Con.Close();

            Con.Open();
            string query = "select FeesTitle from FeesType";
            Cmd = new SqlCommand(query, Con);
            Dtr = Cmd.ExecuteReader();
            cbType.Items.Clear();
            //To see record is not null
            while (Dtr.Read())
            {
                cbType.Items.Add(Dtr[0].ToString());
            }
            Con.Close();

            //Con.Open();
            //string query1 = "select DISTINCT Tmp_Dept from StudentMaster";
            //Cmd = new SqlCommand(query1, Con);
            //Dtr = Cmd.ExecuteReader();
            //cbTmp_Dept.Items.Clear();
            ////To see record is not null
            //while (Dtr.Read())
            //{
            //    cbTmp_Dept.Items.Add(Dtr[0].ToString());
            //}
            //Con.Close();

            //Con.Open();
            //string query2 = "select DISTINCT Tmp_Class from StudentMaster";
            //Cmd = new SqlCommand(query2, Con);
            //Dtr = Cmd.ExecuteReader();
            //cbTmp_Class.Items.Clear();
            ////To see record is not null
            //while (Dtr.Read())
            //{
            //    cbTmp_Class.Items.Add(Dtr[0].ToString());
            //}
            //Con.Close();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (txtSName.Text != "" && txtReDgNo.Text != "")
            {
                add = false;
                edit = true;
                PBPhoto.Enabled = true;
                btnadd.Enabled = false;
                btnedit.Enabled = false;
                btnsave.Enabled = true;
                btndelete.Enabled = false;
                btncancel.Enabled = true;
                btnSearch.Enabled = false;
                txtReadOnly(false);
                btnDocument.Enabled = true;
                txtSName.Focus();
                if (txtSName.Text == "")
                    txtReDgNo.Text = "";
            }
            else
            {
                MessageBox.Show("Search Data To Edit...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (!(add == true || edit == true))
            {
                MessageBox.Show("Select Any Mode", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate only personal tab fields (school details optional)
            if (!ValidatePersonalDetails())
                return;

            if (add == true || edit == true)
            {
            //    if (txtSName.Text != "" && txtName.Text != "" && txtFName.Text != "" && txtGFName.Text != "" && txtMName.Text != "" && txtAddress.Text != "" && txtCity.Text != "" && txtTaluko.Text != "" && txtDistrict.Text != "" && txtLastSchool.Text != "" && txtBirthPlace.Text != "" && txtBirthDistrict.Text != "" && txtReligious.Text != "" && txtCast.Text != "" && txtSubCast.Text != "" && txtSNameG.Text != "" && txtNameG.Text != "" && txtFNameG.Text != "" && txtGFNameG.Text != "" && txtMNameG.Text != "" && txtAddressG.Text != "" && txtCityG.Text != "" && txtTalukoG.Text != "" && txtDistrictG.Text != "" && txtLastSchoolG.Text != "" && txtBirthPlaceG.Text != "" && txtBirthDistrictG.Text != "" && txtReligiousG.Text != "" && txtCastG.Text != "" && txtSubCastG.Text != "" && cbGender.Text != "" && txtHPhone.Text != "" && txtFPhone.Text != "" && txtPPhone.Text != "" && txtAdhar.Text != "" && txtDiseCode.Text != "" &&  cbDeptCode.Text != "" && txtDivision.Text != "" && txtGRNo.Text != "" && txtRollNo.Text != "" && dtpJoinDate.Text != "" && dtpBirthDate.Value.ToString("yyyy-MM-dd") != "" && cbCurrentClass.Text != "" && txtJoiningStarted.Text != "" && cbType.Text != "" && cbStatus.Text != "")
            //    {
                    if (add == true)
                    {
                        int CityID = 0;
                        Con.Open();
                        string nquery1 = "select CityID from CityMaster where City='" + txtCity.Text + "';";
                        Cmd = new SqlCommand(nquery1, Con);
                        Dtr = Cmd.ExecuteReader();
                        if (Dtr.Read())
                        {
                            CityID = Int32.Parse(Dtr[0].ToString());
                            Con.Close();
                        }
                        else
                        {
                            //Con.Close();
                            ////Auto number
                            //Con.Open();
                            //string nquery2 = "select MAX(CityID) from CityMaster;";
                            //Cmd = new SqlCommand(nquery2, Con);
                            //Dtr = Cmd.ExecuteReader();
                            //Dtr.Read();
                            ////To see record is not null
                            //try
                            //{
                            //    MaxNum = Int32.Parse(Dtr[0].ToString());
                            //}
                            //catch
                            //{
                            //    MaxNum = 0;
                            //}
                            //Con.Close();
                            //MaxNum = MaxNum + 1;

                            if (Con.State == ConnectionState.Open)
                            {
                                Con.Close();
                            }

                            Con.Open();
                            string inquery = "insert into CityMaster(City,GCity,Taluko,GTaluko,District,GDistrict,UserId) values('" + txtCity.Text + "',N'" + txtCityG.Text + "','" + txtTaluko.Text + "',N'" + txtTalukoG.Text + "','" + txtDistrict.Text + "',N'" + txtDistrictG.Text + "','" + global.User + "');";
                            Cmd = new SqlCommand(inquery, Con);
                            Cmd.ExecuteNonQuery();
                            Con.Close();
                            CityID = MaxNum;
                        }

                        //Auto number
                        Con.Open();
                        string nquery = "select MAX(ReDgNo) from studentMaster";
                        Cmd = new SqlCommand(nquery, Con);
                        Dtr = Cmd.ExecuteReader();
                        Dtr.Read();
                        //To see record is not null
                        try
                        {
                            MaxNum = Int32.Parse(Dtr[0].ToString());
                        }
                        catch
                        {
                            MaxNum = 0;
                        }
                        Con.Close();
                        MaxNum = MaxNum + 1;
                        txtReDgNo.Text = MaxNum.ToString();

                        String status;
                        if (cbStatus.Text == "Active")
                            status = "A";
                        else
                            status = "D";
                        String gender;
                        if (cbGender.Text == "Male")
                            gender = "M";
                        else
                            gender = "F";
                        string squery = "select * from StudentMaster where ReDgNo=" + txtReDgNo.Text + "";
                        DA = new SqlDataAdapter(squery, Con);
                        DS = new DataSet();
                        DA.Fill(DS);
                        int rowcount = 0;
                        rowcount = DS.Tables[0].Rows.Count;
                        if (rowcount == 0)
                        {
                            string grNo = string.IsNullOrWhiteSpace(txtGRNo.Text) ? "NULL" : txtGRNo.Text;
                            string rollNo = string.IsNullOrWhiteSpace(txtRollNo.Text) ? "NULL" : txtRollNo.Text;
                            string redgNo = string.IsNullOrWhiteSpace(txtReDgNo.Text) ? "NULL" : txtReDgNo.Text;

                            // DeptCode bhi check karo
                            string deptCode = string.IsNullOrWhiteSpace(cbDeptCode.Text) ? "NULL" : "'" + cbDeptCode.Text + "'";

                            Con.Open();
                            //string insquery = "insert into StudentMaster (SurName,Name,FatherName,GFatherName,MotherName,Address,CityID,LastSchool,BirthPlace,BirthDistrict,Religious,Cast,SubCast,SurNameGJ,NameGj,FatherNameGJ,GFatherNameGJ,MotherNameGJ,AddressGJ,LastSchoolGJ,BirthPlaceGJ,BirthDistrictGJ,ReligiousGJ,CastGJ,SubCastGJ,Gender,HomePhone,FatherPhone,PersonalPhone,AdharCard,DiseNo,RedgNo,DeptCode,Division,GRNumber,RollNo,JoinDate,BirthDate,CurrentClass,JoiningStarted,Type,Status,Tmp_Dept,Tmp_Class) values('" + txtSName.Text + "','" + txtName.Text + "','" + txtFName.Text + "','" + txtGFName.Text + "','" + txtMName.Text + "','" + txtAddress.Text + "','" + CityID + "','" + txtLastSchool.Text + "','" + txtBirthPlace.Text + "','" + txtBirthDistrict.Text + "','" + txtReligious.Text + "','" + txtCast.Text + "','" + txtSubCast.Text + "',N'" + txtSNameG.Text + "',N'" + txtNameG.Text + "',N'" + txtFNameG.Text + "',N'" + txtGFNameG.Text + "',N'" + txtMNameG.Text + "',N'" + txtAddressG.Text + "',N'" + txtLastSchoolG.Text + "',N'" + txtBirthPlaceG.Text + "',N'" + txtBirthDistrictG.Text + "',N'" + txtReligiousG.Text + "',N'" + txtCastG.Text + "',N'" + txtSubCastG.Text + "','" + gender + "','" + txtHPhone.Text + "','" + txtFPhone.Text + "','" + txtPPhone.Text + "','" + txtAdhar.Text + "','" + txtDiseCode.Text + "'," + txtReDgNo.Text + ",'" + cbDeptCode.Text + "','" + txtDivision.Text + "', " + txtGRNo.Text + "," + txtRollNo.Text + ",'" + dtpJoinDate.Value.ToString("yyyy-MM-dd") + "','" + dtpBirthDate.Value.ToString("yyyy-MM-dd") + "','" + cbCurrentClass.Text + "','" + txtJoiningStarted.Text + "','" + cbType.Text + "','" + status + "','" + cbTmp_Dept.Text + "','" + cbTmp_Class.Text + "')";
                            string insquery = "insert into StudentMaster (SurName,Name,FatherName,GFatherName,MotherName,Address,CityID,LastSchool,BirthPlace,BirthDistrict,Religious,Cast,SubCast,SurNameGJ,NameGj,FatherNameGJ,GFatherNameGJ,MotherNameGJ,AddressGJ,LastSchoolGJ,BirthPlaceGJ,BirthDistrictGJ,ReligiousGJ,CastGJ,SubCastGJ,Gender,HomePhone,FatherPhone,PersonalPhone,AdharCard,DiseNo,ReDgNo,DeptCode,Division,GRNumber,RollNo,JoinDate,BirthDate,CurrentClass,JoiningStarted,Type,Status,Tmp_Dept,Tmp_Class,Category) values('" + txtSName.Text + "','" + txtName.Text + "','" + txtFName.Text + "','" + txtGFName.Text + "','" + txtMName.Text + "','" + txtAddress.Text + "'," + CityID + ",'" + txtLastSchool.Text + "','" + txtBirthPlace.Text + "','" + txtBirthDistrict.Text + "','" + txtReligious.Text + "','" + txtCast.Text + "','" + txtSubCast.Text + "',N'" + txtSNameG.Text + "',N'" + txtNameG.Text + "',N'" + txtFNameG.Text + "',N'" + txtGFNameG.Text + "',N'" + txtMNameG.Text + "',N'" + txtAddressG.Text + "',N'" + txtLastSchoolG.Text + "',N'" + txtBirthPlaceG.Text + "',N'" + txtBirthDistrictG.Text + "',N'" + txtReligiousG.Text + "',N'" + txtCastG.Text + "',N'" + txtSubCastG.Text + "','" + gender + "','" + txtHPhone.Text + "','" + txtFPhone.Text + "','" + txtPPhone.Text + "','" + txtAdhar.Text + "','" + txtDiseCode.Text + "'," + redgNo + "," + deptCode + ",'" + txtDivision.Text + "'," + grNo + "," + rollNo + ",'" + dtpJoinDate.Value.ToString("yyyy-MM-dd") + "','" + dtpBirthDate.Value.ToString("yyyy-MM-dd") + "','" + cbCurrentClass.Text + "','" + txtJoiningStarted.Text + "','" + cbType.Text + "','" + status + "','" + cbTmp_Dept.Text + "','" + cbTmp_Class.Text + "', '" + cbCategory.Text  + "')";
                            Cmd = new SqlCommand(insquery, Con);
                            Cmd.ExecuteNonQuery();
                            Con.Close();
                            //save photo
                            if (photochange == true)
                            {
                                string photoname = txtReDgNo.Text + ".jpg";
                                File.Copy(photopath, Path.Combine(@"" + global.projectpath + @"StudentPhoto\", photoname), true);
                            }
                            if (cbStatus.Text == "Deactive")
                            {
                                DateTime dt = DateTime.Now;
                                Con.Open();
                                string upqry = "update StudentMaster set LeaveDate='" + dt + "' LeavingReason='" + txtLeavingReason.Text + "' where ReDgNo=" + txtReDgNo.Text + "";
                                Cmd = new SqlCommand(upqry, Con);
                                Cmd.ExecuteNonQuery();
                                Con.Close();
                            }
                            if (cbStatus.Text == "Active")
                            {
                                Con.Open();
                                Cmd = new SqlCommand("update StudentMaster set LeaveDate='" + null + "',LeavingReason='" + null + "' where ReDgNo=" + txtReDgNo.Text + "", Con);
                                Cmd.ExecuteNonQuery();
                                Con.Close();
                            }
                            AddDataInCombo();
                            MessageBox.Show("Record Added...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //FrmDashboard.instance.studentCount();
                            StoreEToG();
                            btncancel.PerformClick();
                            PBPhoto.Image = null;
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("Duplicate entry...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    if (edit == true)
                    {
                        String CityID = "0";
                        Con.Open();
                        string nquery = "select CityId from CityMaster where City='" + txtCity.Text + "';";
                        Cmd = new SqlCommand(nquery, Con);
                        Dtr = Cmd.ExecuteReader();
                        Dtr.Read();
                        //To see record is not null
                        try
                        {
                            CityID = Dtr[0].ToString();
                        }
                        catch
                        {
                        }

                        Con.Close();
                        String status;
                        if (cbStatus.Text == "Active")
                            status = "A";
                        else
                            status = "D";
                        String gender;
                        if (cbGender.Text == "Male")
                            gender = "M";
                        else
                            gender = "F";
                        Con.Open();
                        Cmd = new SqlCommand("update StudentMaster set SurName='" + txtSName.Text + "',Name='" + txtName.Text + "',FatherName='" + txtFName.Text + "',GFatherName='" + txtGFName.Text + "',MotherName='" + txtMName.Text + "',Address='" + txtAddress.Text + "',CityID=" + CityID + ",LastSchool='" + txtLastSchool.Text + "',BirthPlace='" + txtBirthPlace.Text + "',BirthDistrict='" + txtBirthDistrict.Text + "',Religious='" + txtReligious.Text + "',Cast='" + txtCast.Text + "',SubCast='" + txtSubCast.Text + "',SurNameGJ=N'" + txtSNameG.Text + "',NameGj=N'" + txtNameG.Text + "',FatherNameGJ=N'" + txtFNameG.Text + "',GFatherNameGJ=N'" + txtGFNameG.Text + "',MotherNameGJ=N'" + txtMNameG.Text + "',AddressGJ=N'" + txtAddressG.Text + "',LastSchoolGJ=N'" + txtLastSchoolG.Text + "',BirthPlaceGJ=N'" + txtBirthPlaceG.Text + "',BirthDistrictGJ=N'" + txtBirthDistrictG.Text + "',ReligiousGJ=N'" + txtReligiousG.Text + "',CastGJ=N'" + txtCastG.Text + "',SubCastGJ=N'" + txtSubCastG.Text + "',Gender='" + gender + "',HomePhone='" + txtHPhone.Text + "',FatherPhone='" + txtFPhone.Text + "',PersonalPhone='" + txtPPhone.Text + "',AdharCard=" + txtAdhar.Text + ",DiseNo=" + txtDiseCode.Text + ",DeptCode='" + cbDeptCode.Text + "',Division='" + txtDivision.Text + "', GRNumber='" + txtGRNo.Text + "',RollNo='" + txtRollNo.Text + "',JoinDate='" + dtpJoinDate.Value.ToString("yyyy-MM-dd") + "',BirthDate='" + dtpBirthDate.Text + "',CurrentClass='" + cbCurrentClass.Text + "',JoiningStarted='" + txtJoiningStarted.Text + "',Type='" + cbType.Text + "',Status='" + status + "',Tmp_Dept='" + cbTmp_Dept.Text + "',Tmp_Class='" + cbTmp_Class.Text + "',Category='" + cbCategory.Text + "' where ReDgNo=" + txtReDgNo.Text + "", Con);
                        Cmd.ExecuteNonQuery();
                        Con.Close();
                        //save image
                        string photoname = txtReDgNo.Text + ".jpg";
                        if (photochange == true)
                        {
                            PBPhoto.Image = null;
                            File.Copy(photopath, Path.Combine(@"" + global.projectpath + "StudentPhoto", photoname), true);
                            PBPhoto.ImageLocation = null;
                            photochange = false;
                        }
                        if (cbStatus.Text == "Deactive")
                        {
                            DateTime dt = DateTime.Now;
                            Con.Open();
                            Cmd = new SqlCommand("update StudentMaster set LeaveDate='" + dt + "',LeavingReason='" + txtLeavingReason.Text + "' where ReDgNo='" + txtReDgNo.Text + "'", Con);
                            Cmd.ExecuteNonQuery();
                            Con.Close();
                        }
                        if (cbStatus.Text == "Active")
                        {
                            Con.Open();
                            Cmd = new SqlCommand("update StudentMaster set LeaveDate='" + null + "',LeavingReason='" + null + "' where ReDgNo=" + txtReDgNo.Text + "", Con);
                            Cmd.ExecuteNonQuery();
                            Con.Close();
                        }
                        StoreEToG();
                        btncancel.PerformClick();
                        //FrmDashboard.instance.studentCount();
                        AddDataInCombo();
                        MessageBox.Show("Record Updated...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                //}
                //else
                //{
                //    MessageBox.Show("Fill the Data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
            else
            {
                MessageBox.Show("Select Any Mode", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (txtReDgNo.Text != "")
            {
                 DialogResult ds = MessageBox.Show("Do You Want To Delete ?","Message",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                 if (ds == DialogResult.Yes)
                 {
                     Con.Open();
                     string dquery = "delete from StudentMaster where ReDgNo=" + txtReDgNo.Text + "";
                     Cmd = new SqlCommand(dquery, Con);
                     Cmd.ExecuteNonQuery();
                     Con.Close();
                     string photopath = @"" + global.projectpath + "StudentPhoto" + txtReDgNo.Text + ".jpg";
                     File.Delete(photopath);
                     PBPhoto.Image = null;
                     clear();
                     //FrmDashboard.instance.studentCount();
                     btncancel.PerformClick();
                 }
            }
            else
            {
                MessageBox.Show("Nothing selected...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void PBPhoto_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                photopath = open.FileName;
                PBPhoto.ImageLocation = photopath;
                photochange = true;
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            add = false;
            edit = false;
            btnadd.Enabled = true;
            btnedit.Enabled = false;
            btnSearch.Enabled = true;
            btndelete.Enabled = false;
            btnsave.Enabled = false;
            btncancel.Enabled = false;
            txtReadOnly(true);
            txtReDgNo.ReadOnly = false;
            PBPhoto.Enabled = false;
            clear();
            btnadd.Focus();
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStatus.Text == "Active")
            {
                txtLeavingReason.ReadOnly = true;
                txtLeaveDate.ReadOnly = true;
            }
            else
            {
                txtLeavingReason.ReadOnly = false;
                txtLeaveDate.ReadOnly = false;
            }
        }

        private void btnDocument_Click(object sender, EventArgs e)
        {
            if (txtReDgNo.Text != "")
            {
                global.Form = "StudentMaster";
                //FrmStudentDoc fsd = new FrmStudentDoc();
                //fsd.Show();
            }
            else
            {
                MessageBox.Show("No Record Selected...");
            }
        }

        private void dtpBirthDate_ValueChanged(object sender, EventArgs e)
        {
            //double age;
            //TimeSpan t = DateTime.Today - dtpBirthDate.Value;
            //age = Math.Round(t.TotalDays / 365, 0);
            //lblAge.Text = "Age : " + age.ToString();

            DateTime birth = dtpBirthDate.Value;
            DateTime today = DateTime.Today;

            // Normal age
            int age = today.Year - birth.Year;
            if (today < birth.AddYears(age))
                age--;

            // School Rule: agar birth ka month June ya uske baad hai → ek saal kam
            if (birth.Month > 5)
                age--;

            if (age < 0)
                age = 0;

            lblAge.Text = "Age : " + age;


            Con.Close();
            //Add current class using age
            int a = 1;
            Con.Open();
            string nquery = "select ClassName from ClassMaster where MinAge<='" + age + "' AND DeptID='" + cbDeptCode.Text + "';";
            Cmd = new SqlCommand(nquery, Con);
            Dtr = Cmd.ExecuteReader();
            cbCurrentClass.Items.Clear();
            //To see record is not null
            while (a != 0)
            {
                try
                {
                    Dtr.Read();
                    cbCurrentClass.Items.Add(Dtr[0].ToString());
                }
                catch
                {
                    a = 0;
                }
            }
            Con.Close();
        }

        private void cbDeptCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (global.Search == false)
            {
                Con.Open();
                string nquery = "select DeptName from DepartmentMaster where DeptID='" + cbDeptCode.Text + "';";
                Cmd = new SqlCommand(nquery, Con);
                Dtr = Cmd.ExecuteReader();
                //To see record is not null
                try
                {
                    Dtr.Read();
                    txtDivision.Text = Dtr[0].ToString();
                }
                catch
                {

                }
                Con.Close();
                dtpBirthDate_ValueChanged(sender, e);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            global.formname = "FrmStudent";
            FrmSearch fm = new FrmSearch();
            fm.Show();
        }

        private void txtReDgNo_TextChanged(object sender, EventArgs e)
        {
            if (global.Search == true)
            {

                btnadd.Enabled = true;
                btnedit.Enabled = true;
                btnSearch.Enabled = true;
                btndelete.Enabled = true;
                btncancel.Enabled = true;
                btnsave.Enabled = false;

                TextBox[] tx = { txtReDgNo, txtGRNo, txtSName, txtSNameG, txtName, txtNameG, txtFName, txtFNameG, txtGFName, txtGFNameG, txtMName, txtMNameG, txtAddress, txtAddressG, txtReDgNo, txtLastSchool, txtLastSchoolG, txtBirthPlace, txtBirthPlaceG, txtBirthDistrict, txtBirthDistrictG, txtReligious, txtReligiousG, txtCast, txtCastG, txtSubCast, txtSubCastG, txtAdhar, txtDiseCode, txtHPhone, txtFPhone, txtPPhone };
                Con.Open();
                string nquery = "select * from studentMaster where ReDgNo=" + txtReDgNo.Text + "";
                Cmd = new SqlCommand(nquery, Con);
                Dtr = Cmd.ExecuteReader();
                Dtr.Read();
                //try
                //{
                for (int a = 1; a <= 31; a++)
                {
                    if (a != 14)
                        tx[a].Text = Dtr[a].ToString();
                    else
                        CityID = Dtr[a].ToString();
                }

                if (Dtr[32].ToString() == "M")
                    cbGender.Text = "Male";
                else
                    cbGender.Text = "Female";

                if (Dtr[33].ToString() == "A")
                    cbStatus.Text = "Active";
                else
                    cbStatus.Text = "Deactive";
                cbDeptCode.Text = Dtr[34].ToString();
                txtDivision.Text = Dtr[35].ToString();
                txtRollNo.Text = Dtr[36].ToString();
                txtJoiningStarted.Text = Dtr[40].ToString();
                cbType.Text = Dtr[41].ToString();
                txtLeaveDate.Text = Dtr[42].ToString();
                txtLeavingReason.Text = Dtr[43].ToString();
                cbTmp_Class.Text = Dtr[45].ToString();
                cl = Dtr[39].ToString();
                cbTmp_Dept.Text = Dtr[46].ToString();
                cbCategory.Text = Dtr[47].ToString();

                //string dt = Dtr[37].ToString(); // Read from database
                //DateTime parsedDate;
                //if (DateTime.TryParse(dt, out parsedDate))
                //{
                //    dtpJoinDate.Format = DateTimePickerFormat.Custom;
                //    dtpJoinDate.CustomFormat = "dd/MM/yyyy";
                //    dtpJoinDate.Value = parsedDate.Date;  // Use .Date to remove time
                //}

                //String dt1 = Dtr[38].ToString();
                //dtpBirthDate.Format = DateTimePickerFormat.Custom;
                //dtpBirthDate.CustomFormat = "dd/MM/yyyy";
                //dtpBirthDate.Value = DateTime.ParseExact(dt1, "dd/MM/yyyy", null);

                // --- SAFER date parsing for JoinDate (Dtr[37]) ---
                string dt = Dtr[37] == DBNull.Value ? "" : Dtr[37].ToString().Trim();
                DateTime parsedDate;
                if (!string.IsNullOrEmpty(dt) && DateTime.TryParse(dt, out parsedDate))
                {
                    dtpJoinDate.Format = DateTimePickerFormat.Custom;
                    dtpJoinDate.CustomFormat = "dd/MM/yyyy";
                    dtpJoinDate.Value = parsedDate.Date;
                }
                else
                {
                    // if invalid or empty, keep default or fallback to Today
                    dtpJoinDate.Format = DateTimePickerFormat.Custom;
                    dtpJoinDate.CustomFormat = "dd/MM/yyyy";
                    dtpJoinDate.Value = DateTime.Today; // or don't change if you prefer
                }

                // --- SAFER date parsing for BirthDate (Dtr[38]) ---
                string dt1 = Dtr[38] == DBNull.Value ? "" : Dtr[38].ToString().Trim();
                dtpBirthDate.Format = DateTimePickerFormat.Custom;
                dtpBirthDate.CustomFormat = "dd/MM/yyyy";

                DateTime birthDate = DateTime.Today;  // <-- default value set
                bool parsed = false;

                if (!string.IsNullOrEmpty(dt1) && dt1 != "0000-00-00")
                {
                    // try common formats your DB might store
                    string[] formats = new string[] {
                        "dd/MM/yyyy",
                        "d/M/yyyy",
                        "yyyy-MM-dd",
                        "yyyy/MM/dd",
                        "MM/dd/yyyy",
                        "dd-MM-yyyy"
                    };

                    parsed = DateTime.TryParseExact(dt1, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);

                    // fallback to general parse (handles culture-specific or timestamped values)
                    if (!parsed)
                        parsed = DateTime.TryParse(dt1, out birthDate);
                }

                if (parsed)
                    dtpBirthDate.Value = birthDate.Date;
                else
                    dtpBirthDate.Value = DateTime.Today; // or leave as-is


                Con.Close();
                //}

                //catch { Con.Close(); }
                cbCurrentClass.Text = cl;

                string exeLocation = AppDomain.CurrentDomain.BaseDirectory;
                string photopath = exeLocation + @"StudentPhoto\" + txtReDgNo.Text + ".jpg";
                PBPhoto.ImageLocation = photopath;
                try
                {
                    Con.Open();
                    string nquery1 = "select City,GCity,Taluko,GTaluko,District,GDistrict from CityMaster where cityID='" + CityID + "'";
                    Cmd = new SqlCommand(nquery1, Con);
                    Dtr = Cmd.ExecuteReader();
                    Dtr.Read();
                    //To see record is not null
                    try
                    {
                        txtCity.Text = Dtr[0].ToString();
                        txtCityG.Text = Dtr[1].ToString();
                        txtTaluko.Text = Dtr[2].ToString();
                        txtTalukoG.Text = Dtr[3].ToString();
                        txtDistrict.Text = Dtr[4].ToString();
                        txtDistrictG.Text = Dtr[5].ToString();
                    }
                    catch
                    {
                        MaxNum = 0;
                    }
                }
                catch { }
                Con.Close();

                global.Search = false;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtSName.Text != "" && txtReDgNo.Text != "")
            {
                if (chkStructure.Checked == true)
                {
                    global.ReportName = "Student";
                    FrmFeeReport ffr1 = new FrmFeeReport();
                    ffr1.Show();
                }
                //else
                //{
                //    global.ReportName = "StructureStudent";
                //    FrmFeeReport ffr1 = new FrmFeeReport();
                //    ffr1.Show();
                //}

            }
            else
            {
                MessageBox.Show("Search Data For Print...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtCity_Leave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            String txtname = txt.Name;
            if (txtname == "txtCity")
                cityLeave();
            String txtvalue = txt.Text;
            String txtnameg;
            Con.Open();
            string nquery = "select Gujarati from EnglishToGujarati where English='" + txtvalue + "';";
            Cmd = new SqlCommand(nquery, Con);
            Dtr = Cmd.ExecuteReader();
            Dtr.Read();
            //To see record is not null
            try
            {
                txtnameg = Dtr[0].ToString();
            }
            catch
            {
                Con.Close();
                return;
            }
            Con.Close();
            TextBox[] txtb = { txtSName, txtName, txtFName, txtGFName, txtMName, txtAddress, txtCity, txtTaluko, txtDistrict, txtLastSchool, txtBirthPlace, txtBirthDistrict, txtReligious, txtCast, txtSubCast }; ;
            TextBox[] txtbgj = { txtSNameG, txtNameG, txtFNameG, txtGFNameG, txtMNameG, txtAddressG, txtCityG, txtTalukoG, txtDistrictG, txtLastSchoolG, txtBirthPlaceG, txtBirthDistrictG, txtReligiousG, txtCastG, txtSubCastG };
            for (int x = 0; x < 15; x++)
            {
                if (txtname == txtb[x].Name)
                    txtbgj[x].Text = txtnameg;
            }
        }

        private void txtSName_TextChanged(object sender, EventArgs e)
        {
            UpdateFullNameLabel();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            UpdateFullNameLabel();
        }

        private void txtFName_TextChanged(object sender, EventArgs e)
        {
            UpdateFullNameLabel();
        }




    }
}
