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
    public partial class FrmFee : UserControl
    {
        public static FrmFee instance;
        public static string SelectedReceiptNo;


        SqlConnection Con;
        SqlCommand Cmd;
        SqlDataAdapter DA;
        DataSet DS;
        Boolean add, edit;

        public FrmFee()
        {
            InitializeComponent();
            instance = this;
            Con = new SqlConnection(global.ConnectionString);
            add = false;
            edit = false;
            txtReadOnly(true);
            btnSave.Enabled = false;
            txtUserID.ReadOnly = true;
            txtUserID.BackColor = Color.White;
            ShowData();
            txtUserID.Text = global.User;
            //AddDataInCombo();
            dataGridView1.ClearSelection();
        }

        public void resize1()
        {
            panel2.Width = global.screenWidth - 240;
            panel2.Height = global.screenHeight - 395;
        }

        private void FrmFee_Load(object sender, EventArgs e)
        {
            resize1();

            //Button Control (Enable/Disable)
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            //btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            txtClear();
            txtReadOnly(true);
            txtSearch.Enabled = true;
            //Gridview control
            dataGridView1.Enabled = true;
            dataGridView1.ClearSelection();
            ShowData();
            LoadClassCombo();
            LoadDiscountCombo();
            LoadFeesYearCombo();
            btnAdd.Focus();


        }

        private void ShowData()
        {   //Diplay data in gridview 
            String squery = "select * from FeesMaster WHERE ReceiptNo like '%" + txtSearch.Text + "%' OR FeeDate like '%" + txtSearch.Text + "%' OR StudentId like '%" + txtSearch.Text + "%' OR CurrentClass like '%" + txtSearch.Text + "%' OR PaidBy like '%" + txtSearch.Text + "%' OR PaymentMode like '%" + txtSearch.Text + "%' OR Details like '%" + txtSearch.Text + "%' OR Amount like '%" + txtSearch.Text + "%' OR Discount like '%" + txtSearch.Text + "%' ORDER BY FeeDate DESC";
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
            dtpFeeDate.Enabled = !ans;
            cbPaymentMode.Enabled = !ans;
            cbCurrentClass.Enabled = !ans;
            cbStudentId.Enabled = !ans;
            cbDiscount.Enabled = !ans;
            TextBox[] tx = { txtDetails, txtDetails, txtAmount };
            for (int x = 0; x < 3; x++)
            {
                tx[x].ReadOnly = ans;
                tx[x].BackColor = Color.White;
            }
        }

        private void txtClear()
        {
            dtpFeeDate.Value = DateTime.Now;
            cbPaymentMode.SelectedIndex = -1;
            cbCurrentClass.SelectedIndex = -1;
            cbStudentId.SelectedIndex = -1;
            cbDiscount.SelectedIndex = -1;
            txtDetails.Text = "";
            txtAmount.Text = "";
            txtPaidBy.Text = "";
            txtReceiptNo.Text = "";
            cbTerm.SelectedIndex = -1;
            cbYear.SelectedIndex = -1;
            txtFullName.Text = "";
            txtHPhone.Text = "";
            txtFPhone.Text = "";
        }

        private void LoadClassCombo()
        {
            try
            {
                string query = "SELECT ClassId, ClassName FROM ClassMaster ORDER BY ClassName";
                SqlDataAdapter da = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cbCurrentClass.DataSource = dt;
                cbCurrentClass.DisplayMember = "ClassName"; // Jo user ko dikhega
                cbCurrentClass.ValueMember = "ClassId";     // Jo backend me use hoga
                cbCurrentClass.SelectedIndex = -1;          // Initially blank
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading classes: " + ex.Message);
            }
        }

        private void LoadStudentCombo(int classId)
        {
            //try
            //{
            //    string query = "SELECT ReDgNo, Name FROM StudentMaster WHERE CurrentClass = (SELECT ClassName FROM ClassMaster WHERE ClassId=" + classId + ") ORDER BY Name";
            //    SqlDataAdapter da = new SqlDataAdapter(query, Con);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);

            //    cbStudentId.DataSource = null;  // reset
            //    cbStudentId.DataSource = dt;
            //    cbStudentId.DisplayMember = "Name";
            //    cbStudentId.ValueMember = "ReDgNo";
            //    cbStudentId.SelectedIndex = -1;


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error loading students: " + ex.Message);
            //}

            try
            {
                string query = "SELECT ReDgNo, Name FROM StudentMaster WHERE CurrentClass = (SELECT ClassName FROM ClassMaster WHERE ClassId=" + classId + ") ORDER BY Name";
                SqlDataAdapter da = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Combo reset properly
                cbStudentId.DataSource = null;
                cbStudentId.DisplayMember = "Name";
                cbStudentId.ValueMember = "ReDgNo";
                cbStudentId.DataSource = dt;
                cbStudentId.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message);
            }
        }

        private void LoadDiscountCombo()
        {
            try
            {
                //string query = "SELECT FeesTitle, Discount FROM FeesType WHERE status='A' AND Discount > 0 ORDER BY FeesTitle";
                string query = "SELECT FeesTitle, Discount FROM FeesType WHERE status='A' ORDER BY FeesTitle";
                SqlDataAdapter da = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cbDiscount.DataSource = null;
                cbDiscount.DataSource = dt;
                cbDiscount.DisplayMember = "FeesTitle";  // Combo me jo dikhega
                cbDiscount.ValueMember = "Discount";     // Backend me Discount value
                cbDiscount.SelectedIndex = -1;           // Initially blank
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading discount list: " + ex.Message);
            }
        }


        private void LoadFeesYearCombo()
        {
            try
            {
                string query = "SELECT FeesYear FROM FeesStructure WHERE status='A'";
                SqlDataAdapter da = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cbYear.DataSource = null;
                cbYear.DataSource = dt;
                cbYear.DisplayMember = "FeesYear";  // Combo me jo dikhega
                cbYear.ValueMember = "FeesYear";     // Backend me Discount value
                cbYear.SelectedIndex = -1;           // Initially blank
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading FeesYear list: " + ex.Message);
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Button Controls - [Add]
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnSave.Enabled = true;
            //btnDelete.Enabled = false;
            btnCancel.Enabled = true;
            txtClear();
            txtReadOnly(false);
            dataGridView1.Enabled = false;
            add = true;
            edit = false;
            txtReceiptNo.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtReceiptNo.Text != "")
            {
                txtReadOnly(false);
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnSave.Enabled = true;
                //btnDelete.Enabled = false;
                btnCancel.Enabled = true;
                dataGridView1.Enabled = false;
                add = false;
                edit = true;
                txtReceiptNo.Focus();
            }
            else
            {
                MessageBox.Show("Search Data To Edit...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbCurrentClass.Text != "" && cbStudentId.Text != "" && txtDetails.Text != "" && txtAmount.Text != "" && txtUserID.Text != "" && txtPaidBy.Text != "" && txtReceiptNo.Text != "" && cbPaymentMode.Text != "")
            {
                if (add == true)
                {
                    Con.Open();
                    string inquery = "INSERT INTO FeesMaster(FeeDate, StudentId, CurrentClass, PaidBy, PaymentMode, Details, Amount, Discount, FullName, HPhone, FPhone, Year, Term) VALUES('" + txtReceiptNo.Text + "','" + dtpFeeDate.Value.ToString("yyyy/MM/dd") + "','" + cbStudentId.SelectedValue + "','" + cbCurrentClass.Text + "','" + txtPaidBy.Text + "','" + cbPaymentMode.Text + "','" + txtDetails.Text + "','" + txtAmount.Text + "','" + cbDiscount.SelectedValue + "', '" + txtFullName.Text + "', " + txtHPhone.Text + ", " + txtFPhone.Text + ", '" + cbYear.Text + "', '" + cbTerm.Text + "')";
                    Cmd = new SqlCommand(inquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Record Added...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowData();
                    btnCancel.PerformClick();
                }

                else if (edit == true)
                {
                    Con.Open();
                    //string upquery = "UPDATE FeesMaster SET FeeDate='" + dtpFeeDate.Value.ToString("yyyy/MM/dd") + "', StudentId='" + cbStudentId.SelectedValue + "', CurrentClass='" + cbCurrentClass.SelectedValue + "', PaidBy='" + txtPaidBy.Text + "', PaymentMode='" + cbPaymentMode.Text + "', Details='" + txtDetails.Text + "', Amount='" + txtAmount.Text + "', Discount='" + cbDiscount.Text + "' WHERE ReceiptNo='" + txtReceiptNo.Text + "'";
                    string upquery = "UPDATE FeesMaster SET FeeDate='" + dtpFeeDate.Value.ToString("yyyy/MM/dd") + "', StudentId='" + cbStudentId.SelectedValue + "', CurrentClass='" + cbCurrentClass.Text + "', PaidBy='" + txtPaidBy.Text + "', PaymentMode='" + cbPaymentMode.Text + "', Details='" + txtDetails.Text + "', Amount='" + txtAmount.Text + "', Discount='" + cbDiscount.SelectedValue + "', FullName = '" + txtFullName.Text + "', HPhone=" + txtHPhone.Text + ", FPhone=" + txtFPhone.Text + ", Year='" + cbYear.Text + "', Term='" + cbTerm.Text + "'  WHERE ReceiptNo='" + txtReceiptNo.Text + "' ";
                    Cmd = new SqlCommand(upquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    ShowData();
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

        //private void //btnDelete_Click(object sender, EventArgs e)
        //{
        //    if (txtReceiptNo.Text != "")
        //    {
        //        DialogResult ds = MessageBox.Show("Do You Want To Delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //        if (ds == DialogResult.Yes)
        //        {
        //            Con.Open();
        //            string dquery = "delete from FeesMaster where ReceiptNo='" + txtReceiptNo.Text + "'";
        //            Cmd = new SqlCommand(dquery, Con);
        //            Cmd.ExecuteNonQuery();
        //            Con.Close();
        //            ShowData();
        //            txtClear();
        //            btnCancel.PerformClick();
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Nothing selected...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            //btnDelete.Enabled = false;
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


        private int GetClassIdFromName(string className)
        {
            try
            {
                string query = "SELECT ClassId FROM ClassMaster WHERE ClassName='" + className + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                    return Convert.ToInt32(dt.Rows[0]["ClassId"]);
            }
            catch { }
            return 0;
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            //{
            //    btnEdit.Enabled = true;
            //    //btnDelete.Enabled = true;
            //    btnAdd.Focus();

            //    try
            //    {
            //        txtReceiptNo.Text = dataGridView1.CurrentRow.Cells["ReceiptNo"].Value.ToString();
            //        dtpFeeDate.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["FeeDate"].Value);

            //        cbCurrentClass.SelectedValue = dataGridView1.CurrentRow.Cells["CurrentClass"].Value;
            //        cbStudentId.SelectedValue = dataGridView1.CurrentRow.Cells["StudentId"].Value;

            //        txtPaidBy.Text = dataGridView1.CurrentRow.Cells["PaidBy"].Value.ToString();
            //        cbPaymentMode.Text = dataGridView1.CurrentRow.Cells["PaymentMode"].Value.ToString();
            //        txtDetails.Text = dataGridView1.CurrentRow.Cells["Details"].Value.ToString();
            //        txtAmount.Text = dataGridView1.CurrentRow.Cells["Amount"].Value.ToString();

            //        // discount numeric hai, to SelectedValue lagana better hai:
            //        cbDiscount.SelectedValue = dataGridView1.CurrentRow.Cells["Discount"].Value;
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Error filling data: " + ex.Message);
            //    }
            //}


            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {
                try
                {
                    // Button states
                    btnEdit.Enabled = true;
                    btnAdd.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = true;

                    // Row data load
                    txtReceiptNo.Text = dataGridView1.CurrentRow.Cells["ReceiptNo"].Value.ToString();

                    if (dataGridView1.CurrentRow.Cells["FeeDate"].Value != DBNull.Value)
                        dtpFeeDate.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["FeeDate"].Value);
                    else
                        dtpFeeDate.Value = DateTime.Now;

                    // Combo values
                    cbCurrentClass.Text = dataGridView1.CurrentRow.Cells["CurrentClass"].Value.ToString();

                    // Load students of that class (so that cbStudentId.SelectedValue work kare)
                    int classId = GetClassIdFromName(cbCurrentClass.Text);
                    if (classId > 0)
                        LoadStudentCombo(classId);

                    // StudentId assign (safe way)
                    string studentId = dataGridView1.CurrentRow.Cells["StudentId"].Value.ToString();
                    cbStudentId.SelectedValue = studentId;

                    // Other fields
                    txtPaidBy.Text = dataGridView1.CurrentRow.Cells["PaidBy"].Value.ToString();
                    cbPaymentMode.Text = dataGridView1.CurrentRow.Cells["PaymentMode"].Value.ToString();
                    txtDetails.Text = dataGridView1.CurrentRow.Cells["Details"].Value.ToString();
                    txtAmount.Text = dataGridView1.CurrentRow.Cells["Amount"].Value.ToString();

                    // Discount handling (safe for null)
                    if (dataGridView1.CurrentRow.Cells["Discount"].Value != DBNull.Value)
                    {
                        cbDiscount.SelectedValue = dataGridView1.CurrentRow.Cells["Discount"].Value;
                    }
                    else
                    {
                        cbDiscount.SelectedIndex = -1;
                    }

                    // Optional fields (if present in table)
                    if (dataGridView1.Columns.Contains("FullName"))
                        txtFullName.Text = dataGridView1.CurrentRow.Cells["FullName"].Value.ToString();

                    if (dataGridView1.Columns.Contains("HPhone"))
                        txtHPhone.Text = dataGridView1.CurrentRow.Cells["HPhone"].Value.ToString();

                    if (dataGridView1.Columns.Contains("FPhone"))
                        txtFPhone.Text = dataGridView1.CurrentRow.Cells["FPhone"].Value.ToString();

                    if (dataGridView1.Columns.Contains("Year"))
                        cbYear.Text = dataGridView1.CurrentRow.Cells["Year"].Value.ToString();

                    if (dataGridView1.Columns.Contains("Term"))
                        cbTerm.Text = dataGridView1.CurrentRow.Cells["Term"].Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error filling data: " + ex.Message);
                }
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ShowData();
        }

        private void cbCurrentClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCurrentClass.SelectedIndex >= 0 && cbCurrentClass.SelectedValue != null)
            {
                int classId;
                if (int.TryParse(cbCurrentClass.SelectedValue.ToString(), out classId))
                {
                    LoadStudentCombo(classId);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //FrmDashboard.instance.colleciton();
            //global.ReportName = "Fee";
            //FrmFeeReport ffr1 = new FrmFeeReport();
            //ffr1.Show();

            if (txtReceiptNo.Text != "")
            {
                FrmFee.SelectedReceiptNo = txtReceiptNo.Text;
                global.ReportName = "Fee";
                FrmFeeReport f = new FrmFeeReport();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select a record to print receipt.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void cbStudentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbStudentId.SelectedValue != null && cbStudentId.SelectedIndex >= 0)
            //{
                //try
                //{
                    // StudentId (ya ReDgNo) se details fetch karna
                    //string query = "SELECT SurName, Name, FatherName, HomePhone, FatherPhone FROM StudentMaster WHERE ReDgNo = '" + cbStudentId.SelectedValue + "'";
                    //SqlDataAdapter da = new SqlDataAdapter(query, Con);
                    //DataTable dt = new DataTable();
                    //da.Fill(dt);

                    //if (dt.Rows.Count > 0)
                    //{
                    //    string fullName = dt.Rows[0]["SurName"].ToString() + " " + dt.Rows[0]["Name"].ToString() + " " + dt.Rows[0]["FatherName"].ToString();
                    //    txtFullName.Text = fullName;

                    //    // Agar phone numbers bhi lena hai
                    //    txtHPhone.Text = dt.Rows[0]["HomePhone"].ToString();
                    //    txtFPhone.Text = dt.Rows[0]["FatherPhone"].ToString();
                    //}
                    //else
                    //{
                    //    txtFullName.Text = "";
                    //    txtHPhone.Text = "";
                    //    txtFPhone.Text = "";
                    //}
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Error loading student details: " + ex.Message);
                //}
            //}


            // Combo abhi load ho raha hai ya empty hai to skip kar do
            if (cbStudentId.DataSource == null || cbStudentId.SelectedIndex < 0)
                return;

            // Agar value "System.Data.DataRowView" type ki aa rahi ho to skip kar do
            if (cbStudentId.SelectedValue == null || cbStudentId.SelectedValue.ToString() == "System.Data.DataRowView")
                return;

            try
            {
                // Yeh check karo ki ReDgNo varchar hai ya numeric
                // ----> Agar varchar hai, to quotes lagao; agar int hai, quotes hata do.
                string query = "SELECT SurName, Name, FatherName, HomePhone, FatherPhone FROM StudentMaster WHERE ReDgNo = " + cbStudentId.SelectedValue;

                SqlDataAdapter da = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    txtFullName.Text = dt.Rows[0]["SurName"].ToString() + " " + dt.Rows[0]["Name"].ToString() + " " + dt.Rows[0]["FatherName"].ToString();
                    txtHPhone.Text = dt.Rows[0]["HomePhone"].ToString();
                    txtFPhone.Text = dt.Rows[0]["FatherPhone"].ToString();
                }
                else
                {
                    txtFullName.Text = "";
                    txtHPhone.Text = "";
                    txtFPhone.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading student details: " + ex.Message);
            }
        }
    }
}
