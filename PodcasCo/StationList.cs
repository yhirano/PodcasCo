#region ディレクティブを使用する

using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using PodcasCo.Stations;
using MiscPocketCompactLibrary.Net;

#endregion

namespace PodcasCo
{
    /// <summary>
    /// 放送局のリスト
    /// </summary>
    public sealed class StationList
    {
        /// <summary>
        /// 放送局のリスト
        /// </summary>
        private static Station[] stations = new Station[0];

        /// <summary>
        /// 現在の放送局
        /// </summary>
        private static Station currentStation;

        /// <summary>
        /// 放送局のIDを返す
        /// </summary>
        public static string StationIdOfCurrentStation
        {
            get { return (currentStation != null ? currentStation.Id : string.Empty); }
        }

        /// <summary>
        /// 放送局の名前を返す
        /// </summary>
        public static string StationNameOfCurrentStation
        {
            get { return (currentStation != null ? currentStation.Name : string.Empty); }
        }

        /// <summary>
        /// 解析中の放送局
        /// </summary>
        private static Station fetchingStation;

        /// <summary>
        /// 解析中の放送局の名前
        /// </summary>
        public static string FetchingStationName
        {
            get { return (fetchingStation != null ? fetchingStation.Name : string.Empty); }
        }

        /// <summary>
        /// シングルトンのためprivate
        /// </summary>
        private StationList()
        {
        }

        /// <summary>
        /// 現在の放送局を変更する。
        /// 指定した放送局が無い場合は、現在の放送局は変更されない。
        /// nullを指定すると、全放送局が対象となる。
        /// </summary>
        /// <param name="station">現在の放送局にする放送局</param>
        public static void ChangeCurrentStation(Station station)
        {
            if (station != null)
            {
                // 指定された放送局が放送局リストの中に存在する場合は、
                // 指定された放送局を現在の放送局にする
                foreach (Station st in stations)
                {
                    if (st == station)
                    {
                        currentStation = station;
                        break;
                    }
                }
            }
            else
            {
                // 全放送局にする
                currentStation = null;
            }
        }

        /// <summary>
        /// 現在の放送局を変更する。
        /// -1を指定すると全放送局が対象となる。
        /// </summary>
        /// <param name="number">何番目の放送局か（0から始まる）。-1を指定すると全放送局が対象となる。</param>
        public static void ChangeCurrentStationAt(int number)
        {
            currentStation = ((number >= 0) ? stations[number] : null);
        }

        /// <summary>
        /// 放送局のリストを返す
        /// </summary>
        /// <returns>放送局のリスト</returns>
        public static Station[] GetStationList()
        {
            return stations;
        }

