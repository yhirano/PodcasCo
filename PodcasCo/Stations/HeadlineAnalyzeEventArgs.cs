#region ディレクティブを使用する

using System;

#endregion

namespace PodcasCo.Stations
{
    /// <summary>
    /// ヘッドラインの解析イベント
    /// </summary>
    public class HeadlineAnalyzeEventArgs : EventArgs
    {
        /// <summary>
        /// 解析済みのヘッドライン
        /// </summary>
        private int analyzedCount;

        /// <summary>
        /// 解析済みのヘッドライン
        /// </summary>
        public int AnalyzedCount
        {
            get { return analyzedCount; }
        }

        /// <summary>
        /// 全ヘッドライン
        /// </summary>
        private int wholeCount;

        /// <summary>
        /// 全ヘッドライン
        /// </summary>
        public int WholeCount
        {
            get { return wholeCount; }
        }

        /// <summary>
        /// 全ヘッドラインが不明かを取得する
        /// </summary>
        public bool IsUnknownWholeCount
        {
            get { return (wholeCount < 0); }
        }

        /// <summary>
        /// 全ヘッドラインが不明
        /// </summary>
        public const int UNKNOWN_WHOLE_COUNT = -1;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="analyzedCount">解析済みのヘッドライン</param>
        /// <param name="wholeCount">全ヘッドライン。不明の場合はマイナスの値を入れてください。</param>
        public HeadlineAnalyzeEventArgs(int analyzedCount, int wholeCount)
        {
            this.analyzedCount = analyzedCount;
            this.wholeCount = wholeCount;
        }
    }
}
