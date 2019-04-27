using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Assembler
{
    public class Assembler
    {
	    public string HexResult { get; set; }
		public string HexTestResult { get; set; }
	    public string AssemblyCode { get; set; }
	    private Dictionary<string, LabelRecord> _labels = new Dictionary<string, LabelRecord>();
	    public int CurrentAddress { get; set; } = 0;

	    public void ReadAssemFile(string sourceFile)
	    {
			using (var reader = new StreamReader(sourceFile))
			{
				AssemblyCode = reader.ReadToEnd();
			}
		}

	    public void SaveAssemFile(string destinationFile)
	    {
			using (var writer = new StreamWriter(destinationFile))
			{
				writer.Write(AssemblyCode);
			}
		}

	    public void ReadHexFile(string sourceFile)
	    {
			using (var reader = new StreamReader(sourceFile))
			{
				HexResult = reader.ReadToEnd();

				// remove the colon and next 4 bytes (8 chars) - ":1D000000"
				bool found = true;
				while (found)
				{
					var pos = HexResult.IndexOf(":", StringComparison.Ordinal);

					if (pos > -1)
					{
						HexResult = HexResult.Remove(pos, 9);
					}
					else
					{
						found = false;
					}
				}
			}
		}

	    public void SaveHexFile(string destinationFile)
	    {
			// save the raw hex data as a .hex  text file
		    using (var writer = new StreamWriter(destinationFile))
		    {
			    writer.Write(HexResult);
		    }
	    }

	    public void SaveHumanReadableHexFile(string destinationFile)
	    {
			using (var writer = new StreamWriter(destinationFile))
			{
				writer.Write(HexTestResult);
			}
		}

	    public void DissassembleCode()
	    {
			var disassembler = new Disassembler();

			AssemblyCode = disassembler.Parse(HexResult);
	    }

	    private void AppendHexResult(string code)
	    {
		    HexResult += code;
		    HexTestResult += code;
	    }

	    public void AssembleCode()
	    {
		    string[] regPair;
		    string tempData;

			CurrentAddress = 0;
		    HexResult = "";
		    HexTestResult = "";


		    using (var reader = new StringReader(AssemblyCode))
		    {
			    string line;
			    while ((line = reader.ReadLine()) != null)
			    {
				    var sourceLine = new SourceLine(line);

				    if (sourceLine.Label != "" && sourceLine.OpCode != OpcodeEnum.EQU)
				    {
					    if (!_labels.ContainsKey(sourceLine.Label))
					    {
						    _labels.Add(sourceLine.Label, new LabelRecord
							    {
								    name = CurrentAddress.ToHex().PadLeft(4, '0').SwapHex(),
								    size = 2
							    }
						    );
					    }
					    else
					    {
						    if (_labels[sourceLine.Label].name == "")
						    {
							    _labels[sourceLine.Label].name = CurrentAddress.ToHex().PadLeft(4, '0').SwapHex();
							    _labels[sourceLine.Label].size = 2;
						    }
						    else
						    {
							    throw new Exception("Labels can only be defined once!");
						    }
					    }
				    }

				    if (sourceLine.OpCode == OpcodeEnum.NONE && sourceLine.Operand == "")
				    {
					    // must be a comment line
					    continue;
				    }

				    HexTestResult += CurrentAddress.ToHex().PadLeft(4, '0') + "  ";

				    switch (sourceLine.OpCode)
				    {
					    case OpcodeEnum.DB:
						    tempData = ReadByteData(sourceLine.Operand);

						    CurrentAddress += tempData.Length/2;
						    AppendHexResult(tempData);
						    break;
					    case OpcodeEnum.DW:
						    tempData = ReadWordData(sourceLine.Operand);

						    CurrentAddress += tempData.Length/2;
						    AppendHexResult(tempData);
						    break;
					    case OpcodeEnum.DS: // reserve memory space
					    case OpcodeEnum.ORG: // origin

						    int total = 0;
						    if (sourceLine.Operand.IsHex())
						    {
							    total = sourceLine.Operand.HexToInt();
						    }
						    else
						    {
							    total = sourceLine.Operand.ToInt();
						    }

						    CurrentAddress += total;

						    for (int i = 0; i < total; i++)
						    {
							    HexResult += "00";
						    }
						    break;
					    case OpcodeEnum.END:
						    break;
					    case OpcodeEnum.EQU:
						    if (!_labels.ContainsKey(sourceLine.Label))
						    {
							    _labels.Add(sourceLine.Label, new LabelRecord {name = ParseFormula(sourceLine.Operand, 1), size = 2});
						    }
						    else
						    {
							    _labels[sourceLine.Label].name = ParseFormula(sourceLine.Operand, 1);
							    _labels[sourceLine.Label].size = 2;
						    }
						    break;
					    case OpcodeEnum.ACI:
						    AppendHexResult("CE ");
						    AppendHexResult(sourceLine.Operand.ToHex());
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.ADC:
						    AppendHexResult((0x88 + SourceRegisterNumber(sourceLine.Operand)).ToHex());
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.ADD:
						    AppendHexResult((0x80 + SourceRegisterNumber(sourceLine.Operand)).ToHex());
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.ADI:
						    AppendHexResult("C6 " + GetOperand(sourceLine.Operand, 1));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.ANA:
						    AppendHexResult((0xa0 + SourceRegisterNumber(sourceLine.Operand)).ToHex());
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.ANI:
						    AppendHexResult("E6 " + GetOperand(sourceLine.Operand, 1));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.CALL:
						    AppendHexResult("CD " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.CC:
						    AppendHexResult("DC " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.CM:
						    AppendHexResult("FC " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.CMA:
						    AppendHexResult("2F ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.CMC:
						    AppendHexResult("3F ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.CMP:
						    AppendHexResult((0xb8 + SourceRegisterNumber(sourceLine.Operand)).ToHex() + " ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.CNC:
						    AppendHexResult("D4 " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.CNZ:
						    AppendHexResult("C4 " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.CP:
						    AppendHexResult("F4 " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.CPE:
						    AppendHexResult("EC " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.CPI:
						    AppendHexResult("FE " + GetOperand(sourceLine.Operand, 1));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.CPO:
						    AppendHexResult("E4 " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.CZ:
						    AppendHexResult("CC " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.DAA:
						    AppendHexResult("27 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.DAD:
						    AppendHexResult((0x09 + DoubleRegisterNumber(sourceLine.Operand)).ToHex() + " ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.DCR:
						    AppendHexResult((0x05 + DestinationRegisterNumber(sourceLine.Operand)).ToHex() + " ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.DCX:
						    AppendHexResult((0x0b + DoubleRegisterNumber(sourceLine.Operand)).ToHex() + " ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.DI:
						    AppendHexResult("F3 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.EI:
						    AppendHexResult("FB ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.HLT:
						    AppendHexResult("76 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.IN:
						    AppendHexResult("DB " + GetOperand(sourceLine.Operand, 1));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.INR:
						    AppendHexResult((0x04 + DestinationRegisterNumber(sourceLine.Operand)).ToHex() + " ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.INX:
						    AppendHexResult((0x03 + DoubleRegisterNumber(sourceLine.Operand)).ToHex() + " ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.JC:
						    AppendHexResult("DA " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.JM:
						    AppendHexResult("FA " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.JMP:
						    AppendHexResult("C3 " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.JNC:
						    AppendHexResult("E2 " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.JNZ:
						    AppendHexResult("C2 " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.JP:
						    AppendHexResult("F2 " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.JPE:
						    AppendHexResult("EA " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.JPO:
						    AppendHexResult("E2 " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.JZ:
						    AppendHexResult("CA " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.LDA:
						    AppendHexResult("3A " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.LDAX:
						    AppendHexResult((0x0a + DoubleRegisterNumber(sourceLine.Operand)).ToHex() + " ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.LHLD:
						    AppendHexResult("2A " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.LXI:
						    regPair = sourceLine.Operand.Split(',');
						    AppendHexResult((0x01 + DoubleRegisterNumber(regPair[0])).ToHex() + " " + GetOperand(regPair[1], 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.MOV:
						    regPair = sourceLine.Operand.Split(',');
						    AppendHexResult((0x40 + DestinationRegisterNumber(regPair[0]) + SourceRegisterNumber(regPair[1])).ToHex() + " ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.MVI:
						    regPair = sourceLine.Operand.Split(',');
						    AppendHexResult((0x06 + DestinationRegisterNumber(regPair[0])).ToHex() + " " + GetOperand(regPair[1], 1));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.NOP:
						    AppendHexResult("00 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.ORA:
						    AppendHexResult((0xb0 + SourceRegisterNumber(sourceLine.Operand)).ToHex() + " ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.ORI:
						    AppendHexResult("F6 " + GetOperand(sourceLine.Operand, 1));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.OUT:
						    AppendHexResult("D3 " + GetOperand(sourceLine.Operand, 1));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.PCHL:
						    AppendHexResult("E9 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.POP:
						    AppendHexResult((0xc1 + DoubleRegisterNumber(sourceLine.Operand)).ToHex() + " ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.PUSH:
						    AppendHexResult((0xc5 + DoubleRegisterNumber(sourceLine.Operand)).ToHex() + " ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.RAL:
						    AppendHexResult("17 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.RAR:
						    AppendHexResult("1F ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.RC:
						    AppendHexResult("D8 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.RET:
						    AppendHexResult("C9 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.RLC:
						    AppendHexResult("03 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.RM:
						    AppendHexResult("F8 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.RNC:
						    AppendHexResult("D0 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.RNZ:
						    AppendHexResult("C0 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.RP:
						    AppendHexResult("F0 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.RPE:
						    AppendHexResult("E8 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.RPO:
						    AppendHexResult("E0 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.RRC:
						    AppendHexResult("0F ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.RZ:
						    AppendHexResult("C8 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.SBB:
						    AppendHexResult((0x98 + SourceRegisterNumber(sourceLine.Operand)).ToHex() + " ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.SBI:
						    AppendHexResult("DE " + GetOperand(sourceLine.Operand, 1));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.SHLD:
						    AppendHexResult("22 " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.SIM:
						    AppendHexResult("30 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.SPHL:
						    AppendHexResult("F9 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.STA:
						    AppendHexResult("32 " + GetOperand(sourceLine.Operand, 2));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.STAX:
						    AppendHexResult((0x02 + DoubleRegisterNumber(sourceLine.Operand)).ToHex() + " ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.STC:
						    AppendHexResult("37 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.SUB:
						    AppendHexResult((0x90 + SourceRegisterNumber(sourceLine.Operand)).ToHex() + " ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.SUI:
						    AppendHexResult("D6 " + GetOperand(sourceLine.Operand, 1));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.XCHG:
						    AppendHexResult("EB ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.XRA:
						    AppendHexResult((0xa8 + SourceRegisterNumber(sourceLine.Operand)).ToHex() + " ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.XRI:
						    AppendHexResult("EE " + GetOperand(sourceLine.Operand, 1));
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    case OpcodeEnum.XTHL:
						    AppendHexResult("E3 ");
						    CurrentAddress += sourceLine.InstructionSize;
						    break;
					    default:

						    break;
				    }
				    /*
				    if (currentAddress != HexResult.Replace("\n", "").Length / 2)
				    {
					    throw new Exception("address incorrect");
				    }
				    
				    */
				    HexTestResult += "\n";
			    }
		    }

		    foreach (var item in _labels)
		    {
			    // verify all labels have addresses
			    if (item.Value.name == "")
			    {
					// this label could be a formula.
				    var result = ParseFormula(item.Key, 2);

				    if (result.Contains("|"))
				    {
					    throw new Exception($"missing label {item.Key}");
				    }
				    else
				    {
						HexResult = HexResult.Replace("|" + item.Key + "|", result.PadRight(item.Value.size * 2, '0'));
						HexTestResult = HexTestResult.Replace("|" + item.Key + "|", result.PadRight(item.Value.size * 2, '0').SpaceByBytes());
					}
			    }
			    else
			    {
					// replace label names in hexdata with addresses
				    HexResult = HexResult.Replace("|" + item.Key + "|", item.Value.name.PadRight(item.Value.size * 2, '0'));
					HexTestResult = HexTestResult.Replace("|" + item.Key + "|", item.Value.name.PadRight(item.Value.size * 2, '0').SpaceByBytes());
				}
			}

		    HexResult = HexResult.Replace(" ", "");

		    if (HexResult.Contains("|"))
		    {
			    throw new Exception("Label replace failure");
		    }
		}

	    private string ReadByteData(string line)
	    {
		    var hexResult = "";
		    var tempVars = line.Split(',');

		    foreach (var vars in tempVars)
		    {
				hexResult += ParseFormula(vars,1);
			}

		    return hexResult;
	    }

	    private string ReadWordData(string line)
	    {
			var hexResult = "";
			var tempVars = line.Split(',');

			foreach (var vars in tempVars)
			{
				hexResult += ParseFormula(vars, 2);
			}

			return hexResult;
		}

	    private string ParseFormula(string line, int bytesize)
	    {
		    if (!(line.StartsWith("'") || line.StartsWith("\"")))
		    {
			    line = line.Replace("(", "").Replace(")", "");
		    }

		    //TODO: this is a temporary hack for now.  Need to write a formula parser to handle all cases
		    if (line.StartsWith("'") && line.EndsWith("'"))
		    {
			    return AsciiToHex(line);
		    }

            if (line.Contains("+"))
            {
                string[] temp = line.Split('+');
                int result1 = ReadNumber(temp[0]);
                int result2 = ReadNumber(temp[1]);

                if (result1 == -1 || result2 == -1)
                {
                    if (!_labels.ContainsKey(line))
                    {
                        _labels.Add(line, new LabelRecord {name = "", size = bytesize});
                    }
                    return $"|{line}|";
                }

                return (result1 + result2).ToHex().PadRight(bytesize*2, '0');
            }

            if (line.Contains("-"))
            {
                string[] temp = line.Split('-');
                int result1 = ReadNumber(temp[0]);
                int result2 = ReadNumber(temp[1]);

                if (result1 == -1 || result2 == -1)
                {
                    if (!_labels.ContainsKey(line))
                    {
                        _labels.Add(line, new LabelRecord { name = "", size = bytesize });
                    }
                    return $"|{line}|";
                }

                return (result1 - result2).ToHex().PadRight(bytesize*2, '0');
            }

            if (line.Contains("/"))
            {
                string[] temp = line.Split('/');
                int result1 = ReadNumber(temp[0]);
                int result2 = ReadNumber(temp[1]);

                if (result1 == -1 || result2 == -1)
                {
                    if (!_labels.ContainsKey(line))
                    {
                        _labels.Add(line, new LabelRecord { name = "", size = bytesize });
                    }
                    return $"|{line}|";
                }

                return (result1/result2).ToHex().PadRight(bytesize*2, '0');
            }

            if (line.Contains(" AND "))
            {
                string[] temp = line.Split(new string[] {" AND "}, StringSplitOptions.None);
                int result1 = ReadNumber(temp[0]);
                int result2 = ReadNumber(temp[1]);

                if (result1 == -1 || result2 == -1)
                {
                    if (!_labels.ContainsKey(line))
                    {
                        _labels.Add(line, new LabelRecord { name = "", size = bytesize });
                    }
                    return $"|{line}|";
                }

                return (result1 & result2).ToHex().PadRight(bytesize*2, '0');
            }

            if (line.Contains(" OR "))
            {
                string[] temp = line.Split(new string[] {" OR "}, StringSplitOptions.None);
                int result1 = ReadNumber(temp[0]);
                int result2 = ReadNumber(temp[1]);

                if (result1 == -1 || result2 == -1)
                {
                    if (!_labels.ContainsKey(line))
                    {
                        _labels.Add(line, new LabelRecord { name = "", size = bytesize });
                    }
                    return $"|{line}|";
                }

                return (result1 | result2).ToHex().PadRight(bytesize*2, '0');
            }

            if (line.IsHex())
            {
                string temp = line.Replace("H", "");

                if (temp.Length > bytesize*2)
                {
                    temp = temp.Substring(temp.Length - bytesize*2, bytesize*2);
                }

                if (temp.Length < bytesize*2)
                {
                    temp = temp.PadRight(bytesize * 2, '0');
                }

                if (bytesize == 2)
                {
                    temp = temp.SwapHex();
                }

                return temp;
            }

            if (line.IsNumber())
            {
                return line.ToInt().ToHex().PadRight(bytesize*2, '0');
            }

            // must be a label
		    if (!_labels.ContainsKey(line))
		    {
				_labels.Add(line, new LabelRecord { name = "", size = bytesize });
			}

		    return $"|{line}|";
	    }

	    private int ReadNumber(string number)
	    {
		    int result;

		    number = number.Trim();
			if (number.IsHex())
			{
				result = number.Replace("H", "").HexToInt();
			}
			else if (number.IsNumber())
			{
				result = number.ToInt();
			}
			else
			{
				if (_labels.ContainsKey(number))
				{
					if (_labels[number].name == "")
					{
						result = -1;
					}
					else
					{
						result = _labels[number].name.HexToInt();
					}
				}
				else
				{
					return -1; // must be a label that is not yet defined
				}
			}

		    return result;
	    }

	    private string AsciiToHex(string s)
	    {
		    var temp = s.Replace("'", "");

			var sb = new StringBuilder();

			var inputBytes = Encoding.UTF8.GetBytes(temp);

			foreach (byte b in inputBytes)
			{
				sb.Append($"{b:X2}");
			}

			return sb.ToString();
		}

		private int DoubleRegisterNumber(string registerName)
	    {
		    switch (registerName.ToUpper())
		    {
				case "B":
				    return 0x00;
				case "D":
				    return 0x10;
				case "H":
				    return 0x20;
				case "SP":
				    return 0x30;
				case "PSW":
					return 0x30;
			}
		    return 0;
	    }

	    private string GetOperand(string s, int byteSize)
	    {
		    return ParseFormula(s,byteSize);
	    }

	    public byte SourceRegisterNumber(string registerName)
	    {
		    switch (registerName.ToUpper())
		    {
			    case "B":
				    return 0;
			    case "C":
				    return 1;
			    case "D":
				    return 2;
			    case "E":
				    return 3;
			    case "H":
				    return 4;
			    case "L":
				    return 5;
			    case "M":
				    return 6;
				case "A":
					return 7;
			}
		    return 0;
	    }

		public byte DestinationRegisterNumber(string registerName)
		{
			switch (registerName.ToUpper())
			{
				case "B":
					return 0x00;
				case "C":
					return 0x08;
				case "D":
					return 0x10;
				case "E":
					return 0x18;
				case "H":
					return 0x20;
				case "L":
					return 0x28;
				case "M":
					return 0x30;
				case "A":
					return 0x38;
			}
			return 0;
		}
	}
}
