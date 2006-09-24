#region ディレクティブを使用する

using System;

#endregion

namespace PodcasCo
{
    /// <summary>
    /// PocketLadioの固有情報を記述しているクラス
    /// </summary>
    public sealed class PodcasCoInfo
    {
        #region アプリケーション固有の情報

        /// <summary>
        /// アプリケーション名
        /// </summary>
        private const string APPLICATION_NAME = "PodcasCo";

        /// <summary>
        /// アプリケーション名
        /// </summary>
        public static string ApplicationName
        {
            get { return APPLICATION_NAME; }
        }

        /// <summary>
        /// アプリケーションのバージョン
        /// </summary>
        private const string VERSION_NUMBER = "0.2";

        /// <summary>
        /// アプリケーションのバージョン
        /// </summary>
        public static string VersionNumber
        {
            get { return VERSION_NUMBER; }
        }

        /// <summary>
        /// 著作権情報
        /// </summary>
        private const string COPYRIGHT = "Copyright (C) 2006 Uraroji";

        /// <summary>
        /// 著作権情報
        /// </summary>
        public static string Copyright
        {
            get { return COPYRIGHT; }
        }

        #endregion

        #region アプリケーションの設定

        /// <summary>
        /// Web接続時のタイムアウト時間
        /// </summary>
        private const int WEB_REQUESR_TIMEOUT_MILL_SEC = 20000;

        /// <summary>
        /// Web接続時のタイムアウト時間
        /// </summary>
        public static int WebRequestTimeoutMillSec
        {
            get { return WEB_REQUESR_TIMEOUT_MILL_SEC; }
        }

        /// <summary>
        /// ダウンロード時に1サイクルに読み出すバイトサイズ
        /// </summary>
        private const int READ_BYTE_ONE_CYCLE_DOWNLOAD = 0x8000;   // 32KB

        /// <summary>
        /// ダウンロード時に1サイクルに読み出すバイトサイズ
        /// </summary>
        public static int ReadByteOneCycleDownload
        {
            get { return READ_BYTE_ONE_CYCLE_DOWNLOAD; }
        } 

        /// <summary>
        /// ネットアクセス時のUserAgent設定
        /// </summary>
        private const string userAgent = APPLICATION_NAME + "/" + VERSION_NUMBER;

        /// <summary>
        /// ネットアクセス時のUserAgent設定
        /// </summary>
        public static string UserAgent
        {
            get { return userAgent; }
        }

        /// <summary>
        /// PodcastのMIMEタイプの優先度ファイル
        /// </summary>
        private const string RSS_PODCAST_MIME_PRIORITY_FILE
            = "PodcasCo.Resource.RssPodcastMimePriority.txt";

        /// <summary>
        /// PodcastのMIMEタイプの優先度ファイル
        /// </summary>
        public static string RssPodcastMimePriorityFile
        {
            get { return RSS_PODCAST_MIME_PRIORITY_FILE; }
        }

        /// <summary>
        /// アプリケーションの設定ファイル
        /// </summary>
        private const string SETTING_FILE = "Setting.xml";

        /// <summary>
        /// アプリケーションの設定ファイル
        /// </summary>
        public static string SettingFile
        {
            get { return SETTING_FILE; }
        }

        /// <summary>
        /// ローカルヘッドラインのRSSファイル
        /// </summary>
        private const string LOCAL_RSS_FILE = "rss.xml";

        /// <summary>
        /// ローカルヘッドラインのRSSファイル
        /// </summary>
        public static string LocalRssFile
        {
            get { return LOCAL_RSS_FILE; }
        } 


        #endregion

        /// <summary>
        /// シングルトンのためプライベート
        /// </summary>
        private PodcasCoInfo()
        {
        }
    }
}
