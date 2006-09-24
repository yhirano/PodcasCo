#region �f�B���N�e�B�u���g�p����

using System;

#endregion

namespace PodcasCo
{
    /// <summary>
    /// PocketLadio�̌ŗL�����L�q���Ă���N���X
    /// </summary>
    public sealed class PodcasCoInfo
    {
        #region �A�v���P�[�V�����ŗL�̏��

        /// <summary>
        /// �A�v���P�[�V������
        /// </summary>
        private const string APPLICATION_NAME = "PodcasCo";

        /// <summary>
        /// �A�v���P�[�V������
        /// </summary>
        public static string ApplicationName
        {
            get { return APPLICATION_NAME; }
        }

        /// <summary>
        /// �A�v���P�[�V�����̃o�[�W����
        /// </summary>
        private const string VERSION_NUMBER = "0.2";

        /// <summary>
        /// �A�v���P�[�V�����̃o�[�W����
        /// </summary>
        public static string VersionNumber
        {
            get { return VERSION_NUMBER; }
        }

        /// <summary>
        /// ���쌠���
        /// </summary>
        private const string COPYRIGHT = "Copyright (C) 2006 Uraroji";

        /// <summary>
        /// ���쌠���
        /// </summary>
        public static string Copyright
        {
            get { return COPYRIGHT; }
        }

        #endregion

        #region �A�v���P�[�V�����̐ݒ�

        /// <summary>
        /// Web�ڑ����̃^�C���A�E�g����
        /// </summary>
        private const int WEB_REQUESR_TIMEOUT_MILL_SEC = 20000;

        /// <summary>
        /// Web�ڑ����̃^�C���A�E�g����
        /// </summary>
        public static int WebRequestTimeoutMillSec
        {
            get { return WEB_REQUESR_TIMEOUT_MILL_SEC; }
        }

        /// <summary>
        /// �_�E�����[�h����1�T�C�N���ɓǂݏo���o�C�g�T�C�Y
        /// </summary>
        private const int READ_BYTE_ONE_CYCLE_DOWNLOAD = 0x8000;   // 32KB

        /// <summary>
        /// �_�E�����[�h����1�T�C�N���ɓǂݏo���o�C�g�T�C�Y
        /// </summary>
        public static int ReadByteOneCycleDownload
        {
            get { return READ_BYTE_ONE_CYCLE_DOWNLOAD; }
        } 

        /// <summary>
        /// �l�b�g�A�N�Z�X����UserAgent�ݒ�
        /// </summary>
        private const string userAgent = APPLICATION_NAME + "/" + VERSION_NUMBER;

        /// <summary>
        /// �l�b�g�A�N�Z�X����UserAgent�ݒ�
        /// </summary>
        public static string UserAgent
        {
            get { return userAgent; }
        }

        /// <summary>
        /// Podcast��MIME�^�C�v�̗D��x�t�@�C��
        /// </summary>
        private const string RSS_PODCAST_MIME_PRIORITY_FILE
            = "PodcasCo.Resource.RssPodcastMimePriority.txt";

        /// <summary>
        /// Podcast��MIME�^�C�v�̗D��x�t�@�C��
        /// </summary>
        public static string RssPodcastMimePriorityFile
        {
            get { return RSS_PODCAST_MIME_PRIORITY_FILE; }
        }

        /// <summary>
        /// �A�v���P�[�V�����̐ݒ�t�@�C��
        /// </summary>
        private const string SETTING_FILE = "Setting.xml";

        /// <summary>
        /// �A�v���P�[�V�����̐ݒ�t�@�C��
        /// </summary>
        public static string SettingFile
        {
            get { return SETTING_FILE; }
        }

        /// <summary>
        /// ���[�J���w�b�h���C����RSS�t�@�C��
        /// </summary>
        private const string LOCAL_RSS_FILE = "rss.xml";

        /// <summary>
        /// ���[�J���w�b�h���C����RSS�t�@�C��
        /// </summary>
        public static string LocalRssFile
        {
            get { return LOCAL_RSS_FILE; }
        } 


        #endregion

        /// <summary>
        /// �V���O���g���̂��߃v���C�x�[�g
        /// </summary>
        private PodcasCoInfo()
        {
        }
    }
}
