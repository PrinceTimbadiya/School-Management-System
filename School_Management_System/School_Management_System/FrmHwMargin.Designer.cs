namespace School_Management_System
{
    partial class FrmHwMargin
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
            this.btnBack = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.p1 = new System.Windows.Forms.Panel();
            this.cbRowsType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nudVerticalSpace = new System.Windows.Forms.NumericUpDown();
            this.nudHorizontalSpace = new System.Windows.Forms.NumericUpDown();
            this.lblVPadding = new System.Windows.Forms.Label();
            this.lblHPadding = new System.Windows.Forms.Label();
            this.cmbPaperSize = new System.Windows.Forms.ComboBox();
            this.lblPageSize = new System.Windows.Forms.Label();
            this.nudRows = new System.Windows.Forms.NumericUpDown();
            this.nudColumns = new System.Windows.Forms.NumericUpDown();
            this.nudRightMargin = new System.Windows.Forms.NumericUpDown();
            this.nudLeftMargin = new System.Windows.Forms.NumericUpDown();
            this.nudBottomMargin = new System.Windows.Forms.NumericUpDown();
            this.nudTopMargin = new System.Windows.Forms.NumericUpDown();
            this.nudFontSize = new System.Windows.Forms.NumericUpDown();
            this.lblRows = new System.Windows.Forms.Label();
            this.txtHwMarginId = new System.Windows.Forms.TextBox();
            this.lblHwMarginId = new System.Windows.Forms.Label();
            this.lblColumn = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMargin = new System.Windows.Forms.Label();
            this.lblTop = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.p3 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.p4 = new System.Windows.Forms.Panel();
            this.txtHWPreview = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.p1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVerticalSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHorizontalSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRightMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBottomMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).BeginInit();
            this.p3.SuspendLayout();
            this.p4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(90)))), ((int)(((byte)(145)))));
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(2, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(111, 43);
            this.btnBack.TabIndex = 14;
            this.btnBack.TabStop = false;
            this.btnBack.Text = "&Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(135, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(175, 29);
            this.label14.TabIndex = 110;
            this.label14.Text = "Set Margin By";
            // 
            // p1
            // 
            this.p1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p1.Controls.Add(this.cbRowsType);
            this.p1.Controls.Add(this.label3);
            this.p1.Controls.Add(this.nudVerticalSpace);
            this.p1.Controls.Add(this.nudHorizontalSpace);
            this.p1.Controls.Add(this.lblVPadding);
            this.p1.Controls.Add(this.lblHPadding);
            this.p1.Controls.Add(this.cmbPaperSize);
            this.p1.Controls.Add(this.lblPageSize);
            this.p1.Controls.Add(this.nudRows);
            this.p1.Controls.Add(this.nudColumns);
            this.p1.Controls.Add(this.nudRightMargin);
            this.p1.Controls.Add(this.nudLeftMargin);
            this.p1.Controls.Add(this.nudBottomMargin);
            this.p1.Controls.Add(this.nudTopMargin);
            this.p1.Controls.Add(this.nudFontSize);
            this.p1.Controls.Add(this.lblRows);
            this.p1.Controls.Add(this.txtHwMarginId);
            this.p1.Controls.Add(this.lblHwMarginId);
            this.p1.Controls.Add(this.lblColumn);
            this.p1.Controls.Add(this.label1);
            this.p1.Controls.Add(this.label2);
            this.p1.Controls.Add(this.lblMargin);
            this.p1.Controls.Add(this.lblTop);
            this.p1.Controls.Add(this.lblSize);
            this.p1.Controls.Add(this.lblAge);
            this.p1.Location = new System.Drawing.Point(1, 52);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(247, 356);
            this.p1.TabIndex = 1;
            // 
            // cbRowsType
            // 
            this.cbRowsType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRowsType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRowsType.FormattingEnabled = true;
            this.cbRowsType.Location = new System.Drawing.Point(165, 261);
            this.cbRowsType.MaximumSize = new System.Drawing.Size(194, 0);
            this.cbRowsType.MaxLength = 3;
            this.cbRowsType.Name = "cbRowsType";
            this.cbRowsType.Size = new System.Drawing.Size(77, 34);
            this.cbRowsType.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(165, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 26);
            this.label3.TabIndex = 125;
            this.label3.Text = "RowsType";
            // 
            // nudVerticalSpace
            // 
            this.nudVerticalSpace.Location = new System.Drawing.Point(102, 329);
            this.nudVerticalSpace.Name = "nudVerticalSpace";
            this.nudVerticalSpace.Size = new System.Drawing.Size(140, 35);
            this.nudVerticalSpace.TabIndex = 16;
            // 
            // nudHorizontalSpace
            // 
            this.nudHorizontalSpace.Location = new System.Drawing.Point(102, 295);
            this.nudHorizontalSpace.Name = "nudHorizontalSpace";
            this.nudHorizontalSpace.Size = new System.Drawing.Size(140, 35);
            this.nudHorizontalSpace.TabIndex = 15;
            // 
            // lblVPadding
            // 
            this.lblVPadding.AutoSize = true;
            this.lblVPadding.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVPadding.Location = new System.Drawing.Point(12, 330);
            this.lblVPadding.Name = "lblVPadding";
            this.lblVPadding.Size = new System.Drawing.Size(130, 29);
            this.lblVPadding.TabIndex = 124;
            this.lblVPadding.Text = "V Padding";
            // 
            // lblHPadding
            // 
            this.lblHPadding.AutoSize = true;
            this.lblHPadding.Location = new System.Drawing.Point(12, 297);
            this.lblHPadding.Name = "lblHPadding";
            this.lblHPadding.Size = new System.Drawing.Size(126, 29);
            this.lblHPadding.TabIndex = 123;
            this.lblHPadding.Text = "H Padding";
            // 
            // cmbPaperSize
            // 
            this.cmbPaperSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPaperSize.FormattingEnabled = true;
            this.cmbPaperSize.Location = new System.Drawing.Point(102, 263);
            this.cmbPaperSize.Name = "cmbPaperSize";
            this.cmbPaperSize.Size = new System.Drawing.Size(62, 34);
            this.cmbPaperSize.TabIndex = 13;
            // 
            // lblPageSize
            // 
            this.lblPageSize.AutoSize = true;
            this.lblPageSize.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageSize.Location = new System.Drawing.Point(4, 263);
            this.lblPageSize.Name = "lblPageSize";
            this.lblPageSize.Size = new System.Drawing.Size(126, 29);
            this.lblPageSize.TabIndex = 120;
            this.lblPageSize.Text = "Page Size";
            // 
            // nudRows
            // 
            this.nudRows.Location = new System.Drawing.Point(102, 233);
            this.nudRows.Name = "nudRows";
            this.nudRows.Size = new System.Drawing.Size(62, 35);
            this.nudRows.TabIndex = 12;
            this.nudRows.ValueChanged += new System.EventHandler(this.nudRows_ValueChanged);
            // 
            // nudColumns
            // 
            this.nudColumns.Location = new System.Drawing.Point(102, 200);
            this.nudColumns.Name = "nudColumns";
            this.nudColumns.Size = new System.Drawing.Size(140, 35);
            this.nudColumns.TabIndex = 11;
            this.nudColumns.ValueChanged += new System.EventHandler(this.nudColumns_ValueChanged);
            // 
            // nudRightMargin
            // 
            this.nudRightMargin.Location = new System.Drawing.Point(102, 167);
            this.nudRightMargin.Name = "nudRightMargin";
            this.nudRightMargin.Size = new System.Drawing.Size(140, 35);
            this.nudRightMargin.TabIndex = 10;
            // 
            // nudLeftMargin
            // 
            this.nudLeftMargin.Location = new System.Drawing.Point(102, 133);
            this.nudLeftMargin.Name = "nudLeftMargin";
            this.nudLeftMargin.Size = new System.Drawing.Size(140, 35);
            this.nudLeftMargin.TabIndex = 9;
            // 
            // nudBottomMargin
            // 
            this.nudBottomMargin.Location = new System.Drawing.Point(102, 99);
            this.nudBottomMargin.Name = "nudBottomMargin";
            this.nudBottomMargin.Size = new System.Drawing.Size(140, 35);
            this.nudBottomMargin.TabIndex = 8;
            // 
            // nudTopMargin
            // 
            this.nudTopMargin.Location = new System.Drawing.Point(102, 65);
            this.nudTopMargin.Name = "nudTopMargin";
            this.nudTopMargin.Size = new System.Drawing.Size(140, 35);
            this.nudTopMargin.TabIndex = 7;
            // 
            // nudFontSize
            // 
            this.nudFontSize.Location = new System.Drawing.Point(102, 31);
            this.nudFontSize.Name = "nudFontSize";
            this.nudFontSize.Size = new System.Drawing.Size(140, 35);
            this.nudFontSize.TabIndex = 6;
            // 
            // lblRows
            // 
            this.lblRows.AutoSize = true;
            this.lblRows.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRows.Location = new System.Drawing.Point(37, 234);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(77, 29);
            this.lblRows.TabIndex = 108;
            this.lblRows.Text = "Rows";
            // 
            // txtHwMarginId
            // 
            this.txtHwMarginId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHwMarginId.Location = new System.Drawing.Point(102, -1);
            this.txtHwMarginId.Name = "txtHwMarginId";
            this.txtHwMarginId.ReadOnly = true;
            this.txtHwMarginId.Size = new System.Drawing.Size(140, 35);
            this.txtHwMarginId.TabIndex = 5;
            this.txtHwMarginId.TabStop = false;
            // 
            // lblHwMarginId
            // 
            this.lblHwMarginId.AutoSize = true;
            this.lblHwMarginId.Location = new System.Drawing.Point(4, 2);
            this.lblHwMarginId.Name = "lblHwMarginId";
            this.lblHwMarginId.Size = new System.Drawing.Size(151, 29);
            this.lblHwMarginId.TabIndex = 106;
            this.lblHwMarginId.Text = "HwMargin ID";
            // 
            // lblColumn
            // 
            this.lblColumn.AutoSize = true;
            this.lblColumn.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColumn.Location = new System.Drawing.Point(22, 201);
            this.lblColumn.Name = "lblColumn";
            this.lblColumn.Size = new System.Drawing.Size(104, 29);
            this.lblColumn.TabIndex = 104;
            this.lblColumn.Text = "Column";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 29);
            this.label1.TabIndex = 101;
            this.label1.Text = "Right";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 29);
            this.label2.TabIndex = 100;
            this.label2.Text = "Left";
            // 
            // lblMargin
            // 
            this.lblMargin.AutoSize = true;
            this.lblMargin.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMargin.Location = new System.Drawing.Point(22, 100);
            this.lblMargin.Name = "lblMargin";
            this.lblMargin.Size = new System.Drawing.Size(100, 29);
            this.lblMargin.TabIndex = 90;
            this.lblMargin.Text = "Bottom";
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.Location = new System.Drawing.Point(57, 67);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(57, 29);
            this.lblTop.TabIndex = 65;
            this.lblTop.Text = "Top";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(24, 33);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(114, 29);
            this.lblSize.TabIndex = 53;
            this.lblSize.Text = "Font Size";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(276, 83);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(0, 29);
            this.lblAge.TabIndex = 75;
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Location = new System.Drawing.Point(6, 267);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(100, 38);
            this.btnExcel.TabIndex = 5;
            this.btnExcel.Text = "E&xcel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(6, 310);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 38);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear &Text";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // p3
            // 
            this.p3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p3.Controls.Add(this.btnClear);
            this.p3.Controls.Add(this.btnExcel);
            this.p3.Controls.Add(this.btnSave);
            this.p3.Controls.Add(this.btnCancel);
            this.p3.Controls.Add(this.btnEdit);
            this.p3.Controls.Add(this.btnAdd);
            this.p3.Controls.Add(this.btnDelete);
            this.p3.Location = new System.Drawing.Point(253, 52);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(114, 356);
            this.p3.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(6, 114);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 45);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(6, 216);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 45);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(6, 63);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 45);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(6, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 45);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(6, 165);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 45);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // p4
            // 
            this.p4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p4.Controls.Add(this.txtHWPreview);
            this.p4.Location = new System.Drawing.Point(373, 52);
            this.p4.MaximumSize = new System.Drawing.Size(350, 360);
            this.p4.Name = "p4";
            this.p4.Size = new System.Drawing.Size(350, 356);
            this.p4.TabIndex = 3;
            this.p4.TabStop = true;
            // 
            // txtHWPreview
            // 
            this.txtHWPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHWPreview.BackColor = System.Drawing.Color.White;
            this.txtHWPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.txtHWPreview.Location = new System.Drawing.Point(3, 3);
            this.txtHWPreview.MaximumSize = new System.Drawing.Size(350, 360);
            this.txtHWPreview.MaxLength = 500;
            this.txtHWPreview.Multiline = true;
            this.txtHWPreview.Name = "txtHWPreview";
            this.txtHWPreview.ReadOnly = true;
            this.txtHWPreview.Size = new System.Drawing.Size(341, 348);
            this.txtHWPreview.TabIndex = 15;
            this.txtHWPreview.TabStop = false;
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
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(2, 3, 3, 2);
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
            this.dataGridView1.Size = new System.Drawing.Size(999, 389);
            this.dataGridView1.TabIndex = 29;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(3, 414);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(1005, 395);
            this.panel1.TabIndex = 111;
            // 
            // FrmHwMargin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1232, 812);
            this.Controls.Add(this.p4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.p3);
            this.Controls.Add(this.p1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.btnBack);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "FrmHwMargin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmHwMargin";
            this.Load += new System.EventHandler(this.FrmHwMargin_Load);
            this.p1.ResumeLayout(false);
            this.p1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVerticalSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHorizontalSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRightMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBottomMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).EndInit();
            this.p3.ResumeLayout(false);
            this.p4.ResumeLayout(false);
            this.p4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel p1;
        private System.Windows.Forms.Label lblMargin;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblColumn;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Panel p3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.TextBox txtHwMarginId;
        private System.Windows.Forms.Label lblHwMarginId;
        private System.Windows.Forms.Panel p4;
        private System.Windows.Forms.TextBox txtHWPreview;
        private System.Windows.Forms.NumericUpDown nudFontSize;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblRows;
        private System.Windows.Forms.NumericUpDown nudRows;
        private System.Windows.Forms.NumericUpDown nudColumns;
        private System.Windows.Forms.NumericUpDown nudRightMargin;
        private System.Windows.Forms.NumericUpDown nudLeftMargin;
        private System.Windows.Forms.NumericUpDown nudBottomMargin;
        private System.Windows.Forms.NumericUpDown nudTopMargin;
        private System.Windows.Forms.Label lblPageSize;
        private System.Windows.Forms.ComboBox cmbPaperSize;
        private System.Windows.Forms.NumericUpDown nudVerticalSpace;
        private System.Windows.Forms.NumericUpDown nudHorizontalSpace;
        private System.Windows.Forms.Label lblVPadding;
        private System.Windows.Forms.Label lblHPadding;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbRowsType;
    }
}