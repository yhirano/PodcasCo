#region ディレクティブを使用する

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Diagnostics;
using MiscPocketCompactLibrary.Windows.Forms;

#endregion

namespace PodcasCo
{
    /// <summary>
    /// StationsSettingForm の概要の説明です。
    /// </summary>
    public class StationsSettingForm : System.Windows.Forms.Form
    {
        private MenuItem okMenuItem;
        private Label stationListLabel;
        private Label addPodcastUrlLabel;
        private ListBox stationListBox;
        private Button deleteButton;
        private Button addButton;
        private TextBox podcastUrlTextBox;
        private ContextMenu stationListBoxContextMenu;
        private MenuItem deleteMenuItem;
        private ContextMenu podcastUrlContextMenu;
        private MenuItem cutPodcastUrlMenuItem;
        private MenuItem copyPodcastUrlMenuItem;
        private MenuItem pastePodcastUrlMenuItem;

        /// <summary>
        /// 放送局のリスト
        /// </summary>
        private ArrayList alStationList = new ArrayList();

        /// <summary>
        /// フォームのメイン メニューです。
        /// </summary>
        private System.Windows.Forms.MainMenu mainMenu;

        public StationsSettingForm()
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
            this.stationListLabel = new System.Windows.Forms.Label();
            this.addPodcastUrlLabel = new System.Windows.Forms.Label();
            this.stationListBox = new System.Windows.Forms.ListBox();
            this.stationListBoxContextMenu = new System.Windows.Forms.ContextMenu();
            this.deleteMenuItem = new System.Windows.Forms.MenuItem();
            this.deleteButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.podcastUrlTextBox = new System.Windows.Forms.TextBox();
            this.podcastUrlContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutPodcastUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.copyPodcastUrlMenuItem = new System.Windows.Forms.MenuItem();
            this.pastePodcastUrlMenuItem = new System.Windows.Forms.MenuItem();
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
            // stationListLabel
            // 
            this.stationListLabel.Location = new System.Drawing.Point(3, 77);
            this.stationListLabel.Size = new System.Drawing.Size(84, 20);
            this.stationListLabel.Text = "Podcast一覧";
            // 
            // addPodcastUrlLabel
            // 
            this.addPodcastUrlLabel.Location = new System.Drawing.Point(3, 4);
            this.addPodcastUrlLabel.Size = new System.Drawing.Size(110, 20);
            this.addPodcastUrlLabel.Text = "PodcastのRSS URL";
            // 
            // stationListBox
            // 
            this.stationListBox.ContextMenu = this.stationListBoxContextMenu;
            this.stationListBox.Location = new System.Drawing.Point(3, 99);
            this.stationListBox.Size = new System.Drawing.Size(234, 142);
            // 
            // stationListBoxContextMenu
            // 
            this.stationListBoxContextMenu.MenuItems.Add(this.deleteMenuItem);
            // 
            // deleteMenuItem
            // 
            this.deleteMenuItem.Text = "削除(&D)";
            this.deleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(165, 245);
            this.deleteButton.Size = new System.Drawing.Size(72, 20);
            this.deleteButton.Text = "削除(&D)";
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(165, 54);
            this.addButton.Size = new System.Drawing.Size(72, 20);
            this.addButton.Text = "追加(&A)";
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // podcastUrlTextBox
            // 
            this.podcastUrlTextBox.ContextMenu = this.podcastUrlContextMenu;
            this.podcastUrlTextBox.Location = new System.Drawing.Point(3, 27);
            this.podcastUrlTextBox.Size = new System.Drawing.Size(234, 21);
            this.podcastUrlTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StationNameTextBox_KeyDown);
            // 
            // podcastUrlContextMenu
            // 
            this.podcastUrlContextMenu.MenuItems.Add(this.cutPodcastUrlMenuItem);
            this.podcastUrlContextMenu.MenuItems.Add(this.copyPodcastUrlMenuItem);
            this.podcastUrlContextMenu.MenuItems.Add(this.pastePodcastUrlMenuItem);
            // 
            // cutPodcastUrlMenuItem
            // 
            this.cutPodcastUrlMenuItem.Text = "切り取り(&T)";
            this.cutPodcastUrlMenuItem.Click += new System.EventHandler(this.CutPodcastUrlMenuItem_Click);
            // 
            // copyPodcastUrlMenuItem
            // 
            this.copyPodcastUrlMenuItem.Text = "コピー(&C)";
            this.copyPodcastUrlMenuItem.Click += new System.EventHandler(this.CopyPodcastUrlMenuItem_Click);
            // 
            // pastePodcastUrlMenuItem
            // 
            this.pastePodcastUrlMenuItem.Text = "貼り付け(&P)";
            this.pastePodcastUrlMenuItem.Click += new System.EventHandler(this.PastePodcastUrlMenuItem_Click);
            // 
            // StationsSettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.stationListLabel);
            this.Controls.Add(this.addPodcastUrlLabel);
            this.Controls.Add(this.stationListBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.podcastUrlTextBox);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "放送局の追加と削除";
            this.Resize += new System.EventHandler(this.StationsSettingForm_Resize);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.StationsSettingForm_Closing);
            this.Load += new System.EventHandler(this.StationsSettingForm_Load);

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
            this.stationListLabel.Location = new System.Drawing.Point(3, 77);
            this.stationListLabel.Size = new System.Drawing.Size(84, 20);
            this.addPodcastUrlLabel.Location = new System.Drawing.Point(3, 4);
            this.addPodcastUrlLabel.Size = new System.Drawing.Size(110, 20);
            this.stationListBox.ContextMenu = this.stationListBoxContextMenu;
            this.stationListBox.Location = new System.Drawing.Point(3, 99);
            this.stationListBox.Size = new System.Drawing.Size(234, 142);
            this.deleteButton.Location = new System.Drawing.Point(165, 245);
            this.deleteButton.Size = new System.Drawing.Size(72, 20);
            this.addButton.Location = new System.Drawing.Point(165, 54);
            this.addButton.Size = new System.Drawing.Size(72, 20);
            this.podcastUrlTextBox.Location = new System.Drawing.Point(3, 27);
            this.podcastUrlTextBox.Size = new System.Drawing.Size(234, 21);
        }

        /// <summary>
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する（水平）
        /// </summary>
        private void FixWindowSizeHorizon()
        {
            this.stationListLabel.Location = new System.Drawing.Point(3, 77);
            this.stationListLabel.Size = new System.Drawing.Size(84, 20);
            this.addPodcastUrlLabel.Location = new System.Drawing.Point(3, 4);
            this.addPodcastUrlLabel.Size = new System.Drawing.Size(110, 20);
            this.stationListBox.Location = new System.Drawing.Point(3, 99);
            this.stationListBox.Size = new System.Drawing.Size(314, 58);
            this.deleteButton.Location = new System.Drawing.Point(245, 165);
            this.deleteButton.Size = new System.Drawing.Size(72, 20);
            this.addButton.Location = new System.Drawing.Point(245, 55);
            this.addButton.Size = new System.Drawing.Size(72, 20);
            this.podcastUrlTextBox.Location = new System.Drawing.Point(3, 27);
            this.podcastUrlTextBox.Size = new System.Drawing.Size(314, 21);
        }

        /// <summary>
        /// 放送局を作成する。
        /// </summary>
        private void CreateStation()
        {
            Station station = null;
            try
            {
                station = new Station(DateTime.Now.ToString("yyyyMMddHHmmssff"), podcastUrlTextBox.Text.Trim(),
                    Station.StationKind.RssPodcast, new Uri(podcastUrlTextBox.Text.Trim()));

                // リストに追加
                alStationList.Add(station);
                stationListBox.Items.Add(station.DisplayName);
            }
            catch (UriFormatException)
            {
                MessageBox.Show("URLが不正です", "警告");
            }
            catch (WebException)
            {
                MessageBox.Show("RSSが見つかりません", "警告");
            }
            catch (XmlException)
            {
                MessageBox.Show("RSSの内容が不正です", "警告");
            }
            catch (SocketException)
            {
                MessageBox.Show("ネットワークエラー", "警告");
            }
            catch (NotSupportedException) {
                MessageBox.Show("URLが不正かRSSの内容が不正な可能性があります", "警告");
            }
            finally
            {
                podcastUrlTextBox.Text = "";
            }

        }

        private void StationsSettingForm_Load(object sender, EventArgs e)
        {
            // フォーム内の中身のサイズを適正に変更する
            FixWindowSize();

            // 放送局情報の読み込み
            foreach (Station station in StationList.GetStationList())
            {
                alStationList.Add(station);
                stationListBox.Items.Add(station.DisplayName);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteStation();
        }

        private void DeleteStation()
        {
            Station deleteStation = (Station)alStationList[stationListBox.SelectedIndex];

            // 警告を出す
            DialogResult result =
                MessageBox.Show(deleteStation + "を削除しますか？\n（クリップされたファイルも削除されます）", "注意",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

            // 警告ダイアログの結果（削除しても良いか）がOKでない場合は終了
            if (result != DialogResult.Yes)
            {
                return;
            }

            if (stationListBox.SelectedIndex != -1)
            {
                // 設定ファイルの削除
                deleteStation.GlobalHeadline.DeleteUserSettingFile();
                deleteStation.LocalHeadline.DeleteUserSettingFile();

                // クリップしたディレクトリ・ファイルを削除する
                deleteStation.LocalHeadline.DeleteClipDirectory();

                // m3uファイルを削除
                if (File.Exists(UserSetting.M3uFilePath))
                {
                    File.Delete(UserSetting.M3uFilePath);
                }

                alStationList.RemoveAt(stationListBox.SelectedIndex);
                stationListBox.Items.RemoveAt(stationListBox.SelectedIndex);
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            // URLの入力がない場合は何もしない
            if (podcastUrlTextBox.Text.Trim().Length == 0)
            {
                return;
            }

            // 放送局名を得るのに時間がかかる場合があるため、その間操作できないように
            // UIの入力と操作を封じておく
            this.Enabled = false;

            // 放送局を作成する
            CreateStation();

            // UIの入力と操作を使えるようにする
            this.Enabled = true;
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            DeleteStation();
        }

        private void CutPodcastUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(podcastUrlTextBox);
        }

        private void CopyPodcastUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(podcastUrlTextBox);
        }

        private void PastePodcastUrlMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(podcastUrlTextBox);
        }

        private void OkMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StationsSettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 放送局を追加し忘れていると思われる場合
            if (podcastUrlTextBox.Text.Trim().Length != 0)
            {
                // 追加するかを聞く
                DialogResult result = MessageBox.Show(
                    podcastUrlTextBox.Text.Trim() + "を追加しますか？\n（" + podcastUrlTextBox.Text.Trim() + "はまだ追加されていません）",
                    "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    CreateStation();
                }
            }

            // 設定の書き込み
            StationList.SetStationList((Station[])alStationList.ToArray(typeof(Station)));
            try
            {
                UserSetting.SaveSetting();
            }
            catch (IOException)
            {
                MessageBox.Show("設定ファイルが書き込めませんでした", "設定ファイル書き込みエラー");
            }

        }

        private void StationsSettingForm_Resize(object sender, EventArgs e)
        {
            FixWindowSize();
        }

        private void StationNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // 入力ボタンを押したとき
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                AddButton_Click(sender, e);
            }
        }
    }
}
