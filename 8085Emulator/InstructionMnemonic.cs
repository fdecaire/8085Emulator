﻿namespace Processor
{
	public enum InstructionMnemonic
	{
		NOP = 0x00,
		HLT = 0x76,
		MOV = 0x7f,
		CALL = 0xcd,
		ANA = 0xa0,
		CC = 0xdc,
		CNC = 0xd4,
		CZ = 0xcc,
		CNZ = 0xc4,
		CP = 0xf4,
		CM = 0xfc,
		CPE = 0xec,
		CPO = 0xe4,
		RET = 0xc9,
		RC = 0xd8,
		RNC = 0xd0,
		RZ = 0xc8,
		RNZ = 0xc0,
		RP = 0xf0,
		RM = 0xf8,
		RPE = 0xe8,
		RPO = 0xe0,
		RST = 0xff,
		IN = 0xdb,
		OUT = 0xd3,
		LXI = 0x01,
		PUSH = 0xc5,
		POP = 0xc1,
		STA = 0x32,
		LDA = 0x3a,
		XCHG = 0xeb,
		XTHL = 0xe3,
		SPHL = 0xf9,
		PCHL = 0xe9,
		DAD = 0x09,
		STAX = 0x02,
		LDAX = 0x0a,
		INX = 0x03,
		MVI = 0x06,
		INR = 0x04,
		DCR = 0x05,
		ADD = 0x80,
		ADC = 0x88,
		SUB = 0x90,
		SBB = 0x98,
		XRA = 0xa8,
		ORA = 0xb0,
		CMP = 0xb8,
		ADI = 0xc6,
		ACI = 0xce,
		SUI = 0xd6,
		SBI = 0xde,
		ANI = 0xe6,
		XRI = 0xee,
		ORI = 0xf6,
		CPI = 0xfe,
		RLC = 0x07,
		RRC = 0x0f,
		RAL = 0x17,
		RAR = 0x1f,
		JMP = 0xc3,
		JC = 0xda,
		JNC = 0xd2,
		JZ = 0xca,
		JNZ = 0xc2,
		JP = 0xf2,
		JM = 0xfa,
		JPE = 0xea,
		JPO = 0xe2,
		DCX = 0x0b,
		CMA = 0x2f,
		STC = 0x37,
		CMC = 0x3f,
		DAA = 0x27,
		SHLD = 0x22,
		LHLD = 0x2a,
		RIM = 0x20,
		SIM = 0x30,
		EI = 0xfb,
		DI = 0xf3
	}
}