namespace School_Management_System
{
    partial class FrmHomework
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.p1 = new System.Windows.Forms.Panel();
            this.cbSubject = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.dtpHWDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStaffID = new System.Windows.Forms.TextBox();
            this.cbClass = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHomeWorkID = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.p2 = new System.Windows.Forms.Panel();
            this.txtHomeWork = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.p3 = new System.Windows.Forms.Panel();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnMargin = new System.Windows.Forms.Button();
            this.p4 = new System.Windows.Forms.Panel();
            this.txtHWDispaly = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkAllDates = new System.Windows.Forms.CheckBox();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnExport = new System.Windows.Forms.Button();
            this.cbStaffSearch = new System.Windows.Forms.ComboBox();
            this.cbSubjectSearch = new System.Windows.Forms.ComboBox();
            this.lblSortBy = new System.Windows.Forms.Label();
            this.cbClassSearch = new System.Windows.Forms.ComboBox();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.p1.SuspendLayout();
            this.p2.SuspendLayout();
            this.p3.SuspendLayout();
            this.p4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // p1
            // 
            this.p1.BackColor = System.Drawing.Color.Transparent;
            this.p1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p1.Controls.Add(this.cbSubject);
            this.p1.Controls.Add(this.label9);
            this.p1.Controls.Add(this.label2);
            this.p1.Controls.Add(this.txtRemark);
            this.p1.Controls.Add(this.dtpHWDate);
            this.p1.Controls.Add(this.label5);
            this.p1.Controls.Add(this.txtStaffID);
            this.p1.Controls.Add(this.cbClass);
            this.p1.Controls.Add(this.label4);
            this.p1.Controls.Add(this.txtHomeWorkID);
            this.p1.Controls.Add(this.label12);
            this.p1.Controls.Add(this.label8);
            this.p1.Location = new System.Drawing.Point(3, 2);
            this.p1.MaximumSize = new System.Drawing.Size(370, 264);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(315, 201);
            this.p1.TabIndex = 0;
            // 
            // cbSubject
            // 
            this.cbSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSubject.FormattingEnabled = true;
            this.cbSubject.Location = new System.Drawing.Point(107, 94);
            this.cbSubject.MaximumSize = new System.Drawing.Size(194, 0);
            this.cbSubject.MaxLength = 4;
            this.cbSubject.Name = "cbSubject";
            this.cbSubject.Size = new System.Drawing.Size(194, 37);
            this.cbSubject.TabIndex = 9;
            this.cbSubject.SelectedIndexChanged += new System.EventHandler(this.cbSubject_SelectedIndexChanged);
            this.cbSubject.Leave += new System.EventHandler(this.cbSubject_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 170);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 29);
            this.label9.TabIndex = 72;
            this.label9.Text = "Remark";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 29);
            this.label2.TabIndex = 87;
            this.label2.Text = "Subject";
            // 
            // txtRemark
            // 
            this.txtRemark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemark.Location = new System.Drawing.Point(109, 167);
            this.txtRemark.MaximumSize = new System.Drawing.Size(342, 30);
            this.txtRemark.MaxLength = 50;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(182, 35);
            this.txtRemark.TabIndex = 14;
            // 
            // dtpHWDate
            // 
            this.dtpHWDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpHWDate.CustomFormat = "dd/MM/yyyy";
            this.dtpHWDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHWDate.Location = new System.Drawing.Point(107, 70);
            this.dtpHWDate.Margin = new System.Windows.Forms.Padding(0);
            this.dtpHWDate.MaximumSize = new System.Drawing.Size(194, 30);
            this.dtpHWDate.Name = "dtpHWDate";
            this.dtpHWDate.Size = new System.Drawing.Size(194, 30);
            this.dtpHWDate.TabIndex = 8;
            this.dtpHWDate.TabStop = false;
            this.dtpHWDate.Value = new System.DateTime(2024, 8, 28, 0, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 29);
            this.label5.TabIndex = 83;
            this.label5.Text = "H.W. Date";
            // 
            // txtStaffID
            // 
            this.txtStaffID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStaffID.Location = new System.Drawing.Point(107, 127);
            this.txtStaffID.MaximumSize = new System.Drawing.Size(194, 30);
            this.txtStaffID.MaxLength = 30;
            this.txtStaffID.Name = "txtStaffID";
            this.txtStaffID.ReadOnly = true;
            this.txtStaffID.Size = new System.Drawing.Size(194, 35);
            this.txtStaffID.TabIndex = 10;
            this.txtStaffID.TabStop = false;
            // 
            // cbClass
            // 
            this.cbClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbClass.FormattingEnabled = true;
            this.cbClass.Location = new System.Drawing.Point(107, 38);
            this.cbClass.MaximumSize = new System.Drawing.Size(194, 0);
            this.cbClass.MaxLength = 3;
            this.cbClass.Name = "cbClass";
            this.cbClass.Size = new System.Drawing.Size(194, 37);
            this.cbClass.TabIndex = 7;
            this.cbClass.SelectedIndexChanged += new System.EventHandler(this.cbClass_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 29);
            this.label4.TabIndex = 80;
            this.label4.Text = "Staff";
            // 
            // txtHomeWorkID
            // 
            this.txtHomeWorkID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHomeWorkID.Location = new System.Drawing.Point(109, 7);
            this.txtHomeWorkID.MaximumSize = new System.Drawing.Size(194, 30);
            this.txtHomeWorkID.MaxLength = 15;
            this.txtHomeWorkID.Name = "txtHomeWorkID";
            this.txtHomeWorkID.ReadOnly = true;
            this.txtHomeWorkID.Size = new System.Drawing.Size(192, 35);
            this.txtHomeWorkID.TabIndex = 6;
            this.txtHomeWorkID.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1, 8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(163, 29);
            this.label12.TabIndex = 79;
            this.label12.Text = "HomeWork ID";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(59, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 29);
            this.label8.TabIndex = 78;
            this.label8.Text = "Class";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(60, 6);
            this.txtSearch.MaxLength = 25;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(172, 35);
            this.txtSearch.TabIndex = 16;
            this.txtSearch.TabStop = false;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 29);
            this.label6.TabIndex = 77;
            this.label6.Text = "Search";
            // 
            // p2
            // 
            this.p2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p2.Controls.Add(this.txtHomeWork);
            this.p2.Controls.Add(this.label7);
            this.p2.Location = new System.Drawing.Point(324, 3);
            this.p2.MaximumSize = new System.Drawing.Size(464, 264);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(363, 200);
            this.p2.TabIndex = 1;
            // 
            // txtHomeWork
            // 
            this.txtHomeWork.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHomeWork.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.txtHomeWork.Location = new System.Drawing.Point(92, 7);
            this.txtHomeWork.MaximumSize = new System.Drawing.Size(342, 189);
            this.txtHomeWork.MaxLength = 200;
            this.txtHomeWork.Multiline = true;
            this.txtHomeWork.Name = "txtHomeWork";
            this.txtHomeWork.Size = new System.Drawing.Size(260, 181);
            this.txtHomeWork.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(-1, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 29);
            this.label7.TabIndex = 71;
            this.label7.Text = "HomeWork";
            // 
            // p3
            // 
            this.p3.BackColor = System.Drawing.Color.Transparent;
            this.p3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p3.Controls.Add(this.btnExcel);
            this.p3.Controls.Add(this.btnAdd);
            this.p3.Controls.Add(this.btnDelete);
            this.p3.Controls.Add(this.btnCancel);
            this.p3.Controls.Add(this.btnSave);
            this.p3.Controls.Add(this.btnEdit);
            this.p3.Controls.Add(this.btnMargin);
            this.p3.Location = new System.Drawing.Point(3, 209);
            this.p3.MaximumSize = new System.Drawing.Size(1043, 264);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(684, 44);
            this.p3.TabIndex = 2;
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Location = new System.Drawing.Point(585, 3);
            this.btnExcel.MaximumSize = new System.Drawing.Size(113, 47);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(94, 37);
            this.btnExcel.TabIndex = 6;
            this.btnExcel.Text = "Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.MaximumSize = new System.Drawing.Size(113, 47);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(94, 37);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(294, 3);
            this.btnDelete.MaximumSize = new System.Drawing.Size(113, 47);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(94, 37);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(391, 3);
            this.btnCancel.MaximumSize = new System.Drawing.Size(113, 47);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 37);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(197, 3);
            this.btnSave.MaximumSize = new System.Drawing.Size(113, 47);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 37);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(100, 3);
            this.btnEdit.MaximumSize = new System.Drawing.Size(113, 47);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(94, 37);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnMargin
            // 
            this.btnMargin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMargin.Location = new System.Drawing.Point(488, 3);
            this.btnMargin.MaximumSize = new System.Drawing.Size(113, 47);
            this.btnMargin.Name = "btnMargin";
            this.btnMargin.Size = new System.Drawing.Size(94, 37);
            this.btnMargin.TabIndex = 5;
            this.btnMargin.Text = "&Margin";
            this.btnMargin.UseVisualStyleBackColor = true;
            this.btnMargin.Click += new System.EventHandler(this.btnMargin_Click);
            // 
            // p4
            // 
            this.p4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p4.Controls.Add(this.txtHWDispaly);
            this.p4.Location = new System.Drawing.Point(693, 3);
            this.p4.MaximumSize = new System.Drawing.Size(350, 360);
            this.p4.Name = "p4";
            this.p4.Size = new System.Drawing.Size(350, 250);
            this.p4.TabIndex = 3;
            this.p4.TabStop = true;
            // 
            // txtHWDispaly
            // 
            this.txtHWDispaly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHWDispaly.BackColor = System.Drawing.Color.White;
            this.txtHWDispaly.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.txtHWDispaly.Location = new System.Drawing.Point(3, 3);
            this.txtHWDispaly.MaximumSize = new System.Drawing.Size(350, 360);
            this.txtHWDispaly.MaxLength = 500;
            this.txtHWDispaly.Multiline = true;
            this.txtHWDispaly.Name = "txtHWDispaly";
            this.txtHWDispaly.ReadOnly = true;
            this.txtHWDispaly.Size = new System.Drawing.Size(341, 288);
            this.txtHWDispaly.TabIndex = 15;
            this.txtHWDispaly.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Location = new System.Drawing.Point(3, 306);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(1299, 491);
            this.panel2.TabIndex = 62;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(238)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(90)))), ((int)(((byte)(145)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.ColumnHeadersHeight = 32;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(10, 3, 3, 3);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 40;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1293, 485);
            this.dataGridView1.TabIndex = 29;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chkAllDates);
            this.panel1.Controls.Add(this.dtpTo);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.cbStaffSearch);
            this.panel1.Controls.Add(this.cbSubjectSearch);
            this.panel1.Controls.Add(this.lblSortBy);
            this.panel1.Controls.Add(this.cbClassSearch);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.dtpFrom);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Location = new System.Drawing.Point(3, 259);
            this.panel1.MaximumSize = new System.Drawing.Size(1043, 264);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1040, 41);
            this.panel1.TabIndex = 7;
            // 
            // chkAllDates
            // 
            this.chkAllDates.AutoSize = true;
            this.chkAllDates.Location = new System.Drawing.Point(537, 8);
            this.chkAllDates.Name = "chkAllDates";
            this.chkAllDates.Size = new System.Drawing.Size(66, 33);
            this.chkAllDates.TabIndex = 81;
            this.chkAllDates.Text = "All";
            this.chkAllDates.UseVisualStyleBackColor = true;
            this.chkAllDates.CheckedChanged += new System.EventHandler(this.chkAllDates_CheckedChanged);
            // 
            // dtpTo
            // 
            this.dtpTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpTo.CustomFormat = "dd/MM/yyyy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(361, 6);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(0);
            this.dtpTo.MaximumSize = new System.Drawing.Size(194, 30);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(114, 30);
            this.dtpTo.TabIndex = 80;
            this.dtpTo.TabStop = false;
            this.dtpTo.Value = new System.DateTime(2025, 8, 28, 0, 0, 0, 0);
            this.dtpTo.ValueChanged += new System.EventHandler(this.dtpTo_ValueChanged);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(478, 1);
            this.btnExport.MaximumSize = new System.Drawing.Size(113, 47);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(55, 37);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "Exp.";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // cbStaffSearch
            // 
            this.cbStaffSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbStaffSearch.FormattingEnabled = true;
            this.cbStaffSearch.Location = new System.Drawing.Point(892, 6);
            this.cbStaffSearch.MaximumSize = new System.Drawing.Size(194, 0);
            this.cbStaffSearch.MaxLength = 3;
            this.cbStaffSearch.Name = "cbStaffSearch";
            this.cbStaffSearch.Size = new System.Drawing.Size(143, 37);
            this.cbStaffSearch.TabIndex = 79;
            this.cbStaffSearch.TabStop = false;
            this.cbStaffSearch.SelectedIndexChanged += new System.EventHandler(this.cbStaffSearch_SelectedIndexChanged);
            // 
            // cbSubjectSearch
            // 
            this.cbSubjectSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSubjectSearch.FormattingEnabled = true;
            this.cbSubjectSearch.Location = new System.Drawing.Point(747, 6);
            this.cbSubjectSearch.MaximumSize = new System.Drawing.Size(194, 0);
            this.cbSubjectSearch.MaxLength = 3;
            this.cbSubjectSearch.Name = "cbSubjectSearch";
            this.cbSubjectSearch.Size = new System.Drawing.Size(141, 37);
            this.cbSubjectSearch.TabIndex = 19;
            this.cbSubjectSearch.TabStop = false;
            this.cbSubjectSearch.SelectedIndexChanged += new System.EventHandler(this.cbSubjectSearch_SelectedIndexChanged);
            // 
            // lblSortBy
            // 
            this.lblSortBy.AutoSize = true;
            this.lblSortBy.Location = new System.Drawing.Point(581, 9);
            this.lblSortBy.Name = "lblSortBy";
            this.lblSortBy.Size = new System.Drawing.Size(90, 29);
            this.lblSortBy.TabIndex = 78;
            this.lblSortBy.Text = "Sort By";
            // 
            // cbClassSearch
            // 
            this.cbClassSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbClassSearch.FormattingEnabled = true;
            this.cbClassSearch.Location = new System.Drawing.Point(642, 6);
            this.cbClassSearch.MaximumSize = new System.Drawing.Size(194, 0);
            this.cbClassSearch.MaxLength = 3;
            this.cbClassSearch.Name = "cbClassSearch";
            this.cbClassSearch.Size = new System.Drawing.Size(102, 37);
            this.cbClassSearch.TabIndex = 18;
            this.cbClassSearch.TabStop = false;
            this.cbClassSearch.SelectedIndexChanged += new System.EventHandler(this.cbClassSearch_SelectedIndexChanged);
            // 
            // dtpFrom
            // 
            this.dtpFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(235, 6);
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(0);
            this.dtpFrom.MaximumSize = new System.Drawing.Size(194, 30);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(124, 30);
            this.dtpFrom.TabIndex = 17;
            this.dtpFrom.TabStop = false;
            this.dtpFrom.Value = new System.DateTime(2025, 8, 28, 0, 0, 0, 0);
            this.dtpFrom.ValueChanged += new System.EventHandler(this.dtpFrom_ValueChanged);
            // 
            // FrmHomework
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.p4);
            this.Controls.Add(this.p3);
            this.Controls.Add(this.p2);
            this.Controls.Add(this.p1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "FrmHomework";
            this.Size = new System.Drawing.Size(1309, 800);
            this.Load += new System.EventHandler(this.FrmHomework_Load);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.FrmHomework_PreviewKeyDown);
            this.p1.ResumeLayout(false);
            this.p1.PerformLayout();
            this.p2.ResumeLayout(false);
            this.p2.PerformLayout();
            this.p3.ResumeLayout(false);
            this.p4.ResumeLayout(false);
            this.p4.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel p1;
        private System.Windows.Forms.Panel p2;
        private System.Windows.Forms.Panel p3;
        private System.Windows.Forms.Panel p4;
        private System.Windows.Forms.TextBox txtHWDispaly;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DateTimePicker dtpHWDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStaffID;
        private System.Windows.Forms.ComboBox cbClass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtHomeWorkID;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtHomeWork;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbSubject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMargin;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.ComboBox cbClassSearch;
        private System.Windows.Forms.Label lblSortBy;
        private System.Windows.Forms.ComboBox cbSubjectSearch;
        private System.Windows.Forms.ComboBox cbStaffSearch;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.CheckBox chkAllDates;
    }
}