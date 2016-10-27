using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			_computer.ComputerMemory.LoadProgram("test_video.hex");
		}


	}
}
