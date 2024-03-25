using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Core.Logging
{
	public interface ILogService
	{
		void Debug<TService>(string message, object data);
		void Info<TService>(string message, object data);
		void Warn<TService>(string message, object data, Exception? e);
		void Error<TService>(string message, object data, Exception? e);
		void Fatal<TService>(string message, object data, Exception? e);
	}
}
