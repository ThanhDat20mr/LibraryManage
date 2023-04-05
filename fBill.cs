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
    public partial class fBill : Form
    {
        public fBill()
        {
            InitializeComponent();
            LoadData();
        }
        WeLibEntities con = new WeLibEntities();
        void LoadData()
        {
            using (WeLibEntities db = new WeLibEntities())
            {
                var result = from c in db.PHIEUMUONs
                             select new { IDPM = c.IDPM, MSSV = c.Mssv, Title = c.BOOK.Title, NgMUON = c.NGM, NgHH = c.NGHH , Status = c.Status, Fee = c.FeeM };
                dtgvPM.DataSource = result.ToList();
                dtgvPM.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
                dtgvPM.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }
        void LoadDataQH()
        {
            using (WeLibEntities db = new WeLibEntities())
            {
                var result = from c in db.PHIEUMUONs
                             where c.Status == "Quá hạn"
                             select new { IDPM = c.IDPM, MSSV = c.Mssv, Title = c.BOOK.Title, NgMUON = c.NGM, NgHH = c.NGHH, Status = c.Status, Fee = c.FeeM };
                dtgvQH.DataSource = result.ToList();
                dtgvQH.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
                dtgvQH.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }
        void LoadDataBook()
        {
            using (WeLibEntities db = new WeLibEntities())
            {
                var result = from c in db.BOOKs
                             select new { IDBook = c.IDBook, Title = c.Title, Category = c.Category, Author = c.Author, Price = c.Price, Des = c.Des };
                dtgvBook.DataSource = result.ToList();
            }


        }
        void LoadDataPP()
        {
            using (WeLibEntities db = new WeLibEntities())
            {
                var result = from c in db.PHIEUPHATs
                             select new { IDPP = c.IDPP, MSSV = c.READER.Mssv, Fee = c.FeeP, Content = c.Content   };
                dtgvRDs.DataSource = result.ToList();
            }
        }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void setError()
        {
            if (string.IsNullOrEmpty(txtIDPM.Text) || con.PHIEUMUONs.Where(p => p.IDPM == txtIDPM.Text).Count() > 0 ||  string.IsNullOrEmpty(txtMssv.Text) ||
                string.IsNullOrEmpty(txtMssv.Text) || con.READERS.Where(p => p.Mssv == txtMssv.Text).Count() == 0 || string.IsNullOrEmpty(txtIDBook.Text))
            {
                if (con.PHIEUMUONs.Where(p => p.IDPM == txtIDPM.Text).Count() > 0 || string.IsNullOrEmpty(txtIDPM.Text))
                {

                    if (string.IsNullOrEmpty(txtIDPM.Text))
                    {
                        errorProvider1.SetError(txtIDPM, "Không được để trống");
                    }
                    else
                    {
                        errorProvider1.SetError(txtIDPM, "Already exist");
                    }                 
                }
                else
                {
                    errorProvider1.SetError(txtIDPM, null);
                }
                if (con.READERS.Where(p => p.Mssv == txtMssv.Text).Count() == 0 || string.IsNullOrEmpty(txtMssv.Text))
                {

                    if (string.IsNullOrEmpty(txtMssv.Text))
                    {
                        errorProvider1.SetError(txtMssv, "Không được để trống");
                    }
                    else
                    {
                        errorProvider1.SetError(txtMssv, "Does not exits");
                    }
                }
                else
                {
                    errorProvider1.SetError(txtMssv, null);
                }
                if (string.IsNullOrEmpty(txtIDBook.Text))
                {
                    errorProvider1.SetError(txtIDBook, "Không được để trống");
                }
                else
                {
                    errorProvider1.SetError(txtIDBook, null);
                }             
            }         
        }
        void setError1()
        {
            if (string.IsNullOrEmpty(txtPidpp.Text) || con.PHIEUPHATs.Where(p => p.IDPP == txtPidpp.Text).Count() > 0 || string.IsNullOrEmpty(txtpFee.Text) ||
                string.IsNullOrEmpty(txtPmssv.Text) || con.READERS.Where(p => p.Mssv == txtPmssv.Text).Count() == 0 || string.IsNullOrEmpty(txtpContent.Text)      )       
            {
                if (con.PHIEUPHATs.Where(p => p.IDPP == txtPidpp.Text).Count() > 0 || string.IsNullOrEmpty(txtPidpp.Text))
                {

                    if (string.IsNullOrEmpty(txtPidpp.Text))
                    {
                        errorProvider1.SetError(txtPidpp, "Không được để trống");
                    }
                    else
                    {
                        errorProvider1.SetError(txtPidpp, "Already exist");
                    }
                }
                else
                {
                    errorProvider1.SetError(txtPidpp, null);
                }
               
                if (con.READERS.Where(p => p.Mssv == txtPmssv.Text).Count() == 0 || string.IsNullOrEmpty(txtPmssv.Text))
                {

                    if (string.IsNullOrEmpty(txtPmssv.Text))
                    {
                        errorProvider1.SetError(txtPmssv, "Không được để trống");
                    }
                    else
                    {
                        errorProvider1.SetError(txtPmssv, "Does not exist");
                    }
                }
                else
                {
                    errorProvider1.SetError(txtPmssv, null);
                }
                if (string.IsNullOrEmpty(txtpFee.Text))
                {
                    errorProvider1.SetError(txtpFee, "Không được để trống");
                }
                else
                {
                    errorProvider1.SetError(txtpFee, null);
                }
                if (string.IsNullOrEmpty(txtpContent.Text))
                {
                    errorProvider1.SetError(txtpContent, "Không được để trống");
                }
                else
                {
                    errorProvider1.SetError(txtpContent, null);
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void removeError()
        {
            errorProvider1.SetError(txtIDPM, null);
            errorProvider1.SetError(txtMssv, null);
            errorProvider1.SetError(txtIDBook, null);
            errorProvider1.SetError(txtPidpp, null);
            errorProvider1.SetError(txtpFee, null);
            errorProvider1.SetError(txtPmssv, null);
        }
        private void btnSync_Click(object sender, EventArgs e)
        {
            LoadData();
            LoadDataBook();
            LoadDataPP();
            LoadDataQH();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(gbxPM.Visible == true)
            {
                btnConfirm.Visible = true;
                btnConfirm.Enabled = true;
                btnCancel.Visible = true;
                btnCancel.Enabled = true;
                txtIDPM.ReadOnly = false;
                txtIDPM.Enabled = true;
                txtIDPM.Clear();
                txtMssv.Clear();
                txtIDBook.Clear();
                txtTitle.Clear();
                txtFee.Clear();

            }
            if (gbxPP.Visible == true)
            {
                btnPConfirm.Visible = true;
                btnPCancel.Visible = true;
                txtpContent.Enabled = true;
                txtpFee.Enabled = true;
                txtPidpp.Enabled = true;
                txtPmssv.Clear();
                txtpContent.Clear();
                txtpFee.Clear();
                txtPidpm.Clear();
                txtPidpp.Clear();

            }
            if (gbxQL.Visible == true)
                MessageBox.Show("Can't create");
            if (gbxQLPP.Visible == true)
                MessageBox.Show("Can't create");

        }

        private void btnPM_Click(object sender, EventArgs e)
        {
            gbxQL.Visible = false;
            gbxPP.Visible = false;
            gbxQLPP.Visible = false;
            gbxPM.Visible = true;
            btnHH.Visible = true;
            LoadDataBook();
        }

        private void btnPP_Click(object sender, EventArgs e)
        {
            gbxQL.Visible = false;
            gbxPM.Visible = false;
            gbxQLPP.Visible = false;
            gbxPP.Visible = true;
            btnHH.Visible = true;
            LoadDataQH();
        }
        private void btnQLPP_Click(object sender, EventArgs e)
        {
            gbxQL.Visible = false;
            gbxPM.Visible = false;
            gbxPP.Visible = false;
            gbxQLPP.Visible = true;
            LoadDataPP();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            removeError();
            btnConfirm.Visible = false;
            btnCancel.Visible = false;

        }
        private void btnHomePM_Click(object sender, EventArgs e)
        {
            gbxPM.Visible = false;
            gbxPP.Visible = false;
            gbxQLPP.Visible = false;
            gbxQL.Visible = true;
        }
        void blockText()
        {
            textMSSV.Enabled = false;
            textPM.Enabled = false;
            textStatus.Enabled = false;
            textTitle.Enabled = false;
            dateTimeNM.Enabled = false;
        }
        private void btnRetime_Click(object sender, EventArgs e)
        {
            blockText();
            btnAcp.Visible = true;
            btnCan.Visible = true;
        }
        private void btnCan_Click(object sender, EventArgs e)
        {
            btnAcp.Visible = false;
            btnCan.Visible = false;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnPCancel_Click(object sender, EventArgs e)
        {
            btnPCancel.Visible = false;
            btnPConfirm.Visible = false;
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)    //Enter key for search the book infomation
            {
                using (WeLibEntities db = new WeLibEntities())
                {
                    string search = txtSearch.Text.Trim();
                    var result = from c in db.BOOKs.Where(p => p.Title.Contains(search) || p.IDBook.Contains(search))
                                 select new { IDBook = c.IDBook, Title = c.Title, Category = c.Category, Author = c.Author, Price = c.Price, Des = c.Des };
                    dtgvBook.DataSource = result.ToList();
                }
            }
        }
        private void txtSearchStudentID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)    //Enter key for search the StudentID
            {
                using (WeLibEntities db = new WeLibEntities())
                {
                    string search = txtSearchStudentID.Text.Trim();
                    var result = from c in db.PHIEUMUONs.Where(p => p.Mssv == search)
                                 select new { IDPM = c.IDPM, MSSV = c.Mssv, Title = c.BOOK.Title, NgMUON = c.NGM, NgHH = c.NGHH, Status = c.Status, Fee = c.FeeM };
                    dtgvPM.DataSource = result.ToList();
                    dtgvPM.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dtgvPM.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        string status;
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIDPM.Text) || con.PHIEUMUONs.Where(p => p.IDPM == txtIDPM.Text).Count() > 0 || 
                string.IsNullOrEmpty(txtMssv.Text) || con.READERS.Where(p => p.Mssv == txtMssv.Text).Count() == 0|| string.IsNullOrEmpty(txtIDBook.Text))
            {
                setError();
            }          
            else
            {
                string title = txtTitle.Text;
                mssv = txtMssv.Text;
                if( con.PHIEUMUONs.Where(p => p.Mssv == mssv && p.BOOK.Title == title && p.Status == "Chưa trả" || p.Status == "Gia hạn").Count()>0)
                {
                    MessageBox.Show("Sách đã mượn và chưa trả.");
                }
                else
                {
                    MessageBox.Show("Add successful!");
                    using (WeLibEntities db = new WeLibEntities())
                    {
                        if (radbtnChuaTra.Checked)
                        {
                            status = "Chưa trả";
                        }

                        db.PHIEUMUONs.Add(new PHIEUMUON()
                        {
                            IDPM = txtIDPM.Text,
                            Mssv = txtMssv.Text,
                            IDBook = txtIDBook.Text,
                            NGM = dpNgM.Value,
                            NGHH = dpNgHH.Value,
                            Status = status,
                            FeeM = Convert.ToDecimal(txtFee.Text)
                        });
                        db.SaveChanges();
                        removeError();
                        LoadData();
                        btnConfirm.Enabled = false;
                        btnConfirm.Visible = false;
                        btnCancel.Enabled = false;
                        btnCancel.Visible = false;
                        txtIDPM.ReadOnly = true;
                        txtIDPM.Enabled = false;
                    }
                }                                  
            }
        }
        

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        string mssv;
        string idpm;
        string nghh;
        string idpp;
        private void dtgvBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(string.IsNullOrEmpty(txtMssv.Text) || con.READERS.Where(p => p.Mssv == txtMssv.Text).Count() == 0)
            {
                if(con.READERS.Where(p => p.Mssv == txtMssv.Text).Count() == 0)
                {
                    errorProvider1.SetError(txtMssv, "Does not exits");
                }
                if(string.IsNullOrEmpty(txtMssv.Text))
                {
                    errorProvider1.SetError(txtMssv, "Is empty");
                }
                
            }
            else
            {
                errorProvider1.SetError(txtMssv, null);
                mssv = txtMssv.Text;
                READER rd = con.READERS.Find(mssv);
                int i;
                i = dtgvBook.CurrentRow.Index;
                txtIDBook.Text = dtgvBook.Rows[i].Cells[0].Value.ToString();
                txtTitle.Text = dtgvBook.Rows[i].Cells[1].Value.ToString();
                decimal fee = 0;
                if (rd.PHAT >= 3 && rd.PHAT < 5)
                {
                    fee = Convert.ToDecimal(dtgvBook.Rows[i].Cells[4].Value.ToString()) / 2;
                }
                if (rd.PHAT > 4)
                {
                    fee = Convert.ToDecimal(Convert.ToDecimal(dtgvBook.Rows[i].Cells[4].Value.ToString()));
                }
                txtFee.Text = fee.ToString();
            }
            

        }
        private void dtgvPM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dtgvPM.CurrentRow.Index;
            textPM.Text = dtgvPM.Rows[i].Cells[0].Value.ToString();
            textMSSV.Text = dtgvPM.Rows[i].Cells[1].Value.ToString();
            textTitle.Text = dtgvPM.Rows[i].Cells[2].Value.ToString();//title
            dateTimeNM.Text = dtgvPM.Rows[i].Cells[3].Value.ToString();//ngmuon
            dateTimeHH.Text = dtgvPM.Rows[i].Cells[4].Value.ToString();//nghh
            textStatus.Text = dtgvPM.Rows[i].Cells[5].Value.ToString();//status
        }
        private void dtgvRDs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dtgvRDs.CurrentRow.Index;
            txtqlidpp.Text = dtgvRDs.Rows[i].Cells[0].Value.ToString();//idpp
            txtmssvql.Text = dtgvRDs.Rows[i].Cells[1].Value.ToString();//mssv
            txtqlfeep.Text = dtgvRDs.Rows[i].Cells[2].Value.ToString();//fee
            txtqlcontent.Text = dtgvRDs.Rows[i].Cells[3].Value.ToString();//content

        }
        private void dtgvQH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dtgvQH.CurrentRow.Index;
            txtPidpm.Text = dtgvQH.Rows[i].Cells[0].Value.ToString();
            txtPmssv.Text = dtgvQH.Rows[i].Cells[1].Value.ToString();

        }
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnAcp_Click(object sender, EventArgs e)
        {
            using(WeLibEntities db = new WeLibEntities())
            {
                idpm = textPM.Text;
                if (db.PHIEUMUONs.Where(p=>p.IDPM == idpm && p.Status == "Quá hạn").Count()>0)
                {
                    MessageBox.Show("Phiếu quá hạn, không thể gia hạn.");
                }
                else
                {
                    PHIEUMUON rd = db.PHIEUMUONs.Find(idpm);
                    rd.NGHH = dateTimeHH.Value;
                    rd.Status = "Gia hạn";
                    db.SaveChanges();
                    MessageBox.Show("Update successful");
                    LoadData();
                }              
            }
            
        }
        private void btnHH_Click(object sender, EventArgs e)
        {
            using (WeLibEntities db = new WeLibEntities())
            {

                idpm = textPM.Text;
                PHIEUMUON rd = db.PHIEUMUONs.Find(idpm);
                rd.Status = "Quá hạn";
                db.SaveChanges();
                MessageBox.Show("Update successful");
                LoadData();
            }
        }      
        private void btnPConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPidpp.Text) || con.PHIEUPHATs.Where(p => p.IDPP == txtPidpp.Text).Count() > 0 || string.IsNullOrEmpty(txtpFee.Text) ||
                string.IsNullOrEmpty(txtPmssv.Text) || con.READERS.Where(p => p.Mssv == txtPmssv.Text).Count() == 0)
            {
                setError1();
            }
            else
            {
                MessageBox.Show("Add successful!");
                using (WeLibEntities db = new WeLibEntities())
                {
                    int ticket=1;
                    mssv = txtPmssv.Text;
                    idpm = txtPidpm.Text;
                    db.PHIEUPHATs.Add(new PHIEUPHAT() {IDPP = txtPidpp.Text, Mssv = txtPmssv.Text, FeeP = Convert.ToDecimal(txtpFee.Text), Content = txtpContent.Text});;
                    READER rd =  db.READERS.Find(mssv);
                    PHIEUMUON pm = db.PHIEUMUONs.Find(idpm);
                    db.PHIEUMUONs.Remove(pm);
                    rd.PHAT = rd.PHAT + ticket; 
                    db.SaveChanges();
                    removeError();
                    LoadDataQH();
                    LoadData();
                    btnConfirm.Enabled = false;
                    btnConfirm.Visible = false;
                    btnCancel.Enabled = false;
                    btnCancel.Visible = false;
                    txtpContent.Enabled = false;
                    txtpFee.Enabled = false;
                    txtPmssv.Enabled = false;
                    txtPidpp.Enabled = false;
                }
            }
        }

        private void btnGiveback_Click(object sender, EventArgs e)
        {
            idpm = textPM.Text;
            if (MessageBox.Show("Are you sure you want to give back?", " Message", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                using (WeLibEntities db = new WeLibEntities())
                {
                    PHIEUMUON pm = db.PHIEUMUONs.Find(idpm);
                    db.PHIEUMUONs.Remove(pm);
                    db.SaveChanges();
                    LoadData();
                }
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                worksheet.Name = "PhieuPhat";

                //export header
                for (int i = 0; i < dtgvRDs.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dtgvRDs.Columns[i].HeaderText;
                }
                //export content
                for (int i = 0; i < dtgvRDs.RowCount; i++)
                {
                    for (int j = 0; j < dtgvRDs.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dtgvRDs.Rows[i].Cells[j].Value.ToString();

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
            if(gbxQLPP.Visible == true)
            {
                if (MessageBox.Show("File được lưu tại: D:\\PhieuPhatExcel và toàn bộ dữ liệu phiếu phạt sẽ bị XÓA!", " Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    exportExcel(dtgvRDs, @"D:\PhieuPhatExcel.xlsx");
                    
                    using(WeLibEntities db = new WeLibEntities())
                    {
                        var rows = from c in db.PHIEUPHATs
                                   select c;
                        foreach (var row in rows)
                        {      
                            db.PHIEUPHATs.Remove(row);
                        }
                        db.SaveChanges();
                    }
                    
                }
            }
            if (gbxPM.Visible == true)
                MessageBox.Show("Can't export data");
            if (gbxPP.Visible == true)
                MessageBox.Show("Can't export data");
            if (gbxQL.Visible == true)
                MessageBox.Show("Can't export data");
        }

        private void txtPmssv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Wrong character", "Message");
            }
        }

        private void txtpFee_KeyPress(object sender, KeyPressEventArgs e)
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
