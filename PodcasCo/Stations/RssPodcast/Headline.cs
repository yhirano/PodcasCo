#region ディレクティブを使用する

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Xml;
using PodcasCo.Stations;
using MiscPocketCompactLibrary.Net;

#endregion

namespace PodcasCo.Stations.RssPodcast
{
    public class Headline : PodcasCo.Stations.IHeadline
    {
        /// <summary>
        /// ヘッドラインの種類
        /// </summary>
        private const string KIND_NAME = "Podcast";

        /// <summary>
        /// ヘッドラインのID（ヘッドラインを識別するためのキー）
        /// </summary>
        private readonly string id;

        /// <summary>
        /// ヘッドラインの設定
        /// </summary>
        private UserSetting setting;

        /// <summary>
        /// チャンネルのリスト
        /// </summary>
        private Channel[] channels = new Channel[0];

        /// <summary>
        /// ヘッドラインを取得した時間
        /// </summary>
        private DateTime lastCheckTime = DateTime.MinValue;

        /// <summary>
        /// 親放送局
        /// </summary>
        private readonly Station parentStation;

        /// <summary>
        /// 親放送局
        /// </summary>
        public virtual Station ParentStation
        {
            get { return parentStation; }
        }


        /// <summary>
        /// ヘッドラインのコンストラクタ
        /// </summary>
        /// <param name="id">ヘッドラインのID</param>
        /// <param name="parentStation">親放送局</param>
        public Headline(string id, Station parentStation)
        {
            this.id = id;
            this.parentStation = parentStation;
            setting = new UserSetting(this);

            try
            {
                setting.LoadSetting();
            }
            catch (XmlException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
        }

        /// <summary>
        /// 起動時の初期化メソッド。
        /// PodcastのMIMEタイプの優先度テーブルを初期化する。
        /// </summary>
        public static void StartUpInitialize()
        {
            try
            {
                RssPodcastMimePriority.Initialize();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        /// <summary>
        /// ヘッドラインから指定の番組を削除する
        /// </summary>
        /// <param name="channel">削除する番組</param>
        public void RemoveChannel(Channel channel)
        {
            ArrayList alChannels = new ArrayList(channels);
            alChannels.Remove(channel);

            channels = (Channel[])alChannels.ToArray(typeof(Channel));
        }

        /// <summary>
        /// ヘッドラインのIDを返す
        /// </summary>
        /// <returns>ヘッドラインのID</returns>
        public virtual string GetId()
        {
            return id;
        }

        /// <summary>
        /// ヘッドラインの種類の名前を返す
        /// </summary>
        /// <returns>ヘッドラインの種類の名前</returns>
        public virtual string GetKindName()
        {
            return KIND_NAME;
        }

        /// <summary>
        /// ヘッドラインのURLをセットする
        /// </summary>
        /// <param name="url">ヘッドラインのURL</param>
        public virtual void SetUrl(Uri url)
        {
            setting.RssUrl = url;
        }

        /// <summary>
        /// 取得している番組のリストを返す
        /// </summary>
        /// <returns>番組のリスト</returns>
        public virtual IChannel[] GetChannels()
        {
            return channels;
        }

        /// <summary>
        /// 番組を追加する
        /// </summary>
        /// <param name="channels">追加する番組</param>
        public virtual void AddChannel(IChannel channel)
        {
            ArrayList alChannels = new ArrayList(this.channels.Length + 1);
            alChannels.AddRange(this.channels);
            alChannels.Add(channel);

            channels = (Channel[])alChannels.ToArray(typeof(Channel));
        }

        /// <summary>
        /// 番組を破棄する
        /// </summary>
        public virtual void ClearChannels()
        {
            channels = new Channel[0];
        }

        /// <summary>
        /// ヘッドラインの設定を保存する
        /// </summary>
        public virtual void SaveSetting()
        {
            setting.SaveSetting();
        }

        /// <summary>
        /// 放送局名をネットから取得して返す
        /// </summary>
        /// <returns>ネットから取得した放送局名</returns>
        public virtual string FetchStationName()
        {
            WebStream st = null;
            XmlReader reader = null;

            try
            {
                string title = null;

                // itemタグの中にいるか
                bool inItemFlag = false;
                // channelタグの中にいるか
                bool inChannelFlag = false;

                st = PodcasCoUtility.GetWebStream(setting.RssUrl);
                reader = new XmlTextReader(st);

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.LocalName == "channel")
                        {
                            inChannelFlag = true;
                        }
                        // トップ下のtitleの内容を返す
                        if (reader.LocalName == "title" && inChannelFlag == true && inItemFlag == false)
                        {
                            title = reader.ReadString();
                            return title;
                        } // End of title
                        else if (reader.LocalName == "item")
                        {
                            inItemFlag = true;
                        } // End of item
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.LocalName == "channel")
                        {
                            inChannelFlag = false;
                        }
                        else if (reader.LocalName == "item")
                        {
                            inItemFlag = false;
                        }
                    }
                }

                // トップ下のtitleが見あたらないために、とりあえずUrlを返しておく
                return ((title != null) ? title : setting.RssUrl.ToString());
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
            catch (XmlException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (NotSupportedException)
            {
                // NotSupportedExceptionが何故起こるのかは不明。
                // 存在するがRSSでない場合に起こるようではあるが、似た実装をしているFetchHeadline()
                // では起こらないようである。
                throw;
            }
            finally
            {
                if (st != null)
                {
                    st.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// ヘッドラインをネットから取得する
        /// </summary>
        public virtual void FetchHeadline()
        {
            // ローカルヘッドラインで、RSSが見あたらない場合は何もしない
            if (setting.RssUrl.IsFile == true && File.Exists(setting.RssUrl.LocalPath) == false)
            {
                return;
            }

            // 時刻をセットする
            lastCheckTime = DateTime.Now;

            WebStream st = null;
            FileStream fs = null;
            XmlTextReader reader = null;

            try
            {
                // 番組のリスト
                ArrayList alChannels = new ArrayList();

                // チャンネル
                Channel channel = null;
                // itemタグの中にいるか
                bool inItemFlag = false;

                // Enclosureの一時リスト
                ArrayList alTempEnclosure = new ArrayList();

                if (setting.RssUrl.IsFile == true)
                {
                    fs = new FileStream(setting.RssUrl.LocalPath, FileMode.Open, FileAccess.Read);
                    reader = new XmlTextReader(fs);
                }
                else
                {
                    st = PodcasCoUtility.GetWebStream(setting.RssUrl);
                    reader = new XmlTextReader(st);
                }

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.LocalName.Equals("item"))
                        {
                            inItemFlag = true;
                            channel = new Channel(this);
                        } // End of item
                        // itemタグの中にいる場合
                        else if (inItemFlag == true)
                        {
                            if (reader.LocalName == "title")
                            {
                                channel.Title = reader.ReadString();
                            } // End of title
                            else if (reader.LocalName == "description")
                            {
                                channel.Description = reader.ReadString();
                            } // End of description
                            else if (reader.LocalName == "link")
                            {
                                try
                                {
                                    channel.Link = new Uri(reader.ReadString());
                                }
                                catch (UriFormatException)
                                {
                                    ;
                                }
                            } // End of link
                            else if (reader.LocalName == "pubDate")
                            {
                                channel.SetDate(reader.ReadString());
                            } // End of pubDate
                            else if (reader.LocalName == "category")
                            {
                                channel.Category = reader.ReadString();
                            } // End of category
                            else if (reader.LocalName == "author")
                            {
                                channel.Author = reader.ReadString();
                            } // End of author
                            else if (reader.LocalName == "guid")
                            {
                                try
                                {
                                    channel.Link = new Uri(reader.ReadString());
                                }
                                catch (UriFormatException)
                                {
                                    ;
                                }
                            } // End of guid
                            else if (reader.LocalName == "enclosure")
                            {
                                Uri enclosureUrl = null;
                                string enclosureLength = "";
                                string enclosureType = "";

                                try
                                {
                                    if (reader.MoveToFirstAttribute())
                                    {
                                        enclosureUrl = new Uri(reader.GetAttribute("url"));
                                        enclosureLength = reader.GetAttribute("length");
                                        enclosureType = reader.GetAttribute("type");
                                    }

                                    if (enclosureLength == null)
                                    {
                                        enclosureLength = string.Empty;
                                    }
                                    if (enclosureType == null)
                                    {
                                        enclosureType = string.Empty;
                                    }

                                    // Enclosureタグの数だけ、 Enclosure一時リストにEnclosureの内容を追加していく
                                    Enclosure enclosure = new Enclosure(enclosureUrl, enclosureLength, enclosureType);
                                    if (enclosure.IsPodcast() == true)
                                    {
                                        alTempEnclosure.Add(enclosure);
                                    }
                                }
                                catch (UriFormatException)
                                {
                                    ;
                                }
                            } // End of enclosure
                        } // End of itemタグの中にいる場合
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.LocalName == "item")
                        {
                            inItemFlag = false;

                            // Enclosureの要素の数だけ、Channelの複製を作る
                            if (alTempEnclosure.Count != 0)
                            {
                                foreach (Enclosure enclosure in alTempEnclosure)
                                {
                                    Channel clonedChannel = (Channel)channel.Clone(this);
                                    clonedChannel.Url = enclosure.Url;
                                    clonedChannel.Length = enclosure.Length;
                                    clonedChannel.Type = enclosure.Type;
                                    alChannels.Add(clonedChannel);
                                }
                            }

                            // Enclosure一時リストをクリア
                            alTempEnclosure.Clear();
                        }
                    }
                }

                channels = (Channel[])alChannels.ToArray(typeof(Channel));
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
            catch (XmlException)
            {
                // ローカルヘッドラインの解析に失敗した場合は、そのファイルを削除する
                if (setting.RssUrl.IsFile == true)
                {
                    File.Delete(setting.RssUrl.LocalPath);
                }
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            finally
            {
                if (st != null)
                {
                    st.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// ヘッドラインの情報からRssを作成する。
        /// </summary>
        /// <param name="file">Rssのファイル名</param>
        public virtual void GenerateRss(string file)
        {
            FileStream fs = null;
            XmlTextWriter writer = null;

            try
            {
                if (Directory.Exists(Path.GetDirectoryName(file)) == false)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(file));
                }

                fs = new FileStream(file, FileMode.Create, FileAccess.Write);
                writer = new XmlTextWriter(fs, Encoding.GetEncoding("utf-8"));

                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument(true);

                writer.WriteStartElement("rss");
                writer.WriteAttributeString("version", "2.0");

                writer.WriteStartElement("channel");

                writer.WriteStartElement("title");
                writer.WriteString(parentStation.Name);
                writer.WriteEndElement(); // End of title.

                writer.WriteStartElement("pubDate");
                writer.WriteString(DateTime.Now.ToString("ddd, d MMM yyyy HH':'mm':'ss zzz",
                    System.Globalization.DateTimeFormatInfo.InvariantInfo));
                writer.WriteEndElement(); // End of pubDate.

                foreach (Channel channel in GetChannels())
                {
                    writer.WriteStartElement("item");

                    writer.WriteStartElement("title");
                    writer.WriteString(channel.Title);
                    writer.WriteEndElement(); // End of title.

                    if (channel.GetWebsiteUrl() != null)
                    {
                        writer.WriteStartElement("link");
                        writer.WriteString(channel.GetWebsiteUrl().ToString());
                        writer.WriteEndElement(); // End of link.
                    }

                    writer.WriteStartElement("description");
                    writer.WriteString(channel.Description);
                    writer.WriteEndElement(); // End of description.

                    writer.WriteStartElement("category");
                    writer.WriteString(channel.Category);
                    writer.WriteEndElement(); // End of category.

                    writer.WriteStartElement("pubDate");
                    writer.WriteString(channel.Date.ToString("ddd, d MMM yyyy HH':'mm':'ss zzz",
                        System.Globalization.DateTimeFormatInfo.InvariantInfo));
                    writer.WriteEndElement(); // End of pubDate.

                    writer.WriteStartElement("enclosure");
                    writer.WriteAttributeString("url", channel.GetPlayUrl().ToString());
                    writer.WriteAttributeString("length", channel.Length);
                    writer.WriteAttributeString("type", channel.Type);
                    writer.WriteEndElement(); // End of enclosure.

                    writer.WriteEndElement(); // End of item.
                }

                writer.WriteEndElement(); // End of channel.
                writer.WriteEndElement(); // End of rss.

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


        /// <summary>
        /// ヘッドラインをネットから取得した時刻を返す。
        /// 未取得の場合はDateTime.MinValueを返す。
        /// </summary>
        /// <returns>ヘッドラインをネットから取得した時刻</returns>
        public virtual DateTime GetLastCheckTime()
        {
            return lastCheckTime;
        }

        /// <summary>
        /// クリップを保存しているディレクトリを削除する。
        /// クリップファイルも削除する。
        /// </summary>
        public virtual void DeleteClipDirectory()
        {
            if (Directory.Exists(PodcasCo.UserSetting.PodcastClipDirectoryPath + @"\" + id))
            {
                Directory.Delete(PodcasCo.UserSetting.PodcastClipDirectoryPath + @"\" + id, true);
            }
        }

        /// <summary>
        /// 設定を保存していたファイルを削除する
        /// </summary>
        public virtual void DeleteUserSettingFile()
        {
            setting.DeleteUserSettingFile();
        }

        /// <summary>
        /// RSSのEnclosure要素
        /// </summary>
        private class Enclosure
        {
            Uri url;

            public Uri Url
            {
                get { return url; }
            }

            string length;

            public string Length
            {
                get { return length; }
            }

            string type;

            public string Type
            {
                get { return type; }
            }

            public Enclosure(Uri url, string length, string type)
            {
                this.url = url;
                this.length = length;
                this.type = type;
            }

            /// <summary>
            /// このEnclosure要素は再生可能なPodcastかを判断する
            /// </summary>
            /// <returns>このEnclosure要素は再生可能なPodcastが再生可能な場合はtrue。</returns>
            public bool IsPodcast()
            {
                if (type == null || type == string.Empty)
                {
                    return false;
                }

                return ((RssPodcastMimePriority.GetRssPodcastMimePriority(type) != 0) ? true : false);
            }
        }
    }
}
