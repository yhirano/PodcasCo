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
        private const string VERSION_NUMBER = "0.9";

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
        private const string COPYRIGHT = "Copyright (C) 2006-2008 Y.Hirano";

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
        /// メディアプレーヤーのパスのデフォルト設定
        /// </summary>
        private const string DEFAULT_MEDIA_PLAYER_PATH = @"\Program Files\TCPMP\player.exe";

        /// <summary>
        /// メディアプレーヤーのパスのデフォルト設定
        /// </summary>
        public static string DefaultMediaPlayerPath
        {
            get { return DEFAULT_MEDIA_PLAYER_PATH; }
        }

        /// <summary>
        /// ブラウザのパスのデフォルト設定
        /// </summary>
        private const string DEFAULT_BROWSER_PATH = @"\Windows\iexplore.exe";

        /// <summary>
        /// ブラウザのパスのデフォルト設定
        /// </summary>
        public static string DefaultBrowserPath
        {
            get { return DEFAULT_BROWSER_PATH; }
        }

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
        /// メインフォームの幅。
        /// フォームデザインはこの幅をベースにControlを置いている。
        /// </summary>
        private const int FORM_BASE_WIDRH = 240;

        /// <summary>
        /// メインフォームの幅。
        /// フォームデザインはこの幅をベースにControlを置いている。
        /// </summary>
        public static int FormBaseWidth
        {
            get { return FORM_BASE_WIDRH; }
        }

        /// <summary>
        /// メインフォームの高さ。
        /// フォームデザインはこの高さをベースにControlを置いている。
        /// </summary>
        private const int FORM_BASE_HIGHT = 268;

        /// <summary>
        /// メインフォームの高さ。
        /// フォームデザインはこの高さをベースにControlを置いている。
        /// </summary>
        public static int FormBaseHight
        {
            get { return FORM_BASE_HIGHT; }
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

        /// <summary>
        /// 再生時に作成するm3uファイル名
        /// </summary>
        private const string GENERATE_M3U_FILE_NAME = "PodcasCo_podcast_list.m3u";

        /// <summary>
        /// 再生時に作成するm3uファイル名
        /// </summary>
        public static string GenerateM3uFileName
        {
            get { return GENERATE_M3U_FILE_NAME; }
        }

        /// <summary>
        /// 例外に出力するログファイル
        /// </summary>
        private const string EXCEPTION_LOG_FILE = "PodcasCoExceptionLog.log";

        /// <summary>
        /// 例外に出力するログファイル
        /// </summary>
        public static string ExceptionLogFile
        {
            get { return EXCEPTION_LOG_FILE; }
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
