
namespace SendMails
{
    partial class MailSending
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.cmbPlantation = new System.Windows.Forms.ComboBox();
            this.cmbModule = new System.Windows.Forms.ComboBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.txtBuild = new System.Windows.Forms.TextBox();
            this.lblBuild = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbLocationType = new System.Windows.Forms.ComboBox();
            this.gvEmails = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.JobDone = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmails)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Orange;
            this.button1.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(548, 565);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "Send Mails";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(112, 166);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(533, 167);
            this.textBox2.TabIndex = 7;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // cmbPlantation
            // 
            this.cmbPlantation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlantation.FormattingEnabled = true;
            this.cmbPlantation.Location = new System.Drawing.Point(113, 102);
            this.cmbPlantation.Name = "cmbPlantation";
            this.cmbPlantation.Size = new System.Drawing.Size(243, 24);
            this.cmbPlantation.TabIndex = 8;
            this.cmbPlantation.SelectedIndexChanged += new System.EventHandler(this.cmbPlantation_SelectedIndexChanged);
            // 
            // cmbModule
            // 
            this.cmbModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModule.FormattingEnabled = true;
            this.cmbModule.Location = new System.Drawing.Point(113, 132);
            this.cmbModule.Name = "cmbModule";
            this.cmbModule.Size = new System.Drawing.Size(337, 24);
            this.cmbModule.TabIndex = 9;
            this.cmbModule.SelectedIndexChanged += new System.EventHandler(this.cmbSubject_SelectedIndexChanged);
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(563, 135);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(82, 22);
            this.txtVersion.TabIndex = 10;
            this.txtVersion.TextChanged += new System.EventHandler(this.txtVersion_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(492, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Version -";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Plantation   -";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Module       -";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Points -";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(658, 30);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(404, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = ".";
            // 
            // txtBuild
            // 
            this.txtBuild.Location = new System.Drawing.Point(525, 136);
            this.txtBuild.Name = "txtBuild";
            this.txtBuild.Size = new System.Drawing.Size(120, 22);
            this.txtBuild.TabIndex = 17;
            // 
            // lblBuild
            // 
            this.lblBuild.AutoSize = true;
            this.lblBuild.Location = new System.Drawing.Point(472, 139);
            this.lblBuild.Name = "lblBuild";
            this.lblBuild.Size = new System.Drawing.Size(48, 17);
            this.lblBuild.TabIndex = 18;
            this.lblBuild.Text = "Build -";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 17);
            this.label6.TabIndex = 19;
            this.label6.Text = "Location  -";
            // 
            // cmbLocationType
            // 
            this.cmbLocationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocationType.FormattingEnabled = true;
            this.cmbLocationType.Location = new System.Drawing.Point(113, 72);
            this.cmbLocationType.Name = "cmbLocationType";
            this.cmbLocationType.Size = new System.Drawing.Size(167, 24);
            this.cmbLocationType.TabIndex = 20;
            this.cmbLocationType.SelectedIndexChanged += new System.EventHandler(this.cmbLocationType_SelectedIndexChanged);
            // 
            // gvEmails
            // 
            this.gvEmails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEmails.Location = new System.Drawing.Point(112, 349);
            this.gvEmails.Name = "gvEmails";
            this.gvEmails.RowHeadersWidth = 51;
            this.gvEmails.RowTemplate.Height = 24;
            this.gvEmails.Size = new System.Drawing.Size(533, 210);
            this.gvEmails.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 349);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 17);
            this.label7.TabIndex = 22;
            this.label7.Text = "CC Mails -";
            // 
            // JobDone
            // 
            this.JobDone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.JobDone.FormattingEnabled = true;
            this.JobDone.Location = new System.Drawing.Point(525, 72);
            this.JobDone.Name = "JobDone";
            this.JobDone.Size = new System.Drawing.Size(117, 24);
            this.JobDone.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(358, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(161, 17);
            this.label8.TabIndex = 24;
            this.label8.Text = "Development Done by  -";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(112, 567);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(342, 22);
            this.progressBar1.TabIndex = 55;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 567);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 17);
            this.label9.TabIndex = 56;
            this.label9.Text = "Sending -";
            // 
            // MailSending
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 602);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.JobDone);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.gvEmails);
            this.Controls.Add(this.cmbLocationType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblBuild);
            this.Controls.Add(this.txtBuild);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.cmbModule);
            this.Controls.Add(this.cmbPlantation);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MailSending";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sending Release Mails";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox cmbPlantation;
        private System.Windows.Forms.ComboBox cmbModule;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblBuild;
        private System.Windows.Forms.TextBox txtBuild;
        private System.Windows.Forms.ComboBox cmbLocationType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView gvEmails;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox JobDone;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

