namespace School_Management_System
{
    partial class FrmAttendence
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbClass = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRegExcel = new System.Windows.Forms.Button();
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.rbRollNo = new System.Windows.Forms.RadioButton();
            this.rbRollYes = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbEng = new System.Windows.Forms.RadioButton();
            this.rbGuj = new System.Windows.Forms.RadioButton();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rbCategory = new System.Windows.Forms.RadioButton();
            this.rbSurname = new System.Windows.Forms.RadioButton();
            this.rbGender = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbSchool = new System.Windows.Forms.RadioButton();
            this.rbClass = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.cbShift = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rbDept = new System.Windows.Forms.RadioButton();
            this.rbShift = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbClass
            // 
            this.cbClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbClass.FormattingEnabled = true;
            this.cbClass.Location = new System.Drawing.Point(122, 105);
            this.cbClass.MinimumSize = new System.Drawing.Size(228, 0);
            this.cbClass.Name = "cbClass";
            this.cbClass.Size = new System.Drawing.Size(465, 37);
            this.cbClass.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 29);
            this.label2.TabIndex = 14;
            this.label2.Text = "Class By";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 29);
            this.label3.TabIndex = 15;
            this.label3.Text = "SELECT Shift";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 29);
            this.label4.TabIndex = 16;
            this.label4.Text = "Select Class";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnRegExcel);
            this.panel1.Controls.Add(this.cbMonth);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.cbYear);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.cbShift);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.cbClass);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(3, 4);
            this.panel1.MaximumSize = new System.Drawing.Size(692, 492);
            this.panel1.MinimumSize = new System.Drawing.Size(435, 292);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(692, 492);
            this.panel1.TabIndex = 1;
            // 
            // btnRegExcel
            // 
            this.btnRegExcel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegExcel.Location = new System.Drawing.Point(170, 335);
            this.btnRegExcel.Name = "btnRegExcel";
            this.btnRegExcel.Size = new System.Drawing.Size(302, 47);
            this.btnRegExcel.TabIndex = 10;
            this.btnRegExcel.Text = "&Register To Excel";
            this.btnRegExcel.UseVisualStyleBackColor = true;
            this.btnRegExcel.Click += new System.EventHandler(this.btnRegExcel_Click);
            // 
            // cbMonth
            // 
            this.cbMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMonth.FormattingEnabled = true;
            this.cbMonth.Location = new System.Drawing.Point(122, 268);
            this.cbMonth.MinimumSize = new System.Drawing.Size(168, 0);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(168, 37);
            this.cbMonth.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 271);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(142, 29);
            this.label9.TabIndex = 46;
            this.label9.Text = "Enter Month";
            // 
            // cbYear
            // 
            this.cbYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbYear.FormattingEnabled = true;
            this.cbYear.Location = new System.Drawing.Point(122, 231);
            this.cbYear.MinimumSize = new System.Drawing.Size(168, 0);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(168, 37);
            this.cbYear.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 234);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(127, 29);
            this.label8.TabIndex = 44;
            this.label8.Text = "Enter Year";
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.rbRollNo);
            this.panel6.Controls.Add(this.rbRollYes);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Location = new System.Drawing.Point(314, 232);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(273, 71);
            this.panel6.TabIndex = 9;
            // 
            // rbRollNo
            // 
            this.rbRollNo.AutoSize = true;
            this.rbRollNo.Location = new System.Drawing.Point(135, 35);
            this.rbRollNo.Name = "rbRollNo";
            this.rbRollNo.Size = new System.Drawing.Size(70, 33);
            this.rbRollNo.TabIndex = 2;
            this.rbRollNo.TabStop = true;
            this.rbRollNo.Text = "No";
            this.rbRollNo.UseVisualStyleBackColor = true;
            // 
            // rbRollYes
            // 
            this.rbRollYes.AutoSize = true;
            this.rbRollYes.Location = new System.Drawing.Point(49, 35);
            this.rbRollYes.Name = "rbRollYes";
            this.rbRollYes.Size = new System.Drawing.Size(80, 33);
            this.rbRollYes.TabIndex = 1;
            this.rbRollYes.TabStop = true;
            this.rbRollYes.Text = "Yes";
            this.rbRollYes.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(264, 29);
            this.label7.TabIndex = 0;
            this.label7.Text = "Update Roll Number?";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.rbEng);
            this.panel3.Controls.Add(this.rbGuj);
            this.panel3.Controls.Add(this.lblLanguage);
            this.panel3.Location = new System.Drawing.Point(314, 192);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(273, 34);
            this.panel3.TabIndex = 6;
            // 
            // rbEng
            // 
            this.rbEng.AutoSize = true;
            this.rbEng.Location = new System.Drawing.Point(171, 3);
            this.rbEng.Name = "rbEng";
            this.rbEng.Size = new System.Drawing.Size(81, 33);
            this.rbEng.TabIndex = 2;
            this.rbEng.TabStop = true;
            this.rbEng.Text = "Eng";
            this.rbEng.UseVisualStyleBackColor = true;
            // 
            // rbGuj
            // 
            this.rbGuj.AutoSize = true;
            this.rbGuj.Location = new System.Drawing.Point(108, 3);
            this.rbGuj.Name = "rbGuj";
            this.rbGuj.Size = new System.Drawing.Size(75, 33);
            this.rbGuj.TabIndex = 1;
            this.rbGuj.TabStop = true;
            this.rbGuj.Text = "Guj";
            this.rbGuj.UseVisualStyleBackColor = true;
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLanguage.Location = new System.Drawing.Point(3, 3);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(128, 29);
            this.lblLanguage.TabIndex = 0;
            this.lblLanguage.Text = "Language";
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.rbCategory);
            this.panel5.Controls.Add(this.rbSurname);
            this.panel5.Controls.Add(this.rbGender);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Location = new System.Drawing.Point(13, 151);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(574, 34);
            this.panel5.TabIndex = 4;
            // 
            // rbCategory
            // 
            this.rbCategory.AutoSize = true;
            this.rbCategory.Location = new System.Drawing.Point(377, 4);
            this.rbCategory.Name = "rbCategory";
            this.rbCategory.Size = new System.Drawing.Size(135, 33);
            this.rbCategory.TabIndex = 3;
            this.rbCategory.TabStop = true;
            this.rbCategory.Text = "Category";
            this.rbCategory.UseVisualStyleBackColor = true;
            // 
            // rbSurname
            // 
            this.rbSurname.AutoSize = true;
            this.rbSurname.Location = new System.Drawing.Point(231, 4);
            this.rbSurname.Name = "rbSurname";
            this.rbSurname.Size = new System.Drawing.Size(135, 33);
            this.rbSurname.TabIndex = 2;
            this.rbSurname.TabStop = true;
            this.rbSurname.Text = "Surname";
            this.rbSurname.UseVisualStyleBackColor = true;
            // 
            // rbGender
            // 
            this.rbGender.AutoSize = true;
            this.rbGender.Location = new System.Drawing.Point(113, 4);
            this.rbGender.Name = "rbGender";
            this.rbGender.Size = new System.Drawing.Size(119, 33);
            this.rbGender.TabIndex = 1;
            this.rbGender.TabStop = true;
            this.rbGender.Text = "Gender";
            this.rbGender.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 29);
            this.label6.TabIndex = 0;
            this.label6.Text = "Sort By:";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rbSchool);
            this.panel2.Controls.Add(this.rbClass);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(13, 192);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(295, 34);
            this.panel2.TabIndex = 5;
            // 
            // rbSchool
            // 
            this.rbSchool.AutoSize = true;
            this.rbSchool.Location = new System.Drawing.Point(199, 2);
            this.rbSchool.Name = "rbSchool";
            this.rbSchool.Size = new System.Drawing.Size(113, 33);
            this.rbSchool.TabIndex = 2;
            this.rbSchool.TabStop = true;
            this.rbSchool.Text = "School";
            this.rbSchool.UseVisualStyleBackColor = true;
            // 
            // rbClass
            // 
            this.rbClass.AutoSize = true;
            this.rbClass.Location = new System.Drawing.Point(113, 2);
            this.rbClass.Name = "rbClass";
            this.rbClass.Size = new System.Drawing.Size(98, 33);
            this.rbClass.TabIndex = 1;
            this.rbClass.TabStop = true;
            this.rbClass.Text = "Class";
            this.rbClass.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 29);
            this.label5.TabIndex = 0;
            this.label5.Text = "Sort By:";
            // 
            // cbShift
            // 
            this.cbShift.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShift.FormattingEnabled = true;
            this.cbShift.Location = new System.Drawing.Point(122, 62);
            this.cbShift.MinimumSize = new System.Drawing.Size(228, 0);
            this.cbShift.Name = "cbShift";
            this.cbShift.Size = new System.Drawing.Size(465, 37);
            this.cbShift.TabIndex = 2;
            this.cbShift.SelectedIndexChanged += new System.EventHandler(this.cbShift_SelectedIndexChanged);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.rbDept);
            this.panel4.Controls.Add(this.rbShift);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Location = new System.Drawing.Point(122, 13);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(295, 32);
            this.panel4.TabIndex = 1;
            // 
            // rbDept
            // 
            this.rbDept.AutoSize = true;
            this.rbDept.Location = new System.Drawing.Point(199, 2);
            this.rbDept.Name = "rbDept";
            this.rbDept.Size = new System.Drawing.Size(89, 33);
            this.rbDept.TabIndex = 2;
            this.rbDept.TabStop = true;
            this.rbDept.Text = "Dept";
            this.rbDept.UseVisualStyleBackColor = true;
            this.rbDept.CheckedChanged += new System.EventHandler(this.rbDept_CheckedChanged);
            // 
            // rbShift
            // 
            this.rbShift.AutoSize = true;
            this.rbShift.Location = new System.Drawing.Point(113, 2);
            this.rbShift.Name = "rbShift";
            this.rbShift.Size = new System.Drawing.Size(85, 33);
            this.rbShift.TabIndex = 1;
            this.rbShift.TabStop = true;
            this.rbShift.Text = "Shift";
            this.rbShift.UseVisualStyleBackColor = true;
            this.rbShift.CheckedChanged += new System.EventHandler(this.rbShift_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sort By:";
            // 
            // FrmAttendence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1144, 771);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "FrmAttendence";
            this.Text = "FrmAttendence";
            this.Load += new System.EventHandler(this.FrmAttendence_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbClass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbShift;
        private System.Windows.Forms.RadioButton rbDept;
        private System.Windows.Forms.ComboBox cbShift;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton rbSurname;
        private System.Windows.Forms.RadioButton rbGender;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbSchool;
        private System.Windows.Forms.RadioButton rbClass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbCategory;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbEng;
        private System.Windows.Forms.RadioButton rbGuj;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton rbRollNo;
        private System.Windows.Forms.RadioButton rbRollYes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbYear;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnRegExcel;
    }
}