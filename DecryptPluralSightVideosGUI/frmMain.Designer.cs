namespace DecryptPluralSightVideosGUI
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.listView1 = new System.Windows.Forms.ListView();
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
            this.btnOpenOutput = new System.Windows.Forms.Button();
            this.btnOpenDB = new System.Windows.Forms.Button();
            this.chkDecrypt = new System.Windows.Forms.CheckBox();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            this.chkCreateSub = new System.Windows.Forms.CheckBox();
            this.btnOutput = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.pnlLog = new System.Windows.Forms.Panel();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlCourse = new System.Windows.Forms.Panel();
            this.bgwDecrypt = new System.ComponentModel.BackgroundWorker();
            this.bgwGetCourse = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlOption.SuspendLayout();
            this.pnlLog.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlCourse.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.BackColor = System.Drawing.SystemColors.Control;
            this.listView1.CheckBoxes = true;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(794, 363);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
            // 
            // imgList
            // 
            this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgList.ImageSize = new System.Drawing.Size(16, 16);
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnReadCourse
            // 
            this.btnReadCourse.Location = new System.Drawing.Point(682, 41);
            this.btnReadCourse.Name = "btnReadCourse";
            this.btnReadCourse.Size = new System.Drawing.Size(96, 23);
            this.btnReadCourse.TabIndex = 1;
            this.btnReadCourse.Text = "Read Course";
            this.btnReadCourse.UseVisualStyleBackColor = true;
            this.btnReadCourse.Click += new System.EventHandler(this.btnReadCourse_Click);
            // 
            // btnDecypt
            // 
            this.btnDecypt.Location = new System.Drawing.Point(682, 74);
            this.btnDecypt.Name = "btnDecypt";
            this.btnDecypt.Size = new System.Drawing.Size(96, 23);
            this.btnDecypt.TabIndex = 2;
            this.btnDecypt.Text = "Run";
            this.btnDecypt.UseVisualStyleBackColor = true;
            this.btnDecypt.Click += new System.EventHandler(this.btnDecypt_Click);
            // 
            // txtCoursePath
            // 
            this.txtCoursePath.Location = new System.Drawing.Point(75, 12);
            this.txtCoursePath.Name = "txtCoursePath";
            this.txtCoursePath.Size = new System.Drawing.Size(396, 20);
            this.txtCoursePath.TabIndex = 3;
            // 
            // txtDBPath
            // 
            this.txtDBPath.Location = new System.Drawing.Point(75, 43);
            this.txtDBPath.Name = "txtDBPath";
            this.txtDBPath.Size = new System.Drawing.Size(396, 20);
            this.txtDBPath.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Course path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "DB path";
            // 
            // btnCoursePath
            // 
            this.btnCoursePath.Location = new System.Drawing.Point(472, 10);
            this.btnCoursePath.Name = "btnCoursePath";
            this.btnCoursePath.Size = new System.Drawing.Size(75, 23);
            this.btnCoursePath.TabIndex = 7;
            this.btnCoursePath.Text = "Browse ....";
            this.btnCoursePath.UseVisualStyleBackColor = true;
            this.btnCoursePath.Click += new System.EventHandler(this.btnCoursePath_Click);
            // 
            // btnDBPath
            // 
            this.btnDBPath.Location = new System.Drawing.Point(472, 41);
            this.btnDBPath.Name = "btnDBPath";
            this.btnDBPath.Size = new System.Drawing.Size(75, 23);
            this.btnDBPath.TabIndex = 8;
            this.btnDBPath.Text = "Browse ....";
            this.btnDBPath.UseVisualStyleBackColor = true;
            this.btnDBPath.Click += new System.EventHandler(this.btnDBPath_Click);
            // 
            // pnlOption
            // 
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
            this.pnlOption.Location = new System.Drawing.Point(0, 363);
            this.pnlOption.MinimumSize = new System.Drawing.Size(700, 108);
            this.pnlOption.Name = "pnlOption";
            this.pnlOption.Size = new System.Drawing.Size(794, 108);
            this.pnlOption.TabIndex = 10;
            // 
            // btnOpenOutput
            // 
            this.btnOpenOutput.Location = new System.Drawing.Point(563, 74);
            this.btnOpenOutput.Name = "btnOpenOutput";
            this.btnOpenOutput.Size = new System.Drawing.Size(102, 23);
            this.btnOpenOutput.TabIndex = 17;
            this.btnOpenOutput.Text = "Open Output Folder";
            this.btnOpenOutput.UseVisualStyleBackColor = true;
            this.btnOpenOutput.Click += new System.EventHandler(this.btnOpenOutput_Click);
            // 
            // btnOpenDB
            // 
            this.btnOpenDB.Location = new System.Drawing.Point(563, 41);
            this.btnOpenDB.Name = "btnOpenDB";
            this.btnOpenDB.Size = new System.Drawing.Size(102, 23);
            this.btnOpenDB.TabIndex = 16;
            this.btnOpenDB.Text = "Open DB Folder";
            this.btnOpenDB.UseVisualStyleBackColor = true;
            this.btnOpenDB.Click += new System.EventHandler(this.btnOpenDB_Click);
            // 
            // chkDecrypt
            // 
            this.chkDecrypt.AutoSize = true;
            this.chkDecrypt.Checked = true;
            this.chkDecrypt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDecrypt.Location = new System.Drawing.Point(563, 14);
            this.chkDecrypt.Name = "chkDecrypt";
            this.chkDecrypt.Size = new System.Drawing.Size(63, 17);
            this.chkDecrypt.TabIndex = 15;
            this.chkDecrypt.Text = "Decrypt";
            this.chkDecrypt.UseVisualStyleBackColor = true;
            // 
            // chkDelete
            // 
            this.chkDelete.AutoSize = true;
            this.chkDelete.Location = new System.Drawing.Point(728, 14);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(57, 17);
            this.chkDelete.TabIndex = 14;
            this.chkDelete.Text = "Delete";
            this.chkDelete.UseVisualStyleBackColor = true;
            // 
            // chkCreateSub
            // 
            this.chkCreateSub.AutoSize = true;
            this.chkCreateSub.Checked = true;
            this.chkCreateSub.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCreateSub.Location = new System.Drawing.Point(645, 14);
            this.chkCreateSub.Name = "chkCreateSub";
            this.chkCreateSub.Size = new System.Drawing.Size(77, 17);
            this.chkCreateSub.TabIndex = 13;
            this.chkCreateSub.Text = "Create sub";
            this.chkCreateSub.UseVisualStyleBackColor = true;
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(472, 74);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(75, 23);
            this.btnOutput.TabIndex = 12;
            this.btnOutput.Text = "Browse ....";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Output";
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(75, 76);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(396, 20);
            this.txtOutputPath.TabIndex = 10;
            // 
            // pnlLog
            // 
            this.pnlLog.Controls.Add(this.rtbLog);
            this.pnlLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLog.Location = new System.Drawing.Point(803, 3);
            this.pnlLog.MinimumSize = new System.Drawing.Size(400, 0);
            this.pnlLog.Name = "pnlLog";
            this.pnlLog.Size = new System.Drawing.Size(514, 471);
            this.pnlLog.TabIndex = 11;
            // 
            // rtbLog
            // 
            this.rtbLog.BackColor = System.Drawing.Color.Black;
            this.rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLog.DetectUrls = false;
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLog.ForeColor = System.Drawing.SystemColors.Info;
            this.rtbLog.Location = new System.Drawing.Point(0, 0);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.ShowSelectionMargin = true;
            this.rtbLog.Size = new System.Drawing.Size(514, 471);
            this.rtbLog.TabIndex = 1;
            this.rtbLog.TabStop = false;
            this.rtbLog.Text = "";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlCourse);
            this.pnlMain.Controls.Add(this.pnlOption);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMain.MinimumSize = new System.Drawing.Size(700, 430);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(794, 471);
            this.pnlMain.TabIndex = 12;
            // 
            // pnlCourse
            // 
            this.pnlCourse.Controls.Add(this.listView1);
            this.pnlCourse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCourse.Location = new System.Drawing.Point(0, 0);
            this.pnlCourse.Name = "pnlCourse";
            this.pnlCourse.Size = new System.Drawing.Size(794, 363);
            this.pnlCourse.TabIndex = 11;
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 800F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.pnlLog, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlMain, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1320, 477);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1320, 477);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1145, 480);
            this.Name = "frmMain";
            this.Text = "Decrypt Plural Sight Videos GUI";
            this.pnlOption.ResumeLayout(false);
            this.pnlOption.PerformLayout();
            this.pnlLog.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlCourse.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
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
        private System.Windows.Forms.Panel pnlLog;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlCourse;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.ComponentModel.BackgroundWorker bgwDecrypt;
        private System.ComponentModel.BackgroundWorker bgwGetCourse;
        private System.Windows.Forms.CheckBox chkDelete;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox chkDecrypt;
        private System.Windows.Forms.Button btnOpenOutput;
        private System.Windows.Forms.Button btnOpenDB;
    }
}

