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
    public partial class fReaders : Form
    {
        public fReaders()
        {
            InitializeComponent();
            LoadData();
        }
        WeLibEntities con = new WeLibEntities();
        string mssv;
        void LoadData()
        {
            using (WeLibEntities db = new WeLibEntities())
            {
                var result = from c in db.READERS
                             select new { Mssv = c.Mssv, Name = c.Name, DOB = c.DOB, Gender = c.Gender, Ticket = c.PHAT, Phone = c.NumPhone, Address = c.Address  };
                dtgvReaders.DataSource = result.ToList();
                dtgvReaders.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            fRegisterRDs f = new fRegisterRDs();
            f.ShowDialog();
            LoadData();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        void removeError()
        {
            errorProvider1.SetError(txtName, null);
            errorProvider1.SetError(txtPhone, null);
            errorProvider1.SetError(txtAddress, null);
        }
        private void btnSync_Click(object sender, EventArgs e)
        {
            LoadData();
            removeError();
        }

        private void dtgvReaders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dtgvReaders.CurrentRow.Index;
            txtMssv.Text = dtgvReaders.Rows[i].Cells[0].Value.ToString(); // MSSV
            txtName.Text = dtgvReaders.Rows[i].Cells[1].Value.ToString();  //Name
            dpDOB.Text = dtgvReaders.Rows[i].Cells[2].Value.ToString();//DOB
            txtGender.Text = dtgvReaders.Rows[i].Cells[3].Value.ToString();//Gender
            txtTicket.Text = dtgvReaders.Rows[i].Cells[4].Value.ToString(); //ticket
            txtPhone.Text = dtgvReaders.Rows[i].Cells[5].Value.ToString();  //PhoneNum
            string address = dtgvReaders.Rows[i].Cells[6].Value.ToString(); //address
            txtAddress.Text = address;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            mssv = txtMssv.Text;
            if (MessageBox.Show("Are you sure you want to delete?", " Message", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if(con.PHIEUMUONs.Where(p => p.Mssv == mssv).Count() > 0)
                {
                    MessageBox.Show("Độc giả đang mượn sách, không thể xóa.");
                }
                else
                {
                    using (WeLibEntities db = new WeLibEntities())
                    {
                        READER rd = db.READERS.Where(p => p.Mssv == mssv).SingleOrDefault();
                        db.READERS.Remove(rd);
                        var result = from c in db.PHIEUPHATs
                                     where c.Mssv == mssv
                                     select c;
                        foreach(PHIEUPHAT c in result)
                        {
                            db.PHIEUPHATs.Remove(c);
                        }                       
                        db.SaveChanges();
                        LoadData();
                    }
                }
                
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnConfirm.Visible = true;
            btnCancel.Visible = true;
            
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)    //Enter key for search the account infomation
            {
                using (WeLibEntities db = new WeLibEntities())
                {
                    string search = txtSearch.Text;
                    READER rd = db.READERS.Find(search);
                    if (con.READERS.Where(p => p.Mssv == txtSearch.Text).Count() > 0)
                    {
                        txtMssv.Text = rd.Mssv;
                        txtName.Text = rd.Name;
                        txtGender.Text = rd.Gender;
                        txtPhone.Text = rd.NumPhone;
                        txtTicket.Text = rd.PHAT.ToString();
                        dpDOB.Value = rd.DOB.Value;
                        txtAddress.Text = rd.Address;
                    }
                    else
                    {

                        MessageBox.Show("This is not a StudentID or the StudentID does not exist.", "Messeage");
                    }


                }
            }
        }
        private void exportExcel(DataGridView g, string nameExcel)
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;

            try
            {
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;
                workbook = excel.Workbooks.Add(Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                worksheet.Name = "Readers";

                //export header
                for (int i = 0; i < dtgvReaders.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dtgvReaders.Columns[i].HeaderText;
                }
                //export content
                for (int i = 0; i < dtgvReaders.RowCount; i++)
                {
                    for (int j = 0; j < dtgvReaders.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dtgvReaders.Rows[i].Cells[j].Value.ToString();

                    }

                }
                //save
                workbook.SaveAs(nameExcel);
                workbook.Close();
                excel.Quit();
                MessageBox.Show("Export successful.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook = null;
                worksheet = null;
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("File save as: D:\\ReadersExcel", "Message", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                exportExcel(dtgvReaders, @"D:\ReadersExcel.xlsx");
            }
        }

        private void fReaders_Load(object sender, EventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            using (WeLibEntities db = new WeLibEntities())
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtAddress.Text))
                {

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
                        errorProvider1.SetError(txtPhone, "Vui lòng nhập số điện thoại.");
                    }
                    else
                    {
                        errorProvider1.SetError(txtPhone, null);
                    }
                    if (string.IsNullOrEmpty(txtAddress.Text))
                    {
                        errorProvider1.SetError(txtAddress, "Vui lòng nhập địa chỉ.");
                    }
                    else
                    {
                        errorProvider1.SetError(txtAddress, null);
                    }
                }
                else
                {
                    MessageBox.Show("Update successful");
                    mssv = txtMssv.Text;
                    READER rd = db.READERS.Find(mssv);
                    rd.Mssv = txtMssv.Text;
                    rd.Name = txtName.Text;
                    rd.DOB = dpDOB.Value;
                    rd.NumPhone = txtPhone.Text;
                    rd.Address = txtAddress.Text;
                    db.SaveChanges();
                    LoadData();
                    removeError();
                    btnConfirm.Visible = false;
                    btnCancel.Visible = false;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnConfirm.Visible = false;
            btnCancel.Visible = false;
        }
    }
}
