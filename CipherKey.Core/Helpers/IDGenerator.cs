using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Core.Helpers
{
    public static class IDGenerator
    {
		public static string GetComputerID()
		{
			StringBuilder sb = new StringBuilder();
			NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
			foreach (NetworkInterface nic in nics)
			{
				if (nic.NetworkInterfaceType != NetworkInterfaceType.Loopback && nic.OperationalStatus == OperationalStatus.Up)
				{
					sb.Append(nic.GetPhysicalAddress().ToString());
				}
			}
			using (SHA256 sha256 = SHA256.Create())
			{
				byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
				return BitConverter.ToString(hashBytes).Replace("-", "");
			}
		}
	}
}
