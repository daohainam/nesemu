namespace UnitTests;
public partial class Instruction_Tests
{
    [Fact]
    public void LDA_Immediate_Instruction_Test()
    {
        memory.Write((ushort)(cpu.PC), 0xA9);
        memory.Write((ushort)(cpu.PC + 1), defaultTestValue);

        cpu.Clock();

        Assert.Equal(defaultTestValue, cpu.A);
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void LDA_ZeroPage_Instruction_Test()
    {
        byte address = 0x20;

        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xA5);
        memory.Write((ushort)(cpu.PC + 1), address);
        memory.Write(address, defaultTestValue);

        cpu.Clock();

        Assert.Equal(defaultTestValue, cpu.A);
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void LDA_Absolute_Instruction_Test()
    {
        ushort address = 0x1234;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xAD);
        memory.Write((ushort)(cpu.PC + 1), (byte)(address & 0xFF));
        memory.Write((ushort)(cpu.PC + 2), (byte)(address >> 8));
        memory.Write(address, defaultTestValue);

        cpu.Clock();

        Assert.Equal(defaultTestValue, cpu.A);
        Assert.Equal(0x8003, cpu.PC);
    }

    [Fact]
    public void LDA_ZeroPageX_Instruction_Test()
    {
        byte zpAddress = 0x20;
        cpu.X = 0x05;
        ushort effectiveAddress = (ushort)(zpAddress + cpu.X);
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xB5);
        memory.Write((ushort)(cpu.PC + 1), zpAddress);
        memory.Write(effectiveAddress, defaultTestValue);

        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.A);
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void LDA_AbsoluteX_Instruction_Test()
    {
        ushort baseAddress = 0x1230;
        cpu.X = 0x04;
        ushort effectiveAddress = (ushort)(baseAddress + cpu.X);
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xBD);
        memory.Write((ushort)(cpu.PC + 1), (byte)(baseAddress & 0xFF));
        memory.Write((ushort)(cpu.PC + 2), (byte)(baseAddress >> 8));
        memory.Write(effectiveAddress, defaultTestValue);

        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.A);
        Assert.Equal(0x8003, cpu.PC);
    }

    [Fact]
    public void LDA_AbsoluteY_Instruction_Test()
    {
        ushort baseAddress = 0x1230;
        cpu.Y = 0x04;
        ushort effectiveAddress = (ushort)(baseAddress + cpu.Y);
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xB9);
        memory.Write((ushort)(cpu.PC + 1), (byte)(baseAddress & 0xFF));
        memory.Write((ushort)(cpu.PC + 2), (byte)(baseAddress >> 8));
        memory.Write(effectiveAddress, defaultTestValue);

        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.A);
        Assert.Equal(0x8003, cpu.PC);
    }

    [Fact]
    public void LDA_IndirectX_Instruction_Test()
    {
        byte zpAddress = 0x20;
        cpu.X = 0x05;
        ushort effectiveZpAddress = (ushort)(zpAddress + cpu.X);
        ushort indirectAddress = 0x1234;
        memory.Write(effectiveZpAddress, (byte)(indirectAddress & 0xFF));
        memory.Write((ushort)(effectiveZpAddress + 1), (byte)(indirectAddress >> 8));
        memory.Write(indirectAddress, defaultTestValue);

        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xA1);
        memory.Write((ushort)(cpu.PC + 1), zpAddress);

        cpu.Clock();

        Assert.Equal(defaultTestValue, cpu.A);
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void LDA_IndirectY_Instruction_Test()
    {
        byte zpAddress = 0x20;
        cpu.Y = 0x04;
        ushort effectiveZpAddress = (ushort)(zpAddress + cpu.Y);
        ushort indirectAddress = 0x1234;
        memory.Write(effectiveZpAddress, (byte)(indirectAddress & 0xFF));
        memory.Write((ushort)(effectiveZpAddress + 1), (byte)(indirectAddress >> 8));
        memory.Write(indirectAddress, defaultTestValue);

        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xB1);
        memory.Write((ushort)(cpu.PC + 1), zpAddress);

        cpu.Clock();

        Assert.Equal(defaultTestValue, cpu.A);
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void LDX_Immediate_Instruction_Test()
    {
        memory.Write((ushort)(cpu.PC), 0xA2);
        memory.Write((ushort)(cpu.PC + 1), defaultTestValue);
        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.X);
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void LDX_ZeroPage_Instruction_Test()
    {
        byte address = 0x20;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xA6);
        memory.Write((ushort)(cpu.PC + 1), address);
        memory.Write(address, defaultTestValue);

        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.X);
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void LDX_Absolute_Instruction_Test()
    {
        ushort address = 0x1234;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xAE);
        memory.Write((ushort)(cpu.PC + 1), (byte)(address & 0xFF));
        memory.Write((ushort)(cpu.PC + 2), (byte)(address >> 8));
        memory.Write(address, defaultTestValue);

        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.X);
        Assert.Equal(0x8003, cpu.PC);
    }

    [Fact]
    public void LDX_ZeroPageY_Instruction_Test()
    {
        byte zpAddress = 0x20;
        cpu.Y = 0x05;
        ushort effectiveAddress = (ushort)(zpAddress + cpu.Y);
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xB6);
        memory.Write((ushort)(cpu.PC + 1), zpAddress);
        memory.Write(effectiveAddress, defaultTestValue);

        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.X);
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void LDX_AbsoluteY_Instruction_Test()
    {
        ushort baseAddress = 0x1230;
        cpu.Y = 0x04;
        ushort effectiveAddress = (ushort)(baseAddress + cpu.Y);
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xBE);
        memory.Write((ushort)(cpu.PC + 1), (byte)(baseAddress & 0xFF));
        memory.Write((ushort)(cpu.PC + 2), (byte)(baseAddress >> 8));
        memory.Write(effectiveAddress, defaultTestValue);

        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.X);
        Assert.Equal(0x8003, cpu.PC);
    }

    [Fact]
    public void LDY_Immediate_Instruction_Test()
    {
        memory.Write((ushort)(cpu.PC), 0xA0);
        memory.Write((ushort)(cpu.PC + 1), defaultTestValue);
        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.Y);
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void LDY_ZeroPage_Instruction_Test()
    {
        byte address = 0x20;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xA4);
        memory.Write((ushort)(cpu.PC + 1), address);
        memory.Write(address, defaultTestValue);

        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.Y);
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void LDY_Absolute_Instruction_Test()
    {
        ushort address = 0x1234;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xAC);
        memory.Write((ushort)(cpu.PC + 1), (byte)(address & 0xFF));
        memory.Write((ushort)(cpu.PC + 2), (byte)(address >> 8));
        memory.Write(address, defaultTestValue);

        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.Y);
        Assert.Equal(0x8003, cpu.PC);
    }

    [Fact]
    public void LDY_ZeroPageX_Instruction_Test()
    {
        byte zpAddress = 0x20;
        cpu.X = 0x05;
        ushort effectiveAddress = (ushort)(zpAddress + cpu.X);
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xB4);
        memory.Write((ushort)(cpu.PC + 1), zpAddress);
        memory.Write(effectiveAddress, defaultTestValue);

        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.Y);
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void LDY_AbsoluteX_Instruction_Test()
    {
        ushort baseAddress = 0x1230;
        cpu.X = 0x04;
        ushort effectiveAddress = (ushort)(baseAddress + cpu.X);
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xBC);
        memory.Write((ushort)(cpu.PC + 1), (byte)(baseAddress & 0xFF));
        memory.Write((ushort)(cpu.PC + 2), (byte)(baseAddress >> 8));
        memory.Write(effectiveAddress, defaultTestValue);

        cpu.Clock();
        Assert.Equal(defaultTestValue, cpu.Y);
        Assert.Equal(0x8003, cpu.PC);
    }

    [Fact]
    public void STA_ZeroPage_Instruction_Test()
    {
        byte address = 0x20;
        cpu.A = defaultTestValue;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0x85);
        memory.Write((ushort)(cpu.PC + 1), address);

        cpu.Clock();

        Assert.Equal(defaultTestValue, memory.Read(address));
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void STA_ZeroPageX_Instruction_Test()
    {
        byte zpAddress = 0x20;
        cpu.X = 0x05;
        ushort effectiveAddress = (ushort)(zpAddress + cpu.X);
        cpu.A = defaultTestValue;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0x95);
        memory.Write((ushort)(cpu.PC + 1), zpAddress);

        cpu.Clock();

        Assert.Equal(defaultTestValue, memory.Read(effectiveAddress));
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void STA_Absolute_Instruction_Test()
    {
        ushort address = 0x1234;
        cpu.A = defaultTestValue;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0x8D);
        memory.Write((ushort)(cpu.PC + 1), (byte)(address & 0xFF));
        memory.Write((ushort)(cpu.PC + 2), (byte)(address >> 8));

        cpu.Clock();

        Assert.Equal(defaultTestValue, memory.Read(address));
        Assert.Equal(0x8003, cpu.PC);
    }

    [Fact]
    public void STA_AbsoluteX_Instruction_Test()
    {
        ushort baseAddress = 0x1230;
        cpu.X = 0x04;
        ushort effectiveAddress = (ushort)(baseAddress + cpu.X);
        cpu.A = defaultTestValue;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0x9D);
        memory.Write((ushort)(cpu.PC + 1), (byte)(baseAddress & 0xFF));
        memory.Write((ushort)(cpu.PC + 2), (byte)(baseAddress >> 8));

        cpu.Clock();

        Assert.Equal(defaultTestValue, memory.Read(effectiveAddress));
        Assert.Equal(0x8003, cpu.PC);
    }

    [Fact]
    public void STA_AbsoluteY_Instruction_Test()
    {
        ushort baseAddress = 0x1230;
        cpu.Y = 0x04;
        ushort effectiveAddress = (ushort)(baseAddress + cpu.Y);
        cpu.A = defaultTestValue;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0x99);
        memory.Write((ushort)(cpu.PC + 1), (byte)(baseAddress & 0xFF));
        memory.Write((ushort)(cpu.PC + 2), (byte)(baseAddress >> 8));

        cpu.Clock();

        Assert.Equal(defaultTestValue, memory.Read(effectiveAddress));
        Assert.Equal(0x8003, cpu.PC);
    }

    [Fact]
    public void STA_IndirectX_Instruction_Test()
    {
        byte zpAddress = 0x20;
        cpu.X = 0x05;
        ushort effectiveZpAddress = (ushort)(zpAddress + cpu.X);
        ushort indirectAddress = 0x1234;
        memory.Write(effectiveZpAddress, (byte)(indirectAddress & 0xFF));
        memory.Write((ushort)(effectiveZpAddress + 1), (byte)(indirectAddress >> 8));
        cpu.A = defaultTestValue;

        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0x81);
        memory.Write((ushort)(cpu.PC + 1), zpAddress);
        cpu.Clock();

        Assert.Equal(defaultTestValue, memory.Read(indirectAddress));
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void STA_IndirectY_Instruction_Test()
    {
        byte zpAddress = 0x20;
        cpu.Y = 0x04;
        ushort effectiveZpAddress = (ushort)(zpAddress + cpu.Y);
        ushort indirectAddress = 0x1234;
        memory.Write(effectiveZpAddress, (byte)(indirectAddress & 0xFF));
        memory.Write((ushort)(effectiveZpAddress + 1), (byte)(indirectAddress >> 8));
        cpu.A = defaultTestValue;

        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0x91);
        memory.Write((ushort)(cpu.PC + 1), zpAddress);
        cpu.Clock();

        Assert.Equal(defaultTestValue, memory.Read(indirectAddress));
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void STX_ZeroPage_Instruction_Test()
    {
        byte address = 0x20;
        cpu.X = defaultTestValue;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0x86);
        memory.Write((ushort)(cpu.PC + 1), address);

        cpu.Clock();

        Assert.Equal(defaultTestValue, memory.Read(address));
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void STX_ZeroPageY_Instruction_Test()
    {
        byte zpAddress = 0x20;
        cpu.X = defaultTestValue;
        cpu.Y = 0x05;
        ushort effectiveAddress = (ushort)(zpAddress + cpu.Y);
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0x96);
        memory.Write((ushort)(cpu.PC + 1), zpAddress);

        cpu.Clock();

        Assert.Equal(defaultTestValue, memory.Read(effectiveAddress));
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void STX_Absolute_Instruction_Test()
    {
        ushort address = 0x1234;
        cpu.X = defaultTestValue;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0x8E);
        memory.Write((ushort)(cpu.PC + 1), (byte)(address & 0xFF));
        memory.Write((ushort)(cpu.PC + 2), (byte)(address >> 8));

        cpu.Clock();

        Assert.Equal(defaultTestValue, memory.Read(address));
        Assert.Equal(0x8003, cpu.PC);
    }

    [Fact]
    public void STY_ZeroPage_Instruction_Test()
    {
        byte address = 0x20;
        cpu.Y = defaultTestValue;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0x84);
        memory.Write((ushort)(cpu.PC + 1), address);

        cpu.Clock();

        Assert.Equal(defaultTestValue, memory.Read(address));
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void STY_ZeroPageX_Instruction_Test()
    {
        byte zpAddress = 0x20;
        cpu.X = 0x05;
        ushort effectiveAddress = (ushort)(zpAddress + cpu.X);
        cpu.Y = defaultTestValue;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0x94);
        memory.Write((ushort)(cpu.PC + 1), zpAddress);

        cpu.Clock();

        Assert.Equal(defaultTestValue, memory.Read(effectiveAddress));
        Assert.Equal(0x8002, cpu.PC);
    }

    [Fact]
    public void STY_Absolute_Instruction_Test()
    {
        ushort address = 0x1234;
        cpu.Y = defaultTestValue;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0x8C);
        memory.Write((ushort)(cpu.PC + 1), (byte)(address & 0xFF));
        memory.Write((ushort)(cpu.PC + 2), (byte)(address >> 8));

        cpu.Clock();

        Assert.Equal(defaultTestValue, memory.Read(address));
        Assert.Equal(0x8003, cpu.PC);
    }
}
