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
        /// 再生時に作成するm3uファイル名
        /// </summary>
        private const string GENERATE_M3U_FILE_NAME = "PodcasCo_podcast_list.m3u";

        /// <summary>
        /// 再生時に作成するm3uファイルのパス
        /// </summary>
        public static string M3uFilePath
        {
            get { return podcastClipDirectoryPath + @"\" + GENERATE_M3U_FILE_NAME; }
        }

        /// <summary>
        /// 音声再生用のメディアプレーヤーのファイルパス
        /// </summary>
        private static string mediaPlayerPath = @"\Program Files\TCPMP\player.exe";

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
        private static string browserPath = @"\Windows\iexplore.exe";

        /// <summary>
        /// Webブラウザのファイルパス
        /// </summary>
        public static string BrowserPath
        {
            get { return UserSetting.browserPath; }
            set { UserSetting.browserPath = value; }
        }

        /// <summary>
        /// プロキシを使用するか
        /// </summary>
        private static bool proxyUse;

        /// <summary>
        /// プロキシを使用するか
        /// </summary>
        public static bool ProxyUse
        {
            get { return UserSetting.proxyUse; }
            set { UserSetting.proxyUse = value; }
        }

        /// <summary>
        /// プロキシのサーバ名
        /// </summary>
        private static string proxyServer = "";

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
        private static long downLoadBufferSize = 0x1FFFF; // 128KB

        /// <summary>
        /// ダウンロード時のバッファサイズ
        /// </summary>
        public static long DownLoadBufferSize
        {
            get
            {
                if (0xFFFF <= downLoadBufferSize && downLoadBufferSize <= 0x3FFFF)
                {
                    return downLoadBufferSize;
                }
                else
                {
                    return 0x1FFFF;
                }
            }
            set
            {
                if (0xFFFF <= value && value <= 0x3FFFF)
                {
                    downLoadBufferSize = value;
                }
                else
                {
                    ;
                }
            }
        }

        /// <summary>
        /// アプリケーションの設定ファイル
        /// </summary>
        private const string SETTING_PATH = "Setting.xml";

        /// <summary>
        /// アプリケーションの設定ファイルの保存場所
        /// </summary>
        private static string SettingPath
        {
            get
            {
                // アプリケーションの実行ディレクトリ + アプリケーションの設定ファイル
                return AssemblyUtility.GetExecutablePath() + @"\" + SETTING_PATH;
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
                                    string id = "";
                                    string name = "";
                                    Station.StationKind stationKind = Station.StationKind.RssPodcast;

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

                                        alStation.Add(new Station(id, name, stationKind));
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
                                    if (use == bool.TrueString)
                                    {
                                        ProxyUse = true;
                                    }
                                    else if (use == bool.FalseString)
                                    {
                                        ProxyUse = false;
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
                catch (XmlException)
                {
                    throw;
                }
                catch (IOException)
                {
                    throw;
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

                writer.WriteStartElement("DownloadBuffer");
                writer.WriteAttributeString("size", ProxyPort.ToString());
                writer.WriteEndElement(); // End of DownloadBuffer

                writer.WriteEndElement(); // End of Content.

                writer.WriteEndElement(); // End of Setting.

                writer.WriteEndDocument();
            }
            catch (IOException)
            {
                throw;
            }
            finally
            {
                writer.Close();
                fs.Close();
            }
        }
    }
}
