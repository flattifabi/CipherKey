using CipherKey.Core.Models;
using CipherKey.Core.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Core.Data
{
	public class CipherStorage
	{
		public List<Topic> Topics { get; set; } = new List<Topic> { };
		public List<PasswordBase> Passwords { get; set; } = new List<PasswordBase> { };
		public ApplicationConfiguration ApplicationConfiguration { get; set; } = new ApplicationConfiguration { };
		public List<PasswordBackupData> PasswordBackUps { get; set; } = new List<PasswordBackupData> { };
		public bool EnableToSavePassword { get; set; } = false;
		public bool IsRemote { get; set; } = false;
		public string RemotePasswordIfIsRemote { get; set; } = string.Empty;
		public List<string> RemoteOwnerUsernames { get; set; } = new List<string> { Environment.UserName };
		public List<string> RemoteBlacklist { get; set; } = new List<string> { };
	}
}
