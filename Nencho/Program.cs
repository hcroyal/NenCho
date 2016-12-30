using System;
using System.Linq;
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
                a.ButtonLoginEven += a_ButtonLoginEven;
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

        private static void a_ButtonLoginEven(int iLogin, string strMachine, string strUserWindow, string strIpAddress, string strUsername, string password, string strBatch, string strRole, string strToken)
        {
            if (iLogin == 1)
            {
                Global.DataNencho.InsertLoginTime(strUsername, DateTime.Now, strUserWindow, strMachine, strIpAddress, strToken);
                Global.DataNencho.UpdateToken_TableUser(strUsername, strToken);
            }
        }

        private static void a_LoginEvent(string username, string password, ref string strVersion, ref int iKiemtraLogin, ref string role, ref ComboBox cbb)
        {
            try
            {
                iKiemtraLogin = Global.DataNencho.KiemTraLogin(username, password);
                strVersion = (from w in Global.DataNencho.tbl_Versions where w.IdProject == "Nencho" select w.Version).FirstOrDefault();
                role=(from w in Global.DataNencho.GetRoLeUser(username) select w.Column1).FirstOrDefault();
                if (iKiemtraLogin==1 && role == "DE ")
                {
                    cbb.DataSource = Global.DataNencho.GetBatchDE(username);
                    cbb.DisplayMember = "fBatchName"; 
                }
                else if (iKiemtraLogin == 1 && role == "CHECKER ")
                {
                    cbb.DataSource = Global.DataNencho.GetBatchChecker(username);
                    cbb.DisplayMember = "fBatchName"; 
                }
                else if (iKiemtraLogin == 1 && role == "ADMIN")
                {
                    cbb.DataSource = Global.DataNencho.GetBatchAdmin(username);
                    cbb.DisplayMember = "fBatchName"; 
                }
                else if (iKiemtraLogin == 1 && role == "DE CHECKER")
                {
                    cbb.DataSource = Global.DataNencho.GetBatchDE_Cheker(username);
                    cbb.DisplayMember = "fBatchName";
                }
                else if (iKiemtraLogin == 1 && role == "DE ADMIN")
                {
                    cbb.DataSource = Global.DataNencho.GetBatchDE_Admin(username);
                    cbb.DisplayMember = "fBatchName";
                }
                else if (iKiemtraLogin == 1 && role == "CHECKER ADMIN")
                {
                    cbb.DataSource = Global.DataNencho.GetBatchChecker_Admin(username);
                    cbb.DisplayMember = "fBatchName";
                }
                else if (iKiemtraLogin == 1 && role == "DE CHECKER ADMIN")
                {
                    cbb.DataSource = Global.DataNencho.GetBatchDe_Checker_Admin(username);
                    cbb.DisplayMember = "fBatchName";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi " + e);
            }
        }
    }
}
