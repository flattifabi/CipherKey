using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CipherKey.Core.Helpers
{
    public class SecurePasswordHelper
    {
        public static readonly DependencyProperty SecurePasswordProperty =
            DependencyProperty.RegisterAttached("SecurePassword", typeof(SecureString), typeof(SecurePasswordHelper),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSecurePasswordPropertyChanged));

        public static SecureString GetSecurePassword(DependencyObject obj)
        {
            return (SecureString)obj.GetValue(SecurePasswordProperty);
        }

        public static void SetSecurePassword(DependencyObject obj, SecureString value)
        {
            obj.SetValue(SecurePasswordProperty, value);
        }

        private static void OnSecurePasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.TextChanged -= TextBox_TextChanged;
                if (e.NewValue is SecureString secureString)
                {
                    textBox.Text = SecureStringToString(secureString);
                }
                else
                {
                    textBox.Text = string.Empty;
                }
                textBox.TextChanged += TextBox_TextChanged;
            }
        }

        private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                SetSecurePassword(textBox, StringToSecureString(textBox.Text));
            }
        }

        private static SecureString StringToSecureString(string str)
        {
            SecureString secureString = new SecureString();
            foreach (char c in str)
            {
                secureString.AppendChar(c);
            }
            return secureString;
        }

        private static string SecureStringToString(SecureString secureString)
        {
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(ptr);
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.ZeroFreeGlobalAllocUnicode(ptr);
            }
        }
    }
}
