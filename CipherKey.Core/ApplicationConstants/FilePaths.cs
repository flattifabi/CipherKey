using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Core.ApplicationConstants
{
    public static class FilePaths
    {
        public static readonly string PasswordStorageFilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\CipherKey\\";
        public static readonly string CipherStorageFilePath = PasswordStorageFilePath + "cipherStorage.cipher";
        public static readonly string PasswordsFileName = "Passwords.xml";
        public static readonly string TopicFileName = "Topics.xml";
        public static readonly string ApplicationConfigurationFileName = "ApplicationConfiguration.xml";

        public static void CreateBaseFilePath()
        {
			if (!System.IO.Directory.Exists(PasswordStorageFilePath))
            {
				System.IO.Directory.CreateDirectory(PasswordStorageFilePath);
			}
		}
	}
}
