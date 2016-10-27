using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
	public static class ConversionUtilities
	{
		public static string ToHex(this int number)
		{
			return number.ToString("X2");
		}

		public static bool IsHex(this string number)
		{
			if (number.ToUpper().EndsWith("H"))
			{
				try
				{
					var int32 = Convert.ToInt32(number.Replace("H",""), 16);
					return true;
				}
				catch 
				{
				
				}
			}
			return false;
		}

		public static bool IsNumber(this string number)
		{
			try
			{
				var int32 = Convert.ToInt32(number);
				return true;
			}
			catch
			{

			}
			return false;
		}

		public static string ToHex(this string number)
		{
			number = number.ToUpper();
			if (number.EndsWith("H"))
			{
				return number.Replace("H", "").HexToInt().ToString("X2");
			}
			else
			{
				return number.ToInt().ToString("X2");
			}
		}

		public static int HexToInt(this string number)
		{
			if (number.Contains("H"))
			{
				number = number.Replace("H", "");
			}

			return Convert.ToInt32(number, 16);
		}

		public static int ToInt(this string number)
		{
			return int.Parse(number);
		}

		public static string RemoveComments(this string line)
		{
			int pos = line.IndexOf(";", StringComparison.Ordinal);
			if (pos > -1)
			{
				line = line.Substring(0, pos);
			}

			return line;
		}

		public static string ConvertTabsToSpaces(this string line)
		{
			return line.Replace("\t", " ");
		}

		public static string SwapHex(this string hexdata)
		{
			return hexdata.Substring(2, 2) + hexdata.Substring(0, 2);
		}

		public static string SpaceByBytes(this string hexdata)
		{
			string result = "";
			for (int i = 0; i < hexdata.Length/2; i++)
			{
				result += (hexdata.Substring(i*2, 2) + " ");
			}

			return result;
		}
	}
}
