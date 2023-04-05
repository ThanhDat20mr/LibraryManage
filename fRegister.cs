using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeFoneApp
{
    public partial class fResign : Form 
    {
        public fResign()
        {
            InitializeComponent();
        }
        WeLibEntities con = new WeLibEntities();
        void removeError()
        {
            errorProvider1.SetError(txtUsername, null);
            errorProvider1.SetError(txtName, null);
            errorProvider1.SetError(txtMSSV, null);
            errorProvider1.SetError(txtPwd, null);
        }
        void setError()
        {
            if (string.IsNullOrEmpty(txtUsername.Text)|| con.ACCOUNTs.Where(p => p.Username == txtUsername.Text).Count() > 0   || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtMSSV.Text) || string.IsNullOrEmpty(txtPwd.Text) || string.IsNullOrEmpty(txtRepwd.Text) || txtRepwd.Text.CompareTo(txtPwd.Text) != 0)
            {
                if (con.ACCOUNTs.Where(p => p.Username == txtUsername.Text).Count() > 0 || string.IsNullOrEmpty(txtUsername.Text))
                {
                    if (string.IsNullOrEmpty(txtUsername.Text))
                    {
                        errorProvider1.SetError(txtUsername, "Không được để trống");
                    }
                    else
                    {
                        errorProvider1.SetError(txtUsername, "Already exist");
                    }
                }
                else
                {
                    errorProvider1.SetError(txtUsername, null);
                }                
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    errorProvider1.SetError(txtName, "Vui lòng nhập tên.");
                }
                else
                {
                    errorProvider1.SetError(txtName, null);
                }
                if (string.IsNullOrEmpty(txtMSSV.Text))
                {
                    errorProvider1.SetError(txtMSSV, "Vui lòng nhập mã sinh viên");
                }
                else
                {
                    errorProvider1.SetError(txtMSSV, null);
                }
                if (string.IsNullOrEmpty(txtPwd.Text))
                {
                    errorProvider1.SetError(txtPwd, "Vui lòng nhập mật khẩu.");
                }
                else
                {
                    errorProvider1.SetError(txtPwd, null);
                }
                if (string.IsNullOrEmpty(txtRepwd.Text))
                {
                    errorProvider1.SetError(txtRepwd, "Vui lòng nhập lại mật khẩu.");
                }
                else
                {
                    errorProvider1.SetError(txtRepwd, null);
                }
                if (txtRepwd.Text.CompareTo(txtPwd.Text) != 0)
                {
                    errorProvider1.SetError(txtRepwd, "Không giống mật khẩu đã nhập.");
                }
                else
                {
                    errorProvider1.SetError(txtRepwd, null);
                }
            }
            else
            {

                MessageBox.Show("Register successful!");
                using (WeLibEntities db = new WeLibEntities())
                {
                    db.ACCOUNTs.Add(new ACCOUNT() { Name = txtName.Text, Mssv = txtMSSV.Text, Username = txtUsername.Text, Password = txtPwd.Text });
                    db.SaveChanges();
                    this.Close();
                    removeError();
                }
            }
            
        }
        private void listAcc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnRegister_Click(object sender, EventArgs e)
        {           
            setError();
        }
        private void fResign_Load(object sender, EventArgs e)
        {

        }

        private void txtMSSV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Wrong character", "Message");
            }
        }
    }

}