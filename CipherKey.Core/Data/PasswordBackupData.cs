using CipherKey.Core.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Core.Data
{
    public class PasswordBackupData
    {
        public PasswordBase PasswordData { get; set; } = new();
        public DateTime ChangedAt { get; set; } = DateTime.Now;
    }
}
