using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
	public class SourceLine
	{
		public string Label { get; set; }
		public OpcodeEnum OpCode { get; set; }
		public string Operand { get; set; }
		public string Comment { get; set; }
		public int InstructionSize { get; set; }

		public SourceLine(string line)
		{
			line = line.ConvertTabsToSpaces();

			var results = GetTokens(line);

			Label = "";
			OpCode = OpcodeEnum.NONE;
			InstructionSize = 0;
			Operand = "";
			Comment = "";

			if (results.Count == 0)
			{
				return;
			}

			Label = results[0].Replace(":", "");

			if (results.Count == 1)
			{
				return;
			}

			if (results[1] != "")
			{
				DecodeOpcode(results[1]);
				LookupInstructionSize();
			}

			if (results.Count == 2)
			{
				return;
			}

			Operand = results[2];

			if (results.Count == 3)
			{
				return;
			}

			Comment = results[3];
		}

		private void LookupInstructionSize()
		{
			InstructionSize = 0;

			switch (OpCode)
			{
				case OpcodeEnum.ADC:
				case OpcodeEnum.ADD:
				case OpcodeEnum.ANA:
				case OpcodeEnum.CMA:
				case OpcodeEnum.CMC:
				case OpcodeEnum.CMP:
				case OpcodeEnum.DAA:
				case OpcodeEnum.DAD:
				case OpcodeEnum.DCR:
				case OpcodeEnum.DCX:
				case OpcodeEnum.DI:
				case OpcodeEnum.EI:
				case OpcodeEnum.HLT:
				case OpcodeEnum.INR:
				case OpcodeEnum.INX:
				case OpcodeEnum.LDAX:
				case OpcodeEnum.MOV:
				case OpcodeEnum.NOP:
				case OpcodeEnum.ORA:
				case OpcodeEnum.PCHL:
				case OpcodeEnum.POP:
				case OpcodeEnum.PUSH:
				case OpcodeEnum.RAL:
				case OpcodeEnum.RAR:
				case OpcodeEnum.RC:
				case OpcodeEnum.RET:
				case OpcodeEnum.RLC:
				case OpcodeEnum.RM:
				case OpcodeEnum.RNC:
				case OpcodeEnum.RNZ:
				case OpcodeEnum.RP:
				case OpcodeEnum.RPE:
				case OpcodeEnum.RPO:
				case OpcodeEnum.RRC:
				case OpcodeEnum.RZ:
				case OpcodeEnum.SBB:
				case OpcodeEnum.SIM:
				case OpcodeEnum.SPHL:
				case OpcodeEnum.STAX:
				case OpcodeEnum.STC:
				case OpcodeEnum.SUB:
				case OpcodeEnum.XCHG:
				case OpcodeEnum.XRA:
				case OpcodeEnum.XTHL:
					InstructionSize = 1;
					break;
				case OpcodeEnum.ACI:
				case OpcodeEnum.ADI:
				case OpcodeEnum.ANI:
				case OpcodeEnum.CC:
				case OpcodeEnum.CM:
				case OpcodeEnum.CPI:
				case OpcodeEnum.IN:
				case OpcodeEnum.LDA:
				case OpcodeEnum.LHLD:
				case OpcodeEnum.MVI:
				case OpcodeEnum.ORI:
				case OpcodeEnum.OUT:
				case OpcodeEnum.SBI:
				case OpcodeEnum.SHLD:
				case OpcodeEnum.STA:
				case OpcodeEnum.SUI:
				case OpcodeEnum.XRI:
					InstructionSize = 2;
					break;
				case OpcodeEnum.CALL:
				case OpcodeEnum.CNC:
				case OpcodeEnum.CNZ:
				case OpcodeEnum.CP:
				case OpcodeEnum.CPE:
				case OpcodeEnum.CPO:
				case OpcodeEnum.CZ:
				case OpcodeEnum.JC:
				case OpcodeEnum.JM:
				case OpcodeEnum.JMP:
				case OpcodeEnum.JNC:
				case OpcodeEnum.JNZ:
				case OpcodeEnum.JP:
				case OpcodeEnum.JPE:
				case OpcodeEnum.JPO:
				case OpcodeEnum.JZ:
				case OpcodeEnum.LXI:
					InstructionSize = 3;
					break;
			}
		}

		private void DecodeOpcode(string s)
		{
			OpCode =(OpcodeEnum)Enum.Parse(typeof(OpcodeEnum), s.ToUpper());
		}

		public List<string> GetTokens(string line)
		{
			List<string> results = new List<string>();

			bool inToken = false;
			bool inQuote = false;
			bool inParens = false;
			bool inComment = false;
			string currentToken = "";
			for (int i = 0; i < line.Length; i++)
			{
				string currentChar = line.Substring(i, 1);

				if (currentChar == " " && results.Count == 0 && i == 0)
				{
					results.Add(""); // label or name is missing
				}

				if (currentChar == ";")
				{
					currentToken += currentChar;
					inComment = true;
				}
				else if (inComment)
				{
					currentToken += currentChar;
				}
				else if (inParens)
				{
					currentToken += currentChar;
					if (currentChar == ")")
					{
						results.Add(currentToken);
						inParens = false;
					}
				}
				else if (currentChar == "(" && inQuote == false)
				{
					currentToken += currentChar;
					inParens = true;
				}
				else if (inQuote)
				{
					currentToken += currentChar;
					if (currentChar == "'")
					{
						results.Add(currentToken);
						inQuote = false;
					}
				}
				else if (currentChar == "'")
				{
					currentToken += currentChar;
					inQuote = true;
				}
				else if (currentChar != " ")
				{
					currentToken += currentChar;
					inToken = true;
				}
				else if (currentChar == " " && inToken)
				{
					results.Add(currentToken);
					currentToken = "";
					inToken = false;
				}
			}

			if (inComment)
			{
				while (results.Count < 3)
				{
					results.Add("");
				}

				results.Add(currentToken);
			}

			if (inToken || inQuote)
			{
				results.Add(currentToken);
			}

			return results;
		}
	}
}
