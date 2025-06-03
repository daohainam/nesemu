using mini_6502;
using Newtonsoft.Json.Linq;

namespace UnitTests;
public partial class Instruction_Tests
{
    public static readonly IEnumerable<object[]> ArithmeticTestData =
    [
        [0x00, 0x00, true, true, false], // Zero Page
        [0x10, 0x10, true, true, false], // Zero Page X
        [0x20, 0x10, false, true, false], // Absolute
        [0xFF, 0x01, false, true, true], // Absolute X
        [0x80, 0x7F, false, true, false], // Absolute Y
        [0x7F, 0x80, false, false, true] // Indirect X
    ];

    [Fact]
    public void ADC_Immediate_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x69); // Opcode for ADC Immediate
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        cpu.Clock();
        Assert.Equal(0x30, cpu.A);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void ADC_ZeroPage_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x65); // Opcode for ADC Zero Page
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write(0x0020, 0x20);
        cpu.Clock();
        Assert.Equal(0x30, cpu.A);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);

    }

    [Fact]
    public void ADC_ZeroPageX_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.X = 0x05;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x75); // Opcode for ADC Zero Page X
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write(0x0025, 0x20);
        cpu.Clock();
        Assert.Equal(0x30, cpu.A);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void ADC_Absolute_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x6D); // Opcode for ADC Absolute
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write((ushort)(cartridgeAddress + 2), 0x00);
        memory.Write(0x0020, 0x20);
        cpu.Clock();
        Assert.Equal(0x30, cpu.A);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void ADC_AbsoluteX_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.X = 0x05;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x7D); // Opcode for ADC Absolute X
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write((ushort)(cartridgeAddress + 2), 0x00);
        memory.Write(0x0025, 0x20);
        cpu.Clock();
        Assert.Equal(0x30, cpu.A);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void ADC_AbsoluteY_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.Y = 0x05;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x79); // Opcode for ADC Absolute Y
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write((ushort)(cartridgeAddress + 2), 0x00);
        memory.Write(0x0025, 0x20);
        cpu.Clock();
        Assert.Equal(0x30, cpu.A);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void ADC_IndirectX_Instruction_Test()
    {
        byte zpAddress = 0x20;
        cpu.A = 0x10;
        cpu.X = 0x05;
        ushort effectiveZpAddress = (ushort)(zpAddress + cpu.X);
        ushort indirectAddress = 0x1234;
        memory.Write(effectiveZpAddress, (byte)(indirectAddress & 0xFF));
        memory.Write((ushort)(effectiveZpAddress + 1), (byte)(indirectAddress >> 8));
        memory.Write(indirectAddress, defaultTestValue);

        cpu.PC = cartridgeAddress;
        memory.Write((ushort)(cpu.PC), 0x61);
        memory.Write((ushort)(cpu.PC + 1), zpAddress);

        cpu.Clock();
        Assert.Equal(defaultTestValue + 0x10, cpu.A);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void ADC_IndirectY_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.Y = 0x05;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x71); // Opcode for ADC Indirect Y
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write(0x0020, 0x20);
        memory.Write(0x0025, 0x20);
        cpu.Clock();
        Assert.Equal(0x30, cpu.A);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void SBC_Immediate_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x69); // Opcode for ADC Immediate
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        cpu.Clock();
        Assert.Equal(0x30, cpu.A);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void SBC_ZeroPage_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xE5); // Opcode for SBC Zero Page
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write(0x0020, 0x20);
        cpu.Clock();
        Assert.Equal(0xF0, cpu.A);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void SBC_ZeroPageX_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.X = 0x05;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xF5); // Opcode for SBC Zero Page X
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write(0x0025, 0x20);
        cpu.Clock();
        Assert.Equal(0xF0, cpu.A);
    }

    [Fact]
    public void SBC_Absolute_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xED); // Opcode for SBC Absolute
        memory.Write((ushort)(cartridgeAddress + 1), 0x34);
        memory.Write((ushort)(cartridgeAddress + 2), 0x12);
        memory.Write(0x1234, 0x20);
        cpu.Clock();
        Assert.Equal(0xF0, cpu.A);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void SBC_AbsoluteX_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.X = 0x05;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xFD); // Opcode for SBC Absolute X
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write((ushort)(cartridgeAddress + 2), 0x00);
        memory.Write(0x0025, 0x20);
        cpu.Clock();
        Assert.Equal(0xF0, cpu.A);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void SBC_AbsoluteY_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.Y = 0x05;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xF9); // Opcode for SBC Absolute Y
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write((ushort)(cartridgeAddress + 2), 0x00);
        memory.Write(0x0025, 0x20);
        cpu.Clock();
        Assert.Equal(0xF0, cpu.A);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void SBC_IndirectX_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.X = 0x05;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xE1); // Opcode for SBC Indirect X
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write(0x0025, 0x20);
        cpu.Clock();
        Assert.Equal(0xF0, cpu.A);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void SBC_IndirectY_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.Y = 0x05;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xF1); // Opcode for SBC Indirect Y
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write(0x0020, 0x20);
        memory.Write(0x0025, 0x20);
        cpu.Clock();
        Assert.Equal(0xF0, cpu.A);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void CMP_Immediate_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x69); // Opcode for ADC Immediate
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        cpu.Clock();
        Assert.Equal(0x30, cpu.A);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void CMPC_ZeroPage_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xC5); // Opcode for CMP Zero Page
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write(0x0020, 0x20);
        cpu.Clock();
        Assert.Equal(0x10, cpu.A);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void CMP_ZeroPageX_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.X = 0x05;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xD5); // Opcode for CMP Zero Page X
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write(0x0025, 0x20);
        cpu.Clock();
        Assert.Equal(0x10, cpu.A);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void CMP_Absolute_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xCD); // Opcode for CMP Absolute
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write((ushort)(cartridgeAddress + 2), 0x00);
        memory.Write(0x0020, 0x20);
        cpu.Clock();
        Assert.Equal(0x10, cpu.A);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void CMP_AbsoluteX_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.X = 0x05;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xDD); // Opcode for CMP Absolute X
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write((ushort)(cartridgeAddress + 2), 0x00);
        memory.Write(0x0025, 0x20);
        cpu.Clock();
        Assert.Equal(0x10, cpu.A);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void CMP_AbsoluteY_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.Y = 0x05;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xD9); // Opcode for CMP Absolute Y
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write((ushort)(cartridgeAddress + 2), 0x00);
        memory.Write(0x0025, 0x20);
        cpu.Clock();
        Assert.Equal(0x10, cpu.A);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void CMP_IndirectX_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.X = 0x05;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xC1); // Opcode for CMP Indirect X
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write(0x0025, 0x20);
        cpu.Clock();
        Assert.Equal(0x10, cpu.A);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void CMP_IndirectY_Instruction_Test()
    {
        cpu.A = 0x10;
        cpu.Y = 0x05;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xD1); // Opcode for CMP Indirect Y
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write(0x0020, 0x20);
        memory.Write(0x0025, 0x20);
        cpu.Clock();
        Assert.Equal(0x10, cpu.A);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Theory]
    [MemberData(nameof(ArithmeticTestData))]
    public void CPX_Immediate_Instruction_Test(byte x, byte value, bool isZero, bool isCarry, bool isNegative)
    {
        cpu.X = x;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xE0); // Opcode for CPX Immediate
        memory.Write((ushort)(cartridgeAddress + 1), value);
        cpu.Clock();
        Assert.Equal(isZero, cpu.GetFlag(Flags.FLAG_ZERO));
        Assert.Equal(isCarry, cpu.GetFlag(Flags.FLAG_CARRY));
        Assert.Equal(isNegative, cpu.GetFlag(Flags.FLAG_NEGATIVE));
        Assert.Equal(x, cpu.X);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Theory]
    [MemberData(nameof(ArithmeticTestData))]
    public void CPX_ZeroPage_Instruction_Test(byte x, byte value, bool isZero, bool isCarry, bool isNegative)
    {
        cpu.X = x;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xE4); // Opcode for CPX Zero Page
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write(0x0020, value);
        cpu.Clock();
        Assert.Equal(isZero, cpu.GetFlag(Flags.FLAG_ZERO));
        Assert.Equal(isCarry, cpu.GetFlag(Flags.FLAG_CARRY));
        Assert.Equal(isNegative, cpu.GetFlag(Flags.FLAG_NEGATIVE));
        Assert.Equal(x, cpu.X);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Theory]
    [MemberData(nameof(ArithmeticTestData))]
    public void CPX_Absolute_Instruction_Test(byte x, byte value, bool isZero, bool isCarry, bool isNegative)
    {
        cpu.X = x;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xEC); // Opcode for CPX Absolute
        memory.Write((ushort)(cartridgeAddress + 1), 0x34);
        memory.Write((ushort)(cartridgeAddress + 2), 0x12);
        memory.Write(0x1234, value);
        cpu.Clock();
        Assert.Equal(isZero, cpu.GetFlag(Flags.FLAG_ZERO));
        Assert.Equal(isCarry, cpu.GetFlag(Flags.FLAG_CARRY));
        Assert.Equal(isNegative, cpu.GetFlag(Flags.FLAG_NEGATIVE));
        Assert.Equal(x, cpu.X);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Theory]
    [MemberData(nameof(ArithmeticTestData))]
    public void CPY_Immediate_Instruction_Test(byte y, byte value, bool isZero, bool isCarry, bool isNegative)
    {
        cpu.Y = y;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xC0); // Opcode for CPY Immediate
        memory.Write((ushort)(cartridgeAddress + 1), value);
        cpu.Clock();
        Assert.Equal(isZero, cpu.GetFlag(Flags.FLAG_ZERO));
        Assert.Equal(isCarry, cpu.GetFlag(Flags.FLAG_CARRY));
        Assert.Equal(isNegative, cpu.GetFlag(Flags.FLAG_NEGATIVE));
        Assert.Equal(y, cpu.Y);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Theory]
    [MemberData(nameof(ArithmeticTestData))]
    public void CPY_ZeroPage_Instruction_Test(byte y, byte value, bool isZero, bool isCarry, bool isNegative)
    {
        cpu.Y = y;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xC4); // Opcode for CPX Zero Page
        memory.Write((ushort)(cartridgeAddress + 1), 0x20);
        memory.Write(0x0020, value);
        cpu.Clock();
        Assert.Equal(isZero, cpu.GetFlag(Flags.FLAG_ZERO));
        Assert.Equal(isCarry, cpu.GetFlag(Flags.FLAG_CARRY));
        Assert.Equal(isNegative, cpu.GetFlag(Flags.FLAG_NEGATIVE));
        Assert.Equal(y, cpu.Y);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Theory]
    [MemberData(nameof(ArithmeticTestData))]
    public void CPY_Absolute_Instruction_Test(byte y, byte value, bool isZero, bool isCarry, bool isNegative)
    {
        cpu.Y = y;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0xCC); // Opcode for CPY Absolute
        memory.Write((ushort)(cartridgeAddress + 1), 0x34);
        memory.Write((ushort)(cartridgeAddress + 2), 0x12);
        memory.Write(0x1234, value);
        cpu.Clock();
        Assert.Equal(isZero, cpu.GetFlag(Flags.FLAG_ZERO));
        Assert.Equal(isCarry, cpu.GetFlag(Flags.FLAG_CARRY));
        Assert.Equal(isNegative, cpu.GetFlag(Flags.FLAG_NEGATIVE));
        Assert.Equal(y, cpu.Y);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }
}
