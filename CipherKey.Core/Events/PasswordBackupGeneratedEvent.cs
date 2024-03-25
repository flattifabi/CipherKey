using CipherKey.Core.Data;
using CipherKey.Core.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Core.Events
{
	public class PasswordBackupGeneratedEvent
	{
		public PasswordBackupData PasswordBackupData  { get; set; }
		public PasswordBase PasswordBase { get; set; }
	}
}
