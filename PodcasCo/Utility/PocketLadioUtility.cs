#region ディレクティブを使用する

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Collections;
using System.Xml;

#endregion

namespace PodcasCo.Utility
{
    /// <summary>
    /// PocketLadioのユーティリティ
    /// </summary>
    public sealed class PocketLadioUtility
    {
        /// <summary>
        /// シングルトンのためプライベート
        /// </summary>
        private PocketLadioUtility()
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

            Process.CreateProcess(UserSetting.MediaPlayerPath, filePath);
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

            Process.CreateProcess(UserSetting.MediaPlayerPath, streamingUrl.ToString());
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

            Process.CreateProcess(UserSetting.BrowserPath, websiteUrl.ToString());
        }

        /// <summary>
        /// アプリケーションの実行ディレクトリのパスを返す
        /// </summary>
        /// <returns>アプリケーションの実行ディレクトリのパス</returns>
        public static string GetExecutablePath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        }

        /// <summary>
        /// HTTPレスポンスをストリームとして返す。
        /// プロキシ設定やタイムアウトなどの情報については、PocketLadio.UserSettingやControllerを参照している。
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>HTTPレスポンスのストリーム</returns>
        public static Stream GetHttpStream(Uri url) {
            Stream st = null;
            try
            {
                WebRequest req = WebRequest.Create(url);
                req.Timeout = PodcasCoInfo.WebRequestTimeoutMillSec;

                // HTTPプロトコルでネットにアクセスする場合
                if (req.GetType() == typeof(System.Net.HttpWebRequest))
                {
                    // UserAgentを付加
                    ((HttpWebRequest)req).UserAgent = PodcasCoInfo.UserAgent;

                    // プロキシの設定が存在した場合、プロキシを設定
                    if (UserSetting.ProxyUse == true && !(UserSetting.ProxyServer.Length == 0 || UserSetting.ProxyPort.Length == 0))
                    {
                        ((HttpWebRequest)req).Proxy =
                            new WebProxy(UserSetting.ProxyServer, int.Parse(UserSetting.ProxyPort));
                    }
                }

                WebResponse result = req.GetResponse();
                st = result.GetResponseStream();
            }
            catch (WebException)
            {
                throw;
            }
            catch (OutOfMemoryException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
            catch (UriFormatException)
            {
                throw;
            }
            catch (SocketException)
            {
                throw;
            }

            return st;
        }
    }
}
