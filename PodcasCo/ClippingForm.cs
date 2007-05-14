#region ディレクティブを使用する

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Sockets;
using MiscPocketCompactLibrary.Net;
using MiscPocketCompactLibrary.Windows.Forms;

#endregion

namespace PodcasCo
{
    public class ClippingForm : System.Windows.Forms.Form
    {
        private ProgressBar progress1ProgressBar;
        private ProgressBar progress2ProgressBar;
        private Label progress1Label;
        private Label progress2Label;

        /// <summary>
        /// フォームのメイン メニュー
        /// </summary>
        private System.Windows.Forms.MainMenu mainMenu;

        /// <summary>
        /// プログレスバー1の最大値
        /// </summary>
        private int progress1Maximum;
        private Label clippingNowLabel;

        /// <summary>
        /// プログレスバー2の最大値
        /// </summary>
        private int progress2Maximum;

        /// <summary>
        /// アンカーコントロールのリスト
        /// </summary>
        private ArrayList anchorControlList = new ArrayList();

        /// <summary>
        /// ファイル取得前イベントのハンドラ
        /// </summary>
        /// <param name="sender">イベントを発信したオブジェクト</param>
        /// <param name="e">イベント</param>
        public delegate void FileEventHandler(object sender, FileEventArgs e);

        /// <summary>
        /// ファイル取得前イベント
        /// </summary>
        public event FileEventHandler File;

        /// <summary>
        /// ファイル取得前のイベントの実行
        /// </summary>
        /// <param name="e">イベント</param>
        public void OnFile(FileEventArgs e)
        {
            if (File != null)
            {
                File(this, e);
            }
        }

        /// <summary>
        /// ファイル取得中イベントのハンドラ
        /// </summary>
        /// <param name="sender">イベントを発信したオブジェクト</param>
        /// <param name="e">イベント</param>
        public delegate void FilingEventHandler(object sender, FileEventArgs e);

        /// <summary>
        /// ファイル取得中イベント
        /// </summary>
        public event FilingEventHandler Filing;

        /// <summary>
        /// ファイル取得中のイベントの実行
        /// </summary>
        public void OnFiling(FileEventArgs e)
        {
            if (Filing != null)
            {
                Filing(this, e);
            }
        }

        public ClippingForm()
        {
            InitializeComponent();
            this.Filing += new FilingEventHandler(FilingReceiver);
            this.File += new FileEventHandler(FileReceiver);
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
            this.progress1ProgressBar = new System.Windows.Forms.ProgressBar();
            this.progress2ProgressBar = new System.Windows.Forms.ProgressBar();
            this.progress1Label = new System.Windows.Forms.Label();
            this.progress2Label = new System.Windows.Forms.Label();
            this.clippingNowLabel = new System.Windows.Forms.Label();
            // 
            // progress1ProgressBar
            // 
            this.progress1ProgressBar.Location = new System.Drawing.Point(3, 98);
            this.progress1ProgressBar.Size = new System.Drawing.Size(234, 20);
            // 
            // progress2ProgressBar
            // 
            this.progress2ProgressBar.Location = new System.Drawing.Point(3, 144);
            this.progress2ProgressBar.Size = new System.Drawing.Size(234, 20);
            // 
            // progress1Label
            // 
            this.progress1Label.Location = new System.Drawing.Point(3, 75);
            this.progress1Label.Size = new System.Drawing.Size(234, 20);
            this.progress1Label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // progress2Label
            // 
            this.progress2Label.Location = new System.Drawing.Point(3, 121);
            this.progress2Label.Size = new System.Drawing.Size(234, 20);
            this.progress2Label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // clippingNowLabel
            // 
            this.clippingNowLabel.Location = new System.Drawing.Point(3, 3);
            this.clippingNowLabel.Size = new System.Drawing.Size(234, 20);
            this.clippingNowLabel.Text = "クリップ中...";
            this.clippingNowLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ClippingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.clippingNowLabel);
            this.Controls.Add(this.progress2Label);
            this.Controls.Add(this.progress1Label);
            this.Controls.Add(this.progress2ProgressBar);
            this.Controls.Add(this.progress1ProgressBar);
            this.Menu = this.mainMenu;
            this.Text = "Clipping Now";
            this.Resize += new System.EventHandler(this.ClippingForm_Resize);
            this.Load += new System.EventHandler(this.ClippingForm_Load);

        }

