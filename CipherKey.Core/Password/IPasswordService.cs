using CipherKey.Core.Data;
using CipherKey.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Core.Password
{
    public interface IPasswordService
    {
        CipherResult<List<PasswordBase>> GetPasswordsByTopic(string topic);
        CipherResult<List<PasswordBackupData>> GetPasswordBackups();
        CipherResult<bool> AddPassword(PasswordBase password);
        CipherResult<bool> DeletePassword(PasswordBase password);
		CipherResult<bool> ChangePassword(PasswordBase password);
        CipherResult<string> GetEncryptedPassword(string value, string masterPassword);
        CipherResult<string> GetDecryptedPassword(string value, string masterPassword);
        CipherResult<bool> RestorePasswordFromBackup(PasswordBase currentPassword, PasswordBackupData backupData);
    }
}
