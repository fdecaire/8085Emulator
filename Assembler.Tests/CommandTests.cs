using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assembler.Tests
{
	public class CommandTests
	{
		[Fact]
		public void aci_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " ACI 0FEH";
			assembler.AssembleCode();

			Assert.Equal("CEFE", assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "88")]
		[InlineData("C", "89")]
		[InlineData("D", "8A")]
		[InlineData("E", "8B")]
		[InlineData("H", "8C")]
		[InlineData("L", "8D")]
		[InlineData("M", "8E")]
		[InlineData("A", "8F")]
		public void adc_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " ADC " + register;
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "80")]
		[InlineData("C", "81")]
		[InlineData("D", "82")]
		[InlineData("E", "83")]
		[InlineData("H", "84")]
		[InlineData("L", "85")]
		[InlineData("M", "86")]
		[InlineData("A", "87")]
		public void add_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " ADD " + register;
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Fact]
		public void adi_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " ADI 0F3H";
			assembler.AssembleCode();

			Assert.Equal("C6F3", assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "A0")]
		[InlineData("C", "A1")]
		[InlineData("D", "A2")]
		[InlineData("E", "A3")]
		[InlineData("H", "A4")]
		[InlineData("L", "A5")]
		[InlineData("M", "A6")]
		[InlineData("A", "A7")]
		public void ana_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " ANA " + register;
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Fact]
		public void ani_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " ANI 0F3H";
			assembler.AssembleCode();

			Assert.Equal("E6F3", assembler.HexResult);
		}

		[Fact]
		public void call_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " CALL 057F3H";
			assembler.AssembleCode();

			Assert.Equal("CDF357", assembler.HexResult);
		}

		[Fact]
		public void cc_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " CC 057F3H";
			assembler.AssembleCode();

			Assert.Equal("DCF357", assembler.HexResult);
		}

		[Fact]
		public void cm_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " CM 057F3H";
			assembler.AssembleCode();

			Assert.Equal("FCF357", assembler.HexResult);
		}

		[Fact]
		public void cma_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " CMA";
			assembler.AssembleCode();

			Assert.Equal("2F", assembler.HexResult);
		}

		[Fact]
		public void cmc_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " CMC";
			assembler.AssembleCode();

			Assert.Equal("3F", assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "B8")]
		[InlineData("C", "B9")]
		[InlineData("D", "BA")]
		[InlineData("E", "BB")]
		[InlineData("H", "BC")]
		[InlineData("L", "BD")]
		[InlineData("M", "BE")]
		[InlineData("A", "BF")]
		public void cmp_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " CMP " + register;
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Fact]
		public void cnc_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " CNC 057F3H";
			assembler.AssembleCode();

			Assert.Equal("D4F357", assembler.HexResult);
		}

		[Fact]
		public void cnz_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " CNZ 057F3H";
			assembler.AssembleCode();

			Assert.Equal("C4F357", assembler.HexResult);
		}

		[Fact]
		public void cp_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " CP 057F3H";
			assembler.AssembleCode();

			Assert.Equal("F4F357", assembler.HexResult);
		}

		[Fact]
		public void cpe_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " CPE 057F3H";
			assembler.AssembleCode();

			Assert.Equal("ECF357", assembler.HexResult);
		}

		[Fact]
		public void cpi_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " CPI 057H";
			assembler.AssembleCode();

			Assert.Equal("FE57", assembler.HexResult);
		}

		[Fact]
		public void cpo_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " CPO 057F3H";
			assembler.AssembleCode();

			Assert.Equal("E4F357", assembler.HexResult);
		}

		[Fact]
		public void cz_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " CZ 057F3H";
			assembler.AssembleCode();

			Assert.Equal("CCF357", assembler.HexResult);
		}

		[Fact]
		public void daa_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " DAA";
			assembler.AssembleCode();

			Assert.Equal("27", assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "09")]
		[InlineData("D", "19")]
		[InlineData("H", "29")]
		[InlineData("SP", "39")]
		public void dad_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " DAD " + register;
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "05")]
		[InlineData("C", "0D")]
		[InlineData("D", "15")]
		[InlineData("E", "1D")]
		[InlineData("H", "25")]
		[InlineData("L", "2D")]
		[InlineData("M", "35")]
		[InlineData("A", "3D")]
		public void dcr_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " DCR " + register;
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "0B")]
		[InlineData("D", "1B")]
		[InlineData("H", "2B")]
		[InlineData("SP", "3B")]
		public void dcx_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " DCX " + register;
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Fact]
		public void di_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " DI";
			assembler.AssembleCode();

			Assert.Equal("F3", assembler.HexResult);
		}

		[Fact]
		public void ei_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " EI";
			assembler.AssembleCode();

			Assert.Equal("FB", assembler.HexResult);
		}

		[Fact]
		public void hlt_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " HLT";
			assembler.AssembleCode();

			Assert.Equal("76", assembler.HexResult);
		}

		[Fact]
		public void in_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " IN 057H";
			assembler.AssembleCode();

			Assert.Equal("DB57", assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "04")]
		[InlineData("C", "0C")]
		[InlineData("D", "14")]
		[InlineData("E", "1C")]
		[InlineData("H", "24")]
		[InlineData("L", "2C")]
		[InlineData("M", "34")]
		[InlineData("A", "3C")]
		public void inr_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " INR " + register;
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "03")]
		[InlineData("D", "13")]
		[InlineData("H", "23")]
		[InlineData("SP", "33")]
		public void inx_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " INX " + register;
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Fact]
		public void jc_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " JC 057F3H";
			assembler.AssembleCode();

			Assert.Equal("DAF357", assembler.HexResult);
		}

		[Fact]
		public void jm_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " JM 057F3H";
			assembler.AssembleCode();

			Assert.Equal("FAF357", assembler.HexResult);
		}

		[Fact]
		public void jmp_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " JMP 057F3H";
			assembler.AssembleCode();

			Assert.Equal("C3F357", assembler.HexResult);
		}

		[Fact]
		public void jnc_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " JNC 057F3H";
			assembler.AssembleCode();

			Assert.Equal("E2F357", assembler.HexResult);
		}

		[Fact]
		public void jnz_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " JNZ 057F3H";
			assembler.AssembleCode();

			Assert.Equal("C2F357", assembler.HexResult);
		}

		[Fact]
		public void jp_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " JP 057F3H";
			assembler.AssembleCode();

			Assert.Equal("F2F357", assembler.HexResult);
		}

		[Fact]
		public void jpe_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " JPE 057F3H";
			assembler.AssembleCode();

			Assert.Equal("EAF357", assembler.HexResult);
		}

		[Fact]
		public void jpo_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " JPO 057F3H";
			assembler.AssembleCode();

			Assert.Equal("E2F357", assembler.HexResult);
		}

		[Fact]
		public void jz_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " JZ 057F3H";
			assembler.AssembleCode();

			Assert.Equal("CAF357", assembler.HexResult);
		}

		[Fact]
		public void lda_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " LDA 057F3H";
			assembler.AssembleCode();

			Assert.Equal("3AF357", assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "0A")]
		[InlineData("D", "1A")]
		public void ldax_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " LDAX " + register;
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Fact]
		public void lhld_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " LHLD 057F3H";
			assembler.AssembleCode();

			Assert.Equal("2AF357", assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "01D501")]
		[InlineData("D", "11D501")]
		[InlineData("H", "21D501")]
		[InlineData("SP", "31D501")]
		public void lxi_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " LXI " + register + ",001D5H";
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Theory]
		[InlineData("B,A", "47")]
		[InlineData("C,A", "4F")]
		[InlineData("D,A", "57")]
		[InlineData("E,A", "5F")]
		[InlineData("H,A", "67")]
		[InlineData("L,A", "6F")]
		[InlineData("M,A", "77")]
		[InlineData("A,A", "7F")]
		[InlineData("B,B", "40")]
		[InlineData("B,C", "41")]
		[InlineData("B,D", "42")]
		[InlineData("B,E", "43")]
		[InlineData("B,H", "44")]
		[InlineData("B,L", "45")]
		[InlineData("B,M", "46")]
		public void mov_command(string registerPair, string result)
		{
			// note: this is not an exhaustive test, just a random sample
			var assembler = new Assembler();
			assembler.AssemblyCode = " MOV " + registerPair;
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "06")]
		[InlineData("C", "0E")]
		[InlineData("D", "16")]
		[InlineData("E", "1E")]
		[InlineData("H", "26")]
		[InlineData("L", "2E")]
		[InlineData("M", "36")]
		[InlineData("A", "3E")]
		public void mvi_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " MVI " + register + ",04EH";
			assembler.AssembleCode();

			Assert.Equal(result + "4E", assembler.HexResult);
		}

		[Fact]
		public void nop_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " NOP";
			assembler.AssembleCode();

			Assert.Equal("00", assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "B0")]
		[InlineData("C", "B1")]
		[InlineData("D", "B2")]
		[InlineData("E", "B3")]
		[InlineData("H", "B4")]
		[InlineData("L", "B5")]
		[InlineData("M", "B6")]
		[InlineData("A", "B7")]
		public void ora_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " ORA " + register;
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Fact]
		public void ori_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " ORI 08FH";
			assembler.AssembleCode();

			Assert.Equal("F68F", assembler.HexResult);
		}

		[Fact]
		public void out_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " OUT 08FH";
			assembler.AssembleCode();

			Assert.Equal("D38F", assembler.HexResult);
		}

		[Fact]
		public void pchl_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " PCHL";
			assembler.AssembleCode();

			Assert.Equal("E9", assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "C1")]
		[InlineData("D", "D1")]
		[InlineData("H", "E1")]
		[InlineData("PSW", "F1")]
		public void pop_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " POP " + register;
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "C5")]
		[InlineData("D", "D5")]
		[InlineData("H", "E5")]
		[InlineData("PSW", "F5")]
		public void push_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " PUSH " + register;
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Fact]
		public void ral_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " RAL";
			assembler.AssembleCode();

			Assert.Equal("17", assembler.HexResult);
		}

		[Fact]
		public void rar_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " RAR";
			assembler.AssembleCode();

			Assert.Equal("1F", assembler.HexResult);
		}

		[Fact]
		public void rc_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " RC";
			assembler.AssembleCode();

			Assert.Equal("D8", assembler.HexResult);
		}

		[Fact]
		public void ret_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " RET";
			assembler.AssembleCode();

			Assert.Equal("C9", assembler.HexResult);
		}

		[Fact]
		public void rlc_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " RLC";
			assembler.AssembleCode();

			Assert.Equal("03", assembler.HexResult);
		}

		[Fact]
		public void rm_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " RM";
			assembler.AssembleCode();

			Assert.Equal("F8", assembler.HexResult);
		}

		[Fact]
		public void rnc_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " RNC";
			assembler.AssembleCode();

			Assert.Equal("D0", assembler.HexResult);
		}

		[Fact]
		public void rnz_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " RNZ";
			assembler.AssembleCode();

			Assert.Equal("C0", assembler.HexResult);
		}

		[Fact]
		public void rp_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " RP";
			assembler.AssembleCode();

			Assert.Equal("F0", assembler.HexResult);
		}

		[Fact]
		public void rpe_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " RPE";
			assembler.AssembleCode();

			Assert.Equal("E8", assembler.HexResult);
		}

		[Fact]
		public void rpo_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " RPO";
			assembler.AssembleCode();

			Assert.Equal("E0", assembler.HexResult);
		}

		[Fact]
		public void rrc_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " RRC";
			assembler.AssembleCode();

			Assert.Equal("0F", assembler.HexResult);
		}

		[Fact]
		public void rst_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " RST";
			assembler.AssembleCode();

			//Assert.Equal("", assembler.HexResult);
		}

		[Fact]
		public void rz_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " RZ";
			assembler.AssembleCode();

			Assert.Equal("C8", assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "98")]
		[InlineData("C", "99")]
		[InlineData("D", "9A")]
		[InlineData("E", "9B")]
		[InlineData("H", "9C")]
		[InlineData("L", "9D")]
		[InlineData("M", "9E")]
		[InlineData("A", "9F")]
		public void sbb_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " SBB " + register;
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Fact]
		public void sbi_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " SBI 08FH";
			assembler.AssembleCode();

			Assert.Equal("DE8F", assembler.HexResult);
		}

		[Fact]
		public void shld_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " SHLD 08F4CH";
			assembler.AssembleCode();

			Assert.Equal("224C8F", assembler.HexResult);
		}

		[Fact]
		public void sim_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " SIM";
			assembler.AssembleCode();

			Assert.Equal("30", assembler.HexResult);
		}

		[Fact]
		public void sphl_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " SPHL";
			assembler.AssembleCode();

			Assert.Equal("F9", assembler.HexResult);
		}

		[Fact]
		public void sta_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " STA 08F4CH";
			assembler.AssembleCode();

			Assert.Equal("324C8F", assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "02")]
		[InlineData("D", "12")]
		public void stax_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " STAX " + register;
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Fact]
		public void stc_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " STC";
			assembler.AssembleCode();

			Assert.Equal("37", assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "90")]
		[InlineData("C", "91")]
		[InlineData("D", "92")]
		[InlineData("E", "93")]
		[InlineData("H", "94")]
		[InlineData("L", "95")]
		[InlineData("M", "96")]
		[InlineData("A", "97")]
		public void sub_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " SUB " + register;
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Fact]
		public void sui_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " SUI 08FH";
			assembler.AssembleCode();

			Assert.Equal("D68F", assembler.HexResult);
		}

		[Fact]
		public void xchg_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " XCHG";
			assembler.AssembleCode();

			Assert.Equal("EB", assembler.HexResult);
		}

		[Theory]
		[InlineData("B", "A8")]
		[InlineData("C", "A9")]
		[InlineData("D", "AA")]
		[InlineData("E", "AB")]
		[InlineData("H", "AC")]
		[InlineData("L", "AD")]
		[InlineData("M", "AE")]
		[InlineData("A", "AF")]
		public void xra_command(string register, string result)
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " XRA " + register;
			assembler.AssembleCode();

			Assert.Equal(result, assembler.HexResult);
		}

		[Fact]
		public void xri_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " XRI 08FH";
			assembler.AssembleCode();

			Assert.Equal("EE8F", assembler.HexResult);
		}

		[Fact]
		public void xthl_command()
		{
			var assembler = new Assembler();
			assembler.AssemblyCode = " XTHL";
			assembler.AssembleCode();

			Assert.Equal("E3", assembler.HexResult);
		}

	}
}
