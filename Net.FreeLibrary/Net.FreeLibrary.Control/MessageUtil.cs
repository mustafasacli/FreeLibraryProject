using System.Windows.Forms;

namespace Net.FreeLibrary.Control
{
    public class MessageUtil
    {
        public static void Error(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Message(string message, string caption)
        {
            MessageBox.Show(message, caption);
        }

        public static void Info(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Warn(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult ConfirmYesNo(string message, string caption)
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult ConfirmYesNoCancel(string message, string caption)
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }
    }
}
