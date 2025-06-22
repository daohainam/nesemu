using mini_6502;
using mini_6502.Components;

internal class InstructionSet_Stack
{
    internal static void OpPHA(InstructionContext context)
    {
        context.Memory.Write((ushort)(0x100 + context.Cpu.SP--), context.Cpu.A);
    }

    internal static void OpPHP(InstructionContext context)
    {
        context.Memory.Write((ushort)(0x100 + context.Cpu.SP--), context.Cpu.P);
    }

    internal static void OpPLA(InstructionContext context)
    {
        context.Cpu.A = context.Memory.Read((ushort)(0x100 + ++context.Cpu.SP));
        context.Cpu.SetZNFlagsByValue(context.Cpu.A);
    }

    internal static void OpPLP(InstructionContext context)
    {
        context.Cpu.P = context.Memory.Read((ushort)(0x100 + ++context.Cpu.SP));
    }
}
