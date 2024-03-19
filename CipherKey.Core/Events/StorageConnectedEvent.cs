using CipherKey.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Core.Events
{
    public class StorageConnectedEvent
    {
        public StorageType StorageType { get; set; }
        public string MasterPassword { get; set; }
        public string Path { get; set; }
    }
}
