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
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;

namespace School_Management_System
{
    public partial class FrmAttendence : Form
    {
        public static FrmAttendence instance;

        SqlConnection Con;
        //SqlCommand Cmd;
        //SqlDataAdapter DA;
        //SqlDataReader Dtr;
        //DataSet DS;

        public FrmAttendence()
        {
            InitializeComponent();
            instance = this;
            Con = new SqlConnection(global.ConnectionString);
        }

        private void FrmAttendence_Load(object sender, EventArgs e)
        {
            resize1();
            LoadShifts();
            LoadClasses();
            LoadYears();
            LoadMonths();

            rbShift.Checked = true; // Default
            rbShift.Focus();
        }

        public void resize1()
        {
            panel1.Width = (this.Width * 60) / 100;
            
            //panel2.Width = global.screenWidth - 240;
            //panel2.Height = global.screenHeight - 380;
        }

        //classmaster
        // Load all Shifts from ShiftMast
        private void LoadShiftList()
        {
            string query = "SELECT ShiftId, ShiftName FROM ShiftMast ORDER BY ShiftName";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cbShift.DataSource = dt;
            cbShift.DisplayMember = "ShiftName";
            cbShift.ValueMember = "ShiftId";
            cbShift.SelectedIndex = -1;
        }

        // Load Classes by selected Shift
        private void LoadClassListByShift(int shiftId)
        {
            string query = "SELECT ClassId, ClassName FROM ClassMaster WHERE Shift = " + shiftId + " ORDER BY ClassName";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cbClass.DataSource = dt;
            cbClass.DisplayMember = "ClassName";
            cbClass.ValueMember = "ClassId";
            cbClass.SelectedIndex = -1;
        }




        //studentmaster
        private void LoadShiftFromStudentMaster()
        {
            //string query = "SELECT DISTINCT DeptCode FROM StudentMaster";
            //SqlDataAdapter da = new SqlDataAdapter(query, Con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

            //cbShift.DataSource = dt;
            //cbShift.DisplayMember = "DeptCode";   // Shift ki jagah Dept dikhega
            //cbShift.ValueMember = "DeptCode";
        }

        private void LoadClassFromStudentMaster()
        {
            //string query = "SELECT DISTINCT Division FROM StudentMaster";
            //SqlDataAdapter da = new SqlDataAdapter(query, Con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

            //cbClass.DataSource = dt;
            //cbClass.DisplayMember = "Division";   // Class ki jagah Division dikhega
            //cbClass.ValueMember = "Division";
        }



        private void LoadShifts()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT ShiftID, ShiftName FROM ShiftMast", Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbShift.DataSource = dt;
            cbShift.DisplayMember = "ShiftName";
            cbShift.ValueMember = "ShiftID";
        }

