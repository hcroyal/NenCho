using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace Nencho.MyForm
{
    public partial class frm_Main : XtraForm
    {
        private DataView clone = null;

        public frm_Main()
        {
            InitializeComponent();
        }

        public bool Cal(int width, GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
            return true;
        }

        public void LoadNumericalGridview(object sender, RowIndicatorCustomDrawEventArgs e,GridView dgv)
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
                    BeginInvoke(new MethodInvoker(delegate { Cal(width, dgv); }));
                }
            }
            catch (Exception i)
            {
                MessageBox.Show("Lỗi : " + i);
            }
        }

        private void SetValueText(string truongSo, GridView dgv)
        {
            var values = dgv.GetRowCellValue(dgv.FocusedRowHandle, truongSo) != null ? dgv.GetRowCellValue(dgv.FocusedRowHandle, truongSo).ToString() : "";
            if (values == "1")
                dgv.SetRowCellValue(dgv.FocusedRowHandle, truongSo, "○");
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

                //---Load Tab Division

                gridControl_Division.DataSource = Global.DataNencho.GetDivision();

                //---Load Tab DE
                //Load Data Combobox BatchNO DE
                if (!string.IsNullOrEmpty((Global.StrBatch)))
                {
                    cbb_batchnode.DataSource = from w in Global.DataNencho.tbl_Files where w.SubmitFile_Input.Value == false && w.Cot_AA==Global.StrUsername group w by w.Cot_Z into g select  g.Key;
                    cbb_batchnode.SelectedItem = Global.StrBatch;
                }
                //Load Data Combobox BatchNoComplete
                cbb_batchnode_complete.DataSource = from w in Global.DataNencho.tbl_Files where w.SubmitFile_Input.Value && w.Cot_AA == Global.StrUsername group w by w.Cot_Z into g select g.Key;

                if (!string.IsNullOrEmpty(cbb_batchnode.Text))
                {
                    btn_submit_de.Enabled = true;
                }
                //Load gridControl DE
                gridControl_de.DataSource = Global.DataNencho.GetDataDE(cbb_batchnode.Text,Global.StrUsername);
                Lookupedit_column36.DataSource = from w in Global.DataNencho.tbl_DataColumn36s select w.dataColumn36;
                Lookupedit_Column37.DataSource = from w in Global.DataNencho.tbl_DataColumn37s select w.dataColumn37;
                Lookupedit_Column38.DataSource = from w in Global.DataNencho.tbl_DataColumn38s select w.dataColumn38;
                
                //---Load Tab Checker
                //Load Data Combobox BatchNo_Checker
                if (!string.IsNullOrEmpty((Global.StrBatch)))
                {
                    cbb_batchnochecker.DataSource = from w in Global.DataNencho.tbl_Files where w.SubmitFile_Check.Value == false && w.Cot_AB == Global.StrUsername && w.SubmitFile_Input.Value group w by w.Cot_Z into g select g.Key;cbb_batchnochecker.SelectedItem = Global.StrBatch;
                }

                //Load Data Combobox BatchNoComplete
                cbb_batchnochecker_complete.DataSource = from w in Global.DataNencho.tbl_Files where w.SubmitFile_Check.Value && w.Cot_AB == Global.StrUsername group w by w.Cot_Z into g select g.Key;

                if (!string.IsNullOrEmpty(cbb_batchnochecker.Text))
                {
                    btn_submit_checker.Enabled = true;
                }
                //Load gridControl Checker
                gridControl_checker.DataSource = Global.DataNencho.GetDataChecker(cbb_batchnochecker.Text, Global.StrUsername);
                LookUpEdit_Column57.DataSource = from w in Global.DataNencho.tbl_DataColumn36s select w.dataColumn36;
                LookUpEdit_Column58.DataSource = from w in Global.DataNencho.tbl_DataColumn37s select w.dataColumn37;
                LookUpEdit_Column59.DataSource = from w in Global.DataNencho.tbl_DataColumn38s select w.dataColumn38;

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
            LoadNumericalGridview(sender,e,dgv_Division);
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
        
        #region Tab_DE

        private void dgv_de_ShownEditor(object sender, EventArgs e)
        {
            ColumnView view = (ColumnView)sender;
            if (view.FocusedColumn.FieldName == "Truong_38")
            {
                LookUpEdit editor = (LookUpEdit)view.ActiveEditor;
                string countryCode = Convert.ToString(view.GetFocusedRowCellValue("Truong_37"));
                editor.Properties.DataSource = from w in Global.DataNencho.tbl_DataColumn38s where w.dataColumn37 == countryCode select w.dataColumn38;
            }
        }

        private void Lookupedit_Column38_EditValueChanged(object sender, EventArgs e)
        {
            dgv_de.PostEditor();
            var valueTruong38 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_38") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_38").ToString() : "";
            dgv_de.SetRowCellValue(dgv_de.FocusedRowHandle, "Truong_43", !string.IsNullOrEmpty(valueTruong38) ? "○" : "");
        }

        private void cbb_batchnode_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridControl_de.DataSource = Global.DataNencho.GetDataDE(cbb_batchnode.Text, Global.StrUsername);
            btn_submit_de.Enabled = !string.IsNullOrEmpty(cbb_batchnode.Text); 
            dgv_de.OptionsBehavior.ReadOnly = false; 
            btn_submit_de.Enabled = true;
        }

        private void dgv_de_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            SetValueText("Truong_26", dgv_de);
            SetValueText("Truong_27", dgv_de);
            SetValueText("Truong_28", dgv_de);
            SetValueText("Truong_29", dgv_de);
            SetValueText("Truong_30", dgv_de);
            SetValueText("Truong_31", dgv_de);
            SetValueText("Truong_32", dgv_de);
            SetValueText("Truong_33", dgv_de);
            SetValueText("Truong_34", dgv_de);
            SetValueText("Truong_35", dgv_de); 
            string truong26, truong27, truong28, truong29, truong30, truong31, truong32, truong33, truong34, truong35, truong36, truong37, truong38, truong39, truong40, truong41, truong42, truong43,  Idfile, cot_g;
            truong26 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_26") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_26").ToString() : "";
            truong27 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_27") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_27").ToString() : "";
            truong28 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_28") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_28").ToString() : "";
            truong29 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_29") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_29").ToString() : "";
            truong30 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_30") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_30").ToString() : "";
            truong31 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_31") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_31").ToString() : "";
            truong32 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_32") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_32").ToString() : "";
            truong33 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_33") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_33").ToString() : "";
            truong34 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_34") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_34").ToString() : "";
            truong35 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_35") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_35").ToString() : "";
            truong36 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_36") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_36").ToString() : "";
            truong37 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_37") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_37").ToString() : "";
            truong38 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_38") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_38").ToString() : "";
            truong39 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_39") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_39").ToString() : "";
            truong40 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_40") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_40").ToString() : "";
            truong41 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_41") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_41").ToString() : "";
            truong42 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_42") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_42").ToString() : "";
            truong43 = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_43") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Truong_43").ToString() : "";
            Idfile = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "IDFile") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "IDFile").ToString() : "";
            cot_g = dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Cot_G") != null ? dgv_de.GetRowCellValue(dgv_de.FocusedRowHandle, "Cot_G").ToString() : "";

            Global.DataNencho.Update_TableInput(Idfile,cot_g, cbb_batchnode.Text,  truong26, truong27, truong28, truong29, truong30, truong31, truong32, truong33, truong34, truong35, truong36, truong37, truong38, truong39, truong40, truong41, truong42, truong43);
        }

        private void dgv_de_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            LoadNumericalGridview(sender, e, dgv_de);
        }

        private void btn_submit_de_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult thongbao = MessageBox.Show("Bạn chắc chắn đã hoàn thành Batch " + cbb_batchnode.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                int countRowNull = 0; string s = null;
                if (thongbao == DialogResult.Yes)
                {
                    for (int i = 0; i < dgv_de.RowCount; i++)
                    {
                        if (string.IsNullOrEmpty(dgv_de.GetRowCellValue(i, "Truong_26") != null ? dgv_de.GetRowCellValue(i, "Truong_26").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_de.GetRowCellValue(i, "Truong_27") != null ? dgv_de.GetRowCellValue(i, "Truong_27").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_de.GetRowCellValue(i, "Truong_28") != null ? dgv_de.GetRowCellValue(i, "Truong_28").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_de.GetRowCellValue(i, "Truong_29") != null ? dgv_de.GetRowCellValue(i, "Truong_29").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_de.GetRowCellValue(i, "Truong_30") != null ? dgv_de.GetRowCellValue(i, "Truong_30").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_de.GetRowCellValue(i, "Truong_31") != null ? dgv_de.GetRowCellValue(i, "Truong_31").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_de.GetRowCellValue(i, "Truong_32") != null ? dgv_de.GetRowCellValue(i, "Truong_32").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_de.GetRowCellValue(i, "Truong_33") != null ? dgv_de.GetRowCellValue(i, "Truong_33").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_de.GetRowCellValue(i, "Truong_34") != null ? dgv_de.GetRowCellValue(i, "Truong_34").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_de.GetRowCellValue(i, "Truong_35") != null ? dgv_de.GetRowCellValue(i, "Truong_35").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_de.GetRowCellValue(i, "Truong_36") != null ? dgv_de.GetRowCellValue(i, "Truong_36").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_de.GetRowCellValue(i, "Truong_37") != null ? dgv_de.GetRowCellValue(i, "Truong_37").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_de.GetRowCellValue(i, "Truong_38") != null ? dgv_de.GetRowCellValue(i, "Truong_38").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_de.GetRowCellValue(i, "Truong_39") != null ? dgv_de.GetRowCellValue(i, "Truong_39").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_de.GetRowCellValue(i, "Truong_40") != null ? dgv_de.GetRowCellValue(i, "Truong_40").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_de.GetRowCellValue(i, "Truong_41") != null ? dgv_de.GetRowCellValue(i, "Truong_41").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_de.GetRowCellValue(i, "Truong_42") != null ? dgv_de.GetRowCellValue(i, "Truong_42").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_de.GetRowCellValue(i, "Truong_43") != null ? dgv_de.GetRowCellValue(i, "Truong_43").ToString() : ""))
                        {
                            countRowNull += 1;
                            s += (i + 1) + "\n";
                        }
                    }
                    if (countRowNull == 0)
                    {
                        for (int i = 0; i < dgv_de.RowCount; i++)
                        {
                            string truong26, truong27, truong28, truong29, truong30, truong31, truong32, truong33, truong34, truong35, truong36, truong37, truong38, truong39, truong40, truong41, truong42, truong43, cot_g;
                            truong26 = dgv_de.GetRowCellValue(i, "Truong_26") != null ? dgv_de.GetRowCellValue(i, "Truong_26").ToString() : "";
                            truong27 = dgv_de.GetRowCellValue(i, "Truong_27") != null ? dgv_de.GetRowCellValue(i, "Truong_27").ToString() : "";
                            truong28 = dgv_de.GetRowCellValue(i, "Truong_28") != null ? dgv_de.GetRowCellValue(i, "Truong_28").ToString() : "";
                            truong29 = dgv_de.GetRowCellValue(i, "Truong_29") != null ? dgv_de.GetRowCellValue(i, "Truong_29").ToString() : "";
                            truong30 = dgv_de.GetRowCellValue(i, "Truong_30") != null ? dgv_de.GetRowCellValue(i, "Truong_30").ToString() : "";
                            truong31 = dgv_de.GetRowCellValue(i, "Truong_31") != null ? dgv_de.GetRowCellValue(i, "Truong_31").ToString() : "";
                            truong32 = dgv_de.GetRowCellValue(i, "Truong_32") != null ? dgv_de.GetRowCellValue(i, "Truong_32").ToString() : "";
                            truong33 = dgv_de.GetRowCellValue(i, "Truong_33") != null ? dgv_de.GetRowCellValue(i, "Truong_33").ToString() : "";
                            truong34 = dgv_de.GetRowCellValue(i, "Truong_34") != null ? dgv_de.GetRowCellValue(i, "Truong_34").ToString() : "";
                            truong35 = dgv_de.GetRowCellValue(i, "Truong_35") != null ? dgv_de.GetRowCellValue(i, "Truong_35").ToString() : "";
                            truong36 = dgv_de.GetRowCellValue(i, "Truong_36") != null ? dgv_de.GetRowCellValue(i, "Truong_36").ToString() : "";
                            truong37 = dgv_de.GetRowCellValue(i, "Truong_37") != null ? dgv_de.GetRowCellValue(i, "Truong_37").ToString() : "";
                            truong38 = dgv_de.GetRowCellValue(i, "Truong_38") != null ? dgv_de.GetRowCellValue(i, "Truong_38").ToString() : "";
                            truong43 = dgv_de.GetRowCellValue(i, "Truong_43") != null ? dgv_de.GetRowCellValue(i, "Truong_43").ToString() : "";
                            cot_g = dgv_de.GetRowCellValue(i, "Cot_G") != null ? dgv_de.GetRowCellValue(i, "Cot_G").ToString() : "";
                            Global.DataNencho.Update_TableFile_Checker1(cot_g, cbb_batchnode.Text);
                            Global.DataNencho.InsertTableChecker(cot_g, cbb_batchnode.Text, truong26, truong27, truong28, truong29, truong30, truong31, truong32, truong33, truong34, truong35, truong36, truong37, truong38, truong43);
                        }
                        cbb_batchnode_complete.DataSource = from w in Global.DataNencho.tbl_Files where w.SubmitFile_Input.Value && w.Cot_AA == Global.StrUsername group w by w.Cot_Z into g select g.Key;
                        cbb_batchnode.Text = "";
                        cbb_batchnode.DataSource = from w in Global.DataNencho.tbl_Files where w.SubmitFile_Input.Value == false && w.Cot_AA == Global.StrUsername group w by w.Cot_Z into g select g.Key;
                    }
                    else
                    {
                        DialogResult notifyRowNull = MessageBox.Show("Còn " + countRowNull + " dòng: \n" + s + "chưa có dữ liệu, Bạn vẫn muốn Submit", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (notifyRowNull != DialogResult.Yes) return;
                        for (int i = 0; i < dgv_de.RowCount; i++)
                        {
                            string truong26, truong27, truong28, truong29, truong30, truong31, truong32, truong33, truong34, truong35, truong36, truong37, truong38, truong39, truong40, truong41, truong42, truong43, cot_g;
                            truong26 = dgv_de.GetRowCellValue(i, "Truong_26") != null ? dgv_de.GetRowCellValue(i, "Truong_26").ToString() : "";
                            truong27 = dgv_de.GetRowCellValue(i, "Truong_27") != null ? dgv_de.GetRowCellValue(i, "Truong_27").ToString() : "";
                            truong28 = dgv_de.GetRowCellValue(i, "Truong_28") != null ? dgv_de.GetRowCellValue(i, "Truong_28").ToString() : "";
                            truong29 = dgv_de.GetRowCellValue(i, "Truong_29") != null ? dgv_de.GetRowCellValue(i, "Truong_29").ToString() : "";
                            truong30 = dgv_de.GetRowCellValue(i, "Truong_30") != null ? dgv_de.GetRowCellValue(i, "Truong_30").ToString() : "";
                            truong31 = dgv_de.GetRowCellValue(i, "Truong_31") != null ? dgv_de.GetRowCellValue(i, "Truong_31").ToString() : "";
                            truong32 = dgv_de.GetRowCellValue(i, "Truong_32") != null ? dgv_de.GetRowCellValue(i, "Truong_32").ToString() : "";
                            truong33 = dgv_de.GetRowCellValue(i, "Truong_33") != null ? dgv_de.GetRowCellValue(i, "Truong_33").ToString() : "";
                            truong34 = dgv_de.GetRowCellValue(i, "Truong_34") != null ? dgv_de.GetRowCellValue(i, "Truong_34").ToString() : "";
                            truong35 = dgv_de.GetRowCellValue(i, "Truong_35") != null ? dgv_de.GetRowCellValue(i, "Truong_35").ToString() : "";
                            truong36 = dgv_de.GetRowCellValue(i, "Truong_36") != null ? dgv_de.GetRowCellValue(i, "Truong_36").ToString() : "";
                            truong37 = dgv_de.GetRowCellValue(i, "Truong_37") != null ? dgv_de.GetRowCellValue(i, "Truong_37").ToString() : "";
                            truong38 = dgv_de.GetRowCellValue(i, "Truong_38") != null ? dgv_de.GetRowCellValue(i, "Truong_38").ToString() : "";
                            truong43 = dgv_de.GetRowCellValue(i, "Truong_43") != null ? dgv_de.GetRowCellValue(i, "Truong_43").ToString() : "";
                            cot_g = dgv_de.GetRowCellValue(i, "Cot_G") != null ? dgv_de.GetRowCellValue(i, "Cot_G").ToString() : "";

                            Global.DataNencho.Update_TableFile_Checker1(cot_g, cbb_batchnode.Text);
                            Global.DataNencho.InsertTableChecker(cot_g, cbb_batchnode.Text, truong26, truong27, truong28, truong29, truong30, truong31, truong32, truong33, truong34, truong35, truong36, truong37, truong38, truong43);
                        }
                        cbb_batchnode_complete.DataSource = from w in Global.DataNencho.tbl_Files where w.SubmitFile_Input.Value && w.Cot_AA == Global.StrUsername group w by w.Cot_Z into g select g.Key;
                        cbb_batchnode.Text = "";
                        cbb_batchnode.DataSource = from w in Global.DataNencho.tbl_Files where w.SubmitFile_Input.Value == false && w.Cot_AA == Global.StrUsername group w by w.Cot_Z into g select g.Key;
                    }
                }
            }
            catch (Exception i)
            {
                MessageBox.Show("Lỗi : " + i);
            }
        }

        private void cbb_batchnode_complete_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridControl_de.DataSource = Global.DataNencho.GetDataDEComplete(cbb_batchnode_complete.Text, Global.StrUsername);
            dgv_de.OptionsBehavior.ReadOnly = true;
            btn_submit_de.Enabled = false;
        }

        #endregion

        #region Tab_Checker

        private void dgv_checker_ShownEditor(object sender, EventArgs e)
        {
            ColumnView view = (ColumnView)sender;
            if (view.FocusedColumn.FieldName == "Truong_38")
            {
                LookUpEdit editor = (LookUpEdit)view.ActiveEditor;
                string countryCode = Convert.ToString(view.GetFocusedRowCellValue("Truong_37"));
                editor.Properties.DataSource = from w in Global.DataNencho.tbl_DataColumn38s where w.dataColumn37 == countryCode select w.dataColumn38;
            }
        }

        private void LookUpEdit_Column59_EditValueChanged(object sender, EventArgs e)
        {
            dgv_checker.PostEditor();
            var valueTruong38 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_38") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_38").ToString() : "";
            dgv_checker.SetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_43", !string.IsNullOrEmpty(valueTruong38) ? "○" : "");
        }

        private void cbb_batchnochecker_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridControl_checker.DataSource = Global.DataNencho.GetDataChecker(cbb_batchnochecker.Text, Global.StrUsername);
            btn_submit_checker.Enabled = !string.IsNullOrEmpty(cbb_batchnochecker.Text);
            dgv_checker.OptionsBehavior.ReadOnly = false;
            btn_submit_checker.Enabled = true;
        }

        private void dgv_checker_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            SetValueText("Truong_26", dgv_checker);
            SetValueText("Truong_27", dgv_checker);
            SetValueText("Truong_28", dgv_checker);
            SetValueText("Truong_29", dgv_checker);
            SetValueText("Truong_30", dgv_checker);
            SetValueText("Truong_31", dgv_checker);
            SetValueText("Truong_32", dgv_checker);
            SetValueText("Truong_33", dgv_checker);
            SetValueText("Truong_34", dgv_checker);
            SetValueText("Truong_35", dgv_checker);
           
            string truong26, truong27, truong28, truong29, truong30, truong31, truong32, truong33, truong34, truong35, truong36, truong37, truong38, truong39, truong40, truong41, truong42, truong43, cot_g;
            truong26 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_26") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_26").ToString() : "";
            truong27 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_27") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_27").ToString() : "";
            truong28 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_28") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_28").ToString() : "";
            truong29 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_29") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_29").ToString() : "";
            truong30 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_30") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_30").ToString() : "";
            truong31 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_31") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_31").ToString() : "";
            truong32 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_32") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_32").ToString() : "";
            truong33 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_33") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_33").ToString() : "";
            truong34 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_34") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_34").ToString() : "";
            truong35 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_35") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_35").ToString() : "";
            truong36 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_36") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_36").ToString() : "";
            truong37 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_37") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_37").ToString() : "";
            truong38 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_38") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_38").ToString() : "";
            truong39 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_39") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_39").ToString() : "";
            truong40 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_40") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_40").ToString() : "";
            truong41 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_41") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_41").ToString() : "";
            truong42 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_42") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_42").ToString() : "";
            truong43 = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_43") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Truong_43").ToString() : "";
            cot_g = dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Cot_G") != null ? dgv_checker.GetRowCellValue(dgv_checker.FocusedRowHandle, "Cot_G").ToString() : "";

            Global.DataNencho.Update_TableChecker(Global.StrUsername, cot_g, cbb_batchnochecker.Text, truong26, truong27, truong28, truong29, truong30, truong31, truong32, truong33, truong34, truong35, truong36, truong37, truong38, truong39, truong40, truong41, truong42, truong43);
        }

        private void dgv_checker_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            LoadNumericalGridview(sender, e, dgv_checker);}

        private void btn_submit_checker_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult thongbao = MessageBox.Show("Bạn chắc chắn đã hoàn thành Batch " + cbb_batchnode.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                int countRowNull = 0; string s = null;
                if (thongbao == DialogResult.Yes)
                {
                    for (int i = 0; i < dgv_checker.RowCount; i++)
                    {
                        if (string.IsNullOrEmpty(dgv_checker.GetRowCellValue(i, "Truong_26") != null ? dgv_checker.GetRowCellValue(i, "Truong_26").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_checker.GetRowCellValue(i, "Truong_27") != null ? dgv_checker.GetRowCellValue(i, "Truong_27").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_checker.GetRowCellValue(i, "Truong_28") != null ? dgv_checker.GetRowCellValue(i, "Truong_28").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_checker.GetRowCellValue(i, "Truong_29") != null ? dgv_checker.GetRowCellValue(i, "Truong_29").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_checker.GetRowCellValue(i, "Truong_30") != null ? dgv_checker.GetRowCellValue(i, "Truong_30").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_checker.GetRowCellValue(i, "Truong_31") != null ? dgv_checker.GetRowCellValue(i, "Truong_31").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_checker.GetRowCellValue(i, "Truong_32") != null ? dgv_checker.GetRowCellValue(i, "Truong_32").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_checker.GetRowCellValue(i, "Truong_33") != null ? dgv_checker.GetRowCellValue(i, "Truong_33").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_checker.GetRowCellValue(i, "Truong_34") != null ? dgv_checker.GetRowCellValue(i, "Truong_34").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_checker.GetRowCellValue(i, "Truong_35") != null ? dgv_checker.GetRowCellValue(i, "Truong_35").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_checker.GetRowCellValue(i, "Truong_36") != null ? dgv_checker.GetRowCellValue(i, "Truong_36").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_checker.GetRowCellValue(i, "Truong_37") != null ? dgv_checker.GetRowCellValue(i, "Truong_37").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_checker.GetRowCellValue(i, "Truong_38") != null ? dgv_checker.GetRowCellValue(i, "Truong_38").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_checker.GetRowCellValue(i, "Truong_39") != null ? dgv_checker.GetRowCellValue(i, "Truong_39").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_checker.GetRowCellValue(i, "Truong_40") != null ? dgv_checker.GetRowCellValue(i, "Truong_40").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_checker.GetRowCellValue(i, "Truong_41") != null ? dgv_checker.GetRowCellValue(i, "Truong_41").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_checker.GetRowCellValue(i, "Truong_42") != null ? dgv_checker.GetRowCellValue(i, "Truong_42").ToString() : "") &&
                            string.IsNullOrEmpty(dgv_checker.GetRowCellValue(i, "Truong_43") != null ? dgv_checker.GetRowCellValue(i, "Truong_43").ToString() : ""))
                        {
                            countRowNull += 1;
                            s += (i + 1) + "\n";
                        }
                    }
                    if (countRowNull == 0)
                    {
                        for (int i = 0; i < dgv_checker.RowCount; i++)
                        {
                            string cot_g;
                            cot_g = dgv_checker.GetRowCellValue(i, "Cot_G") != null ? dgv_checker.GetRowCellValue(i, "Cot_G").ToString() : "";
                            Global.DataNencho.Update_TableFile_Checker2(cot_g, cbb_batchnochecker.Text);
                        }
                        cbb_batchnochecker_complete.DataSource = from w in Global.DataNencho.tbl_Files where w.SubmitFile_Check.Value && w.Cot_AB == Global.StrUsername group w by w.Cot_Z into g select g.Key;
                        cbb_batchnochecker.Text = "";
                        cbb_batchnochecker.DataSource = from w in Global.DataNencho.tbl_Files where w.SubmitFile_Input.Value && w.SubmitFile_Check.Value == false && w.Cot_AB == Global.StrUsername group w by w.Cot_Z into g select g.Key;
                    }
                    else
                    {
                        DialogResult notifyRowNull = MessageBox.Show("Còn " + countRowNull + " dòng: \n" + s + "chưa có dữ liệu, Bạn vẫn muốn Submit", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (notifyRowNull != DialogResult.Yes) return;
                        for (int i = 0; i < dgv_checker.RowCount; i++)
                        {
                            string  cot_g;
                            cot_g = dgv_checker.GetRowCellValue(i, "Cot_G") != null ? dgv_checker.GetRowCellValue(i, "Cot_G").ToString() : "";
                            Global.DataNencho.Update_TableFile_Checker2(cot_g, cbb_batchnochecker.Text);
                        }
                        cbb_batchnochecker_complete.DataSource = from w in Global.DataNencho.tbl_Files where w.SubmitFile_Check.Value && w.Cot_AB == Global.StrUsername group w by w.Cot_Z into g select g.Key;
                        cbb_batchnochecker.Text = "";
                        cbb_batchnochecker.DataSource = from w in Global.DataNencho.tbl_Files where w.SubmitFile_Input.Value && w.SubmitFile_Check.Value == false && w.Cot_AB == Global.StrUsername group w by w.Cot_Z into g select g.Key;
                    }
                }
            }
            catch (Exception i)
            {
                MessageBox.Show("Lỗi : " + i);
            }
        }

        private void cbb_batchnochecker_complete_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridControl_checker.DataSource = Global.DataNencho.GetDataCheckerComplete(cbb_batchnochecker_complete.Text, Global.StrUsername);
            dgv_checker.OptionsBehavior.ReadOnly = true;btn_submit_checker.Enabled = false;
        }

        #endregion

        #region TabAdmin



        #endregion

        #region TabError



        #endregion


    }
}