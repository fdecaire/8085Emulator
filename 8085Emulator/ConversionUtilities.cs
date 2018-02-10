using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor
{
	internal static class ConversionUtilities
	{
		public static string ToBinaryString(this byte hexdata)
		{
			return Convert.ToString(hexdata, 2).PadLeft(8, '0');
		}

		public static byte TwosComplement(this byte hexdata)
		{
			return (byte)((~(hexdata - 0x01)) );
		}
	}
}
