using mini_6502;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests;
public partial class Instruction_Tests
{
	[Theory]
	[InlineData(0x00, false, true)] // Zero value
	[InlineData(0xFF, true, false)]
	[InlineData(0x7F, false, false)]
	public void TAX_Instruction_Test(byte value, bool isNegative, bool isZero)
	{
		cpu.A = value;
		cpu.PC = cartridgeAddress;
		memory.Write(cartridgeAddress, 0xAA); 

		cpu.Clock(); 
					 
		Assert.Equal(value, cpu.X);
		Assert.Equal(isNegative, (cpu.P & Flags.FLAG_NEGATIVE) == Flags.FLAG_NEGATIVE);
		Assert.Equal(isZero, (cpu.P & Flags.FLAG_ZERO) == Flags.FLAG_ZERO);
		Assert.Equal(cartridgeAddress + 1, cpu.PC);
	}

	[Theory]
	[InlineData(0x00, false, true)] 
	[InlineData(0xFF, true, false)]
	[InlineData(0x7F, false, false)]
	public void TAY_Instruction_Test(byte value, bool isNegative, bool isZero)
	{
		cpu.A = value;
		cpu.PC = cartridgeAddress;
		memory.Write(cartridgeAddress, 0xA8); 

		cpu.Clock();

		Assert.Equal(value, cpu.Y);
		Assert.Equal(isNegative, (cpu.P & Flags.FLAG_NEGATIVE) == Flags.FLAG_NEGATIVE);
		Assert.Equal(isZero, (cpu.P & Flags.FLAG_ZERO) == Flags.FLAG_ZERO);
		Assert.Equal(cartridgeAddress + 1, cpu.PC);
	}

	[Theory]
	[InlineData(0x00, false, true)] // Zero value
	[InlineData(0xFF, true, false)]
	[InlineData(0x7F, false, false)]
	public void TXA_Instruction_Test(byte value, bool isNegative, bool isZero)
	{
		cpu.X = value;
		cpu.PC = cartridgeAddress;
		memory.Write(cartridgeAddress, 0x8A); 

		cpu.Clock();

		Assert.Equal(value, cpu.A);
		Assert.Equal(isNegative, (cpu.P & Flags.FLAG_NEGATIVE) == Flags.FLAG_NEGATIVE);
		Assert.Equal(isZero, (cpu.P & Flags.FLAG_ZERO) == Flags.FLAG_ZERO);
		Assert.Equal(cartridgeAddress + 1, cpu.PC);
	}

	[Theory]
	[InlineData(0x00, false, true)] 
	[InlineData(0xFF, true, false)]
	[InlineData(0x7F, false, false)]
	public void TYA_Instruction_Test(byte value, bool isNegative, bool isZero)
	{
		cpu.Y = value;
		cpu.PC = cartridgeAddress;
		memory.Write(cartridgeAddress, 0x98);

		cpu.Clock();

		Assert.Equal(value, cpu.A);
		Assert.Equal(isNegative, (cpu.P & Flags.FLAG_NEGATIVE) == Flags.FLAG_NEGATIVE);
		Assert.Equal(isZero, (cpu.P & Flags.FLAG_ZERO) == Flags.FLAG_ZERO);
		Assert.Equal(cartridgeAddress + 1, cpu.PC);
	}

	[Theory]
	[InlineData(0x00, false, true)] // Zero value
	[InlineData(0xFF, true, false)]
	[InlineData(0x7F, false, false)]
	public void TSX_Instruction_Test(byte value, bool isNegative, bool isZero)
	{
		cpu.SP = value;
		cpu.PC = cartridgeAddress;
		memory.Write(cartridgeAddress, 0xBA);

		cpu.Clock();

		Assert.Equal(value, cpu.X);
		Assert.Equal(isNegative, (cpu.P & Flags.FLAG_NEGATIVE) == Flags.FLAG_NEGATIVE);
		Assert.Equal(isZero, (cpu.P & Flags.FLAG_ZERO) == Flags.FLAG_ZERO);
		Assert.Equal(cartridgeAddress + 1, cpu.PC);
	}

	[Fact]
	public void TXS_Instruction_Test()
	{
		cpu.X = 0x7F;
		cpu.PC = cartridgeAddress;
		memory.Write(cartridgeAddress, 0x9A);
		cpu.Clock();
		Assert.Equal(0x7F, cpu.SP);
		Assert.Equal(cartridgeAddress + 1, cpu.PC);
	}

}
