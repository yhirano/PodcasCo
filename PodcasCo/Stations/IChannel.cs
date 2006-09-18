#region ディレクティブを使用する

using System;

#endregion

namespace PodcasCo.Stations
{
    /// <summary>
    /// 番組インターフェース
    /// </summary>
    public interface IChannel
    {
        /// <summary>
        /// クリップするかどうかのチェックを記憶しておく
        /// </summary>
        bool Check { get; set; }

        /// <summary>
        /// 親ヘッドライン
        /// </summary>
        IHeadline ParentHeadline { get; }

        /// <summary>
        /// 番組のハッシュを返す。
        /// 番組のハッシュは、番組のタイトル、長さ、配信日時を元に求める。
        /// 番組のタイトル、長さ、配信日時が同一の場合は同じハッシュとなる。
        /// </summary>
        /// <returns>番組のハッシュ</returns>
        int GetHash();

        /// <summary>
        /// 番組がローカルファイルにあるかを返す
        /// </summary>
        /// <returns>番組がローカルファイルにあるか</returns>
        bool IsLocal();

        /// <summary>
        /// 番組のタイトルを返す
        /// </summary>
        /// <returns>番組のタイトル</returns>
        string GetTitle();

        /// <summary>
        /// 番組の放送URLを返す
        /// </summary>
        /// <returns>番組の放送URL</returns>
        Uri GetPlayUrl();

        /// <summary>
        /// 番組の放送URLをセットする
        /// </summary>        
        /// <param name="uri">番組の放送URL</param>
        void SetPlayUrl(Uri uri);
        
        /// <summary>
        /// 番組のウェブサイトURLを返す
        /// </summary>
        /// <returns>番組のウェブサイトURL</returns>
        Uri GetWebsiteUrl();

        /// <summary>
        /// 番組の詳細フォームを表示する
        /// </summary>
        /// <param name="channel">番組</param>
        void ShowPropertyForm();

        /// <summary>
        /// クリップされているファイルを削除する
        /// </summary>
        void DeleteClipFile();

        /// <summary>
        /// 番組のクローンを返す
        /// </summary>
        /// <param name="parentHeadline">親ヘッドライン</param>
        /// <returns>番組のクローン</returns>
        IChannel Clone(IHeadline parentHeadline);
    }
}