        private void LoadClasses()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT ClassID, ClassName FROM ClassMaster ", Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbClass.DataSource = dt;
            cbClass.DisplayMember = "ClassName";
            cbClass.ValueMember = "ClassID";
        }

        private void LoadYears()
        {
            // Dynamic year (Current year +/- 5 years)
            int currentYear = DateTime.Now.Year;
            for (int y = currentYear - 2; y <= currentYear + 2; y++)
            {
                cbYear.Items.Add(y);
            }
            cbYear.SelectedItem = currentYear;
        }

        private void LoadMonths()
        {
            cbMonth.Items.Clear();
            for (int m = 1; m <= 12; m++)
            {
                cbMonth.Items.Add(new DateTime(2000, m, 1).ToString("MMMM"));
            }
            cbMonth.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void SetFontByLanguage(Excel.Range cell, string text)
        {
            cell.Value = text;

            // Gujarati Unicode range check
            if (text.Any(c => c >= 0x0A80 && c <= 0x0AFF))
            {
                cell.Font.Name = "Shruti";  // Gujarati
            }
            else
            {
                cell.Font.Name = "Arial";   // English/Numbers
            }
        }

        

        private void btnRegExcel_Click(object sender, EventArgs e)
        {

            //Excel.Application xlApp = null;
            //Excel.Workbook wb = null;
            //Excel.Worksheet ws = null;

            //try
            //{
            //    int year = Convert.ToInt32(cbYear.SelectedItem);
            //    int month = cbMonth.SelectedIndex + 1;
            //    int daysInMonth = DateTime.DaysInMonth(year, month);

            //    // Gujarati weekday names
            //    string[] gujDays = { "રવિ", "સોમ", "મંગળ", "બુધ", "ગુરુ", "શુક્ર", "શનિ" };

            //    // --- Language wise field selection ---
            //    string surnameField = rbGuj.Checked ? "SurNameGj" : "SurName";
            //    string nameField = rbGuj.Checked ? "NameGj" : "Name";
            //    string fatherField = rbGuj.Checked ? "FatherNameGj" : "FatherName";

            //    // --- Student Query ---
            //    string query = "SELECT ReDgNo, GRNumber, BirthDate, Gender, Cast, SubCast, Category, RollNo, "
            //                    + surnameField + " AS SurName, "
            //                    + nameField + " AS Name, "
            //                    + fatherField + " AS FatherName " +
            //                    "FROM StudentMaster ORDER BY RollNo";

            //    SqlDataAdapter da = new SqlDataAdapter(query, Con);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);

            //    // --- Excel Start ---
            //    xlApp = new Excel.Application();
            //    wb = xlApp.Workbooks.Add(Type.Missing);
            //    ws = (Excel.Worksheet)wb.ActiveSheet;
            //    ws.Name = "Attendance Register";

            //    // ---------------- Title ----------------
            //    Excel.Range titleCell = ws.Range["A1", "F1"];
            //    titleCell.Merge();
            //    titleCell.Value = "મોનાર્ક શૈક્ષણિક સંકુલ";
            //    titleCell.Font.Name = "Shruti";
            //    titleCell.Font.Size = 18;
            //    titleCell.Font.Bold = true;
            //    //titleCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            //    // ---------------- Month ----------------
            //    Excel.Range monthCell = ws.Range["V1", "AC1"];
            //    monthCell.Merge();
            //    monthCell.Value = "Month : " + new DateTime(year, month, 1).ToString("MMMM-yyyy");
            //    monthCell.Font.Name = "Arial";
            //    monthCell.Font.Size = 12;
            //    monthCell.Font.Bold = true;

            //    // ---------------- Teacher ----------------
            //    Excel.Range teacherCell = ws.Range["AF1", "AK1"];
            //    teacherCell.Merge();
            //    teacherCell.Value = "Teacher’s Name : ____________";
            //    teacherCell.Font.Name = "Calibri";
            //    teacherCell.Font.Size = 11;
            //    teacherCell.Font.Bold = true;

            //    // ---------------- Subtitle ----------------
            //    Excel.Range subtitleCell = ws.Range["A2", "G2"];
            //    subtitleCell.Merge();
            //    subtitleCell.Value = "વિદ્યાર્થીઓનો ક્લાસ વાઈજ ડેટા";
            //    subtitleCell.Font.Name = "Shruti";
            //    subtitleCell.Font.Size = 12;

            //    // ---------------- Class Info ----------------
            //    Excel.Range classCell = ws.Range["I2", "K2"];
            //    classCell.Merge();
            //    classCell.Value = "Class : " + cbClass.Text;
            //    classCell.Font.Name = "Arial";
            //    classCell.Font.Size = 12;
            //    classCell.Font.Bold = true;

            //    // ---------------- Headers ----------------
            //    string[] headers = { "રજી નં.", "જર નં.", "જન્મ તા.", "જાતિ", "ધર્મ", "જ્ઞાતિ", "કેટેગરી", "રોલ નં.", "અટક", "નામ", "પિતાનું નામ" };
            //    for (int i = 0; i < headers.Length; i++)
            //    {
            //        Excel.Range mergeHeader = ws.Range[ws.Cells[4, i + 1], ws.Cells[5, i + 1]];
            //        mergeHeader.Merge();
            //        mergeHeader.Font.Name = "Shruti";
            //        mergeHeader.Font.Size = 10;
            //        mergeHeader.Font.Bold = true;
            //        mergeHeader.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //        mergeHeader.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            //        mergeHeader.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            //        mergeHeader.Interior.Color = Color.White; // Header BG hamesha white

            //        string headerText = headers[i];
            //        string finalText = headerText;

            //        if (headerText == "જાતિ")
            //        {
            //            finalText = headerText;
            //            mergeHeader.Orientation = 90; // vertical text
            //            mergeHeader.WrapText = true;
            //        }
            //        else if (headerText == "રજી નં.")
            //        {
            //            finalText = "રજી\nનં.";
            //            mergeHeader.WrapText = true;
            //        }
            //        else if (headerText == "જર નં.")
            //        {
            //            finalText = "જર\nનં.";
            //            mergeHeader.WrapText = true;
            //        }
            //        else if (headerText == "કેટેગરી")
            //        {
            //            finalText = "કેટે\nગરી";
            //            mergeHeader.WrapText = true;
            //        }
            //        else if (headerText == "રોલ નં.")
            //        {
            //            finalText = "રોલ\nનં.";
            //            mergeHeader.WrapText = true;
            //        }

            //        mergeHeader.Value = finalText;
            //    }


            //    // ---------------- Day Headers ----------------
            //    for (int d = 1; d <= daysInMonth; d++)
            //    {
            //        DateTime currentDate = new DateTime(year, month, d);
            //        string dayName = gujDays[(int)currentDate.DayOfWeek];

            //        // Day Name (Gujarati, Bold, Vertical)
            //        Excel.Range dayCell = ws.Cells[4, d + 11];
            //        dayCell.Value = dayName;
            //        dayCell.Font.Name = "Shruti";
            //        dayCell.Font.Size = 9;
            //        dayCell.Font.Bold = true;
            //        dayCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //        dayCell.Orientation = 90;
            //        dayCell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

            //        // Date Number (English, Bold)
            //        Excel.Range dateCell = ws.Cells[5, d + 11];
            //        dateCell.Value = d;
            //        dateCell.Font.Name = "Arial";
            //        dateCell.Font.Size = 9;
            //        dateCell.Font.Bold = true;
            //        dateCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //        dateCell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            //    }

            //    // ---------------- Student Rows ----------------
            //    int row = 6;
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        ws.Cells[row, 1].Value = dr["ReDgNo"];
            //        ws.Cells[row, 2].Value = dr["GRNumber"];
            //        ws.Cells[row, 3].Value = Convert.ToDateTime(dr["BirthDate"]).ToString("dd-MM-yy");
            //        ws.Cells[row, 4].Value = dr["Gender"].ToString();
            //        ws.Cells[row, 5].Value = dr["Cast"];
            //        ws.Cells[row, 6].Value = dr["SubCast"];
            //        ws.Cells[row, 7].Value = dr["Category"];
            //        ws.Cells[row, 8].Value = dr["RollNo"];
            //        ws.Cells[row, 9].Value = dr["SurName"];
            //        ws.Cells[row, 10].Value = dr["Name"];
            //        ws.Cells[row, 11].Value = dr["FatherName"];

            //        // Apply Student Font
            //        Excel.Range rowRange = ws.Range[ws.Cells[row, 1], ws.Cells[row, 11 + daysInMonth]];
            //        rowRange.Font.Name = "Arial";
            //        rowRange.Font.Size = 10;

            //        // Zebra Striping (Alternate BG)
            //        rowRange.Interior.Color = (row % 2 == 0) ? Color.LightGray : Color.White;
            //        rowRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

            //        row++;
            //    }

            //    ws.Columns.AutoFit();

            //    // ---------------- Save ----------------
            //    SaveFileDialog sfd = new SaveFileDialog();
            //    sfd.Filter = "Excel Files|*.xlsx";
            //    sfd.FileName = "Attendance_" + cbClass.Text + "_" + year + "_" + month + ".xlsx";

            //    if (sfd.ShowDialog() == DialogResult.OK)
            //    {
            //        string filePath = sfd.FileName;
            //        wb.SaveAs(filePath);
            //        wb.Close();
            //        xlApp.Quit();

            //        System.Diagnostics.Process.Start(filePath);
            //        MessageBox.Show("Attendance Register exported successfully:\n" + filePath);
            //    }
            //    else
            //    {
            //        wb.Close(false);
            //        xlApp.Quit();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}
            //finally
            //{
            //    ReleaseObject(ws);
            //    ReleaseObject(wb);
            //    ReleaseObject(xlApp);
            //    GC.Collect();
            //    GC.WaitForPendingFinalizers();
            //}




            // ---- Validation ----
            if (cbShift.SelectedIndex < 0)
            {
                MessageBox.Show("Please select Shift.");
                return;
            }
            if (cbClass.SelectedIndex < 0)
            {
                MessageBox.Show("Please select Class.");
                return;
            }
            if (cbYear.SelectedIndex < 0)
            {
                MessageBox.Show("Please select Year.");
                return;
            }
            if (cbMonth.SelectedIndex < 0)
            {
                MessageBox.Show("Please select Month.");
                return;
            }
            if (!(rbGuj.Checked || rbEng.Checked))
            {
                MessageBox.Show("Please select Language (Gujarati / English).");
                return;
            }
            if (!(rbShift.Checked || rbDept.Checked || rbGender.Checked || rbSurname.Checked || rbCategory.Checked || rbClass.Checked || rbSchool.Checked))
            {
                MessageBox.Show("Please select Sorting Option.");
                return;
            }
            if (!(rbRollYes.Checked || rbRollNo.Checked))
            {
                MessageBox.Show("Please select RollNo update option (Yes / No).");
                return;
            }

            Excel.Application xlApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            try
            {
                int year = Convert.ToInt32(cbYear.SelectedItem);
                int month = cbMonth.SelectedIndex + 1;
                int daysInMonth = DateTime.DaysInMonth(year, month);

                // Gujarati weekday names
                string[] gujDays = { "રવિ", "સોમ", "મંગળ", "બુધ", "ગુરુ", "શુક્ર", "શનિ" };

                // --- Student Query ---
                // Language wise field
                string surnameField = rbGuj.Checked ? "SurNameGj" : "SurName";
                string nameField = rbGuj.Checked ? "NameGj" : "Name";
                string fatherField = rbGuj.Checked ? "FatherNameGj" : "FatherName";

                // Sorting Condition
                string orderBy = "RollNo"; // default
                //if (rbShift.Checked) orderBy = "ShiftName";   // ✅ shift name
                //if (rbDept.Checked) orderBy = "Division"; // ✅ dept name
                //else if (rbGender.Checked) orderBy = "Gender";
                //else if (rbSurname.Checked) orderBy = surnameField;
                //else if (rbCategory.Checked) orderBy = "Category";
                //else if (rbClass.Checked) orderBy = "ClassName"; // ✅ class name

                // Student Query with ClassName & ShiftName
                string query = "SELECT ReDgNo, GRNumber, BirthDate, Gender, Religious, SubCast, Category, RollNo, "
                                + surnameField + " AS SurName, "
                                + nameField + " AS Name, "
                                + fatherField + " AS FatherName " +
                                "FROM StudentMaster " +
                                //"WHERE ShiftName='" + cbShift.Text + "' " +
                                "WHERE CurrentClass='" + cbClass.Text + "' " +
                                "ORDER BY " + orderBy;


                SqlDataAdapter da = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No students found for selected criteria.");
                    return;
                }

                    
                // --- Excel Start ---
                xlApp = new Excel.Application();
                wb = xlApp.Workbooks.Add(Type.Missing);
                ws = (Excel.Worksheet)wb.ActiveSheet;
                ws.Name = "Attendance Register";

                // ---------------- Title ----------------
                Excel.Range titleCell = ws.Range["A1", "F1"];
                titleCell.Merge();
                titleCell.Value = "મોનાર્ક શૈક્ષણિક સંકુલ";
                titleCell.Font.Name = "Shruti";
                titleCell.Font.Size = 18;
                titleCell.Font.Bold = true;
                //titleCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // ---------------- Month ----------------
                Excel.Range monthCell = ws.Range["V1", "AC1"];
                monthCell.Merge();
                monthCell.Value = "Month : " + new DateTime(year, month, 1).ToString("MMMM-yyyy");
                monthCell.Font.Name = "Arial";
                monthCell.Font.Size = 12;
                monthCell.Font.Bold = true;

                // ---------------- Teacher ----------------
                Excel.Range teacherCell = ws.Range["AF1", "AK1"];
                teacherCell.Merge();
                teacherCell.Value = "Teacher’s Name : ____________";
                teacherCell.Font.Name = "Calibri";
                teacherCell.Font.Size = 11;
                teacherCell.Font.Bold = true;

                // ---------------- Subtitle ----------------
                Excel.Range subtitleCell = ws.Range["A2", "G2"];
                subtitleCell.Merge();
                subtitleCell.Value = "વિદ્યાર્થીઓનો ક્લાસ વાઈજ ડેટા";
                subtitleCell.Font.Name = "Shruti";
                subtitleCell.Font.Size = 12;

                // ---------------- Class Info ----------------
                Excel.Range classCell = ws.Range["I2", "K2"];
                classCell.Merge();
                classCell.Value = "Class : " + cbClass.Text;
                classCell.Font.Name = "Arial";
                classCell.Font.Size = 12;
                classCell.Font.Bold = true;

                // ---------------- Headers ----------------
                string[] headers = { "રજી નં.", "જર નં.", "જન્મ તા.", "જાતિ", "ધર્મ", "જ્ઞાતિ", "કેટેગરી", "રોલ નં.", "અટક", "નામ", "પિતાનું નામ" };
                for (int i = 0; i < headers.Length; i++)
                {
                    Excel.Range mergeHeader = ws.Range[ws.Cells[3, i + 1], ws.Cells[4, i + 1]];
                    mergeHeader.Merge();
                    mergeHeader.Font.Name = "Shruti";
                    mergeHeader.Font.Size = 10;
                    mergeHeader.Font.Bold = true;
                    mergeHeader.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    mergeHeader.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    mergeHeader.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    mergeHeader.Interior.Color = Color.White; // Header BG hamesha white

                    string headerText = headers[i];
                    string finalText = headerText;

                    if (headerText == "જાતિ")
                    {
                        finalText = headerText;
                        mergeHeader.Orientation = 90; // vertical text
                        //mergeHeader.WrapText = true;
                    }
                    else if (headerText == "રજી નં.")
                    {
                        finalText = "રજી\nનં.";
                        mergeHeader.WrapText = true;
                    }
                    else if (headerText == "જર નં.")
                    {
                        finalText = "જર\nનં.";
                        mergeHeader.WrapText = true;
                    }
                    else if (headerText == "કેટેગરી")
                    {
                        finalText = "કેટે\nગરી";
                        mergeHeader.WrapText = true;
                    }
                    else if (headerText == "રોલ નં.")
                    {
                        finalText = "રોલ\nનં.";
                        mergeHeader.WrapText = true;
                    }

                    mergeHeader.Value = finalText;
                }


                // ---------------- Day Headers ----------------
                for (int d = 1; d <= daysInMonth; d++)
                {
                    DateTime currentDate = new DateTime(year, month, d);
                    string dayName = gujDays[(int)currentDate.DayOfWeek];

                    // Day Name (Gujarati, Bold, Vertical)
                    Excel.Range dayCell = ws.Cells[3, d + 11];
                    dayCell.Value = dayName;
                    dayCell.Font.Name = "Shruti";
                    dayCell.Font.Size = 9;
                    dayCell.Font.Bold = true;
                    dayCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    dayCell.Orientation = 90;
                    dayCell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    // Date Number (English, Bold)
                    Excel.Range dateCell = ws.Cells[4, d + 11];
                    dateCell.Value = d;
                    dateCell.Font.Name = "Arial";
                    dateCell.Font.Size = 9;
                    dateCell.Font.Bold = true;
                    dateCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    dateCell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                }

                // ---------------- Student Rows ----------------
                int row = 5;
                foreach (DataRow dr in dt.Rows)
                {
                    ws.Cells[row, 1].Value = dr["ReDgNo"];
                    ws.Cells[row, 2].Value = dr["GRNumber"];
                    ws.Cells[row, 3].Value = Convert.ToDateTime(dr["BirthDate"]).ToString("dd/MM/yy");
                    ws.Cells[row, 4].Value = dr["Gender"].ToString();
                    ws.Cells[row, 5].Value = dr["Religious"];
                    ws.Cells[row, 6].Value = dr["SubCast"];
                    //ws.Cells[row, 7].Value = dr["Category"];
                    ws.Cells[row, 7].Value = dr["Category"];
                    ws.Cells[row, 8].Value = dr["RollNo"];
                    ws.Cells[row, 9].Value = dr["SurName"];
                    ws.Cells[row, 10].Value = dr["Name"];
                    ws.Cells[row, 11].Value = dr["FatherName"];


                    // ---- Attendance columns ----
                    for (int d = 1; d <= daysInMonth; d++)
                    {
                        DateTime currentDate = new DateTime(year, month, d);

                        if (currentDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            // Sunday → chhutti (all students will get "||")
                            ws.Cells[row, d + 11].Value = "||";
                            ws.Cells[row, d + 11].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        }
                        else
                        {
                            // Other days → blank cell
                            ws.Cells[row, d + 11].Value = "";
                        }
                        ws.Cells[row, d + 11].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    }


                    // Apply Student Font
                    Excel.Range rowRange = ws.Range[ws.Cells[row, 1], ws.Cells[row, 11 + daysInMonth]];
                    rowRange.Font.Name = "Arial";
                    rowRange.Font.Size = 10;

                    // Zebra Striping (Alternate BG)
                    rowRange.Interior.Color = (row % 2 != 0) ? Color.LightGray : Color.White;
                    rowRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    row++;
                    //ws.Columns.AutoFit();
                }

                // ================= EXTRA 3 BLANK ROWS =================
                for (int i = 0; i < 3; i++)
                {

                    // ---- Attendance columns ----
                    for (int d = 1; d <= daysInMonth; d++)
                    {
                        DateTime currentDate = new DateTime(year, month, d);

                        if (currentDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            // Sunday → chhutti (all students will get "||")
                            ws.Cells[row, d + 11].Value = "||";
                            ws.Cells[row, d + 11].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        }
                        else
                        {
                            // Other days → blank cell
                            ws.Cells[row, d + 11].Value = "";
                        }
                        ws.Cells[row, d + 11].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    }

                    Excel.Range blankRow = ws.Range[ws.Cells[row, 1], ws.Cells[row, 11 + daysInMonth]];
                    //blankRow.Interior.Color = Color.White;   // Blank row white BG
                    blankRow.Interior.Color = (row % 2 != 0) ? Color.LightGray : Color.White;
                    blankRow.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    row++;
                }
                

                // --- Final Summary Rows like image ---
                string[] summaryLabels = { "હાજર સંખ્યા", "ગેરહાજર સંખ્યા", "કુલ સંખ્યા" };
                int labelColStart = 10; // J
                int labelColEnd = 11;   // K

                // 🔹 Merge LEFT side (A–I) for ALL 3 rows together
                Excel.Range mergeGray = ws.Range[ws.Cells[row, 1], ws.Cells[row + 2, 9]];
                mergeGray.Merge();
                mergeGray.Interior.Color = Color.LightGray;
                mergeGray.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                mergeGray.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                // Sirf outside border
                mergeGray.Borders.LineStyle = Excel.XlLineStyle.xlLineStyleNone;
                mergeGray.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin);

                // 🔹 Ab 3 rows ka loop → J–K me Gujarati text + attendance columns
                for (int i = 0; i < summaryLabels.Length; i++)
                {
                    // J–K merge with Gujarati label
                    Excel.Range labelCell = ws.Range[ws.Cells[row + i, labelColStart], ws.Cells[row + i, labelColEnd]];
                    labelCell.Merge();
                    labelCell.Value = summaryLabels[i];
                    labelCell.Font.Name = "Shruti";
                    labelCell.Font.Size = 11;
                    labelCell.Font.Bold = true;
                    labelCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    labelCell.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    labelCell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    // Attendance columns (L onwards)
                    for (int d = 1; d <= daysInMonth; d++)
                    {
                        Excel.Range cell = ws.Cells[row + i, d + 11]; // Column 12 onwards
                        DateTime currentDate = new DateTime(year, month, d);

                        if (currentDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            cell.Value = "||"; // holiday
                            cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        }
                        else
                        {
                            cell.Value = "";
                        }

                        cell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        cell.Interior.Color = (i % 2 == 0) ? Color.LightGray : Color.White;
                    }
                }

                row += 3; // jump after summary rows
                ws.Columns.AutoFit();


                // ---------------- Save ----------------
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Files|*.xlsx";
                sfd.FileName = "Attendance_" + cbClass.Text + "_" + year + "_" + month + ".xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = sfd.FileName;
                    wb.SaveAs(filePath);
                    wb.Close();
                    xlApp.Quit();

                    System.Diagnostics.Process.Start(filePath);
                    MessageBox.Show("Attendance Register exported successfully:\n" + filePath);
                }
                else
                {
                    wb.Close(false);
                    xlApp.Quit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                ReleaseObject(ws);
                ReleaseObject(wb);
                ReleaseObject(xlApp);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }


        
        private void ReleaseObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch { obj = null; }
        }

        private void rbShift_CheckedChanged(object sender, EventArgs e)
        {

            if (cbShift.SelectedIndex != -1)
            {
                int shiftId = Convert.ToInt32(cbShift.SelectedValue);
                LoadClassListByShift(shiftId);
            }

            //if (rbShift.Checked)
            //{
                //LoadShiftFromClassMaster();
                //LoadClassFromClassMaster();
            //}
        }   

        private void rbDept_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDept.Checked)
            {
                LoadShiftFromStudentMaster();
                LoadClassFromStudentMaster();
            }
        }

        private void cbShift_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if (cbShift.SelectedIndex != -1)
            //{
            //    int shiftId = Convert.ToInt32(cbShift.SelectedValue);
            //    LoadClassListByShift(shiftId);
            //}


        }





    }
}
