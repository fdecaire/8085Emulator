using System;
using System.Collections.Generic;
using System.Text;

namespace Processor
{
	public class OutputDevice
	{
		private List<byte> Buffer { get; set; } = new List<byte>();
		private bool _characterRead = true;

		public void SendData(byte data)
		{
			Buffer.Add(data);

			_characterRead = false;
		}

		public string GetText()
		{
			return Encoding.ASCII.GetString(Buffer.ToArray());
		}

		public string GetNextCharacter()
		{
			if (_characterRead)
			{
				return "";
			}

			_characterRead = true;

			return Convert.ToChar(Buffer[Buffer.Count - 1]).ToString();
		}
	}
}
