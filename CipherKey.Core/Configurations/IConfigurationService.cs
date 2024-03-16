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
		CipherResult<bool> GetDecodedPasswords<T>(T entry, string masterPassword);
		CipherResult<bool> ChangeMasterPassword(string oldMasterPassword, string newMasterPassword);
		CipherResult<bool> IsConfigured();
		CipherResult<bool> SetMasterPassword(string enteredMasterPassword);
		CipherResult<string> CheckPassword(string password);
		CipherResult<bool> AddTopic(Topic topic);
		CipherResult<bool> DeleteTopic(Topic topic);
		CipherResult<bool> UpdateTopic(Topic topic);
		CipherResult<List<Topic>> GetTopics();
	}
}
