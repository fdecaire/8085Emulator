using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog;

namespace Processor
{
	public class Memory
	{
		private static Logger logger;

		//TODO: add a status so we can tell if a memory cell is ROM or unavailable
		private List<byte> _memoryCell = new List<byte>();

		public Memory()
		{
			logger = LogManager.GetCurrentClassLogger();

			_memoryCell.Clear();
			for (int i = 0; i < 65536; i++)
			{
				_memoryCell.Add(new byte());
			}
		}

		public void Clear()
		{
			for (int i = 0; i < 65536; i++)
			{
				_memoryCell[i] = 0;
			}
		}

		public byte this[int address]
		{
			get
			{
				return _memoryCell[address];
			}
			set
			{
				_memoryCell[address] = value;
			}
		}

		// dump the memory 
		public void Dump()
		{
			for (int i = 0; i < 8000; i++)
			{
				logger.Debug(i.ToString("X2").PadLeft(4, '0') + "  " + _memoryCell[i].ToString("X2"));
			}
		}

		public void LoadProgram(string fileName)
		{
			//TODO: load a program from a file (assembler needed for this)
		}

		public void LoadMachineCodeFromFile(string fileName)
		{
			using (var reader = new StreamReader(fileName))
			{
				string program = reader.ReadToEnd();
				LoadMachineCodeDirect(program);
			}
		}


		public void LoadMachineCodeDirect(string program)
		{
			// remove the colon and next 4 bytes (8 chars) - ":1D000000"
			bool found = true;
			while (found)
			{
				var pos = program.IndexOf(":", StringComparison.Ordinal);

				if (pos > -1)
				{
					program = program.Remove(pos, 9);
				}
				else
				{
					found = false;
				}
			}

			program = program.Replace("\n", "");
			program = program.Replace("\r", "");
			program = program.Replace(",", "");


			// load a string of hex data directly into memory
			var temp = Enumerable.Range(0, program.Length)
					 .Where(x => x % 2 == 0)
					 .Select(x => Convert.ToByte(program.Substring(x, 2), 16))
					 .ToArray();

			for (int i = 0; i < temp.Length; i++)
			{
				_memoryCell[i] = temp[i];
			}
		}

		public List<byte> VideoRam()
		{
			//2400 - 3FFF 7K Video RAM
			var buffer = new List<byte>();
			for (int i = 0x2400; i < 0x4000; i++)
			{
				buffer.Add(_memoryCell[i]);
			}

			return buffer;
		}
	}
}
