using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeFoneApp
{
    public partial class fHomePage : Form
    {
        //private int borderSize = 2;
        public fHomePage()
        {
            InitializeComponent();
            /*this.Padding = new Padding(borderSize);
            this.BackColor = Color.FromArgb(48, 45, 104);*/
        }
        [DllImport("user32.DLL", EntryPoint = "ReleasseCapture")]
        private extern static void ReleaseCaptrue();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd , int wMsg, int wParam, int lParam);
        private void pbtnAcc_Click(object sender, EventArgs e)
        {
            fAM f = new fAM();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnRestore_Click(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnRestore_Click_1(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void fHomePage_Load(object sender, EventArgs e)
        {
            /*Screen scr = Screen.PrimaryScreen; //đi lấy màn hình chính
            *//*this.Left = (scr.WorkingArea.Width - this.Width) / 2;
            this.Top = (scr.WorkingArea.Height - this.Height) / 2;*/
            
        }

        private void pnTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCaptrue();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pbtnLib_Click(object sender, EventArgs e)
        {
            fBook f = new fBook();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void pbtnReaders_Click(object sender, EventArgs e)
        {
            fReaders f = new fReaders();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void pbtnBill_Click(object sender, EventArgs e)
        {
            fBill f = new fBill();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
    }
}
