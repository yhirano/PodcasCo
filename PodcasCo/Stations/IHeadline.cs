#region ディレクティブを使用する

using System;

#endregion

namespace PodcasCo.Stations
{
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
}
