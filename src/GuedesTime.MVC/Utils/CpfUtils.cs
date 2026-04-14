using System.Text.RegularExpressions;

namespace GuedesTime.MVC.Utils
{
	public static class CpfUtils
	{
		public static string OnlyDigits(string value)
		{
			if (string.IsNullOrWhiteSpace(value)) return string.Empty;
			return Regex.Replace(value, @"\D", "");
		}

		public static bool IsValid(string? cpf)
		{
			var d = OnlyDigits(cpf ?? string.Empty);
			if (d.Length != 11) return false;
			if (d.Distinct().Count() == 1) return false;

			int Calc(int len)
			{
				var sum = 0;
				for (var i = 0; i < len; i++)
				{
					sum += (d[i] - '0') * (len + 1 - i);
				}
				var mod = sum % 11;
				return mod < 2 ? 0 : 11 - mod;
			}

			var dig1 = Calc(9);
			var dig2 = Calc(10);

			return d[9] - '0' == dig1 && d[10] - '0' == dig2;
		}
	}
}

