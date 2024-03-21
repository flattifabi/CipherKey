using CipherKey.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Core.SafeConnect
{
    public interface ISafeConnectService
    {
        event Action AllConnectedStateChanged;
        Task<bool> ConnectAllAvailabelSafes(List<RemoteAdressData> addresses, string localMasterPassword);
        Task<bool> ConnectToSafe(RemoteAdressData address, string password);
    }
}
