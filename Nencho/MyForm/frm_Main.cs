using System;
using System.Drawing;
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

        private void frm_Main_Load(object sender, EventArgs e)
        {
            gridControl_Division.DataSource = Global.DataNencho.GetDivision();
            if (Global.StrRole == "ADMIN")
            {baritem_Manager.Enabled = true;
                tab_phancong.PageVisible = true;
                tab_admin.PageVisible = true;
                tab_checker1.PageVisible = true;
                tab_checker2.PageVisible = true;
                tab_error.PageVisible = true;
            }
        }

        private void btn_ManagerFiles_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frm_ManagerFile().ShowDialog();
        }

        bool Cal(int width,GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
            return true;
        }
        private void dgv_Division_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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

        void SetColorCell(object sender, RowCellStyleEventArgs e,string fielname, string s)
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

        private void dgv_Division_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            SetColorCell(sender, e, "Cot_M", "0");
            SetColorCell(sender, e, "Cot_N", "0");
            SetColorCell(sender, e, "Cot_S", "1");
            SetColorCell(sender, e, "Cot_T", "1");
            SetColorCell(sender, e, "Cot_U", "1");
        }
    }
}