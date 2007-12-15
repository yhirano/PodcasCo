﻿#region ディレクティブを使用する

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;

#endregion

namespace PodcasCo
{
    /// <summary>
    /// StationStartupSettingForm の概要の説明です。
    /// </summary>
    public class StationStartupSettingForm : System.Windows.Forms.Form
    {
        private MenuItem okMenuItem;
        private ComboBox stationListComboBox;
        /// <summary>
        /// フォームのメイン メニューです。
        /// </summary>
        private System.Windows.Forms.MainMenu mainMenu;
        private CheckBox startupDownloadCheckBox;
        private CheckBox startupDeleteCheckBox;
        private Label downloadNumLabel;
        private Label deleteDayLabel;
        private NumericUpDown downloadNumNumericUpDown;
        private NumericUpDown deleteDayNumericUpDown;
        private Label startupDeleteDiscriptionLabel;

        /// <summary>
        /// 放送局の設定の保持
        /// </summary>
        private StationStartupSetting[] stationStartupSettings;

        public StationStartupSettingForm()
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
            this.stationListComboBox = new System.Windows.Forms.ComboBox();
            this.startupDownloadCheckBox = new System.Windows.Forms.CheckBox();
            this.startupDeleteCheckBox = new System.Windows.Forms.CheckBox();
            this.downloadNumLabel = new System.Windows.Forms.Label();
            this.deleteDayLabel = new System.Windows.Forms.Label();
            this.downloadNumNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.deleteDayNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.startupDeleteDiscriptionLabel = new System.Windows.Forms.Label();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.okMenuItem);
            // 
            // okMenuItem
            // 
            this.okMenuItem.Text = "&OK";
            this.okMenuItem.Click += new System.EventHandler(this.okMenuItem_Click);
            // 
            // stationListComboBox
            // 
            this.stationListComboBox.Location = new System.Drawing.Point(3, 3);
            this.stationListComboBox.Size = new System.Drawing.Size(234, 22);
            this.stationListComboBox.SelectedIndexChanged += new System.EventHandler(this.stationListComboBox_SelectedIndexChanged);
            // 
            // startupDownloadCheckBox
            // 
            this.startupDownloadCheckBox.Location = new System.Drawing.Point(3, 45);
            this.startupDownloadCheckBox.Size = new System.Drawing.Size(210, 20);
            this.startupDownloadCheckBox.Text = "起動時に自動でダウンロードする";
            this.startupDownloadCheckBox.CheckStateChanged += new System.EventHandler(this.startupDownloadCheckBox_CheckStateChanged);
            // 
            // startupDeleteCheckBox
            // 
            this.startupDeleteCheckBox.Location = new System.Drawing.Point(3, 98);
            this.startupDeleteCheckBox.Size = new System.Drawing.Size(172, 20);
            this.startupDeleteCheckBox.Text = "起動時に自動で削除する";
            this.startupDeleteCheckBox.CheckStateChanged += new System.EventHandler(this.startupDeleteCheckBox_CheckStateChanged);
            // 
            // downloadNumLabel
            // 
            this.downloadNumLabel.Location = new System.Drawing.Point(75, 74);
            this.downloadNumLabel.Size = new System.Drawing.Size(100, 20);
            this.downloadNumLabel.Text = "件ダウンロード";
            // 
            // deleteDayLabel
            // 
            this.deleteDayLabel.Location = new System.Drawing.Point(75, 128);
            this.deleteDayLabel.Size = new System.Drawing.Size(138, 20);
            this.deleteDayLabel.Text = "日前までの放送を残す";
            // 
            // downloadNumNumericUpDown
            // 
            this.downloadNumNumericUpDown.Location = new System.Drawing.Point(21, 70);
            this.downloadNumNumericUpDown.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.downloadNumNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.downloadNumNumericUpDown.Size = new System.Drawing.Size(48, 22);
            this.downloadNumNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.downloadNumNumericUpDown.ValueChanged += new System.EventHandler(this.downloadNumNumericUpDown_ValueChanged);
            // 
            // deleteDayNumericUpDown
            // 
            this.deleteDayNumericUpDown.Location = new System.Drawing.Point(21, 124);
            this.deleteDayNumericUpDown.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.deleteDayNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.deleteDayNumericUpDown.Size = new System.Drawing.Size(48, 22);
            this.deleteDayNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.deleteDayNumericUpDown.ValueChanged += new System.EventHandler(this.deletDayNumericUpDown_ValueChanged);
            // 
            // startupDeleteDiscriptionLabel
            // 
            this.startupDeleteDiscriptionLabel.Location = new System.Drawing.Point(21, 154);
            this.startupDeleteDiscriptionLabel.Size = new System.Drawing.Size(216, 40);
            this.startupDeleteDiscriptionLabel.Text = "Discription";
            // 
            // StationStartupSettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.startupDeleteDiscriptionLabel);
            this.Controls.Add(this.deleteDayNumericUpDown);
            this.Controls.Add(this.downloadNumNumericUpDown);
            this.Controls.Add(this.deleteDayLabel);
            this.Controls.Add(this.downloadNumLabel);
            this.Controls.Add(this.startupDeleteCheckBox);
            this.Controls.Add(this.startupDownloadCheckBox);
            this.Controls.Add(this.stationListComboBox);
            this.Menu = this.mainMenu;
            this.Text = "起動時の設定";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.StationStartupSettingForm_Closing);
            this.Load += new System.EventHandler(this.StationStartupSettingForm_Load);

        }

        #endregion

        private void StationStartupSettingForm_Load(object sender, EventArgs e)
        {
            downloadNumNumericUpDown.Minimum = Station.STARTUP_DOWNLOAD_NUM_MIN;
            deleteDayNumericUpDown.Minimum = Station.STARTUP_DELETE_DAY_MIN;

            Station[] stations = StationList.GetStationList();
            stationStartupSettings = new StationStartupSetting[stations.Length];
            for (int i = 0; i < stations.Length; ++i)
            {
                stationStartupSettings[i] = new StationStartupSetting(stations[i]);
            }

            foreach (Station station in StationList.GetStationList())
            {
                stationListComboBox.Items.Add(station.Name);
            }

            if (stationListComboBox.Items.Count > 0)
            {
                stationListComboBox.SelectedIndex = 0;
            }
            else
            {
                stationListComboBox.Enabled = false;
                startupDownloadCheckBox.Enabled = false;
                downloadNumNumericUpDown.Enabled = false;
                startupDeleteCheckBox.Enabled = false;
                deleteDayNumericUpDown.Enabled = false;
            }
        }

        private void okMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void stationListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            startupDownloadCheckBox.Checked = stationStartupSettings[stationListComboBox.SelectedIndex].StartupDownload;
            if (startupDownloadCheckBox.Checked == true)
            {
                downloadNumNumericUpDown.Enabled = true;
            }
            else
            {
                downloadNumNumericUpDown.Enabled = false;
            }
            downloadNumNumericUpDown.Value = stationStartupSettings[stationListComboBox.SelectedIndex].StartupDownloadNum;
            startupDeleteCheckBox.Checked = stationStartupSettings[stationListComboBox.SelectedIndex].StartupDelete;
            if (startupDeleteCheckBox.Checked == true)
            {
                deleteDayNumericUpDown.Enabled = true;
            }
            else
            {
                deleteDayNumericUpDown.Enabled = false;
            }
            deleteDayNumericUpDown.Value = stationStartupSettings[stationListComboBox.SelectedIndex].StartupDeleteDay;
            WriteStartupDeleteDescription((int)deleteDayNumericUpDown.Value);
        }

        private void startupDownloadCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (startupDownloadCheckBox.Checked == true)
            {
                downloadNumNumericUpDown.Enabled = true;
            }
            else
            {
                downloadNumNumericUpDown.Enabled = false;
            }

            stationStartupSettings[stationListComboBox.SelectedIndex].StartupDownload = startupDownloadCheckBox.Checked;
        }

        private void startupDeleteCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (startupDeleteCheckBox.Checked == true)
            {
                deleteDayNumericUpDown.Enabled = true;
                startupDeleteDiscriptionLabel.Enabled = true;
            }
            else
            {
                deleteDayNumericUpDown.Enabled = false;
                startupDeleteDiscriptionLabel.Enabled = false;
            }

            stationStartupSettings[stationListComboBox.SelectedIndex].StartupDelete = startupDeleteCheckBox.Checked;
        }

        private void downloadNumNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            stationStartupSettings[stationListComboBox.SelectedIndex].StartupDownloadNum = (int)downloadNumNumericUpDown.Value;
        }

        private void deletDayNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            stationStartupSettings[stationListComboBox.SelectedIndex].StartupDeleteDay = (int)deleteDayNumericUpDown.Value;
            WriteStartupDeleteDescription((int)deleteDayNumericUpDown.Value);
        }

        private void WriteStartupDeleteDescription(int deleteDay)
        {
            string description = "今日が{0}月{1}日の場合、{2}月{3}日0:00のまでの放送を残します。";
            DateTime today = DateTime.Today;
            DateTime delete = today.Subtract(new TimeSpan((int)deleteDay, 0, 0, 0));
            startupDeleteDiscriptionLabel.Text = string.Format(description, today.Month, today.Day, delete.Month, delete.Day);
        }

        private void StationStartupSettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 設定の書き込み
            foreach (StationStartupSetting stationStartupSetting in stationStartupSettings)
            {
                stationStartupSetting.SetStationSetting();
            }
        }

        /// <summary>
        /// 放送局の設定の仮保持クラス
        /// </summary>
        private class StationStartupSetting
        {
            private Station station = null;

            public Station Station
            {
                get { return station; }
            }

            private bool startupDownload = false;

            public bool StartupDownload
            {
                get { return startupDownload; }
                set { startupDownload = value; }
            }

            private int startupDownloadNum = Station.STARTUP_DOWNLOAD_NUM_MIN;

            public int StartupDownloadNum
            {
                get { return startupDownloadNum; }
                set
                {
                    if (value >= Station.STARTUP_DOWNLOAD_NUM_MIN)
                    {
                        startupDownloadNum = value;
                    }
                    else
                    {
                        startupDownloadNum = Station.STARTUP_DOWNLOAD_NUM_MIN;
                    }
                }
            }

            private bool startupDelete = false;

            public bool StartupDelete
            {
                get { return startupDelete; }
                set { startupDelete = value; }
            }

            private int startupDeleteDay = Station.STARTUP_DELETE_DAY_MIN;

            public int StartupDeleteDay
            {
                get { return startupDeleteDay; }
                set
                {
                    if (value >= Station.STARTUP_DELETE_DAY_MIN)
                    {
                        startupDeleteDay = value;
                    }
                    else
                    {
                        startupDeleteDay = Station.STARTUP_DELETE_DAY_MIN;
                    }
                }
            }

            public StationStartupSetting(Station station)
            {
                this.station = station;
                this.startupDownload = station.StartupDownload;
                this.startupDownloadNum = station.StartupDownloadNum;
                this.startupDelete = station.StartupDelete;
                this.startupDeleteDay = station.StartupDeleteDay;
            }

            public void SetStationSetting()
            {
                station.StartupDownload = this.startupDownload;
                station.StartupDownloadNum = this.startupDownloadNum;
                station.StartupDelete = this.startupDelete;
                station.StartupDeleteDay = this.startupDeleteDay;
            }
        }
    }
}
