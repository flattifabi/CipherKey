using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace CipherKey.Core.Helpers
{
    public static class DialogHelper
    {
        public static ContentDialog BuildContentDialog(Control view)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Content = view;
            return dialog;
        }
    }
}
