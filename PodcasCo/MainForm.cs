#region Using directives

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
using PodcasCo.Stations;
using MiscPocketCompactLibrary.Reflection;

#endregion

namespace PodcasCo
{
    /// <summary>
    /// フォームの概要の説明です。
    /// </summary>
    public class MainForm : System.Windows.Forms.Form
    {
        private MenuItem menuMenuItem;
        private MenuItem versionInfoMenuItem;
        private MenuItem exitMenuItem;
        private Button updateButton;
        private Button clipButton;
        private Button playButton;
        private ComboBox stationFilterComboBox;
        private RadioButton allClipRadioButton;
        private RadioButton clipedRadioButton;
        private Panel clipFilterPanel;
        private RadioButton unclipedRadioButton;
        private Label lastCheckInfomationLabel;
        private ListView channelListView;
        private ColumnHeader channelColumnHeader;
        private Label clipInfomationLabel;
        private MenuItem separateMenuItem2;
        private MenuItem separateMenuItem1;
        private MenuItem podcasCoSettingMenuItem;
        private MenuItem stationsSettingMenuItem;
        /// <summary>
        /// フォームのメイン メニューです。
        /// </summary>
        private System.Windows.Forms.MainMenu mainMenu;
        private ContextMenu stationListContextMenu;
        private MenuItem selectAllMenuItem;
        private MenuItem selectUnclipedMenuItem;
        private MenuItem selectClipedMenuItem;
        private MenuItem clipSelectedPodcastMenuItem;
        private MenuItem deleteSelectedPodcastMenuItem;

        /// <summary>
        /// 現在表示されている番組リスト
        /// </summary>
        private IChannel[] currentChannels;

