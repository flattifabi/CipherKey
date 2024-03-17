using CipherKey.Core.Enums;
using CipherKey.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace CipherKey.Core.Password
{
	public class PasswordBase : BaseViewModel
    {
        [XmlIgnore]
        private string _oldText = string.Empty;
		[XmlIgnore]
		public DispatcherTimer _clipboardTimer = new DispatcherTimer();
		[XmlIgnore]
        public int AvailableSeconds { get; set; } = 15;

		public void StartClipboardTimer(string oldText)
		{
            _oldText = oldText;
			_clipboardTimer.Interval = TimeSpan.FromSeconds(1);
			_clipboardTimer.Tick += ClipboardTimer_Tick;

			// Start a task to run the timer
			Task.Run(() =>
			{
				_clipboardTimer.Start();
			});
		}

		private void ClipboardTimer_Tick(object? sender, EventArgs e)
		{
			AvailableSeconds--;
            OnPropertyChanged(nameof(AvailableSeconds));
			if (AvailableSeconds <= 0)
			{
                Clipboard.SetText(_oldText);
                _clipboardTimer.Stop();
			}
		}

		[XmlAttribute]
        public PasswordType PasswordType { get; set; } = PasswordType.Other;
        [XmlAttribute]
        public string Topic { get; set; } = "General";
        [XmlAttribute]
        public string Value { get; set; } = string.Empty;
        [XmlAttribute]
        public string Note { get; set; } = string.Empty;
        [XmlAttribute]
        public string Username { get; set; } = string.Empty;
        [XmlAttribute]
        public string Password { get; set; } = string.Empty;
        [XmlAttribute]
        public string Name { get; set; } = string.Empty;
        [XmlAttribute]
        public int PasswordScore { get; set; }
        [XmlAttribute]
        public DateTime Created { get; set; } = DateTime.Now;
        [XmlAttribute]
        public StorageType StorageType { get; set; } = StorageType.Local;
        public PasswordEntryDesign PasswordEntryDesign { get; set; } = new PasswordEntryDesign();
        public PasswordProperties PasswordProperties { get; set; } = new PasswordProperties();
    }
}
