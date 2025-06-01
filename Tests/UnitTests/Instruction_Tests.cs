namespace UnitTests;

using mini_6502;
using mini_6502.Components;

public class Instruction_Tests
{
    private readonly Ppu ppu;
    private readonly Memory memory;
    private readonly Cpu cpu;

    public Instruction_Tests()
    {
        ppu = new Ppu();
        memory = new Memory(ppu);
        cpu = new Cpu(memory);

        cpu.Reset(); // Reset CPU to initial state before each test
        cpu.Cycles = 0; // Reset cycles to 0 to ensure fresh execution
    }


    [Fact]
    public void LDA_Immediate_Instruction_Test()
    {
        // Arrange
        byte value = 0x42;
        cpu.PC = 0x8000;
        memory.Write((ushort)(cpu.PC), 0xA9); // Write immediate value at PC
        memory.Write((ushort)(cpu.PC + 1), value); // Write the immediate value

        cpu.Clock(); // Execute the instruction

        // Assert
        Assert.Equal(value, cpu.A);
        Assert.Equal(0x8002, cpu.PC); // PC should increment by 2 (1 for opcode + 1 for immediate value)
    }

}
