namespace UnitTests;

using mini_6502;
using mini_6502.Components;

public class Instruction_Tests
{
    private readonly Ppu ppu;
    private readonly Memory memory;
    private readonly Cpu cpu;

    private byte defaultTestValue = 0xCF;
    private ushort cartridgeAddress = 0x8000;

    public Instruction_Tests()
    {
        ppu = new Ppu();
        memory = new Memory(ppu);
        cpu = new Cpu(memory);

        cpu.Reset(); // Reset CPU to initial state before each test
        cpu.Cycles = 0; // Reset cycles to 0 to ensure fresh execution
        cpu.PC = cartridgeAddress; // Point to starting address of the cartridge memory
    }


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

}
