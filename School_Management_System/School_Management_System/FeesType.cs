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
    public partial class FeesType : UserControl
    {
        public static FeesType instance;
        SqlConnection Con;
        SqlCommand Cmd;
        SqlDataAdapter DA;
        //SqlDataReader Dtr;
        DataSet DS;
        //int MaxNum;
        Boolean add, edit;

        public FeesType()
        {
            InitializeComponent();
            instance = this;
            Con = new SqlConnection(global.ConnectionString);
            add = false;
            edit = false;
            ShowData();
            txtReadOnly(true);
            dataGridView1.ClearSelection();
        }

        public void resize1()
        {
            panel2.Width = global.screenWidth - 240;
            panel2.Height = global.screenHeight - 375;
        }

        private void FeesType_Load(object sender, EventArgs e)
        {
            resize1();
            btnadd.Enabled = true;
            btnedit.Enabled = false;
            btndelete.Enabled = false;
            btnsave.Enabled = false;
            btnCancel.Enabled = false;
            dataGridView1.Enabled = true;
            dataGridView1.ClearSelection();
            btnadd.Focus();
        }

        private void ShowData()
        {   //Diplay data in gridview 
            String squery = "select * from FeesType order by TypeID DESC";
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
        }


        private void txtReadOnly(Boolean ans)
        {
            cbStatus.Enabled = !ans;
            TextBox[] tx = { txtFeesTitle, txtDiscount };
            for (int x = 0; x < 2; x++)
            {
                tx[x].ReadOnly = ans;
                tx[x].BackColor = Color.White;
            }
        }

        private void txtClear()
        {
            txtTypeID.Text = "";
            txtFeesTitle.Text = "";
            txtDiscount.Text = "";
            cbStatus.SelectedIndex = -1;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            txtReadOnly(false);
            btnadd.Enabled = false;
            btnedit.Enabled = false;
            btndelete.Enabled = false;
            btnsave.Enabled = true;
            btnCancel.Enabled = true;
            dataGridView1.Enabled = false;
            txtClear();
            add = true;
            edit = false;
            cbStatus.SelectedIndex = 0;

            txtFeesTitle.Focus();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (txtTypeID.Text != "")
            {
                txtReadOnly(false);
                btnadd.Enabled = false;
                btnedit.Enabled = false;
                btnsave.Enabled = true;
                btnCancel.Enabled = true;
                btndelete.Enabled = false;
                add = false;
                edit = true;
                dataGridView1.Enabled = false;
                txtFeesTitle.Focus();
            }
            else
            {
                MessageBox.Show("Search Data To Edit...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (txtFeesTitle.Text != "" && txtDiscount.Text != "" && cbStatus.Text != "")
            {
                if (add == true)
                {
                   
                    String status;
                    if (cbStatus.Text == "Active")
                        status = "A";
                    else
                        status = "D";
                    DateTime dt = DateTime.Now;
                    
                        Con.Open();
                        string inquery = "insert into FeesType values('" + txtFeesTitle.Text + "','" + txtDiscount.Text + "','" + status + "');";
                        Cmd = new SqlCommand(inquery, Con);
                        Cmd.ExecuteNonQuery();
                        Con.Close();
                        //FrmStructure.instance.AddDataInCombo();
                        FrmStudent.instance.AddDataInCombo();
                        MessageBox.Show("Record Added...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowData();
                        btnCancel.PerformClick();
                   
                }

                if (edit == true)
                {
                    String status;
                    if (cbStatus.Text == "Active")
                        status = "A";
                    else
                        status = "D";
                    Con.Open();
                    string upquery = "update FeesType set FeesTitle='" + txtFeesTitle.Text + "',Discount='" + txtDiscount.Text + "',Status='" + status + "' where TypeID='" + txtTypeID.Text + "'";
                    Cmd = new SqlCommand(upquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    ShowData();
                    //FrmStructure.instance.AddDataInCombo();
                    FrmStudent.instance.AddDataInCombo();
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

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (txtTypeID.Text != "")
            {
                DialogResult ds = MessageBox.Show("Do You Want To Delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ds == DialogResult.Yes)
                {
                    Con.Open();
                    string dquery = "delete from FeesType where TypeID='" + txtTypeID.Text + "'";
                    Cmd = new SqlCommand(dquery, Con);
                    Cmd.ExecuteNonQuery();
                    Con.Close();
                    ShowData();
                    //FrmStructure.instance.AddDataInCombo();
                    FrmStudent.instance.AddDataInCombo();
                    txtClear();
                }
            }
            else
            {
                MessageBox.Show("Nothing selected...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtReadOnly(true);
            btnadd.Enabled = true;
            btnedit.Enabled = false;
            btnsave.Enabled = false;
            btndelete.Enabled = false;
            btnCancel.Enabled = false;
            dataGridView1.Enabled = true;
            txtClear();
            btnadd.Focus();
        }

        private void txtFeesTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Backspace:8 
            //char ch = e.KeyChar;
            //if (!char.IsLetter(ch) && ch != 8)
            //    e.Handled = true;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (cbStatusSearch.Text == "All")
            {
                String squery = "select * from FeesType where TypeID like '%" + txtSearch.Text + "%' OR FeesTitle like '" + txtSearch.Text + "%' OR Discount like '" + txtSearch.Text + "%' OR Status like '" + txtSearch.Text + "%' order by TypeID DESC";
                ShowAllData(squery);
            }
            else if (cbStatusSearch.Text == "Active")
            {
                String squery = "select * from FeesType where (Status='A') AND (TypeID like '%" + txtSearch.Text + "%' OR FeesTitle like '" + txtSearch.Text + "%' OR Discount like '" + txtSearch.Text + "%' OR Status like '" + txtSearch.Text + "%' ) order by TypeID DESC";
                ShowAllData(squery);
            }
            else
            {
                String squery = "select * from FeesType where (Status='D') AND (TypeID like '%" + txtSearch.Text + "%' OR FeesTitle like '" + txtSearch.Text + "%' OR Discount like '" + txtSearch.Text + "%' OR Status like '" + txtSearch.Text + "%' ) order by TypeID DESC";
                ShowAllData(squery);
            }
        }

        private void cbStatusSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch_TextChanged(sender, e);
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int discount = int.Parse(txtDiscount.Text);

                if (discount > 100)
                {
                    MessageBox.Show("Invalid Discount. It cannot be greater than 100.");
                    txtDiscount.Text = "";
                }
            }
            catch
            {
                if (!string.IsNullOrWhiteSpace(txtDiscount.Text))
                {
                    MessageBox.Show("Please enter a valid numeric discount.");
                    txtDiscount.Text = "";
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {
                btnedit.Enabled = true;
                btndelete.Enabled = true;
                btnadd.Focus();

                try
                {
                    txtTypeID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txtFeesTitle.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txtDiscount.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    if (dataGridView1.CurrentRow.Cells[3].Value.ToString() == "A")
                        cbStatus.Text = "Active";
                    else
                        cbStatus.Text = "Deactive";
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

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Backspace:8
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
                e.Handled = true;
        }



    }
}
