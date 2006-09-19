#region ディレクティブを使用する

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

#endregion

namespace PodcasCo.Stations.RssPodcast
{
    /// <summary>
    /// 番組の詳細情報表示フォーム
    /// </summary>
    public class ChannelPropertyForm : System.Windows.Forms.Form
    {
        private Label titleCaptionLabel;
        private Label descriptionCaptionLabel;
        private Label linkCaptionLabel;
        private Label dateCaptionLabel;
        private Label titleLabel;
        private Label descriptionLabel;
        private Label linkLabel;
        private Label dateLabel;
        private Button accessButton;
        private MenuItem okMenuItem;
        private MainMenu mainMenu;
        private Label authorLabel;
        private Label authorCaptionLabel;
        private Label lengthLabel;
        private Label lengthCaptionLabel;
        private Label typeLabel;
        private Label typeCaptionLabel;

        /// <summary>
        /// チャンネル
        /// </summary>
        private Channel channel;

        public ChannelPropertyForm(Channel channel)
        {
            //
            // Windows フォーム デザイナ サポートに必要です。
            //
            InitializeComponent();

            this.channel = channel;
        }

        /// <summary>
        /// 使用されているリソースに後処理を実行します。
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
            this.titleCaptionLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.descriptionCaptionLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.linkCaptionLabel = new System.Windows.Forms.Label();
            this.linkLabel = new System.Windows.Forms.Label();
            this.dateCaptionLabel = new System.Windows.Forms.Label();
            this.dateLabel = new System.Windows.Forms.Label();
            this.accessButton = new System.Windows.Forms.Button();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.okMenuItem = new System.Windows.Forms.MenuItem();
            this.authorLabel = new System.Windows.Forms.Label();
            this.authorCaptionLabel = new System.Windows.Forms.Label();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.lengthCaptionLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.typeCaptionLabel = new System.Windows.Forms.Label();
            // 
            // titleCaptionLabel
            // 
            this.titleCaptionLabel.Location = new System.Drawing.Point(3, 3);
            this.titleCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.titleCaptionLabel.Text = "番組名";
            // 
            // titleLabel
            // 
            this.titleLabel.Location = new System.Drawing.Point(3, 19);
            this.titleLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // descriptionCaptionLabel
            // 
            this.descriptionCaptionLabel.Location = new System.Drawing.Point(3, 35);
            this.descriptionCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.descriptionCaptionLabel.Text = "詳細";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Location = new System.Drawing.Point(3, 51);
            this.descriptionLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // linkCaptionLabel
            // 
            this.linkCaptionLabel.Location = new System.Drawing.Point(3, 93);
            this.linkCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.linkCaptionLabel.Text = "リンク";
            // 
            // linkLabel
            // 
            this.linkLabel.Location = new System.Drawing.Point(3, 109);
            this.linkLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // dateCaptionLabel
            // 
            this.dateCaptionLabel.Location = new System.Drawing.Point(3, 183);
            this.dateCaptionLabel.Size = new System.Drawing.Size(64, 16);
            this.dateCaptionLabel.Text = "配信時刻";
            // 
            // dateLabel
            // 
            this.dateLabel.Location = new System.Drawing.Point(3, 199);
            this.dateLabel.Size = new System.Drawing.Size(234, 16);
            // 
            // accessButton
            // 
            this.accessButton.Location = new System.Drawing.Point(165, 128);
            this.accessButton.Size = new System.Drawing.Size(72, 20);
            this.accessButton.Text = "&Access";
            this.accessButton.Click += new System.EventHandler(this.AccessButton_Click);
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
            // authorLabel
            // 
            this.authorLabel.Location = new System.Drawing.Point(3, 167);
            this.authorLabel.Size = new System.Drawing.Size(88, 16);
            // 
            // authorCaptionLabel
            // 
            this.authorCaptionLabel.Location = new System.Drawing.Point(3, 151);
            this.authorCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.authorCaptionLabel.Text = "著者";
            // 
            // lengthLabel
            // 
            this.lengthLabel.Location = new System.Drawing.Point(3, 231);
            this.lengthLabel.Size = new System.Drawing.Size(88, 16);
            // 
            // lengthCaptionLabel
            // 
            this.lengthCaptionLabel.Location = new System.Drawing.Point(3, 215);
            this.lengthCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.lengthCaptionLabel.Text = "長さ";
            // 
            // typeLabel
            // 
            this.typeLabel.Location = new System.Drawing.Point(97, 231);
            this.typeLabel.Size = new System.Drawing.Size(140, 16);
            // 
            // typeCaptionLabel
            // 
            this.typeCaptionLabel.Location = new System.Drawing.Point(97, 215);
            this.typeCaptionLabel.Size = new System.Drawing.Size(34, 16);
            this.typeCaptionLabel.Text = "タイプ";
            // 
            // ChannelPropertyForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lengthLabel);
            this.Controls.Add(this.lengthCaptionLabel);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.typeCaptionLabel);
            this.Controls.Add(this.authorLabel);
            this.Controls.Add(this.authorCaptionLabel);
            this.Controls.Add(this.accessButton);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.dateCaptionLabel);
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.linkCaptionLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.descriptionCaptionLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.titleCaptionLabel);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "番組の詳細";
            this.Resize += new System.EventHandler(this.ChannelPropertyForm_Resize);
            this.Load += new System.EventHandler(this.ChannelPropertyForm_Load);

        }
        #endregion

        /// <summary>
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する
        /// </summary>
        private void FixWindowSize()
        {
            // 水平モードの場合
            if (this.Size.Width > this.Size.Height)
            {
                // 横長のウィンドウ
                FixWindowSizeHorizon();
            }
            else
            {
                // 縦長のウィンドウ
                FixWindowSizeVertical();
            }
        }

        /// <summary>
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する（垂直）
        /// </summary>
        private void FixWindowSizeVertical()
        {
            this.titleCaptionLabel.Location = new System.Drawing.Point(3, 3);
            this.titleCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.titleLabel.Location = new System.Drawing.Point(3, 19);
            this.titleLabel.Size = new System.Drawing.Size(234, 16);
            this.descriptionCaptionLabel.Location = new System.Drawing.Point(3, 35);
            this.descriptionCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.descriptionLabel.Location = new System.Drawing.Point(3, 51);
            this.descriptionLabel.Size = new System.Drawing.Size(234, 16);
            this.linkCaptionLabel.Location = new System.Drawing.Point(3, 93);
            this.linkCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.linkLabel.Location = new System.Drawing.Point(3, 109);
            this.linkLabel.Size = new System.Drawing.Size(234, 16);
            this.dateCaptionLabel.Location = new System.Drawing.Point(3, 183);
            this.dateCaptionLabel.Size = new System.Drawing.Size(64, 16);
            this.dateLabel.Location = new System.Drawing.Point(3, 199);
            this.dateLabel.Size = new System.Drawing.Size(234, 16);
            this.accessButton.Location = new System.Drawing.Point(165, 128);
            this.accessButton.Size = new System.Drawing.Size(72, 20);
            this.authorLabel.Location = new System.Drawing.Point(3, 167);
            this.authorLabel.Size = new System.Drawing.Size(88, 16);
            this.authorCaptionLabel.Location = new System.Drawing.Point(3, 151);
            this.authorCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.lengthLabel.Location = new System.Drawing.Point(3, 231);
            this.lengthLabel.Size = new System.Drawing.Size(88, 16);
            this.lengthCaptionLabel.Location = new System.Drawing.Point(3, 215);
            this.lengthCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.typeLabel.Location = new System.Drawing.Point(97, 231);
            this.typeLabel.Size = new System.Drawing.Size(140, 16);
            this.typeCaptionLabel.Location = new System.Drawing.Point(97, 215);
            this.typeCaptionLabel.Size = new System.Drawing.Size(34, 16);
        }

        /// <summary>
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する（水平）
        /// </summary>
        private void FixWindowSizeHorizon()
        {
            this.titleCaptionLabel.Location = new System.Drawing.Point(3, 3);
            this.titleCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.titleCaptionLabel.Text = "番組名";
            this.titleLabel.Location = new System.Drawing.Point(3, 19);
            this.titleLabel.Size = new System.Drawing.Size(314, 16);
            this.descriptionCaptionLabel.Location = new System.Drawing.Point(3, 35);
            this.descriptionCaptionLabel.Size = new System.Drawing.Size(48, 16);
            this.descriptionLabel.Location = new System.Drawing.Point(3, 51);
            this.descriptionLabel.Size = new System.Drawing.Size(314, 16);
            this.linkCaptionLabel.Location = new System.Drawing.Point(3, 93);
            this.linkCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.linkLabel.Location = new System.Drawing.Point(3, 109);
            this.linkLabel.Size = new System.Drawing.Size(236, 16);
            this.dateCaptionLabel.Location = new System.Drawing.Point(3, 141);
            this.dateCaptionLabel.Size = new System.Drawing.Size(64, 16);
            this.dateLabel.Location = new System.Drawing.Point(73, 141);
            this.dateLabel.Size = new System.Drawing.Size(244, 16);
            this.accessButton.Location = new System.Drawing.Point(245, 109);
            this.accessButton.Size = new System.Drawing.Size(72, 20);
            this.authorLabel.Location = new System.Drawing.Point(41, 125);
            this.authorLabel.Size = new System.Drawing.Size(88, 16);
            this.authorCaptionLabel.Location = new System.Drawing.Point(3, 125);
            this.authorCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.lengthLabel.Location = new System.Drawing.Point(41, 157);
            this.lengthLabel.Size = new System.Drawing.Size(88, 16);
            this.lengthCaptionLabel.Location = new System.Drawing.Point(3, 157);
            this.lengthCaptionLabel.Size = new System.Drawing.Size(32, 16);
            this.typeLabel.Location = new System.Drawing.Point(175, 157);
            this.typeLabel.Size = new System.Drawing.Size(142, 16);
            this.typeCaptionLabel.Location = new System.Drawing.Point(135, 157);
            this.typeCaptionLabel.Size = new System.Drawing.Size(34, 16);
        }

        private void ChannelPropertyForm_Load(object sender, System.EventArgs e)
        {
            FixWindowSize();
            titleLabel.Text = channel.Title.Trim();
            descriptionLabel.Text = channel.Description.Trim();
            linkLabel.Text = ((channel.GetWebsiteUrl() != null) ? channel.GetWebsiteUrl().ToString().Trim() : "");
            authorLabel.Text = channel.Author.Trim();
            dateLabel.Text = channel.Date.ToString().Trim();
            lengthLabel.Text = channel.Length.Trim();
            typeLabel.Text = channel.Type.Trim();
        }

        private void AccessButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (linkLabel.Text.Trim().Length != 0)
                {
                    PocketLadioUtility.AccessWebsite(channel.GetWebsiteUrl());
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("ブラウザが見つかりません", "警告");
            }
        }

        private void OkMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void ChannelPropertyForm_Resize(object sender, EventArgs e)
        {
            FixWindowSize();
        }

    }
}
