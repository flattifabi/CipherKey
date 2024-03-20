﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Core.Data
{
	public class RemoteAdressData
	{
		/// <summary>
		///		The User can select the remote address name
		/// </summary>
		public string PersonalName { get; set; } = string.Empty;
		/// <summary>
		///		Remote address, have to end with .cipher
		/// </summary>
		public string FilePath { get; set; } = string.Empty;
		/// <summary>
		///		Only available to save if remote allows this
		/// </summary>
		public string Password { get; set; } = string.Empty;
	}
}
