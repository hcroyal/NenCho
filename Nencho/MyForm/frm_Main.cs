using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace Nencho.MyForm
{
    public partial class frm_Main : XtraForm
    {
        public frm_Main()
        {
            InitializeComponent();
        }

        public bool Cal(int width, GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
            return true;
        }

        public void LoadNumericalGridview(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                if (e.Info.IsRowIndicator)
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1;
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    }
                    SizeF size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 width = Convert.ToInt32(size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { Cal(width, dgv_Division); }));
                }
            }
            catch (Exception i)
            {
                MessageBox.Show("Lỗi : " + i);
            }
        }

        void SetColorCell(object sender, RowCellStyleEventArgs e, string fielname, string s)
        {
            try
            {
                GridView view = sender as GridView;
                string cot = view.GetRowCellDisplayText(e.RowHandle, view.Columns[fielname]);
                if (e.Column.FieldName == fielname)
                {
                    if (cot == s)
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception i)
            {
                MessageBox.Show("Lỗi : " + i);
            }
        }

        private void frm_Main_Load(object sender, EventArgs e)
        {
            try
            {
                if (Global.StrRole == "ADMIN")
                {
                    baritem_Manager.Enabled = true;
                    tab_phancong.PageVisible = true;
                    tab_admin.PageVisible = true;
                    tab_de.PageVisible = true;
                    tab_checker.PageVisible = true;
                    tab_error.PageVisible = true;
                }

                //Load Tab Division

                gridControl_Division.DataSource = Global.DataNencho.GetDivision();

                //---Load Tab DE

                //Load label BatchName and Combobox BatchNO
                lb_fbatchname_de.Text = Global.StrBatch;
                if (!string.IsNullOrEmpty((Global.StrBatch)))
                {
                    cbb_batchnode.DataSource = from w in Global.DataNencho.tbl_Files where w.SubmitFile_Input.Value == false && w.fBatchName == Global.StrBatch group w by w.Cot_Z into g select g.Key;
                }
                gridControl_de.DataSource = Global.DataNencho.tbl_Inputs;
                //Load gridControl DE
                Lookupedit_column36.DataSource = from w in Global.DataNencho.tbl_DataColumn36s select w.dataColumn36;
                Lookupedit_Column37.DataSource = from w in Global.DataNencho.tbl_DataColumn37s select w.dataColumn37;
                string s = dgv_de.GetRowCellValue(dgv_Division.FocusedRowHandle, "gridColumn36") != null ? dgv_de.GetRowCellValue(dgv_Division.FocusedRowHandle, "gridColumn36").ToString() : "";
                if (!string.IsNullOrEmpty(s))
                {
                    Lookupedit_Column38.DataSource = from w in Global.DataNencho.tbl_DataColumn38s where w.dataColumn37 == s select w.dataColumn38;
                }
                else
                {
                    Lookupedit_Column38.DataSource = from w in Global.DataNencho.tbl_DataColumn38s select w.dataColumn38;
                }

                //---Load Tab Checker

                //---Load Tad Admin

                //---Load Tab Error

            }
            catch (Exception i)
            {
                MessageBox.Show("Lỗi : " + i);
            }
        }

        private void btn_logout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult=DialogResult.Yes;
        }

        private void btn_exit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void btn_quanlyuser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm_ManagerUser fuser=new frm_ManagerUser();
            fuser.ShowDialog();
        }

        private void btn_ManagerFiles_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frm_ManagerFile().ShowDialog();
        }

        #region Tabdivision

        private void dgv_Division_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            //Load numerical order of gridControl
            LoadNumericalGridview(sender,e);
        }

        private void dgv_Division_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                //Load Color cell gridControl
                SetColorCell(sender, e, "Cot_M", "0");
                SetColorCell(sender, e, "Cot_N", "0");
                SetColorCell(sender, e, "Cot_S", "1");
                SetColorCell(sender, e, "Cot_T", "1");
                SetColorCell(sender, e, "Cot_U", "1");
            }
            catch (Exception i)
            {
                MessageBox.Show("Lỗi : " + i);
            }
        }

        #endregion
        private void Lookupedit_Column37_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit edit = sender as LookUpEdit;
            var row = edit.Properties.GetDataSourceRowByKeyValue(edit.EditValue);
            
            //var s = dgv_de.GetRowCellValue(dgv_Division.FocusedRowHandle, "Truong_37") != null ? dgv_de.GetRowCellValue(dgv_Division.FocusedRowHandle, "Truong_37").ToString() : "";
            if (!string.IsNullOrEmpty(row.ToString()))
            {
                Lookupedit_Column38.DataSource = from w in Global.DataNencho.tbl_DataColumn38s where w.dataColumn37 == row.ToString() select w.dataColumn38;
            }
        }
        #region TabChecker1

        #endregion

        #region TabChecker2

        

        #endregion

        #region TabAdmin

        

        #endregion

        #region TabError

        

        #endregion
    }
}