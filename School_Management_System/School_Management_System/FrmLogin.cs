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

namespace School_Management_System
{
    public partial class FrmLogin : Form
    {
        SqlConnection Con;
        SqlCommand Cmd;
        SqlDataAdapter DA;
        DataTable dt;

        public FrmLogin()
        {
            InitializeComponent();
            string exeLocation = AppDomain.CurrentDomain.BaseDirectory;
            string exeLocation1 = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(exeLocation1, @"..\..\"));
            //global.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + exeLocation + "school.mdf;Integrated Security=True"; global.Con1 = exeLocation;
            //global.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\project\School_Management_System\School_Management_System\school.mdf;Integrated Security=True";
            global.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + projectPath + "school.mdf;Integrated Security=True"; global.Con1 = projectPath;
            Con = new SqlConnection(global.ConnectionString);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Please enter both Username and Password.");
                if (txtUserName.Text == "")
                {
                    txtUserName.Focus();
                }
                else
                {
                    txtPassword.Focus();
                }
                return;
            }

            try
            {
                Con.Open();
                string squery = "SELECT userId, userName, userPass, userType, validTillDt, status FROM LoginMaster WHERE userName = '" + txtUserName.Text.Trim() + "' AND userPass = '" + txtPassword.Text.Trim() + "'";
                Cmd = new SqlCommand(squery, Con);

                DA = new SqlDataAdapter(Cmd);
                dt = new DataTable();

                DA.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string status = dt.Rows[0]["status"].ToString();
                    DateTime validTill = Convert.ToDateTime(dt.Rows[0]["validTillDt"]);
                    int loggedInUserId = Convert.ToInt32(dt.Rows[0]["userId"]);
                    DateTime loginTime = DateTime.Now;

                    if (status != "A")
                    {
                        MessageBox.Show("User is not active.");
                        txtUserName.Focus();
                        return;
                    }

                    if (validTill < DateTime.Now)
                    {
                        MessageBox.Show("User access has expired.");
                        return;
                    }

                    // Count how many times this user has logged in
                    string countQuery = "SELECT COUNT(*) FROM LogBook WHERE userId = " + loggedInUserId;
                    Cmd = new SqlCommand(countQuery, Con);
                    int loginCount = (int)Cmd.ExecuteScalar(); // gives current count

                    // INSERT query to add login info in LogBook
                    string insertQuery = "INSERT INTO LogBook (userId, LoginDt) VALUES (" + loggedInUserId + ", '" + loginTime.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    Cmd = new SqlCommand(insertQuery, Con);
                    Cmd.ExecuteNonQuery();

                    loginCount++;

                    global.User = dt.Rows[0]["userId"].ToString();
                    // MessageBox.Show("Welcome, User ID: " + global.User);

                    // Login success: show all user details
                    //string userDetails = "Login Successful!\n\n" +
                    //                     "User ID: " + dt.Rows[0]["userId"].ToString() + "\n" +
                    //                     "Username: " + dt.Rows[0]["userName"].ToString() + "\n" +
                    //                     "Password: " + dt.Rows[0]["userPass"].ToString() + "\n" +
                    //                     "User Type: " + dt.Rows[0]["userType"].ToString() + "\n" +
                    //                     "Valid Till: " + validTill.ToString("dd-MM-yyyy") + "\n" +
                    //                     "Status: " + status + "\n\n" +
                    //                     "You have logged in " + loginCount + " times.";

                    //MessageBox.Show(userDetails, "Login Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    FrmMain fm = new FrmMain();
                    fm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                    Con.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }
            }

        }

        private void chkpassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkpassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtUserName.Focus();
        }
    }
}
