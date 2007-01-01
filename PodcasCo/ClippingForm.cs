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

#endregion

namespace PodcasCo
{
    /// <summary>
    /// ClipingForm の概要の説明です。
    /// </summary>
    public class ClippingForm : System.Windows.Forms.Form
    {
        private ProgressBar progress1ProgressBar;
        private ProgressBar progress2ProgressBar;
        private Label progress1Label;
        private Label progress2Label;
        /// <summary>
        /// フォームのメイン メニューです。
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

        public ClippingForm()
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
            this.Load += new System.EventHandler(this.ClippingForm_Load);
        }

        #endregion

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
            progress1ProgressBar.Value = value;

            // 強制的に再描画
            this.Update();
        }

        private void SetClipingProgressMinimum(int minimum)
        {
            progress2ProgressBar.Minimum = minimum;
        }

        private void SetClipingProgressMaximum(int maximum)
        {
            progress2ProgressBar.Maximum = maximum;
            progress2Maximum = maximum;
        }

        private void SetClipingProgressValue(int value)
        {
            progress2Label.Text = (value / 1024).ToString() + " KB / "
                + (progress2Maximum / 1024).ToString() + " KB";
            progress2ProgressBar.Value = value;

            // 強制的に再描画
            this.Update();
        }

        private void ClippingForm_Load(object sender, EventArgs e)
        {
            // フォームを強制的に再描画
            this.Update();

            try
            {
                StationList.ClippingPodcast(
                    new StationList.SetDownloadProgressMinimumInvoker(this.SetFileProgressMinimum),
                    new StationList.SetDownloadProgressMaximumInvoker(this.SetFileProgressMaximum),
                    new StationList.SetDownloadProgressValueInvoker(this.SetFileProgressValue),
                    new WebStream.SetDownloadProgressMinimumInvoker(this.SetClipingProgressMinimum),
                    new WebStream.SetDownloadProgressMaximumInvoker(this.SetClipingProgressMaximum),
                    new WebStream.SetDownloadProgressValueInvoker(this.SetClipingProgressValue));
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
            catch (ClippingException ex) {
                MessageBox.Show("クリップできなかったPodcastがあります\n" + ex.Message, "警告");
            }

            this.Close();
        }
    }
}
