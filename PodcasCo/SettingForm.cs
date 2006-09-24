#region �f�B���N�e�B�u���g�p����

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MiscPocketCompactLibrary.Windows.Forms;

#endregion

namespace PodcasCo
{
    /// <summary>
    /// PocketLadio�̐ݒ�t�H�[��
    /// </summary>
    public class SettingForm : System.Windows.Forms.Form
    {
        private MainMenu mainMenu;
        private MenuItem okMenuItem;
        private ContextMenu mediaPlayeraPathContextMenu;
        private MenuItem cutMediaPlayeraPathMenuItem;
        private MenuItem copyMediaPlayeraPathMenuItem;
        private MenuItem pasteMediaPlayeraPathMenuItem;
        private ContextMenu browserPathContextMenu;
        private MenuItem cutBrowserPathMenuItem;
        private MenuItem copyBrowserPathMenuItem;
        private MenuItem pasteBrowserPathMenuItem;
        private TabPage podcasCoTabPage;
        private TextBox browserPathTextBox;
        private TextBox mediaPlayerPathTextBox;
        private Label browserPathLabel;
        private Label mediaPlayerPathLabel;
        private Label podcastClipDirectoryPathLabel;
        private TabPage networkTabPage;
        private TextBox proxyPortTextBox;
        private TextBox proxyServerTextBox;
        private Label proxyPortLabel;
        private Label proxyServerLabel;
        private ContextMenu proxyServerContextMenu;
        private MenuItem cutProxyServerMenuItem;
        private MenuItem copyProxyServerMenuItem;
        private MenuItem pasteProxyServerMenuItem;
        private ContextMenu proxyPortContextMenu;
        private MenuItem cutProxyPortMenuItem;
        private MenuItem copyProxyPortMenuItem;
        private MenuItem pasteProxyPortMenuItem;
        private CheckBox proxyUseCheckBox;
        private TextBox podcastClipDirectoryPathTextBox;
        private ContextMenu podcastClipDirectoryPathContextMenu;
        private MenuItem cutPodcastClipDirectoryPathMenuItem;
        private MenuItem copyPodcastClipDirectoryPathMenuItem;
        private MenuItem pastePodcastClipDirectoryPathMenuItem;
        private Button browserPathReferenceButton;
        private Button mediaPlayerPathReferenceButton;
        private Button podcastClipDirectoryPathReferenceButton;
        private TabControl settingTabControl;

        public SettingForm()
        {
            //
            // Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
            //
            InitializeComponent();
        }

