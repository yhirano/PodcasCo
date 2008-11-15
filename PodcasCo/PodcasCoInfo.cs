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
        private const string VERSION_NUMBER = "0.9";

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
        private const string COPYRIGHT = "Copyright (C) 2006-2008 Y.Hirano";

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
        /// ���f�B�A�v���[���[�̃p�X�̃f�t�H���g�ݒ�
        /// </summary>
        private const string DEFAULT_MEDIA_PLAYER_PATH = @"\Program Files\TCPMP\player.exe";

        /// <summary>
        /// ���f�B�A�v���[���[�̃p�X�̃f�t�H���g�ݒ�
        /// </summary>
        public static string DefaultMediaPlayerPath
        {
            get { return DEFAULT_MEDIA_PLAYER_PATH; }
        }

        /// <summary>
        /// �u���E�U�̃p�X�̃f�t�H���g�ݒ�
        /// </summary>
        private const string DEFAULT_BROWSER_PATH = @"\Windows\iexplore.exe";

        /// <summary>
        /// �u���E�U�̃p�X�̃f�t�H���g�ݒ�
        /// </summary>
        public static string DefaultBrowserPath
        {
            get { return DEFAULT_BROWSER_PATH; }
        }

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
        /// ���C���t�H�[���̕��B
        /// �t�H�[���f�U�C���͂��̕����x�[�X��Control��u���Ă���B
        /// </summary>
        private const int FORM_BASE_WIDRH = 240;

        /// <summary>
        /// ���C���t�H�[���̕��B
        /// �t�H�[���f�U�C���͂��̕����x�[�X��Control��u���Ă���B
        /// </summary>
        public static int FormBaseWidth
        {
            get { return FORM_BASE_WIDRH; }
        }

        /// <summary>
        /// ���C���t�H�[���̍����B
        /// �t�H�[���f�U�C���͂��̍������x�[�X��Control��u���Ă���B
        /// </summary>
        private const int FORM_BASE_HIGHT = 268;

        /// <summary>
        /// ���C���t�H�[���̍����B
        /// �t�H�[���f�U�C���͂��̍������x�[�X��Control��u���Ă���B
        /// </summary>
        public static int FormBaseHight
        {
            get { return FORM_BASE_HIGHT; }
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

        /// <summary>
        /// �Đ����ɍ쐬����m3u�t�@�C����
        /// </summary>
        private const string GENERATE_M3U_FILE_NAME = "PodcasCo_podcast_list.m3u";

        /// <summary>
        /// �Đ����ɍ쐬����m3u�t�@�C����
        /// </summary>
        public static string GenerateM3uFileName
        {
            get { return GENERATE_M3U_FILE_NAME; }
        }

        /// <summary>
        /// ��O�ɏo�͂��郍�O�t�@�C��
        /// </summary>
        private const string EXCEPTION_LOG_FILE = "PodcasCoExceptionLog.log";

        /// <summary>
        /// ��O�ɏo�͂��郍�O�t�@�C��
        /// </summary>
        public static string ExceptionLogFile
        {
            get { return EXCEPTION_LOG_FILE; }
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
