using mini_6502;
using mini_6502.Instructions;

internal class InstructionSet_Logical
{
    internal static void OpAND(InstructionContext context)
    {
        byte value = (byte)InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode);
        context.Cpu.A &= value;
        context.Cpu.SetFlag(Flags.FLAG_ZERO, context.Cpu.A == 0);
        context.Cpu.SetFlag(Flags.FLAG_NEGATIVE, (context.Cpu.A & 0x80) != 0);
    }

    internal static void OpORA(InstructionContext context)
    {
        byte value = (byte)InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode);
        context.Cpu.A |= value;
        context.Cpu.SetFlag(Flags.FLAG_ZERO, context.Cpu.A == 0);
        context.Cpu.SetFlag(Flags.FLAG_NEGATIVE, (context.Cpu.A & 0x80) != 0);
    }

    internal static void OpEOR(InstructionContext context)
    {
        byte value = (byte)InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode);
        context.Cpu.A ^= value;
        context.Cpu.SetFlag(Flags.FLAG_ZERO, context.Cpu.A == 0);
        context.Cpu.SetFlag(Flags.FLAG_NEGATIVE, (context.Cpu.A & 0x80) != 0);
    }

    internal static void OpBIT(InstructionContext context)
    {
        byte value = (byte)InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode);
        context.Cpu.SetFlag(Flags.FLAG_ZERO, (context.Cpu.A & value) == 0);
        context.Cpu.SetFlag(Flags.FLAG_NEGATIVE, (value & 0x80) != 0);
        context.Cpu.SetFlag(Flags.FLAG_OVERFLOW, (value & 0x40) != 0);
    }
}
