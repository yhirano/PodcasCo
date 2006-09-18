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
        private const string applicationName = "PodcasCo";

        /// <summary>
        /// アプリケーション名
        /// </summary>
        public static string ApplicationName
        {
            get { return applicationName; }
        }

        /// <summary>
        /// アプリケーションのバージョン
        /// </summary>
        private const string versionNumber = "0.1";

        /// <summary>
        /// アプリケーションのバージョン
        /// </summary>
        public static string VersionNumber
        {
            get { return versionNumber; }
        }

        /// <summary>
        /// 著作権情報
        /// </summary>
        private const string copyright = "Copyright (C) 2006 Uraroji";

        /// <summary>
        /// 著作権情報
        /// </summary>
        public static string Copyright
        {
            get { return copyright; }
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
        private const int READ_BYTE_ONE_CYCLE_DOWNLOAD = 0x4ffff;

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
        private const string userAgent = applicationName + "/" + versionNumber;

        /// <summary>
        /// ネットアクセス時のUserAgent設定
        /// </summary>
        public static string UserAgent
        {
            get { return userAgent; }
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
