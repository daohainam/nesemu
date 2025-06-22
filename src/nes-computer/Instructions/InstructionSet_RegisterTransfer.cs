using mini_6502;
using mini_6502.Components;

internal class InstructionSet_RegisterTransfer
{
    internal static void OpTAX(InstructionContext context)
    {
        context.Cpu.X = context.Cpu.A;
        context.Cpu.SetZNFlagsByValue(context.Cpu.X);
    }

    internal static void OpTAY(InstructionContext context)
    {
        context.Cpu.Y = context.Cpu.A;
        context.Cpu.SetZNFlagsByValue(context.Cpu.Y);
    }

    internal static void OpTXA(InstructionContext context)
    {
        context.Cpu.A = context.Cpu.X;
        context.Cpu.SetZNFlagsByValue(context.Cpu.A);
    }

    internal static void OpTYA(InstructionContext context)
    {
        context.Cpu.A = context.Cpu.Y;
        context.Cpu.SetZNFlagsByValue(context.Cpu.A);
    }

    internal static void OpTSX(InstructionContext context)
    {
        context.Cpu.X = context.Cpu.SP;
        context.Cpu.SetZNFlagsByValue(context.Cpu.X);
    }

    internal static void OpTXS(InstructionContext context)
    {
        context.Cpu.SP = context.Cpu.X;
    }
}
