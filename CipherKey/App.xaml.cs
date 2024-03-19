using CipherKey.Core.ApplicationConstants;
using CipherKey.Core.Helpers;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CipherKey
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			Bootstrapper bootstrapper = new Bootstrapper();
			bootstrapper.Run();
        }

		private void Application_Exit(object sender, ExitEventArgs e)
		{
			FolderCryptor.EncryptFolder(FilePaths.PasswordStorageFilePath, IDGenerator.GetComputerID());
		}
	}
}
