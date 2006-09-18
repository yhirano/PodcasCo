#region �f�B���N�e�B�u���g�p����

using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using System.Diagnostics;
using PodcasCo.Utility;

#endregion

namespace PodcasCo
{
    /// <summary>
    /// PocketLadio�̐ݒ��ێ�����N���X
    /// </summary>
    public sealed class UserSetting
    {
        /// <summary>
        /// Podcast���N���b�v����f�B���N�g��
        /// </summary>
        private static string podcastClipDirectoryPath = PocketLadioUtility.GetExecutablePath() + @"\" + PodcasCoInfo.ApplicationName + "Clip";

        /// <summary>
        /// Podcast���N���b�v����f�B���N�g��
        /// </summary>
        public static string PodcastClipDirectoryPath
        {
            get { return UserSetting.podcastClipDirectoryPath; }
            set { UserSetting.podcastClipDirectoryPath = value; }
        }

        /// <summary>
        /// �Đ����ɍ쐬����m3u�t�@�C����
        /// </summary>
        private const string GENERATE_M3U_FILE_NAME = "PodcasCo_podcast_list.m3u";

        /// <summary>
        /// �Đ����ɍ쐬����m3u�t�@�C���̃p�X
        /// </summary>
        public static string M3uFilePath
        {
            get { return podcastClipDirectoryPath + @"\" + GENERATE_M3U_FILE_NAME; }
        }

        /// <summary>
        /// �����Đ��p�̃��f�B�A�v���[���[�̃t�@�C���p�X
        /// </summary>
        private static string mediaPlayerPath = @"\Program Files\TCPMP\player.exe";

        /// <summary>
        /// �����Đ��p�̃��f�B�A�v���[���[�̃t�@�C���p�X
        /// </summary>
        public static string MediaPlayerPath
        {
            get { return UserSetting.mediaPlayerPath; }
            set { UserSetting.mediaPlayerPath = value; }
        }

        /// <summary>
        /// Web�u���E�U�̃t�@�C���p�X
        /// </summary>
        private static string browserPath = @"\Windows\iexplore.exe";

        /// <summary>
        /// Web�u���E�U�̃t�@�C���p�X
        /// </summary>
        public static string BrowserPath
        {
            get { return UserSetting.browserPath; }
            set { UserSetting.browserPath = value; }
        }

        /// <summary>
        /// �v���L�V���g�p���邩
        /// </summary>
        private static bool proxyUse;

        /// <summary>
        /// �v���L�V���g�p���邩
        /// </summary>
        public static bool ProxyUse
        {
            get { return UserSetting.proxyUse; }
            set { UserSetting.proxyUse = value; }
        }

        /// <summary>
        /// �v���L�V�̃T�[�o��
        /// </summary>
        private static string proxyServer = "";

        /// <summary>
        /// �v���L�V�̃T�[�o��
        /// </summary>
        public static string ProxyServer
        {
            get { return proxyServer; }
            set { proxyServer = value; }
        }

        /// <summary>
        /// �v���L�V�̃|�[�g�ԍ�
        /// </summary>
        private static string proxyPort = "";

        /// <summary>
        /// �v���L�V�̃|�[�g�ԍ�
        /// </summary>
        public static string ProxyPort
        {
            get
            {
                try
                {
                    if (0x00 <= int.Parse(proxyPort) && int.Parse(proxyPort) <= 0xFFFF)
                    {
                        return proxyPort;
                    }
                    else
                    {
                        return "";
                    }
                }
                catch (ArgumentException)
                {
                    return "";
                }
                catch (FormatException)
                {
                    return "";
                }
                catch (OverflowException)
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    if (0x00 <= int.Parse(value) && int.Parse(value) <= 0xFFFF)
                    {
                        proxyPort = value;
                    }
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

        }

        /// <summary>
        /// �A�v���P�[�V�����̐ݒ�t�@�C��
        /// </summary>
        private const string settingPath = "Setting.xml";

        /// <summary>
        /// �A�v���P�[�V�����̐ݒ�t�@�C���̕ۑ��ꏊ
        /// </summary>
        private static string SettingPath
        {
            get
            {
                // �A�v���P�[�V�����̎��s�f�B���N�g�� + �A�v���P�[�V�����̐ݒ�t�@�C��
                return PocketLadioUtility.GetExecutablePath() + @"\" + settingPath;
            }
        }

        /// <summary>
        /// �V���O���g���̂��߃v���C�x�[�g
        /// </summary>
        private UserSetting()
        {
        }

        /// <summary>
        /// �ݒ���t�@�C������ǂݍ���
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

                    // StationList�^�O�̒��ɂ��邩
                    bool inStationListFlag = false;

                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {

                            if (reader.LocalName == "StationList")
                            {
                                inStationListFlag = true;
                            } // End of StationList
                            // StationList�^�O�̒��ɂ���ꍇ
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
                                            // �����ɓ��B���邱�Ƃ͂��蓾�Ȃ�
                                            Trace.Assert(false, "�z��O�̓���̂��߁A�I�����܂�");
                                        }

                                        alStation.Add(new Station(id, name, stationKind));
                                    }
                                } // End of Station
                            } // End of StationList�^�O�̒��ɂ���ꍇ
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
                                    ProxyPort = reader.GetAttribute("port");
                                }
                            } // End of Proxy
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
        /// �ݒ���t�@�C���ɕۑ�
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
                writer.WriteAttributeString("port", ProxyPort);
                writer.WriteEndElement(); // End of Porxy

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
