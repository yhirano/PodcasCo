#region ディレクティブを使用する

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using MiscPocketCompactLibrary.Windows.Forms;

#endregion

namespace PodcasCo.Stations.RssPodcast
{
    /// <summary>
    /// 番組の詳細情報表示フォーム
    /// </summary>
    public class ChannelPropertyForm : System.Windows.Forms.Form
    {
        private MenuItem okMenuItem;
        private MainMenu mainMenu;
        private ListView propertyListView;
        private ColumnHeader infomationColumnHeader;
        private ColumnHeader discriptionColumnHeader;
        private Button accessButton;

        /// <summary>
        /// チャンネル
        /// </summary>
        private Channel channel;

        /// <summary>
        /// アンカーコントロールのリスト
        /// </summary>
        private ArrayList anchorControlList = new ArrayList();

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
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.okMenuItem = new System.Windows.Forms.MenuItem();
            this.propertyListView = new System.Windows.Forms.ListView();
            this.infomationColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.discriptionColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.accessButton = new System.Windows.Forms.Button();
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
            // propertyListView
            // 
            this.propertyListView.Columns.Add(this.infomationColumnHeader);
            this.propertyListView.Columns.Add(this.discriptionColumnHeader);
            this.propertyListView.Location = new System.Drawing.Point(3, 3);
            this.propertyListView.Size = new System.Drawing.Size(234, 236);
            this.propertyListView.View = System.Windows.Forms.View.Details;
            // 
            // infomationColumnHeader
            // 
            this.infomationColumnHeader.Text = "情報";
            this.infomationColumnHeader.Width = 60;
            // 
            // discriptionColumnHeader
            // 
            this.discriptionColumnHeader.Text = "詳細";
            this.discriptionColumnHeader.Width = 150;
            // 
            // accessButton
            // 
            this.accessButton.Location = new System.Drawing.Point(165, 245);
            this.accessButton.Size = new System.Drawing.Size(72, 20);
            this.accessButton.Text = "&Access";
            // 
            // ChannelPropertyForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.propertyListView);
            this.Controls.Add(this.accessButton);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "番組の詳細";
            this.Resize += new System.EventHandler(this.ChannelPropertyForm_Resize);
            this.Load += new System.EventHandler(this.ChannelPropertyForm_Load);

        }
        #endregion

        /// <summary>
        /// コントロールにアンカーをセットする
        /// </summary>
        private void SetAnchorControl()
        {
            anchorControlList.Add(new AnchorLayout(propertyListView, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom, PodcasCoInfo.FormBaseWidth, PodcasCoInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(accessButton, AnchorStyles.Right | AnchorStyles.Bottom, PodcasCoInfo.FormBaseWidth, PodcasCoInfo.FormBaseHight));
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

        /// <summary>
        /// ListViewのコラムサイズを自動で調整する
        /// </summary>
        /// <param name="listView">ListView</param>
        private void AutoResizeColumnListView(ListView listView)
        {
            foreach (ColumnHeader ch in listView.Columns)
            {
                ch.Width = -2;
            }
        }

        private void ChannelPropertyForm_Load(object sender, System.EventArgs e)
        {
            SetAnchorControl();
            FixWindowSize();

            string[] titleProperty = { "番組名", channel.Title.Trim() };
            string[] descriptionProperty = { "詳細", channel.Description.Trim() };
            string[] linkProperty = { "リンク", ((channel.GetWebsiteUrl() != null) ? channel.GetWebsiteUrl().ToString().Trim() : "") };
            string[] authorProperty = { "著者", channel.Date.ToString().Trim() };
            string[] dateProperty = { "配信時刻", channel.Date.ToString().Trim() };
            string[] lengthProperty = { "長さ", channel.Length.Trim() };
            string[] typeProperty = { "タイプ", channel.Type.Trim() };

            propertyListView.Items.Add(new ListViewItem(titleProperty));
            propertyListView.Items.Add(new ListViewItem(descriptionProperty));
            propertyListView.Items.Add(new ListViewItem(linkProperty));
            propertyListView.Items.Add(new ListViewItem(authorProperty));
            propertyListView.Items.Add(new ListViewItem(dateProperty));
            propertyListView.Items.Add(new ListViewItem(lengthProperty));
            propertyListView.Items.Add(new ListViewItem(typeProperty));

            AutoResizeColumnListView(propertyListView);
        }

        private void AccessButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (channel.GetWebsiteUrl() != null)
                {
                    PodcasCoUtility.AccessWebsite(channel.GetWebsiteUrl());
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
