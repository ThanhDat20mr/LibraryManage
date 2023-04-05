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
    public partial class fRegisterRDs : Form
    {
        public fRegisterRDs()
        {
            InitializeComponent();
        }
        string gender;
        WeLibEntities con = new WeLibEntities();
        void removeError()
        {
            errorProvider1.SetError(txtMssv, null);
            errorProvider1.SetError(txtName, null);
            errorProvider1.SetError(txtPhone, null);
            errorProvider1.SetError(txtAddress, null);
        }
        void setError()
        {
            if (string.IsNullOrEmpty(txtMssv.Text) || con.READERS.Where(p => p.Mssv == txtMssv.Text).Count() > 0 || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtMssv.Text) || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(txtPhone.Text))
            {
                if (con.READERS.Where(p => p.Mssv == txtMssv.Text).Count() > 0 || string.IsNullOrEmpty(txtMssv.Text))
                {
                    if (string.IsNullOrEmpty(txtMssv.Text))
                    {
                        errorProvider1.SetError(txtMssv, "Không được để trống");
                    }
                    else
                    {
                        errorProvider1.SetError(txtMssv, "Already exist");
                    }
                }
                else
                {
                    errorProvider1.SetError(txtMssv, null);
                }
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    errorProvider1.SetError(txtName, "Vui lòng nhập tên.");
                }
                else
                {
                    errorProvider1.SetError(txtName, null);
                }
                if (string.IsNullOrEmpty(txtPhone.Text))
                {
                    errorProvider1.SetError(txtPhone, "Vui lòng nhập số điện thoại");
                }
                else
                {
                    errorProvider1.SetError(txtPhone, null);
                }
                if (string.IsNullOrEmpty(txtAddress.Text))
                {
                    errorProvider1.SetError(txtAddress, "Vui lòng nhập địa chỉ");
                }
                else
                {
                    errorProvider1.SetError(txtAddress, null);
                }

            }
            else
            {
                if (radbtnMale.Checked)
                    gender = "Nam";
                if (radbtnFemale.Checked)
                    gender = "Nữ";
                MessageBox.Show("Add new readers successful!");
                using (WeLibEntities db = new WeLibEntities())
                {
                    db.READERS.Add(new READER() { Mssv = txtMssv.Text, Name = txtName.Text, DOB = dateDOB.Value, Gender = gender , PHAT =Convert.ToInt32( txtTicket.Text), NumPhone = txtPhone.Text, Address = txtAddress.Text  });
                    db.SaveChanges();
                    this.Close();
                    removeError();
                }
            }
            
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            setError();
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Wrong character", "Message");
            }
        }

        private void txtMssv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Wrong character", "Message");
            }
        }
    }
}