        /// <summary>
        /// 放送局のリストをセットする
        /// </summary>
        /// <param name="Stations">放送局のリスト</param>
        /// <returns></returns>
        public static void SetStationList(Station[] stations)
        {
            // 放送局に変化があったか
            bool changed = false;

            // 放送局に変化があったかを調べる
            if (stations.Length != StationList.stations.Length)
            {
                changed = true;
            }
            else
            {
                for (int i = 0; i < stations.Length && i < StationList.stations.Length; ++i)
                {
                    if (stations[i] != StationList.stations[i])
                    {
                        changed = true;
                        break;
                    }
                }
            }

            if (changed == true)
            {
                StationList.stations = stations;

                // 現在の放送局をクリアする
                currentStation = null;
                
                OnStationListChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// 取得している番組のリストを返す
        /// </summary>
        /// <returns>番組のリスト</returns>
        public static IChannel[] GetChannelsOfCurrentStationFromAllHeadline()
        {
            return ChannelMarge(GetUnclipedChannelsOfCurrentStation(),
                GetChannelsOfCurrentStationFromLocalHeadline());
        }

        /// <summary>
        /// 取得している番組のリストで、グローバル（ネット上）にある番組を返す
        /// </summary>
        /// <returns>番組のリスト</returns>
        public static IChannel[] GetChannelsOfCurrentStationFromGlobalHeadline()
        {
            // 放送局が選択されている場合
            if (currentStation != null)
            {
                return currentStation.GlobalHeadline.GetChannels();
            }
            // 放送局が選択されていない場合
            else
            {

                IChannel[] channels = new IChannel[0];

                foreach (Station station in GetStationList())
                {
                    channels = ChannelMarge(channels, station.GlobalHeadline.GetChannels());
                }

                return channels;
            }
        }

        /// <summary>
        /// 取得している番組のリストで、ローカルにある番組を返す
        /// </summary>
        /// <returns>番組のリスト</returns>
        public static IChannel[] GetChannelsOfCurrentStationFromLocalHeadline()
        {
            // 放送局が選択されている場合
            if (currentStation != null)
            {
                return currentStation.LocalHeadline.GetChannels();
            }
            // 放送局が選択されていない場合
            else
            {
                IChannel[] channels = new IChannel[0];
                foreach (Station station in GetStationList())
                {
                    channels = ChannelMarge(channels, station.LocalHeadline.GetChannels());
                }

                return channels;
            }
        }

        /// <summary>
        /// 取得している番組のリストで、未クリップな番組を返す
        /// </summary>
        /// <returns>取得している番組のリストで、未クリップな番組</returns>
        public static IChannel[] GetUnclipedChannelsOfCurrentStation()
        {
            // 放送局が選択されている場合
            if (currentStation != null)
            {
                ArrayList alGlobalHeadlineChannels = new ArrayList(GetChannelsOfCurrentStationFromGlobalHeadline());

                foreach (IChannel globalChannel in GetChannelsOfCurrentStationFromGlobalHeadline())
                {
                    foreach (IChannel localChannel in GetChannelsOfCurrentStationFromLocalHeadline())
                    {
                        if (globalChannel.GetHash() == localChannel.GetHash())
                        {
                            alGlobalHeadlineChannels.Remove(globalChannel);
                        }
                    }
                }

                return (IChannel[])alGlobalHeadlineChannels.ToArray(typeof(IChannel));
            }
            // 放送局が選択されていない場合
            else
            {
                ArrayList alChannels = new ArrayList();

                foreach (Station station in GetStationList())
                {
                    ArrayList alGlobalHeadlineChannels = new ArrayList(station.GlobalHeadline.GetChannels());

                    foreach (IChannel globalChannel in station.GlobalHeadline.GetChannels())
                    {
                        foreach (IChannel localChannel in station.LocalHeadline.GetChannels())
                        {
                            if (globalChannel.GetHash() == localChannel.GetHash())
                            {
                                alGlobalHeadlineChannels.Remove(globalChannel);
                            }
                        }
                    }
                    alChannels.AddRange(alGlobalHeadlineChannels);
                }

                return (IChannel[])alChannels.ToArray(typeof(IChannel));
            }
        }

        /// <summary>
        /// 二つの番組をマージし、マージした結果を返す。
        /// </summary>
        /// <param name="channel1">番組1</param>
        /// <param name="channel2">番組2</param>
        /// <returns>二つの番組をマージした配列</returns>
        private static IChannel[] ChannelMarge(IChannel[] channel1, IChannel[] channel2)
        {
            ArrayList alChannels = new ArrayList(channel1.Length + channel2.Length);
            alChannels.AddRange(channel1);
            alChannels.AddRange(channel2);

            return (IChannel[])alChannels.ToArray(typeof(IChannel));
        }

        /// <summary>
        /// 全放送局のヘッドラインを取得する
        /// </summary>
        public static void FetchHeadlineOnStations()
        {
            foreach (Station station in GetStationList())
            {
                if (HeadlineFetch != null)
                {
                    station.LocalHeadline.HeadlineFetch += HeadlineFetch;
                    station.GlobalHeadline.HeadlineFetch += HeadlineFetch;
                }
                if (HeadlineFetching != null)
                {
                    station.LocalHeadline.HeadlineFetching += HeadlineFetching;
                    station.GlobalHeadline.HeadlineFetching += HeadlineFetching;
                }
                if (HeadlineFetched != null)
                {
                    station.LocalHeadline.HeadlineFetched += HeadlineFetched;
                    station.GlobalHeadline.HeadlineFetched += HeadlineFetched;
                }
                if (HeadlineAnalyze != null)
                {
                    station.LocalHeadline.HeadlineAnalyze += HeadlineAnalyze;
                    station.GlobalHeadline.HeadlineAnalyze += HeadlineAnalyze;
                }
                if (HeadlineAnalyzing != null)
                {
                    station.LocalHeadline.HeadlineAnalyzing += HeadlineAnalyzing;
                    station.GlobalHeadline.HeadlineAnalyzing += HeadlineAnalyzing;
                }
                if (HeadlineAnalyzed != null)
                {
                    station.LocalHeadline.HeadlineAnalyzed += HeadlineAnalyzed;
                    station.GlobalHeadline.HeadlineAnalyzed += HeadlineAnalyzed;
                }
                
                fetchingStation = station;

                station.LocalHeadline.FetchHeadline();
                station.GlobalHeadline.FetchHeadline();

                if (HeadlineFetch != null)
                {
                    station.LocalHeadline.HeadlineFetch -= HeadlineFetch;
                    station.GlobalHeadline.HeadlineFetch -= HeadlineFetch;
                }
                if (HeadlineFetching != null)
                {
                    station.LocalHeadline.HeadlineFetching -= HeadlineFetching;
                    station.GlobalHeadline.HeadlineFetching -= HeadlineFetching;
                }
                if (HeadlineFetched != null)
                {
                    station.LocalHeadline.HeadlineFetched -= HeadlineFetched;
                    station.GlobalHeadline.HeadlineFetched -= HeadlineFetched;
                }
                if (HeadlineAnalyze != null)
                {
                    station.LocalHeadline.HeadlineAnalyze -= HeadlineAnalyze;
                    station.GlobalHeadline.HeadlineAnalyze -= HeadlineAnalyze;
                }
                if (HeadlineAnalyzing != null)
                {
                    station.LocalHeadline.HeadlineAnalyzing -= HeadlineAnalyzing;
                    station.GlobalHeadline.HeadlineAnalyzing -= HeadlineAnalyzing;
                }
                if (HeadlineAnalyzed != null)
                {
                    station.LocalHeadline.HeadlineAnalyzed -= HeadlineAnalyzed;
                    station.GlobalHeadline.HeadlineAnalyzed -= HeadlineAnalyzed;
                }
            }
        }

        /// <summary>
        /// 全放送局のグローバルにあるヘッドラインを取得する
        /// </summary>
        public static void FetchGlobalHeadlineOnStations()
        {
            foreach (Station station in GetStationList())
            {
                if (HeadlineFetch != null)
                {
                    station.GlobalHeadline.HeadlineFetch += HeadlineFetch;
                }
                if (HeadlineFetching != null)
                {
                    station.GlobalHeadline.HeadlineFetching += HeadlineFetching;
                }
                if (HeadlineFetched != null)
                {
                    station.GlobalHeadline.HeadlineFetch += HeadlineFetched;
                }
                if (HeadlineAnalyze != null)
                {
                    station.GlobalHeadline.HeadlineAnalyze += HeadlineAnalyze;
                }
                if (HeadlineAnalyzing != null)
                {
                    station.GlobalHeadline.HeadlineAnalyzing += HeadlineAnalyzing;
                }
                if (HeadlineAnalyzed != null)
                {
                    station.GlobalHeadline.HeadlineAnalyzed += HeadlineAnalyzed;
                }

                fetchingStation = station;

                station.GlobalHeadline.FetchHeadline();

                if (HeadlineFetch != null)
                {
                    station.GlobalHeadline.HeadlineFetch -= HeadlineFetch;
                }
                if (HeadlineFetching != null)
                {
                    station.GlobalHeadline.HeadlineFetching -= HeadlineFetching;
                }
                if (HeadlineFetched != null)
                {
                    station.GlobalHeadline.HeadlineFetched -= HeadlineFetched;
                }
                if (HeadlineAnalyze != null)
                {
                    station.GlobalHeadline.HeadlineAnalyze -= HeadlineAnalyze;
                }
                if (HeadlineAnalyzing != null)
                {
                    station.GlobalHeadline.HeadlineAnalyzing -= HeadlineAnalyzing;
                }
                if (HeadlineAnalyzed != null)
                {
                    station.GlobalHeadline.HeadlineAnalyzed -= HeadlineAnalyzed;
                }
            }
        }

        /// <summary>
        /// 全放送局のローカルにあるヘッドラインを取得する
        /// </summary>
        public static void FetchLocalHeadlineOnStations()
        {
            foreach (Station station in GetStationList())
            {
                if (HeadlineFetch != null)
                {
                    station.LocalHeadline.HeadlineFetch += HeadlineFetch;
                }
                if (HeadlineFetching != null)
                {
                    station.LocalHeadline.HeadlineFetching += HeadlineFetching;
                }
                if (HeadlineFetched != null)
                {
                    station.LocalHeadline.HeadlineFetched += HeadlineFetched;
                }
                if (HeadlineAnalyze != null)
                {
                    station.LocalHeadline.HeadlineAnalyze += HeadlineAnalyze;
                }
                if (HeadlineAnalyzing != null)
                {
                    station.LocalHeadline.HeadlineAnalyzing += HeadlineAnalyzing;
                }
                if (HeadlineAnalyzed != null)
                {
                    station.LocalHeadline.HeadlineAnalyzed += HeadlineAnalyzed;
                }

                fetchingStation = station;

                station.LocalHeadline.FetchHeadline();

                if (HeadlineFetch != null)
                {
                    station.LocalHeadline.HeadlineFetch -= HeadlineFetch;
                }
                if (HeadlineFetching != null)
                {
                    station.LocalHeadline.HeadlineFetching -= HeadlineFetching;
                }
                if (HeadlineFetched != null)
                {
                    station.LocalHeadline.HeadlineFetched -= HeadlineFetched;
                }
                if (HeadlineAnalyze != null)
                {
                    station.LocalHeadline.HeadlineAnalyze -= HeadlineAnalyze;
                }
                if (HeadlineAnalyzing != null)
                {
                    station.LocalHeadline.HeadlineAnalyzing -= HeadlineAnalyzing;
                }
                if (HeadlineAnalyzed != null)
                {
                    station.LocalHeadline.HeadlineAnalyzed -= HeadlineAnalyzed;
                }
            }
        }

        /// <summary>
        /// 選択している放送局のヘッドラインを取得する
        /// </summary>
        public static void FetchHeadlineOfCurrentStation()
        {
            if (currentStation != null)
            {
                if (HeadlineFetch != null)
                {
                    currentStation.LocalHeadline.HeadlineFetch += HeadlineFetch;
                    currentStation.GlobalHeadline.HeadlineFetch += HeadlineFetch;
                }
                if (HeadlineFetching != null)
                {
                    currentStation.LocalHeadline.HeadlineFetching += HeadlineFetching;
                    currentStation.GlobalHeadline.HeadlineFetching += HeadlineFetching;
                }
                if (HeadlineFetched != null)
                {
                    currentStation.LocalHeadline.HeadlineFetched += HeadlineFetched;
                    currentStation.GlobalHeadline.HeadlineFetched += HeadlineFetched;
                }
                if (HeadlineAnalyze != null)
                {
                    currentStation.LocalHeadline.HeadlineAnalyze += HeadlineAnalyze;
                    currentStation.GlobalHeadline.HeadlineAnalyze += HeadlineAnalyze;
                }
                if (HeadlineAnalyzing != null)
                {
                    currentStation.LocalHeadline.HeadlineAnalyzing += HeadlineAnalyzing;
                    currentStation.GlobalHeadline.HeadlineAnalyzing += HeadlineAnalyzing;
                }
                if (HeadlineAnalyzed != null)
                {
                    currentStation.LocalHeadline.HeadlineAnalyzed += HeadlineAnalyzed;
                    currentStation.GlobalHeadline.HeadlineAnalyzed += HeadlineAnalyzed;
                }

                fetchingStation = currentStation;

                currentStation.LocalHeadline.FetchHeadline();
                currentStation.GlobalHeadline.FetchHeadline();

                if (HeadlineFetch != null)
                {
                    currentStation.LocalHeadline.HeadlineFetch -= HeadlineFetch;
                    currentStation.GlobalHeadline.HeadlineFetch -= HeadlineFetch;
                }
                if (HeadlineFetching != null)
                {
                    currentStation.LocalHeadline.HeadlineFetching -= HeadlineFetching;
                    currentStation.GlobalHeadline.HeadlineFetching -= HeadlineFetching;
                }
                if (HeadlineFetched != null)
                {
                    currentStation.LocalHeadline.HeadlineFetched -= HeadlineFetched;
                    currentStation.GlobalHeadline.HeadlineFetched -= HeadlineFetched;
                }
                if (HeadlineAnalyze != null)
                {
                    currentStation.LocalHeadline.HeadlineAnalyze -= HeadlineAnalyze;
                    currentStation.GlobalHeadline.HeadlineAnalyze -= HeadlineAnalyze;
                }
                if (HeadlineAnalyzing != null)
                {
                    currentStation.LocalHeadline.HeadlineAnalyzing -= HeadlineAnalyzing;
                    currentStation.GlobalHeadline.HeadlineAnalyzing -= HeadlineAnalyzing;
                }
                if (HeadlineAnalyzed != null)
                {
                    currentStation.LocalHeadline.HeadlineAnalyzed -= HeadlineAnalyzed;
                    currentStation.GlobalHeadline.HeadlineAnalyzed -= HeadlineAnalyzed;
                }
            }
            else
            {
                FetchHeadlineOnStations();
            }
        }

        /// <summary>
        /// 選択している放送局のグローバルにあるヘッドラインを取得する
        /// </summary>
        public static void FetchGlobalHeadlineOfCurrentStation()
        {
            if (currentStation != null)
            {
                if (HeadlineFetch != null)
                {
                    currentStation.GlobalHeadline.HeadlineFetch += HeadlineFetch;
                }
                if (HeadlineFetching != null)
                {
                    currentStation.GlobalHeadline.HeadlineFetching += HeadlineFetching;
                }
                if (HeadlineFetched != null)
                {
                    currentStation.GlobalHeadline.HeadlineFetched += HeadlineFetched;
                }
                if (HeadlineAnalyze != null)
                {
                    currentStation.GlobalHeadline.HeadlineAnalyze += HeadlineAnalyze;
                }
                if (HeadlineAnalyzing != null)
                {
                    currentStation.GlobalHeadline.HeadlineAnalyzing += HeadlineAnalyzing;
                }
                if (HeadlineAnalyzed != null)
                {
                    currentStation.GlobalHeadline.HeadlineAnalyzed += HeadlineAnalyzed;
                }

                fetchingStation = currentStation;

                currentStation.GlobalHeadline.FetchHeadline();

                if (HeadlineFetch != null)
                {
                    currentStation.GlobalHeadline.HeadlineFetch -= HeadlineFetch;
                }
                if (HeadlineFetching != null)
                {
                    currentStation.GlobalHeadline.HeadlineFetching -= HeadlineFetching;
                }
                if (HeadlineFetched != null)
                {
                    currentStation.GlobalHeadline.HeadlineFetched -= HeadlineFetched;
                }
                if (HeadlineAnalyze != null)
                {
                    currentStation.GlobalHeadline.HeadlineAnalyze -= HeadlineAnalyze;
                }
                if (HeadlineAnalyzing != null)
                {
                    currentStation.GlobalHeadline.HeadlineAnalyzing -= HeadlineAnalyzing;
                }
                if (HeadlineAnalyzed != null)
                {
                    currentStation.GlobalHeadline.HeadlineAnalyzed -= HeadlineAnalyzed;
                }
            }
            else
            {
                FetchGlobalHeadlineOnStations();
            }
        }

        /// <summary>
        /// 全放送局のローカルにあるヘッドラインの情報からRSSを作成する
        /// </summary>
        public static void GenerateRssLocalHeadlines()
        {
            foreach (Station station in GetStationList())
            {
                string rssFileName = UserSetting.PodcastClipDirectoryPath + @"\"
                    + station.LocalHeadline.GetId() + @"\" + PodcasCoInfo.LocalRssFile;
                station.LocalHeadline.GenerateRss(rssFileName);
            }
        }


        /// <summary>
        /// ダウンロード進捗（ファイル数）の最小値（0）をセットするデリゲート
        /// </summary>
        /// <param name="minimum">ダウンロード進捗の最小値</param>
        public delegate void SetDownloadProgressMinimumInvoker(int minimum);

        /// <summary>
        /// ダウンロード進捗（ファイル数）の最大値（ファイル数）をセットするデリゲート
        /// </summary>
        /// <param name="maximum">ダウンロードの進捗の最大値</param>
        public delegate void SetDownloadProgressMaximumInvoker(int maximum);

        /// <summary>
        /// ダウンロード進捗（ファイル数）の状況（すでにダウンロードしたファイル数）をセットするデリゲート
        /// </summary>
        /// <param name="value">ダウンロード進捗の状況</param>
        public delegate void SetDownloadProgressValueInvoker(int value);

        /// <summary>
        /// 番組をクリップする
        /// </summary>
        private void ClippingPodcast()
        {
            ClippingPodcast(null);
        }

        /// <summary>
        /// 番組をクリップする
        /// </summary>
        /// <param name="clippingForm">ClippingForm</param>
        public static void ClippingPodcast(ClippingForm clippingForm)
        {
            // すでにクリップした番組
            int alreadyClippingFiles = 0;

            // クリップできない番組があったか
            bool cannotCliped = false;

            // クリップできなかった番組の情報
            string cannotClipedString = string.Empty;

            // 選択された番組のリスト
            ArrayList alSelectedGlobalChannels = new ArrayList();

            // 選択された番組で未クリップの番組のリストを作る
            foreach (IChannel channel in GetUnclipedChannelsOfCurrentStation())
            {
                if (channel.Check == true)
                {
                    alSelectedGlobalChannels.Add(channel);
                }
            }

            if (clippingForm != null)
            {
                clippingForm.OnFile(new FileEventArgs(0, alSelectedGlobalChannels.Count));
            }

            foreach (IChannel channel in (IChannel[])alSelectedGlobalChannels.ToArray(typeof(IChannel)))
            {
                try
                {
                    // URLがnullの場合に対処
                    if (channel.GetPlayUrl() == null)
                    {
                        throw new WebException();
                    }

                    string directoryName = UserSetting.PodcastClipDirectoryPath
                        + @"\" + channel.ParentHeadline.ParentStation.LocalHeadline.GetId();

                    if (Directory.Exists(directoryName) == false)
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    string generateFilePath = directoryName + @"\"
                        + Path.GetFileNameWithoutExtension(channel.GetPlayUrl().LocalPath)
                        + "-" + channel.GetHash().ToString("x")
                        + Path.GetExtension(channel.GetPlayUrl().LocalPath);

                    PodcasCoUtility.FetchFile(channel.GetPlayUrl(), generateFilePath,
                        new FetchEventHandler(clippingForm.ClipReceiver),
                        new FetchEventHandler(clippingForm.ClippingReceiver),
                        null);

                    // 番組をローカルヘッドラインに加える
                    IChannel localChannel = (IChannel)channel.Clone(channel.ParentHeadline.ParentStation.LocalHeadline);
                    localChannel.SetPlayUrl(new Uri(generateFilePath));
                    localChannel.ParentHeadline.ParentStation.LocalHeadline.AddChannel(localChannel);

                    // ダウンロードした番組の元のグローバル番組からはチェックを外す
                    channel.Check = false;
                }
                catch (WebException)
                {
                    cannotCliped = true;
                    cannotClipedString += channel.ParentHeadline.ParentStation.Name + " - " + channel.GetTitle() + "\n";
                }
                catch (UriFormatException)
                {
                    cannotCliped = true;
                    cannotClipedString += channel.ParentHeadline.ParentStation.Name + " - " + channel.GetTitle() + "\n";
                }
                catch (SocketException)
                {
                    cannotCliped = true;
                    cannotClipedString += channel.ParentHeadline.ParentStation.Name + " - " + channel.GetTitle() + "\n";
                }

                if (clippingForm != null)
                {
                    clippingForm.OnFiling(new FileEventArgs(++alreadyClippingFiles, alSelectedGlobalChannels.Count));
                }
            }

            // RSSを作成
            StationList.GenerateRssLocalHeadlines();

            if (cannotCliped == true)
            {
                throw new ClippingException(cannotClipedString);
            }
        }

        /// <summary>
        /// 番組の詳細フォームを表示する
        /// </summary>
        /// <param name="channel">番組</param>
        public static void ShowPropertyFormOfCurrentStation(IChannel channel)
        {
            channel.ShowPropertyForm();
        }

        /// <summary>
        /// ヘッドラインをネットから取得する前に発生するイベント
        /// </summary>
        public static event FetchEventHandler HeadlineFetch;

        /// <summary>
        /// ヘッドラインをネットから取得している最中に発生するイベント
        /// </summary>
        public static event FetchEventHandler HeadlineFetching;

        /// <summary>
        /// ヘッドラインをネットから取得した後に発生するイベント
        /// </summary>
        public static event FetchEventHandler HeadlineFetched;

        /// <summary>
        /// ヘッドラインを解析する前に発生するイベント
        /// </summary>
        public static event HeadlineAnalyzeEventHandler HeadlineAnalyze;

        /// <summary>
        /// ヘッドラインを解析している最中に発生するイベント
        /// </summary>
        public static event HeadlineAnalyzeEventHandler HeadlineAnalyzing;

        /// <summary>
        /// ヘッドラインを解析した後に発生するイベント
        /// </summary>
        public static event HeadlineAnalyzeEventHandler HeadlineAnalyzed;

        /// <summary>
        /// StationListに変化があった場合に起こるイベント
        /// </summary>
        public static event EventHandler StationListChanged;

        private static void OnStationListChanged(EventArgs e)
        {
            if (StationListChanged != null)
            {
                StationListChanged(null, e);
            }
        }
    }
}
