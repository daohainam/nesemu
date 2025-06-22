using mini_6502.Components;

namespace mini_6502.Instructions;
internal class InstructionSet_LoadStore
{
    // https://www.masswerk.at/6502/6502_instruction_set.html
    internal static void OpLDA(InstructionContext context)
    {
        switch (context.Mode)
        {
            case AddressingMode.Immediate:
            case AddressingMode.ZeroPage:
            case AddressingMode.ZeroPageX:
            case AddressingMode.Absolute:
            case AddressingMode.AbsoluteX:
            case AddressingMode.AbsoluteY:
            case AddressingMode.IndirectX:
            case AddressingMode.IndirectY:
                byte value = (byte)InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode);
                context.Cpu.A = value;
                context.Cpu.SetZNFlagsByValue(value);
                break;
            default:
                Cpu.Panic($"LDA: Unsupported addressing mode: {context.Mode}");
                break;
        }
    }

    internal static void OpLDX(InstructionContext context)
    {
        switch (context.Mode)
        {
            case AddressingMode.Immediate:
            case AddressingMode.ZeroPage:
            case AddressingMode.ZeroPageY:
            case AddressingMode.Absolute:
            case AddressingMode.AbsoluteY:
                byte value = (byte)InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode);
                context.Cpu.X = value;
                context.Cpu.SetZNFlagsByValue(value);
                break;
            default:
                Cpu.Panic($"LDX: Unsupported addressing mode: {context.Mode}");
                break;
        }
    }

    internal static void OpLDY(InstructionContext context)
    {
        switch (context.Mode)
        {
            case AddressingMode.Immediate:
            case AddressingMode.ZeroPage:
            case AddressingMode.ZeroPageX:
            case AddressingMode.Absolute:
            case AddressingMode.AbsoluteX:
                byte value = (byte)InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode);
                context.Cpu.Y = value;
                context.Cpu.SetZNFlagsByValue(value);
                break;
            default:
                Cpu.Panic($"LDY: Unsupported addressing mode: {context.Mode}");
                break;
        }
    }
    internal static void OpSTA(InstructionContext context)
    {
        switch (context.Mode)
        {
            case AddressingMode.ZeroPage:
            case AddressingMode.ZeroPageX:
            case AddressingMode.Absolute:
            case AddressingMode.AbsoluteX:
            case AddressingMode.AbsoluteY:
            case AddressingMode.IndirectX:
            case AddressingMode.IndirectY:
                InstructionHelpers.WriteMemory(context.Cpu, context.Memory, context.Mode, context.Cpu.A);
                break;
            default:
                Cpu.Panic($"STA: Unsupported addressing mode: {context.Mode}");
                break;
        }
    }

    internal static void OpSTX(InstructionContext context)
    {
        switch (context.Mode)
        {
            case AddressingMode.ZeroPage:
            case AddressingMode.ZeroPageY:
            case AddressingMode.Absolute:
            case AddressingMode.AbsoluteY:
                InstructionHelpers.WriteMemory(context.Cpu, context.Memory, context.Mode, context.Cpu.X);
                break;
            default:
                Cpu.Panic($"STX: Unsupported addressing mode: {context.Mode}");
                break;
        }
    }

    internal static void OpSTY(InstructionContext context)
    {
        switch (context.Mode)
        {
            case AddressingMode.ZeroPage:
            case AddressingMode.ZeroPageX:
            case AddressingMode.Absolute:
            case AddressingMode.AbsoluteX:
                InstructionHelpers.WriteMemory(context.Cpu, context.Memory, context.Mode, context.Cpu.Y);
                break;
            default:
                Cpu.Panic($"STY: Unsupported addressing mode: {context.Mode}");
                break;
        }
    }
}
