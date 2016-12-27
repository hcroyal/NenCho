namespace Nencho.MyForm
{
    partial class frm_CreateFile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_CreateFile));
            this.btn_HuyBo = new DevExpress.XtraEditors.SimpleButton();
            this.btn_TaoBatch = new DevExpress.XtraEditors.SimpleButton();
            this.cbb_ChonSheet = new DevExpress.XtraEditors.LookUpEdit();
            this.btn_ChonFile = new DevExpress.XtraEditors.SimpleButton();
            this.txt_FileExcel = new DevExpress.XtraEditors.TextEdit();
            this.txt_NgayTao = new DevExpress.XtraEditors.TextEdit();
            this.txt_UserName = new DevExpress.XtraEditors.TextEdit();
            this.txt_fBatchName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.cbb_ChonSheet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_FileExcel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_NgayTao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_UserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_fBatchName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_HuyBo
            // 
            this.btn_HuyBo.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btn_HuyBo.Appearance.Font")));
            this.btn_HuyBo.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.btn_HuyBo, "btn_HuyBo");
            this.btn_HuyBo.Name = "btn_HuyBo";
            this.btn_HuyBo.Click += new System.EventHandler(this.btn_HuyBo_Click);
            // 
            // btn_TaoBatch
            // 
            this.btn_TaoBatch.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btn_TaoBatch.Appearance.Font")));
            this.btn_TaoBatch.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.btn_TaoBatch, "btn_TaoBatch");
            this.btn_TaoBatch.Name = "btn_TaoBatch";
            this.btn_TaoBatch.Click += new System.EventHandler(this.btn_TaoBatch_Click);
            // 
            // cbb_ChonSheet
            // 
            resources.ApplyResources(this.cbb_ChonSheet, "cbb_ChonSheet");
            this.cbb_ChonSheet.Name = "cbb_ChonSheet";
            this.cbb_ChonSheet.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cbb_ChonSheet.Properties.Buttons"))))});
            this.cbb_ChonSheet.Properties.NullText = resources.GetString("cbb_ChonSheet.Properties.NullText");
            this.cbb_ChonSheet.EditValueChanged += new System.EventHandler(this.cbb_ChonSheet_EditValueChanged);
            // 
            // btn_ChonFile
            // 
            resources.ApplyResources(this.btn_ChonFile, "btn_ChonFile");
            this.btn_ChonFile.Name = "btn_ChonFile";
            this.btn_ChonFile.Click += new System.EventHandler(this.btn_ChonFile_Click);
            // 
            // txt_FileExcel
            // 
            resources.ApplyResources(this.txt_FileExcel, "txt_FileExcel");
            this.txt_FileExcel.Name = "txt_FileExcel";
            this.txt_FileExcel.Properties.ReadOnly = true;
            // 
            // txt_NgayTao
            // 
            resources.ApplyResources(this.txt_NgayTao, "txt_NgayTao");
            this.txt_NgayTao.Name = "txt_NgayTao";
            this.txt_NgayTao.Properties.ReadOnly = true;
            // 
            // txt_UserName
            // 
            resources.ApplyResources(this.txt_UserName, "txt_UserName");
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.Properties.ReadOnly = true;
            // 
            // txt_fBatchName
            // 
            resources.ApplyResources(this.txt_fBatchName, "txt_fBatchName");
            this.txt_fBatchName.Name = "txt_fBatchName";
            this.txt_fBatchName.Properties.ReadOnly = true;
            // 
            // labelControl6
            // 
            resources.ApplyResources(this.labelControl6, "labelControl6");
            this.labelControl6.Name = "labelControl6";
            // 
            // labelControl5
            // 
            resources.ApplyResources(this.labelControl5, "labelControl5");
            this.labelControl5.Name = "labelControl5";
            // 
            // labelControl4
            // 
            resources.ApplyResources(this.labelControl4, "labelControl4");
            this.labelControl4.Name = "labelControl4";
            // 
            // labelControl3
            // 
            resources.ApplyResources(this.labelControl3, "labelControl3");
            this.labelControl3.Name = "labelControl3";
            // 
            // labelControl2
            // 
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Name = "labelControl2";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl1.Appearance.Font")));
            this.labelControl1.Appearance.ForeColor = ((System.Drawing.Color)(resources.GetObject("labelControl1.Appearance.ForeColor")));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // frm_CreateFile
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_HuyBo);
            this.Controls.Add(this.btn_TaoBatch);
            this.Controls.Add(this.cbb_ChonSheet);
            this.Controls.Add(this.btn_ChonFile);
            this.Controls.Add(this.txt_FileExcel);
            this.Controls.Add(this.txt_NgayTao);
            this.Controls.Add(this.txt_UserName);
            this.Controls.Add(this.txt_fBatchName);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "frm_CreateFile";
            this.Load += new System.EventHandler(this.frm_CreateFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbb_ChonSheet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_FileExcel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_NgayTao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_UserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_fBatchName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_HuyBo;
        private DevExpress.XtraEditors.SimpleButton btn_TaoBatch;
        private DevExpress.XtraEditors.LookUpEdit cbb_ChonSheet;
        private DevExpress.XtraEditors.SimpleButton btn_ChonFile;
        private DevExpress.XtraEditors.TextEdit txt_FileExcel;
        private DevExpress.XtraEditors.TextEdit txt_NgayTao;
        private DevExpress.XtraEditors.TextEdit txt_UserName;
        private DevExpress.XtraEditors.TextEdit txt_fBatchName;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}