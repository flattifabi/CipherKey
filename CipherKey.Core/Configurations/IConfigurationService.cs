using CipherKey.Core.Data;
using CipherKey.Core.Models;
using CipherKey.Core.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Core.Configurations
{
    public interface IConfigurationService
    {
		void Initialize();
		CipherResult<bool> ChangeMasterPassword(string oldMasterPassword, string newMasterPassword);
		CipherResult<List<RemoteAdressData>> GetRemoteAdresses();
		CipherResult<bool> IsConfigured();
		CipherResult<bool> SetMasterPassword(string enteredMasterPassword);
		CipherResult<string> CheckPassword(string password);
		CipherResult<bool> AddTopic(Topic topic);
		CipherResult<bool> DeleteTopic(Topic topic);
		CipherResult<bool> UpdateTopic(Topic topic);
		CipherResult<List<Topic>> GetTopics();
		CipherResult<bool> AddRemoteAddress(RemoteAdressData addressData);
		CipherResult<bool> RemoveRemoteAddress(string address);
	}
}
