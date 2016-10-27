using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor
{
	public class InterruptMask
	{
		private byte _mask;

		public byte Mask
		{
			set { _mask = value; }
			get { return _mask; }
		}

		public bool RST_7_5Mask
		{
			get { return (_mask & 0x04) > 0; }
		}

		public bool RST_6_5Mask
		{
			get { return (_mask & 0x02) > 0; }
		}

		public bool RST_5_5Mask
		{
			get { return (_mask & 0x01) > 0; }
		}

		//TODO: finish getters for interrupt mask
	}
}
