using mini_6502.Components;

namespace mini_6502.Instructions;
internal class InstructionSet_Branching
{
    internal static void OpBCC(InstructionContext context)
    {
        if (!context.Cpu.GetFlag(Flags.FLAG_CARRY))
        {
            byte offset = context.Memory.Read(context.Cpu.PC++);
            context.Cpu.PC = (ushort)(context.Cpu.PC + (sbyte)offset); // Signed offset
        }
        else
        {
            context.Cpu.PC++;
        }
    }
    internal static void OpBCS(InstructionContext context)
    {
        if (context.Cpu.GetFlag(Flags.FLAG_CARRY))
        {
            byte offset = context.Memory.Read(context.Cpu.PC++);
            context.Cpu.PC = (ushort)(context.Cpu.PC + (sbyte)offset); // Signed offset
        }
        else
        {
            context.Cpu.PC++;
        }
    }
    internal static void OpBEQ(InstructionContext context)
    {
        if (context.Cpu.GetFlag(Flags.FLAG_ZERO))
        {
            byte offset = context.Memory.Read(context.Cpu.PC++);
            context.Cpu.PC = (ushort)(context.Cpu.PC + (sbyte)offset); // Signed offset
        }
        else
        {
            context.Cpu.PC++;
        }
    }
    internal static void OpBNE(InstructionContext context)
    {
        if (!context.Cpu.GetFlag(Flags.FLAG_ZERO))
        {
            byte offset = context.Memory.Read(context.Cpu.PC++);
            context.Cpu.PC = (ushort)(context.Cpu.PC + (sbyte)offset); // Signed offset
        }
        else
        {
            context.Cpu.PC++;
        }
    }
    internal static void OpBPL(InstructionContext context)
    {
        if (context.Cpu.GetFlag(Flags.FLAG_NEGATIVE))
        {
            byte offset = context.Memory.Read(context.Cpu.PC++);
            context.Cpu.PC = (ushort)(context.Cpu.PC + (sbyte)offset); // Signed offset
        }
        else
        {
            context.Cpu.PC++;
        }
    }
    internal static void OpBVS(InstructionContext context)
    {
        if (context.Cpu.GetFlag(Flags.FLAG_OVERFLOW))
        {
            byte offset = context.Memory.Read(context.Cpu.PC++);
            context.Cpu.PC = (ushort)(context.Cpu.PC + (sbyte)offset); // Signed offset
        }
        else
        {
            context.Cpu.PC++;
        }
    }
    internal static void OpBVC(InstructionContext context)
    {
        if (!context.Cpu.GetFlag(Flags.FLAG_OVERFLOW))
        {
            byte offset = context.Memory.Read(context.Cpu.PC++);
            context.Cpu.PC = (ushort)(context.Cpu.PC + (sbyte)offset); // Signed offset
        }
        else
        {
            context.Cpu.PC++;
        }
    }
    internal static void OpBMI(InstructionContext context)
    {
        if (context.Cpu.GetFlag(Flags.FLAG_NEGATIVE))
        {
            byte offset = context.Memory.Read(context.Cpu.PC++);
            context.Cpu.PC = (ushort)(context.Cpu.PC + (sbyte)offset); // Signed offset
        }
        else
        {
            context.Cpu.PC++;
        }
    }
}