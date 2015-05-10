namespace BackupSync
{
    partial class AddEntryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEntryForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnIzlez = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.odberiSource = new System.Windows.Forms.Button();
            this.tbSourcePath = new System.Windows.Forms.TextBox();
            this.odberiDest = new System.Windows.Forms.Button();
            this.tbDestPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cbCopyAll = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(495, 171);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(116, 38);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnIzlez
            // 
            this.btnIzlez.CausesValidation = false;
            this.btnIzlez.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnIzlez.Location = new System.Drawing.Point(617, 171);
            this.btnIzlez.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnIzlez.Name = "btnIzlez";
            this.btnIzlez.Size = new System.Drawing.Size(116, 38);
            this.btnIzlez.TabIndex = 1;
            this.btnIzlez.Text = "Излез";
            this.btnIzlez.UseVisualStyleBackColor = true;
            this.btnIzlez.Click += new System.EventHandler(this.btnIzlez_Click);
            // 
            // odberiSource
            // 
            this.odberiSource.CausesValidation = false;
            this.odberiSource.Image = global::BackupSync.Properties.Resources.Open_24x24;
            this.odberiSource.Location = new System.Drawing.Point(699, 33);
            this.odberiSource.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.odberiSource.Name = "odberiSource";
            this.odberiSource.Size = new System.Drawing.Size(35, 36);
            this.odberiSource.TabIndex = 2;
            this.odberiSource.UseVisualStyleBackColor = true;
            this.odberiSource.Click += new System.EventHandler(this.odberiSource_Click);
            // 
            // tbSourcePath
            // 
            this.tbSourcePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbSourcePath.Location = new System.Drawing.Point(12, 34);
            this.tbSourcePath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbSourcePath.Name = "tbSourcePath";
            this.tbSourcePath.Size = new System.Drawing.Size(680, 34);
            this.tbSourcePath.TabIndex = 3;
            this.tbSourcePath.TextChanged += new System.EventHandler(this.tbSourcePath_TextChanged);
            // 
            // odberiDest
            // 
            this.odberiDest.CausesValidation = false;
            this.odberiDest.Image = global::BackupSync.Properties.Resources.Open_24x24;
            this.odberiDest.Location = new System.Drawing.Point(699, 116);
            this.odberiDest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.odberiDest.Name = "odberiDest";
            this.odberiDest.Size = new System.Drawing.Size(35, 36);
            this.odberiDest.TabIndex = 4;
            this.odberiDest.UseVisualStyleBackColor = true;
            this.odberiDest.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbDestPath
            // 
            this.tbDestPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbDestPath.Location = new System.Drawing.Point(12, 117);
            this.tbDestPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbDestPath.Name = "tbDestPath";
            this.tbDestPath.Size = new System.Drawing.Size(680, 34);
            this.tbDestPath.TabIndex = 5;
            this.tbDestPath.TextChanged += new System.EventHandler(this.tbSourcePath_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(278, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Локација на оригиналниот директориум:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 98);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Локација на резервната копија:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // cbCopyAll
            // 
            this.cbCopyAll.AutoSize = true;
            this.cbCopyAll.Checked = true;
            this.cbCopyAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCopyAll.Location = new System.Drawing.Point(13, 181);
            this.cbCopyAll.Name = "cbCopyAll";
            this.cbCopyAll.Size = new System.Drawing.Size(201, 21);
            this.cbCopyAll.TabIndex = 8;
            this.cbCopyAll.Text = "Копирај се од оригиналот";
            this.cbCopyAll.UseVisualStyleBackColor = true;
            // 
            // AddEntryForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnIzlez;
            this.ClientSize = new System.Drawing.Size(748, 228);
            this.Controls.Add(this.cbCopyAll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbDestPath);
            this.Controls.Add(this.odberiDest);
            this.Controls.Add(this.tbSourcePath);
            this.Controls.Add(this.odberiSource);
            this.Controls.Add(this.btnIzlez);
            this.Controls.Add(this.btnOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AddEntryForm";
            this.Text = "Додај датотека";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnIzlez;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button odberiSource;
        private System.Windows.Forms.TextBox tbSourcePath;
        private System.Windows.Forms.Button odberiDest;
        private System.Windows.Forms.TextBox tbDestPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.CheckBox cbCopyAll;
    }
}