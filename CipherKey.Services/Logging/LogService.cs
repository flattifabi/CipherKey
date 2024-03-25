using CipherKey.Core.ApplicationConstants;
using CipherKey.Core.Logging;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Services.Logging
{
	public class LogService : ILogService
	{
		private readonly ILogger _logger;
		public LogService()
		{
			_logger = new LoggerConfiguration()
				.MinimumLevel.Verbose()
				.WriteTo.File(FilePaths.LogFolder + "cipherKeyLog.txt", rollingInterval: RollingInterval.Day)
				.CreateLogger();
		}
		public void Debug<TService>(string message, object data)
		{
			SetThreadIdToMessage(ref message);
			_logger.Debug(message, JsonConvert.SerializeObject(data));
		}

		public void Error<TService>(string message, object data, Exception? e)
		{
			SetThreadIdToMessage(ref message);
			_logger.Error(e, message, JsonConvert.SerializeObject(data));
		}

		public void Fatal<TService>(string message, object data, Exception? e)
		{
			SetThreadIdToMessage(ref message);
			_logger.Fatal(e, message, JsonConvert.SerializeObject(data));
		}

		public void Info<TService>(string message, object data)
		{
			SetThreadIdToMessage(ref message);
			_logger.Information(message, JsonConvert.SerializeObject(data));
		}

		public void Warn<TService>(string message, object data, Exception? e)
		{
			SetThreadIdToMessage(ref message);
			_logger.Warning(e, message, JsonConvert.SerializeObject(data));
		}
		private void SetThreadIdToMessage(ref string message, [CallerMemberName] string caller = "")
		{
			long threadId = Thread.CurrentThread.ManagedThreadId;
			message = $"[ThreadID: {threadId}] >> [{caller}] {message}";
		}
	}
}
