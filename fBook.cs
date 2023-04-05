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
    public partial class fBook : Form
    {
        public fBook()
        {
            InitializeComponent();          
            load_cbxAuthor();
            //load_cbxCategory();            
            LoadDataBook();
        }
        string idbook;
        string title;
        string author;
        string category;
        string price;
        string description;
        void LoadDataBook()
        {
            using (WeLibEntities db = new WeLibEntities())
            {
                var result = from c in db.BOOKs
                             select new {IDBook = c.IDBook, Title = c.Title, Category = c.Category, Author = c.Author,  Price = c.Price, Des = c.Des };
                dtgvBook.DataSource = result.ToList();
            }


        }
        

        private void btnShow_Click(object sender, EventArgs e)
        {
            LoadDataBook();
        }

        private void dtgvBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dtgvBook.CurrentRow.Index;
            txtIDBook.Text = dtgvBook.Rows[i].Cells[0].Value.ToString();
            txtTitle.Text = dtgvBook.Rows[i].Cells[1].Value.ToString();
            cboCategory.Text = dtgvBook.Rows[i].Cells[2].Value.ToString();//Category
            txtAuthor.Text = dtgvBook.Rows[i].Cells[3].Value.ToString();//Author
            txtPrice.Text = dtgvBook.Rows[i].Cells[4].Value.ToString(); //price
            string des = dtgvBook.Rows[i].Cells[5].Value.ToString(); //des
            rtxtDes.Text = des;          
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fBook_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'weLibDataSet.BOOK' table. You can move, or remove it, as needed.


        }
        WeLibEntities con = new WeLibEntities();
        void removeError()
        {
            errorProvider1.SetError(txtIDBook, null);
            errorProvider1.SetError(txtTitle, null);
            errorProvider1.SetError(txtAuthor, null);
            errorProvider1.SetError(cboCategory, null);
            errorProvider1.SetError(txtPrice, null);
            errorProvider1.SetError(rtxtDes, null);
        }
        void setError()
        {
            if (con.BOOKs.Where(p => p.IDBook == idbook).Count() > 0 || string.IsNullOrEmpty(idbook))
            {
                if (string.IsNullOrEmpty(txtIDBook.Text))
                {
                    errorProvider1.SetError(txtIDBook, "Không được để trống");
                }
                else
                {
                    errorProvider1.SetError(txtIDBook, "Already exist");
                }
            }
            else
            {
                errorProvider1.SetError(txtIDBook, null);
            }
            if (con.BOOKs.Where(p => p.Title == txtTitle.Text).Count() > 0 || string.IsNullOrEmpty(txtTitle.Text))
            {

                if (string.IsNullOrEmpty(txtTitle.Text))
                {
                    errorProvider1.SetError(txtTitle, "Không được để trống");
                }
                else
                {
                    errorProvider1.SetError(txtTitle, "Already exist");
                }
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }
            if (string.IsNullOrEmpty(txtAuthor.Text))
            {
                errorProvider1.SetError(txtAuthor, "Vui lòng nhập tác giả.");
            }
            else
            {
                errorProvider1.SetError(txtAuthor, null);
            }
            if (string.IsNullOrEmpty(cboCategory.Text))
            {
                errorProvider1.SetError(cboCategory, "Vui lòng nhập thể loại.");
            }
            else
            {
                errorProvider1.SetError(cboCategory, null);
            }
            if (string.IsNullOrEmpty(txtPrice.Text))
            {
                errorProvider1.SetError(txtPrice, "Vui lòng nhập mệnh giá.");
            }
            else
            {
                errorProvider1.SetError(txtPrice, null);
            }
            if (string.IsNullOrEmpty(rtxtDes.Text))
            {
                errorProvider1.SetError(rtxtDes, "Vui lòng nhập mô tả.");
            }
            else
            {
                errorProvider1.SetError(rtxtDes, null);
            }
        }
        void setError1()
        {
            if (string.IsNullOrEmpty(txtTitle.Text) || con.BOOKs.Where(p => p.Title == txtTitle.Text).Count() > 0 || string.IsNullOrEmpty(txtAuthor.Text) || string.IsNullOrEmpty(cboCategory.Text) || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(rtxtDes.Text))
            {
                
                if (con.BOOKs.Where(p => p.Title == txtTitle.Text).Count() > 0 || string.IsNullOrEmpty(txtTitle.Text))
                {

                    if (string.IsNullOrEmpty(txtTitle.Text))
                    {
                        errorProvider1.SetError(txtTitle, "Không được để trống");
                    }
                    else
                    {
                        errorProvider1.SetError(txtTitle, "Already exist");
                    }
                }
                else
                {
                    errorProvider1.SetError(txtTitle, null);
                }
                if (string.IsNullOrEmpty(txtAuthor.Text))
                {
                    errorProvider1.SetError(txtAuthor, "Vui lòng nhập tác giả.");
                }
                else
                {
                    errorProvider1.SetError(txtAuthor, null);
                }
                if (string.IsNullOrEmpty(cboCategory.Text))
                {
                    errorProvider1.SetError(cboCategory, "Vui lòng nhập thể loại.");
                }
                else
                {
                    errorProvider1.SetError(cboCategory, null);
                }
                if (string.IsNullOrEmpty(txtPrice.Text))
                {
                    errorProvider1.SetError(txtPrice, "Vui lòng nhập mệnh giá.");
                }
                else
                {
                    errorProvider1.SetError(txtPrice, null);
                }
                if (string.IsNullOrEmpty(rtxtDes.Text))
                {
                    errorProvider1.SetError(rtxtDes, "Vui lòng nhập mô tả.");
                }
                else
                {
                    errorProvider1.SetError(rtxtDes, null);
                }
            }
            

        }
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            btnConfirm.Visible = true;
            btnConfirm.Enabled = true;
            btnCancel.Visible = true;
            btnCancel.Enabled = true;
            txtIDBook.ReadOnly = false;
            txtIDBook.Enabled = true;
            txtIDBook.Clear();
            txtAuthor.Clear();
            txtTitle.Clear();
            rtxtDes.Clear();
            txtPrice.Clear();
               
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            using (WeLibEntities db = new WeLibEntities())

            {
                if ( string.IsNullOrEmpty(txtTitle.Text) || con.BOOKs.Where(p => p.Title == txtTitle.Text).Count() > 0 || string.IsNullOrEmpty(txtAuthor.Text) || string.IsNullOrEmpty(cboCategory.Text) || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(rtxtDes.Text))
                {
                    setError1();
                }
                else
                {
                    MessageBox.Show("Update successful");
                    removeError();                  
                    idbook = txtIDBook.Text;
                    BOOK book = db.BOOKs.Find(idbook);
                    book.Title = txtTitle.Text;
                    book.Price =Convert.ToDecimal( txtPrice.Text);
                    book.Author = txtAuthor.Text;
                    book.Category = cboCategory.Text;
                    book.Des = rtxtDes.Text;
                    db.SaveChanges();
                    LoadDataBook();
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
                worksheet.Name = "Library";

                //export header
                for (int i = 0; i < dtgvBook.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dtgvBook.Columns[i].HeaderText;
                }
                //export content
                for (int i = 0; i < dtgvBook.RowCount; i++)
                {
                    for (int j = 0; j < dtgvBook.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dtgvBook.Rows[i].Cells[j].Value.ToString();

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
            if (MessageBox.Show("File được lưu tại: D:\\BookExcel", " Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                exportExcel(dtgvBook, @"D:\BookExcel.xlsx");
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete?", " Message", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                using (WeLibEntities db = new WeLibEntities())
                {
                    BOOK b = db.BOOKs.Where(p => p.IDBook == txtIDBook.Text).SingleOrDefault();
                    db.BOOKs.Remove(b);
                    db.SaveChanges();
                    LoadDataBook();
                }
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            cbxCategory.Text = "None";
            txtIDBook.Clear();
            txtPrice.Clear();
            txtTitle.Clear();
            cboCategory.Text = "None";
            txtAuthor.Clear();
            rtxtDes.Clear();
            removeError();
            LoadDataBook();
            btnConfirm.Enabled = false;
            btnConfirm.Visible = false;
            btnCancel.Enabled = false;
            btnCancel.Visible = false;
            txtIDBook.ReadOnly = true;
            txtIDBook.Enabled = false;
            cbAuthor.Enabled = true;
            cbxCategory.Enabled = true;
            
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            removeError();
            
            btnConfirm.Enabled = false;
            btnConfirm.Visible = false;
            btnCancel.Enabled = false;
            btnCancel.Visible = false;
            txtIDBook.ReadOnly = true;
            txtIDBook.Enabled = false;
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            idbook = labelCategory.Text + txtIDBook.Text;
            if (string.IsNullOrEmpty(idbook) || con.BOOKs.Where(p => p.IDBook == idbook).Count() > 0 || string.IsNullOrEmpty(txtTitle.Text) || con.BOOKs.Where(p => p.Title == txtTitle.Text).Count() > 0 || string.IsNullOrEmpty(txtAuthor.Text) || string.IsNullOrEmpty(cboCategory.Text) || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(rtxtDes.Text))
            {
                setError();
            }
            else
            {
                MessageBox.Show("Add successful!");
                using (WeLibEntities db = new WeLibEntities())
                {
                    db.BOOKs.Add(new BOOK() { IDBook = idbook, Title = txtTitle.Text, Category = cboCategory.Text, Author = txtAuthor.Text, Price = Convert.ToDecimal(txtPrice.Text), Des = rtxtDes.Text });
                    db.SaveChanges();
                    //this.Close();
                    removeError();
                    LoadDataBook();
                    btnConfirm.Enabled = false;
                    btnConfirm.Visible = false;
                    btnCancel.Enabled = false;
                    btnCancel.Visible = false;
                    txtIDBook.ReadOnly = true;
                    txtIDBook.Enabled = false;
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*void load_cbxCategory()
        {
            using (WeLibEntities db = new WeLibEntities())
            {
                var result = from c in db.BOOKs
                             group c by c.Category into g
                             select g.Key;

                cbxCategory.DataSource = result.ToList();

            }
        }*/
        string selectItem;
        string selectItem1;
        void AutoCategory()
        {
            if (cboCategory.SelectedItem.ToString() == "None")
            {
                labelCategory.Text = "...";
            }
            if (cboCategory.SelectedItem.ToString() == "Tư duy")
            {
                labelCategory.Text = "KT";
            }
            if (cboCategory.SelectedItem.ToString() == "Cổ tích")
            {
                labelCategory.Text = "CT";
            }
            if (cboCategory.SelectedItem.ToString() == "Ngoại ngữ")
            {
                labelCategory.Text = "NN";
            }
            if (cboCategory.SelectedItem.ToString() == "Kinh dị")
            {
                labelCategory.Text = "MA";
            }
            if (cboCategory.SelectedItem.ToString() == "Khoa học")
            {
                labelCategory.Text = "KH";
            }
            if (cboCategory.SelectedItem.ToString() == "Lịch sử")
            {
                labelCategory.Text = "LS";
            }
            if (cboCategory.SelectedItem.ToString() == "Văn học")
            {
                labelCategory.Text = "VH";
            }
            if (cboCategory.SelectedItem.ToString() == "Lập trình")
            {
                labelCategory.Text = "IT";
            }
            

        }
        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCategory();
        }
        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
              
            string selectItemCategory = cbxCategory.SelectedItem.ToString();
            selectItem = cbxCategory.SelectedItem.ToString();
            if(cbAuthor.SelectedItem == null || cbAuthor.SelectedItem.ToString() == "")
            {
                selectItem1 = "";
                
            }
            else
            {
                selectItem1 = cbAuthor.SelectedItem.ToString();
            }
            
            if (selectItemCategory != "None" && selectItem1 !="")
            {
                using (WeLibEntities db = new WeLibEntities())
                {
                    var result = from c in db.BOOKs
                                 where c.Category == selectItemCategory && c.Author == selectItem1
                                 select new { IDBook = c.IDBook, Title = c.Title, Category = c.Category, Author = c.Author, Price = c.Price, Des = c.Des };
                    dtgvBook.DataSource = result.ToList();
                }
            }
            if(selectItemCategory != "None" && selectItem1 =="")
            {
                using (WeLibEntities db = new WeLibEntities())
                {
                    var result = from c in db.BOOKs
                                 where c.Category == selectItemCategory
                                 select new { IDBook = c.IDBook, Title = c.Title, Category = c.Category, Author = c.Author, Price = c.Price, Des = c.Des };
                    dtgvBook.DataSource = result.ToList();
                }
            }
            if(selectItemCategory == "None" && selectItem1 =="")
            {
                LoadDataBook();
            }
            if(selectItemCategory == "None" && selectItem1 != "")
            {
                using (WeLibEntities db = new WeLibEntities())
                {
                    var result = from c in db.BOOKs
                                 where c.Author == selectItem1
                                 select new { IDBook = c.IDBook, Title = c.Title, Category = c.Category, Author = c.Author, Price = c.Price, Des = c.Des };
                    dtgvBook.DataSource = result.ToList();
                }
            }
            
        }
        void load_cbxAuthor()
        {
            using (WeLibEntities db = new WeLibEntities())
            {
                var result = from c in db.BOOKs
                             group c by c.Author into g
                             select g.Key;

                cbAuthor.DataSource = result.ToList();
            }
        }
        private void cbAuthor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectItemAuthor = cbAuthor.SelectedItem.ToString();
            
            if (selectItem== "None" && selectItemAuthor != null || selectItemAuthor!="")
            {                            
                using (WeLibEntities db = new WeLibEntities())
                {
                    var result = from c in db.BOOKs
                                 where c.Author == selectItemAuthor 
                                 select new { IDBook = c.IDBook, Title = c.Title, Category = c.Category, Author = c.Author, Price = c.Price, Des = c.Des };
                    dtgvBook.DataSource = result.ToList();
                }             
            }
            if(selectItem!="None" && selectItemAuthor != null)
            {
                using (WeLibEntities db = new WeLibEntities())
                {
                    var result = from c in db.BOOKs
                                 where c.Author == selectItemAuthor && c.Category ==  selectItem
                                 select new { IDBook = c.IDBook, Title = c.Title, Category = c.Category, Author = c.Author, Price = c.Price, Des = c.Des };
                    dtgvBook.DataSource = result.ToList();
                }
            }
            if(selectItem!="None" && selectItemAuthor == null || selectItemAuthor == "" )
            {
                using (WeLibEntities db = new WeLibEntities())
                {
                    var result = from c in db.BOOKs
                                 where c.Category ==  selectItem
                                 select new { IDBook = c.IDBook, Title = c.Title, Category = c.Category, Author = c.Author, Price = c.Price, Des = c.Des };
                    dtgvBook.DataSource = result.ToList();
                }
            }
            if(selectItem =="None" && selectItemAuthor == "" || selectItemAuthor == null)
            {
                LoadDataBook();
            }
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

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Wrong character", "Message");
            }
        }

        private void txtIDBook_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Wrong character", "Message");
            }
        }
    }
}