        /// <summary>
        /// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h
        /// <summary>
        /// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
        /// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.okMenuItem = new System.Windows.Forms.MenuItem();
            this.browserPathContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutBrowserPathMenuItem = new System.Windows.Forms.MenuItem();
            this.copyBrowserPathMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteBrowserPathMenuItem = new System.Windows.Forms.MenuItem();
            this.mediaPlayeraPathContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutMediaPlayeraPathMenuItem = new System.Windows.Forms.MenuItem();
            this.copyMediaPlayeraPathMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteMediaPlayeraPathMenuItem = new System.Windows.Forms.MenuItem();
            this.podcasCoTabPage = new System.Windows.Forms.TabPage();
            this.podcastClipDirectoryPathReferenceButton = new System.Windows.Forms.Button();
            this.browserPathReferenceButton = new System.Windows.Forms.Button();
            this.mediaPlayerPathReferenceButton = new System.Windows.Forms.Button();
            this.podcastClipDirectoryPathTextBox = new System.Windows.Forms.TextBox();
            this.podcastClipDirectoryPathContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutPodcastClipDirectoryPathMenuItem = new System.Windows.Forms.MenuItem();
            this.copyPodcastClipDirectoryPathMenuItem = new System.Windows.Forms.MenuItem();
            this.pastePodcastClipDirectoryPathMenuItem = new System.Windows.Forms.MenuItem();
            this.browserPathTextBox = new System.Windows.Forms.TextBox();
            this.mediaPlayerPathTextBox = new System.Windows.Forms.TextBox();
            this.browserPathLabel = new System.Windows.Forms.Label();
            this.mediaPlayerPathLabel = new System.Windows.Forms.Label();
            this.podcastClipDirectoryPathLabel = new System.Windows.Forms.Label();
            this.settingTabControl = new System.Windows.Forms.TabControl();
            this.networkTabPage = new System.Windows.Forms.TabPage();
            this.proxyUseCheckBox = new System.Windows.Forms.CheckBox();
            this.proxyPortTextBox = new System.Windows.Forms.TextBox();
            this.proxyPortContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutProxyPortMenuItem = new System.Windows.Forms.MenuItem();
            this.copyProxyPortMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteProxyPortMenuItem = new System.Windows.Forms.MenuItem();
            this.proxyServerTextBox = new System.Windows.Forms.TextBox();
            this.proxyServerContextMenu = new System.Windows.Forms.ContextMenu();
            this.cutProxyServerMenuItem = new System.Windows.Forms.MenuItem();
            this.copyProxyServerMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteProxyServerMenuItem = new System.Windows.Forms.MenuItem();
            this.proxyPortLabel = new System.Windows.Forms.Label();
            this.proxyServerLabel = new System.Windows.Forms.Label();
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
            // browserPathContextMenu
            // 
            this.browserPathContextMenu.MenuItems.Add(this.cutBrowserPathMenuItem);
            this.browserPathContextMenu.MenuItems.Add(this.copyBrowserPathMenuItem);
            this.browserPathContextMenu.MenuItems.Add(this.pasteBrowserPathMenuItem);
            // 
            // cutBrowserPathMenuItem
            // 
            this.cutBrowserPathMenuItem.Text = "�؂���(&T)";
            this.cutBrowserPathMenuItem.Click += new System.EventHandler(this.CutBrowserPathMenuItem_Click);
            // 
            // copyBrowserPathMenuItem
            // 
            this.copyBrowserPathMenuItem.Text = "�R�s�[(&C)";
            this.copyBrowserPathMenuItem.Click += new System.EventHandler(this.CopyBrowserPathMenuItem_Click);
            // 
            // pasteBrowserPathMenuItem
            // 
            this.pasteBrowserPathMenuItem.Text = "�\��t��(&P)";
            this.pasteBrowserPathMenuItem.Click += new System.EventHandler(this.PasteBrowserPathMenuItem_Click);
            // 
            // mediaPlayeraPathContextMenu
            // 
            this.mediaPlayeraPathContextMenu.MenuItems.Add(this.cutMediaPlayeraPathMenuItem);
            this.mediaPlayeraPathContextMenu.MenuItems.Add(this.copyMediaPlayeraPathMenuItem);
            this.mediaPlayeraPathContextMenu.MenuItems.Add(this.pasteMediaPlayeraPathMenuItem);
            // 
            // cutMediaPlayeraPathMenuItem
            // 
            this.cutMediaPlayeraPathMenuItem.Text = "�؂���(&T)";
            this.cutMediaPlayeraPathMenuItem.Click += new System.EventHandler(this.CutMediaPlayeraPathMenuItem_Click);
            // 
            // copyMediaPlayeraPathMenuItem
            // 
            this.copyMediaPlayeraPathMenuItem.Text = "�R�s�[(&C)";
            this.copyMediaPlayeraPathMenuItem.Click += new System.EventHandler(this.CopyMediaPlayeraPathMenuItem_Click);
            // 
            // pasteMediaPlayeraPathMenuItem
            // 
            this.pasteMediaPlayeraPathMenuItem.Text = "�\��t��(&P)";
            this.pasteMediaPlayeraPathMenuItem.Click += new System.EventHandler(this.PasteMediaPlayeraPathMenuItem_Click);
            // 
            // podcasCoTabPage
            // 
            this.podcasCoTabPage.Controls.Add(this.podcastClipDirectoryPathReferenceButton);
            this.podcasCoTabPage.Controls.Add(this.browserPathReferenceButton);
            this.podcasCoTabPage.Controls.Add(this.mediaPlayerPathReferenceButton);
            this.podcasCoTabPage.Controls.Add(this.podcastClipDirectoryPathTextBox);
            this.podcasCoTabPage.Controls.Add(this.browserPathTextBox);
            this.podcasCoTabPage.Controls.Add(this.mediaPlayerPathTextBox);
            this.podcasCoTabPage.Controls.Add(this.browserPathLabel);
            this.podcasCoTabPage.Controls.Add(this.mediaPlayerPathLabel);
            this.podcasCoTabPage.Controls.Add(this.podcastClipDirectoryPathLabel);
            this.podcasCoTabPage.Location = new System.Drawing.Point(0, 0);
            this.podcasCoTabPage.Size = new System.Drawing.Size(240, 245);
            this.podcasCoTabPage.Text = "PodcasCo�ݒ�";
            // 
            // podcastClipDirectoryPathReferenceButton
            // 
            this.podcastClipDirectoryPathReferenceButton.Location = new System.Drawing.Point(189, 27);
            this.podcastClipDirectoryPathReferenceButton.Size = new System.Drawing.Size(48, 20);
            this.podcastClipDirectoryPathReferenceButton.Text = "�Q��";
            this.podcastClipDirectoryPathReferenceButton.Click += new System.EventHandler(this.PodcastClipDirectoryPathReferenceButton_Click);
            // 
            // browserPathReferenceButton
            // 
            this.browserPathReferenceButton.Location = new System.Drawing.Point(189, 126);
            this.browserPathReferenceButton.Size = new System.Drawing.Size(48, 20);
            this.browserPathReferenceButton.Text = "�Q��";
            this.browserPathReferenceButton.Click += new System.EventHandler(this.BrowserPathReferenceButton_Click_1);
            // 
            // mediaPlayerPathReferenceButton
            // 
            this.mediaPlayerPathReferenceButton.Location = new System.Drawing.Point(189, 83);
            this.mediaPlayerPathReferenceButton.Size = new System.Drawing.Size(48, 20);
            this.mediaPlayerPathReferenceButton.Text = "�Q��";
            this.mediaPlayerPathReferenceButton.Click += new System.EventHandler(this.MediaPlayerPathReferenceButton_Click_1);
            // 
            // podcastClipDirectoryPathTextBox
            // 
            this.podcastClipDirectoryPathTextBox.ContextMenu = this.podcastClipDirectoryPathContextMenu;
            this.podcastClipDirectoryPathTextBox.Location = new System.Drawing.Point(3, 27);
            this.podcastClipDirectoryPathTextBox.Size = new System.Drawing.Size(180, 21);
            this.podcastClipDirectoryPathTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.podcastClipDirectoryPathTextBox_KeyUp);
            this.podcastClipDirectoryPathTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.podcastClipDirectoryPathTextBox_KeyDown);
            // 
            // podcastClipDirectoryPathContextMenu
            // 
            this.podcastClipDirectoryPathContextMenu.MenuItems.Add(this.cutPodcastClipDirectoryPathMenuItem);
            this.podcastClipDirectoryPathContextMenu.MenuItems.Add(this.copyPodcastClipDirectoryPathMenuItem);
            this.podcastClipDirectoryPathContextMenu.MenuItems.Add(this.pastePodcastClipDirectoryPathMenuItem);
            // 
            // cutPodcastClipDirectoryPathMenuItem
            // 
            this.cutPodcastClipDirectoryPathMenuItem.Text = "�؂���(&T)";
            this.cutPodcastClipDirectoryPathMenuItem.Click += new System.EventHandler(this.CutPodcastClipDirectoryPathMenuItem_Click);
            // 
            // copyPodcastClipDirectoryPathMenuItem
            // 
            this.copyPodcastClipDirectoryPathMenuItem.Text = "�R�s�[(&C)";
            this.copyPodcastClipDirectoryPathMenuItem.Click += new System.EventHandler(this.CopyPodcastClipDirectoryPathMenuItem_Click);
            // 
            // pastePodcastClipDirectoryPathMenuItem
            // 
            this.pastePodcastClipDirectoryPathMenuItem.Text = "�\��t��(&P)";
            this.pastePodcastClipDirectoryPathMenuItem.Click += new System.EventHandler(this.PastePodcastClipDirectoryPathMenuItem_Click);
            // 
            // browserPathTextBox
            // 
            this.browserPathTextBox.ContextMenu = this.browserPathContextMenu;
            this.browserPathTextBox.Location = new System.Drawing.Point(3, 126);
            this.browserPathTextBox.Size = new System.Drawing.Size(180, 21);
            this.browserPathTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BrowserPathTextBox_KeyUp);
            this.browserPathTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BrowserPathTextBox_KeyDown);
            // 
            // mediaPlayerPathTextBox
            // 
            this.mediaPlayerPathTextBox.ContextMenu = this.mediaPlayeraPathContextMenu;
            this.mediaPlayerPathTextBox.Location = new System.Drawing.Point(3, 83);
            this.mediaPlayerPathTextBox.Size = new System.Drawing.Size(180, 21);
            this.mediaPlayerPathTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MediaPlayerPathTextBox_KeyUp);
            this.mediaPlayerPathTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MediaPlayerPathTextBox_KeyDown);
            // 
            // browserPathLabel
            // 
            this.browserPathLabel.Location = new System.Drawing.Point(3, 107);
            this.browserPathLabel.Size = new System.Drawing.Size(79, 16);
            this.browserPathLabel.Text = "�u���E�U�̃p�X";
            // 
            // mediaPlayerPathLabel
            // 
            this.mediaPlayerPathLabel.Location = new System.Drawing.Point(3, 64);
            this.mediaPlayerPathLabel.Size = new System.Drawing.Size(132, 16);
            this.mediaPlayerPathLabel.Text = "���f�B�A�v���[���[�̃p�X";
            // 
            // podcastClipDirectoryPathLabel
            // 
            this.podcastClipDirectoryPathLabel.Location = new System.Drawing.Point(3, 4);
            this.podcastClipDirectoryPathLabel.Size = new System.Drawing.Size(161, 20);
            this.podcastClipDirectoryPathLabel.Text = "Podcast���N���b�v����t�H���_";
            // 
            // settingTabControl
            // 
            this.settingTabControl.Controls.Add(this.podcasCoTabPage);
            this.settingTabControl.Controls.Add(this.networkTabPage);
            this.settingTabControl.Location = new System.Drawing.Point(0, 0);
            this.settingTabControl.SelectedIndex = 0;
            this.settingTabControl.Size = new System.Drawing.Size(240, 268);
            // 
            // networkTabPage
            // 
            this.networkTabPage.Controls.Add(this.proxyUseCheckBox);
            this.networkTabPage.Controls.Add(this.proxyPortTextBox);
            this.networkTabPage.Controls.Add(this.proxyServerTextBox);
            this.networkTabPage.Controls.Add(this.proxyPortLabel);
            this.networkTabPage.Controls.Add(this.proxyServerLabel);
            this.networkTabPage.Location = new System.Drawing.Point(0, 0);
            this.networkTabPage.Size = new System.Drawing.Size(232, 242);
            this.networkTabPage.Text = "�l�b�g���[�N�ݒ�";
            // 
            // proxyUseCheckBox
            // 
            this.proxyUseCheckBox.Location = new System.Drawing.Point(3, 3);
            this.proxyUseCheckBox.Size = new System.Drawing.Size(135, 20);
            this.proxyUseCheckBox.Text = "�v���L�V���g�p����";
            // 
            // proxyPortTextBox
            // 
            this.proxyPortTextBox.ContextMenu = this.proxyPortContextMenu;
            this.proxyPortTextBox.Location = new System.Drawing.Point(3, 88);
            this.proxyPortTextBox.Size = new System.Drawing.Size(74, 21);
            this.proxyPortTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ProxyPortTextBox_KeyUp);
            this.proxyPortTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProxyPortTextBox_KeyDown);
            // 
            // proxyPortContextMenu
            // 
            this.proxyPortContextMenu.MenuItems.Add(this.cutProxyPortMenuItem);
            this.proxyPortContextMenu.MenuItems.Add(this.copyProxyPortMenuItem);
            this.proxyPortContextMenu.MenuItems.Add(this.pasteProxyPortMenuItem);
            // 
            // cutProxyPortMenuItem
            // 
            this.cutProxyPortMenuItem.Text = "�؂���(&T)";
            this.cutProxyPortMenuItem.Click += new System.EventHandler(this.CutProxyPortMenuItem_Click);
            // 
            // copyProxyPortMenuItem
            // 
            this.copyProxyPortMenuItem.Text = "�R�s�[(&C)";
            this.copyProxyPortMenuItem.Click += new System.EventHandler(this.CopyProxyPortMenuItem_Click);
            // 
            // pasteProxyPortMenuItem
            // 
            this.pasteProxyPortMenuItem.Text = "�\��t��(&P)";
            this.pasteProxyPortMenuItem.Click += new System.EventHandler(this.PasteProxyPortMenuItem_Click);
            // 
            // proxyServerTextBox
            // 
            this.proxyServerTextBox.ContextMenu = this.proxyServerContextMenu;
            this.proxyServerTextBox.Location = new System.Drawing.Point(3, 45);
            this.proxyServerTextBox.Size = new System.Drawing.Size(234, 21);
            this.proxyServerTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ProxyServerTextBox_KeyUp);
            this.proxyServerTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProxyServerTextBox_KeyDown);
            // 
            // proxyServerContextMenu
            // 
            this.proxyServerContextMenu.MenuItems.Add(this.cutProxyServerMenuItem);
            this.proxyServerContextMenu.MenuItems.Add(this.copyProxyServerMenuItem);
            this.proxyServerContextMenu.MenuItems.Add(this.pasteProxyServerMenuItem);
            // 
            // cutProxyServerMenuItem
            // 
            this.cutProxyServerMenuItem.Text = "�؂���(&T)";
            this.cutProxyServerMenuItem.Click += new System.EventHandler(this.CutProxyServerMenuItem_Click);
            // 
            // copyProxyServerMenuItem
            // 
            this.copyProxyServerMenuItem.Text = "�R�s�[(&C)";
            this.copyProxyServerMenuItem.Click += new System.EventHandler(this.CopyProxyServerMenuItem_Click);
            // 
            // pasteProxyServerMenuItem
            // 
            this.pasteProxyServerMenuItem.Text = "�\��t��(&P)";
            this.pasteProxyServerMenuItem.Click += new System.EventHandler(this.PasteProxyServerMenuItem_Click);
            // 
            // proxyPortLabel
            // 
            this.proxyPortLabel.Location = new System.Drawing.Point(3, 69);
            this.proxyPortLabel.Size = new System.Drawing.Size(192, 16);
            this.proxyPortLabel.Text = "�v���L�V�̃|�[�g�ԍ� �i��F 8080�j";
            // 
            // proxyServerLabel
            // 
            this.proxyServerLabel.Location = new System.Drawing.Point(3, 26);
            this.proxyServerLabel.Size = new System.Drawing.Size(230, 16);
            this.proxyServerLabel.Text = "�v���L�V�T�[�o �i��Fproxy.example.com�j";
            // 
            // SettingForm
            // 
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.settingTabControl);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Text = "�ݒ�";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SettingForm_Closing);
            this.Load += new System.EventHandler(this.SettingForm_Load);

        }
        #endregion

        private void SettingForm_Load(object sender, System.EventArgs e)
        {
            // �ݒ�̓ǂݍ���
            podcastClipDirectoryPathTextBox.Text = UserSetting.PodcastClipDirectoryPath;
            mediaPlayerPathTextBox.Text = UserSetting.MediaPlayerPath;
            browserPathTextBox.Text = UserSetting.BrowserPath;
            proxyUseCheckBox.Checked = UserSetting.ProxyUse;
            proxyServerTextBox.Text = UserSetting.ProxyServer;
            proxyPortTextBox.Text = UserSetting.ProxyPort.ToString();
        }

        private void SettingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // �ݒ�̏�������
            UserSetting.PodcastClipDirectoryPath = podcastClipDirectoryPathTextBox.Text.Trim();
            UserSetting.MediaPlayerPath = mediaPlayerPathTextBox.Text.Trim();
            UserSetting.BrowserPath = browserPathTextBox.Text.Trim();
            UserSetting.ProxyUse = proxyUseCheckBox.Checked;
            UserSetting.ProxyServer = proxyServerTextBox.Text.Trim();
            UserSetting.ProxyPort = int.Parse(proxyPortTextBox.Text.Trim());

            try
            {
                UserSetting.SaveSetting();
            }
            catch (IOException)
            {
                MessageBox.Show("�ݒ�t�@�C�����������߂܂���ł���", "�ݒ�t�@�C���������݃G���[");
            }

            if(podcastClipDirectoryPathTextBox.Modified == true){
                // �uPodcast���N���b�v����t�H���_�v�Ƀ��[�J���w�b�h���C�����i�[����邽�߂ɁA
                // �uPodcast���N���b�v����t�H���_�v�̏ꏊ���ς�����̂ŁA���[�J���w�b�h���C����
                // URL����蒼��
                foreach (Station station in StationList.GetStationList())
                {
                    // ���[�J���w�b�h���C����URL�̍�蒼��
                    station.LocalHeadline.SetUrl(
                        new Uri(UserSetting.PodcastClipDirectoryPath + @"\" 
                        + station.LocalHeadline.GetId() + @"\" + PodcasCoInfo.LocalRssFile));
                    // ���[�J���w�b�h���C���̔ԑg��j������
                    station.LocalHeadline.ClearChannels();
                    // ���[�J���w�b�h���C���̓��e��������������̂Őݒ��ۑ�����
                    station.LocalHeadline.SaveSetting();
                }
            }
        }

        private void OkMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                // �v���L�V�T�[�o�ݒ�E�v���L�V�|�[�g�ݒ�̂ǂ��炩�ɉ��������͂���Ă���ꍇ���A�v���L�V�|�[�g�̐ݒ肪�s���ȏꍇ
                if ((proxyServerTextBox.Text.Trim().Length != 0 || proxyPortTextBox.Text.Trim().Length != 0)
                    && (int.Parse(proxyPortTextBox.Text) < 0x00 || int.Parse(proxyPortTextBox.Text) > 0xFFFF))
                {
                    MessageBox.Show("�v���L�V�̃|�[�g�ԍ���0�`65535�Őݒ肵�Ă�������");
                }
                else
                {
                    this.Close();
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("�v���L�V�̃|�[�g�ԍ���0�`65535�Őݒ肵�Ă�������");
            }
            catch (FormatException)
            {
                MessageBox.Show("�v���L�V�̃|�[�g�ԍ���0�`65535�Őݒ肵�Ă�������");
            }
            catch (OverflowException)
            {
                MessageBox.Show("�v���L�V�̃|�[�g�ԍ���0�`65535�Őݒ肵�Ă�������");
            }
        }

        private void CutMediaPlayeraPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(mediaPlayerPathTextBox);
        }

        private void CopyMediaPlayeraPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(mediaPlayerPathTextBox);
        }

        private void PasteMediaPlayeraPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(mediaPlayerPathTextBox);
        }

        private void CutBrowserPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(browserPathTextBox);
        }

        private void CopyBrowserPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(browserPathTextBox);
        }

        private void PasteBrowserPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(browserPathTextBox);
        }

        private void CutProxyServerMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(proxyServerTextBox);
        }

        private void CopyProxyServerMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(proxyServerTextBox);
        }

        private void PasteProxyServerMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(proxyServerTextBox);
        }

        private void CutProxyPortMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(proxyPortTextBox);
        }

        private void CopyProxyPortMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(proxyPortTextBox);
        }

        private void PasteProxyPortMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(proxyPortTextBox);
        }

        private void CutPodcastClipDirectoryPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Cut(podcastClipDirectoryPathTextBox);
        }

        private void CopyPodcastClipDirectoryPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Copy(podcastClipDirectoryPathTextBox);
        }

        private void PastePodcastClipDirectoryPathMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardTextBox.Paste(podcastClipDirectoryPathTextBox);
        }

        private void MediaPlayerPathReferenceButton_Click_1(object sender, EventArgs e)
        {
            SmartPDA.Windows.Forms.OpenFileDialog fd = SmartPDA.Windows.Forms.FileDialogFactory.MakeOpenFileDialog();

            if (Directory.Exists(Path.GetDirectoryName(mediaPlayerPathTextBox.Text.Trim())))
            {
                fd.InitialDirectory = Path.GetDirectoryName(mediaPlayerPathTextBox.Text.Trim());
            }
            fd.Filter = "*.exe|*.exe|*.*|*.*";
            fd.IconVisible = true;
            fd.Activation = ItemActivation.OneClick;

            if (fd.ShowDialog() == DialogResult.OK)
            {
                mediaPlayerPathTextBox.Text = fd.FileName;
            }
        }

        private void BrowserPathReferenceButton_Click_1(object sender, EventArgs e)
        {
            SmartPDA.Windows.Forms.OpenFileDialog fd = SmartPDA.Windows.Forms.FileDialogFactory.MakeOpenFileDialog();

            if (Directory.Exists(Path.GetDirectoryName(browserPathTextBox.Text.Trim())))
            {
                fd.InitialDirectory = Path.GetDirectoryName(browserPathTextBox.Text.Trim());
            }
            fd.Filter = "*.exe|*.exe|*.*|*.*";
            fd.IconVisible = true;
            fd.Activation = ItemActivation.OneClick;

            if (fd.ShowDialog() == DialogResult.OK)
            {
                browserPathTextBox.Text = fd.FileName;
            }
        }

        private void PodcastClipDirectoryPathReferenceButton_Click(object sender, EventArgs e)
        {
            SmartPDA.Windows.Forms.FolderBrowserDialog fd = SmartPDA.Windows.Forms.FolderBrowserDialogFactory.MakeFolderBrowserDialog();

            if (Directory.Exists(podcastClipDirectoryPathTextBox.Text.Trim()))
            {
                fd.SelectedPath = podcastClipDirectoryPathTextBox.Text.Trim();
            }
            fd.IconVisible = true;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                podcastClipDirectoryPathTextBox.Text = fd.SelectedPath;
            }

        }

        private void podcastClipDirectoryPathTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // �؂���V���[�g�J�b�g
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(podcastClipDirectoryPathTextBox);
            }
            // �\��t���V���[�g�J�b�g
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(podcastClipDirectoryPathTextBox);
            }
        }

        private void podcastClipDirectoryPathTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // �R�s�[�V���[�g�J�b�g
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(podcastClipDirectoryPathTextBox);
            }
        }

        private void MediaPlayerPathTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // �؂���V���[�g�J�b�g
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(mediaPlayerPathTextBox);
            }
            // �\��t���V���[�g�J�b�g
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(mediaPlayerPathTextBox);
            }
        }

        private void MediaPlayerPathTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // �R�s�[�V���[�g�J�b�g
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(mediaPlayerPathTextBox);
            }
        }

        private void BrowserPathTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // �؂���V���[�g�J�b�g
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(browserPathTextBox);
            }
            // �\��t���V���[�g�J�b�g
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(browserPathTextBox);
            }
        }

        private void BrowserPathTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // �R�s�[�V���[�g�J�b�g
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(browserPathTextBox);
            }
        }

        private void ProxyServerTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // �؂���V���[�g�J�b�g
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(proxyServerTextBox);
            }
            // �\��t���V���[�g�J�b�g
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(proxyServerTextBox);
            }
        }

        private void ProxyServerTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // �R�s�[�V���[�g�J�b�g
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(proxyServerTextBox);
            }
        }

        private void ProxyPortTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // �؂���V���[�g�J�b�g
            if (e.KeyCode == Keys.X && e.Control)
            {
                ClipboardTextBox.Cut(proxyPortTextBox);
            }
            // �\��t���V���[�g�J�b�g
            else if (e.KeyCode == Keys.V && e.Control)
            {
                ClipboardTextBox.Paste(proxyPortTextBox);
            }
        }

        private void ProxyPortTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // �R�s�[�V���[�g�J�b�g
            if (e.KeyCode == Keys.C && e.Control)
            {
                ClipboardTextBox.Copy(proxyPortTextBox);
            }
        }
    }
}
