using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor
{
	public static class Register
	{
		public static byte Increment(this byte data)
		{
			return (byte) (data + 1);
		}

		public static byte Decrement(this byte data)
		{
			return (byte) (data - 1);
		}

		public static byte ShiftLeft(this byte data)
		{
			return (byte) ((data << 1) & 0xff);
		}

		public static byte ShiftRight(this byte data)
		{
			return (byte) ((data >> 1) & 0xff);
		}
	}
}
