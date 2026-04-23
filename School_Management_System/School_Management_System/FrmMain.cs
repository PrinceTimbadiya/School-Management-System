using Microsoft.Win32;
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
    public partial class FrmMain : Form
    {
        public static FrmMain instance;

        FrmUser user1 = new FrmUser();
        FrmCity city1 = new FrmCity();
        FrmSubject sub1 = new FrmSubject();
        FrmHomework hw1 = new FrmHomework();
        Staff st2 = new Staff();
        FrmClassMaster cls1 = new FrmClassMaster();
        FrmSubjectAllotment sa1 = new FrmSubjectAllotment();
        FrmDepartment dept1 = new FrmDepartment();
        FrmShift shift1 = new FrmShift();
        FrmStudent stu1 = new FrmStudent();
        FeesType ft1 = new FeesType();
        FrmYear fy1 = new FrmYear();
        FrmStructure fs1 = new FrmStructure();
        FrmClassDefault cd1 = new FrmClassDefault();
        FrmAttendence at1 = new FrmAttendence();
        FrmFee f1 = new FrmFee();

        public FrmMain()
        {
            InitializeComponent();
            instance = this;
            resize1();

            SystemEvents.DisplaySettingsChanged += OnDisplaySettingsChanged;
        }

        private void myButtonSetting(object sender)
        {
            foreach (Control c in fpanMenu.Controls)
            {
                c.BackColor = Color.FromArgb(15, 90, 145);
                c.ForeColor = Color.White;
            }
            Control click = (Control)sender;
            click.BackColor = Color.White;
            click.ForeColor = Color.FromArgb(15, 90, 145);
        }

        public void resize1()
        {
            global.screenWidth = Screen.PrimaryScreen.Bounds.Width;
            global.screenHeight = Screen.PrimaryScreen.Bounds.Height;
            this.Width = global.screenWidth;
            this.Height = global.screenHeight;
            panDisplay.Width = this.Width - 230;
            panDisplay.Height = this.Height - 20;
            fpanMenu.Height = this.Height - 230;
        }

        private void OnDisplaySettingsChanged(object sender, EventArgs e)
        {
            // Adjust the form size when display settings change
            resize1();
            if (FrmUser.instance != null) FrmUser.instance.resize1();
            if (FrmCity.instance != null) FrmCity.instance.resize1();
            if (FrmSubject.instance != null) FrmSubject.instance.resize1();
            if (FrmHomework.instance != null) FrmHomework.instance.resize1();
            if (Staff.instance != null) Staff.instance.resize1();
            if (FrmClassMaster.instance != null) FrmClassMaster.instance.resize1();
            if (FrmSubjectAllotment.instance != null) FrmSubjectAllotment.instance.resize1();
            if (FrmDepartment.instance != null) FrmDepartment.instance.resize1();
            if (FrmShift.instance != null) FrmShift.instance.resize1();
            if (FrmStudent.instance != null) FrmStudent.instance.resize1();
            if (FeesType.instance != null) FeesType.instance.resize1();
            if (FrmYear.instance != null) FrmYear.instance.resize1();
            if (FrmStructure.instance != null) FrmStructure.instance.resize1();
            if (FrmClassDefault.instance != null) FrmClassDefault.instance.resize1();
            if (FrmAttendence.instance != null) FrmAttendence.instance.resize1();
            if (FrmFee.instance != null) FrmFee.instance.resize1();

        }

        private void btndashboard_Click(object sender, EventArgs e)
        {

        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            //open form
            panDisplay.Controls.Clear();
            panDisplay.Controls.Add(user1);
            user1.Dock = DockStyle.Fill;

            myButtonSetting(btnUser);
        }

        private void btnCity_Click(object sender, EventArgs e)
        {
            //open form
            panDisplay.Controls.Clear();
            panDisplay.Controls.Add(city1);
            city1.Dock = DockStyle.Fill;

            myButtonSetting(btnCity);
        }

        private void btnSubject_Click(object sender, EventArgs e)
        {
            //open form
            panDisplay.Controls.Clear();
            panDisplay.Controls.Add(sub1);
            sub1.Dock = DockStyle.Fill;

            myButtonSetting(btnSubject);
        }

        private void btnHomework_Click(object sender, EventArgs e)
        {
            //open form
            panDisplay.Controls.Clear();
            panDisplay.Controls.Add(hw1);
            hw1.Dock = DockStyle.Fill;

            myButtonSetting(btnHomework);
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            //open form
            panDisplay.Controls.Clear();
            //panDisplay.Controls.Add(staff1);
            //staff1.Dock = DockStyle.Fill;
            panDisplay.Controls.Add(st2);
            st2.Dock = DockStyle.Fill;

            myButtonSetting(btnStaff);
        }

        private void btnClass_Click(object sender, EventArgs e)
        {
            //open form
            panDisplay.Controls.Clear();
            panDisplay.Controls.Add(cls1);
            cls1.Dock = DockStyle.Fill;

            myButtonSetting(btnClass);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            resize1();
            fpanMenu.AutoScroll = true;
            fpanMenu.VerticalScroll.Visible = true;
        }

        private void btnSubAllotment_Click(object sender, EventArgs e)
        {
            //open form
            panDisplay.Controls.Clear();
            panDisplay.Controls.Add(sa1);
            sa1.Dock = DockStyle.Fill;

            myButtonSetting(btnSubAllotment);
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            //open form
            panDisplay.Controls.Clear();
            panDisplay.Controls.Add(dept1);
            dept1.Dock = DockStyle.Fill;

            myButtonSetting(btnDepartment);
        }

        private void btnShift_Click(object sender, EventArgs e)
        {
            //open form
            panDisplay.Controls.Clear();
            panDisplay.Controls.Add(shift1);
            shift1.Dock = DockStyle.Fill;

            myButtonSetting(btnShift);
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            //open form
            panDisplay.Controls.Clear();
            panDisplay.Controls.Add(stu1);
            stu1.Dock = DockStyle.Fill;

            myButtonSetting(btnStudent);
        }

        private void btnFeesType_Click(object sender, EventArgs e)
        {
            //open form
            panDisplay.Controls.Clear();
            panDisplay.Controls.Add(ft1);
            ft1.Dock = DockStyle.Fill;

            myButtonSetting(btnFeesType);
        }

        private void btnYear_Click(object sender, EventArgs e)
        {
            //open form
            panDisplay.Controls.Clear();
            panDisplay.Controls.Add(fy1);
            fy1.Dock = DockStyle.Fill;

            myButtonSetting(btnYear);
        }

        private void btnFeeStructure_Click(object sender, EventArgs e)
        {
            panDisplay.Controls.Clear();
            panDisplay.Controls.Add(fs1);
            fs1.Dock = DockStyle.Fill;

            myButtonSetting(btnFeeStructure);
        }

        private void btnClassStatic_Click(object sender, EventArgs e)
        {
            panDisplay.Controls.Clear();
            panDisplay.Controls.Add(cd1);
            cd1.Dock = DockStyle.Fill;

            myButtonSetting(btnClassStatic);

        }

        private void btnAttendence_Click(object sender, EventArgs e)
        {
            //panDisplay.Controls.Clear();
            //panDisplay.Controls.Add(at1);
            //at1.Dock = DockStyle.Fill;

            //myButtonSetting(btnAttendence);

            panDisplay.Controls.Clear();

            at1.TopLevel = false;                     
            at1.FormBorderStyle = FormBorderStyle.None;
            at1.Dock = DockStyle.Fill;

            panDisplay.Controls.Add(at1);
            at1.Show();                               

            myButtonSetting(btnAttendence);
        }

        private void btnFees_Click(object sender, EventArgs e)
        {
            panDisplay.Controls.Clear();
            panDisplay.Controls.Add(f1);
            f1.Dock = DockStyle.Fill;

            myButtonSetting(btnFees);
        }


    }
}
