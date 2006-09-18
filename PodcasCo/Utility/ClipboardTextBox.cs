#region ディレクティブを使用する

using System;
using System.Windows.Forms;
using PodcasCo.ExtCF.Windows.Forms;

#endregion

namespace PodcasCo.Utility
{
    /// <summary>
    /// テキストボックスへのクリップボードの処理ユーティリティ
    /// </summary>
    public sealed class ClipboardTextBox
    {
        private ClipboardTextBox()
        {
        }

        public static void Cut(TextBox txtBox)
        {
            if (txtBox != null && txtBox.SelectionLength > 0)
            {
                Clipboard.SetText(txtBox.SelectedText);
                txtBox.SelectedText = "";
            }
        }

        public static void Copy(TextBox txtBox)
        {
            if (txtBox != null && txtBox.SelectionLength > 0)
            {
                Clipboard.SetText(txtBox.SelectedText);
            }
        }

        public static void Paste(TextBox txtBox) {
            string clipboardText = Clipboard.GetText();
            if (txtBox != null && clipboardText != null)
            {
                string before = txtBox.Text.Substring(0, txtBox.SelectionStart);
                string after = txtBox.Text.Substring(txtBox.SelectionStart + txtBox.SelectionLength, txtBox.TextLength - (txtBox.SelectionStart + txtBox.SelectionLength));
                txtBox.Text = before + clipboardText + after;
            }
        }
    }
}
