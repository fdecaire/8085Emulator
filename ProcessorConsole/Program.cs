using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Processor;
using Assembler;

namespace ProcessorConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			TestVideoSave();
		}

		static void TestVideoSave()
		{
			var assembler = new Assembler.Assembler();
			assembler.ReadAssemFile("test_video.asm");
			assembler.AssembleCode();

			assembler.SaveHexFile("test_video.hex");
		}

		static void RunHelloWorldProgram()
		{
			// run hello world using i/o port 1 as a teletype output

			var computer = new Computer();
			computer.Reset();

			computer.ComputerMemory.LoadMachineCodeFromFile("hello_world.hex");

			while (computer.Step())
			{
				string nextChar = computer.OutputPorts[1].GetNextCharacter();
				if (nextChar != "")
				{
					Console.Write(nextChar);
				}
			}

			Console.ReadKey();
		}

		static void RunDiagnosticProgram()
		{
			var assembler = new Assembler.Assembler();
			assembler.ReadAssemFile("cpudiag.asm");
			assembler.AssembleCode();

			assembler.SaveHumanReadableHexFile("cpudiag.hex");
			assembler.SaveHexFile("cpudiag_raw.hex");
			/*
			
			//computer.ComputerMemory.LoadMachineCodeFromFile("loop.hex");
			// computer.ComputerMemory.LoadMachineCodeFromFile("basic.hex");


			*/


			var computer = new Computer();
			computer.Reset();
			computer.ComputerMemory.LoadMachineCodeDirect(assembler.HexResult);
			//computer.ComputerMemory.Dump();

			while (computer.Step())
			{
				string nextChar = computer.OutputPorts[1].GetNextCharacter();
				if (nextChar != "")
				{
					Console.Write(nextChar);
				}
			}

			Console.ReadKey();
		}

		static void SpaceInvaders()
		{
			//http://www.computerarcheology.com/Arcade/SpaceInvaders/RAMUse.html
			//The raster resolution is 256x224 at 60Hz.The monitor is rotated in the cabinet 90 degrees counter-clockwise.
			//The screens pixels are on/ off(1 bit each). 256 * 224 / 8 = 7168(7K) bytes
			//0000 - 1FFF 8K ROM
			//2000 - 23FF 1K RAM
			//2400 - 3FFF 7K Video RAM
			//4000 - RAM mirror

			var assembler = new Assembler.Assembler();
			assembler.ReadAssemFile("space_invaders.asm");
			assembler.AssembleCode();
		}
	}
}
