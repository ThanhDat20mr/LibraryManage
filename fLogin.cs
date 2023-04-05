using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace WeFoneApp
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
   
        }
       

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
        

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", " Message", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
        
        WeLibEntities con = new WeLibEntities();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (con.ACCOUNTs.Where(p => p.Username == txtUsername.Text && p.Password == txtPwd.Text).Count() > 0)
            {
                MessageBox.Show("Login successful");
                fHomePage f = new fHomePage();
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Login failed");
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void linkRegister_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fResign f = new fResign();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

    }
}
