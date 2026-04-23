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
    public partial class FrmFeeReport : Form
    {
        SqlConnection Con;
        SqlDataAdapter DA;
        DataSet DS;

        public FrmFeeReport()
        {
            InitializeComponent();
            Con = new SqlConnection(global.ConnectionString);
        }

        private void FrmFeeReport_Load(object sender, EventArgs e)
        {

            try
            {


                if (global.ReportName == "Student")
                {
                    Student s = new Student();

                    DA = new SqlDataAdapter("select * from studentMaster where ReDgNo='" + FrmStudent.instance.txtReDgNo.Text + "'", Con);
                    DS = new DataSet();
                    DA.Fill(DS, "studentMaster");

                    SqlDataAdapter DA1 = new SqlDataAdapter("select * from CityMaster where CityID=(select CityID from studentMaster where ReDgNo='" + FrmStudent.instance.txtReDgNo.Text + "');", Con);
                    DA1.Fill(DS, "CityMaster");

                    s.SetDataSource(DS);
                    crystalReportViewer1.ReportSource = s;
                    MessageBox.Show("report");



                    crystalReportViewer1.Refresh();

                    //s.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat,@"D:\abc.pdf");

                }
                //this.reportViewer1.RefreshReport();

                if (global.ReportName == "Fee")
                {
                    FeeReceipt cr = new FeeReceipt();

                    DA = new SqlDataAdapter("select * from FeesMaster", Con);
                    DataSet DS = new DataSet();
                    DA.Fill(DS, "FeesMaster");

                    SqlDataAdapter DA1 = new SqlDataAdapter("select * from StudentMaster", Con);
                    DA.Fill(DS, "StudentMaster");
                    //cr.SetDataSource(DS.Tables["FeesMaster"]);
                    cr.SetDataSource(DS);
                    crystalReportViewer1.ReportSource = cr;
                    crystalReportViewer1.Refresh();
                }

                    //FeeReceipt cr = new FeeReceipt();

                    //DA = new SqlDataAdapter("select * from FeesMaster", Con);
                    //DataSet DS = new DataSet();
                    //DA.Fill(DS, "FeesMaster");

                    //SqlDataAdapter DA1 = new SqlDataAdapter("select * from StudentMaster", Con);
                    //DA.Fill(DS, "StudentMaster");
                    ////cr.SetDataSource(DS.Tables["FeesMaster"]);
                    //cr.SetDataSource(DS);
                    //crystalReportViewer1.ReportSource = cr;
                    //crystalReportViewer1.Refresh();
                


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message);
            }
        }
    }
}

