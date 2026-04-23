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
using System.IO;
using System.Drawing.Printing;

namespace School_Management_System
{
    public partial class FrmHwMargin : Form
    {
        SqlConnection Con;
        SqlCommand Cmd;
        SqlDataAdapter DA;
        DataSet DS;
        SqlDataReader dtr;
        Boolean add, edit;
        private string homeworkText;
        private string SelectedClassName;
        public static bool IsDefaultChecked;
        public static bool IsDynamicChecked;
        public static int RowsValue;
        public static int ColsValue;


        //public FrmHwMargin()
        //{
        //    InitializeComponent();
        //    Con = new SqlConnection(global.ConnectionString);
        //    add = false;
        //    edit = false;
        //    txtReadOnly(true);
        //    ShowData();
        //    dataGridView1.ClearSelection();
        //}


        public FrmHwMargin(string hwText, string className)
        {
            InitializeComponent();
            Con = new SqlConnection(global.ConnectionString);
            add = false;
            edit = false;
            homeworkText = hwText;
            txtHWPreview.Text = hwText;
            SelectedClassName = className; 
            txtReadOnly(true);
            ShowData();
            SetGridColumnHeaders();
            dataGridView1.ClearSelection();

            //Default data
            //nudFontSize.Value = 12;
            //nudTopMargin.Value = 0;
            //nudBottomMargin.Value = 0;
            //nudLeftMargin.Value = 0;
            //nudRightMargin.Value = 0;
            //nudRows.Value = 2;
            //nudColumns.Value = 3;

            cmbPaperSize.Items.Clear();
            cmbPaperSize.Items.AddRange(new string[] { "A4", "A3", "A5", "Letter", "Legal" });
            cmbPaperSize.SelectedIndex = 0; // default A4

            txtHwMarginId.BackColor = Color.White;

            cbRowsType.Items.Clear();
            cbRowsType.Items.AddRange(new string[] { "Default", "ByStudent", "Static" });
            //cbRowsType.SelectedIndex = 0;
        }

        //private string GetDefaultClass()
        //{
        //    string className = "";
        //    try
        //    {
        //        Con.Open();
        //        string query = "SELECT TOP 1 ClassName FROM ClassDefaults ORDER BY DefaultId DESC";
        //        Cmd = new SqlCommand(query, Con);
        //        object val = Cmd.ExecuteScalar();
        //        if (val != null)
        //            className = val.ToString();
        //    }
        //    finally
        //    {
        //        Con.Close();
        //    }
        //    return className;
        //}

        //public static class HWSettings
        //{
        //    public static bool IsDefault { get; set; }
        //    public static bool IsDynamic { get; set; }
        //    public static int Rows { get; set; }
        //    public static string RowsType { get; set; }
        //    public static int Cols { get; set; }
        //    public static decimal FontSize { get; set; }
        //    public static decimal MTop { get; set; }
        //    public static decimal MBottom { get; set; }
        //    public static decimal MLeft { get; set; }
        //    public static decimal MRight { get; set; }
        //    public static string PaperSize { get; set; }
        //    public static decimal HSpace { get; set; }
        //    public static decimal VSpace { get; set; }
        //}

        // -------- helpers to keep code clean ----------
        //private void PushFormValuesToHWSettings()
        //{
        //    HWSettings.FontSize = nudFontSize.Value;
        //    HWSettings.MTop = nudTopMargin.Value;
        //    HWSettings.MBottom = nudBottomMargin.Value;
        //    HWSettings.MLeft = nudLeftMargin.Value;
        //    HWSettings.MRight = nudRightMargin.Value;
        //    HWSettings.Cols = (int)nudColumns.Value;
        //    HWSettings.Rows = (int)nudRows.Value;
        //    HWSettings.RowsType = cbRowsType.Text;
        //    HWSettings.PaperSize = cmbPaperSize.Text;
        //    HWSettings.HSpace = nudHorizontalSpace.Value;
        //    HWSettings.VSpace = nudVerticalSpace.Value;
        //}

        //private int GetRowsForOutput(int cols)
        //{
        //    if (cols <= 0) return 1;

