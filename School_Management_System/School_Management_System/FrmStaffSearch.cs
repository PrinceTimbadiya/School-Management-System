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
    public partial class FrmStaffSearch : Form
    {
        SqlConnection Con;
        SqlDataAdapter DA;
        DataSet DS;

        public FrmStaffSearch()
        {
            InitializeComponent();
            
            Con = new SqlConnection(global.ConnectionString);
            showData();
        }

        public void resize1()
        {
            this.Width = global.screenWidth - 100;
            this.Height = global.screenHeight - 50;

            p1.Width = (this.Width * 22) / 100;
            p5.Width = (this.Width * 30) / 100;

            p2.Width = (this.Width * 23) / 100;
            p6.Width = (this.Width * 28) / 100;

            p3.Width = (this.Width * 26) / 100;
            p7.Width = (this.Width * 25) / 100;

            p4.Width = (this.Width * 27) / 100;
            p8.Width = (this.Width * 15) / 100;

            p2.Location = new System.Drawing.Point(p1.Width + 6, 48);
            p6.Location = new System.Drawing.Point(p5.Width + 6, 165);

            p3.Location = new System.Drawing.Point(p1.Width + p2.Width + 9, 48);
            p7.Location = new System.Drawing.Point(p5.Width + p6.Width + 9, 165);

            p4.Location = new System.Drawing.Point(p1.Width + p2.Width + p3.Width + 12, 48);
            p8.Location = new System.Drawing.Point(p5.Width + p6.Width + p7.Width + 12, 165);

            panel1.Width = this.Width - 6;
            panel1.Height = this.Height - 390;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        

        private void showData()
        {
            //string query = "SELECT A.StaffId, A.SurName, A.Name, A.FatherName, A.Address, B.City, B.Taluko, B.District, A.Religious, A.Cast, A.SubCast, A.Gender, A.Status, A.JoinDate, A.BirthDate, A.PPhone, A.Refrence, A.Qulification, A.SpecialQulification, A.WorkTiming, A.ExperienceDetail, A.ExperienceYear, A.MarritialStatus FROM StaffMaster A INNER JOIN CityMaster B ON A.CityId = B.CityId";
            string query = "SELECT StaffId, Surname, Name, FatherName, Address, CityId, Religious, Cast, SubCast, Gender, Status, JoinDate, BirthDate, PPhone FROM StaffMaster";
            DA = new SqlDataAdapter(query, Con);
            DS = new DataSet();
            DA.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            dataGridView1.ClearSelection();
        }


        
        private void FrmStaffSearch_Load(object sender, EventArgs e)
        {
            resize1();
            showData();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (txtStaffID.Text != "")
            {
                global.Search = true;
                Staff.instance.txtStaffID.Text = txtStaffID.Text;
                this.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
                return;

            //MessageBox.Show(SafeValue(dataGridView1.CurrentRow.Cells["BirthDate"]), "BirthDate Value");

            SetSafeText(txtStaffID, "StaffId");
            SetSafeText(txtSName, "SurName");
            SetSafeText(txtName, "Name");
            SetSafeText(txtFName, "FatherName");
            SetSafeText(txtAddress, "Address");

            SetSafeText(txtCity, "City");
            SetSafeText(txtTaluko, "Taluko");
            SetSafeText(txtDistrict, "District");

            SetSafeText(txtReligious, "Religious");
            SetSafeText(txtCast, "Cast");
            SetSafeText(txtSubCast, "SubCast");
            SetSafeText(txtGender, "Gender");
            SetSafeText(txtStatus, "Status");

            SetSafeText(txtJoinDate, "JoinDate");
            SetSafeText(txtBirthDate, "BirthDate");

            SetSafeText(txtPhone, "PPhone");
            SetSafeText(txtReference, "Refrence");
            SetSafeText(txtQulification, "Qualification");
            SetSafeText(txtSpQulification, "SpecialQualification");
            SetSafeText(txtWorkTiming, "WorkTiming");
            SetSafeText(txtEXDetail, "ExperienceDetail");
            SetSafeText(txtEXYear, "ExperienceYear");

            //alag thi combobox handle karu che
            SetSafeCB(cbMarried, "MarritialStatus");


            //if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
            //    return;

            //if (dataGridView1.CurrentRow.Cells.Count < 23)
            //{
            //    MessageBox.Show("DataGridView does not contain expected 23 columns.");
            //    return;
            //}

            //try
            //{
            //    txtStaffID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //    txtSName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //    txtName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            //    txtFName.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            //    txtAddress.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            //    txtCity.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            //    txtTaluko.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            //    txtDistrict.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            //    txtReligious.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            //    txtCast.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            //    txtSubCast.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            //    txtGender.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            //    txtStatus.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();

            //    txtJoinDate.Text = (dataGridView1.CurrentRow.Cells[13] != null && dataGridView1.CurrentRow.Cells[13].Value != null && dataGridView1.CurrentRow.Cells[13].Value != DBNull.Value) ? Convert.ToDateTime(dataGridView1.CurrentRow.Cells[13].Value).ToString("dd/MM/yyyy") : "";

                //txtBirthDate.Text = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[14].Value).ToString("dd/MM/yyyy");

                //txtPhone.Text = dataGridView1.CurrentRow.Cells[15].Value.ToString();
                //txtReference.Text = dataGridView1.CurrentRow.Cells[16].Value.ToString();
                //txtQulification.Text = dataGridView1.CurrentRow.Cells[17].Value.ToString();
                //txtSpQulification.Text = dataGridView1.CurrentRow.Cells[18].Value.ToString();
                //txtWorkTiming.Text = dataGridView1.CurrentRow.Cells[19].Value.ToString();
                //txtEXDetail.Text = dataGridView1.CurrentRow.Cells[20].Value.ToString();
                //txtEXYear.Text = dataGridView1.CurrentRow.Cells[21].Value.ToString();

                //cbMarried.Text = dataGridView1.CurrentRow.Cells[22].Value.ToString();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error while loading data: " + ex.Message);
            //}

        }



        private string SafeValue(DataGridViewCell cell)
        {
            return (cell != null && cell.Value != null && cell.Value != DBNull.Value) ? cell.Value.ToString() : "";
        }

        private void SetSafeText(TextBox textBox, string columnName)
        {
            try
            {
                if (dataGridView1.CurrentRow != null &&
                    dataGridView1.Columns.Contains(columnName) &&
                    dataGridView1.CurrentRow.Cells[columnName] != null)
                {
                    object val = dataGridView1.CurrentRow.Cells[columnName].Value;

                    if (val != null && val != DBNull.Value)
                    {
                        // jo value DateTime hoy to format karo
                        if (val is DateTime || val.GetType().Name == "DateTime")
                        {
                            DateTime dt = Convert.ToDateTime(val);
                            textBox.Text = dt.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            textBox.Text = val.ToString();
                        }
                    }
                    else
                    {
                        textBox.Text = "";
                    }
                }
                else
                {
                    textBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in SetSafeText [" + columnName + "]: " + ex.Message);
                textBox.Text = "";
            }
        }


        private void SetSafeCB(ComboBox cb, string columnName)
        {
            if (dataGridView1.CurrentRow != null &&
                dataGridView1.Columns.Contains(columnName) &&
                dataGridView1.CurrentRow.Cells[columnName] != null)
            {
                object val = dataGridView1.CurrentRow.Cells[columnName].Value;

                // Agar null/DBNull nahi hai to set karo, warna blank
                cb.Text = (val != null && val != DBNull.Value) ? val.ToString() : "";
            }
            else
            {
                cb.Text = "";
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            TextBox[] tx = { txtStaffID, txtSName, txtName, txtFName, txtAddress, txtCity, txtTaluko, txtDistrict, txtReligious, txtCast, txtSubCast, txtGender, txtStatus, txtJoinDate, txtBirthDate, txtPhone, txtReference, txtQulification, txtSpQulification, txtWorkTiming, txtEXDetail, txtEXYear };
            for (int x = 0; x < 22; x++)
                tx[x].Text = "";
            showData();
        }


        //For searching
        private void txtBirthDate_TextChanged(object sender, EventArgs e)
        {
            String married = "";
            if (cbMarried.Text == "Married")
                married = "M";
            else
                married = "U";
            String query = "select A.StaffId,A.SurName,A.Name,A.FatherName,A.Address,B.City,B.Taluko,B.District,A.Religious,A.Cast,A.SubCast,A.Gender,A.Status,A.joinDate,A.BirthDate,A.PPhone from (select * from StaffMaster where staffID like '" + txtStaffID.Text + "%' AND SurName like '" + txtSName.Text + "%' AND Name like '" + txtName.Text + "%' AND FatherName like '" + txtFName.Text + "%' AND  Address like '" + txtAddress.Text + "%' AND Religious like '" + txtReligious.Text + "%' AND Cast like '" + txtCast.Text + "%' AND SubCast like '" + txtSubCast.Text + "%' AND (HPhone like '" + txtPhone.Text + "%' OR PPhone like '" + txtPhone.Text + "%') AND Gender like '" + txtGender.Text + "%' AND JoinDate like '" + txtJoinDate.Text + "%' AND BirthDate like '" + txtBirthDate.Text + "%' AND Status like '" + txtStatus.Text + "%'  AND Refrence like '" + txtReference.Text + "%'  AND Qulification like '" + txtQulification.Text + "%'  AND SpecialQulification like '" + txtSpQulification.Text + "%' AND WorkTiming like '" + txtWorkTiming.Text + "%'  AND ExperienceDetail like '" + txtEXDetail.Text + "%' AND ExperienceYear like '" + txtEXYear.Text + "%' AND MarritialStatus like '%" + married + "%') As A INNER JOIN (select * from citymaster where city like '" + txtCity.Text + "%' AND Taluko like '" + txtTaluko.Text + "%' AND District like '" + txtDistrict.Text + "%' ) AS B ON A.CityId=B.CityId;";
            DA = new SqlDataAdapter(query, Con);
            DS = new DataSet();
            DA.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
        }
    }
}