        public MainForm()
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
            this.menuMenuItem = new System.Windows.Forms.MenuItem();
            this.stationsSettingMenuItem = new System.Windows.Forms.MenuItem();
            this.podcasCoSettingMenuItem = new System.Windows.Forms.MenuItem();
            this.separateMenuItem1 = new System.Windows.Forms.MenuItem();
            this.versionInfoMenuItem = new System.Windows.Forms.MenuItem();
            this.separateMenuItem2 = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.updateButton = new System.Windows.Forms.Button();
            this.clipButton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.stationFilterComboBox = new System.Windows.Forms.ComboBox();
            this.allClipRadioButton = new System.Windows.Forms.RadioButton();
            this.clipedRadioButton = new System.Windows.Forms.RadioButton();
            this.clipFilterPanel = new System.Windows.Forms.Panel();
            this.unclipedRadioButton = new System.Windows.Forms.RadioButton();
            this.lastCheckInfomationLabel = new System.Windows.Forms.Label();
            this.channelListView = new System.Windows.Forms.ListView();
            this.channelColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.stationListContextMenu = new System.Windows.Forms.ContextMenu();
            this.selectAllMenuItem = new System.Windows.Forms.MenuItem();
            this.selectUnclipedMenuItem = new System.Windows.Forms.MenuItem();
            this.selectClipedMenuItem = new System.Windows.Forms.MenuItem();
            this.clipSelectedPodcastMenuItem = new System.Windows.Forms.MenuItem();
            this.deleteSelectedPodcastMenuItem = new System.Windows.Forms.MenuItem();
            this.clipInfomationLabel = new System.Windows.Forms.Label();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.menuMenuItem);
            // 
            // menuMenuItem
            // 
            this.menuMenuItem.MenuItems.Add(this.stationsSettingMenuItem);
            this.menuMenuItem.MenuItems.Add(this.podcasCoSettingMenuItem);
            this.menuMenuItem.MenuItems.Add(this.separateMenuItem1);
            this.menuMenuItem.MenuItems.Add(this.versionInfoMenuItem);
            this.menuMenuItem.MenuItems.Add(this.separateMenuItem2);
            this.menuMenuItem.MenuItems.Add(this.exitMenuItem);
            this.menuMenuItem.Text = "メニュー(&M)";
            // 
            // stationsSettingMenuItem
            // 
            this.stationsSettingMenuItem.Text = "放送局の追加と削除 (&A)";
            this.stationsSettingMenuItem.Click += new System.EventHandler(this.StationsSettingMenuItem_Click);
            // 
            // podcasCoSettingMenuItem
            // 
            this.podcasCoSettingMenuItem.Text = "podcasCo設定(&P)";
            this.podcasCoSettingMenuItem.Click += new System.EventHandler(this.PodcasCoSettingMenuItem_Click);
            // 
            // separateMenuItem1
            // 
            this.separateMenuItem1.Text = "-";
            // 
            // versionInfoMenuItem
            // 
            this.versionInfoMenuItem.Text = "バージョン情報(&A)";
            this.versionInfoMenuItem.Click += new System.EventHandler(this.VersionInfoMenuItem_Click);
            // 
            // separateMenuItem2
            // 
            this.separateMenuItem2.Text = "-";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Text = "終了(&X)";
            this.exitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(3, 3);
            this.updateButton.Size = new System.Drawing.Size(72, 20);
            this.updateButton.Text = "&Update";
            this.updateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // clipButton
            // 
            this.clipButton.Location = new System.Drawing.Point(81, 3);
            this.clipButton.Size = new System.Drawing.Size(72, 20);
            this.clipButton.Text = "&Clip";
            this.clipButton.Click += new System.EventHandler(this.ClipButton_Click);
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(165, 3);
            this.playButton.Size = new System.Drawing.Size(72, 20);
            this.playButton.Text = "&Play";
            this.playButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // stationFilterComboBox
            // 
            this.stationFilterComboBox.Location = new System.Drawing.Point(3, 29);
            this.stationFilterComboBox.Size = new System.Drawing.Size(234, 22);
            this.stationFilterComboBox.SelectedIndexChanged += new System.EventHandler(this.StationFilterComboBox_SelectedIndexChanged);
            // 
            // allClipRadioButton
            // 
            this.allClipRadioButton.Checked = true;
            this.allClipRadioButton.Location = new System.Drawing.Point(3, 0);
            this.allClipRadioButton.Size = new System.Drawing.Size(71, 20);
            this.allClipRadioButton.Text = "A&ll";
            this.allClipRadioButton.CheckedChanged += new System.EventHandler(this.ClipRadioButton_CheckedChanged);
            // 
            // clipedRadioButton
            // 
            this.clipedRadioButton.Location = new System.Drawing.Point(80, 0);
            this.clipedRadioButton.Size = new System.Drawing.Size(71, 20);
            this.clipedRadioButton.Text = "&Cliped";
            this.clipedRadioButton.CheckedChanged += new System.EventHandler(this.ClipRadioButton_CheckedChanged);
            // 
            // clipFilterPanel
            // 
            this.clipFilterPanel.Controls.Add(this.unclipedRadioButton);
            this.clipFilterPanel.Controls.Add(this.allClipRadioButton);
            this.clipFilterPanel.Controls.Add(this.clipedRadioButton);
            this.clipFilterPanel.Location = new System.Drawing.Point(3, 52);
            this.clipFilterPanel.Size = new System.Drawing.Size(234, 20);
            // 
            // unclipedRadioButton
            // 
            this.unclipedRadioButton.Location = new System.Drawing.Point(157, 0);
            this.unclipedRadioButton.Size = new System.Drawing.Size(71, 20);
            this.unclipedRadioButton.Text = "&Uncliped";
            this.unclipedRadioButton.CheckedChanged += new System.EventHandler(this.ClipRadioButton_CheckedChanged);
            // 
            // lastCheckInfomationLabel
            // 
            this.lastCheckInfomationLabel.Location = new System.Drawing.Point(83, 248);
            this.lastCheckInfomationLabel.Size = new System.Drawing.Size(154, 20);
            this.lastCheckInfomationLabel.Text = "No check";
            this.lastCheckInfomationLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // channelListView
            // 
            this.channelListView.CheckBoxes = true;
            this.channelListView.Columns.Add(this.channelColumnHeader);
            this.channelListView.ContextMenu = this.stationListContextMenu;
            this.channelListView.Location = new System.Drawing.Point(3, 78);
            this.channelListView.Size = new System.Drawing.Size(234, 167);
            this.channelListView.View = System.Windows.Forms.View.Details;
            this.channelListView.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.StationListView_ItemCheck);
            // 
            // channelColumnHeader
            // 
            this.channelColumnHeader.Text = "Title";
            this.channelColumnHeader.Width = 210;
            // 
            // stationListContextMenu
            // 
            this.stationListContextMenu.MenuItems.Add(this.selectAllMenuItem);
            this.stationListContextMenu.MenuItems.Add(this.selectUnclipedMenuItem);
            this.stationListContextMenu.MenuItems.Add(this.selectClipedMenuItem);
            this.stationListContextMenu.MenuItems.Add(this.clipSelectedPodcastMenuItem);
            this.stationListContextMenu.MenuItems.Add(this.deleteSelectedPodcastMenuItem);
            // 
            // selectAllMenuItem
            // 
            this.selectAllMenuItem.Text = "すべて選択(&A)";
            this.selectAllMenuItem.Click += new System.EventHandler(this.SelectAllMenuItem_Click);
            // 
            // selectUnclipedMenuItem
            // 
            this.selectUnclipedMenuItem.Text = "未クリップのPodcastを選択(&U)";
            this.selectUnclipedMenuItem.Click += new System.EventHandler(this.SelectUnclipedMenuItem_Click);
            // 
            // selectClipedMenuItem
            // 
            this.selectClipedMenuItem.Text = "クリップ済みのPodcastを選択(&L)";
            this.selectClipedMenuItem.Click += new System.EventHandler(this.SelectClipedMenuItem_Click);
            // 
            // clipSelectedPodcastMenuItem
            // 
            this.clipSelectedPodcastMenuItem.Text = "選択したPodcastをクリップ(&C)";
            this.clipSelectedPodcastMenuItem.Click += new System.EventHandler(this.ClipSelectedPodcastMenuItem_Click);
            // 
            // deleteSelectedPodcastMenuItem
            // 
            this.deleteSelectedPodcastMenuItem.Text = "選択したPodcastを削除(&D)";
            this.deleteSelectedPodcastMenuItem.Click += new System.EventHandler(this.DeleteSelectedPodcastMenuItem_Click);
            // 
            // clipInfomationLabel
            // 
            this.clipInfomationLabel.Location = new System.Drawing.Point(3, 248);
            this.clipInfomationLabel.Size = new System.Drawing.Size(74, 20);
            this.clipInfomationLabel.Text = "0/0/0";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.clipInfomationLabel);
            this.Controls.Add(this.channelListView);
            this.Controls.Add(this.lastCheckInfomationLabel);
            this.Controls.Add(this.clipFilterPanel);
            this.Controls.Add(this.stationFilterComboBox);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.clipButton);
            this.Controls.Add(this.updateButton);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "PodcasCo";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);

        }

        #endregion

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        static void Main()
        {
            Application.Run(new MainForm());
        }

        /// <summary>
        /// 切り替えボックスの追加
        /// </summary>
        private void AddStationFilterComboBoxItem()
        {
            // 各放送局の切り替えボックスをいったん選択不可にする
            this.stationFilterComboBox.Enabled = false;
            // 各放送局の切り替えボックスをいったんクリアする
            this.stationFilterComboBox.Items.Clear();
            // 切り替えボックスの先頭に"All"を追加
            this.stationFilterComboBox.Items.Add("All");
            // 各放送局の切り替えボックスの追加
            foreach (Station station in StationList.GetStationList())
            {
                this.stationFilterComboBox.Items.Add(station.DisplayName);
            }
            // 切り替えボックスが追加し終わったので、各放送局の切り替えボックスを選択可能にする
            this.stationFilterComboBox.Enabled = true;

            // 放送局が選択されておらず、かつ放送局がある場合
            if (this.stationFilterComboBox.SelectedIndex == -1 && this.stationFilterComboBox.Items.Count > 0)
            {
                // トップの放送局を選択
                this.stationFilterComboBox.SelectedIndex = 0;
            }
        }

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
            this.updateButton.Location = new System.Drawing.Point(3, 3);
            this.updateButton.Size = new System.Drawing.Size(72, 20);
            this.clipButton.Location = new System.Drawing.Point(81, 3);
            this.clipButton.Size = new System.Drawing.Size(72, 20);
            this.playButton.Location = new System.Drawing.Point(165, 3);
            this.playButton.Size = new System.Drawing.Size(72, 20);
            this.stationFilterComboBox.Location = new System.Drawing.Point(3, 29);
            this.stationFilterComboBox.Size = new System.Drawing.Size(234, 22);
            this.allClipRadioButton.Location = new System.Drawing.Point(3, 0);
            this.allClipRadioButton.Size = new System.Drawing.Size(71, 20);
            this.clipedRadioButton.Location = new System.Drawing.Point(80, 0);
            this.clipedRadioButton.Size = new System.Drawing.Size(71, 20);
            this.clipFilterPanel.Location = new System.Drawing.Point(3, 52);
            this.clipFilterPanel.Size = new System.Drawing.Size(234, 20);
            this.unclipedRadioButton.Location = new System.Drawing.Point(157, 0);
            this.unclipedRadioButton.Size = new System.Drawing.Size(71, 20);
            this.lastCheckInfomationLabel.Location = new System.Drawing.Point(83, 248);
            this.lastCheckInfomationLabel.Size = new System.Drawing.Size(154, 20);
            this.channelListView.Location = new System.Drawing.Point(3, 78);
            this.channelListView.Size = new System.Drawing.Size(234, 167);
            this.clipInfomationLabel.Location = new System.Drawing.Point(3, 248);
            this.clipInfomationLabel.Size = new System.Drawing.Size(74, 20);
            this.clipInfomationLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
        }

        /// <summary>
        /// フォームのサイズ変更時にフォーム内の中身のサイズを適正に変更する（水平）
        /// </summary>
        private void FixWindowSizeHorizon()
        {
            this.updateButton.Location = new System.Drawing.Point(245, 3);
            this.updateButton.Size = new System.Drawing.Size(72, 20);
            this.clipButton.Location = new System.Drawing.Point(245, 31);
            this.clipButton.Size = new System.Drawing.Size(72, 20);
            this.playButton.Location = new System.Drawing.Point(245, 57);
            this.playButton.Size = new System.Drawing.Size(72, 20);
            this.stationFilterComboBox.Location = new System.Drawing.Point(3, 3);
            this.stationFilterComboBox.Size = new System.Drawing.Size(234, 22);
            this.allClipRadioButton.Location = new System.Drawing.Point(3, 0);
            this.allClipRadioButton.Size = new System.Drawing.Size(71, 20);
            this.clipedRadioButton.Location = new System.Drawing.Point(80, 0);
            this.clipedRadioButton.Size = new System.Drawing.Size(71, 20);
            this.clipFilterPanel.Location = new System.Drawing.Point(3, 31);
            this.clipFilterPanel.Size = new System.Drawing.Size(234, 20);
            this.unclipedRadioButton.Location = new System.Drawing.Point(157, 0);
            this.unclipedRadioButton.Size = new System.Drawing.Size(71, 20);
            this.lastCheckInfomationLabel.Location = new System.Drawing.Point(245, 168);
            this.lastCheckInfomationLabel.Size = new System.Drawing.Size(72, 20);
            this.channelListView.Location = new System.Drawing.Point(3, 57);
            this.channelListView.Size = new System.Drawing.Size(234, 128);
            this.clipInfomationLabel.Location = new System.Drawing.Point(245, 148);
            this.clipInfomationLabel.Size = new System.Drawing.Size(72, 20);
            this.clipInfomationLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
        }

        /// <summary>
        /// 番組リストを描画する
        /// </summary>
        /// <param name="channels">番組の配列</param>
        private void DrawChannelList(IChannel[] channels)
        {
            #region UI前処理

            channelListView.BeginUpdate();

            #endregion

            channelListView.Items.Clear();
            foreach (IChannel channel in channels)
            {
                ListViewItem item = new ListViewItem(
                    (channel.IsLocal() == false) ? channel.GetTitle() : "[C] " + channel.GetTitle() + " " + channel.ParentHeadline.GetId()
                    );
                // 番組のチェック欄がすでにチェックされていた場合にはチェックを入れる
                item.Checked = channel.Check;
                channelListView.Items.Add(item);
            }

            #region UI前処理

            channelListView.EndUpdate();

            #endregion
        }

        /// <summary>
        /// ヘッドラインを取得時のオプション
        /// </summary>
        private enum CheckHeadlinesOption
        {
            All, LocalOnly
        }

        /// <summary>
        /// ヘッドラインを取得、表示する
        /// </summary>
        /// <param name="option">オプション。CheckHeadlinesOption.LocalOnlyの場合はローカルのヘッドラインのみが対象となる。</param>
        private void CheckHeadlines(CheckHeadlinesOption option)
        {
            try
            {
                #region UI前処理

                // Updateボタン、Clipボタン、Playボタンをいったん選択不可にする
                updateButton.Enabled = false;
                clipButton.Enabled = false;
                playButton.Enabled = false;
                // 放送局選択ボックスをいったん選択不可にする
                stationFilterComboBox.Enabled = false;

                #endregion

                #region 番組取得処理

                if (option == CheckHeadlinesOption.All)
                {
                    // 番組を取得する（グローバル・ローカル）
                    StationList.FetchHeadlineOfCurrentStations();

                }
                else if (option == CheckHeadlinesOption.LocalOnly)
                {
                    // 番組を取得する（ローカル）
                    StationList.FetchLocalHeadlineOfCurrentStations();
                }
                else
                {
                    // ここに到達することはあり得ない
                    Trace.Assert(false, "想定外の動作のため、終了します");
                }

                // 番組が取得できなかった場合
                if (StationList.LastCheckTimeOfCurrentStation.Equals(DateTime.MinValue))
                {
                    lastCheckInfomationLabel.Text = "No Check";
                }
                // 番組が取得できた場合
                else
                {
                    lastCheckInfomationLabel.Text = "Last " + StationList.LastCheckTimeOfCurrentStation.ToString();
                }

                #endregion

                // 番組リストを更新する
                UpdateChannelList();
            }
            catch (WebException)
            {
                MessageBox.Show("番組表を取得できませんでした", "接続エラー");
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("メモリが足りません", "メモリエラー");
            }
            catch (IOException)
            {
                MessageBox.Show("記録デバイスが何らかのエラーです", "デバイスエラー");
            }
            catch (UriFormatException)
            {
                MessageBox.Show(StationList.StationNameOfCurrentStation + "のURLが不正です", "URLエラー");
            }
            catch (SocketException)
            {
                MessageBox.Show("番組表を取得できませんでした", "ネットワークエラー");
            }
            catch (NotSupportedException)
            {
                MessageBox.Show(StationList.StationNameOfCurrentStation + "のURLが不正です", "URLエラー");
            }
            catch (XmlException)
            {
                MessageBox.Show("XML形式のヘッドラインが正常に処理できませんでした", "XMLエラー");
            }
            catch (ArgumentException)
            {
                MessageBox.Show(StationList.StationNameOfCurrentStation + "のURLが不正です", "URLエラー");
            }
            finally
            {
                #region UI後処理

                // Updateボタン、Clipボタン、Playボタンをを選択可能に回復する
                updateButton.Enabled = true;
                clipButton.Enabled = true;
                playButton.Enabled = true;
                // 放送局選択ボックスを選択可能に回復する
                stationFilterComboBox.Enabled = true;

                #endregion
            }
        }

        /// <summary>
        /// 番組リストの更新処理
        /// </summary>
        private void UpdateChannelList()
        {
            #region UI前処理

            // 放送局フィルターとクリップフィルターをいったん選択不可にする
            stationFilterComboBox.Enabled = false;
            allClipRadioButton.Enabled = false;
            clipedRadioButton.Enabled = false;
            unclipedRadioButton.Enabled = false;

            #endregion

            #region フィルタリング処理

            // 放送局切り替えボックス Allが選択されている場合
            if (stationFilterComboBox.SelectedIndex == 0)
            {
                // 全放送局を対象とする
                StationList.ChangeCurrentStationAt(-1);
            }
            else
            {
                /*
                 * 放送局切り替えボックスに対して、StationList配列の並びは-1となるため、-1を指定
                 * stationListComboBox => ALL, Station1, Station2 ...
                 * StationList => Station1, Station2 ...
                 */
                StationList.ChangeCurrentStationAt(stationFilterComboBox.SelectedIndex - 1);
            }

            // クリップ ALLが選択されている場合
            if (allClipRadioButton.Checked == true)
            {
                currentChannels = StationList.GetChannelsOfCurrentStationFromAllHeadline();
            }
            // クリップ Clipedが選択されている場合
            else if (clipedRadioButton.Checked == true)
            {
                currentChannels = StationList.GetChannelsOfCurrentStationFromLocalHeadline();
            }
            // クリップ Unclipedが選択されている場合
            else if (unclipedRadioButton.Checked == true)
            {
                currentChannels = StationList.GetUnclipedChannelsOfCurrentStation();
            }
            else
            {
                // ここに到達することはあり得ない
                Trace.Assert(false, "想定外の動作のため、終了します");
            }

            DrawChannelList(currentChannels);

            #endregion

            #region UI後処理

            clipInfomationLabel.Text = StationList.GetChannelsOfCurrentStationFromAllHeadline().Length
                + "/" + StationList.GetChannelsOfCurrentStationFromLocalHeadline().Length
                + "/" + StationList.GetUnclipedChannelsOfCurrentStation().Length;

            // 放送局フィルターとクリップフィルターを選択可能に回復する
            stationFilterComboBox.Enabled = true;
            allClipRadioButton.Enabled = true;
            clipedRadioButton.Enabled = true;
            unclipedRadioButton.Enabled = true;

            #endregion
        }

        /// <summary>
        /// stationListViewの選択が変わった場合の処理
        /// </summary>
        private void StationListViewSelectChanged()
        {
            for (int count = 0; count < channelListView.Items.Count; ++count)
            {
                currentChannels[count].Check = ((ListViewItem)channelListView.Items[count]).Checked;
            }
        }

        /// <summary>
        /// 選択された番組からm3uファイルを作成し、それをメディアプレイヤーに渡して実行する
        /// </summary>
        private void PlayPodcast()
        {
            // 選択された番組のリスト
            ArrayList alSelectedLocalChannels = new ArrayList();

            // 選択された番組でクリップ済みの番組のリストを作る
            foreach (IChannel channel in StationList.GetChannelsOfCurrentStationFromLocalHeadline())
            {
                if (channel.Check == true)
                {
                    alSelectedLocalChannels.Add(channel);
                }
            }

            // 選択されたクリップ済みの番組が無い場合は警告を出して終了
            if (alSelectedLocalChannels.Count == 0)
            {
                MessageBox.Show("クリップ済の番組が選択されていません", "情報");

                return;
            }

            // すでにあるm3uファイルを削除
            if (File.Exists(UserSetting.M3uFilePath))
            {
                File.Delete(UserSetting.M3uFilePath);
            }

            try
            {
                // ディレクトリを作成する
                if (Directory.Exists(UserSetting.PodcastClipDirectoryPath) == false)
                {
                    Directory.CreateDirectory(UserSetting.PodcastClipDirectoryPath);
                }

                using (StreamWriter sw = new StreamWriter(UserSetting.M3uFilePath))
                {
                    foreach (IChannel channel in (IChannel[])alSelectedLocalChannels.ToArray(typeof(IChannel)))
                    {
                        sw.WriteLine(channel.GetPlayUrl().LocalPath);
                    }
                }
            }
            catch (IOException) { }

            try
            {
                PocketLadioUtility.PlayStreaming(UserSetting.M3uFilePath);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("メディアプレイヤーが見つかりません", "警告");
            }
        }

        /// <summary>
        /// チェックした番組をダウンロードし、RSSを作成する
        /// </summary>
        private void ClipPodcast()
        {
            #region UI前処理

            // Updateボタン、Clipボタン、Playボタンをいったん選択不可にする
            updateButton.Enabled = false;
            clipButton.Enabled = false;
            playButton.Enabled = false;
            // 放送局フィルターとクリップフィルターをいったん選択不可にする
            stationFilterComboBox.Enabled = false;
            allClipRadioButton.Enabled = false;
            clipedRadioButton.Enabled = false;
            unclipedRadioButton.Enabled = false;
            // 番組リストをいったん選択不可にする
            channelListView.Enabled = false;
            // 放送局選択ボックスをいったん選択不可にする
            stationFilterComboBox.Enabled = false;

            #endregion

            #region ダウンロード処理

            // 選択された番組のリスト
            ArrayList alSelectedGlobalChannels = new ArrayList();

            // 選択された番組で未クリップの番組のリストを作る
            foreach (IChannel channel in StationList.GetUnclipedChannelsOfCurrentStation())
            {
                if (channel.Check == true)
                {
                    alSelectedGlobalChannels.Add(channel);
                }
            }

            // 選択された未クリップの番組がある場合のみ
            if (alSelectedGlobalChannels.Count != 0)
            {
                try
                {
                    foreach (IChannel channel in (IChannel[])alSelectedGlobalChannels.ToArray(typeof(IChannel)))
                    {
                        try
                        {
                            string directoryName = UserSetting.PodcastClipDirectoryPath + @"\" + channel.ParentHeadline.ParentStation.LocalHeadline.GetId();

                            if (Directory.Exists(directoryName) == false)
                            {
                                Directory.CreateDirectory(directoryName);
                            }

                            string generateFilePath = directoryName + @"\"
                                + Path.GetFileNameWithoutExtension(channel.GetPlayUrl().LocalPath)
                                + "-" + channel.GetHash().ToString("x")
                                + Path.GetExtension(channel.GetPlayUrl().LocalPath);

                            if (File.Exists(generateFilePath) == false)
                            {
                                PocketLadioUtility.FetchFile(channel.GetPlayUrl(), generateFilePath);
                                
                                // 番組をローカルヘッドラインに加える
                                IChannel localChannel = (IChannel)channel.Clone(channel.ParentHeadline.ParentStation.LocalHeadline);
                                localChannel.SetPlayUrl(new Uri(generateFilePath));
                                localChannel.ParentHeadline.ParentStation.LocalHeadline.AddChannel(localChannel);

                                // ダウンロードした番組の元のグローバル番組からはチェックを外す
                                channel.Check = false;
                            }
                        }
                        catch (WebException)
                        {
                            ;
                        }
                        catch (UriFormatException)
                        {
                            ;
                        }
                        catch (SocketException)
                        {
                            ;
                        }
                    }
                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show("ファイルがダウンロードできませんでした", "警告"); ;
                }
                catch (IOException)
                {
                    MessageBox.Show("ファイルがダウンロードできませんでした", "警告"); ;
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("ファイルがダウンロードできませんでした", "警告"); ;
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("ファイルがダウンロードできませんでした", "警告"); ;
                }

                foreach (Station station in StationList.GetStationList())
                {
                    string rssFileName = UserSetting.PodcastClipDirectoryPath + @"\"
                        + station.LocalHeadline.GetId() + @"\rss.xml";
                    station.LocalHeadline.GenerateRss(rssFileName);
                }
            }

            #endregion

            #region UI後処理

            // 選択された未クリップの番組が無い場合は警告を出す
            if (alSelectedGlobalChannels.Count == 0) {
                MessageBox.Show("未クリップの番組が選択されていません","情報");
            }

            // 番組リストを選択可能に回復する
            channelListView.Enabled = true;
            // 放送局フィルターとクリップフィルターを選択可能に回復する
            stationFilterComboBox.Enabled = true;
            allClipRadioButton.Enabled = true;
            clipedRadioButton.Enabled = true;
            unclipedRadioButton.Enabled = true;
            // Updateボタン、Clipボタン、Playボタンをを選択可能に回復する
            updateButton.Enabled = true;
            clipButton.Enabled = true;
            playButton.Enabled = true;
            // 放送局選択ボックスを選択可能に回復する
            stationFilterComboBox.Enabled = true;

            #endregion
        }

        /// <summary>
        /// チェックした番組を削除する
        /// </summary>
        private void DeletePodcast()
        {

            #region UI前処理

            // Updateボタン、Clipボタン、Playボタンをいったん選択不可にする
            updateButton.Enabled = false;
            clipButton.Enabled = false;
            playButton.Enabled = false;
            // 放送局フィルターとクリップフィルターをいったん選択不可にする
            stationFilterComboBox.Enabled = false;
            allClipRadioButton.Enabled = false;
            clipedRadioButton.Enabled = false;
            unclipedRadioButton.Enabled = false;
            // 番組リストをいったん選択不可にする
            channelListView.Enabled = false;
            // 放送局選択ボックスをいったん選択不可にする
            stationFilterComboBox.Enabled = false;

            #endregion

            #region 削除処理

            // 選択された番組のリスト
            ArrayList alSelectedLocalChannels = new ArrayList();

            // 選択された番組でクリップ済みの番組のリストを作る
            foreach (IChannel channel in StationList.GetChannelsOfCurrentStationFromLocalHeadline())
            {
                if (channel.Check == true)
                {
                    alSelectedLocalChannels.Add(channel);
                }
            }

            // 選択された番組がある場合
            if (alSelectedLocalChannels.Count != 0)
            {
                foreach (IChannel channel in (IChannel[])alSelectedLocalChannels.ToArray(typeof(IChannel)))
                {
                    try
                    {
                        // ヘッドラインから自分を削除
                        channel.DeleteClipFile();
                    }
                    catch (FileNotFoundException)
                    {
                        ;
                    }
                }
            }

            foreach (Station station in StationList.GetStationList())
            {
                string rssFileName = UserSetting.PodcastClipDirectoryPath + @"\"
                    + station.LocalHeadline.GetId() + @"\rss.xml";

                station.LocalHeadline.GenerateRss(rssFileName);
            }

            #endregion

            #region UI後処理

            // 番組リストを選択可能に回復する
            channelListView.Enabled = true;
            // 放送局フィルターとクリップフィルターを選択可能に回復する
            stationFilterComboBox.Enabled = true;
            allClipRadioButton.Enabled = true;
            clipedRadioButton.Enabled = true;
            unclipedRadioButton.Enabled = true;
            // Updateボタン、Clipボタン、Playボタンをを選択可能に回復する
            updateButton.Enabled = true;
            clipButton.Enabled = true;
            playButton.Enabled = true;
            // 放送局選択ボックスを選択可能に回復する
            stationFilterComboBox.Enabled = true;

            #endregion
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // 起動時の初期化
                PodcasCoSpecificProcess.StartUpInitialize();

                AddStationFilterComboBoxItem();

                // 起動直後には放送局切り替えボックスで"All"を選択する
                stationFilterComboBox.SelectedIndex = 0;

                // 番組リストがある場合
                if (StationList.GetChannelsOfCurrentStationFromLocalHeadline().Length > 0)
                {
                    // ローカルヘッドラインのみの番組表をチェックする
                    CheckHeadlines(CheckHeadlinesOption.LocalOnly);

                    UpdateChannelList();
                }
            }
            catch (XmlException)
            {
                MessageBox.Show("設定ファイルが読み込めませんでした", "設定ファイルの解析エラー");
            }
            catch (IOException)
            {
                MessageBox.Show("設定ファイルが読み込めませんでした", "設定ファイルの読み込みエラー");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("設定ファイルが読み込めませんでした", "設定ファイルの読み込みエラー");
            }

            FixWindowSize();
        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                // 終了時処理
                PodcasCoSpecificProcess.ExitDisable();
            }
            catch (IOException)
            {
                MessageBox.Show("設定ファイルが書き込めませんでした", "設定ファイル書き込みエラー");
            }
        }

        private void VersionInfoMenuItem_Click(object sender, EventArgs e)
        {
            VersionInfoForm versionInfoForm = new VersionInfoForm();
            versionInfoForm.ShowDialog();
            versionInfoForm.Dispose();
        }

        private void PodcasCoSettingMenuItem_Click(object sender, EventArgs e)
        {
            SettingForm settingForm = new SettingForm();
            settingForm.ShowDialog();
            settingForm.Dispose();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StationsSettingMenuItem_Click(object sender, EventArgs e)
        {
            StationsSettingForm stationSettingForm = new StationsSettingForm();
            stationSettingForm.ShowDialog();
            stationSettingForm.Dispose();
        }

        private void StationFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateChannelList();
        }

        private void ClipRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            /*
             * 自分がtrueになった場合のみUpdateChannelList()を実行
             * 自分がfalseの場合は他のラジオボタンのSelectedIndexChanged()でUpdateChannelList()を実行する（はず）
             */
            if (((RadioButton)sender).Checked == true)
            {
                UpdateChannelList();
            }
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            AddStationFilterComboBoxItem();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            CheckHeadlines(CheckHeadlinesOption.All);
        }

        private void StationListView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            StationListViewSelectChanged();
        }

        private void SelectAllMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in channelListView.Items)
            {
                item.Checked = true;
            }
            StationListViewSelectChanged();
        }

        private void SelectUnclipedMenuItem_Click(object sender, EventArgs e)
        {
            for (int count = 0; count < channelListView.Items.Count; ++count)
            {
                ((ListViewItem)channelListView.Items[count]).Checked = !currentChannels[count].IsLocal();
            }
            StationListViewSelectChanged();
        }

        private void SelectClipedMenuItem_Click(object sender, EventArgs e)
        {
            for (int count = 0; count < channelListView.Items.Count; ++count)
            {
                ((ListViewItem)channelListView.Items[count]).Checked = currentChannels[count].IsLocal();
            }
            StationListViewSelectChanged();
        }

        private void ClipSelectedPodcastMenuItem_Click(object sender, EventArgs e)
        {
            ClipPodcast();

            // 番組リストを更新する
            UpdateChannelList();
        }

        private void DeleteSelectedPodcastMenuItem_Click(object sender, EventArgs e)
        {
            DeletePodcast();

            // 番組リストを更新する
            UpdateChannelList();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            PlayPodcast();
        }

        private void ClipButton_Click(object sender, EventArgs e)
        {
            ClipPodcast();

            // 番組リストを更新する
            UpdateChannelList();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            FixWindowSize();
        }
    }
}

