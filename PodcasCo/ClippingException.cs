#region ディレクティブを使用する

using System;

#endregion

namespace PodcasCo
{
    /// <summary>
    /// 番組のクリップ時の例外
    /// </summary>
    public class ClippingException : Exception
    {
        public ClippingException() : base()
        {
        }

        public ClippingException(string message) : base(message)
        {
        }
    }
}
