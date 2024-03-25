using CipherKey.Core.Enums;
using CipherKey.Core.Password;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CipherKey.Core.Data
{
	public class RemoteAdressData : INotifyPropertyChanged
	{

		#region Events

		public event PropertyChangedEventHandler? PropertyChanged;

		#endregion Events


		#region Properties

		[XmlIgnore]
		public CipherStorage CipherStorage { get; set; }

		/// <summary>
		///		Remote address, have to end with .cipher
		/// </summary>
		public string FilePath { get; set; } = string.Empty;

		/// <summary>
		///		Only available to save if remote allows this
		/// </summary>
		public string Password { get; set; } = string.Empty;

		/// <summary>
		///		The User can select the remote address name
		/// </summary>
		public string PersonalName { get; set; } = string.Empty;
		[XmlIgnore]
		public RemoteAddressState RemoteAddressState { get; set; } = RemoteAddressState.NotConnected;

		#endregion Properties


		#region Public Methods

		public void Changed()
		{
			OnPropertyChanged(new List<string> { nameof(CipherStorage), nameof(FilePath), nameof(Password), 
				nameof(PersonalName), nameof(RemoteAddressState) });
			this.CipherStorage.OnPropertyChanged(nameof(CipherStorage.Topics));
		}
		public void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		public void OnPropertyChanged(List<string> propertieNames)
		{
			foreach(var propertyName in propertieNames)
			{
				OnPropertyChanged(propertyName);
			}
		}

		#endregion Public Methods
	}
}
