using CipherKey.Core.ApplicationConstants;
using CipherKey.Core.Configurations;
using CipherKey.Core.Data;
using CipherKey.Core.Extensions;
using CipherKey.Core.Helpers;
using CipherKey.Core.Models;
using CipherKey.Core.Password;
using CipherKey.Crypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Services.Configuration
{
	public class ConfigurationService : IConfigurationService
	{

        #region Public Constructors

        public ConfigurationService()
		{ }

		#endregion Public Constructors

		#region Public Methods

		public CipherResult<bool> AddTopic(Topic topic)
        {
            var t = CipherF<CipherStorage>.Load();
            t.Topics.Add(topic);
            CipherF<CipherStorage>.Save(t);
            return new CipherResult<bool> { ResultData = true };
        }

        public CipherResult<bool> ChangeMasterPassword(string oldMasterPassword, string newMasterPassword)
        {
            throw new NotImplementedException();
        }

        public CipherResult<string> CheckPassword(string password)
        {
            var t = CipherF<CipherStorage>.Load();
            if (t.ApplicationConfiguration.MasterPassword == password.Hash())
            {
                return new CipherResult<string> { ResultData = password };
            }
            return new CipherResult<string> { ResultData = string.Empty };
        }

        public CipherResult<bool> DeleteTopic(Topic topic)
        {
            var t = CipherF<CipherStorage>.Load();
            t.Topics.Remove(topic);
            CipherF<CipherStorage>.Save(t);
            return new CipherResult<bool> { ResultData = true };
        }

		public CipherResult<List<string>> GetRemoteAdresses()
		{
			throw new NotImplementedException();
		}

		public CipherResult<List<Topic>> GetTopics()
        {
            CipherF<CipherStorage>.Load();
            return new CipherResult<List<Topic>> { ResultData = CipherF<CipherStorage>.Load().Topics };
        }

        public void Initialize()
        {
			FilePaths.CreateBaseFilePath();
			SetApplicationConfiguration();
		}
        public CipherResult<bool> IsConfigured()
        {
            try
            {
                var cipherData = CipherF<CipherStorage>.Load();
                var applicationConfiguration = cipherData.ApplicationConfiguration;
                if (applicationConfiguration != null && !string.IsNullOrEmpty(applicationConfiguration.MasterPassword))
                {
                    return new CipherResult<bool> { ResultData = true };
                }
                return new CipherResult<bool> { ResultData = false };
            }
            catch (Exception e)
            {
                return new CipherResult<bool> { ResultData = false };
            }
        }

		public CipherResult<bool> SetMasterPassword(string enteredMasterPassword)
        {
            var f = CipherF<CipherStorage>.Load();
            f.ApplicationConfiguration.MasterPassword = enteredMasterPassword.Hash();
            CipherF<CipherStorage>.Save(f);
            return new CipherResult<bool> { ResultData = true };
        }

        public CipherResult<bool> UpdateTopic(Topic topic)
        {

            var t = CipherF<CipherStorage>.Load();
            var oldTopic = t.Topics.FirstOrDefault(x => x.Name == topic.Name);
            if (oldTopic != null)
            {
                t.Topics.Remove(oldTopic);
                t.Topics.Add(topic);
                CipherF<CipherStorage>.Save(t);
            }
            CipherF<CipherStorage>.Save(t);
            return new CipherResult<bool> { ResultData = true };
        }
		public CipherResult<bool> AddRemoteAddress(string address)
		{
			var t = CipherF<CipherStorage>.Load();
			t.ApplicationConfiguration.RemoteAddresses.Add(new RemoteAdressData() { FilePath = address });
			CipherF<CipherStorage>.Save(t);
			return new CipherResult<bool> { ResultData = true };
		}
		public CipherResult<bool> RemoveRemoteAddress(string address)
		{
			var t = CipherF<CipherStorage>.Load();
            var remoteAddress = t.ApplicationConfiguration.RemoteAddresses.FirstOrDefault(x => x.FilePath == address);
            if(remoteAddress != null)
            {
				t.ApplicationConfiguration.RemoteAddresses.Remove(remoteAddress);
				CipherF<CipherStorage>.Save(t);
                return new CipherResult<bool> { ResultData = true };
			}
            return new CipherResult<bool> { ResultData = false, ErrorText = "Adress not available on personal source" };
		}
		#endregion Public Methods

		#region Private Methods

		private void SetApplicationConfiguration()
        {
			if (!IsConfigured().ResultData)
			{
				var t = CipherF<CipherStorage>.Load();
				t = new CipherStorage();
				t.Topics.Add(new Topic()
				{
					Name = "Allgemein",
					Description = "Allgemeine Passwörter",
					Design = new TopicEntryDesign()
					{
						BackgroundHex = "FF0000",
						BorderHex = "#FF0000",
						ForegroundHex = "#575757",
					}
				});
				CipherF<CipherStorage>.Save(t);
			}
        }

        #endregion Private Methods
    }
}
