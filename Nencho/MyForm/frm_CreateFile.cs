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
            txt_UserName.Text = Global.StrUsername;txt_NgayTao.Text = DateTime.Now.ToShortDateString();
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
                    txt_fBatchName.Text = openFileDialog.SafeFileName;
                    _oleDbcon.Open();
                    txt_FileExcel.Text = openFileDialog.FileName;
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
            if (string.IsNullOrEmpty(txt_FileExcel.Text))
            {
                MessageBox.Show("Không được để trống file Excel!");

            }
            else
            {
                var fbatchname = (from w in Global.DataNencho.tbl_Batches where w.fBatchName == txt_fBatchName.Text select w.fBatchName).FirstOrDefault();
                if (!string.IsNullOrEmpty(fbatchname))
                {
                    MessageBox.Show("Đã tồn tại Batch này, vui lòng nhập lại tên batch");
                    return;
                }
                else
                {
                    Global.DataNencho.CreateBatch(txt_fBatchName.Text, txt_UserName.Text,txt_FileExcel.Text);
                    var dbExcel = new OleDbDataAdapter("Select * from [" + cbb_ChonSheet.Text + "]", _oleDbcon);
                    DataTable dt = new DataTable();
                    dbExcel.Fill(dt);
                    int n = 1;
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        if (n>7 && n<dt.Rows.Count)
                        {
                            Global.DataNencho.CreateFile(txt_fBatchName.Text,
                                                    dataRow[1].ToString(),
                                                    dataRow[2].ToString(),
                                                    dataRow[3].ToString(),
                                                    dataRow[4].ToString(),
                                                    dataRow[5].ToString(),
                                                    dataRow[6].ToString(),
                                                    dataRow[7].ToString(),
                                                    dataRow[8].ToString(),
                                                    dataRow[9].ToString(),
                                                    dataRow[10].ToString(),
                                                    dataRow[11].ToString(),
                                                    dataRow[12].ToString(),
                                                    dataRow[13].ToString(),
                                                    dataRow[14].ToString(),
                                                    dataRow[15].ToString(),
                                                    dataRow[16].ToString(),
                                                    dataRow[17].ToString(),
                                                    dataRow[18].ToString(),
                                                    dataRow[29].ToString(),
                                                    dataRow[20].ToString(),
                                                    dataRow[21].ToString(),
                                                    dataRow[22].ToString(),
                                                    dataRow[23].ToString(),
                                                    dataRow[24].ToString(),
                                                    dataRow[25].ToString(),
                                                    dataRow[26].ToString(),
                                                    dataRow[27].ToString(),
                                                    dataRow[28].ToString(),
                                                    dataRow[29].ToString(),
                                                    dataRow[30].ToString());
                        }
                        n++;
                    }
                    if (MessageBox.Show("Đã tạo thành công batch!\nBạn có muốn tiếp tục tạo batch khác?", "Thông báo!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        txt_fBatchName.Text = "";
                        txt_FileExcel.Text = "";
                        cbb_ChonSheet.SelectedText = null;
                    }
                    else
                    {
                        Close();
                    }
                }
            }
        }
    }
}