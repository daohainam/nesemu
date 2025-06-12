namespace UnitTests;

using mini_6502;
using mini_6502.Components;

public partial class Instruction_Tests
{
    internal readonly Ppu ppu;
    internal readonly Memory memory;
    internal readonly Cpu cpu;

    internal readonly byte defaultTestValue = 0xCF;
    internal readonly ushort cartridgeAddress = 0x8000;

    public Instruction_Tests()
    {
        ppu = new Ppu();
        memory = new Memory(MapperFactory.CreateMapper0(), ppu);
        cpu = new Cpu(memory);

        cpu.Reset(); // Reset CPU to initial state before each test
        cpu.Cycles = 0; // Reset cycles to 0 to ensure fresh execution
        cpu.PC = cartridgeAddress; // Point to starting address of the cartridge memory
    }
}
