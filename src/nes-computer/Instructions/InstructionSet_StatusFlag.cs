using mini_6502.Components;

namespace mini_6502.Instructions;
internal class InstructionSet_StatusFlag
{
    internal static void OpCLC(InstructionContext context)
    {
        context.Cpu.SetFlag(Flags.FLAG_CARRY, false);
    }

    internal static void OpCLD(InstructionContext context)
    {
        context.Cpu.SetFlag(Flags.FLAG_DECIMAL, false);
    }

    internal static void OpCLI(InstructionContext context)
    {
        context.Cpu.SetFlag(Flags.FLAG_INTERRUPT, false);
    }

    internal static void OpCLV(InstructionContext context)
    {
        context.Cpu.SetFlag(Flags.FLAG_OVERFLOW, false);
    }

    internal static void OpSEC(InstructionContext context)
    {
        context.Cpu.SetFlag(Flags.FLAG_CARRY, true);
    }

    internal static void OpSED(InstructionContext context)
    {
        context.Cpu.SetFlag(Flags.FLAG_DECIMAL, true);
    }

    internal static void OpSEI(InstructionContext context)
    {
        context.Cpu.SetFlag(Flags.FLAG_INTERRUPT, true);
    }
}