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
    public partial class FrmCity : UserControl
    {
        public static FrmCity instance;

        SqlConnection Con;
        SqlCommand Cmd;
        SqlDataAdapter DA;
        DataSet DS;
        //SqlDataReader Dtr;
        Boolean add, edit;

        public FrmCity()
        {
            InitializeComponent();
            instance = this;
            Con = new SqlConnection(global.ConnectionString);
            //global.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Project\School_Management_System\School_Management_System\school.mdf;Integrated Security=True";
            //Con = new SqlConnection(global.ConnectionString);
            add = false;
            edit = false;
            txtReadOnly(true);
            btnSave.Enabled = false;
            txtUserID.ReadOnly = true;
            txtUserID.BackColor = Color.White;
            ShowData();
            SetGridColumnHeaders();
            txtUserID.Text = global.User;

            //Attach common leave handler
            txtCity.Leave += EnglishTextBox_Leave;
            txtTaluko.Leave += EnglishTextBox_Leave;
            txtDistrict.Leave += EnglishTextBox_Leave;
            txtState.Leave += EnglishTextBox_Leave;
        }

        private void FrmCity_Load(object sender, EventArgs e)
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
            txtSearch.Enabled = true;
            //Gridview control
            dataGridView1.Enabled = true;
            dataGridView1.ClearSelection();
            ShowData();
            btnAdd.Focus();
        }

        public void resize1()
        {
            panel2.Width = global.screenWidth - 240;
            panel2.Height = global.screenHeight - 327;
        }

        private void ShowData()
        {   //Diplay data in gridview 
            string sText = txtSearch.Text.Trim();
            string squery = "SELECT * FROM CityMaster WHERE CityID LIKE '%" + sText + "%' OR City LIKE '%" + sText + "%' OR GCity LIKE N'%" + sText + "%' OR Taluko LIKE '%" + sText + "%' OR GTaluko LIKE N'%" + sText + "%' OR District LIKE '%" + sText + "%' OR GDistrict LIKE N'%" + sText + "%' OR State LIKE '%" + sText + "%' OR GState LIKE N'%" + sText + "%' OR Pincode LIKE '%" + sText + "%' OR UserID LIKE '%" + sText + "%' ORDER BY CityID DESC";

            DA = new SqlDataAdapter(squery, Con);
            DS = new DataSet();
            DA.Fill(DS);
            dataGridView1.DataSource = DS.Tables[0];
            dataGridView1.ClearSelection();
        }

        private void txtClear()
        {
            TextBox[] tx = { txtCityID, txtCity, txtGCity, txtTaluko, txtGTaluko, txtDistrict, txtGDistrict, txtState, txtGState, txtPinCode };
            for (int x = 0; x < 10; x++)
            {
                tx[x].Text = "";
            }
        }

        private void txtReadOnly(Boolean ans)
        {
            TextBox[] tx = { txtCity, txtGCity, txtTaluko, txtGTaluko, txtDistrict, txtGDistrict, txtState, txtGState, txtPinCode };
            for (int x = 0; x < 9; x++)
            {
                tx[x].ReadOnly = ans;
                tx[x].BackColor = Color.White;
            }
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
            txtCity.Focus();
            txtUserID.Text = global.User;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtCityID.Text != "")
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
                txtCity.Focus();
            }
            else
            {
                MessageBox.Show("Search Data To Edit...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCity.Text != "" && txtGCity.Text != "" && txtTaluko.Text != "" && txtGTaluko.Text != "" && txtDistrict.Text != "" && txtGDistrict.Text != "" && txtState.Text != "" && txtGState.Text != "" && txtPinCode.Text != "")
            {
                if (add == true)
                {
                    Con.Open();
                    string inquery = "INSERT INTO CityMaster (City, GCity, Taluko, GTaluko, District, GDistrict, State, GState, Pincode, UserId) VALUES ('" + txtCity.Text + "', N'" + txtGCity.Text + "', '" + txtTaluko.Text + "', N'" + txtGTaluko.Text + "', " + "'" + txtDistrict.Text + "', N'" + txtGDistrict.Text + "', '" + txtState.Text + "', N'" + txtGState.Text + "', " + "'" + txtPinCode.Text + "', '" + txtUserID.Text + "')";
                    Cmd = new SqlCommand(inquery, Con);
                    Cmd.ExecuteNonQuery();
                    StoreEToG();
                    Con.Close();
                    
                    MessageBox.Show("Record Added...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel.PerformClick();
                    ShowData();
                }

                if (edit == true)
                {
                    Con.Open();
                    string upquery = "UPDATE CityMaster SET City = '" + txtCity.Text + "', GCity = N'" + txtGCity.Text + "', Taluko = '" + txtTaluko.Text + "', " + "GTaluko = N'" + txtGTaluko.Text + "', District = '" + txtDistrict.Text + "', GDistrict = N'" + txtGDistrict.Text + "', " + "State = '" + txtState.Text + "', GState = N'" + txtGState.Text + "', PinCode = '" + txtPinCode.Text + "', " + "UserID = " + txtUserID.Text + " WHERE CityID = " + txtCityID.Text + "";
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtCityID.Text != "")
            {
                DialogResult ds = MessageBox.Show("Do You Want To Delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ds == DialogResult.Yes)
                {
                    Con.Open();
                    string dquery = "delete from CityMaster where CityID='" + txtCityID.Text + "'";
                    Cmd = new SqlCommand(dquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Record Deleted Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtUserID.Text = global.User;
        }

        private void SetGridColumnHeaders()
        {
            dataGridView1.Columns[0].HeaderText = "City ID";
            dataGridView1.Columns[1].HeaderText = "City";
            dataGridView1.Columns[2].HeaderText = "સીટી";
            dataGridView1.Columns[3].HeaderText = "Taluko";
            dataGridView1.Columns[4].HeaderText = "તાલુકો";
            dataGridView1.Columns[5].HeaderText = "District";
            dataGridView1.Columns[6].HeaderText = "જીલ્લો";
            dataGridView1.Columns[7].HeaderText = "State";
            dataGridView1.Columns[8].HeaderText = "રાજ્ય";
            dataGridView1.Columns[9].HeaderText = "Pincode";
            dataGridView1.Columns[10].HeaderText = "User Id";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnAdd.Focus();

                try
                {
                    txtCityID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txtCity.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txtGCity.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    txtTaluko.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    txtGTaluko.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    txtDistrict.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    txtGDistrict.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                    txtState.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                    txtGState.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                    txtPinCode.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                    txtUserID.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ShowData();
        }

        private void txtCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Backspace:8 space:32 
            char ch = e.KeyChar;
            if (!char.IsLetter(ch) && ch != 8 && ch != 32)
                e.Handled = true;
        }

        private void txtPinCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Backspace:8
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
                e.Handled = true;
        }


        private void EnglishTextBox_Leave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string englishValue = txt.Text.Trim().Replace("'", "''");

            // Step 1: Find corresponding Gujarati textbox
            TextBox gujaratiTextBox = null;

            if (txt == txtCity) gujaratiTextBox = txtGCity;
            else if (txt == txtTaluko) gujaratiTextBox = txtGTaluko;
            else if (txt == txtDistrict) gujaratiTextBox = txtGDistrict;
            else if (txt == txtState) gujaratiTextBox = txtGState;

            // Step 2: Agar English box empty hai to Gujarati box bhi clear kar do and return
            if (string.IsNullOrWhiteSpace(englishValue))
            {
                if (gujaratiTextBox != null)
                    gujaratiTextBox.Text = "";
                return;
            }

            SqlDataReader reader = null;

            try
            {
                if (Con.State == ConnectionState.Closed)
                    Con.Open();

                string selectQuery = "SELECT Gujarati FROM EnglishToGujarati WHERE English = '" + englishValue + "'";
                SqlCommand cmd = new SqlCommand(selectQuery, Con);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (gujaratiTextBox != null)
                        gujaratiTextBox.Text = reader["Gujarati"].ToString();
                }
                else
                {
                    reader.Close(); // Close reader before insert

                    if (gujaratiTextBox != null && gujaratiTextBox.Text.Trim() != "")
                    {
                        string gujaratiValue = gujaratiTextBox.Text.Trim().Replace("'", "''");

                        string insertQuery = "INSERT INTO EnglishToGujarati (English, Gujarati) VALUES ('" + englishValue + "', N'" + gujaratiValue + "')";
                        SqlCommand insertCmd = new SqlCommand(insertQuery, Con);
                        insertCmd.ExecuteNonQuery();
                    }
                }

                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting into EnglishToGujarati: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                if (Con.State == ConnectionState.Open)
                    Con.Close();
            }
        }



        private void StoreEToG()
        {
            TextBox[] txtEng = { txtCity, txtTaluko, txtDistrict, txtState };
            TextBox[] txtGuj = { txtGCity, txtGTaluko, txtGDistrict, txtGState };

            for (int i = 0; i < txtEng.Length; i++)
            {
                string eng = txtEng[i].Text.Trim().Replace("'", "''");
                string guj = txtGuj[i].Text.Trim().Replace("'", "''");

                if (eng == "") continue;

                try
                {
                    if (Con.State == ConnectionState.Closed)
                        Con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM EnglishToGujarati WHERE English = '" + eng + "'";
                    SqlCommand cmd = new SqlCommand(checkQuery, Con);
                    int count = (int)cmd.ExecuteScalar();

                    if (count == 0 && guj != "")
                    {
                        string insertQuery = "INSERT INTO EnglishToGujarati (English, Gujarati) VALUES ('" + eng + "', N'" + guj + "')";
                        SqlCommand insertCmd = new SqlCommand(insertQuery, Con);
                        insertCmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting into EnglishToGujarati: " + ex.Message);
                }
                finally
                {
                    if (Con.State == ConnectionState.Open)
                        Con.Close();
                }
            }
        }






    }
}
