#region ディレクティブを使用する

using System;
using System.Collections;
using System.IO;
using System.Text;

#endregion

namespace PodcasCo.Stations.RssPodcast
{
    /// <summary>
    /// PodcastのMIMEタイプの優先度を格納するクラス
    /// </summary>
    public sealed class RssPodcastMimePriority
    {

        /// <summary>
        /// PodcastのMIMEタイプの優先度テーブル。数値が高い方が優先度が高い。
        /// Key => string MIME, value => int Priority
        /// </summary>
        private static Hashtable rssPodcastMimePriorityTable =
            new Hashtable(CaseInsensitiveHashCodeProvider.DefaultInvariant,
            CaseInsensitiveComparer.DefaultInvariant);

        /// <summary>
        /// シングルトンのためプライベート
        /// </summary>
        private RssPodcastMimePriority()
        {
        }

        /// <summary>
        /// PodcastのMIMEタイプの優先度をファイルから読み込み、
        /// Mimeの優先度を決定する。
        /// </summary>
        public static void Initialize()
        {
            StreamReader sr =null;

            try
            {
                // 現在のコードを実行しているAssemblyを取得
                System.Reflection.Assembly thisAssembly
                    = System.Reflection.Assembly.GetExecutingAssembly();
                // 指定されたマニフェストリソースを読み込む
                sr =
                    new StreamReader(thisAssembly.GetManifestResourceStream(PodcasCoInfo.RssPodcastMimePriorityFile),
                    Encoding.GetEncoding("shift-jis"));
                // 内容を読み込む
                string mimeString = sr.ReadToEnd();

                string[] mimePriorityRawArray = mimeString.Split('\n');

                foreach (string mimePriorityRaw in mimePriorityRawArray)
                {
                    if (mimePriorityRaw.Length != 0)
                    {
                        string[] mimePriority = mimePriorityRaw.Split(',');
                        rssPodcastMimePriorityTable.Add(mimePriority[0], int.Parse(mimePriority[1]));
                    }
                }
            }
            finally {
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }

        /// <summary>
        /// PodcastのMIMEタイプの再生優先度を返す。数値が高い方が優先度が高い。
        /// 再生しないMIMEタイプの場合や、優先度が存在しないMIMEタイプ場合は0を返す。
        /// </summary>
        /// <param name="mime">MIMEタイプ</param>
        /// <returns></returns>
        public static int GetRssPodcastMimePriority(string mime)
        {
            if (mime == null)
            {
                return 0;
            }

            return ((rssPodcastMimePriorityTable.ContainsKey(mime)) == false ? 0 : (int)rssPodcastMimePriorityTable[mime]);
        }
    }
}