        //    if (chkDefaultRow.Checked)         // mode: Default
        //    {
        //        int totalStudents = GetDefaultStudentCount(SelectedClassName);
        //        if (totalStudents <= 0) 
        //            totalStudents = 1;
        //        return (int)Math.Ceiling((double)totalStudents / cols);
        //    }
        //    else if (chkDynamicRows.Checked)   // mode: Dynamic
        //    {
        //        int totalStudents = GetStudentCount(SelectedClassName);
        //        if (totalStudents <= 0) 
        //            totalStudents = 1;
        //        return (int)Math.Ceiling((double)totalStudents / cols);
        //    }
        //    else                               // mode: Manual (nudRows)
        //    {
        //        return (int)nudRows.Value;      
        //    }
        //}
        // ------------------------------------------------


        private int GetDefaultStudentCount(string className)
        {
            int count = 0;
            try
            {
                Con.Open();
                // Latest DefaultDate/Id ke hisaab se students nikalna
                string q = "SELECT TOP 1 TotalStudents FROM ClassDefaults " +
                           "WHERE ClassName='" + className + "' " +
                           "ORDER BY DefaultDate DESC, DefaultId DESC";
                Cmd = new SqlCommand(q, Con);
                object obj = Cmd.ExecuteScalar();
                if (obj != null && obj != DBNull.Value)
                    count = Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching default student count: " + ex.Message);
            }
            finally
            {
                if (Con.State == ConnectionState.Open) 
                    Con.Close();
            }
            return count;
        }

        // frmHwMargin.cs
        public string SelectedRowType
        {
            get { return cbRowsType.Text.Trim(); }
        }



        private void FrmHwMargin_Load(object sender, EventArgs e)
        {
            resize1();
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            dataGridView1.Enabled = true;
            dataGridView1.ClearSelection();
            txtReadOnly(true);

            nudFontSize.DecimalPlaces = 2;
            nudTopMargin.DecimalPlaces = 2;
            nudBottomMargin.DecimalPlaces = 2;
            nudLeftMargin.DecimalPlaces = 2;
            nudRightMargin.DecimalPlaces = 2;

            nudHorizontalSpace.DecimalPlaces = 2;
            nudVerticalSpace.DecimalPlaces = 2;

            LoadDefaultRecord();
            //PushFormValuesToHWSettings();
            btnAdd.Focus();
        }

