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
//using Microsoft.Office.Interop.Excel;

namespace School_Management_System
{
    public partial class FrmSearch : Form
    {
        public static FrmSearch instance;
        SqlConnection Con;
        SqlDataAdapter DA;
        DataSet DS;
        //SqlCommand Cmd;
        //SqlDataReader Dtr;
        //String city, taluko, district;

        public FrmSearch()
        {
            InitializeComponent();
            instance = this;
            Con = new SqlConnection(global.ConnectionString);
            showData();
        }

        public void resize1()
        {
            this.Width = global.screenWidth - 200;
            this.Height = global.screenHeight - 50;

            p1.Width = (this.Width * 26) / 100;
            p5.Width = ((this.Width - 300) * 35) / 100;

            p2.Width = (this.Width * 23) / 100;
            p6.Width = ((this.Width - 300) * 30) / 100;

            p3.Width = (this.Width * 25) / 100;
            p7.Width = ((this.Width - 300) * 34) / 100;

            p4.Width = (this.Width * 23) / 100;


            p2.Location = new System.Drawing.Point(p1.Width + 6, 48);
            p6.Location = new System.Drawing.Point(p5.Width + 6, 161);

            p3.Location = new System.Drawing.Point(p1.Width + p2.Width + 9, 48);
            p7.Location = new System.Drawing.Point(p5.Width + p6.Width + 9, 161);

            p4.Location = new System.Drawing.Point(p1.Width + p2.Width + p3.Width + 12, 48);

            p8.Location = new System.Drawing.Point(p5.Width + p6.Width + p7.Width + 12, 162);
            p9.Location = new System.Drawing.Point(p5.Width + p6.Width + p7.Width + 12, 288);

            panel8.Width = this.Width - 6;
            panel8.Height = this.Height - 380;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox[] tx = { txtReDgNo, txtGRNo, txtSName, txtName, txtFName, txtGFName, txtMName, txtAddress, txtCity, txtTaluko, txtDistrict, txtLastSchool, txtBirthPlace, txtBirthDistrict, txtReligious, txtCast, txtSubCast, txtDeptCode, txtClass, txtJoiningStarted, txtRollNo, txtType, txtGender, txtStatus, txtJoinDate, txtBirthDate, txtPhone };
            for (int x = 0; x < 27; x++)
                tx[x].Text = "";
            showData();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showData()
        {
            if (global.formname == "Staff")
            {
                lblID.Text = "Staff ID";
                txtDeptCode.Enabled = false;
                txtGRNo.Enabled = false;
                txtClass.Enabled = false;
                txtJoiningStarted.Enabled = false;
                txtRollNo.Enabled = false;
                txtType.Enabled = false;
                txtLastSchool.Enabled = false;
                txtGFName.Enabled = false;
                txtMName.Enabled = false;
                txtBirthPlace.Enabled = false;
                txtBirthDistrict.Enabled = false;

                string query = "select StaffId,SurName,Name,FatherName,Address,CityID,Religious,Cast,SubCast,Gender,Status,joinDate,BirthDate,PPhone from StaffMaster ";
                DA = new SqlDataAdapter(query, Con);
                DS = new DataSet();
                DA.Fill(DS);
                dataGridView1.DataSource = DS.Tables[0];
                dataGridView1.ClearSelection();
            }
            else
            {
                lblID.Text = "ReDgNo";
                string query = "select ReDgNo,GRNumber,SurName,Name,FatherName,GFatherName,MotherName,Address,LastSchool,BirthPlace,BirthDistrict,Religious,Cast,SubCast,DeptCode,CurrentClass,JoiningStarted,RollNo,Type,Gender,Status,joinDate,BirthDate,FatherPhone from StudentMaster ";
                DA = new SqlDataAdapter(query, Con);
                DS = new DataSet();
                DA.Fill(DS);
                dataGridView1.DataSource = DS.Tables[0];
            }
        }

        private void txtReDgNo_TextChanged(object sender, EventArgs e)
        {
            if (global.formname == "Staff")
            {
                String query = "select A.StaffID,A.SurName,A.Name,A.FatherName,A.Address,B.city,B.Taluko,B.District,A.Religious,A.Cast,A.SubCast,A.Gender,A.Status,A.joinDate,A.BirthDate,A.PPhone from (select * from StaffMaster where staffID like '" + txtReDgNo.Text + "%' AND SurName like '" + txtSName.Text + "%' AND Name like '" + txtName.Text + "%' AND FatherName like '" + txtFName.Text + "%' AND  Address like '" + txtAddress.Text + "%' AND Religious like '" + txtReligious.Text + "%' AND Cast like '" + txtCast.Text + "%' AND SubCast like '" + txtSubCast.Text + "%' AND (HPhone like '" + txtPhone.Text + "%' OR PPhone like '" + txtPhone.Text + "%') AND Gender like '" + txtGender.Text + "%' AND JoinDate like '" + txtJoinDate.Text + "%' AND BirthDate like '" + dtpBirthDate.Value.ToString("yyyy-MM-dd") + "%' AND Status like '" + txtStatus.Text + "%') As A INNER JOIN (select * from citymaster where city like '" + txtCity.Text + "%' AND Taluko like '" + txtTaluko.Text + "%' AND District like '" + txtDistrict.Text + "%') AS B ON A.CityId=B.CityId;";
                DA = new SqlDataAdapter(query, Con);
                DS = new DataSet();
                DA.Fill(DS);
                dataGridView1.DataSource = DS.Tables[0];
            }
            else
            {
                String query = "select A.ReDgNo,A.GRNumber,A.SurName,A.Name,A.FatherName,A.GFatherName,A.MotherName,A.Address,B.City,B.Taluko,B.District,A.LastSchool,A.BirthPlace,A.BirthDistrict,A.Religious,A.Cast,A.SubCast,A.DeptCode,A.CurrentClass,A.JoiningStarted,A.RollNo,A.Type,A.Gender,A.Status,A.joinDate,A.BirthDate,A.FatherPhone from (select * from StudentMaster where ReDgNo like '" + txtReDgNo.Text + "%' AND SurName like '" + txtSName.Text + "%' AND Name like '" + txtName.Text + "%' AND FatherName like '" + txtFName.Text + "%' AND GFatherName like '" + txtGFName.Text + "%' AND MotherName like '" + txtMName.Text + "%'AND Address like '" + txtAddress.Text + "%' AND LastSchool like '" + txtLastSchool.Text + "%' AND BirthPlace like '" + txtBirthPlace.Text + "%' AND BirthDistrict like '" + dtpBirthDate.Value.ToString("yyyy-MM-dd") + "%' AND Religious like '" + txtReligious.Text + "%' AND Cast like '" + txtCast.Text + "%' AND SubCast like '" + txtSubCast.Text + "%' AND (HomePhone like '" + txtPhone.Text + "%' OR FatherPhone like '" + txtPhone.Text + "%' OR PersonalPhone like '" + txtPhone.Text + "%') AND GRNumber like '" + txtGRNo.Text + "%' AND RollNo like '" + txtRollNo.Text + "%' AND JoiningStarted like '" + txtJoiningStarted.Text + "%' AND Gender like '" + txtGender.Text + "%'   AND DeptCode like '" + txtDeptCode.Text + "%'  AND JoinDate like '" + txtJoinDate.Text + "%' AND BirthDate like '" + txtBirthDate.Text + "%' AND CurrentClass like '" + txtClass.Text + "%' AND Type like '" + txtType.Text + "%' AND Status like '" + txtStatus.Text + "%') AS A INNER JOIN (select * from citymaster where city like '" + txtCity.Text + "%' AND Taluko like '" + txtTaluko.Text + "%' AND District like '" + txtDistrict.Text + "%') AS B ON A.CityId=B.CityId";
                DA = new SqlDataAdapter(query, Con);
                DS = new DataSet();
                DA.Fill(DS);
                dataGridView1.DataSource = DS.Tables[0];
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (chkStructure.Checked == true)
            {
                global.ReportName = "StudentSerach";
                //FrmFeeReport ffm = new FrmFeeReport();
                //ffm.Show();
            }
            else
            {
                global.ReportName = "StructureStudentSerach";
                //FrmFeeReport ffm = new FrmFeeReport();
                //ffm.Show();
            }
        }

        private void btnPrintExcel_Click(object sender, EventArgs e)
        {
            //Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
            //Workbook wb;
            //Worksheet ws;

            //wb = Excel.Workbooks.Add();
            //ws = wb.Worksheets[1];

            //Con.Open();
            //string nquery1 = "select StudentMaster.ReDgNo,StudentMaster.RollNo,StudentMaster.GRNumber,StudentMaster.BirthDate,StudentMaster.gender,StudentMaster.CurrentClass,StudentMaster.SurNameGj,StudentMaster.NameGj,StudentMaster.FatherNameGj,StudentMaster.gFatherNameGj,StudentMaster.MotherNamegj,StudentMaster.addressGj,CityMaster.cityGj,CityMaster.talukoGj,CityMaster.districtGj,StudentMaster.Division,StudentMaster.SurName,StudentMaster.Name,StudentMaster.FatherName,StudentMaster.MotherName,StudentMaster.Address,CityMaster.City,CityMaster.Taluko,CityMaster.District,StudentMaster.HomePhone,StudentMaster.FatherPhone,StudentMaster.PersonalPhone,StudentMaster.LastSchool,StudentMaster.Religious,StudentMaster.Cast,StudentMaster.SubCast,StudentMaster.Status,StudentMaster.LeaveDate from StudentMaster INNER JOIN CityMaster ON StudentMaster.CityID=CityMaster.CityID where StudentMaster.ReDgNo like '" + txtReDgNo.Text + "%' AND StudentMaster.SurName like '" + txtSName.Text + "%' AND StudentMaster.Name like '" + txtName.Text + "%' AND StudentMaster.FatherName like '" + txtFName.Text + "%' AND StudentMaster.GFatherName like '" + txtGFName.Text + "%' AND StudentMaster.MotherName like '" + txtMName.Text + "%'AND StudentMaster.Address like '" + txtAddress.Text + "%' AND StudentMaster.LastSchool like '" + txtLastSchool.Text + "%' AND StudentMaster.BirthPlace like '" + txtBirthPlace.Text + "%' AND StudentMaster.BirthDistrict like '" + txtBirthDistrict.Text + "%' AND StudentMaster.Religious like '" + txtReligious.Text + "%' AND StudentMaster.Cast like '" + txtCast.Text + "%' AND StudentMaster.SubCast like '" + txtSubCast.Text + "%' AND (StudentMaster.HomePhone like '" + txtPhone.Text + "%' OR StudentMaster.FatherPhone like '" + txtPhone.Text + "%' OR StudentMaster.PersonalPhone like '" + txtPhone.Text + "%') AND StudentMaster.GRNumber like '" + txtGRNo.Text + "%' AND StudentMaster.RollNo like '" + txtRollNo.Text + "%' AND StudentMaster.JoiningStarted like '" + txtJoiningStarted.Text + "%' AND StudentMaster.Gender like '" + txtGender.Text + "%'   AND StudentMaster.DeptCode like '" + txtDeptCode.Text + "%'  AND StudentMaster.JoinDate like '" + txtJoinDate.Text + "%' AND StudentMaster.BirthDate like '" + txtBirthDate.Text + "%' AND StudentMaster.CurrentClass like '" + txtClass.Text + "%' AND StudentMaster.Type like '" + txtType.Text + "%' AND StudentMaster.Status like '" + txtStatus.Text + "%' AND CityMaster.city like '" + txtCity.Text + "%' AND CityMaster.Taluko like '" + txtTaluko.Text + "%' AND CityMaster.District like '" + txtDistrict.Text + "%';";
            //Cmd = new SqlCommand(nquery1, Con);
            //Dtr = Cmd.ExecuteReader();
            //int x = 4;
            //int nfields = Dtr.FieldCount;
            //for (int i = 15; i < nfields; i++)
            //{
            //    ws.Cells[3, i + 1] = Dtr.GetName(i);
            //}
            //while (Dtr.Read())
            //{
            //    for (int y = 0; y < 33; y++)
            //    {
            //        ws.Cells[x, (y + 1)] = Dtr[y].ToString();
            //    }
            //    x++;
            //}
            //Con.Close();

            //ws.Cells[3, 1] = "ReDgNo";
            //ws.Cells[3, 2] = "Roll No";
            //ws.Cells[3, 3] = "G.R.No.";
            //ws.Cells[3, 4] = "BirthDate";
            //ws.Cells[3, 5] = "Gender";
            //ws.Cells[3, 6] = "Class";
            //ws.Cells[3, 7] = "અટક";
            //ws.Cells[3, 8] = "નામ";
            //ws.Cells[3, 9] = "પિતાનું નામ";
            //ws.Cells[3, 10] = "દાદાનું નામ";
            //ws.Cells[3, 11] = "માતાનું નામ";
            //ws.Cells[3, 12] = "સરનામું";
            //ws.Cells[3, 13] = "ગામ /શહેર";
            //ws.Cells[3, 14] = "તાલુકો";
            //ws.Cells[3, 15] = "જીલ્લો";

            //Range range = ws.Range[ws.Cells[1, 1], ws.Cells[x - 1, 33]];
            //range.Borders.LineStyle = XlLineStyle.xlContinuous;
            //range.Borders.Weight = XlBorderWeight.xlThin;
            //range.Borders.Color = System.Drawing.Color.Black;

            //Range range2 = ws.Range[ws.Cells[1, 1], ws.Cells[1, 33]];
            //range2.Merge();
            //range2.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //range2.Font.Size = 15;
            //ws.Cells[1, 1] = "School Name";

            //Range range3 = ws.Range[ws.Cells[2, 1], ws.Cells[2, 33]];
            //range3.Merge();
            //range3.HorizontalAlignment = XlHAlign.xlHAlignLeft;

            //Range range4 = ws.Range[ws.Cells[3, 1], ws.Cells[3, 33]];
            //range4.Font.Bold = true;
            //range4.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            //ws.Columns[4].ColumnWidth = 10;
            //ws.Columns[5].ColumnWidth = 7;
            //ws.Columns[6].ColumnWidth = 10;
            //ws.Columns[7].ColumnWidth = 10;
            //ws.Columns[8].ColumnWidth = 10;
            //ws.Columns[9].ColumnWidth = 10;
            //ws.Columns[10].ColumnWidth = 10;
            //ws.Columns[11].ColumnWidth = 10;
            //ws.Columns[12].ColumnWidth = 20;
            //ws.Columns[13].ColumnWidth = 10;
            //ws.Columns[14].ColumnWidth = 10;
            //ws.Columns[15].ColumnWidth = 10;
            //ws.Columns[16].ColumnWidth = 15;
            //ws.Columns[17].ColumnWidth = 10;
            //ws.Columns[18].ColumnWidth = 10;
            //ws.Columns[19].ColumnWidth = 10;
            //ws.Columns[20].ColumnWidth = 10;
            //ws.Columns[21].ColumnWidth = 20;
            //ws.Columns[22].ColumnWidth = 10;
            //ws.Columns[23].ColumnWidth = 10;
            //ws.Columns[24].ColumnWidth = 10;
            //ws.Columns[25].ColumnWidth = 12;
            //ws.Columns[26].ColumnWidth = 12;
            //ws.Columns[27].ColumnWidth = 12;
            //ws.Columns[28].ColumnWidth = 15;
            //ws.Columns[29].ColumnWidth = 10;
            //ws.Columns[30].ColumnWidth = 10;
            //ws.Columns[31].ColumnWidth = 10;
            //ws.Columns[32].ColumnWidth = 10;
            //ws.Columns[33].ColumnWidth = 12;

            //string folderPath = global.projectpath + "StudentReport";
            //// Check if the folder already exists
            //if (!Directory.Exists(folderPath))
            //{
            //    // Create the folder
            //    Directory.CreateDirectory(folderPath);
            //}

            //string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            //String file = global.projectpath + "StudentReport\\StudentReport" + timestamp + ".xlsx";
            //wb.SaveAs(file);
            //wb.Close();
            //Excel.Quit();
            //MessageBox.Show("Report Saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmSearch_Load(object sender, EventArgs e)
        {
            resize1();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            global.Search = true;
            if (global.formname == "FrmStudent")
            {
                FrmStudent.instance.txtReDgNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
            else if (global.formname == "FrmFee")
            {
                //FrmFee.instance.txtReDgNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
            else if (global.formname == "Staff")
            {
                Staff.instance.txtStaffID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
            else if (global.formname == "FrmComplaints")
            {
                //FrmComplaints.instance.txtReDgNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
            else if (global.formname == "FrmRajaReport")
            {
                //FrmRajaReport.instance.txtReDgNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
            else if (global.formname == "FrmCertificateIssue")
            {
                //FrmCertificateIssue.instance.txtReDgNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
            else if (global.formname == "FrmMailMaster")
            {
                //FrmMailMaster.instance.txtReDgNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
            else if (global.formname == "FrmMailRegister")
            {
                //FrmMailRegister.instance.txtReDgNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
            else if (global.formname == "FrmMarkEntry")
            {
                //FrmMarkEntry.instance.txtReDgNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
            this.Close();
        }
    }
}
