﻿#region ディレクティブを使用する

using System;
using System.Collections;
using MiscPocketCompactLibrary.Net;

#endregion

namespace PodcasCo.Stations
{
    /// <summary>
    /// ヘッドライン解析のイベントハンドラ
    /// </summary>
    /// <param name="sender">送信元</param>
    /// <param name="e">イベント</param>
    public delegate void HeadlineAnalyzeEventHandler(object sender, HeadlineAnalyzeEventArgs e);

    /// <summary>
    /// ヘッドラインインターフェース
    /// </summary>
    public interface IHeadline
    {
        /// <summary>
        /// 親放送局
        /// </summary>
        Station ParentStation { get; }

        /// <summary>
        /// ヘッドラインのIDを返す
        /// </summary>
        /// <returns>ヘッドラインのID</returns>
        string GetId();

        /// <summary>
        /// ヘッドラインの種類の名前を返す
        /// </summary>
        /// <returns>ヘッドラインの種類の名前</returns>
        string GetKindName();
        
        /// <summary>
        /// ヘッドラインのURLをセットする
        /// </summary>
        /// <param name="url">ヘッドラインのURL</param>
        void SetUrl(Uri url);

        /// <summary>
        /// 取得している番組のリストを返す
        /// </summary>
        /// <returns>番組のリスト</returns>
        IChannel[] GetChannels();

        /// <summary>
        /// 番組を追加する
        /// </summary>
        /// <param name="channels">追加する番組</param>
        void AddChannel(IChannel channels);

        /// <summary>
        /// 番組を破棄する
        /// </summary>
        void ClearChannels();

        /// <summary>
        /// 放送局名をネットから取得して返す
        /// </summary>
        /// <returns>ネットから取得した放送局名</returns>
        string FetchStationName();

        /// <summary>
        /// ヘッドラインをネットから取得する
        /// </summary>
        void FetchHeadline();

        /// <summary>
        /// ヘッドラインをネットから取得する前に発生するイベント
        /// </summary>
        event FetchEventHandler HeadlineFetch;

        /// <summary>
        /// ヘッドラインをネットから取得している最中に発生するイベント
        /// </summary>
        event FetchEventHandler HeadlineFetching;

        /// <summary>
        /// ヘッドラインをネットから取得した後に発生するイベント
        /// </summary>
        event FetchEventHandler HeadlineFetched;

        /// <summary>
        /// ヘッドラインを解析する前に発生するイベント
        /// </summary>
        event HeadlineAnalyzeEventHandler HeadlineAnalyze;

        /// <summary>
        /// ヘッドラインを解析している最中に発生するイベント
        /// </summary>
        event HeadlineAnalyzeEventHandler HeadlineAnalyzing;

        /// <summary>
        /// ヘッドラインを解析した後に発生するイベント
        /// </summary>
        event HeadlineAnalyzeEventHandler HeadlineAnalyzed;

        /// <summary>
        /// ヘッドラインをネットから取得した時刻を返す
        /// </summary>
        /// <returns>ヘッドラインをネットから取得した時刻</returns>
        DateTime GetLastCheckTime();

        /// <summary>
        /// ヘッドラインの設定を保存する
        /// </summary>
        void SaveSetting();

        /// <summary>
        /// ヘッドラインの情報からRssを作成する。
        /// </summary>
        /// <param name="file">Rssのファイル名</param>
        void GenerateRss(string file);

        /// <summary>
        /// クリップを保存しているディレクトリを削除する。
        /// クリップファイルも削除する。
        /// </summary>
        void DeleteClipDirectory();
        
        /// <summary>
        /// 設定を保存していたファイルを削除する
        /// </summary>
        void DeleteUserSettingFile();
    }

    class DateNewerComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            IChannel cx = x as IChannel;
            IChannel cy = y as IChannel;
            if (cx == null || cy == null)
            {
                throw new ArgumentException();
            }

            if (cx.GetDate() != DateTime.MinValue && cy.GetDate() != DateTime.MinValue)
            {
                return -1 * cx.GetDate().CompareTo(cy.GetDate());
            }
            else if (cx.GetDate() == DateTime.MinValue && cy.GetDate() != DateTime.MinValue)
            {
                return 1;
            }
            else if (cx.GetDate() != DateTime.MinValue && cy.GetDate() == DateTime.MinValue)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    class DateOlderComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            IChannel cx = x as IChannel;
            IChannel cy = y as IChannel;
            if (cx == null || cy == null)
            {
                throw new ArgumentException();
            }

            if (cx.GetDate() != DateTime.MinValue && cy.GetDate() != DateTime.MinValue)
            {
                return cx.GetDate().CompareTo(cy.GetDate());
            }
            else if (cx.GetDate() == DateTime.MinValue && cy.GetDate() != DateTime.MinValue)
            {
                return 1;
            }
            else if (cx.GetDate() != DateTime.MinValue && cy.GetDate() == DateTime.MinValue)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
