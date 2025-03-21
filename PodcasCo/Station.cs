﻿#region ディレクティブを使用する

using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using PodcasCo.Stations;

#endregion

namespace PodcasCo
{
    /// <summary>
    /// 放送局
    /// </summary>
    public class Station
    {
        /// <summary>
        /// 放送局のID
        /// </summary>
        private string id;

        /// <summary>
        /// 放送局のID
        /// </summary>
        public string Id
        {
            get { return id; }
        }

        /// <summary>
        /// 放送局の名前
        /// </summary>
        private string name;

        /// <summary>
        /// ローカルのヘッドライン（ローカルファイル）
        /// </summary>
        private IHeadline localHeadline;

        /// <summary>
        /// グローバルのヘッドライン（ネット上）
        /// </summary>
        private IHeadline globalHeadline;

        /// <summary>
        /// 放送局の種類
        /// </summary>
        private StationKind kind;

        /// <summary>
        /// 放送局の種類列挙
        /// </summary>
        public enum StationKind
        {
            RssPodcast
        };

        /// <summary>
        /// 表示用の名前
        /// </summary>
        public string DisplayName
        {
            get { return name; }
        }

        /// <summary>
        /// 所持しているローカルヘッドライン
        /// </summary>
        public IHeadline LocalHeadline
        {
            get { return localHeadline; }
        }

        /// <summary>
        /// 所持しているグローバルヘッドライン
        /// </summary>
        public IHeadline GlobalHeadline
        {
            get { return globalHeadline; }
        }

        /// <summary>
        /// 放送局の名前
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// 放送局の種類を返す
        /// </summary>
        public StationKind Kind
        {
            get { return kind; }
        }

        /// <summary>
        /// 起動時にヘッドラインをダウンロードするか
        /// </summary>
        private bool startupDownload = false;

        /// <summary>
        /// 起動時にヘッドラインをダウンロードするか
        /// </summary>
        public bool StartupDownload
        {
            get { return startupDownload; }
            set { startupDownload = value; }
        }

        /// <summary>
        /// 起動時にこの放送局がダウンロードするヘッドラインの数の下限値
        /// </summary>
        public const int STARTUP_DOWNLOAD_NUM_MIN = 1;

        /// <summary>
        /// 起動時にこの放送局がダウンロードするヘッドラインの数
        /// </summary>
        private int startupDownloadNum = STARTUP_DOWNLOAD_NUM_MIN;

        /// <summary>
        /// 起動時にこの放送局がダウンロードするヘッドラインの数
        /// </summary>
        public int StartupDownloadNum
        {
            get { return startupDownloadNum; }
            set {
                if (value >= STARTUP_DOWNLOAD_NUM_MIN)
                {
                    startupDownloadNum = value;
                }
                else
                {
                    startupDownloadNum = STARTUP_DOWNLOAD_NUM_MIN;
                }
            }
        }

        /// <summary>
        /// 起動時にヘッドラインを削除するか
        /// </summary>
        private bool startupDelete = false;

        /// <summary>
        /// 起動時にヘッドラインを削除するか
        /// </summary>
        public bool StartupDelete
        {
            get { return startupDelete; }
            set { startupDelete = value; }
        }

        /// <summary>
        /// 起動時にこの放送局がこれよりも古いヘッドラインを削除する日数の下限値
        /// </summary>
        public const int STARTUP_DELETE_REMAIN_DAY_MIN = 1;

        /// <summary>
        /// 起動時にヘッドラインを削除する場合に、残す番組の日数。
        /// 起動時の時刻からこの日数以内の番組は削除されない。
        /// </summary>
        private int startupDeleteRemainDay = 7;

        /// <summary>
        /// 起動時にヘッドラインを削除する場合に、残す番組の日数。
        /// 起動時の時刻からこの日数以内の番組は削除されない。
        /// </summary>
        public int StartupDeleteRemainDay
        {
            get { return startupDeleteRemainDay; }
            set
            {
                if (value >= STARTUP_DELETE_REMAIN_DAY_MIN)
                {
                    startupDeleteRemainDay = value;
                }
                else
                {
                    startupDeleteRemainDay = STARTUP_DELETE_REMAIN_DAY_MIN;
                }
            }
        }

        /// <summary>
        /// 放送局のコンストラクタ
        /// </summary>
        /// <param name="id">放送局のID</param>
        /// <param name="name">放送局の名前</param>
        /// <param name="stationKind">放送局の種類</param>
        public Station(string id, string name, StationKind stationKind)
        {
            this.id = id;
            this.name = name;
            this.kind = stationKind;

            if (kind.Equals(StationKind.RssPodcast))
            {
                globalHeadline = new PodcasCo.Stations.RssPodcast.Headline(id + "-global", this);
                localHeadline = new PodcasCo.Stations.RssPodcast.Headline(id + "-local", this);
                localHeadline.FetchHeadline();
            }
            else
            {
                // ここには到達しない
                Trace.Assert(false, "想定外の動作のため、終了します");
            }
        }

        /// <summary>
        /// 放送局のコンストラクタ
        /// </summary>
        /// <param name="id">放送局のID</param>
        /// <param name="name">放送局の名前</param>
        /// <param name="stationKind">放送局の種類</param>
        /// <param name="url">放送局のURL</param>
        public Station(string id, string name, StationKind stationKind, Uri url)
        {
            this.id = id;
            this.name = name;
            this.kind = stationKind;

            if (kind.Equals(StationKind.RssPodcast))
            {
                globalHeadline = new PodcasCo.Stations.RssPodcast.Headline(id + "-global", this);
                globalHeadline.SetUrl(url);
                localHeadline = new PodcasCo.Stations.RssPodcast.Headline(id + "-local", this);
                localHeadline.SetUrl(new Uri(UserSetting.PodcastClipDirectoryPath + @"\" + localHeadline.GetId() + @"\" + PodcasCoInfo.LocalRssFile));
                localHeadline.FetchHeadline();

                /*
                 * 放送局の名前を得る（URLがNotFoundや、存在してもRSSで無いっぽい場合は例外が投げられるため
                 * 以下の設定保存は実行されない）
                 */
                FetchName();

                localHeadline.SaveSetting();
                globalHeadline.SaveSetting();
            }
            else
            {
                // ここには到達しない
                Trace.Assert(false, "想定外の動作のため、終了します");
            }
        }

        /// <summary>
        /// 放送局名を得る
        /// </summary>
        private void FetchName()
        {
            name = globalHeadline.FetchStationName();
        }

        /// <summary>
        /// channelがローカルヘッドラインに含まれるかを返す。
        /// </summary>
        /// <param name="channel">channel</param>
        /// <returns>channelがローカルヘッドラインに含まれる場合はtrue、そうでない場合はfalse。</returns>
        public bool ContainLocalHeadline(IChannel channel)
        {
            foreach (IChannel ch in LocalHeadline.GetChannels())
            {
                if (ch == channel)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
