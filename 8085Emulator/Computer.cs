using System;
using System.Activities.Expressions;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Processor
{
	public class Computer
	{
		private static Logger _logger;

		public int SP { get; set; }
		public int PC { get; set; }
		public Memory ComputerMemory { get; set; }
		public byte H { get; set; }
		public byte L { get; set; }
		public byte D { get; set; }
		public byte E { get; set; }
		public byte B { get; set; }
		public byte C { get; set; }
		public byte A { get; set; }
		public FlagRegister Flags { get; set; }
		public bool InterruptsEnabled;
		public List<OutputDevice> OutputPorts;
		public InterruptMask IM;
		public int CPUSpeed { get; set; }
		// The CPU speed will allow the emulator to run at the clock speed of a real 8085 CPU.

		public Computer()
		{
			LogManager.ThrowExceptions = true;
			_logger = LogManager.GetCurrentClassLogger();

			ComputerMemory = new Memory();
			OutputPorts = new List<OutputDevice>();
			for (int i = 0; i < 256; i++)
			{
				OutputPorts.Add(new OutputDevice());
			}

			IM = new InterruptMask();
			Flags = new FlagRegister();
			CPUSpeed = -1; // no speed setting.
		}

		public void Reset()
		{
			SP = 0xffff;
			PC = 0;
			ComputerMemory.Clear();
			H = 0;
			L = 0;
			D = 0;
			E = 0;
			B = 0;
			A = 0;
			Flags.Reset();
			InterruptsEnabled = true;
			IM.Mask = 0x0; //TODO: verify that this is the reset conditions
		}

		public void Run()
		{
			while (ExecuteInstruction())
			{
				if (PC >= 65535)
				{
					return;
				}
			}
		}

		public bool Step()
		{
			return ExecuteInstruction();
		}

		public bool ExecuteInstruction()
		{
			InstructionMnemonic instruction = InstructionDecoder.Decode(ComputerMemory[PC]);

			string pc = PC.ToString("X4");
			_logger.Debug($"{pc} {instruction}" );

			switch (instruction)
			{
				case InstructionMnemonic.ACI: // add immediate with carry
					AddImmediateWithCarry();
					break;
				case InstructionMnemonic.ADC: // add with carry
					AddWithCarry();
					break;
				case InstructionMnemonic.ADD: // add
					Add();
					break;
				case InstructionMnemonic.ADI: // add immediate
					AddImmediate();
					break;
				case InstructionMnemonic.ANA: // and with accumulator
					LogicalAndWithAccumulator();
					break;
				case InstructionMnemonic.ANI:
					AndImmediateWithAccumulator();
					break;
				case InstructionMnemonic.CALL: // subroutine call
					Call();
					break;
				case InstructionMnemonic.CC: // subroutine call if carry
					CallIfCarry();
					break;
				case InstructionMnemonic.CM: // subroutine call if minus
					CallIfMinus();
					break;
				case InstructionMnemonic.CMA: // complement accumulator
					ComplementAccoumulator();
					break;
				case InstructionMnemonic.CMC: // complement carry
					ComplementCarry();
					break;
				case InstructionMnemonic.CMP: // compare with accumulator
					CompareWithAccumulator();
					break;
				case InstructionMnemonic.CNC: // call if no carry
					CallIfNoCarry();
					break;
				case InstructionMnemonic.CNZ: // call if not zero
					CallIfNotZero();
					break;
				case InstructionMnemonic.CP: // call if positive
					CallIfPositive();
					break;
				case InstructionMnemonic.CPE: // call if parity even
					CallIfParityEven();
					break;
				case InstructionMnemonic.CPI: // compare immediate
					CompareImmediate();
					break;
				case InstructionMnemonic.CPO: // call if parity odd
					CallIfParityOdd();
					break;
				case InstructionMnemonic.CZ: // call if zero
					CallIfZero();
					break;
				case InstructionMnemonic.DAA: // decimal adjust accumulator
					DecimalAdjustAccumulator();
					break;
				case InstructionMnemonic.DAD: // double register add					
					DoubleRegisterAdd();
					break;
				case InstructionMnemonic.DCR: // decrement
					Decrement();
					break;
				case InstructionMnemonic.DCX: // decrement register pair
					DecrementRegisterPair();
					break;
				case InstructionMnemonic.DI:
					DisableInterrupts();
					break;
				case InstructionMnemonic.EI:
					EnableInterrupts();
					break;
				case InstructionMnemonic.HLT:
					return false;
				case InstructionMnemonic.IN:
					GetInput();
					break;
				case InstructionMnemonic.INR:
					IncrementRegister();
					break;
				case InstructionMnemonic.INX:
					IncrementRegisterPair();
					break;
				case InstructionMnemonic.JC:
					JumpIfCarry();
					break;
				case InstructionMnemonic.JM:
					JumpIfMinus();
					break;
				case InstructionMnemonic.JMP:
					Jump();
					break;
				case InstructionMnemonic.JNC:
					JumpIfNoCarry();
					break;
				case InstructionMnemonic.JNZ:
					JumpIfNotZero();
					break;
				case InstructionMnemonic.JP:
					JumpIfPositive();
					break;
				case InstructionMnemonic.JPE:
					JumpIfParityEven();
					break;
				case InstructionMnemonic.JPO:
					JumpIfParityOdd();
					break;
				case InstructionMnemonic.JZ:
					JumpIfZero();
					break;
				case InstructionMnemonic.LDA:
					LoadAccumulatorDirect();
					break;
				case InstructionMnemonic.LDAX:
					LoadAccumulatorIndirect();
					break;
				case InstructionMnemonic.LHLD:
					LoadHLDirect();
					break;
				case InstructionMnemonic.LXI:
					LoadRegisterPairImmediate();
					break;
				case InstructionMnemonic.MOV: // move
					Move();
					break;
				case InstructionMnemonic.MVI:
					MoveImmediate();
					break;
				case InstructionMnemonic.NOP: // no operation
					PC++;
					break;
				case InstructionMnemonic.ORA:
					InclusiveORWithAccumulator();
					break;
				case InstructionMnemonic.ORI:
					InclusiveOrImmediate();
					break;
				case InstructionMnemonic.OUT:
					SendOutput();
					break;
				case InstructionMnemonic.PCHL:
					MoveHLToPC();
					break;
				case InstructionMnemonic.POP:
					Pop();
					break;
				case InstructionMnemonic.PUSH:
					Push();
					break;
				case InstructionMnemonic.RAL:
					RotateLeftThroughCarry();
					break;
				case InstructionMnemonic.RAR:
					RotateRightThroughCarry();
					break;
				case InstructionMnemonic.RC:
					ReturnIfCarry();
					break;
				case InstructionMnemonic.RET:
					Return();
					break;
				case InstructionMnemonic.RIM:
					ReadInterruptMask();
					break;
				case InstructionMnemonic.RLC:
					RotateAccumulatorLeft();
					break;
				case InstructionMnemonic.RM:
					ReturnIfMinus();
					break;
				case InstructionMnemonic.RNC:
					ReturnIfNoCarry();
					break;
				case InstructionMnemonic.RNZ:
					ReturnIfNotZero();
					break;
				case InstructionMnemonic.RP:
					ReturnIfPositive();
					break;
				case InstructionMnemonic.RPE:
					ReturnIfParityEven();
					break;
				case InstructionMnemonic.RPO:
					ReturnIfParityOdd();
					break;
				case InstructionMnemonic.RRC:
					RotateAccumulatorRight();
					break;
				case InstructionMnemonic.RST:
					// get the AAA address from the RST instruction
					Restart();
					break;
				case InstructionMnemonic.RZ:
					ReturnIfZero();
					break;
				case InstructionMnemonic.SBB:
					SubtractWithBorrow();
					break;
				case InstructionMnemonic.SBI:
					SubtractImmediateWithBorrow();
					break;
				case InstructionMnemonic.SHLD:
					StoreHLDirect();
					break;
				case InstructionMnemonic.SIM:
					SetInterruptMask();
					break;
				case InstructionMnemonic.SPHL:
					MoveHLToSP();
					break;
				case InstructionMnemonic.STA:
					StoreAccumulatorDirect();
					break;
				case InstructionMnemonic.STAX:
					StoreAccumulatorIndirect();
					break;
				case InstructionMnemonic.STC:
					Flags.Carry = true;
					break;
				case InstructionMnemonic.SUB:
					Subtract();
					break;
				case InstructionMnemonic.SUI:
					SubtractImmediate();
					break;
				case InstructionMnemonic.XCHG:
					ExchangeHLandDE();
					break;
				case InstructionMnemonic.XRA:
					ExclusiveOrWithAccumulator();
					break;
				case InstructionMnemonic.XRI:
					ExclusiveOrImmediate();
					break;
				case InstructionMnemonic.XTHL:
					ExchangeXLWithTopOfStack();
					break;
				default:
					throw new NotImplementedException();
					break;
			}

			return true;
		}

		private void Restart()
		{
			//TODO: need to code this
		}

		//TODO: verify flags on all instructions
		//TODO: verify PC is incremented correctly on all instructions
		//TODO: fill in any missing unit tests

		public void DecimalAdjustAccumulator()
		{
			// if least significant 4 bits of A are greater than 9, or the aux flag is set, then daa adds 6 to A
			if (((A & 0x0f) > 0x09) || Flags.AuxCarry)
			{
				A += 0x06;
				Flags.AuxCarry = true;
			}
			else
			{
				Flags.AuxCarry = false;
			}

			// if most significant 4 bits of A are greather than 9, or the carry flag is on, then daa adds 6 to most sig bits of A
			if (((A & 0xf0) > 0x90) || Flags.Carry)
			{
				A += 0x60;
				Flags.Carry = true;
			}
			else
			{
				Flags.Carry = false;
			}

			SetFlags(A);

			PC++;
		}

		public void RotateRightThroughCarry()
		{
			bool oldCarry = Flags.Carry;

			Flags.Carry = ((A & 0x01) > 0);

			A = (byte)((A >> 1) & 0xff);

			if (oldCarry)
			{
				A = (byte) (A | 0x80);
			}

			PC++;
		}

		public void RotateLeftThroughCarry()
		{
			bool oldCarry = Flags.Carry;

			Flags.Carry = ((A & 0x80) > 0);

			A = (byte)((A << 1) & 0xff);

			if (oldCarry)
			{
				A = (byte) (A | 0x01);
			}

			PC++;
		}

		public void RotateAccumulatorLeft()
		{
			Flags.Carry = ((A & 0x80) > 0);

			bool highBit = (A & 0x80) > 0;

			A = (byte)((A << 1) & 0xff);

			if (highBit)
			{
				A = (byte)(A | 0x01);
			}

			PC++;
		}

		public void RotateAccumulatorRight()
		{
			Flags.Carry = ((A & 0x01) > 0);

			bool lowBit = (A & 0x01) > 0;

			A = (byte)((A >> 1) & 0xff);

			if (lowBit)
			{
				A = (byte)(A | 0x80);
			}

			PC++;
		}

		public void ReadInterruptMask()
		{
			A = IM.Mask;

			PC++;
		}

		public void SetInterruptMask()
		{
			IM.Mask = A;

			PC++;
		}

		private void ExchangeXLWithTopOfStack()
		{
			//TODO: need to verify that H -> memory[SP] and L -> memory[SP-1]
			byte temp = H;
			H = ComputerMemory[SP];
			ComputerMemory[SP] = temp;

			temp = L;
			L = ComputerMemory[SP - 1];
			ComputerMemory[SP - 1] = temp;

			PC++;
		}

		public void ExclusiveOrImmediate()
		{
			byte data = ComputerMemory[PC + 1];

			A = (byte)((A ^ data) & 0xff);

			SetFlags(A);
			//TODO: set the carry and ac flags

			PC+=2;
		}

		public void ExclusiveOrWithAccumulator()
		{
			var regNum = DecodeRegisterNumbers(ComputerMemory[PC]);
			byte data = ReadRegister(regNum.Source);

			A = (byte)((A ^ data) & 0xff);

			SetFlags(A);
			//TODO: set the carry and ac flags

			PC++;
		}

		public void ExchangeHLandDE()
		{
			byte temp = H;
			H = D;
			D = temp;

			temp = L;
			L = E;
			E = temp;

			PC++;
		}

		public void MoveHLToPC()
		{
			PC = H*256 + L;

			// do not increment the PC
		}

		public void StoreAccumulatorIndirect()
		{
			var regNum = DecodeRegisterNumbers(ComputerMemory[PC]);
			int address;
			if (regNum.R == 0)
			{
				// use B & C
				address = B * 256 + C;
			}
			else
			{
				// use D & E
				address = D * 256 + E;
			}

			ComputerMemory[address] = A;

			PC++;
		}

		public void StoreAccumulatorDirect()
		{
			var address = ComputerMemory[PC + 2]*256 + ComputerMemory[PC + 1];
			ComputerMemory[address] = A;

			PC += 3;
		}

		public void MoveHLToSP()
		{
			SP = H*256 + L;

			PC++;

			// no flags set
		}

		// SUB
		public void Subtract()
		{
			var regNum = DecodeRegisterNumbers(ComputerMemory[PC]);
			byte data = ReadRegister(regNum.Source);

			int tempresult = (A - data);
			A = (byte)(A - data);

			SetFlags(A);

			Flags.Carry = (tempresult >= 0x100 || tempresult < 0);

			Flags.AuxCarry = (((data ^ A) ^ data) & 0x10) > 0;

			PC++;
		}

		// SUI
		public void SubtractImmediate()
		{
			var data = ComputerMemory[PC + 1];
			int tempresult = (A - data);
			A = (byte)(A - data);

			SetFlags(A);

			Flags.Carry = (tempresult >= 0x100 || tempresult < 0);

			Flags.AuxCarry = (((data ^ A) ^ data) & 0x10) > 0;

			PC += 2;
		}

		// SBI
		public void SubtractImmediateWithBorrow()
		{
			// memory = F3H
			// A = 45H	after 52H
			// flags = cy=1, ac=1, s=0, p=0, z=0

			var data = ComputerMemory[PC + 1];
			int tempresult = (A - data);

			A = (byte)(A - data);

			if (Flags.Carry)
			{
				A--;
			}

			SetFlags(A);

			Flags.Carry = (tempresult >= 0x100 || tempresult < 0);

			Flags.AuxCarry = (((data ^ A) ^ data) & 0x10) > 0;

			PC += 2;
		}

		public void SubtractWithBorrow()
		{
			var regNum = DecodeRegisterNumbers(ComputerMemory[PC]);
			byte data = ReadRegister(regNum.Source);

			int tempresult = (A - data);
			A = (byte)(A - data);

			if (Flags.Carry)
			{
				A--;
				tempresult--;
			}

			SetFlags(A);

			Flags.Carry = (tempresult >= 0x100 || tempresult < 0);

			Flags.AuxCarry = (((data ^ A) ^ data) & 0x10) > 0;

			PC++;
		}

		public void StoreHLDirect()
		{
			ComputerMemory[PC + 1] = L;
			ComputerMemory[PC + 2] = H;

			PC += 3;
		}

		public void Pop()
		{
			var regNum = DecodeRegisterNumbers(ComputerMemory[PC]);

			SP += 2;
			SaveRPRegister(regNum.RP, ComputerMemory[SP - 1]*256 + ComputerMemory[SP - 2]); //TODO: verify PSW register pop

			PC++;
		}

		public void Push()
		{
			var regNum = DecodeRegisterNumbers(ComputerMemory[PC]);
			var data = ReadRPRegister(regNum.RP); //TODO: verify the PSW push part of this

			ComputerMemory[SP - 2] = (byte)(data & 0xff);
			ComputerMemory[SP - 1] = (byte)((data >> 8) & 0xff);
			SP -= 2;

			PC++;
		}

		public void LoadHLDirect()
		{
			L = ComputerMemory[PC + 1];
			H = ComputerMemory[PC + 2];

			PC += 3;
		}

		public void LoadAccumulatorIndirect()
		{
			var regNum = DecodeRegisterNumbers(ComputerMemory[PC]);
			int address = 0;
			if (regNum.R == 0)
			{
				// use B & C
				address = B*256 + C;
			}
			else
			{
				// use D & E
				address = D*256 + E;
			}

			A = ComputerMemory[address];

			PC++;
		}

		public void LoadAccumulatorDirect()
		{
			int address = ComputerMemory[PC + 2]*256 + ComputerMemory[PC + 1];
			A = ComputerMemory[address];

			PC += 3;
		}

		public void IncrementRegister()
		{
			var regnums = DecodeRegisterNumbers(ComputerMemory[PC]);
			byte data = ReadRegister(regnums.Destination);
			byte beforeData = data;
			int rawData = data + 1;

			data++;

			SaveRegister(regnums.Destination, data);

			SetFlags(data);

			// need to compute the carry and ac flags
			Flags.Carry = (rawData > 255);
			Flags.AuxCarry = ((beforeData & 0x0f) + (data & 0x0f) > 0x0f);

			PC++;
		}

		public void CompareImmediate()
		{
			var data = ComputerMemory[PC + 1];

			Flags.Zero = (data == A);

			Flags.Carry = (A > data);

			//TODO: need to handle the sign bit, this command needs verification
			PC += 2;
		}

		public void CompareWithAccumulator()
		{
			var regnums = DecodeRegisterNumbers(ComputerMemory[PC]);
			byte data = ReadRegister(regnums.Source);

			Flags.Zero = (data == A);

			Flags.Carry = (A > data);

			//TODO: need to handle the sign bit, this command needs verification
			PC++;
		}

		public void ComplementCarry()
		{
			Flags.Carry = !Flags.Carry;

			PC++;
		}

		#region return
		public void ReturnIfParityOdd()
		{
			if (!Flags.Parity)
			{
				Return();
			}
			else
			{
				PC++;
			}
		}

		public void ReturnIfParityEven()
		{
			if (Flags.Parity)
			{
				Return();
			}
			else
			{
				PC++;
			}
		}

		public void ReturnIfZero()
		{
			if (Flags.Zero)
			{
				Return();
			}
			else
			{
				PC++;
			}
		}

		public void ReturnIfNotZero()
		{
			if (!Flags.Zero)
			{
				Return();
			}
			else
			{
				PC++;
			}
		}

		public void ReturnIfPositive()
		{
			if (!Flags.Sign)
			{
				Return();
			}
			else
			{
				PC++;
			}
		}

		public void ReturnIfMinus()
		{
			if (Flags.Sign)
			{
				Return();
			}
			else
			{
				PC++;
			}
		}

		public void ReturnIfNoCarry()
		{
			if (!Flags.Carry)
			{
				Return();
			}
			else
			{
				PC++;
			}
		}

		public void ReturnIfCarry()
		{
			if (Flags.Carry)
			{
				Return();
			}
			else
			{
				PC ++;
			}
		}

		public void Return()
		{
			SP += 2;
			PC = ComputerMemory[SP] * 256 + ComputerMemory[SP - 1];
		}
		#endregion

		#region jump
		public void JumpIfParityOdd()
		{
			if (!Flags.Parity)
			{
				Jump();
			}
			else
			{
				PC += 3;
			}
		}

		public void JumpIfParityEven()
		{
			if (Flags.Parity)
			{
				Jump();
			}
			else
			{
				PC += 3;
			}
		}

		public void JumpIfCarry()
		{
			if (Flags.Carry)
			{
				Jump();
			}
			else
			{
				PC += 3;
			}
		}

		public void JumpIfNoCarry()
		{
			if (!Flags.Carry)
			{
				Jump();
			}
			else
			{
				PC += 3;
			}
		}

		public void JumpIfNotZero()
		{
			if (!Flags.Zero)
			{
				Jump();
			}
			else
			{
				PC += 3;
			}
		}

		public void JumpIfZero()
		{
			if (Flags.Zero)
			{
				PC = ComputerMemory[PC + 2] * 256 + ComputerMemory[PC + 1];
			}
			else
			{
				PC += 3;
			}
		}

		public void Jump()
		{
			PC = ComputerMemory[PC + 2] * 256 + ComputerMemory[PC + 1];
		}

		public void JumpIfPositive()
		{
			if (!Flags.Sign)
			{
				Jump();
			}
			else
			{
				PC += 3;
			}
		}

		public void JumpIfMinus()
		{
			if (Flags.Sign)
			{
				Jump();
			}
			else
			{
				PC += 3;
			}
		}
		#endregion

		public void ComplementAccoumulator()
		{
			A = (byte)~A;

			PC++;
		}

		#region call
		public void CallIfZero()
		{
			if (Flags.Zero)
			{
				Call();
			}
			else
			{
				PC += 3;
			}
		}

		public void CallIfNotZero()
		{
			if (!Flags.Zero)
			{
				Call();
			}
			else
			{
				PC += 3;
			}
		}

		public void CallIfParityOdd()
		{
			if (!Flags.Parity)
			{
				Call();
			}
			else
			{
				PC += 3;
			}
		}

		public void CallIfParityEven()
		{
			if (Flags.Parity)
			{
				Call();
			}
			else
			{
				PC += 3;
			}
		}

		public void CallIfPositive()
		{
			if (!Flags.Sign)
			{
				Call();
			}
			else
			{
				PC += 3;
			}
		}

		public void CallIfMinus()
		{
			if (Flags.Sign)
			{
				Call();
			}
			else
			{
				PC += 3;
			}
		}

		public void CallIfNoCarry()
		{
			if (!Flags.Carry)
			{
				Call();
			}
			else
			{
				PC += 3;
			}
		}

		public void CallIfCarry()
		{
			if (Flags.Carry)
			{
				Call();
			}
			else
			{
				PC += 3;
			}
		}

		public void Call()
		{
			int nextInstruction = PC + 3;

			//TODO: need to verify that these are in the right order
			ComputerMemory[SP - 1] = (byte) (nextInstruction & 0xff);
			ComputerMemory[SP] = (byte) ((nextInstruction >> 8) & 0xff);
			SP -= 2;

			PC = ComputerMemory[PC + 2] * 256 + ComputerMemory[PC + 1];
		}
#endregion

		public void MoveImmediate()
		{
			var regnums = DecodeRegisterNumbers(ComputerMemory[PC]);

			SaveRegister(regnums.Destination, ComputerMemory[PC + 1]);

			// no flags

			PC++;
		}

		public void IncrementRegisterPair()
		{
			var regnums = DecodeRegisterNumbers(ComputerMemory[PC]);
			int data = ReadRPRegister(regnums.RP);

			data++;

			SaveRPRegister(regnums.RP, data);

			// no flags

			PC++;
		}

		public void SendOutput()
		{
			OutputPorts[ComputerMemory[PC+1]].SendData(A);
			PC+=2;
		}

		public void InclusiveORWithAccumulator()
		{
			var regnums = DecodeRegisterNumbers(ComputerMemory[PC]);
			var data = ReadRegister(regnums.Source);

			A = (byte)(A | (byte)(data & 0xff));

			SetFlags(A);
			Flags.AuxCarry = false;
			Flags.Carry = false;

			PC++;
		}

		public void InclusiveOrImmediate()
		{
			A = (byte)(A | ComputerMemory[PC + 1]);

			SetFlags(A);
			Flags.AuxCarry = false;
			Flags.Carry = false;

			PC += 2;
		}

		public void LoadRegisterPairImmediate()
		{
			var regnums = DecodeRegisterNumbers(ComputerMemory[PC]);

			int data = ComputerMemory[PC + 2]*256 + ComputerMemory[PC + 1];

			SaveRPRegister(regnums.RP, data);

			PC += 3;
		}

		private void GetInput()
		{
			throw new NotImplementedException();

			PC++;
		}

		public void EnableInterrupts()
		{
			InterruptsEnabled = true;

			PC++;
		}

		public void DisableInterrupts()
		{
			InterruptsEnabled = false;

			PC++;
		}

		public void DecrementRegisterPair()
		{
			var regnums = DecodeRegisterNumbers(ComputerMemory[PC]);
			var data = ReadRPRegister(regnums.RP);

			data--;

			SaveRPRegister(regnums.RP, data);

			// no flags are set

			PC++;
		}

		private void SaveRPRegister(int regnumsRp, int data)
		{
			switch (regnumsRp)
			{
				case 0:
					B = (byte)((data >> 8) & 0xff);
					C = (byte)(data & 0xff);
					break;
				case 1:
					D = (byte)((data >> 8) & 0xff);
					E = (byte)(data & 0xff);
					break;
				case 2:
					H = (byte)((data >> 8) & 0xff);
					L = (byte)(data & 0xff);
					break;
				case 3:
					A = (byte)((data >> 8) & 0xff);
					Flags.Register = (byte)(data & 0xff);
					break;
			}
		}

		private int ReadRPRegister(int regnum)
		{
			switch (regnum)
			{
				case 0:
					return B * 256 + C;
				case 1:
					return D * 256 + E;
				case 2:
					return H * 256 + L;
				case 3:
					return A * 256 + Flags.Register; //TODO: verify this ordering is correct
			}
			return 0x0;
		}

		public void Decrement()
		{
			var regnums = DecodeRegisterNumbers(ComputerMemory[PC]);
			var data = ReadRegister(regnums.Destination);

			data = (byte)((data -1) & 0xff);

			SaveRegister(regnums.Destination, data);
			SetFlags(data);
			Flags.AuxCarry = false;//TODO: this needs to be set the same as subtracting a 1
			// no carry flag set here

			PC++;
		}

		public void DoubleRegisterAdd()
		{
			var regnums = DecodeRegisterNumbers(ComputerMemory[PC]);
			var data = ReadRPRegister(regnums.RP);

			var hlRegisterData = ConvertRegisterPairsToInt(H, L);

			hlRegisterData += data;

			H = (byte)((byte) ((hlRegisterData >> 8) & 0xff) + H);
			L = (byte)(hlRegisterData & 0xff);

			// only affects the carry flag
			Flags.Carry = (hlRegisterData > 0xffff);

			PC++;
		}

		private int ConvertRegisterPairsToInt(byte reg1, byte reg2)
		{
			return reg1*256 + reg2;
		}

		public void LogicalAndWithAccumulator()
		{
			var regnums = DecodeRegisterNumbers(ComputerMemory[PC]);
			var data = ReadRegister(regnums.Source);

			A = (byte)((A & data) & 0xff);
			SetFlags(A);

			Flags.Carry = false; // always cleared
			Flags.AuxCarry = true; // always a 1

			PC++;
		}

		public void AndImmediateWithAccumulator()
		{
			A = (byte)(A & ComputerMemory[PC + 1]);

			SetFlags(A);

			Flags.Carry = false; // always cleared

			//https://books.google.com/books?id=QUOtjcwP0OUC&pg=PA78&lpg=PA78&dq=8085+ana+instruction&source=bl&ots=50Sp7FP1mg&sig=sBfQTkXUFrMus4X2WWzMlGLCguU&hl=en&sa=X&ved=0ahUKEwj-jq6V-fHPAhUFOD4KHYu0BcQQ6AEIPTAF#v=onepage&q=8085%20ana%20instruction&f=false
			Flags.AuxCarry = true;

			PC += 2;
		}

		public void AddImmediate()
		{
			var data = ComputerMemory[PC + 1];
			byte beforeAccumulator = A;

			int rawData = data + A;

			A += data;
			SetFlags(A);

			// need to compute the carry and ac flags
			Flags.Carry = (rawData > 255);
			Flags.AuxCarry = ((beforeAccumulator & 0x0f) + (data & 0x0f) > 0x0f);

			PC++;
		}

		public void Add()
		{
			var regnums = DecodeRegisterNumbers(ComputerMemory[PC]);
			var data = ReadRegister(regnums.Source);
			byte beforeAccumulator = A;

			int rawData = data + A;

			A += data;
			SetFlags(A);

			// need to compute the carry and ac flags
			Flags.Carry = (rawData > 255);
			Flags.AuxCarry = ((beforeAccumulator & 0x0f) + (data & 0x0f) > 0x0f);

			PC++;
		}

		public void AddWithCarry()
		{
			var regnums = DecodeRegisterNumbers(ComputerMemory[PC]);
			var data = ReadRegister(regnums.Source);
			byte beforeAccumulator = A;

			int rawData = data + A;

			A += data;
			if (Flags.Carry)
			{
				A++;
			}
			SetFlags(A);

			// need to compute the carry and ac flags
			Flags.Carry = (rawData > 255);
			Flags.AuxCarry = ((beforeAccumulator & 0x0f) + (data & 0x0f) > 0x0f);

			PC++;
		}

		public void AddImmediateWithCarry()
		{
			var data = ComputerMemory[PC + 1];
			byte beforeAccumulator = A;

			int rawData = data + A;

			A += data;
			if (Flags.Carry)
			{
				A++;
			}

			SetFlags(A);

			// need to compute the carry and ac flags
			Flags.Carry = (rawData > 255);

			Flags.AuxCarry = ((beforeAccumulator & 0x0f) + (data & 0x0f) > 0x0f);

			PC+=2;
		}

		public void Move()
		{
			var regnums = DecodeRegisterNumbers(ComputerMemory[PC]);
			SaveRegister(regnums.Destination, ReadRegister(regnums.Source));

			PC++;
		}

		public byte ReadRegister(int source)
		{
			switch (source)
			{
				case 0:
					return B;
				case 1:
					return C;
				case 2:
					return D;
				case 3:
					return E;
				case 4:
					return H;
				case 5:
					return L;
				case 6:
					// move H,L into address reg and read memory
					int address = H*256 + L;
					return ComputerMemory[address];
				case 7:
					return A;
			}
			return A; //TODO: to be removed
		}

		public void SaveRegister(int dest, byte data)
		{
			switch (dest)
			{
				case 0:
					B = data;
					break;
				case 1:
					C = data;
					break;
				case 2:
					D = data;
					break;
				case 3:
					E = data;
					break;
				case 4:
					H = data;
					break;
				case 5:
					L = data;
					break;
				case 6:
					// move H,L into address reg and write data to memory
					int address = H * 256 + L;
					ComputerMemory[address] = data;
					break;
				case 7:
					A = data;
					break;
			}
		}

		public RegisterNumbers DecodeRegisterNumbers(byte instruction)
		{
			var regNums = new RegisterNumbers
			{
				Source = instruction & 0x07,
				Destination = (instruction >> 3) & 0x07,
				RP = (instruction >> 4) & 0x03,
				R = (instruction >> 4) & 0x01
			};

			return regNums;
		}

		public void SetFlags(byte data)
		{
			// zero flag
			Flags.Zero = data == 0;

			// parity flag (even number of 1's)
			int totalOnes = 0;
			byte tempData = data;
			for (int i = 0; i < 8; i++)
			{
				if ((tempData & 0x01) == 0x01)
				{
					totalOnes++;
				}
				tempData = (byte)((tempData >> 1) & 0xff);
			}

			Flags.Parity = totalOnes%2 == 0;

			// set sign
			Flags.Sign = (data & 0x80) == 0x80;
		}

		public void SetAuxCarryFlag(byte beforeResult,byte afterResult)
		{
			//TODO: need to examine this a lot closer
			if ((beforeResult & 0x08) > 0)
			{
				if ((afterResult & 0x08) == 0)
				{
					Flags.AuxCarry = true;
					return;
				}
			}
			Flags.AuxCarry = false;
		}
	}
}
