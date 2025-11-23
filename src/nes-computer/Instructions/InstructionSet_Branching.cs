using mini_6502.Components;

namespace mini_6502.Instructions;

internal static class InstructionSet_Branching
{
    private static void BranchIf(InstructionContext context, bool condition)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;

        if (condition)
        {
            byte offset = memory.Read(cpu.PC++);
            cpu.PC = (ushort)(cpu.PC + (sbyte)offset);
        }
        else
        {
            cpu.PC++;
        }
    }

    internal static void OpBCC(InstructionContext context)
    {
        BranchIf(context, !context.Cpu.GetFlag(Flags.FLAG_CARRY));
    }

    internal static void OpBCS(InstructionContext context)
    {
        BranchIf(context, context.Cpu.GetFlag(Flags.FLAG_CARRY));
    }

    internal static void OpBEQ(InstructionContext context)
    {
        BranchIf(context, context.Cpu.GetFlag(Flags.FLAG_ZERO));
    }

    internal static void OpBNE(InstructionContext context)
    {
        BranchIf(context, !context.Cpu.GetFlag(Flags.FLAG_ZERO));
    }

    internal static void OpBMI(InstructionContext context)
    {
        BranchIf(context, context.Cpu.GetFlag(Flags.FLAG_NEGATIVE));
    }

    internal static void OpBPL(InstructionContext context)
    {
        // Branch on PLus: N == 0
        BranchIf(context, !context.Cpu.GetFlag(Flags.FLAG_NEGATIVE));
    }

    internal static void OpBVC(InstructionContext context)
    {
        BranchIf(context, !context.Cpu.GetFlag(Flags.FLAG_OVERFLOW));
    }

    internal static void OpBVS(InstructionContext context)
    {
        BranchIf(context, context.Cpu.GetFlag(Flags.FLAG_OVERFLOW));
    }
}
