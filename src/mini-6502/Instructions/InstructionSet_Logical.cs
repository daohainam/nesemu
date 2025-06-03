using mini_6502.Components;

namespace mini_6502.Instructions;
internal class InstructionSet_Logical
{
    internal static void OpAND(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, mode);
        cpu.A &= value;
        cpu.SetFlag(Flags.FLAG_ZERO, cpu.A == 0);
        cpu.SetFlag(Flags.FLAG_NEGATIVE, (cpu.A & 0x80) != 0);
    }

    internal static void OpORA(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, mode);
        cpu.A |= value;
        cpu.SetFlag(Flags.FLAG_ZERO, cpu.A == 0);
        cpu.SetFlag(Flags.FLAG_NEGATIVE, (cpu.A & 0x80) != 0);
    }

    internal static void OpEOR(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, mode);
        cpu.A ^= value;
        cpu.SetFlag(Flags.FLAG_ZERO, cpu.A == 0);
        cpu.SetFlag(Flags.FLAG_NEGATIVE, (cpu.A & 0x80) != 0);
    }

    internal static void OpBIT(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, mode);
        cpu.SetFlag(Flags.FLAG_ZERO, (cpu.A & value) == 0);
        cpu.SetFlag(Flags.FLAG_NEGATIVE, (value & 0x80) != 0);
        cpu.SetFlag(Flags.FLAG_OVERFLOW, (value & 0x40) != 0);
    }



}
