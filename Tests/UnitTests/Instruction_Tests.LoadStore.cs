namespace UnitTests;

using mini_6502.Components;

public partial class Instruction_Tests
{
    [Fact]
    public void LDA_Immediate_Instruction_Test()
    {
        memory.Write((ushort)(cpu.PC), 0xA9); // Write immediate value at PC
        memory.Write((ushort)(cpu.PC + 1), defaultTestValue); // Write the immediate value

        cpu.Clock(); // Execute the instruction

        Assert.Equal(defaultTestValue, cpu.A);
        Assert.Equal(0x8002, cpu.PC); // PC should increment by 2 (1 for opcode + 1 for immediate value)
    }

    [Fact]
    public void LDA_ZeroPage_Instruction_Test()
    {
        byte address = 0x20;

        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xA5); // LDA Zero Page opcode
        memory.Write((ushort)(cpu.PC + 1), address); // Zero page address
        memory.Write(address, defaultTestValue); // Write value to zero page
        
        cpu.Clock();

        Assert.Equal(defaultTestValue, cpu.A);
        Assert.Equal(0x8002, cpu.PC); // PC should increment by 2 (1 for opcode + 1 for address)
    }

    [Fact]
    public void LDA_Absolute_Instruction_Test()
    {
        ushort address = 0x1234;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xAD); // LDA Absolute opcode
        memory.Write((ushort)(cpu.PC + 1), (byte)(address & 0xFF)); // Low byte of address
        memory.Write((ushort)(cpu.PC + 2), (byte)(address >> 8)); // High byte of address
        memory.Write(address, defaultTestValue); // Write value to absolute address
        
        cpu.Clock();

        Assert.Equal(defaultTestValue, cpu.A);
        Assert.Equal(0x8003, cpu.PC); // PC should increment by 3 (1 for opcode + 2 for address)
    }

    [Fact]
    public void LDA_ZeroPageX_Instruction_Test()
    {
        byte zpAddress = 0x20;
        cpu.X = 0x05; // Set X register to 5
        ushort effectiveAddress = (ushort)(zpAddress + cpu.X); // Effective address should be 0x25
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xB5); // LDA Zero Page X opcode
        memory.Write((ushort)(cpu.PC + 1), zpAddress); // Zero page address
        memory.Write(effectiveAddress, defaultTestValue); // Write value to effective address
        
        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.A);
        Assert.Equal(0x8002, cpu.PC); // PC should increment by 2 (1 for opcode + 1 for address)
    }

    [Fact]
    public void LDA_AbsoluteX_Instruction_Test()
    {
        ushort baseAddress = 0x1230;
        cpu.X = 0x04; // Set X register to 4
        ushort effectiveAddress = (ushort)(baseAddress + cpu.X); // Effective address should be 0x1234
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xBD); // LDA Absolute X opcode
        memory.Write((ushort)(cpu.PC + 1), (byte)(baseAddress & 0xFF)); // Low byte of base address
        memory.Write((ushort)(cpu.PC + 2), (byte)(baseAddress >> 8)); // High byte of base address
        memory.Write(effectiveAddress, defaultTestValue); // Write value to effective address
        
        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.A);
        Assert.Equal(0x8003, cpu.PC); // PC should increment by 3 (1 for opcode + 2 for address)
    }

    [Fact]
    public void LDA_AbsoluteY_Instruction_Test()
    {
        ushort baseAddress = 0x1230;
        cpu.Y = 0x04; // Set Y register to 4
        ushort effectiveAddress = (ushort)(baseAddress + cpu.Y); // Effective address should be 0x1234
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xB9); // LDA Absolute Y opcode
        memory.Write((ushort)(cpu.PC + 1), (byte)(baseAddress & 0xFF)); // Low byte of base address
        memory.Write((ushort)(cpu.PC + 2), (byte)(baseAddress >> 8)); // High byte of base address
        memory.Write(effectiveAddress, defaultTestValue); // Write value to effective address
        
        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.A);
        Assert.Equal(0x8003, cpu.PC); // PC should increment by 3 (1 for opcode + 2 for address)
    }

    [Fact]
    public void LDA_IndirectX_Instruction_Test()
    {
        byte zpAddress = 0x20;
        cpu.X = 0x05; // Set X register to 5
        ushort effectiveZpAddress = (ushort)(zpAddress + cpu.X); // Effective zero page address should be 0x25
        ushort indirectAddress = 0x1234; // Indirect address to read from
        memory.Write(effectiveZpAddress, (byte)(indirectAddress & 0xFF)); // Low byte of indirect address
        memory.Write((ushort)(effectiveZpAddress + 1), (byte)(indirectAddress >> 8)); // High byte of indirect address
        memory.Write(indirectAddress, defaultTestValue); // Write value to indirect address
        
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xA1); // LDA Indirect X opcode
        memory.Write((ushort)(cpu.PC + 1), zpAddress); // Zero page address

        cpu.Clock();
        
        Assert.Equal(defaultTestValue, cpu.A);
        Assert.Equal(0x8002, cpu.PC); // PC should increment by 2 (1 for opcode + 1 for zero page address)
    }

    [Fact]
    public void LDA_IndirectY_Instruction_Test()
    {
        byte zpAddress = 0x20;
        cpu.Y = 0x04; // Set Y register to 4
        ushort effectiveZpAddress = (ushort)(zpAddress + cpu.Y); // Effective zero page address should be 0x24
        ushort indirectAddress = 0x1234; // Indirect address to read from
        memory.Write(effectiveZpAddress, (byte)(indirectAddress & 0xFF)); // Low byte of indirect address
        memory.Write((ushort)(effectiveZpAddress + 1), (byte)(indirectAddress >> 8)); // High byte of indirect address
        memory.Write(indirectAddress, defaultTestValue); // Write value to indirect address
        
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xB1); // LDA Indirect Y opcode
        memory.Write((ushort)(cpu.PC + 1), zpAddress); // Zero page address

        cpu.Clock();
        
        Assert.Equal(defaultTestValue, cpu.A);
        Assert.Equal(0x8002, cpu.PC); // PC should increment by 2 (1 for opcode + 1 for zero page address)
    }

    [Fact]
    public void LDX_Immediate_Instruction_Test()
    {
        memory.Write((ushort)(cpu.PC), 0xA2); // Write immediate value at PC
        memory.Write((ushort)(cpu.PC + 1), defaultTestValue); // Write the immediate value
        cpu.Clock(); // Execute the instruction
        Assert.Equal(defaultTestValue, cpu.X);
        Assert.Equal(0x8002, cpu.PC); // PC should increment by 2 (1 for opcode + 1 for immediate value)
    }

    [Fact]
    public void LDX_ZeroPage_Instruction_Test()
    {
        byte address = 0x20;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xA6); // LDX Zero Page opcode
        memory.Write((ushort)(cpu.PC + 1), address); // Zero page address
        memory.Write(address, defaultTestValue); // Write value to zero page
        
        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.X);
        Assert.Equal(0x8002, cpu.PC); // PC should increment by 2 (1 for opcode + 1 for address)
    }

    [Fact]
    public void LDX_Absolute_Instruction_Test()
    {
        ushort address = 0x1234;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xAE); // LDX Absolute opcode
        memory.Write((ushort)(cpu.PC + 1), (byte)(address & 0xFF)); // Low byte of address
        memory.Write((ushort)(cpu.PC + 2), (byte)(address >> 8)); // High byte of address
        memory.Write(address, defaultTestValue); // Write value to absolute address
        
        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.X);
        Assert.Equal(0x8003, cpu.PC); // PC should increment by 3 (1 for opcode + 2 for address)
    }

    [Fact]
    public void LDX_ZeroPageY_Instruction_Test()
    {
        byte zpAddress = 0x20;
        cpu.Y = 0x05; // Set Y register to 5
        ushort effectiveAddress = (ushort)(zpAddress + cpu.Y); // Effective address should be 0x25
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xB6); // LDX Zero Page Y opcode
        memory.Write((ushort)(cpu.PC + 1), zpAddress); // Zero page address
        memory.Write(effectiveAddress, defaultTestValue); // Write value to effective address
        
        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.X);
        Assert.Equal(0x8002, cpu.PC); // PC should increment by 2 (1 for opcode + 1 for address)
    }

    [Fact]
    public void LDX_AbsoluteY_Instruction_Test()
    {
        ushort baseAddress = 0x1230;
        cpu.Y = 0x04; // Set Y register to 4
        ushort effectiveAddress = (ushort)(baseAddress + cpu.Y); // Effective address should be 0x1234
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xBE); // LDX Absolute Y opcode
        memory.Write((ushort)(cpu.PC + 1), (byte)(baseAddress & 0xFF)); // Low byte of base address
        memory.Write((ushort)(cpu.PC + 2), (byte)(baseAddress >> 8)); // High byte of base address
        memory.Write(effectiveAddress, defaultTestValue); // Write value to effective address
        
        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.X);
        Assert.Equal(0x8003, cpu.PC); // PC should increment by 3 (1 for opcode + 2 for address)
    }

    [Fact]
    public void LDY_Immediate_Instruction_Test()
    {
        memory.Write((ushort)(cpu.PC), 0xA0); // Write immediate value at PC
        memory.Write((ushort)(cpu.PC + 1), defaultTestValue); // Write the immediate value
        cpu.Clock(); // Execute the instruction
        Assert.Equal(defaultTestValue, cpu.Y);
        Assert.Equal(0x8002, cpu.PC); // PC should increment by 2 (1 for opcode + 1 for immediate value)
    }

    [Fact]
    public void LDY_ZeroPage_Instruction_Test()
    {
        byte address = 0x20;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xA4); // LDY Zero Page opcode
        memory.Write((ushort)(cpu.PC + 1), address); // Zero page address
        memory.Write(address, defaultTestValue); // Write value to zero page
        
        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.Y);
        Assert.Equal(0x8002, cpu.PC); // PC should increment by 2 (1 for opcode + 1 for address)
    }

    [Fact]
    public void LDY_Absolute_Instruction_Test()
    {
        ushort address = 0x1234;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xAC); // LDY Absolute opcode
        memory.Write((ushort)(cpu.PC + 1), (byte)(address & 0xFF)); // Low byte of address
        memory.Write((ushort)(cpu.PC + 2), (byte)(address >> 8)); // High byte of address
        memory.Write(address, defaultTestValue); // Write value to absolute address
        
        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.Y);
        Assert.Equal(0x8003, cpu.PC); // PC should increment by 3 (1 for opcode + 2 for address)
    }

    [Fact]
    public void LDY_ZeroPageX_Instruction_Test()
    {
        byte zpAddress = 0x20;
        cpu.X = 0x05; // Set X register to 5
        ushort effectiveAddress = (ushort)(zpAddress + cpu.X); // Effective address should be 0x25
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xB4); // LDY Zero Page X opcode
        memory.Write((ushort)(cpu.PC + 1), zpAddress); // Zero page address
        memory.Write(effectiveAddress, defaultTestValue); // Write value to effective address
        
        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.Y);
        Assert.Equal(0x8002, cpu.PC); // PC should increment by 2 (1 for opcode + 1 for address)
    }

    [Fact]
    public void LDY_AbsoluteX_Instruction_Test()
    {
        ushort baseAddress = 0x1230;
        cpu.X = 0x04; // Set X register to 4
        ushort effectiveAddress = (ushort)(baseAddress + cpu.X); // Effective address should be 0x1234
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xBC); // LDY Absolute X opcode
        memory.Write((ushort)(cpu.PC + 1), (byte)(baseAddress & 0xFF)); // Low byte of base address
        memory.Write((ushort)(cpu.PC + 2), (byte)(baseAddress >> 8)); // High byte of base address
        memory.Write(effectiveAddress, defaultTestValue); // Write value to effective address
        
        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.Y);
        Assert.Equal(0x8003, cpu.PC); // PC should increment by 3 (1 for opcode + 2 for address)
    }
}
