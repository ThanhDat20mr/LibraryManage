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
using Microsoft.Office.Interop.Excel;
using app = Microsoft.Office.Interop.Excel.Application;



namespace WeFoneApp
{
    public partial class fAM : Form
    {
        public fAM()
        {
            InitializeComponent();
            LoadDataAcc();
        }
        string name;
        string mssv;
        string numphone;
        string username;
        string password;
        void LoadDataAcc()
        {
            using (WeLibEntities db = new WeLibEntities())
            {
                var result = from c in db.ACCOUNTs

                             select c;
                dtgvAcc.DataSource = result.ToList();
            }


        }
        private void exportExcel(DataGridView g , string nameExcel)
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
                worksheet.Name = "Account Infomation";

                //export header
                for(int i = 0; i < dtgvAcc.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dtgvAcc.Columns[i].HeaderText;
                }
                //export content
                for(int i = 0; i < dtgvAcc.RowCount; i++)
                {
                    for(int j = 0; j < dtgvAcc.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dtgvAcc.Rows[i].Cells[j].Value.ToString();

                    }

                }
                //save
                workbook.SaveAs(nameExcel);
                workbook.Close();
                excel.Quit();
                MessageBox.Show("Export successful.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook = null;
                worksheet = null;
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete?", " Message", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                using (WeLibEntities db = new WeLibEntities())
                {                   
                    ACCOUNT sv = db.ACCOUNTs.Where(p => p.Username == txtUsername.Text).SingleOrDefault();
                    db.ACCOUNTs.Remove(sv);
                    db.SaveChanges();
                    LoadDataAcc();
                }
            }
        }
        WeLibEntities con = new WeLibEntities();
        void AddBinding()
        {
            txtMSSV.DataBindings.Add(new Binding("Text", dtgvAcc.DataSource, "Mssv", true, DataSourceUpdateMode.Never));
            txtName.DataBindings.Add(new Binding("Text", dtgvAcc.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtPwd.DataBindings.Add(new Binding("Text", dtgvAcc.DataSource, "Password", true, DataSourceUpdateMode.Never));

        }
        private void btnEdit_Click(object sender, EventArgs e)
        {

            btnCancel.Visible = true;
            btnConfirm.Visible = true;
            
        }





        private void fAM_Load(object sender, EventArgs e)
        {
        }

        

        private void dtgvAcc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dtgvAcc.CurrentRow.Index;
            txtName.Text = dtgvAcc.Rows[i].Cells[1].Value.ToString();
            txtMSSV.Text = dtgvAcc.Rows[i].Cells[0].Value.ToString();
            txtUsername.Text = dtgvAcc.Rows[i].Cells[2].Value.ToString();
            txtPwd.Text = dtgvAcc.Rows[i].Cells[3].Value.ToString();
            
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /*private void btnRestore_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }*/
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("File được lưu tại: D:\\AccExcel", " Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                exportExcel(dtgvAcc, @"D:\AccExcel.xlsx");
            }
            
        }

    

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            fResign f = new fResign();
            f.ShowDialog();
            LoadDataAcc();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)    //Enter key for search the account infomation
            {
                using (WeLibEntities db = new WeLibEntities())
                {
                    string search = txtSearch.Text;
                    ACCOUNT acc = db.ACCOUNTs.Find(search);
                    if(con.ACCOUNTs.Where(p => p.Username == txtSearch.Text).Count() > 0)
                    {
                        txtMSSV.Text = acc.Mssv;
                        txtName.Text = acc.Name;
                        txtUsername.Text = acc.Username;
                        txtPwd.Text = acc.Password;
                    }
                    else
                    {

                        MessageBox.Show("This is not a username or the username does not exist.","Messeage");
                    }
                    
                    
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            using (WeLibEntities db = new WeLibEntities())
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPwd.Text))
                {

                    if (string.IsNullOrEmpty(txtName.Text))
                    {
                        errorProvider1.SetError(txtName, "Vui lòng nhập tên.");
                    }
                    else
                    {
                        errorProvider1.SetError(txtName, null);
                    }

                    if (string.IsNullOrEmpty(txtPwd.Text))
                    {
                        errorProvider1.SetError(txtPwd, "Vui lòng nhập mật khẩu.");
                    }
                    else
                    {
                        errorProvider1.SetError(txtPwd, null);
                    }
                }
                else
                {
                    MessageBox.Show("Update successful");
                    username = txtUsername.Text;
                    ACCOUNT acc = db.ACCOUNTs.Find(username);
                    acc.Name = txtName.Text;
                    acc.Password = txtPwd.Text;
                    db.SaveChanges();
                    LoadDataAcc();
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
