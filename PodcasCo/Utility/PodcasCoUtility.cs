#region ディレクティブを使用する

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Collections;
using System.Xml;

#endregion

namespace PodcasCo.Utility
{
    /// <summary>
    /// PocketLadioのユーティリティ
    /// </summary>
    public sealed class PodcasCoUtility
    {
        /// <summary>
        /// シングルトンのためプライベート
        /// </summary>
        private PodcasCoUtility()
        {
        }

        /// <summary>
        /// ネット上からファイルをダウンロードする
        /// </summary>
        /// <param name="url">ダウンロードするファイルのURL</param>
        /// <param name="fileName">保存先のファイル名</param>
        public static void FetchFile(Uri url, string fileName)
        {
            Stream st = null;
            FileStream fs = null;

            try
            {
                st = PocketLadioUtility.GetHttpStream(url);
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);

                // 応答データをファイルに書き込む
                Byte[] buf = new Byte[PodcasCoInfo.ReadByteOneCycleDownload];
                int count = 0;

                do
                {
                    count = st.Read(buf, 0, buf.Length);
                    fs.Write(buf, 0, count);
                } while (count != 0);
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
            catch (ArgumentException)
            {
                throw;
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
                if (st != null)
                {
                    st.Close();
                }
            }
        }
    }
}
