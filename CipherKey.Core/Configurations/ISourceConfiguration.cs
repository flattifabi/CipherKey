using CipherKey.Core.ApplicationConstants;
using CipherKey.Core.Data;
using CipherKey.Crypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Core.Configurations
{
	public interface ISourceConfiguration
	{
		public string Path { get; set; }
		public string Password { get; set; }
		public string LocalMasterPassword { get; set; }
	}
	public class SourceConfiguration : ISourceConfiguration
	{
		public string Path { get; set; } = FilePaths.CipherStorageFilePath;
		public string Password { get; set; } = CipherF<CipherStorage>.Key;
		public string LocalMasterPassword { get; set; } = string.Empty;
	}
}
