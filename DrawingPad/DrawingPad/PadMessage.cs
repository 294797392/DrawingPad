using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SciencePad
{
    class PadMessage
    {
        public static void Info(string msg, params object[] param)
        {
            MessageBox.Show(string.Format(msg, param), "信息", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void Warn(string msg, params object[] param)
        {
            MessageBox.Show(string.Format(msg, param), "警告", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public static void Error(string msg, params object[] param)
        {
            MessageBox.Show(string.Format(msg, param), "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static bool Confirm(string msg, params object[] param)
        {
            return MessageBox.Show(string.Format(msg, param), "提示", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation) == MessageBoxResult.OK;
        }
    }
}
