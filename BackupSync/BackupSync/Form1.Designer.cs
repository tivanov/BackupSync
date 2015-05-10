namespace BackupSync
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.postavuvanjaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.izlezToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lvEntries = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnIzbrisi = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.опцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.синхронизирајГиСитеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заприСинхронизацијаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.излезToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.помошToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заПрограматаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pbStatus = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbRecent = new System.Windows.Forms.ListBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnToggle = new System.Windows.Forms.Button();
            this.tbDestFull = new System.Windows.Forms.TextBox();
            this.tbOriginalFull = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkerTimer = new System.Windows.Forms.Timer(this.components);
            this.ErrorBaloonTimer = new System.Windows.Forms.Timer(this.components);
            this.ttImage = new System.Windows.Forms.ToolTip(this.components);
            this.trayMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // trayIcon
            // 
            this.trayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.trayIcon.BalloonTipText = "BackupSync ги синхронизира вашите податоци во позадина.";
            this.trayIcon.BalloonTipTitle = "BackupSync";
            this.trayIcon.ContextMenuStrip = this.trayMenu;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "BackupSync";
            this.trayIcon.Visible = true;
            this.trayIcon.DoubleClick += new System.EventHandler(this.trayIcon_DoubleClick);
            this.trayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseDoubleClick);
            // 
            // trayMenu
            // 
            this.trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.postavuvanjaToolStripMenuItem,
            this.toolStripMenuItem1,
            this.izlezToolStripMenuItem});
            this.trayMenu.Name = "trayMenu";
            this.trayMenu.Size = new System.Drawing.Size(171, 58);
            // 
            // postavuvanjaToolStripMenuItem
            // 
            this.postavuvanjaToolStripMenuItem.Name = "postavuvanjaToolStripMenuItem";
            this.postavuvanjaToolStripMenuItem.Size = new System.Drawing.Size(170, 24);
            this.postavuvanjaToolStripMenuItem.Text = "Поставувања";
            this.postavuvanjaToolStripMenuItem.Click += new System.EventHandler(this.postavuvanjaToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(167, 6);
            // 
            // izlezToolStripMenuItem
            // 
            this.izlezToolStripMenuItem.Name = "izlezToolStripMenuItem";
            this.izlezToolStripMenuItem.Size = new System.Drawing.Size(170, 24);
            this.izlezToolStripMenuItem.Text = "Излез";
            this.izlezToolStripMenuItem.Click += new System.EventHandler(this.izlezToolStripMenuItem_Click);
            // 
            // lvEntries
            // 
            this.lvEntries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvEntries.Dock = System.Windows.Forms.DockStyle.Top;
            this.lvEntries.Location = new System.Drawing.Point(7, 22);
            this.lvEntries.MultiSelect = false;
            this.lvEntries.Name = "lvEntries";
            this.lvEntries.Size = new System.Drawing.Size(906, 223);
            this.lvEntries.TabIndex = 1;
            this.lvEntries.UseCompatibleStateImageBehavior = false;
            this.lvEntries.View = System.Windows.Forms.View.Details;
            this.lvEntries.SelectedIndexChanged += new System.EventHandler(this.lvEntries_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Оригинална Датотека";
            this.columnHeader1.Width = 453;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Резервна Копија";
            this.columnHeader2.Width = 449;
            // 
            // btnDodaj
            // 
            this.btnDodaj.Image = global::BackupSync.Properties.Resources.Add_24x24;
            this.btnDodaj.Location = new System.Drawing.Point(629, 251);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(139, 49);
            this.btnDodaj.TabIndex = 2;
            this.btnDodaj.Text = "Додај";
            this.btnDodaj.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDodaj.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // btnIzbrisi
            // 
            this.btnIzbrisi.Image = global::BackupSync.Properties.Resources.Remove_24x24;
            this.btnIzbrisi.Location = new System.Drawing.Point(774, 251);
            this.btnIzbrisi.Name = "btnIzbrisi";
            this.btnIzbrisi.Size = new System.Drawing.Size(139, 49);
            this.btnIzbrisi.TabIndex = 4;
            this.btnIzbrisi.Text = "Избриши";
            this.btnIzbrisi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIzbrisi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnIzbrisi.UseVisualStyleBackColor = true;
            this.btnIzbrisi.Click += new System.EventHandler(this.btnIzbrisi_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvEntries);
            this.groupBox1.Controls.Add(this.btnDodaj);
            this.groupBox1.Controls.Add(this.btnIzbrisi);
            this.groupBox1.Location = new System.Drawing.Point(12, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(7);
            this.groupBox1.Size = new System.Drawing.Size(920, 308);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Листа на синхронизирани директориуми";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.опцииToolStripMenuItem,
            this.помошToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(943, 28);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // опцииToolStripMenuItem
            // 
            this.опцииToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.синхронизирајГиСитеToolStripMenuItem,
            this.заприСинхронизацијаToolStripMenuItem,
            this.toolStripMenuItem2,
            this.излезToolStripMenuItem});
            this.опцииToolStripMenuItem.Name = "опцииToolStripMenuItem";
            this.опцииToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.опцииToolStripMenuItem.Text = "Опции";
            // 
            // синхронизирајГиСитеToolStripMenuItem
            // 
            this.синхронизирајГиСитеToolStripMenuItem.Name = "синхронизирајГиСитеToolStripMenuItem";
            this.синхронизирајГиСитеToolStripMenuItem.Size = new System.Drawing.Size(223, 24);
            this.синхронизирајГиСитеToolStripMenuItem.Text = "Надгледувај ги сите";
            this.синхронизирајГиСитеToolStripMenuItem.Click += new System.EventHandler(this.синхронизирајГиСитеToolStripMenuItem_Click);
            // 
            // заприСинхронизацијаToolStripMenuItem
            // 
            this.заприСинхронизацијаToolStripMenuItem.Name = "заприСинхронизацијаToolStripMenuItem";
            this.заприСинхронизацијаToolStripMenuItem.Size = new System.Drawing.Size(223, 24);
            this.заприСинхронизацијаToolStripMenuItem.Text = "Запри надгледување";
            this.заприСинхронизацијаToolStripMenuItem.Click += new System.EventHandler(this.заприСинхронизацијаToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(220, 6);
            // 
            // излезToolStripMenuItem
            // 
            this.излезToolStripMenuItem.Name = "излезToolStripMenuItem";
            this.излезToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.излезToolStripMenuItem.Size = new System.Drawing.Size(223, 24);
            this.излезToolStripMenuItem.Text = "Излез";
            this.излезToolStripMenuItem.Click += new System.EventHandler(this.излезToolStripMenuItem_Click);
            // 
            // помошToolStripMenuItem
            // 
            this.помошToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.заПрограматаToolStripMenuItem});
            this.помошToolStripMenuItem.Name = "помошToolStripMenuItem";
            this.помошToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.помошToolStripMenuItem.Text = "Помош";
            // 
            // заПрограматаToolStripMenuItem
            // 
            this.заПрограматаToolStripMenuItem.Name = "заПрограматаToolStripMenuItem";
            this.заПрограматаToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.заПрограматаToolStripMenuItem.Text = "За Програмата";
            this.заПрограматаToolStripMenuItem.Click += new System.EventHandler(this.заПрограматаToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pbStatus);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.lblStatus);
            this.groupBox2.Controls.Add(this.btnToggle);
            this.groupBox2.Controls.Add(this.tbDestFull);
            this.groupBox2.Controls.Add(this.tbOriginalFull);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 360);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(920, 249);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Информации за директориум";
            // 
            // pbStatus
            // 
            this.pbStatus.Location = new System.Drawing.Point(224, 175);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(48, 48);
            this.pbStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbStatus.TabIndex = 10;
            this.pbStatus.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbRecent);
            this.groupBox3.Location = new System.Drawing.Point(505, 22);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(7);
            this.groupBox3.Size = new System.Drawing.Size(408, 209);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Неодамна Синхронизирани";
            // 
            // lbRecent
            // 
            this.lbRecent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRecent.FormattingEnabled = true;
            this.lbRecent.ItemHeight = 16;
            this.lbRecent.Location = new System.Drawing.Point(7, 22);
            this.lbRecent.Name = "lbRecent";
            this.lbRecent.Size = new System.Drawing.Size(394, 180);
            this.lbRecent.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(9, 175);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(209, 48);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnToggle
            // 
            this.btnToggle.Location = new System.Drawing.Point(310, 176);
            this.btnToggle.Name = "btnToggle";
            this.btnToggle.Size = new System.Drawing.Size(174, 48);
            this.btnToggle.TabIndex = 7;
            this.btnToggle.Text = "btnToggle";
            this.btnToggle.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnToggle.UseVisualStyleBackColor = true;
            this.btnToggle.Visible = false;
            this.btnToggle.Click += new System.EventHandler(this.btnToggle_Click);
            // 
            // tbDestFull
            // 
            this.tbDestFull.Location = new System.Drawing.Point(6, 87);
            this.tbDestFull.Name = "tbDestFull";
            this.tbDestFull.ReadOnly = true;
            this.tbDestFull.Size = new System.Drawing.Size(478, 22);
            this.tbDestFull.TabIndex = 5;
            // 
            // tbOriginalFull
            // 
            this.tbOriginalFull.Location = new System.Drawing.Point(6, 42);
            this.tbOriginalFull.Name = "tbOriginalFull";
            this.tbOriginalFull.ReadOnly = true;
            this.tbOriginalFull.Size = new System.Drawing.Size(478, 22);
            this.tbOriginalFull.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Целосна патека на копија";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Целосна патека на оригинал";
            // 
            // checkerTimer
            // 
            this.checkerTimer.Enabled = true;
            this.checkerTimer.Interval = 1000;
            this.checkerTimer.Tick += new System.EventHandler(this.checkerTimer_Tick);
            // 
            // ErrorBaloonTimer
            // 
            this.ErrorBaloonTimer.Interval = 600000;
            this.ErrorBaloonTimer.Tick += new System.EventHandler(this.ErrorBaloonTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 621);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "BackupSync";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.trayMenu.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayMenu;
        private System.Windows.Forms.ToolStripMenuItem postavuvanjaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem izlezToolStripMenuItem;
        private System.Windows.Forms.ListView lvEntries;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Button btnIzbrisi;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem опцииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem синхронизирајГиСитеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заприСинхронизацијаToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem излезToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem помошToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заПрограматаToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnToggle;
        private System.Windows.Forms.TextBox tbDestFull;
        private System.Windows.Forms.TextBox tbOriginalFull;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Timer checkerTimer;
        private System.Windows.Forms.Timer ErrorBaloonTimer;
        private System.Windows.Forms.ListBox lbRecent;
        private System.Windows.Forms.PictureBox pbStatus;
        private System.Windows.Forms.ToolTip ttImage;
    }
}

