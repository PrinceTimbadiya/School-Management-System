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
    public partial class Staff : UserControl
    {
        public static Staff instance;
        SqlConnection Con;
        SqlCommand Cmd;
        SqlDataAdapter DA;
        SqlDataReader Dtr;
        DataSet DS;
        Boolean add, edit;
        String photopath;
        Boolean photochange;
        int MaxNum;

        public Staff()
        {
            InitializeComponent();
            instance = this;
            Con = new SqlConnection(global.ConnectionString);
            //global.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Project\School_Management_System\School_Management_System\school.mdf;Integrated Security=True";
            //Con = new SqlConnection(global.ConnectionString);
            add = false;
            edit = false;
            txtReadOnly(true);
            btnsave.Enabled = false;
            txtLeavingReason.ReadOnly = true;
            dtpBirthDate.MaxDate = DateTime.Today;
            dtpBirthDate.Value = DateTime.Today;
            dtpJoinDate.MaxDate = DateTime.Today;
            dtpJoinDate.Value = DateTime.Today;
            txtUserID.Text = global.User;
            PBPhoto.Enabled = false;
            AddDataInCombo();
        }

        private void Staff_Load(object sender, EventArgs e)
        {
            resize1();

            //Button Control (Enable/Disable)
            btnadd.Enabled = true;
            btnedit.Enabled = false;
            btndelete.Enabled = false;
            btnsave.Enabled = false;
            btncancel.Enabled = false;
            txtClear();
            txtReadOnly(true);
            btnadd.Focus();
        }

        public void resize1()
        {
            this.Width = global.screenWidth - 230;
            this.Height = global.screenHeight - 20;
        }

        private void txtReadOnly(Boolean a)
        {
            cbGender.Enabled = !a;
            cbStatus.Enabled = !a;
            dtpBirthDate.Enabled = !a;
            dtpJoinDate.Enabled = !a;
            cbShiftID.Enabled = !a;
            cbMarritialStatus.Enabled = !a;

            TextBox[] tx = { txtSName, txtName, txtFName, txtAddress, txtCity, txtTaluko, txtDistrict, txtReligious, txtCast, txtSubCast, txtSNameG, txtNameG, txtFNameG, txtAddressG, txtCityG, txtTalukoG, txtDistrictG, txtReligiousG, txtCastG, txtSubCastG, txtHPhone, txtPPhone, txtAdhar, txtCategory, txtPancard, txtEmail, txtBankAcNo, txtBankIFSC, txtBankName, txtUpiID, txtPenNumber, txtQulification, txtSPQulification, txtWorkTiming, txtExDetails, txtExYear, txtReference, txtRemark };
            for (int x = 0; x < 38; x++)
            {
                tx[x].ReadOnly = a;
                tx[x].BackColor = Color.White;
            }
        }

        private void txtClear()
        {
            TextBox[] tx = { txtSName, txtName, txtFName, txtAddress, txtCity, txtTaluko, txtDistrict, txtReligious, txtCast, txtSubCast, txtSNameG, txtNameG, txtFNameG, txtAddressG, txtCityG, txtTalukoG, txtDistrictG, txtReligiousG, txtCastG, txtSubCastG, txtHPhone, txtPPhone, txtAdhar, txtStaffID, txtLeavingReason, txtCategory, txtPancard, txtEmail, txtBankAcNo, txtBankIFSC, txtBankName, txtUpiID, txtPenNumber ,txtStaffID, txtQulification, txtSPQulification, txtWorkTiming, txtExDetails, txtExYear, txtReference, txtRemark };
            for (int x = 0; x < 41; x++)
            {
                tx[x].Text = "";
            }
            dtpJoinDate.Value = DateTime.Today;
            dtpBirthDate.Value = DateTime.Today;
            cbGender.SelectedIndex = -1;
            cbStatus.SelectedIndex = -1;
            cbShiftID.SelectedIndex = -1;
            cbMarritialStatus.SelectedIndex = -1;
            PBPhoto.Image = null;

            //btncancel.PerformClick();
        }

        public void AddDataInCombo()
        {
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
            Con.Close();
        }

        private bool CheckPersonalDetails()
        {
            if (txtSName.Text == "" || txtName.Text == "" || txtFName.Text == "" || cbGender.Text == "" ||
                txtQulification.Text == "" || txtSPQulification.Text == "" || cbShiftID.Text == "" || txtWorkTiming.Text == "" ||
                txtExYear.Text == "" || txtExDetails.Text == "" || txtSNameG.Text == "" || txtNameG.Text == "" ||
                txtFNameG.Text == "" || txtReference.Text == "" || txtCategory.Text == "" || txtRemark.Text == "" ||
                cbStatus.Text == "" || txtUserID.Text == "" || cbMarritialStatus.Text == "" || dtpJoinDate.Text == "" || dtpBirthDate.Text == "")
            {
                return false;
            }
            return true;
        }


        private void btnadd_Click(object sender, EventArgs e)
        {            
            txtReadOnly(false);
            add = true;
            edit = false;
            PBPhoto.Enabled = true;
            PBPhoto.Image = null;
            btnadd.Enabled = false;
            btnsave.Enabled = true;
            btnedit.Enabled = false;
            btndelete.Enabled = false;
            btnSearch.Enabled = false;
            btncancel.Enabled = true;
            btnDocument.Enabled = false;
            txtClear();
            cbStatus.SelectedIndex = 0;
            txtRemark.Text = "-";
            txtSName.Focus();
        }        

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (txtSName.Text != "" && txtStaffID.Text != "")
            {
                add = false;
                edit = true;
                PBPhoto.Enabled = true;
                btnadd.Enabled = false;
                btnedit.Enabled = false;
                btnsave.Enabled = true;
                btnDocument.Enabled = true;
                btndelete.Enabled = false;
                btnSearch.Enabled = false;
                btncancel.Enabled = true;
                txtReadOnly(false);
                txtStaffID.ReadOnly = true;
                btnDocument.Enabled = true;
                txtSName.Focus();

                if (txtSName.Text == "")
                    txtStaffID.Text = "";
            }
            else
            {
                MessageBox.Show("Search Data To Edit...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (add == true || edit == true)
            {
                //if(txtName.Text != "" && txtFName.Text != "" && txtAddress.Text != "" && txtCity.Text != "" && txtTaluko.Text != "" && txtDistrict.Text != "" && txtReligious.Text != "" && txtCast.Text != "" && txtSubCast.Text != "" && txtSNameG.Text != "" && txtNameG.Text != "" && txtFNameG.Text != "" && txtAddressG.Text != "" && txtCityG.Text != "" && txtTalukoG.Text != "" && txtDistrictG.Text != "" && txtReligiousG.Text != "" && txtCastG.Text != "" && txtSubCastG.Text != "" && cbGender.Text != "" && txtHPhone.Text != "" && txtPPhone.Text != "" && txtAdhar.Text != "" && dtpJoinDate.Text != "" && dtpBirthDate.Text != "" && txtEmail.Text != "" && txtQulification.Text != "" && txtWorkTiming.Text != "" && cbShiftID.Text != "" && txtExYear.Text != "" && txtBankAcNo.Text != "" && txtBankIFSC.Text != "" && txtBankName.Text != "" && cbMarritialStatus.Text != "" && txtUserID.Text != "")
                if (CheckPersonalDetails())
                {
                    if (add == true)
                    {
                        int CityID = 0;
                        Con.Open();
                        string nquery1 = "select CityId from CityMaster where City='" + txtCity.Text + "';";
                        Cmd = new SqlCommand(nquery1, Con);
                        Dtr = Cmd.ExecuteReader();
                        if (Dtr.Read())
                        {
                            CityID = Int32.Parse(Dtr[0].ToString());
                            Con.Close();
                        }
                        else
                        {
                            Con.Close();
                            //Auto number
                            Con.Open();
                            string nquery2 = "select MAX(CityID) from CityMaster";
                            Cmd = new SqlCommand(nquery2, Con);
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
                            txtStaffID.Text = MaxNum.ToString();
                        }
                        //Auto number
                        Con.Open();
                        string nquery = "select MAX(staffID) from staffMaster";
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
                        Con.Open();
                        string query = "SELECT ISNULL(MAX(StaffId), 0) + 1 FROM StaffMaster";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        txtStaffID.Text = cmd.ExecuteScalar().ToString();
                        Con.Close();

                        if (txtExYear.Text == "")
                            txtExYear.Text = "0";

                        String Marrid;
                        if (cbMarritialStatus.Text == "Married")
                            Marrid = "M";
                        else
                            Marrid = "U";
                        String status;
                        if (cbStatus.Text == "Active")
                            status = "A";
                        else
                            status = "D";

                        string squery = "select * from StaffMaster where StaffId='" + MaxNum + "'";
                        DA = new SqlDataAdapter(squery, Con);
                        DS = new DataSet();
                        DA.Fill(DS);
                        int rowcount = 0;
                        rowcount = DS.Tables[0].Rows.Count;
                       
                        if (rowcount == 0)
                        {
                            

                            // ShiftId (extract from "1 - Morning")
                            string fullShiftText = cbShiftID.Text;
                            string shiftId = fullShiftText.Split('-')[0].Trim();
                            Con.Open();
                            //string insertqry = "insert into StaffMaster (Surname, GSurname, Name, GName, FatherName, GFatherName, Address, GAddress, Religious, GReligious, Cast, GCast, SubCast, GSubCast, Category, CityId, PPhone, HPhone, Email, BirthDate, JoinDate, Gender, Qulification, SpecialQulification, WorkTiming, ShiftId, ExperienceDetail, ExperienceYear, Refrence, AdharcardNumber, PancardNumber, BankName, BankAcNo, BankIFSC, UPI_ID, PenNumber, MarritialStatus, LeaveDate, LeavingReason, Remark, Status, UserId) values('" + txtSName.Text + "',N'" + txtSNameG.Text + "','" + txtName.Text + "',N'" + txtNameG.Text + "','" + txtFName.Text + "',N'" + txtFNameG.Text + "','" + txtAddress.Text + "',N'" + txtAddressG.Text + "','" + txtReligious.Text + "',N'" + txtReligiousG.Text + "','" + txtCast.Text + "',N'" + txtCastG.Text + "','" + txtSubCast.Text + "',N'" + txtSubCast.Text + "','" + txtCategory.Text + "'," + CityID + ",'" + txtPPhone.Text + "','" + txtHPhone.Text + "','" + txtEmail.Text + "','" + dtpBirthDate.Value.ToString("yyyy/MM/dd") + "','" + dtpJoinDate.Value.ToString("yyyy/MM/dd") + "','" + cbGender.Text + "','" + txtQulification.Text + "','" + txtSPQulification.Text + "','" + txtWorkTiming.Text + "'," + shiftId + ",'" + txtExDetails.Text + "'," + txtExYear.Text + ",'" + txtReference.Text + "'," + txtAdhar.Text + ",'" + txtPancard.Text + "','" + txtBankName.Text + "'," + txtBankAcNo.Text + ",'" + txtBankIFSC.Text + "','" + txtUpiID.Text + "'," + txtPenNumber.Text + ",'" + Marrid + "',NULL,NULL,'" + txtRemark.Text + "','" + status + "'," + txtUserID.Text + ")";
                            //string insertqry = "insert into StaffMaster (Surname, GSurname, Name, GName, FatherName, GFatherName, Address, GAddress, Religious, GReligious, Cast, GCast, SubCast, GSubCast, Category, CityId, PPhone, HPhone, Email, BirthDate, JoinDate, Gender, Qulification, SpecialQulification, WorkTiming, ShiftId, ExperienceDetail, ExperienceYear, Refrence, AdharcardNumber, PancardNumber, BankName, BankAcNo, BankIFSC, UPI_ID, PenNumber, MarritialStatus, LeaveDate, LeavingReason, Remark, Status, UserId) values('" + txtSName.Text + "',N'" + txtSNameG.Text + "','" + txtName.Text + "',N'" + txtNameG.Text + "','" + txtFName.Text + "',N'" + txtFNameG.Text + "','" + txtAddress.Text + "',N'" + txtAddressG.Text + "','" + txtReligious.Text + "',N'" + txtReligiousG.Text + "','" + txtCast.Text + "',N'" + txtCastG.Text + "','" + txtSubCast.Text + "',N'" + txtSubCastG.Text + "','" + txtCategory.Text + "'," + CityID + ",'" + txtPPhone.Text + "','" + txtHPhone.Text + "','" + txtEmail.Text + "','" + dtpBirthDate.Value.ToString("yyyy/MM/dd") + "','" + dtpJoinDate.Value.ToString("yyyy/MM/dd") + "','" + cbGender.Text + "','" + txtQulification.Text + "','" + txtSPQulification.Text + "','" + txtWorkTiming.Text + "'," + shiftId + ",'" + txtExDetails.Text + "'," + (txtExYear.Text == "" ? "NULL" : txtExYear.Text) + ",'" + txtReference.Text + "'," + (txtAdhar.Text == "" ? "NULL" : txtAdhar.Text) + ",'" + txtPancard.Text + "','" + txtBankName.Text + "'," + (txtBankAcNo.Text == "" ? "NULL" : txtBankAcNo.Text) + ",'" + txtBankIFSC.Text + "','" + txtUpiID.Text + "'," + (txtPenNumber.Text == "" ? "NULL" : txtPenNumber.Text) + ",'" + Marrid + "',NULL,NULL,'" + txtRemark.Text + "','" + status + "'," + txtUserID.Text + ")";
                            string insertqry = "insert into StaffMaster (Surname, GSurname, Name, GName, FatherName, GFatherName, Address, GAddress, Religious, GReligious, Cast, GCast, SubCast, GSubCast, Category, CityId, PPhone, HPhone, Email, BirthDate, JoinDate, Gender, Qulification, SpecialQulification, WorkTiming, ShiftId, ExperienceDetail, ExperienceYear, Refrence, AdharcardNumber, PancardNumber, BankName, BankAcNo, BankIFSC, UPI_ID, PenNumber, MarritialStatus, LeaveDate, LeavingReason, Remark, Status, UserId) values('" + txtSName.Text + "',N'" + txtSNameG.Text + "','" + txtName.Text + "',N'" + txtNameG.Text + "','" + txtFName.Text + "',N'" + txtFNameG.Text + "','" + txtAddress.Text + "',N'" + txtAddressG.Text + "','" + txtReligious.Text + "',N'" + txtReligiousG.Text + "','" + txtCast.Text + "',N'" + txtCastG.Text + "','" + txtSubCast.Text + "',N'" + txtSubCastG.Text + "','" + txtCategory.Text + "'," + CityID + "," + (txtPPhone.Text == "" ? "NULL" : txtPPhone.Text) + "," + (txtHPhone.Text == "" ? "NULL" : txtHPhone.Text) + ",'" + txtEmail.Text + "','" + dtpBirthDate.Value.ToString("yyyy/MM/dd") + "','" + dtpJoinDate.Value.ToString("yyyy/MM/dd") + "','" + cbGender.Text + "','" + txtQulification.Text + "','" + txtSPQulification.Text + "','" + txtWorkTiming.Text + "'," + shiftId + ",'" + txtExDetails.Text + "'," + (txtExYear.Text == "" ? "NULL" : txtExYear.Text) + ",'" + txtReference.Text + "'," + (txtAdhar.Text == "" ? "NULL" : txtAdhar.Text) + ",'" + txtPancard.Text + "','" + txtBankName.Text + "'," + (txtBankAcNo.Text == "" ? "NULL" : txtBankAcNo.Text) + ",'" + txtBankIFSC.Text + "','" + txtUpiID.Text + "'," + (txtPenNumber.Text == "" ? "NULL" : txtPenNumber.Text) + ",'" + Marrid + "',NULL,NULL,'" + txtRemark.Text + "','" + status + "'," + txtUserID.Text + ")";
                            Cmd = new SqlCommand(insertqry, Con);
                            Cmd.ExecuteNonQuery();
                            Con.Close();
                            if (photochange == true)
                            {
                                string photoname = txtStaffID.Text + ".jpg";
                                File.Copy(photopath, Path.Combine(@"" + global.projectpath + @"StaffPhoto\", photoname), true);
                            }
                            if (cbStatus.Text == "Deactive")
                            {
                                Con.Open();
                                Cmd = new SqlCommand("update StaffMaster set LeaveDate='" + txtLeaveDate.Text + "' LeavingReason='" + txtLeavingReason.Text + "' where StaffID='" + txtStaffID.Text + "'", Con);
                                Cmd.ExecuteNonQuery();
                                Con.Close();
                            }
                            if (cbStatus.Text == "Active")
                            {
                                Con.Open();
                                Cmd = new SqlCommand("update StaffMaster set LeaveDate='" + null + "',LeavingReason='" + null + "' where StaffID='" + txtStaffID.Text + "'", Con);
                                Cmd.ExecuteNonQuery();
                                Con.Close();
                            }
                            MessageBox.Show("Record Added...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //FrmDashboard.instance.studentCount();
                            StoreEToG();
                            btncancel.PerformClick();
                            PBPhoto.Image = null;
                            txtClear();
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
                        string nquery = "select CityID from CityMaster where City='" + txtCity.Text + "';";
                        Cmd = new SqlCommand(nquery, Con);
                        Dtr = Cmd.ExecuteReader();
                        Dtr.Read();
                        //To see record is not null
                        try
                        {
                            CityID = Dtr[0].ToString();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Error: " + ex);
                        }
                        Con.Close();

                        if (txtExYear.Text == "")
                            txtExYear.Text = "0";
                        String Marrid;
                        if (cbMarritialStatus.Text == "Married")
                            Marrid = "M";
                        else
                            Marrid = "U";
                        String status;
                        if (cbStatus.Text == "Active")
                            status = "A";
                        else
                            status = "D";
                        // ShiftId (extract from "1 - Morning")
                        string fullShiftText = cbShiftID.Text;
                        string shiftId = fullShiftText.Split('-')[0].Trim();

                        Con.Open();
                        String upqry = "update StaffMaster set SurName='" + txtSName.Text + "',Name='" + txtName.Text + "',FatherName='" + txtFName.Text + "',Address='" + txtAddress.Text + "',cityID=" + CityID + ",Religious='" + txtReligious.Text + "',Cast='" + txtCast.Text + "',SubCast='" + txtSubCast.Text + "', Category= '"+txtCategory.Text+"' ,GSurName=N'" + txtSNameG.Text + "',GName=N'" + txtNameG.Text + "',GFatherName=N'" + txtFNameG.Text + "',GAddress=N'" + txtAddressG.Text + "',GReligious=N'" + txtReligiousG.Text + "',GCast=N'" + txtCastG.Text + "',GSubCast=N'" + txtSubCastG.Text + "',Gender='" + cbGender.Text + "',HPhone=" + txtHPhone.Text + ",PPhone=" + txtPPhone.Text + ",AdharcardNumber=" + txtAdhar.Text + ",JoinDate='" + dtpJoinDate.Value.ToString("yyyy-MM-dd") + "',BirthDate='" + dtpBirthDate.Value.ToString("yyyy-MM-dd") + "',Status='" + status + "',Email='" + txtEmail.Text + "',Qulification='" + txtQulification.Text + "',SpecialQulification='" + txtSPQulification.Text + "',WorkTiming='" + txtWorkTiming.Text + "',ShiftID=" + shiftId + ",ExperienceDetail='" + txtExDetails.Text + "',ExperienceYear=" + txtExYear.Text + ",Refrence='" + txtReference.Text + "',PancardNumber='" + txtPancard.Text + "',PenNumber='" + txtPenNumber.Text + "',BankName='" + txtBankName.Text + "',BankIFSC='" + txtBankIFSC.Text + "',BankAcNo=" + txtBankAcNo.Text + ",UPI_ID='" + txtUpiID.Text + "',MarritialStatus='" + Marrid + "',Remark='" + txtRemark.Text + "',UserID=" + txtUserID.Text + " where StaffID=" + txtStaffID.Text + "";
                        Cmd = new SqlCommand(upqry, Con);
                        Cmd.ExecuteNonQuery();
                        Con.Close();
                        if (photochange == true)
                        {
                            string photoname = txtStaffID.Text + ".jpg";
                            File.Copy(photopath, Path.Combine(@"" + global.projectpath + "StaffPhoto", photoname), true);
                            PBPhoto.ImageLocation = null;
                            photochange = false;
                        }
                        if (cbStatus.Text == "Deactive")
                        {
                            DateTime dt = DateTime.Now;
                            Con.Open();
                            Cmd = new SqlCommand("update StaffMaster set LeaveDate='" + dt + "',LeavingReason='" + txtLeavingReason.Text + "' where StaffID='" + txtStaffID.Text + "'", Con);
                            Cmd.ExecuteNonQuery();
                            Con.Close();
                        }
                        if (cbStatus.Text == "Active")
                        {
                            Con.Open();
                            Cmd = new SqlCommand("update StaffMaster set LeaveDate='" + null + "',LeavingReason='" + null + "' where StaffID='" + txtStaffID.Text + "'", Con);
                            Cmd.ExecuteNonQuery();
                            Con.Close();
                        }
                        StoreEToG();
                        btncancel.PerformClick();
                        MessageBox.Show("Record Updated...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Fill the Data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Select Any Mode", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            TextBox[] txtb = { txtSName, txtName, txtFName, txtAddress, txtCity, txtTaluko, txtDistrict, txtReligious, txtCast, txtSubCast }; ;
            TextBox[] txtbgj = { txtSNameG, txtNameG, txtFNameG, txtAddressG, txtCityG, txtTalukoG, txtDistrictG, txtReligiousG, txtCastG, txtSubCastG };
            for (int x = 0; x < 10; x++)
            {
                if (txtname == txtb[x].Name)
                    txtbgj[x].Text = txtnameg;
            }
        }

        private void StoreEToG()
        {

            TextBox[] txtb = { txtSName, txtName, txtFName, txtAddress, txtCity, txtTaluko, txtDistrict, txtReligious, txtCast, txtSubCast }; ;
            TextBox[] txtbgj = { txtSNameG, txtNameG, txtFNameG, txtAddressG, txtCityG, txtTalukoG, txtDistrictG, txtReligiousG, txtCastG, txtSubCastG };
            Boolean flag = true;
            for (int x = 0; x < 10; x++)
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
                    string inquery = "insert into EnglishToGujarati values('" + txtb[x].Text + "',N'" + txtbgj[x].Text + "');";
                    Cmd = new SqlCommand(inquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                }
            }
        }

        private void cityLeave()
        {
            Con.Open();
            string nquery = "select City,GCity,Taluko,GTaluko,District,GDistrict from CityMaster where city='" + txtCity.Text + "';";
            Cmd = new SqlCommand(nquery, Con);
            Dtr = Cmd.ExecuteReader();
            if (Dtr.Read()) 
            {
                txtCity.Text = Dtr[0].ToString();
                txtCityG.Text = Dtr[1].ToString();
                txtTaluko.Text = Dtr[2].ToString();
                txtTalukoG.Text = Dtr[3].ToString();
                txtDistrict.Text = Dtr[4].ToString();
                txtDistrictG.Text = Dtr[5].ToString();
            }
           
            Con.Close();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (txtStaffID.Text != "")
            {
                DialogResult ds = MessageBox.Show("Do You Want To Delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ds == DialogResult.Yes)
                {
                    Con.Open();
                    string dquery = "delete from StaffMaster where StaffID='" + txtStaffID.Text + "'";
                    Cmd = new SqlCommand(dquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    string photopath = @"" + global.projectpath + "StudentPhoto" + txtStaffID.Text + ".jpg";
                    File.Delete(photopath);
                    PBPhoto.Image = null;
                    txtClear();
                    //FrmDashboard.instance.studentCount();
                }
            }
            else
            {
                MessageBox.Show("Nothing selected...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            //Button Controls
            btnadd.Enabled = true;
            btnedit.Enabled = false;
            btnsave.Enabled = false;
            btnSearch.Enabled = true;
            btnDocument.Enabled = false;
            btncancel.Enabled = false;
            btndelete.Enabled = false;
            add = false;
            edit = false;
            txtReadOnly(true);
            txtStaffID.ReadOnly = true;
            PBPhoto.Enabled = false;
            txtClear();
            txtUserID.Text = global.User;
            btnadd.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FrmStaffSearch fm = new FrmStaffSearch();
            fm.Show();
        }

        private void btnDocument_Click(object sender, EventArgs e)
        {
            if (txtStaffID.Text != "")
            {
                global.Form = "StaffMaster";
                //FrmStudentDoc fsd = new FrmStudentDoc();
                //fsd.Show();
            }
            else
            {
                MessageBox.Show("No Record Selected...");
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

        private void dtpBirthDate_ValueChanged(object sender, EventArgs e)
        {
            double age;
            TimeSpan t = DateTime.Today - dtpBirthDate.Value;
            age = Math.Round(t.TotalDays / 365, 0);
            lblAge.Text = "Age : " + age.ToString();
        }

        private void txtStaffID_TextChanged(object sender, EventArgs e)
        {
            if (global.Search == true)
            {
                TextBox[] tx = { txtStaffID, txtSName, txtSNameG, txtName, txtNameG, txtFName, txtFNameG, txtAddress, txtAddressG, txtReligious, txtReligiousG, txtCast, txtCastG, txtSubCast, txtSubCastG };
                Con.Open();
                string nquery = "select * from staffMaster where StaffID='" + txtStaffID.Text + "';";
                Cmd = new SqlCommand(nquery, Con);
                Dtr = Cmd.ExecuteReader();
                Dtr.Read();

                for (int a = 0; a < 15; a++)
                {
                    tx[a].Text = Dtr[a].ToString();
                }
                txtCategory.Text = Dtr[15].ToString();
                String CityID = Dtr[16].ToString();

                txtPPhone.Text = Dtr[17].ToString();
                txtHPhone.Text = Dtr[18].ToString();
                txtEmail.Text = Dtr[19].ToString();

                dtpBirthDate.Format = DateTimePickerFormat.Custom;
                dtpBirthDate.CustomFormat = "dd/MM/yyyy";
                dtpBirthDate.Value = (DateTime)Dtr[20];

                dtpJoinDate.Format = DateTimePickerFormat.Custom;
                dtpJoinDate.CustomFormat = "dd/MM/yyyy";
                dtpJoinDate.Value = (DateTime)Dtr[21];


                cbGender.Text = Dtr[22].ToString();
                txtQulification.Text = Dtr[23].ToString();
                txtSPQulification.Text = Dtr[24].ToString();
                txtWorkTiming.Text = Dtr[25].ToString();
                //cbShiftID.Text = Dtr[26].ToString();
                string shiftId = Dtr[26].ToString();
                foreach (string item in cbShiftID.Items)
                {
                    if (item.StartsWith(shiftId + " -"))
                    {
                        cbShiftID.SelectedItem = item;
                        break;
                    }
                }

                txtExDetails.Text = Dtr[27].ToString();
                txtExYear.Text = Dtr[28].ToString();
                txtReference.Text = Dtr[29].ToString();
                txtAdhar.Text = Dtr[30].ToString();
                txtPancard.Text = Dtr[31].ToString();
                txtBankName.Text = Dtr[32].ToString();
                txtBankAcNo.Text = Dtr[33].ToString();
                txtBankIFSC.Text = Dtr[34].ToString();
                txtUpiID.Text = Dtr[35].ToString();
                txtPenNumber.Text = Dtr[36].ToString();
                String marrid = Dtr[37].ToString();
                if (marrid == "M")
                    cbMarritialStatus.Text = "Married";
                else
                    cbMarritialStatus.Text = "Unmarried";
                txtLeaveDate.Text = Dtr[38].ToString();
                txtLeavingReason.Text = Dtr[39].ToString();
                txtRemark.Text = Dtr[40].ToString();
                if (Dtr[41].ToString() == "A")
                    cbStatus.Text = "Active";
                else
                    cbStatus.Text = "Deactive";
                txtUserID.Text = Dtr[42].ToString();
                Con.Close();
                Con.Open();
                string query = "select City,GCity,Taluko,GTaluko,District,GDistrict from CityMaster where cityID=" + CityID + "";
                Cmd = new SqlCommand(query, Con);
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
                Con.Close();

                string photopath = @"" + global.projectpath + @"StaffPhoto\" + txtStaffID.Text + ".jpg";
                PBPhoto.ImageLocation = photopath;
                global.Search = false;


                //button Controls
                btnadd.Enabled = true;
                btnedit.Enabled = true;
                btndelete.Enabled = true;
                btncancel.Enabled = false;
                btnsave.Enabled = false;
                btnSearch.Enabled = true;
                btnDocument.Enabled = true;
                btnPrint.Enabled = true;
            }
        }

        private void txtExYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == 8))
            {
                e.Handled = true; // kisi aur input ko block kar dena
            }
        }


    }
}
