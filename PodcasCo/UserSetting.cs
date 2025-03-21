#region ディレクティブを使用する

using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using System.Diagnostics;
using MiscPocketCompactLibrary.Reflection;

#endregion

namespace PodcasCo
{
    /// <summary>
    /// PocketLadioの設定を保持するクラス
    /// </summary>
    public sealed class UserSetting
    {
        /// <summary>
        /// Podcastをクリップするディレクトリ
        /// </summary>
        private static string podcastClipDirectoryPath = AssemblyUtility.GetExecutablePath() + @"\" + PodcasCoInfo.ApplicationName + "Clip";

        /// <summary>
        /// Podcastをクリップするディレクトリ
        /// </summary>
        public static string PodcastClipDirectoryPath
        {
            get { return UserSetting.podcastClipDirectoryPath; }
            set { UserSetting.podcastClipDirectoryPath = value; }
        }

        /// <summary>
        /// 再生時に作成するm3uファイルのパス
        /// </summary>
        public static string M3uFilePath
        {
            get { return podcastClipDirectoryPath + @"\" + PodcasCoInfo.GenerateM3uFileName; }
        }

        /// <summary>
        /// 音声再生用のメディアプレーヤーのファイルパス
        /// </summary>
        private static string mediaPlayerPath = PodcasCoInfo.DefaultMediaPlayerPath;

        /// <summary>
        /// 音声再生用のメディアプレーヤーのファイルパス
        /// </summary>
        public static string MediaPlayerPath
        {
            get { return UserSetting.mediaPlayerPath; }
            set { UserSetting.mediaPlayerPath = value; }
        }

        /// <summary>
        /// Webブラウザのファイルパス
        /// </summary>
        private static string browserPath = PodcasCoInfo.DefaultBrowserPath;

        /// <summary>
        /// Webブラウザのファイルパス
        /// </summary>
        public static string BrowserPath
        {
            get { return UserSetting.browserPath; }
            set { UserSetting.browserPath = value; }
        }

        /// <summary>
        /// プロキシの接続方法列挙
        /// </summary>
        public enum ProxyConnect
        {
            Unuse, OsSetting, OriginalSetting
        }

        /// <summary>
        /// プロキシを接続方法
        /// </summary>
        private static ProxyConnect proxyUse = ProxyConnect.OsSetting;

        /// <summary>
        /// プロキシを接続方法
        /// </summary>
        public static ProxyConnect ProxyUse
        {
            get { return UserSetting.proxyUse; }
            set { UserSetting.proxyUse = value; }
        }

        /// <summary>
        /// プロキシのサーバ名
        /// </summary>
        private static string proxyServer = string.Empty;

        /// <summary>
        /// プロキシのサーバ名
        /// </summary>
        public static string ProxyServer
        {
            get { return proxyServer; }
            set { proxyServer = value; }
        }

        /// <summary>
        /// プロキシのポート番号
        /// </summary>
        private static int proxyPort = 0;

        /// <summary>
        /// プロキシのポート番号
        /// </summary>
        public static int ProxyPort
        {
            get
            {
                if (0x00 <= proxyPort && proxyPort <= 0xFFFF)
                {
                    return proxyPort;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (0x00 <= value && value <= 0xFFFF)
                {
                    proxyPort = value;
                }
                else
                {
                    ;
                }
            }
        }

        /// <summary>
        /// ダウンロード時のバッファサイズ
        /// </summary>
        private static long downLoadBufferSize = 0x8000;    // 32KB

        /// <summary>
        /// ダウンロード時のバッファサイズ
        /// </summary>
        public static long DownLoadBufferSize
        {
            get
            {
                // 512B〜64KB
                if (512 <= downLoadBufferSize && downLoadBufferSize <= 0x10000)
                {
                    return downLoadBufferSize;
                }
                else
                {
                    return 0x8000;    // 32KB
                }
            }
            set
            {
                if (512 <= value && value <= 0x10000)
                {
                    downLoadBufferSize = value;
                }
                else
                {
                    ;
                }
            }
        }

        public enum ChannelSorts
        {
            None, DateNewer, DateOlder
        }

        /// <summary>
        /// 番組のソート方法
        /// </summary>
        private static ChannelSorts channelSort = ChannelSorts.None;

        /// <summary>
        /// 番組のソート方法を取得・設定する
        /// </summary>
        public static ChannelSorts ChannelSort
        {
            get { return channelSort; }
            set { channelSort = value; }
        }

        /// <summary>
        /// アプリケーションの設定ファイルの保存場所
        /// </summary>
        private static string SettingPath
        {
            get
            {
                // アプリケーションの実行ディレクトリ + アプリケーションの設定ファイル
                return AssemblyUtility.GetExecutablePath() + @"\" + PodcasCoInfo.SettingFile;
            }
        }

        /// <summary>
        /// シングルトンのためプライベート
        /// </summary>
        private UserSetting()
        {
        }

        /// <summary>
        /// 設定をファイルから読み込む
        /// </summary>
        public static void LoadSetting()
        {
            if (File.Exists(SettingPath))
            {
                FileStream fs = null;
                XmlTextReader reader = null;

                try
                {
                    fs = new FileStream(SettingPath, FileMode.Open, FileAccess.Read);
                    reader = new XmlTextReader(fs);

                    ArrayList alStation = new ArrayList();

                    // StationListタグの中にいるか
                    bool inStationListFlag = false;

                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {

                            if (reader.LocalName == "StationList")
                            {
                                inStationListFlag = true;
                            } // End of StationList
                            // StationListタグの中にいる場合
                            else if (inStationListFlag == true)
                            {
                                if (reader.LocalName == "Station")
                                {
                                    string id = string.Empty;
                                    string name = string.Empty;
                                    Station.StationKind stationKind = Station.StationKind.RssPodcast;
                                    bool startupDownload = false;
                                    int startupDownloadNum = Station.STARTUP_DOWNLOAD_NUM_MIN;
                                    bool startupDelete = false;
                                    int startupDeleteRemainDay = Station.STARTUP_DELETE_REMAIN_DAY_MIN;

                                    if (reader.MoveToFirstAttribute())
                                    {
                                        id = reader.GetAttribute("id");
                                        name = reader.GetAttribute("name");

                                        string kind = reader.GetAttribute("kind");
                                        if (kind == Station.StationKind.RssPodcast.ToString())
                                        {
                                            stationKind = Station.StationKind.RssPodcast;
                                        }
                                        else
                                        {
                                            // ここに到達することはあり得ない
                                            Trace.Assert(false, "想定外の動作のため、終了します");
                                        }

                                        string startupDownloadString = reader.GetAttribute("startupDownload");
                                        if (startupDownloadString == true.ToString())
                                        {
                                            startupDownload = true;
                                        }
                                        else
                                        {
                                            startupDownload = false;
                                        }

                                        string startupDownloadNumString = reader.GetAttribute("startupDownloadNum");
                                        if (startupDownloadNumString != null)
                                        {
                                            try
                                            {
                                                startupDownloadNum = int.Parse(startupDownloadNumString);
                                            }
                                            catch (ArgumentException)
                                            {
                                                ;
                                            }
                                            catch (FormatException)
                                            {
                                                ;
                                            }
                                            catch (OverflowException)
                                            {
                                                ;
                                            }
                                        }
                                        if (startupDownloadNum < Station.STARTUP_DOWNLOAD_NUM_MIN)
                                        {
                                            startupDownloadNum = Station.STARTUP_DOWNLOAD_NUM_MIN;
                                        }

                                        string startupDeleteString = reader.GetAttribute("startupDelete");
                                        if (startupDeleteString == true.ToString())
                                        {
                                            startupDelete = true;
                                        }
                                        else
                                        {
                                            startupDelete = false;
                                        }

                                        string startupDeleteRemainDayString = reader.GetAttribute("startupDeleteRemainDay");
                                        if (startupDeleteRemainDayString != null)
                                        {
                                            try
                                            {
                                                startupDeleteRemainDay = int.Parse(startupDeleteRemainDayString);
                                            }
                                            catch (ArgumentException)
                                            {
                                                ;
                                            }
                                            catch (FormatException)
                                            {
                                                ;
                                            }
                                            catch (OverflowException)
                                            {
                                                ;
                                            }
                                        }

                                        #region Version 0.7 〜 Version 0.8 TestRelease5互換
                                        string startupDeleteDayString = reader.GetAttribute("startupDeleteDay");
                                        if (startupDeleteDayString != null)
                                        {
                                            try
                                            {
                                                startupDeleteRemainDay = int.Parse(startupDeleteDayString) + 1;
                                            }
                                            catch (ArgumentException)
                                            {
                                                ;
                                            }
                                            catch (FormatException)
                                            {
                                                ;
                                            }
                                            catch (OverflowException)
                                            {
                                                ;
                                            }
                                        }
                                        #endregion // Version 0.7 〜 Version 0.8 TestRelease5互換

                                        if (startupDeleteRemainDay < Station.STARTUP_DELETE_REMAIN_DAY_MIN)
                                        {
                                            startupDeleteRemainDay = Station.STARTUP_DELETE_REMAIN_DAY_MIN;
                                        }

                                        try
                                        {
                                            Station station = new Station(id, name, stationKind);
                                            station.StartupDownload = startupDownload;
                                            station.StartupDownloadNum = startupDownloadNum;
                                            station.StartupDelete = startupDelete;
                                            station.StartupDeleteRemainDay = startupDeleteRemainDay;
                                            alStation.Add(station);
                                        }
                                        // ローカルヘッドラインの解析が失敗した場合は、そのヘッドラインを無視する
                                        catch (XmlException)
                                        {
                                            ;
                                        }
                                        catch (IOException)
                                        {
                                            ;
                                        }
                                    }
                                } // End of Station
                            } // End of StationListタグの中にいる場合
                            else if (reader.LocalName == "PodcastClipDirectoryPath")
                            {
                                string path = reader.GetAttribute("path");
                                if (path.Length > 0)
                                {
                                    PodcastClipDirectoryPath = path;
                                }
                            } // End of PodcastClipDirectory
                            else if (reader.LocalName == "MediaPlayerPath")
                            {
                                MediaPlayerPath = reader.GetAttribute("path");
                            } // End of MediaPlayerPath
                            else if (reader.LocalName == "BrowserPath")
                            {
                                BrowserPath = reader.GetAttribute("path");
                            } // End of BrowserPath
                            else if (reader.LocalName == "Proxy")
                            {
                                if (reader.MoveToFirstAttribute())
                                {
                                    string use = reader.GetAttribute("use");
                                    if (use == ProxyConnect.Unuse.ToString())
                                    {
                                        ProxyUse = ProxyConnect.Unuse;
                                    }
                                    else if (use == ProxyConnect.OsSetting.ToString())
                                    {
                                        ProxyUse = ProxyConnect.OsSetting;
                                    }
                                    else if (use == ProxyConnect.OriginalSetting.ToString())
                                    {
                                        ProxyUse = ProxyConnect.OriginalSetting;
                                    }

                                    ProxyServer = reader.GetAttribute("server");

                                    try
                                    {
                                        string port = reader.GetAttribute("port");
                                        ProxyPort = int.Parse(port);
                                    }
                                    catch (ArgumentException)
                                    {
                                        ;
                                    }
                                    catch (FormatException)
                                    {
                                        ;
                                    }
                                    catch (OverflowException)
                                    {
                                        ;
                                    }
                                }
                            } // End of Proxy
                            else if (reader.LocalName == "DownloadBuffer")
                            {
                                if (reader.MoveToFirstAttribute())
                                {
                                    try
                                    {
                                        string size = reader.GetAttribute("size");
                                        DownLoadBufferSize = long.Parse(size);
                                    }
                                    catch (ArgumentException)
                                    {
                                        ;
                                    }
                                    catch (FormatException)
                                    {
                                        ;
                                    }
                                    catch (OverflowException)
                                    {
                                        ;
                                    }
                                }
                            } // End of DownloadBuffer
                            else if (reader.LocalName == "ChannelSort")
                            {
                                string sort = reader.GetAttribute("sort");
                                if (sort == ChannelSorts.None.ToString())
                                {
                                    ChannelSort = ChannelSorts.None;
                                }
                                else if (sort == ChannelSorts.DateNewer.ToString())
                                {
                                    ChannelSort = ChannelSorts.DateNewer;
                                }
                                else if (sort == ChannelSorts.DateOlder.ToString())
                                {
                                    ChannelSort = ChannelSorts.DateOlder;
                                }

                            } // End of ChannelSort
                        }
                        else if (reader.NodeType == XmlNodeType.EndElement)
                        {
                            if (reader.LocalName == "StationList")
                            {
                                inStationListFlag = false;
                                StationList.SetStationList((Station[])alStation.ToArray(typeof(Station)));
                            }
                        }
                    }
                }
                finally
                {
                    reader.Close();
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// 設定をファイルに保存
        /// </summary>
        public static void SaveSetting()
        {
            FileStream fs = null;
            XmlTextWriter writer = null;

            try
            {
                fs = new FileStream(SettingPath, FileMode.Create, FileAccess.Write);
                writer = new XmlTextWriter(fs, Encoding.GetEncoding("utf-8"));

                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument(true);

                writer.WriteStartElement("Setting");

                writer.WriteStartElement("Header");

                writer.WriteStartElement("Name");
                writer.WriteAttributeString("name", PodcasCoInfo.ApplicationName);
                writer.WriteEndElement(); // End of Name.
                writer.WriteStartElement("Version");
                writer.WriteAttributeString("version", PodcasCoInfo.VersionNumber);
                writer.WriteEndElement(); // End of Version.

                writer.WriteStartElement("Date");
                writer.WriteAttributeString("date", DateTime.Now.ToString());
                writer.WriteEndElement(); // End of Date.

                writer.WriteEndElement(); // End of Header.

                writer.WriteStartElement("Content");

                writer.WriteStartElement("StationList");
                foreach (Station station in StationList.GetStationList())
                {
                    writer.WriteStartElement("Station");
                    writer.WriteAttributeString("id", station.Id);
                    writer.WriteAttributeString("name", station.Name);
                    writer.WriteAttributeString("kind", station.Kind.ToString());
                    writer.WriteAttributeString("startupDownload", station.StartupDownload.ToString());
                    writer.WriteAttributeString("startupDownloadNum", station.StartupDownloadNum.ToString());
                    writer.WriteAttributeString("startupDelete", station.StartupDelete.ToString());
                    writer.WriteAttributeString("startupDeleteRemainDay", station.StartupDeleteRemainDay.ToString());
                    writer.WriteEndElement(); // End of Station
                }
                writer.WriteEndElement(); // End of StationList

                writer.WriteStartElement("PodcastClipDirectoryPath");
                writer.WriteAttributeString("path", PodcastClipDirectoryPath);
                writer.WriteEndElement(); // End of PodcastClipDirectoryPath

                writer.WriteStartElement("MediaPlayerPath");
                writer.WriteAttributeString("path", MediaPlayerPath);
                writer.WriteEndElement(); // End of MediaPlayerPath

                writer.WriteStartElement("BrowserPath");
                writer.WriteAttributeString("path", BrowserPath);
                writer.WriteEndElement(); // End of BrowserPath

                writer.WriteStartElement("Proxy");
                writer.WriteAttributeString("use", ProxyUse.ToString());
                writer.WriteAttributeString("server", ProxyServer);
                writer.WriteAttributeString("port", ProxyPort.ToString());
                writer.WriteEndElement(); // End of Porxy

                writer.WriteStartElement("ChannelSort");
                writer.WriteAttributeString("sort", ChannelSort.ToString());
                writer.WriteEndElement(); // End of ChannelSort

                writer.WriteEndElement(); // End of Content.

                writer.WriteEndElement(); // End of Setting.

                writer.WriteEndDocument();
            }
            finally
            {
                writer.Close();
                fs.Close();
            }
        }
    }
}
