using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendMails
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();

            // Remove form borders and make it transparent
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Magenta; // used for transparency
            this.TransparencyKey = Color.Magenta;
            this.TopMost = true;

            // Spinner GIF
            PictureBox spinner = new PictureBox
            {
                Image = Properties.Resources.spinner, // Add your spinner.gif to Resources
                SizeMode = PictureBoxSizeMode.AutoSize,
                BackColor = Color.Transparent,
                Location = new Point(10, 10)
            };
            this.Controls.Add(spinner);

            //// "Sending..." label
            //Label lblStatus = new Label
            //{
            //    Text = "Sending...",
            //    Font = new Font("Segoe UI", 10, FontStyle.Bold),
            //    ForeColor = Color.IndianRed,
            //    BackColor = Color.Transparent,
            //    AutoSize = true,
            //    Location = new Point((spinner.Width - 100) / 2 + 20, spinner.Bottom + 10) // center under GIF
            //};
            //this.Controls.Add(lblStatus);

            //// Resize form to fit content
            //int width = Math.Max(spinner.Width, lblStatus.Width) + 40;
            //int height = spinner.Height + lblStatus.Height + 30;
            //this.ClientSize = new Size(width, height);
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {

        }
    }



}
