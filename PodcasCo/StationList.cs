#region ディレクティブを使用する

using System;
using System.Collections;
using PodcasCo.Stations;

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
            get { return (currentStation != null ? currentStation.Id : ""); }
        }

        /// <summary>
        /// 放送局の名前を返す
        /// </summary>
        public static string StationNameOfCurrentStation
        {
            get { return (currentStation != null ? currentStation.Name : ""); }
        }

        /// <summary>
        /// シングルトンのためprivate
        /// </summary>
        private StationList()
        {
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
            StationList.stations = stations;

            // 現在の放送局をクリアする
            currentStation = null;
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
                Station[] stations = GetStationList();
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
                        if (globalChannel.GetHash() == localChannel.GetHash()) {
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
        public static void FetchHeadlineOfCurrentStations()
        {
            foreach (Station station in GetStationList())
            {
                station.LocalHeadline.FetchHeadline();
                station.GlobalHeadline.FetchHeadline();
            }
        }

        /// <summary>
        /// 全放送局のローカルにあるヘッドラインを取得する
        /// </summary>
        public static void FetchLocalHeadlineOfCurrentStations()
        {
            foreach (Station station in GetStationList())
            {
                station.LocalHeadline.FetchHeadline();
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
        /// 番組の詳細フォームを表示する
        /// </summary>
        /// <param name="channel">番組</param>
        public static void ShowPropertyFormOfCurrentStation(IChannel channel)
        {
            channel.ShowPropertyForm();
        }
    }
}
