#region �f�B���N�e�B�u���g�p����

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
    /// �ԑg�̏ڍ׏��\���t�H�[��
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
        /// �`�����l��
        /// </summary>
        private Channel channel;

        /// <summary>
        /// �A���J�[�R���g���[���̃��X�g
        /// </summary>
        private ArrayList anchorControlList = new ArrayList();

        public ChannelPropertyForm(Channel channel)
        {
            //
            // Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
            //
            InitializeComponent();

            this.channel = channel;
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
            this.infomationColumnHeader.Text = "���";
            this.infomationColumnHeader.Width = 60;
            // 
            // discriptionColumnHeader
            // 
            this.discriptionColumnHeader.Text = "�ڍ�";
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
            this.Text = "�ԑg�̏ڍ�";
            this.Resize += new System.EventHandler(this.ChannelPropertyForm_Resize);
            this.Load += new System.EventHandler(this.ChannelPropertyForm_Load);

        }
        #endregion

        /// <summary>
        /// �R���g���[���ɃA���J�[���Z�b�g����
        /// </summary>
        private void SetAnchorControl()
        {
            anchorControlList.Add(new AnchorLayout(propertyListView, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom, PodcasCoInfo.FormBaseWidth, PodcasCoInfo.FormBaseHight));
            anchorControlList.Add(new AnchorLayout(accessButton, AnchorStyles.Right | AnchorStyles.Bottom, PodcasCoInfo.FormBaseWidth, PodcasCoInfo.FormBaseHight));
        }

        /// <summary>
        /// �t�H�[���̃T�C�Y�ύX���Ƀt�H�[�����̒��g�̃T�C�Y��K���ɕύX����
        /// </summary>
        private void FixWindowSize()
        {
            foreach (AnchorLayout anchorLayout in anchorControlList)
            {
                anchorLayout.LayoutControl();
            }
        }

        /// <summary>
        /// ListView�̃R�����T�C�Y�������Œ�������
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

            string[] titleProperty = { "�ԑg��", channel.Title.Trim() };
            string[] descriptionProperty = { "�ڍ�", channel.Description.Trim() };
            string[] linkProperty = { "�����N", ((channel.GetWebsiteUrl() != null) ? channel.GetWebsiteUrl().ToString().Trim() : "") };
            string[] authorProperty = { "����", channel.Date.ToString().Trim() };
            string[] dateProperty = { "�z�M����", channel.Date.ToString().Trim() };
            string[] lengthProperty = { "����", channel.Length.Trim() };
            string[] typeProperty = { "�^�C�v", channel.Type.Trim() };

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
                MessageBox.Show("�u���E�U��������܂���", "�x��");
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
