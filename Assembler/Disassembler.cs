namespace Assembler
{
	public class Disassembler
	{
		private string _hexData;
		private int _currentLocation = 0; // current location in the _hexData string to parse

		private string GetNextHexPair()
		{
			if (_hexData.Length >= _currentLocation*2 + 2)
			{
				string result = _hexData.Substring(_currentLocation*2, 2);
				_currentLocation++;

				return result;
			}
			return "";
		}

		// read the next two hex values as an address
		private string GetAddressData()
		{
			var lower = GetNextHexPair();
			var higher = GetNextHexPair();

			return higher + lower;
		}

		private string GetAddress()
		{
			return (_currentLocation - 1).ToString("X4");
		}

		public string Parse(string hexData)
		{
			_hexData = hexData.Replace(",", "").Replace(" ", "").Replace("\n", "").Replace("\r", "");
			var assemblyCode = ""; //TODO: convert this into StringBuilder

			var nextOpCode = GetNextHexPair();
			while (nextOpCode != "")
			{
				assemblyCode += GetAddress();

				//TODO: refactor this (using brute force for now)
				switch (nextOpCode)
				{
					case "00":
						assemblyCode += "\t\tNOP\n";
						break;
					case "01":
						assemblyCode += "\t\tLXI\tB," + GetAddressData() + "\n";
						break;
					case "02":
						assemblyCode += "\t\tSTAX\tB\n";
						break;
					case "03":
						assemblyCode += "\t\tINX\tB\n";
						break;
					case "04":
						assemblyCode += "\t\tINR\tB\n";
						break;
					case "05":
						assemblyCode += "\t\tDCR\tB\n";
						break;
					case "06":
						assemblyCode += "\t\tMVI\tB," + GetNextHexPair() + "\n";
						break;
					case "07":
						assemblyCode += "\t\tRLC\n";
						break;
					case "08":
						// no opcode
						break;
					case "09":
						assemblyCode += "\t\tdDAD\tB\n";
						break;
					case "0A":
						assemblyCode += "\t\tLDAX\tB\n";
						break;
					case "0B":
						assemblyCode += "\t\tDCX\tB\n";
						break;
					case "0C":
						assemblyCode += "\t\tINR\tC\n";
						break;
					case "0D":
						assemblyCode += "\t\tDCR\tC\n";
						break;
					case "0E":
						assemblyCode += "\t\tMVI\tC," + GetNextHexPair() + "\n";
						break;
					case "0F":
						assemblyCode += "\t\tRRC\n";
						break;
					case "10":
						// no opcode
						assemblyCode += "\n";
						break;
					case "11":
						assemblyCode += "\t\tLXI\tD," + GetAddressData() + "\n";
						break;
					case "12":
						assemblyCode += "\t\tSTAX\tD\n";
						break;
					case "13":
						assemblyCode += "\t\tINX\tD\n";
						break;
					case "14":
						assemblyCode += "\t\tINR\tD\n";
						break;
					case "15":
						assemblyCode += "\t\tDCR\tD\n";
						break;
					case "16":
						assemblyCode += "\t\tMVI\tD," + GetNextHexPair() + "\n";
						break;
					case "17":
						assemblyCode += "\t\tRAL\n";
						break;
					case "18":
						// no opcode
						assemblyCode += "\n";
						break;
					case "19":
						assemblyCode += "\t\tDAD\tD\n";
						break;
					case "1A":
						assemblyCode += "\t\tLDAX\tD\n";
						break;
					case "1B":
						assemblyCode += "\t\tDCX\tD\n";
						break;
					case "1C":
						assemblyCode += "\t\tINR\tE\n";
						break;
					case "1D":
						assemblyCode += "\t\tDCR\tE\n";
						break;
					case "1E":
						assemblyCode += "\t\tMVI\tE," + GetNextHexPair() + "\n";
						break;
					case "1F":
						assemblyCode += "\t\tRAR\n";
						break;
					case "20":
						assemblyCode += "\t\tRIM\n";
						break;
					case "21":
						assemblyCode += "\t\tLXI\tH," + GetAddressData() + "\n";
						break;
					case "22":
						assemblyCode += "\t\tSHLD\t" + GetAddressData() + "\n";
						break;
					case "23":
						assemblyCode += "\t\tINX\tH\n";
						break;
					case "24":
						assemblyCode += "\t\tINR\tH\n";
						break;
					case "25":
						assemblyCode += "\t\tDCR\tH\n";
						break;
					case "26":
						assemblyCode += "\t\tMVI\tH," + GetNextHexPair() + "\n";
						break;
					case "27":
						assemblyCode += "\t\tDAA\n";
						break;
					case "28":
						// no opcode
						assemblyCode += "\n";
						break;
					case "29":
						assemblyCode += "\t\tDAD\tH\n";
						break;
					case "2A":
						assemblyCode += "\t\tLHLD\t" + GetAddressData() + "\n";
						break;
					case "2B":
						assemblyCode += "\t\tDCX\tH\n";
						break;
					case "2C":
						assemblyCode += "\t\tINR\tL\n";
						break;
					case "2D":
						assemblyCode += "\t\tDCR\tL\n";
						break;
					case "2E":
						assemblyCode += "\t\tMVI\tL," + GetNextHexPair() + "\n";
						break;
					case "2F":
						assemblyCode += "\t\tCMA\n";
						break;
					case "30":
						assemblyCode += "\t\tSIM\n";
						break;
					case "31":
						assemblyCode += "\t\tLXI\tSP," + GetAddressData() + "\n";
						break;
					case "32":
						assemblyCode += "\t\tSTA\t" + GetAddressData() + "\n";
						break;
					case "33":
						assemblyCode += "\t\tINX\tSP\n";
						break;
					case "34":
						assemblyCode += "\t\tINR\tM\n";
						break;
					case "35":
						assemblyCode += "\t\tDCR\tM\n";
						break;
					case "36":
						assemblyCode += "\t\tMVI\tM," + GetNextHexPair() + "\n";
						break;
					case "37":
						assemblyCode += "\t\tSTC\n";
						break;
					case "38":
						// no opcode
						assemblyCode += "\n";
						break;
					case "39":
						assemblyCode += "\t\tDAD\tSP\n";
						break;
					case "3A":
						assemblyCode += "\t\tLDA\t" + GetAddressData() + "\n";
						break;
					case "3B":
						assemblyCode += "\t\tDCX\tSP\n";
						break;
					case "3C":
						assemblyCode += "\t\tINR\tA\n";
						break;
					case "3D":
						assemblyCode += "\t\tDCR\tA\n";
						break;
					case "3E":
						assemblyCode += "\t\tMVI\tA," + GetNextHexPair() + "\n";
						break;
					case "3F":
						assemblyCode += "\t\tCMC\n";
						break;
					case "40":
						assemblyCode += "\t\tMOV\tB,B\n";
						break;
					case "41":
						assemblyCode += "\t\tMOV\tB,C\n";
						break;
					case "42":
						assemblyCode += "\t\tMOV\tB,D\n";
						break;
					case "43":
						assemblyCode += "\t\tMOV\tB,E\n";
						break;
					case "44":
						assemblyCode += "\t\tMOV\tB,H\n";
						break;
					case "45":
						assemblyCode += "\t\tMOV\tB,L\n";
						break;
					case "46":
						assemblyCode += "\t\tMOV\tB,M\n";
						break;
					case "47":
						assemblyCode += "\t\tMOV\tB,A\n";
						break;
					case "48":
						assemblyCode += "\t\tMOV\tC,B\n";
						break;
					case "49":
						assemblyCode += "\t\tMOV\tC,C\n";
						break;
					case "4A":
						assemblyCode += "\t\tMOV\tC,D\n";
						break;
					case "4B":
						assemblyCode += "\t\tMOV\tC,E\n";
						break;
					case "4C":
						assemblyCode += "\t\tMOV\tC,H\n";
						break;
					case "4D":
						assemblyCode += "\t\tMOV\tC,L\n";
						break;
					case "4E":
						assemblyCode += "\t\tMOV\tC,M\n";
						break;
					case "4F":
						assemblyCode += "\t\tMOV\tC,A\n";
						break;
					case "50":
						assemblyCode += "\t\tMOV\tD,B\n";
						break;
					case "51":
						assemblyCode += "\t\tMOV\tD,C\n";
						break;
					case "52":
						assemblyCode += "\t\tMOV\tD,D\n";
						break;
					case "53":
						assemblyCode += "\t\tMOV\tD,E\n";
						break;
					case "54":
						assemblyCode += "\t\tMOV\tD,H\n";
						break;
					case "55":
						assemblyCode += "\t\tMOV\tD,L\n";
						break;
					case "56":
						assemblyCode += "\t\tMOV\tD,M\n";
						break;
					case "57":
						assemblyCode += "\t\tMOV\tD,A\n";
						break;
					case "58":
						assemblyCode += "\t\tMOV\tE,B\n";
						break;
					case "59":
						assemblyCode += "\t\tMOV\tE,C\n";
						break;
					case "5A":
						assemblyCode += "\t\tMOV\tE,D\n";
						break;
					case "5B":
						assemblyCode += "\t\tMOV\tE,E\n";
						break;
					case "5C":
						assemblyCode += "\t\tMOV\tE,H\n";
						break;
					case "5D":
						assemblyCode += "\t\tMOV\tE,L\n";
						break;
					case "5E":
						assemblyCode += "\t\tMOV\tE,M\n";
						break;
					case "5F":
						assemblyCode += "\t\tMOV\tE,A\n";
						break;
					case "60":
						assemblyCode += "\t\tMOV\tH,B\n";
						break;
					case "61":
						assemblyCode += "\t\tMOV\tH,C\n";
						break;
					case "62":
						assemblyCode += "\t\tMOV\tH,D\n";
						break;
					case "63":
						assemblyCode += "\t\tMOV\tH,E\n";
						break;
					case "64":
						assemblyCode += "\t\tMOV\tH,H\n";
						break;
					case "65":
						assemblyCode += "\t\tMOV\tH,L\n";
						break;
					case "66":
						assemblyCode += "\t\tMOV\tH,M\n";
						break;
					case "67":
						assemblyCode += "\t\tMOV\tH,A\n";
						break;
					case "68":
						assemblyCode += "\t\tMOV\tL,B\n";
						break;
					case "69":
						assemblyCode += "\t\tMOV\tL,C\n";
						break;
					case "6A":
						assemblyCode += "\t\tMOV\tL,D\n";
						break;
					case "6B":
						assemblyCode += "\t\tMOV\tL,E\n";
						break;
					case "6C":
						assemblyCode += "\t\tMOV\tL,H\n";
						break;
					case "6D":
						assemblyCode += "\t\tMOV\tL,L\n";
						break;
					case "6E":
						assemblyCode += "\t\tMOV\tL,M\n";
						break;
					case "6F":
						assemblyCode += "\t\tMOV\tL,A\n";
						break;
					case "70":
						assemblyCode += "\t\tMOV\tM,B\n";
						break;
					case "71":
						assemblyCode += "\t\tMOV\tM,C\n";
						break;
					case "72":
						assemblyCode += "\t\tMOV\tM,D\n";
						break;
					case "73":
						assemblyCode += "\t\tMOV\tM,E\n";
						break;
					case "74":
						assemblyCode += "\t\tMOV\tM,H\n";
						break;
					case "75":
						assemblyCode += "\t\tMOV\tM,L\n";
						break;
					case "76":
						assemblyCode += "\t\tHLT\n";
						break;
					case "77":
						assemblyCode += "\t\tMOV\tM,A\n";
						break;
					case "78":
						assemblyCode += "\t\tMOV\tA,B\n";
						break;
					case "79":
						assemblyCode += "\t\tMOV\tA,C\n";
						break;
					case "7A":
						assemblyCode += "\t\tMOV\tA,D\n";
						break;
					case "7B":
						assemblyCode += "\t\tMOV\tA,E\n";
						break;
					case "7C":
						assemblyCode += "\t\tMOV\tA,H\n";
						break;
					case "7D":
						assemblyCode += "\t\tMOV\tA,L\n";
						break;
					case "7E":
						assemblyCode += "\t\tMOV\tA,M\n";
						break;
					case "7F":
						assemblyCode += "\t\tMOV\tA,A\n";
						break;
					case "80":
						assemblyCode += "\t\tADD\tB\n";
						break;
					case "81":
						assemblyCode += "\t\tADD\tC\n";
						break;
					case "82":
						assemblyCode += "\t\tADD\tD\n";
						break;
					case "83":
						assemblyCode += "\t\tADD\tE\n";
						break;
					case "84":
						assemblyCode += "\t\tADD\tH\n";
						break;
					case "85":
						assemblyCode += "\t\tADD\tL\n";
						break;
					case "86":
						assemblyCode += "\t\tADD\tM\n";
						break;
					case "87":
						assemblyCode += "\t\tADD\tA\n";
						break;
					case "88":
						assemblyCode += "\t\tADC\tB\n";
						break;
					case "89":
						assemblyCode += "\t\tADC\tC\n";
						break;
					case "8A":
						assemblyCode += "\t\tADC\tD\n";
						break;
					case "8B":
						assemblyCode += "\t\tADC\tE\n";
						break;
					case "8C":
						assemblyCode += "\t\tADC\tH\n";
						break;
					case "8D":
						assemblyCode += "\t\tADC\tL\n";
						break;
					case "8E":
						assemblyCode += "\t\tADC\tM\n";
						break;
					case "8F":
						assemblyCode += "\t\tADC\tA\n";
						break;
					case "90":
						assemblyCode += "\t\tSUB\tB\n";
						break;
					case "91":
						assemblyCode += "\t\tSUB\tC\n";
						break;
					case "92":
						assemblyCode += "\t\tSUB\tD\n";
						break;
					case "93":
						assemblyCode += "\t\tSUB\tE\n";
						break;
					case "94":
						assemblyCode += "\t\tSUB\tH\n";
						break;
					case "95":
						assemblyCode += "\t\tSUB\tL\n";
						break;
					case "96":
						assemblyCode += "\t\tSUB\tM\n";
						break;
					case "97":
						assemblyCode += "\t\tSUB\tA\n";
						break;
					case "98":
						assemblyCode += "\t\tSBB\tB\n";
						break;
					case "99":
						assemblyCode += "\t\tSBB\tC\n";
						break;
					case "9A":
						assemblyCode += "\t\tSBB\tD\n";
						break;
					case "9B":
						assemblyCode += "\t\tSBB\tE\n";
						break;
					case "9C":
						assemblyCode += "\t\tSBB\tH\n";
						break;
					case "9D":
						assemblyCode += "\t\tSBB\tL\n";
						break;
					case "9E":
						assemblyCode += "\t\tSBB\tM\n";
						break;
					case "9F":
						assemblyCode += "\t\tSBB\tA\n";
						break;
					case "A0":
						assemblyCode += "\t\tANA\tB\n";
						break;
					case "A1":
						assemblyCode += "\t\tANA\tC\n";
						break;
					case "A2":
						assemblyCode += "\t\tANA\tD\n";
						break;
					case "A3":
						assemblyCode += "\t\tANA\tE\n";
						break;
					case "A4":
						assemblyCode += "\t\tANA\tH\n";
						break;
					case "A5":
						assemblyCode += "\t\tANA\tL\n";
						break;
					case "A6":
						assemblyCode += "\t\tANA\tM\n";
						break;
					case "A7":
						assemblyCode += "\t\tANA\tA\n";
						break;
					case "A8":
						assemblyCode += "\t\tXRA\tB\n";
						break;
					case "A9":
						assemblyCode += "\t\tXRA\tC\n";
						break;
					case "AA":
						assemblyCode += "\t\tXRA\tD\n";
						break;
					case "AB":
						assemblyCode += "\t\tXRA\tE\n";
						break;
					case "AC":
						assemblyCode += "\t\tXRA\tH\n";
						break;
					case "AD":
						assemblyCode += "\t\tXRA\tL\n";
						break;
					case "AE":
						assemblyCode += "\t\tXRA\tM\n";
						break;
					case "AF":
						assemblyCode += "\t\tXRA\tA\n";
						break;
					case "B0":
						assemblyCode += "\t\tORA\tB\n";
						break;
					case "B1":
						assemblyCode += "\t\tORA\tC\n";
						break;
					case "B2":
						assemblyCode += "\t\tORA\tD\n";
						break;
					case "B3":
						assemblyCode += "\t\tORA\tE\n";
						break;
					case "B4":
						assemblyCode += "\t\tORA\tH\n";
						break;
					case "B5":
						assemblyCode += "\t\tORA\tL\n";
						break;
					case "B6":
						assemblyCode += "\t\tORA\tM\n";
						break;
					case "B7":
						assemblyCode += "\t\tORA\tA\n";
						break;
					case "B8":
						assemblyCode += "\t\tCMP\tB\n";
						break;
					case "B9":
						assemblyCode += "\t\tCMP\tC\n";
						break;
					case "BA":
						assemblyCode += "\t\tCMP\tD\n";
						break;
					case "BB":
						assemblyCode += "\t\tCMP\tE\n";
						break;
					case "BC":
						assemblyCode += "\t\tCMP\tH\n";
						break;
					case "BD":
						assemblyCode += "\t\tCMP\tL\n";
						break;
					case "BE":
						assemblyCode += "\t\tCMP\tM\n";
						break;
					case "BF":
						assemblyCode += "\t\tCMP\tA\n";
						break;
					case "C0":
						assemblyCode += "\t\tRNZ\n";
						break;
					case "C1":
						assemblyCode += "\t\tPOP\tB\n";
						break;
					case "C2":
						assemblyCode += "\t\tJNZ\t" + GetAddressData() + "\n";
						break;
					case "C3":
						assemblyCode += "\t\tJMP\t" + GetAddressData() + "\n";
						break;
					case "C4":
						assemblyCode += "\t\tCNZ\t" + GetAddressData() + "\n";
						break;
					case "C5":
						assemblyCode += "\t\tPUSH\tB\n";
						break;
					case "C6":
						assemblyCode += "\t\tADI\t" + GetNextHexPair() + "\n";
						break;
					case "C7":
						assemblyCode += "\t\tRST\t0\n";
						break;
					case "C8":
						assemblyCode += "\t\tRZ\n";
						break;
					case "C9":
						assemblyCode += "\t\tRET\t" + GetAddressData() + "\n";
						break;
					case "CA":
						assemblyCode += "\t\tJZ\t" + GetAddressData() + "\n";
						break;
					case "CB":
						// no opcode
						assemblyCode += "\n";
						break;
					case "CC":
						assemblyCode += "\t\tCZ\t" + GetAddressData() + "\n";
						break;
					case "CD":
						assemblyCode += "\t\tCALL\t" + GetAddressData() + "\n";
						break;
					case "CE":
						assemblyCode += "\t\tACI\t" + GetNextHexPair() + "\n";
						break;
					case "CF":
						assemblyCode += "\t\tRST\t1\n";
						break;
					case "D0":
						assemblyCode += "\t\tRNC\n";
						break;
					case "D1":
						assemblyCode += "\t\tPOP\tD\n";
						break;
					case "D2":
						assemblyCode += "\t\tJNC\t" + GetAddressData() + "\n";
						break;
					case "D3":
						assemblyCode += "\t\tOUT\t" + GetNextHexPair() + "\n";
						break;
					case "D4":
						assemblyCode += "\t\tCNC\t" + GetAddressData() + "\n";
						break;
					case "D5":
						assemblyCode += "\t\tPUSH\tD\n";
						break;
					case "D6":
						assemblyCode += "\t\tSUI\t" + GetNextHexPair() + "\n";
						break;
					case "D7":
						assemblyCode += "\t\tRST\t2\n";
						break;
					case "D8":
						assemblyCode += "\t\tRC\n";
						break;
					case "D9":
						// no opcode
						assemblyCode += "\n";
						break;
					case "DA":
						assemblyCode += "\t\tJC\t" + GetAddressData() + "\n";
						break;
					case "DB":
						assemblyCode += "\t\tIN\t" + GetNextHexPair() + "\n";
						break;
					case "DC":
						assemblyCode += "\t\tCC\t" + GetAddressData() + "\n";
						break;
					case "DD":
						// no opcode
						assemblyCode += "\n";
						break;
					case "DE":
						assemblyCode += "\t\tSBI\t" + GetNextHexPair() + "\n";
						break;
					case "DF":
						assemblyCode += "\t\tRST\t3\n";
						break;
					case "E0":
						assemblyCode += "\t\tRPO\n";
						break;
					case "E1":
						assemblyCode += "\t\tPOP\tH\n";
						break;
					case "E2":
						assemblyCode += "\t\tJPO\t" + GetAddressData() + "\n";
						break;
					case "E3":
						assemblyCode += "\t\tXTHL\n";
						break;
					case "E4":
						assemblyCode += "\t\tCPO\t" + GetAddressData() + "\n";
						break;
					case "E5":
						assemblyCode += "\t\tPUSH\tH\n";
						break;
					case "E6":
						assemblyCode += "\t\tANI\t" + GetNextHexPair() + "\n";
						break;
					case "E7":
						assemblyCode += "\t\tRST\t4\n";
						break;
					case "E8":
						assemblyCode += "\t\tRPE\n";
						break;
					case "E9":
						assemblyCode += "\t\tPCHL\n";
						break;
					case "EA":
						assemblyCode += "\t\tJPE\t" + GetAddressData() + "\n";
						break;
					case "EB":
						assemblyCode += "\t\tXCHG\n";
						break;
					case "EC":
						assemblyCode += "\t\tCPE\t" + GetAddressData() + "\n";
						break;
					case "ED":
						// no opcode
						assemblyCode += "\n";
						break;
					case "EE":
						assemblyCode += "\t\tXRI\t" + GetNextHexPair() + "\n";
						break;
					case "EF":
						assemblyCode += "\t\tRST\t5\n";
						break;
					case "F0":
						assemblyCode += "\t\tRP\n";
						break;
					case "F1":
						assemblyCode += "\t\tPOP\tPSW\n";
						break;
					case "F2":
						assemblyCode += "\t\tJP\t" + GetAddressData() + "\n";
						break;
					case "F3":
						assemblyCode += "\t\tDI\n";
						break;
					case "F4":
						assemblyCode += "\t\tCP\t" + GetAddressData() + "\n";
						break;
					case "F5":
						assemblyCode += "\t\tPUSH PSW\n";
						break;
					case "F6":
						assemblyCode += "\t\tORI\t" + GetNextHexPair() + "\n";
						break;
					case "F7":
						assemblyCode += "\t\tRST\t6\n";
						break;
					case "F8":
						assemblyCode += "\t\tRM\n";
						break;
					case "F9":
						assemblyCode += "\t\tSPHL\n";
						break;
					case "FA":
						assemblyCode += "\t\tJM\t" + GetAddressData() + "\n";
						break;
					case "FB":
						assemblyCode += "\t\tEI\n";
						break;
					case "FC":
						assemblyCode += "\t\tCM\t" + GetAddressData() + "\n";
						break;
					case "FD":
						// no opcode
						assemblyCode += "\n";
						break;
					case "FE":
						assemblyCode += "\t\tCPI\t" + GetNextHexPair() + "\n";
						break;
					case "FF":
						assemblyCode += "\t\tRST\t7\n";
						break;
				}

				nextOpCode = GetNextHexPair();
			}

			return assemblyCode.Replace("\n", "\r\n");
		}
	}
}
