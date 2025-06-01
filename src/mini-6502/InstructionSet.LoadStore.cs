using mini_6502.Components;

namespace mini_6502;
internal partial class InstructionSet
{
    private static void OpLDA(Cpu cpu, IMemory memory, AddressingMode mode)
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
                //OpLDAZeroPageX(cpu, memory);
                break;
            case AddressingMode.Absolute:
                OpLDAAbsolute(cpu, memory);
                break;
            case AddressingMode.AbsoluteX:
                //OpLDAAbsoluteX(cpu, memory);
                break;
            case AddressingMode.AbsoluteY:
                //OpLDAAbsoluteY(cpu, memory);
                break;
            case AddressingMode.IndirectX:
                //OpLDAIndirectX(cpu, memory);
                break;
            case AddressingMode.IndirectY:
                //OpLDAIndirectY(cpu, memory);
                break;
            default:
                Cpu.Panic($"Unsupported addressing mode: {mode}");
                break;
        }
    }

    private static void OpLDAAbsolute(Cpu cpu, IMemory memory)
    {
        byte value = ReadMemory(cpu.PC, memory, AddressingMode.Absolute);

        cpu.PC += 2; // Increment PC by 2 for absolute addressing
        cpu.A = value;
        cpu.SetFlag(Flags.FLAG_ZERO, value == 0);
        cpu.SetFlag(Flags.FLAG_NEGATIVE, (value & 0x80) != 0);

    }

    private static void OpLDAZeroPage(Cpu cpu, IMemory memory)
    {
        byte value = ReadMemory(cpu.PC++, memory, AddressingMode.ZeroPage);

        cpu.A = value;
        cpu.SetFlag(Flags.FLAG_ZERO, value == 0);
        cpu.SetFlag(Flags.FLAG_NEGATIVE, (value & 0x80) != 0);
    }

    private static void OpLDAImmediate(Cpu cpu, IMemory memory)
    {
        byte value = ReadMemory(cpu.PC++, memory, AddressingMode.Immediate);

        cpu.A = value;
        cpu.SetFlag(Flags.FLAG_ZERO, value == 0);
        cpu.SetFlag(Flags.FLAG_NEGATIVE, (value & 0x80) != 0);
    }
}
