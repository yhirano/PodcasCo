#region ディレクティブを使用する

using System;

#endregion

namespace PodcasCo
{
    public class FileEventArgs : EventArgs
    {
        /// <summary>
        /// ダウンロード済みのファイル
        /// </summary>
        private long filedCount;

        /// <summary>
        /// ダウンロード済みのファイル
        /// </summary>
        public long FiledCount
        {
            get { return filedCount; }
        }

        /// <summary>
        /// ダウンロードするすべてのファイル
        /// </summary>
        private long fileCount;

        /// <summary>
        /// ダウンロードするすべてのファイル
        /// </summary>
        public long FileCount
        {
            get { return fileCount; }
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="filedCount">ダウンロード済みのファイル</param>
        /// <param name="fileCount">ダウンロードするすべてのファイル</param>
        public FileEventArgs(int filedCount,int fileCount)
        {
            this.filedCount = filedCount;
            this.fileCount = fileCount;
        }
    }
}
