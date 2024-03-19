using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Core.Helpers
{
    public static class PasswordHelpers
    {
		public static string GenerateStrongPassword(int length)
		{
			const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+-=[]{}|;:,.<>?";
			var random = new Random();
			var password = new char[length];

			for (int i = 0; i < length; i++)
			{
				password[i] = validChars[random.Next(validChars.Length)];
			}
			return new string(password);
		}
		public static int CheckPasswordStrength(string password)
		{
			int score = 0;

			// Längenprüfung
			if (password.Length >= 8)
				score += 20;
			if (password.Length >= 12)
				score += 20;

			// Großbuchstabenprüfung
			if (ContainsUpperCase(password))
				score += 20;

			// Kleinbuchstabenprüfung
			if (ContainsLowerCase(password))
				score += 20;

			// Zahlenprüfung
			if (ContainsDigit(password))
				score += 20;

			// Sonderzeichenprüfung
			if (ContainsSpecialChar(password))
				score += 20;

			// Bonuspunkte für zusätzliche Sicherheitsmaßnahmen können hier hinzugefügt werden
			// Beispiel: Bonuspunkte für die Verwendung von mehr als einem Sonderzeichen
			if (CountSpecialChars(password) > 1)
				score += 20;

			// Maximale Punktzahl auf 100 begrenzen
			if (score > 100)
				score = 100;

			return score;
		}

		// Die Hilfsmethoden bleiben unverändert

		private static bool ContainsUpperCase(string password)
		{
			foreach (char c in password)
			{
				if (char.IsUpper(c))
					return true;
			}
			return false;
		}

		private static bool ContainsLowerCase(string password)
		{
			foreach (char c in password)
			{
				if (char.IsLower(c))
					return true;
			}
			return false;
		}

		private static bool ContainsDigit(string password)
		{
			foreach (char c in password)
			{
				if (char.IsDigit(c))
					return true;
			}
			return false;
		}

		private static bool ContainsSpecialChar(string password)
		{
			const string specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";
			foreach (char c in password)
			{
				if (specialChars.Contains(c))
					return true;
			}
			return false;
		}

		private static int CountSpecialChars(string password)
		{
			const string specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";
			int count = 0;
			foreach (char c in password)
			{
				if (specialChars.Contains(c))
					count++;
			}
			return count;
		}
	}
}
