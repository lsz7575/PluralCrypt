namespace PluralCrypt
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lsvCourse = new System.Windows.Forms.ListView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.btnReadCourse = new System.Windows.Forms.Button();
            this.btnDecypt = new System.Windows.Forms.Button();
            this.txtCoursePath = new System.Windows.Forms.TextBox();
            this.txtDBPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCoursePath = new System.Windows.Forms.Button();
            this.btnDBPath = new System.Windows.Forms.Button();
            this.pnlOption = new System.Windows.Forms.Panel();
            this.chkCopyImage = new System.Windows.Forms.CheckBox();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.chkShowErrOnly = new System.Windows.Forms.CheckBox();
            this.chkStartModuleIndexAt1 = new System.Windows.Forms.CheckBox();
            this.chkStartClipIndexAt1 = new System.Windows.Forms.CheckBox();
            this.btnOpenOutput = new System.Windows.Forms.Button();
            this.btnOpenDB = new System.Windows.Forms.Button();
            this.chkDecrypt = new System.Windows.Forms.CheckBox();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            this.chkCreateSub = new System.Windows.Forms.CheckBox();
            this.btnOutput = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlCourse = new System.Windows.Forms.Panel();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.bgwDecrypt = new System.ComponentModel.BackgroundWorker();
            this.bgwGetCourse = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslToolVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslPOPVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnl1 = new System.Windows.Forms.Panel();
            this.pnl2 = new System.Windows.Forms.Panel();
            this.tlsHelp = new System.Windows.Forms.ToolStripDropDownButton();
            this.pnlOption.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlCourse.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.pnl1.SuspendLayout();
            this.pnl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsvCourse
            // 
            this.lsvCourse.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lsvCourse.BackColor = System.Drawing.SystemColors.Control;
            this.lsvCourse.CheckBoxes = true;
            this.lsvCourse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvCourse.HideSelection = false;
            this.lsvCourse.Location = new System.Drawing.Point(0, 0);
            this.lsvCourse.Margin = new System.Windows.Forms.Padding(4);
            this.lsvCourse.MultiSelect = false;
            this.lsvCourse.Name = "lsvCourse";
            this.lsvCourse.Size = new System.Drawing.Size(868, 355);
            this.lsvCourse.TabIndex = 0;
            this.lsvCourse.UseCompatibleStateImageBehavior = false;
            this.lsvCourse.ItemActivate += new System.EventHandler(this.lsvCourse_ItemActivate);
            // 
            // imgList
            // 
            this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgList.ImageSize = new System.Drawing.Size(16, 16);
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnReadCourse
            // 
            this.btnReadCourse.Location = new System.Drawing.Point(88, 116);
            this.btnReadCourse.Margin = new System.Windows.Forms.Padding(4);
            this.btnReadCourse.Name = "btnReadCourse";
            this.btnReadCourse.Size = new System.Drawing.Size(112, 26);
            this.btnReadCourse.TabIndex = 12;
            this.btnReadCourse.Text = "Read course";
            this.btnReadCourse.UseVisualStyleBackColor = true;
            this.btnReadCourse.Click += new System.EventHandler(this.btnReadCourse_Click);
            // 
            // btnDecypt
            // 
            this.btnDecypt.Location = new System.Drawing.Point(396, 116);
            this.btnDecypt.Margin = new System.Windows.Forms.Padding(4);
            this.btnDecypt.Name = "btnDecypt";
            this.btnDecypt.Size = new System.Drawing.Size(112, 26);
            this.btnDecypt.TabIndex = 15;
            this.btnDecypt.Text = "Run";
            this.btnDecypt.UseVisualStyleBackColor = true;
            this.btnDecypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // txtCoursePath
            // 
            this.txtCoursePath.Location = new System.Drawing.Point(88, 13);
            this.txtCoursePath.Margin = new System.Windows.Forms.Padding(4);
            this.txtCoursePath.Name = "txtCoursePath";
            this.txtCoursePath.Size = new System.Drawing.Size(381, 23);
            this.txtCoursePath.TabIndex = 0;
            // 
            // txtDBPath
            // 
            this.txtDBPath.Location = new System.Drawing.Point(88, 49);
            this.txtDBPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtDBPath.Name = "txtDBPath";
            this.txtDBPath.Size = new System.Drawing.Size(381, 23);
            this.txtDBPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Course path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "DB path";
            // 
            // btnCoursePath
            // 
            this.btnCoursePath.Location = new System.Drawing.Point(476, 11);
            this.btnCoursePath.Margin = new System.Windows.Forms.Padding(4);
            this.btnCoursePath.Name = "btnCoursePath";
            this.btnCoursePath.Size = new System.Drawing.Size(77, 26);
            this.btnCoursePath.TabIndex = 3;
            this.btnCoursePath.Text = "Browse ....";
            this.btnCoursePath.UseVisualStyleBackColor = true;
            this.btnCoursePath.Click += new System.EventHandler(this.btnCoursePath_Click);
            // 
            // btnDBPath
            // 
            this.btnDBPath.Location = new System.Drawing.Point(476, 47);
            this.btnDBPath.Margin = new System.Windows.Forms.Padding(4);
            this.btnDBPath.Name = "btnDBPath";
            this.btnDBPath.Size = new System.Drawing.Size(77, 26);
            this.btnDBPath.TabIndex = 4;
            this.btnDBPath.Text = "Browse ....";
            this.btnDBPath.UseVisualStyleBackColor = true;
            this.btnDBPath.Click += new System.EventHandler(this.btnDBPath_Click);
            // 
            // pnlOption
            // 
            this.pnlOption.Controls.Add(this.chkCopyImage);
            this.pnlOption.Controls.Add(this.btnDeselectAll);
            this.pnlOption.Controls.Add(this.btnSelectAll);
            this.pnlOption.Controls.Add(this.chkShowErrOnly);
            this.pnlOption.Controls.Add(this.chkStartModuleIndexAt1);
            this.pnlOption.Controls.Add(this.chkStartClipIndexAt1);
            this.pnlOption.Controls.Add(this.btnOpenOutput);
            this.pnlOption.Controls.Add(this.btnOpenDB);
            this.pnlOption.Controls.Add(this.chkDecrypt);
            this.pnlOption.Controls.Add(this.chkDelete);
            this.pnlOption.Controls.Add(this.chkCreateSub);
            this.pnlOption.Controls.Add(this.btnDecypt);
            this.pnlOption.Controls.Add(this.btnOutput);
            this.pnlOption.Controls.Add(this.btnReadCourse);
            this.pnlOption.Controls.Add(this.label3);
            this.pnlOption.Controls.Add(this.txtOutputPath);
            this.pnlOption.Controls.Add(this.btnCoursePath);
            this.pnlOption.Controls.Add(this.btnDBPath);
            this.pnlOption.Controls.Add(this.txtCoursePath);
            this.pnlOption.Controls.Add(this.label2);
            this.pnlOption.Controls.Add(this.txtDBPath);
            this.pnlOption.Controls.Add(this.label1);
            this.pnlOption.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOption.Location = new System.Drawing.Point(0, 355);
            this.pnlOption.Margin = new System.Windows.Forms.Padding(4);
            this.pnlOption.MinimumSize = new System.Drawing.Size(817, 125);
            this.pnlOption.Name = "pnlOption";
            this.pnlOption.Size = new System.Drawing.Size(868, 153);
            this.pnlOption.TabIndex = 1;
            // 
            // chkCopyImage
            // 
            this.chkCopyImage.AutoSize = true;
            this.chkCopyImage.Location = new System.Drawing.Point(693, 52);
            this.chkCopyImage.Margin = new System.Windows.Forms.Padding(4);
            this.chkCopyImage.Name = "chkCopyImage";
            this.chkCopyImage.Size = new System.Drawing.Size(90, 19);
            this.chkCopyImage.TabIndex = 18;
            this.chkCopyImage.Text = "Copy Image";
            this.toolTip1.SetToolTip(this.chkCopyImage, "Copy course picture to the decrypted folder");
            this.chkCopyImage.UseVisualStyleBackColor = true;
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Location = new System.Drawing.Point(301, 116);
            this.btnDeselectAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(88, 26);
            this.btnDeselectAll.TabIndex = 14;
            this.btnDeselectAll.Text = "Deselect all";
            this.btnDeselectAll.UseVisualStyleBackColor = true;
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(206, 116);
            this.btnSelectAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(88, 26);
            this.btnSelectAll.TabIndex = 13;
            this.btnSelectAll.Text = "Select all";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // chkShowErrOnly
            // 
            this.chkShowErrOnly.AutoSize = true;
            this.chkShowErrOnly.Location = new System.Drawing.Point(570, 52);
            this.chkShowErrOnly.Margin = new System.Windows.Forms.Padding(4);
            this.chkShowErrOnly.Name = "chkShowErrOnly";
            this.chkShowErrOnly.Size = new System.Drawing.Size(109, 19);
            this.chkShowErrOnly.TabIndex = 9;
            this.chkShowErrOnly.Text = "Show error only";
            this.toolTip1.SetToolTip(this.chkShowErrOnly, "Show error only in the log panel");
            this.chkShowErrOnly.UseVisualStyleBackColor = true;
            // 
            // chkStartModuleIndexAt1
            // 
            this.chkStartModuleIndexAt1.AutoSize = true;
            this.chkStartModuleIndexAt1.Checked = true;
            this.chkStartModuleIndexAt1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStartModuleIndexAt1.Location = new System.Drawing.Point(693, 89);
            this.chkStartModuleIndexAt1.Margin = new System.Windows.Forms.Padding(4);
            this.chkStartModuleIndexAt1.Name = "chkStartModuleIndexAt1";
            this.chkStartModuleIndexAt1.Size = new System.Drawing.Size(121, 19);
            this.chkStartModuleIndexAt1.TabIndex = 11;
            this.chkStartModuleIndexAt1.Text = "Module index at 1";
            this.toolTip1.SetToolTip(this.chkStartModuleIndexAt1, "Module index starts at 1 instead of 0");
            this.chkStartModuleIndexAt1.UseVisualStyleBackColor = true;
            // 
            // chkStartClipIndexAt1
            // 
            this.chkStartClipIndexAt1.AutoSize = true;
            this.chkStartClipIndexAt1.Checked = true;
            this.chkStartClipIndexAt1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStartClipIndexAt1.Location = new System.Drawing.Point(570, 89);
            this.chkStartClipIndexAt1.Margin = new System.Windows.Forms.Padding(4);
            this.chkStartClipIndexAt1.Name = "chkStartClipIndexAt1";
            this.chkStartClipIndexAt1.Size = new System.Drawing.Size(101, 19);
            this.chkStartClipIndexAt1.TabIndex = 10;
            this.chkStartClipIndexAt1.Text = "Clip index at 1";
            this.toolTip1.SetToolTip(this.chkStartClipIndexAt1, "Clip index starts at 1 instead of 0");
            this.chkStartClipIndexAt1.UseVisualStyleBackColor = true;
            // 
            // btnOpenOutput
            // 
            this.btnOpenOutput.Location = new System.Drawing.Point(518, 116);
            this.btnOpenOutput.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenOutput.Name = "btnOpenOutput";
            this.btnOpenOutput.Size = new System.Drawing.Size(126, 26);
            this.btnOpenOutput.TabIndex = 16;
            this.btnOpenOutput.Text = "Open output folder";
            this.btnOpenOutput.UseVisualStyleBackColor = true;
            this.btnOpenOutput.Click += new System.EventHandler(this.btnOpenOutput_Click);
            // 
            // btnOpenDB
            // 
            this.btnOpenDB.Location = new System.Drawing.Point(651, 116);
            this.btnOpenDB.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenDB.Name = "btnOpenDB";
            this.btnOpenDB.Size = new System.Drawing.Size(112, 26);
            this.btnOpenDB.TabIndex = 17;
            this.btnOpenDB.Text = "Open DB folder";
            this.btnOpenDB.UseVisualStyleBackColor = true;
            this.btnOpenDB.Click += new System.EventHandler(this.btnOpenDB_Click);
            // 
            // chkDecrypt
            // 
            this.chkDecrypt.AutoSize = true;
            this.chkDecrypt.Checked = true;
            this.chkDecrypt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDecrypt.Location = new System.Drawing.Point(570, 16);
            this.chkDecrypt.Margin = new System.Windows.Forms.Padding(4);
            this.chkDecrypt.Name = "chkDecrypt";
            this.chkDecrypt.Size = new System.Drawing.Size(67, 19);
            this.chkDecrypt.TabIndex = 6;
            this.chkDecrypt.Text = "Decrypt";
            this.chkDecrypt.UseVisualStyleBackColor = true;
            // 
            // chkDelete
            // 
            this.chkDelete.AutoSize = true;
            this.chkDelete.Checked = true;
            this.chkDelete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDelete.Location = new System.Drawing.Point(798, 16);
            this.chkDelete.Margin = new System.Windows.Forms.Padding(4);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(59, 19);
            this.chkDelete.TabIndex = 8;
            this.chkDelete.Text = "Delete";
            this.chkDelete.UseVisualStyleBackColor = true;
            // 
            // chkCreateSub
            // 
            this.chkCreateSub.AutoSize = true;
            this.chkCreateSub.Checked = true;
            this.chkCreateSub.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCreateSub.Location = new System.Drawing.Point(693, 16);
            this.chkCreateSub.Margin = new System.Windows.Forms.Padding(4);
            this.chkCreateSub.Name = "chkCreateSub";
            this.chkCreateSub.Size = new System.Drawing.Size(82, 19);
            this.chkCreateSub.TabIndex = 7;
            this.chkCreateSub.Text = "Create sub";
            this.chkCreateSub.UseVisualStyleBackColor = true;
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(476, 85);
            this.btnOutput.Margin = new System.Windows.Forms.Padding(4);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(77, 26);
            this.btnOutput.TabIndex = 5;
            this.btnOutput.Text = "Browse ....";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 91);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Output";
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(88, 86);
            this.txtOutputPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(381, 23);
            this.txtOutputPath.TabIndex = 2;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlCourse);
            this.pnlMain.Controls.Add(this.pnlOption);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(4, 4);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(4);
            this.pnlMain.MinimumSize = new System.Drawing.Size(0, 496);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(868, 508);
            this.pnlMain.TabIndex = 12;
            // 
            // pnlCourse
            // 
            this.pnlCourse.Controls.Add(this.lsvCourse);
            this.pnlCourse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCourse.Location = new System.Drawing.Point(0, 0);
            this.pnlCourse.Margin = new System.Windows.Forms.Padding(4);
            this.pnlCourse.Name = "pnlCourse";
            this.pnlCourse.Size = new System.Drawing.Size(868, 355);
            this.pnlCourse.TabIndex = 11;
            // 
            // rtbLog
            // 
            this.rtbLog.BackColor = System.Drawing.Color.Black;
            this.rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.rtbLog.ForeColor = System.Drawing.SystemColors.Info;
            this.rtbLog.Location = new System.Drawing.Point(0, 0);
            this.rtbLog.Margin = new System.Windows.Forms.Padding(4);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.ShortcutsEnabled = false;
            this.rtbLog.ShowSelectionMargin = true;
            this.rtbLog.Size = new System.Drawing.Size(466, 510);
            this.rtbLog.TabIndex = 1;
            this.rtbLog.TabStop = false;
            this.rtbLog.Text = "";
            // 
            // bgwDecrypt
            // 
            this.bgwDecrypt.WorkerReportsProgress = true;
            this.bgwDecrypt.WorkerSupportsCancellation = true;
            // 
            // bgwGetCourse
            // 
            this.bgwGetCourse.WorkerReportsProgress = true;
            this.bgwGetCourse.WorkerSupportsCancellation = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 876F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.pnlMain, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1348, 516);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rtbLog);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(879, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(466, 510);
            this.panel1.TabIndex = 13;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslToolVersion,
            this.tslPOPVersion});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1348, 24);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslToolVersion
            // 
            this.tslToolVersion.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tslToolVersion.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.tslToolVersion.Name = "tslToolVersion";
            this.tslToolVersion.Size = new System.Drawing.Size(77, 19);
            this.tslToolVersion.Text = "Tool Version:";
            // 
            // tslPOPVersion
            // 
            this.tslPOPVersion.Name = "tslPOPVersion";
            this.tslPOPVersion.Size = new System.Drawing.Size(181, 19);
            this.tslPOPVersion.Text = "Pluralsight Offline Player Version:";
            // 
            // pnl1
            // 
            this.pnl1.Controls.Add(this.tableLayoutPanel1);
            this.pnl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl1.Location = new System.Drawing.Point(0, 0);
            this.pnl1.Margin = new System.Windows.Forms.Padding(4);
            this.pnl1.Name = "pnl1";
            this.pnl1.Size = new System.Drawing.Size(1348, 516);
            this.pnl1.TabIndex = 15;
            // 
            // pnl2
            // 
            this.pnl2.Controls.Add(this.statusStrip1);
            this.pnl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl2.Location = new System.Drawing.Point(0, 516);
            this.pnl2.Margin = new System.Windows.Forms.Padding(4);
            this.pnl2.Name = "pnl2";
            this.pnl2.Size = new System.Drawing.Size(1348, 25);
            this.pnl2.TabIndex = 16;
            // 
            // tlsHelp
            // 
            this.tlsHelp.Name = "tlsHelp";
            this.tlsHelp.Size = new System.Drawing.Size(23, 23);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1348, 541);
            this.Controls.Add(this.pnl1);
            this.Controls.Add(this.pnl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1364, 580);
            this.MinimumSize = new System.Drawing.Size(1364, 580);
            this.Name = "MainForm";
            this.Text = "PluralCrypt";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.pnlOption.ResumeLayout(false);
            this.pnlOption.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlCourse.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnl1.ResumeLayout(false);
            this.pnl2.ResumeLayout(false);
            this.pnl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvCourse;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.Button btnReadCourse;
        private System.Windows.Forms.Button btnDecypt;
        private System.Windows.Forms.TextBox txtCoursePath;
        private System.Windows.Forms.TextBox txtDBPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCoursePath;
        private System.Windows.Forms.Button btnDBPath;
        private System.Windows.Forms.Panel pnlOption;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.CheckBox chkCreateSub;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlCourse;
        private System.ComponentModel.BackgroundWorker bgwDecrypt;
        private System.ComponentModel.BackgroundWorker bgwGetCourse;
        private System.Windows.Forms.CheckBox chkDelete;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox chkDecrypt;
        private System.Windows.Forms.Button btnOpenOutput;
        private System.Windows.Forms.Button btnOpenDB;
        private System.Windows.Forms.CheckBox chkStartModuleIndexAt1;
        private System.Windows.Forms.CheckBox chkStartClipIndexAt1;
        private System.Windows.Forms.CheckBox chkShowErrOnly;
        private System.Windows.Forms.Button btnDeselectAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chkCopyImage;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel pnl1;
        private System.Windows.Forms.Panel pnl2;
        private System.Windows.Forms.ToolStripStatusLabel tslToolVersion;
        private System.Windows.Forms.ToolStripStatusLabel tslPOPVersion;
        private System.Windows.Forms.ToolStripDropDownButton tlsHelp;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Panel panel1;
    }
}

