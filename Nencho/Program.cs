using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryLogin;
using Nencho.MyForm;

namespace Nencho
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frm_Main());
            bool temp;
            do
            {
                temp = false;
                Frm_Login a = new Frm_Login();
                a.lb_programName.Text = "          Chương trình\n        Nencho";
                a.lb_version.Text = @"1.0";
                a.UrlUpdateVersion = @"\\10.10.10.254\DE_Viet\2016\PHIẾU KIỂM ĐỊNH\Tool";
                a.LoginEvent += a_LoginEvent;
                a.ButtonLoginEven+=a_ButtonLoginEven;
                if (a.ShowDialog() == DialogResult.OK)
                {
                    Global.StrMachine = a.StrMachine;
                    Global.StrUserWindow = a.StrUserWindow;
                    Global.StrIpAddress = a.StrIpAddress;
                    Global.StrUsername = a.StrUserName;
                    Global.StrBatch = a.StrBatch;
                    Global.StrRole = a.StrRole;
                    Global.Strtoken = a.Token;
                    frm_Main frMain = new frm_Main();

                    if (frMain.ShowDialog() == DialogResult.Yes)
                    {
                        frMain.Close();
                        temp = true;
                    }
                }
            }
            while (temp);
        }

        private static void a_ButtonLoginEven(int iLogin, string StrMachine, string StrUserWindow, string StrIpAddress, string Strusername, string Password, string Strbatch, string Strrole, string Strtoken)
        {
            if (iLogin == 1)
            {
                Global.DataNencho.InsertLoginTime(Strusername, DateTime.Now, StrUserWindow, StrMachine, StrIpAddress, Strtoken);
                Global.DataNencho.UpdateToken_TableUser(Strusername, Strtoken);
            }
        }

        private static void a_LoginEvent(string Username, string Password, ref string StrVersion, ref int iKiemtraLogin, ref string Role, ref ComboBox cbb)
        {
            try
            {
                iKiemtraLogin = Global.DataNencho.KiemTraLogin(Username, Password);
                StrVersion = (from w in Global.DataNencho.tbl_Versions where w.IdProject == "Nencho" select w.Version).FirstOrDefault();
                Role=(from w in Global.DataNencho.GetRoLe(Username) select w.Column1).FirstOrDefault();
                if (iKiemtraLogin==1 && Role == "DE")
                {
                    cbb.DataSource = Global.DataNencho.GetBatchDE(Username);
                    cbb.DisplayMember = "Cot_Z"; 
                }
                else if (iKiemtraLogin==1 && Role =="Checker")
                {
                    cbb.DataSource = Global.DataNencho.GetBatchChecker(Username);
                    cbb.DisplayMember = "Cot_Z"; 
                }
                else if (iKiemtraLogin == 1 && Role == "Admin")
                {
                    cbb.DataSource = Global.DataNencho.GetBatchAdmin(Username);
                    cbb.DisplayMember = "Cot_Z"; 
                }
                else if (iKiemtraLogin == 1 && Role == "DE, Checker")
                {
                    cbb.DataSource = Global.DataNencho.GetBatchAdmin(Username);
                    cbb.DisplayMember = "Cot_Z";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi " + e);
            }
        }
    }
}
