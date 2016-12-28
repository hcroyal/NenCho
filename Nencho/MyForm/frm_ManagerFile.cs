using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Nencho.MyForm
{
    public partial class frm_ManagerFile : DevExpress.XtraEditors.XtraForm
    {
        public frm_ManagerFile()
        {
            InitializeComponent();
        }
        private void RefreshGrid()
        {
            var dbGrid = from w in Global.DataNencho.tbl_Batches select new { w.fBatchName, w.UserCreate, w.DateCreate, w.UrlFileExcel };
            gridControl1.DataSource = dbGrid;
        }
        private void btn_AddBatch_Click(object sender, EventArgs e)
        {
            new frm_CreateFile().ShowDialog();
            RefreshGrid();

        }

        private void frm_ManagerFile_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void btn_DeleteBatch_Click(object sender, EventArgs e)
        {
            try
            {
                string fbatchname = gridView1.GetFocusedRowCellValue("fBatchName").ToString();
                if (MessageBox.Show("Bạn chắc chắn muốn xóa batch: " + fbatchname + "?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Global.DataNencho.DeleteFile(fbatchname);
                    MessageBox.Show("Đã xóa batch thành công!");
                    RefreshGrid();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không có batch nào để xóa!");
            }
        }
    }
}