        public void resize1()
        {
            this.Width = global.screenWidth - 200;
            this.Height = global.screenHeight - 50;

            panel1.Width = this.Width - 10;
            panel1.Height = this.Height - 410;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        //private int GetStudentCount(string className)
        //{
        //    int count = 0;
        //    try
        //    {
        //        Con.Open();
        //        string query = "SELECT COUNT(*) FROM StudentMaster WHERE CurrentClass = (SELECT ClassName FROM ClassMaster WHERE ClassName='" + className + "')";
        //        Cmd = new SqlCommand(query, Con);
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


        private void LoadDefaultRecord()
        {
            try
            {
                Con.Open();
                string getDefault = "SELECT TOP 1 * FROM HomeworkMargin WHERE IsDefault='Y' ORDER BY HwMarginId DESC";
                Cmd = new SqlCommand(getDefault, Con);
                dtr = Cmd.ExecuteReader();

                if (dtr.Read())
                {
                    txtHwMarginId.Text = dtr["HwMarginId"].ToString();
                    nudFontSize.Value = Convert.ToDecimal(dtr["Size"]);
                    nudTopMargin.Value = Convert.ToDecimal(dtr["MTop"]);
                    nudBottomMargin.Value = Convert.ToDecimal(dtr["MBottom"]);
                    nudLeftMargin.Value = Convert.ToDecimal(dtr["MLeft"]);
                    nudRightMargin.Value = Convert.ToDecimal(dtr["MRight"]);
                    nudColumns.Value = Convert.ToDecimal(dtr["Columns"]);
                    nudRows.Value = Convert.ToDecimal(dtr["Rows"]);
                    cbRowsType.Text = dtr["RowType"].ToString();
                    cmbPaperSize.Text = dtr["PaperSize"].ToString();
                    nudHorizontalSpace.Value = Convert.ToDecimal(dtr["HorizontalSpace"]);
                    nudVerticalSpace.Value = Convert.ToDecimal(dtr["VerticalSpace"]);
                }
                dtr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading default record: " + ex.Message);
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                    Con.Close();
            }
        }

        private void ShowData()
        {   //Diplay data in gridview 
            String squery = "SELECT HwMarginId, Size, MTop, MBottom, MLeft, MRight, Columns, Rows, RowType,PaperSize, IsDefault, HorizontalSpace, VerticalSpace FROM HomeworkMargin ORDER BY HwMarginId DESC";
            DA = new SqlDataAdapter(squery, Con);
            DS = new DataSet();
            DA.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            dataGridView1.ClearSelection();
        }

        private void txtReadOnly(Boolean ans)
        {
            NumericUpDown[] nud = { nudFontSize, nudTopMargin, nudBottomMargin, nudLeftMargin, nudRightMargin, nudColumns, nudRows, nudHorizontalSpace, nudVerticalSpace };
            foreach (var ctrl in nud)
            {
                ctrl.Enabled = !ans;
                ctrl.BackColor = Color.White;
            }
            cmbPaperSize.Enabled = !ans;
            cbRowsType.Enabled = !ans;
        }

        private void txtClear()
        {
            txtHwMarginId.Text = "";
            nudFontSize.Value = 0;
            nudTopMargin.Value = 0;
            nudBottomMargin.Value = 0;
            nudLeftMargin.Value = 0;
            nudRightMargin.Value = 0;
            nudColumns.Value = 0;
            nudRows.Value = 0;
            cbRowsType.SelectedIndex = -1;
            nudHorizontalSpace.Value = 0;
            nudVerticalSpace.Value = 0;
            txtHWPreview.Text = "";
            cmbPaperSize.SelectedIndex = -1;
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


        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            add = true;
            edit = false;
            txtReadOnly(false);

            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            dataGridView1.Enabled = false;
            nudFontSize.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            add = false;
            edit = true;
            txtReadOnly(false);
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            dataGridView1.Enabled = false;
            nudFontSize.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (nudFontSize.Value > 0 && nudRows.Value > 0 && nudColumns.Value > 0 && nudTopMargin.Value > 0 && nudBottomMargin.Value > 0 && nudLeftMargin.Value > 0 && nudRightMargin.Value > 0)
            if (nudFontSize.Value > 0 && nudRows.Value > 0 && nudColumns.Value > 0)
            {
                Con.Open();
                Cmd = new SqlCommand("UPDATE HomeworkMargin SET IsDefault='N'", Con);
                Cmd.ExecuteNonQuery();

                if (add == true)
                {
                    string inquery = "INSERT INTO HomeworkMargin(Size,MTop,MBottom,MLeft,MRight,Columns,Rows,RowType,PaperSize,HorizontalSpace,VerticalSpace,IsDefault) VALUES (" + nudFontSize.Value + "," + nudTopMargin.Value + "," + nudBottomMargin.Value + "," + nudLeftMargin.Value + "," + nudRightMargin.Value + "," + nudColumns.Value + "," + nudRows.Value + ",'" + cbRowsType.Text + "','" + cmbPaperSize.Text + "'," + nudHorizontalSpace.Value + "," + nudVerticalSpace.Value + ",'Y')";
                    Cmd = new SqlCommand(inquery, Con);
                    Cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Added...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtClear();
                }
                else if (edit == true)
                {
                    string upquery = "UPDATE HomeworkMargin SET Size=" + nudFontSize.Value + ",MTop=" + nudTopMargin.Value + ",MBottom=" + nudBottomMargin.Value + ",MLeft=" + nudLeftMargin.Value + ",MRight=" + nudRightMargin.Value + ",Columns=" + nudColumns.Value + ",Rows=" + nudRows.Value + ",RowType='" + cbRowsType.Text + "' ,PaperSize='" + cmbPaperSize.Text + "'" + ",HorizontalSpace=" + nudHorizontalSpace.Value + ",VerticalSpace=" + nudVerticalSpace.Value + ",IsDefault='Y' WHERE HwMarginId=" + txtHwMarginId.Text;
                    Cmd = new SqlCommand(upquery, Con);
                    Cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Updated...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtClear();
                }
                Con.Close();
                ShowData();
                btnCancel.PerformClick();
            }
            else
            {
                MessageBox.Show("Fill the Data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtReadOnly(true);
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            dataGridView1.Enabled = true;
            dataGridView1.ClearSelection();
            btnAdd.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtHwMarginId.Text != "")
            {
                if (MessageBox.Show("Do You Want To Delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Con.Open();
                    Cmd = new SqlCommand("DELETE FROM HomeworkMargin WHERE HwMarginId=" + txtHwMarginId.Text, Con);
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

        private void btnExcel_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(homeworkText))
            {
                MessageBox.Show("Homework text is empty!");
                return;
            }

            //PushFormValuesToHWSettings();

            decimal fontSize = nudFontSize.Value;
            decimal mTop = nudTopMargin.Value;
            decimal mBottom = nudBottomMargin.Value;
            decimal mLeft = nudLeftMargin.Value;
            decimal mRight = nudRightMargin.Value;

            int cols = (int)nudColumns.Value;
            int rows = 1; // Default fallback

            // 🔹 Rows calculation based on cbRowsType
            string type = cbRowsType.Text.Trim();
            if (type == "Static") // ClassDefaults table
            {
                try
                {
                    Con.Open();
                    string q = "SELECT TOP 1 TotalStudents FROM ClassDefaults WHERE ClassName='" + SelectedClassName + "' ORDER BY DefaultDate DESC, DefaultId DESC";
                    Cmd = new SqlCommand(q, Con);
                    object val = Cmd.ExecuteScalar();
                    int totalStudents = (val != null && val != DBNull.Value) ? Convert.ToInt32(val) : 1;
                    rows = (int)Math.Ceiling((double)totalStudents / cols);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching static students: " + ex.Message);
                }
                finally { if (Con.State == ConnectionState.Open) Con.Close(); }
            }
            else if (type == "ByStudent") // StudentMaster table
            {
                try
                {
                    Con.Open();
                    string q = "SELECT COUNT(*) FROM StudentMaster WHERE CurrentClass='" + SelectedClassName + "'";
                    Cmd = new SqlCommand(q, Con);
                    int totalStudents = Convert.ToInt32(Cmd.ExecuteScalar());
                    if (totalStudents <= 0) totalStudents = 1;
                    rows = (int)Math.Ceiling((double)totalStudents / cols);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching student count: " + ex.Message);
                }
                finally { if (Con.State == ConnectionState.Open) Con.Close(); }
            }
            else if (type == "Default") // nudRows value ko use kare
            {
                rows = (int)nudRows.Value;
            }

            if (cols <= 0 || rows <= 0)
            {
                MessageBox.Show("Rows/Columns must be greater than 0.");
                return;
            }

            // Baaki Excel generation code wahi purana rahega
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

                double paperWidthInches = GetPaperWidth(cmbPaperSize.Text);
                double charsPerInch = 12.8;
                double usableInches = paperWidthInches;
                double totalChars = usableInches * charsPerInch;
                double contentWidth = (totalChars - ((double)nudHorizontalSpace.Value * (cols - 1))) / (double)cols;
                if (contentWidth <= 0) contentWidth = 5;

                int startRow = 1;

                for (int r = 0; r < rows; r++)
                {
                    int actualRow = startRow + (r * 2);
                    for (int b = 0; b < cols; b++)
                    {
                        int col = (b * 2) + 1;
                        worksheet.Cells[actualRow, col] = homeworkText;
                    }
                }

                worksheet.Cells.Font.Size = (int)fontSize;
                worksheet.Cells.WrapText = true;
                worksheet.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                worksheet.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                for (int c = 1; c <= (cols * 2) - 1; c++)
                    worksheet.Columns[c].ColumnWidth = (c % 2 == 1) ? contentWidth : (double)nudHorizontalSpace.Value;

                //for (int r = 1; r <= (rows * 2) - 1; r++)
                //    worksheet.Rows[r].RowHeight = (r % 2 == 1) ? worksheet.Rows[r].RowHeight : ColumnWidthToRowHeight((double)nudHorizontalSpace.Value);

                for (int r = 1; r <= (rows * 2) - 1; r++)
                {
                    if (r % 2 == 1) // content row
                    {
                        Excel.Range rowRange = worksheet.Rows[r];
                        rowRange.WrapText = true;
                        rowRange.AutoFit(); // <-- AutoFit content
                    }
                    else // separator row
                    {
                        worksheet.Rows[r].RowHeight = ColumnWidthToRowHeight((double)nudHorizontalSpace.Value);
                    }
                }


                worksheet.PageSetup.LeftMargin = excelApp.InchesToPoints((double)mLeft / 10);
                worksheet.PageSetup.RightMargin = excelApp.InchesToPoints((double)mRight / 10);
                worksheet.PageSetup.TopMargin = excelApp.InchesToPoints((double)mTop / 10);
                worksheet.PageSetup.BottomMargin = excelApp.InchesToPoints((double)mBottom / 10);
                worksheet.PageSetup.Zoom = false;
                worksheet.PageSetup.FitToPagesWide = 1;
                worksheet.PageSetup.FitToPagesTall = false;

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtClear();
            btnAdd.Focus();
        }


        // Desi helper function (VS2012 compatible)
        private decimal SafeDecimal(object val)
        {
            if (val == null || val == DBNull.Value) return 0;
            decimal d;
            if (decimal.TryParse(val.ToString(), out d))
                return d;
            else
                return 0;
        }



        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {
                string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                Con.Open();
                Cmd = new SqlCommand("UPDATE HomeworkMargin SET IsDefault='N'; UPDATE HomeworkMargin SET IsDefault='Y' WHERE HwMarginId=" + id, Con);
                Cmd.ExecuteNonQuery();
                Con.Close();

                MessageBox.Show("Record set as Default!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowData();
            }

        }

        private void SetGridColumnHeaders()
        {
            dataGridView1.Columns[0].HeaderText = "Margin ID";
            dataGridView1.Columns[1].HeaderText = "Font Size";
            dataGridView1.Columns[2].HeaderText = "Top";
            dataGridView1.Columns[3].HeaderText = "Bottom";
            dataGridView1.Columns[4].HeaderText = "Left";
            dataGridView1.Columns[5].HeaderText = "Right";
            dataGridView1.Columns[6].HeaderText = "Columns";
            dataGridView1.Columns[7].HeaderText = "Rows";
            dataGridView1.Columns[8].HeaderText = "RowsType";
            dataGridView1.Columns[9].HeaderText = "Paper Size";
            dataGridView1.Columns[10].HeaderText = "Default";
            dataGridView1.Columns[11].HeaderText = "H_Space";
            dataGridView1.Columns[12].HeaderText = "V_Space";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {
                txtReadOnly(true);
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnAdd.Focus();

                try
                {
                    txtHwMarginId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                    nudFontSize.Value = SafeDecimal(dataGridView1.CurrentRow.Cells[1].Value);
                    nudTopMargin.Value = SafeDecimal(dataGridView1.CurrentRow.Cells[2].Value);
                    nudBottomMargin.Value = SafeDecimal(dataGridView1.CurrentRow.Cells[3].Value);
                    nudLeftMargin.Value = SafeDecimal(dataGridView1.CurrentRow.Cells[4].Value);
                    nudRightMargin.Value = SafeDecimal(dataGridView1.CurrentRow.Cells[5].Value);
                    nudColumns.Value = SafeDecimal(dataGridView1.CurrentRow.Cells[6].Value);
                    nudRows.Value = SafeDecimal(dataGridView1.CurrentRow.Cells[7].Value);
                    cbRowsType.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                    cmbPaperSize.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();

                    // Index 9 = IsDefault (ignore karna hai)
                    nudHorizontalSpace.Value = SafeDecimal(dataGridView1.CurrentRow.Cells[11].Value);
                    nudVerticalSpace.Value = SafeDecimal(dataGridView1.CurrentRow.Cells[12].Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error filling form: " + ex.Message);
                }
            }
        }
        
        private void nudColumns_ValueChanged(object sender, EventArgs e)
        {
            //HWSettings.Cols = (int)nudColumns.Value;
        }

        private void nudRows_ValueChanged(object sender, EventArgs e)
        {
           //HWSettings.Rows = (int)nudRows.Value;
        }


    }
}