        #endregion

        /// <summary>
        /// コントロールにアンカーをセットする
        /// </summary>
        private void SetAnchorControl()
        {
            anchorControlList.Add(new AnchorLayout(clippingNowLabel, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, PodcasCoInfo.FormBaseWidth, PodcasCoInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(progress1Label, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, PodcasCoInfo.FormBaseWidth, PodcasCoInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(progress1ProgressBar, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, PodcasCoInfo.FormBaseWidth, PodcasCoInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(progress2Label, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, PodcasCoInfo.FormBaseWidth, PodcasCoInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(progress2ProgressBar, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, PodcasCoInfo.FormBaseWidth, PodcasCoInfo.FormBaseHight));
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

        private void SetFileProgressMinimum(int minimum)
        {
            progress1ProgressBar.Minimum = minimum;
        }

        private void SetFileProgressMaximum(int maximum)
        {
            progress1Label.Text = "0 / "
                + maximum.ToString() + " File";
            progress1ProgressBar.Maximum = maximum;
            progress1Maximum = maximum;

            // 強制的に再描画
            this.Update();
        }

        private void SetFileProgressValue(int value)
        {
            progress1Label.Text = value.ToString() + " / "
                + progress1Maximum.ToString() + " File";
            if (progress1ProgressBar.Maximum >= value)
            {
                progress1ProgressBar.Value = value;
            }

            // 強制的に再描画
            progress1Label.Update();
            progress1ProgressBar.Update();
        }

        private void FileReceiver(object sender, FileEventArgs e)
        {
            SetFileProgressMinimum(0);
            SetFileProgressMaximum((int)e.FileCount);
        }

        private void FilingReceiver(object sender, FileEventArgs e)
        {
            SetFileProgressValue((int)e.FiledCount);
        }

        private void SetClipingProgressMinimum(int minimum)
        {
            progress2ProgressBar.Minimum = (minimum >> 10); // キロバイト単位に変換
        }

        private void SetClipingProgressMaximum(int maximum)
        {
            int kbMaximum = (maximum >> 10); // キロバイト単位に変換
            progress2ProgressBar.Maximum = kbMaximum;
            progress2Maximum = kbMaximum;
        }

        private void SetClipingProgressValue(int value)
        {
            int kbValue = (value >> 10); // キロバイト単位に変換
            progress2Label.Text = kbValue.ToString() + " KB / "
                + progress2Maximum.ToString() + " KB";
            if (progress2ProgressBar.Maximum >= kbValue)
            {
                progress2ProgressBar.Value = kbValue;
            }

            // 強制的に再描画
            progress2Label.Update();
            progress2ProgressBar.Update();
        }

        public void ClipReceiver(object sender, FetchEventArgs e)
        {
            SetClipingProgressMinimum(0);
            SetClipingProgressMaximum((int)e.ContentSize);
        }

        public void ClippingReceiver(object sender, FetchEventArgs e)
        {
            SetClipingProgressValue((int)e.FetchedSize);
        }

        private void ClippingForm_Load(object sender, EventArgs e)
        {
            SetAnchorControl();
            FixWindowSize();

            // フォームを強制的に再描画
            this.Update();

            try
            {
                StationList.ClippingPodcast(this);
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("ファイルがダウンロードできませんでした", "警告");
            }
            catch (IOException)
            {
                MessageBox.Show("ファイルがダウンロードできませんでした", "警告");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("ファイルがダウンロードできませんでした", "警告");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("ファイルがダウンロードできませんでした", "警告");
            }
            catch (ClippingException ex)
            {
                MessageBox.Show("クリップできなかったPodcastがあります\n" + ex.Message, "警告");
            }

            this.Close();
        }

        private void ClippingForm_Resize(object sender, EventArgs e)
        {
            FixWindowSize();
        }
    }
}
