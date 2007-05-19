#region ディレクティブを使用する

using System;
using System.IO;
using MiscPocketCompactLibrary.Runtime.InteropServices;
using MiscPocketCompactLibrary.Net;

#endregion

namespace PodcasCo
{
    /// <summary>
    /// PocketLadioのユーティリティ
    /// </summary>
    public sealed class PodcasCoUtility
    {
        /// <summary>
        /// シングルトンのためプライベート
        /// </summary>
        private PodcasCoUtility()
        {
        }

        /// <summary>
        /// 音声ファイルを再生する。
        /// 再生用プログラムが見つからない場合はFileNotFoundExceptionを投げる。
        /// </summary>
        /// <param name="filePath">音声ファイルのパス</param>
        public static void PlayStreaming(string filePath)
        {
            // 再生用メディアプレイヤーが見つからない場合には例外を投げる
            if (File.Exists(UserSetting.MediaPlayerPath) == false)
            {
                throw new FileNotFoundException("Not found media player.");
            }
            if (filePath == "")
            {
                return;
            }

            ProcessUtility.CreateProcess(UserSetting.MediaPlayerPath, "\"" + filePath + "\"");
        }

        /// <summary>
        /// ストリーミングを再生する。
        /// 再生用プログラムが見つからない場合はFileNotFoundExceptionを投げる。
        /// </summary>
        /// <param name="streamingUrl">ストリーミングのURL</param>
        public static void PlayStreaming(Uri streamingUrl)
        {
            // 再生用メディアプレイヤーが見つからない場合には例外を投げる
            if (File.Exists(UserSetting.MediaPlayerPath) == false)
            {
                throw new FileNotFoundException("Not found media player.");
            }
            if (streamingUrl == null)
            {
                return;
            }

            ProcessUtility.CreateProcess(UserSetting.MediaPlayerPath, streamingUrl.ToString());
        }

        /// <summary>
        /// Webサイトにアクセスする。
        /// ブラウザが見つからない場合はFileNotFoundExceptionを投げる。
        /// </summary>
        /// <param name="url">WebサイトのURL</param>
        public static void AccessWebsite(Uri websiteUrl)
        {
            // ブラウザが見つからない場合には例外を投げる
            if (File.Exists(UserSetting.BrowserPath) == false)
            {
                throw new FileNotFoundException("Not found web browser.");
            }
            if (websiteUrl == null)
            {
                return;
            }

            ProcessUtility.CreateProcess(UserSetting.BrowserPath, websiteUrl.ToString());
        }

        /// <summary>
        /// Web上のストリームを返す
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>Web上のストリーム</returns>
        public static WebStream GetWebStream(Uri url)
        {
            WebStream ws = new WebStream(url);
            if (PodcasCo.UserSetting.ProxyUse == UserSetting.ProxyConnect.Unuse)
            {
                ws.ProxyUse = WebStream.ProxyConnect.Unuse;
            }
            else if (PodcasCo.UserSetting.ProxyUse == UserSetting.ProxyConnect.OsSetting)
            {
                ws.ProxyUse = WebStream.ProxyConnect.OsSetting;
            }
            else if (PodcasCo.UserSetting.ProxyUse == UserSetting.ProxyConnect.OriginalSetting)
            {
                ws.ProxyUse = WebStream.ProxyConnect.OriginalSetting;
            }
            ws.ProxyServer = PodcasCo.UserSetting.ProxyServer;
            ws.ProxyPort = PodcasCo.UserSetting.ProxyPort;
            ws.TimeOut = PodcasCoInfo.WebRequestTimeoutMillSec;
            ws.UserAgent = PodcasCoInfo.UserAgent;
            ws.CreateWebStream();

            return ws;
        }


        /// <summary>
        /// Web上のストリームをダウンロードする
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="fileName">保存するファイル名</param>
        public static void FetchFile(Uri url, string fileName)
        {
            FetchFile(url, fileName, null, null, null);
        }

        /// <summary>
        /// Web上のストリームをダウンロードする
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="fileName">保存するファイル名</param>
        /// <param name="doDownloadProgressMinimum">ファイルサイズの最小値（0）をセットするデリゲート</param>
        /// <param name="doSetDownloadProgressMaximum">ファイルサイズをセットするデリゲート</param>
        /// <param name="doSetDownloadProgressValue">ダウンロード済みのファイルサイズをセットするデリゲート</param>
        public static void FetchFile(Uri url, string fileName,
            WebStream.FetchEventHandler fetchEventHandler,
            WebStream.FetchingEventHandler fetchingEventHandler,
            WebStream.FetchedEventHandler fetchedEventHandler)
        {

            WebStream ws = null;
            try
            {
                ws = new WebStream(url);

                if (fetchEventHandler != null)
                {
                    ws.Fetch += fetchEventHandler;
                }
                if (fetchingEventHandler != null)
                {
                    ws.Fetching += fetchingEventHandler;

                }
                if (fetchedEventHandler != null)
                {
                    ws.Fetched += fetchedEventHandler;

                }

                if (PodcasCo.UserSetting.ProxyUse == UserSetting.ProxyConnect.Unuse)
                {
                    ws.ProxyUse = WebStream.ProxyConnect.Unuse;
                }
                else if (PodcasCo.UserSetting.ProxyUse == UserSetting.ProxyConnect.OsSetting)
                {
                    ws.ProxyUse = WebStream.ProxyConnect.OsSetting;
                }
                else if (PodcasCo.UserSetting.ProxyUse == UserSetting.ProxyConnect.OriginalSetting)
                {
                    ws.ProxyUse = WebStream.ProxyConnect.OriginalSetting;
                }
                ws.ProxyServer = PodcasCo.UserSetting.ProxyServer;
                ws.ProxyPort = PodcasCo.UserSetting.ProxyPort;
                ws.TimeOut = PodcasCoInfo.WebRequestTimeoutMillSec;
                ws.UserAgent = PodcasCoInfo.UserAgent;
                ws.DownLoadBufferSize = PodcasCo.UserSetting.DownLoadBufferSize;
                ws.SetResume(fileName);
                try
                {
                    ws.CreateWebStream();
                    ws.FetchFile(fileName);
                }
                catch (MismatchFetchFileException)
                {
                    // ダウンロードすべきよりも、すでにダウンロードしているファイルの方が大きい場合は
                    // 一端ファイルを削除し、リジュームを無効にする
                    File.Delete(fileName);
                    ws.RemoveResume();
                    ws.CreateWebStream();
                    ws.FetchFile(fileName);
                }
                catch (AlreadyFetchFileException)
                {
                    // ファイルがすでに存在する場合は何もしない
                    ;
                }
            }
            finally
            {
                if (ws != null)
                {
                    ws.Close();
                }
            }
        }
    }
}
