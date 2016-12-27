﻿using System;
using System.Collections.Generic;
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
               
                //if (role == "DESO")
                //{
                //    cbb.DataSource = data_LoadBatch.GetBatNotFinish_MissImageDESO(username);
                //    cbb.DisplayMember = "fBatchName";
                //    cbb.ValueMember = "fBatchName";
                //    if (cbb.Items.Count < 1)
                //    {
                //        cbb.DataSource = data_LoadBatch.GetBatNotFinishDeSo();
                //        cbb.DisplayMember = "fBatchName";
                //        cbb.ValueMember = "fBatchName";
                //    }
                //}
                //else if (role == "DEJP")
                //{
                //    cbb.DataSource = data_LoadBatch.GetBatNotFinish_MissImageDEJP(username);
                //    cbb.DisplayMember = "fBatchName";
                //    cbb.ValueMember = "fBatchName";
                //    if (cbb.Items.Count < 1)
                //    {
                //        cbb.DataSource = data_LoadBatch.GetBatNotFinishDeJP();
                //        cbb.DisplayMember = "fBatchName";
                //        cbb.ValueMember = "fBatchName";
                //    }
                //}
                //else
                //{
                //    cbb.DataSource = data_LoadBatch.GetBatch();
                //    cbb.DisplayMember = "fBatchName";
                //    cbb.ValueMember = "fBatchName";
                //}
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi " + e);
            }
        }
    }
}
