using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor
{
	//http://www.righto.com/2013/07/reverse-engineering-flag-circuits-in.html
	public class FlagRegister
	{
		public bool Carry { get; set; }
		public bool AuxCarry { get; set; }
		public bool Parity { get; set; }
		public bool Zero { get; set; }
		public bool Sign { get; set; }

		public void Reset()
		{
			Carry = false;
			AuxCarry = false;
			Parity = false;
			Zero = false;
			Sign = false;
		}

		public byte Register
		{
			get
			{
				byte temp = 0;

				if (Carry)
				{
					temp += 0x01;
				}

				if (Parity)
				{
					temp += 0x04;
				}

				if (AuxCarry)
				{
					temp += 0x10;
				}

				if (Zero)
				{
					temp += 0x40;
				}

				if (Sign)
				{
					temp += 0x80;
				}

				return temp;
			}
			set
			{
				Carry = (value & 0x01) > 0;
				Parity = (value & 0x04) > 0;
				AuxCarry = (value & 0x10) > 0;
				Zero = (value & 0x40) > 0;
				Sign = (value & 0x80) > 0;
			}
		}
	}
}
