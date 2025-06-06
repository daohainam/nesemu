using mini_6502.Components;

namespace mini_6502.Instructions;
internal class InstructionSet_StatusFlag
{
    internal static void OpCLC(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.SetFlag(Flags.FLAG_CARRY, false);
    }

    internal static void OpCLD(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.SetFlag(Flags.FLAG_DECIMAL, false);
    }

    internal static void OpCLI(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.SetFlag(Flags.FLAG_INTERRUPT, false);
    }

    internal static void OpCLV(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.SetFlag(Flags.FLAG_OVERFLOW, false);
    }

    internal static void OpSEC(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.SetFlag(Flags.FLAG_CARRY, true);
    }

    internal static void OpSED(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.SetFlag(Flags.FLAG_DECIMAL, true);
    }

    internal static void OpSEI(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.SetFlag(Flags.FLAG_INTERRUPT, true);
    }
}