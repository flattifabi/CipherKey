using CipherKey.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Core.Models
{
    public class ApplicationConfiguration
    {
        public string MasterPassword { get; set; } = string.Empty;
        public string MetaData { get; set; } = IDGenerator.GetComputerID();
        public List<string> RemoteAddresses { get; set; } = new List<string> { };
    }
}
