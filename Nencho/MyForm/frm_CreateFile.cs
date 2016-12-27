using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Nencho.MyForm
{
    public partial class frm_CreateFile : DevExpress.XtraEditors.XtraForm
    {
        private OleDbConnection _oleDbcon;
        public frm_CreateFile()
        {
            InitializeComponent();
        }

        private void frm_CreateFile_Load(object sender, EventArgs e)
        {

            labelControl6.Visible = false;
            cbb_ChonSheet.Visible = false;
            txt_UserName.Text = Global.StrUsername;
            txt_NgayTao.Text = DateTime.Now.ToShortDateString();
        }

        private void cbb_ChonSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_FileExcel.Text))
            {
                labelControl6.Visible = true;
                cbb_ChonSheet.Visible = true;
            }
            else
            {
                labelControl6.Visible = false;
                cbb_ChonSheet.Visible = false;
            }
        }

        private void btn_HuyBo_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void btn_ChonFile_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = @"Excel Files|*.xls;*.xlsx";

                openFileDialog.ShowDialog();
                if (!string.IsNullOrEmpty(openFileDialog.FileName))

                {
                    _oleDbcon = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + openFileDialog.FileName + ";Extended Properties=Excel 12.0;");

                    _oleDbcon.Open();
                    txt_FileExcel.Text = openFileDialog.SafeFileName;
                    DataTable dt = _oleDbcon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    _oleDbcon.Close();

                    cbb_ChonSheet.EditValue = null;
                    List<string> temp = new List<string>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string sheetName = dt.Rows[i]["TABLE_NAME"].ToString();

                        //sheetName = sheetName.Substring(0, sheetName.Length - 1);

                        temp.Add(sheetName);
                    }
                    cbb_ChonSheet.Properties.DataSource = temp;
                    cbb_ChonSheet.ItemIndex = 0;
                }
            }
        }

        private void btn_TaoBatch_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txt_fBatchName.Text))
            //{
            //    MessageBox.Show("Không được để trống tên batch!");

            //}
            //else
            //{
            //    var fbatchname = (from w in db.tbl_Batches where w.fBatchName == txt_fBatchName.Text select w.fBatchName).FirstOrDefault();
            //    if (!string.IsNullOrEmpty(fbatchname))
            //    {
            //        MessageBox.Show("Đã tồn tại Batch này, vui lòng nhập lại tên batch");
            //        return;
            //    }
            //    else
            //    {
            //        db.CreateBatch(txt_fBatchName.Text, txt_UserName.Text);
            //        var dbExcel = new OleDbDataAdapter("Select * from [" + cbb_ChonSheet.Text + "]", _oleDbcon);
            //        DataTable dt = new DataTable();
            //        dbExcel.Fill(dt);
            //        foreach (DataRow dataRow in dt.Rows)
            //        {
            //            db.CreateLink(txt_fBatchName.Text, dataRow[0].ToString(), dataRow[1].ToString(), dataRow[2].ToString(), dataRow[3].ToString());
            //        }
            //        if (MessageBox.Show("Đã tạo thành công batch!\nBạn có muốn tiếp tục tạo batch khác?", "Thông báo!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        {
            //            txt_fBatchName.Text = "";
            //            txt_FileExcel.Text = "";
            //            cbb_ChonSheet.SelectedText = null;
            //        }
            //        else
            //        {
            //            this.Close();
            //        }
            //    }
            //}
        }
    }
}