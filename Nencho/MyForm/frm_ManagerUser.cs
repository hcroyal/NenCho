using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Nencho.MyForm
{
    public partial class frm_ManagerUser : XtraForm
    {
        public frm_ManagerUser()
        {
            InitializeComponent();
        }

        public static string GetMd5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        private void frm_ManagerUser_Load(object sender, EventArgs e)
        {
            dgv_listuser.DataSource = Global.DataNencho.GetListUser();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                string username = gridView1.GetFocusedRowCellValue("UserName") != null ? gridView1.GetFocusedRowCellValue("UserName").ToString() : "";
                DialogResult thongbao = MessageBox.Show("Bạn chắc chắn muốn xóa UserName '" + username + "'", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (thongbao == DialogResult.Yes)
                {
                    if (!string.IsNullOrEmpty(username))
                    {
                        Global.DataNencho.DeleteUsername(username);
                        frm_ManagerUser_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Username không tồn tại, không thể xóa!");
                    }
                }
            }
            catch (Exception i)
            {
                MessageBox.Show("Lỗi: " + i);
            }
        }

        private void gridView1_RowCellDefaultAlignment(object sender, DevExpress.XtraGrid.Views.Base.RowCellAlignmentEventArgs e)
        {
            try
            {
                string Username, Password, nhanvien;

                Username = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "UserName").ToString();
                Password = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Password").ToString();
                nhanvien = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Nhanvien").ToString();

                txt_username.Text = Username;
                txt_password.Text = Password;
                txt_nhanvien.Text = nhanvien;
            }
            catch (Exception i)
            {
                MessageBox.Show("Lỗi: " + i);
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            try
            {
                var token = (from w in Global.DataNencho.GetToken(Global.StrUsername) select w.Token).FirstOrDefault();
                if (token == Global.Strtoken)
                {
                    string pass = GetMd5Hash(txt_password.Text);

                    if (!string.IsNullOrEmpty(txt_nhanvien.Text) && !string.IsNullOrEmpty(txt_username.Text) && !string.IsNullOrEmpty(pass))
                    {
                        int r = Global.DataNencho.InsertUser(txt_username.Text, pass, null, txt_nhanvien.Text);
                        if (r == 0)
                        {
                            MessageBox.Show("UserName đã tồn tại, Vui lòng nhập UserName khác !");
                        }
                        if (r == 1)
                        {
                            MessageBox.Show("Đã thêm UserName '" + txt_username.Text + "' !");
                            frm_ManagerUser_Load(sender, e);
                            txt_username.Text = "";
                            txt_nhanvien.Text = "";
                            txt_password.Text = "";
                            txt_username.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nhập đầy đủ thông tin trước khi lưu !");
                    }
                }
                else
                {
                    MessageBox.Show("User của bạn đang được đăng nhập trên PC khác, vui lòng đăng nhập lại và thực hiện lại giao dich!");
                }
            }
            catch (Exception i)
            {
                MessageBox.Show("Lỗi: " + i);
            }}

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                var token = (from w in Global.DataNencho.GetToken(Global.StrUsername) select w.Token).FirstOrDefault();
                if (token == Global.Strtoken)
                {
                    string pass = GetMd5Hash(txt_password.Text);

                    if (!string.IsNullOrEmpty(txt_nhanvien.Text) && !string.IsNullOrEmpty(txt_username.Text) &&
                        !string.IsNullOrEmpty(pass))
                    {
                        DialogResult thongbao =
                            MessageBox.Show("Bạn chắc chắn muốn sửa thông tin UserName '" + txt_username.Text + "'",
                                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (thongbao == DialogResult.Yes)
                        {
                            Global.DataNencho.UpdateUser(txt_username.Text, pass, null, txt_nhanvien.Text);
                            frm_ManagerUser_Load(sender, e);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nhập đầy đủ thông tin trước khi lưu !");
                    }
                }
                else
                {
                    MessageBox.Show(
                        "User của bạn đang được đăng nhập trên PC khác, vui lòng đăng nhập lại và thực hiện lại giao dich!");
                }
            }
            catch (Exception i)
            {
                MessageBox.Show("Lỗi: " + i);
            }
        }}
}