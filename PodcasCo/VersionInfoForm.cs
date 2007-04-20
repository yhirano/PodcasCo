#region ディレクティブを使用する

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using MiscPocketCompactLibrary.Windows.Forms;

#endregion

namespace PodcasCo
{
    /// <summary>
    /// バージョン情報のフォーム
    /// </summary>
    public class VersionInfoForm : System.Windows.Forms.Form
    {
        private MenuItem okMenuItem;
        private Label applicationNameLabel;
        private Label versionNumberLabel;
        private Label copyrightLabel;

        /// <summary>
        /// アンカーコントロールのリスト
        /// </summary>
        private ArrayList anchorControlList = new ArrayList();

        /// <summary>
        /// フォームのメイン メニュー
        /// </summary>
        private System.Windows.Forms.MainMenu mainMenu;

        public VersionInfoForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.okMenuItem = new System.Windows.Forms.MenuItem();
            this.applicationNameLabel = new System.Windows.Forms.Label();
            this.versionNumberLabel = new System.Windows.Forms.Label();
            this.copyrightLabel = new System.Windows.Forms.Label();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.okMenuItem);
            // 
            // okMenuItem
            // 
            this.okMenuItem.Text = "&OK";
            this.okMenuItem.Click += new System.EventHandler(this.OkMenuItem_Click);
            // 
            // applicationNameLabel
            // 
            this.applicationNameLabel.Location = new System.Drawing.Point(3, 9);
            this.applicationNameLabel.Size = new System.Drawing.Size(234, 20);
            this.applicationNameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // versionNumberLabel
            // 
            this.versionNumberLabel.Location = new System.Drawing.Point(3, 29);
            this.versionNumberLabel.Size = new System.Drawing.Size(234, 20);
            this.versionNumberLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // copyrightLabel
            // 
            this.copyrightLabel.Location = new System.Drawing.Point(3, 49);
            this.copyrightLabel.Size = new System.Drawing.Size(234, 20);
            this.copyrightLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // VersionInfoForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.copyrightLabel);
            this.Controls.Add(this.versionNumberLabel);
            this.Controls.Add(this.applicationNameLabel);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "バージョン情報";
            this.Resize += new System.EventHandler(this.VersionInfoForm_Resize);
            this.Load += new System.EventHandler(this.VersionInfoForm_Load);

        }

        #endregion

        /// <summary>
        /// コントロールにアンカーをセットする
        /// </summary>
        private void SetAnchorControl()
        {
            anchorControlList.Add(new AnchorLayout(applicationNameLabel, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, PodcasCoInfo.FormBaseWidth, PodcasCoInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(versionNumberLabel, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, PodcasCoInfo.FormBaseWidth, PodcasCoInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(copyrightLabel, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, PodcasCoInfo.FormBaseWidth, PodcasCoInfo.FormBaseHight));
        }

        /// <summary>
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する
        /// </summary>
        private void FixWindowSize()
        {
            foreach (AnchorLayout anchorLayout in anchorControlList)
            {
                anchorLayout.LayoutControl();
            }
        }

        private void VersionInfoForm_Load(object sender, EventArgs e)
        {
            SetAnchorControl();
            FixWindowSize();

            applicationNameLabel.Text = PodcasCoInfo.ApplicationName;
            versionNumberLabel.Text = "Version " + PodcasCoInfo.VersionNumber;
            copyrightLabel.Text = PodcasCoInfo.Copyright;
        }

        private void OkMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VersionInfoForm_Resize(object sender, EventArgs e)
        {
            FixWindowSize();
        }
    }
}
