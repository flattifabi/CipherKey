using CipherKey.Core.Configurations;
using CipherKey.Core.Data;
using CipherKey.Core.Extensions;
using CipherKey.Core.Password;
using CipherKey.Core.SafeConnect;
using CipherKey.Crypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Services.SafeConnection
{
    public class SafeConnectService : ISafeConnectService
    {
        public event Action AllConnectedStateChanged;

        private readonly IPasswordService _passwordService;
        private readonly IConfigurationService _configurationService;
        public SafeConnectService(IPasswordService passwordService, IConfigurationService configurationService)
        {
            _passwordService = passwordService;
            _configurationService = configurationService;
        }

        public async Task<bool> ConnectAllAvailabelSafes(List<RemoteAdressData> addresses, string localMasterPassword)
        {
            var addr = addresses.Where(x => !string.IsNullOrEmpty(x.Password)).ToList();
            await Task.Run(() =>
            {
                foreach(var adress in addr)
                {
                    var blankPassword = _passwordService.GetDecryptedPassword(adress.Password, localMasterPassword);
                    try
                    {
						var t = CipherF<CipherStorage>.Load(adress.FilePath, blankPassword.ResultData.Hash());
						if (t != null)
						{
							adress.RemoteAddressState = Core.Enums.RemoteAddressState.Connected;
							adress.CipherStorage = t;
						}
						else adress.RemoteAddressState = Core.Enums.RemoteAddressState.NotConnected;
					}
                    catch(Exception)
                    {
						adress.RemoteAddressState = Core.Enums.RemoteAddressState.NotConnected;
						continue;
					}
                }
            });
            return true;
        }

        public async Task<bool> ConnectToSafe(RemoteAdressData address, string password)
        {
            CipherStorage storage = null;
            await Task.Run(() =>
            {
                string blankPassword = _passwordService.GetEncryptedPassword(address.Password, password).ResultData;
                var t = CipherF<CipherStorage>.Load(address.FilePath, blankPassword.Hash());
                storage = t;
            });
            if (storage == null)
                return false;
            return true;
        }
    }
}
