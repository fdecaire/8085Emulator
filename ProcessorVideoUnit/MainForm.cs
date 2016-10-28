using System;
using System.Drawing;
using System.Windows.Forms;
using Processor;

namespace ProcessorVideoUnit
{
	public partial class MainForm : Form
	{
		private Computer _computer;

		public MainForm()
		{
			InitializeComponent();
			
			_computer = new Computer();
			_computer.Reset();

			var assembler = new Assembler.Assembler();
			assembler.ReadAssemFile("test_video.asm");
			assembler.AssembleCode();

			assembler.SaveHexFile("test_video.hex");
			_computer.ComputerMemory.LoadMachineCodeFromFile("test_video.hex");
		}

		private void tmrRefresh_Tick(object sender, EventArgs e)
		{
			tmrRefresh.Enabled = false;

			//TODO: test with 5 second refresh for now
			var video = _computer.ComputerMemory.VideoRam();
			Brush aBrush = (Brush)Brushes.Black;
			Graphics g = this.CreateGraphics();

			//224×256 resolution.
			byte bitPosition = 1;
			int byteNumber = 0;
			for (int y = 0; y < 256; y++)
			{
				for (int x = 0; x < 224; x++)
				{
					if ((video[byteNumber] & bitPosition) > 0)
					{
						g.FillRectangle(aBrush, x, y, 1, 1);
					}

					if (bitPosition == 0x80)
					{
						bitPosition = 0x01;
						byteNumber++;
					}
					else
					{
						bitPosition = (byte) (bitPosition << 1);
					}
				}
			}

			tmrRefresh.Enabled = true;
		}
	}
}
