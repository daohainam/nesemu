using mini_6502.Components;

namespace mini_6502;
internal partial class InstructionSet
{
    private static void OpLDA(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        byte value = ReadMemory(cpu.PC++, memory, mode);

        cpu.A = value;
        cpu.SetFlag(Flags.FLAG_ZERO, value == 0);
        cpu.SetFlag(Flags.FLAG_NEGATIVE, (value & 0x80) != 0);
    }

}
