using System;

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
				number = number.ToUpper().Replace("H", "");
				foreach (var num in number)
                {
                    if (num < '0' || num > '9' && num < 'A' || num > 'F')
                    {
                        return false;
                    }
                }

				return true;
			}
			return false;
		}

		public static bool IsNumber(this string number)
        {
            foreach (var num in number)
            {
                if (num < '0' || num > '9')
                {
                    return false;
                }
            }

            return true;
        }

		public static string ToHex(this string number)
		{
			number = number.ToUpper();
			if (number.EndsWith("H"))
			{
				return number.Replace("H", "").HexToInt().ToString("X2");
			}

			return number.ToInt().ToString("X2");
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
			var pos = line.IndexOf(";", StringComparison.Ordinal);
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
			var result = "";
			for (int i = 0; i < hexdata.Length/2; i++)
			{
				result += (hexdata.Substring(i*2, 2) + " ");
			}

			return result;
		}
	}
}
