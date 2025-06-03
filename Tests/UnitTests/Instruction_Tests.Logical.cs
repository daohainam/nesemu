using mini_6502;

namespace UnitTests;
public partial class Instruction_Tests
{
    [Fact]
    public void AND_Immediate_Instruction_Test()
    {
        byte value = 0x55; 
        byte immediateValue = 0x33; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x29); // AND with immediate opcode
        memory.Write((ushort)(cartridgeAddress + 1), immediateValue); 
        cpu.Clock();
        byte expectedResult = (byte)(value & immediateValue);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void AND_ZeroPage_Instruction_Test()
    {
        byte value = 0x55; 
        byte zeroPageAddress = 0x20; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x25); // AND with zero page opcode
        memory.Write((ushort)(cartridgeAddress + 1), zeroPageAddress); 
        memory.Write(zeroPageAddress, 0x33); // Value at zero page address
        cpu.Clock();
        
        byte expectedResult = (byte)(value & 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void AND_ZeroPageX_Instruction_Test()
    {
        byte value = 0x55; 
        byte zeroPageAddress = 0x20; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x35); // AND with zero page X opcode
        memory.Write((ushort)(cartridgeAddress + 1), zeroPageAddress); 
        memory.Write((ushort)(zeroPageAddress + cpu.X), 0x33); // Value at zero page address + X
        cpu.Clock();
        
        byte expectedResult = (byte)(value & 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void AND_Absolute_Instruction_Test()
    {
        byte value = 0x55; 
        ushort absoluteAddress = 0x1234; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x2D); // AND with absolute opcode
        memory.Write((ushort)(cartridgeAddress + 1), (byte)(absoluteAddress & 0xFF)); 
        memory.Write((ushort)(cartridgeAddress + 2), (byte)(absoluteAddress >> 8)); 
        memory.Write(absoluteAddress, 0x33); // Value at absolute address
        cpu.Clock();
        
        byte expectedResult = (byte)(value & 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void AND_AbsoluteX_Instruction_Test()
    {
        byte value = 0x55; 
        ushort absoluteAddress = 0x1234; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x3D); // AND with absolute X opcode
        memory.Write((ushort)(cartridgeAddress + 1), (byte)(absoluteAddress & 0xFF)); 
        memory.Write((ushort)(cartridgeAddress + 2), (byte)(absoluteAddress >> 8)); 
        memory.Write((ushort)(absoluteAddress + cpu.X), 0x33); // Value at absolute address + X
        cpu.Clock();
        
        byte expectedResult = (byte)(value & 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void AND_AbsoluteY_Instruction_Test()
    {
        byte value = 0x55; 
        ushort absoluteAddress = 0x1234; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x39); // AND with absolute Y opcode
        memory.Write((ushort)(cartridgeAddress + 1), (byte)(absoluteAddress & 0xFF)); 
        memory.Write((ushort)(cartridgeAddress + 2), (byte)(absoluteAddress >> 8)); 
        memory.Write((ushort)(absoluteAddress + cpu.Y), 0x33); // Value at absolute address + Y
        cpu.Clock();
        
        byte expectedResult = (byte)(value & 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void AND_IndirectX_Instruction_Test()
    {
        byte value = 0x55; 
        byte zeroPageAddress = 0x20; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x21); // AND with indirect X opcode
        memory.Write((ushort)(cartridgeAddress + 1), zeroPageAddress); 
        memory.Write(zeroPageAddress, 0x34); // Low byte of address
        memory.Write((ushort)(zeroPageAddress + 1), 0x12); // High byte of address
        memory.Write(0x1234, 0x33); // Value at indirect address
        cpu.Clock();
        
        byte expectedResult = (byte)(value & 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void AND_IndirectY_Instruction_Test()
    {
        byte value = 0x55; 
        byte zeroPageAddress = 0x20; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x31); // AND with indirect Y opcode
        memory.Write((ushort)(cartridgeAddress + 1), zeroPageAddress); 
        memory.Write(zeroPageAddress, 0x34); // Low byte of address
        memory.Write((ushort)(zeroPageAddress + 1), 0x12); // High byte of address
        memory.Write((ushort)(0x1234 + cpu.Y), 0x33); // Value at indirect address + Y
        cpu.Clock();
        
        byte expectedResult = (byte)(value & 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void BIT_ZeroPage_Instruction_Test()
    {
        byte value = 0x55; 
        byte zeroPageAddress = 0x20; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x24); // BIT with zero page opcode
        memory.Write((ushort)(cartridgeAddress + 1), zeroPageAddress); 
        memory.Write(zeroPageAddress, 0xC3); // Value at zero page address
        cpu.Clock();
        
        Assert.Equal((value & 0xC3) == 0 ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((0xC3 & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal((0xC3 & 0x40) != 0 ? Flags.FLAG_OVERFLOW : (byte)0, cpu.P & Flags.FLAG_OVERFLOW);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void BIT_Absolute_Instruction_Test()
    {
        byte value = 0x55; 
        ushort absoluteAddress = 0x1234; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x2C); // BIT with absolute opcode
        memory.Write((ushort)(cartridgeAddress + 1), (byte)(absoluteAddress & 0xFF)); 
        memory.Write((ushort)(cartridgeAddress + 2), (byte)(absoluteAddress >> 8)); 
        memory.Write(absoluteAddress, 0xC3); // Value at absolute address
        cpu.Clock();
        
        Assert.Equal((value & 0xC3) == 0 ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((0xC3 & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal((0xC3 & 0x40) != 0 ? Flags.FLAG_OVERFLOW : (byte)0, cpu.P & Flags.FLAG_OVERFLOW);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void EOR_Immediate_Instruction_Test()
    {
        byte value = 0b_1111_1111; 
        byte immediateValue = 0b_1010_1010; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x49); // EOR with immediate opcode
        memory.Write((ushort)(cartridgeAddress + 1), immediateValue); 
        cpu.Clock();
        byte expectedResult = (byte)(value ^ immediateValue);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void EOR_ZeroPage_Instruction_Test()
    {
        byte value = 0x55; 
        byte zeroPageAddress = 0x20; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x45); // EOR with zero page opcode
        memory.Write((ushort)(cartridgeAddress + 1), zeroPageAddress); 
        memory.Write(zeroPageAddress, 0x33); // Value at zero page address
        cpu.Clock();
        
        byte expectedResult = (byte)(value ^ 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void EOR_ZeroPageX_Instruction_Test()
    {
        byte value = 0x55; 
        byte zeroPageAddress = 0x20; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x55); // EOR with zero page X opcode
        memory.Write((ushort)(cartridgeAddress + 1), zeroPageAddress); 
        memory.Write((ushort)(zeroPageAddress + cpu.X), 0x33); // Value at zero page address + X
        cpu.Clock();
        
        byte expectedResult = (byte)(value ^ 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void EOR_Absolute_Instruction_Test()
    {
        byte value = 0x55; 
        ushort absoluteAddress = 0x1234; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x4D); // EOR with absolute opcode
        memory.Write((ushort)(cartridgeAddress + 1), (byte)(absoluteAddress & 0xFF)); 
        memory.Write((ushort)(cartridgeAddress + 2), (byte)(absoluteAddress >> 8)); 
        memory.Write(absoluteAddress, 0x33); // Value at absolute address
        cpu.Clock();
        
        byte expectedResult = (byte)(value ^ 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void EOR_AbsoluteX_Instruction_Test()
    {
        byte value = 0x55; 
        ushort absoluteAddress = 0x1234; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x5D); // EOR with absolute X opcode
        memory.Write((ushort)(cartridgeAddress + 1), (byte)(absoluteAddress & 0xFF)); 
        memory.Write((ushort)(cartridgeAddress + 2), (byte)(absoluteAddress >> 8)); 
        memory.Write((ushort)(absoluteAddress + cpu.X), 0x33); // Value at absolute address + X
        cpu.Clock();
        
        byte expectedResult = (byte)(value ^ 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void EOR_AbsoluteY_Instruction_Test()
    {
        byte value = 0x55; 
        ushort absoluteAddress = 0x1234; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x59); // EOR with absolute Y opcode
        memory.Write((ushort)(cartridgeAddress + 1), (byte)(absoluteAddress & 0xFF)); 
        memory.Write((ushort)(cartridgeAddress + 2), (byte)(absoluteAddress >> 8)); 
        memory.Write((ushort)(absoluteAddress + cpu.Y), 0x33); // Value at absolute address + Y
        cpu.Clock();
        
        byte expectedResult = (byte)(value ^ 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void EOR_IndirectX_Instruction_Test()
    {
        byte value = 0x55; 
        byte zeroPageAddress = 0x20; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x41); // EOR with indirect X opcode
        memory.Write((ushort)(cartridgeAddress + 1), zeroPageAddress); 
        memory.Write(zeroPageAddress, 0x34); // Low byte of address
        memory.Write((ushort)(zeroPageAddress + 1), 0x12); // High byte of address
        memory.Write(0x1234, 0x33); // Value at indirect address
        cpu.Clock();
        
        byte expectedResult = (byte)(value ^ 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void EOR_IndirectY_Instruction_Test()
    {
        byte value = 0x55; 
        byte zeroPageAddress = 0x20; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x51); // EOR with indirect Y opcode
        memory.Write((ushort)(cartridgeAddress + 1), zeroPageAddress); 
        memory.Write(zeroPageAddress, 0x34); // Low byte of address
        memory.Write((ushort)(zeroPageAddress + 1), 0x12); // High byte of address
        memory.Write((ushort)(0x1234 + cpu.Y), 0x33); // Value at indirect address + Y
        cpu.Clock();
        
        byte expectedResult = (byte)(value ^ 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void ORA_Immediate_Instruction_Test()
    {
        byte value = 0x55; 
        byte immediateValue = 0x33; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x09); // ORA with immediate opcode
        memory.Write((ushort)(cartridgeAddress + 1), immediateValue); 
        cpu.Clock();
        byte expectedResult = (byte)(value | immediateValue);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void ORA_ZeroPage_Instruction_Test()
    {
        byte value = 0x55; 
        byte zeroPageAddress = 0x20; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x05); // ORA with zero page opcode
        memory.Write((ushort)(cartridgeAddress + 1), zeroPageAddress); 
        memory.Write(zeroPageAddress, 0x33); // Value at zero page address
        cpu.Clock();
        
        byte expectedResult = (byte)(value | 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void ORA_ZeroPageX_Instruction_Test()
    {
        byte value = 0x55; 
        byte zeroPageAddress = 0x20; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x15); // ORA with zero page X opcode
        memory.Write((ushort)(cartridgeAddress + 1), zeroPageAddress); 
        memory.Write((ushort)(zeroPageAddress + cpu.X), 0x33); // Value at zero page address + X
        cpu.Clock();
        
        byte expectedResult = (byte)(value | 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void ORA_Absolute_Instruction_Test()
    {
        byte value = 0x55; 
        ushort absoluteAddress = 0x1234; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x0D); // ORA with absolute opcode
        memory.Write((ushort)(cartridgeAddress + 1), (byte)(absoluteAddress & 0xFF)); 
        memory.Write((ushort)(cartridgeAddress + 2), (byte)(absoluteAddress >> 8)); 
        memory.Write(absoluteAddress, 0x33); // Value at absolute address
        cpu.Clock();
        
        byte expectedResult = (byte)(value | 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void ORA_AbsoluteX_Instruction_Test()
    {
        byte value = 0x55; 
        ushort absoluteAddress = 0x1234; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x1D); // ORA with absolute X opcode
        memory.Write((ushort)(cartridgeAddress + 1), (byte)(absoluteAddress & 0xFF)); 
        memory.Write((ushort)(cartridgeAddress + 2), (byte)(absoluteAddress >> 8)); 
        memory.Write((ushort)(absoluteAddress + cpu.X), 0x33); // Value at absolute address + X
        cpu.Clock();
        
        byte expectedResult = (byte)(value | 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void ORA_AbsoluteY_Instruction_Test()
    {
        byte value = 0x55; 
        ushort absoluteAddress = 0x1234; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x19); // ORA with absolute Y opcode
        memory.Write((ushort)(cartridgeAddress + 1), (byte)(absoluteAddress & 0xFF)); 
        memory.Write((ushort)(cartridgeAddress + 2), (byte)(absoluteAddress >> 8)); 
        memory.Write((ushort)(absoluteAddress + cpu.Y), 0x33); // Value at absolute address + Y
        cpu.Clock();
        
        byte expectedResult = (byte)(value | 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 3, cpu.PC);
    }

    [Fact]
    public void ORA_IndirectX_Instruction_Test()
    {
        byte value = 0x55; 
        byte zeroPageAddress = 0x20; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x01); // ORA with indirect X opcode
        memory.Write((ushort)(cartridgeAddress + 1), zeroPageAddress); 
        memory.Write(zeroPageAddress, 0x34); // Low byte of address
        memory.Write((ushort)(zeroPageAddress + 1), 0x12); // High byte of address
        memory.Write(0x1234, 0x33); // Value at indirect address
        cpu.Clock();
        
        byte expectedResult = (byte)(value | 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }

    [Fact]
    public void ORA_IndirectY_Instruction_Test()
    {
        byte value = 0x55; 
        byte zeroPageAddress = 0x20; 
        cpu.A = value;
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x11); // ORA with indirect Y opcode
        memory.Write((ushort)(cartridgeAddress + 1), zeroPageAddress); 
        memory.Write(zeroPageAddress, 0x34); // Low byte of address
        memory.Write((ushort)(zeroPageAddress + 1), 0x12); // High byte of address
        memory.Write((ushort)(0x1234 + cpu.Y), 0x33); // Value at indirect address + Y
        cpu.Clock();
        
        byte expectedResult = (byte)(value | 0x33);
        Assert.Equal(expectedResult, cpu.A);
        Assert.Equal((expectedResult == 0) ? Flags.FLAG_ZERO : (byte)0, cpu.P & Flags.FLAG_ZERO);
        Assert.Equal((expectedResult & 0x80) != 0 ? Flags.FLAG_NEGATIVE : (byte)0, cpu.P & Flags.FLAG_NEGATIVE);
        Assert.Equal(cartridgeAddress + 2, cpu.PC);
    }
}
