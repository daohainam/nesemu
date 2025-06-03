using mini_6502.Components;

namespace mini_6502.Instructions;
internal class InstructionSet_LoadStore
{

    
    internal static void OpLDA(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        switch (mode)
        {
            case AddressingMode.Immediate:
                OpLDAImmediate(cpu, memory);
                break;
            case AddressingMode.ZeroPage:
                OpLDAZeroPage(cpu, memory);
                break;
            case AddressingMode.ZeroPageX:
                OpLDAZeroPageX(cpu, memory);
                break;
            case AddressingMode.Absolute:
                OpLDAAbsolute(cpu, memory);
                break;
            case AddressingMode.AbsoluteX:
                OpLDAAbsoluteX(cpu, memory);
                break;
            case AddressingMode.AbsoluteY:
                OpLDAAbsoluteY(cpu, memory);
                break;
            case AddressingMode.IndirectX:
                OpLDAIndirectX(cpu, memory);
                break;
            case AddressingMode.IndirectY:
                OpLDAIndirectY(cpu, memory);
                break;
            default:
                Cpu.Panic($"Unsupported addressing mode: {mode}");
                break;
        }
    }

    private static void OpLDAIndirectY(Cpu cpu, IMemory memory)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, AddressingMode.IndirectY);
        cpu.A = value;
        cpu.SetFlagsByValue(value);
    }

    private static void OpLDAIndirectX(Cpu cpu, IMemory memory)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, AddressingMode.IndirectX);
        cpu.A = value;
        cpu.SetFlagsByValue(value);
    }

    private static void OpLDAAbsoluteY(Cpu cpu, IMemory memory)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, AddressingMode.AbsoluteY);
        cpu.A = value;
        cpu.SetFlagsByValue(value);
    }

    private static void OpLDAAbsoluteX(Cpu cpu, IMemory memory)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, AddressingMode.AbsoluteX);
        cpu.A = value;
        cpu.SetFlagsByValue(value);
    }

    private static void OpLDAZeroPageX(Cpu cpu, IMemory memory)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, AddressingMode.ZeroPageX);
        cpu.A = value;
        cpu.SetFlagsByValue(value);
    }

    private static void OpLDAAbsolute(Cpu cpu, IMemory memory)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, AddressingMode.Absolute);
        cpu.A = value;
        cpu.SetFlagsByValue(value);
    }

    private static void OpLDAZeroPage(Cpu cpu, IMemory memory)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, AddressingMode.ZeroPage);

        cpu.A = value;
        cpu.SetFlagsByValue(value);
    }

    private static void OpLDAImmediate(Cpu cpu, IMemory memory)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, AddressingMode.Immediate);

        cpu.A = value;
        cpu.SetFlagsByValue(value);
    }
}
