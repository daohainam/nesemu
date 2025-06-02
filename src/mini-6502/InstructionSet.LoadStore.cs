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
        byte zpAddress = ReadMemory(cpu.PC++, memory, AddressingMode.IndirectY);
        ushort address = (ushort)(zpAddress + cpu.Y);
        byte value = memory.Read(address);
        cpu.A = value;
        cpu.SetFlagsByValue(value);
        cpu.PC++;
    }

    private static void OpLDAIndirectX(Cpu cpu, IMemory memory)
    {
        byte zpAddress = ReadMemory(cpu.PC++, memory, AddressingMode.IndirectX);
        ushort address = (ushort)(zpAddress + cpu.X);
        byte value = memory.Read(address);
        cpu.A = value;
        cpu.SetFlagsByValue(value);
        cpu.PC++;
    }

    private static void OpLDAAbsoluteY(Cpu cpu, IMemory memory)
    {
        byte zpAddress = ReadMemory(cpu.PC++, memory, AddressingMode.AbsoluteY);
        ushort address = (ushort)(zpAddress + cpu.Y);
        byte value = memory.Read(address);
        cpu.A = value;
        cpu.SetFlagsByValue(value);
        cpu.PC++;
    }

    private static void OpLDAAbsoluteX(Cpu cpu, IMemory memory)
    {
        byte zpAddress = ReadMemory(cpu.PC++, memory, AddressingMode.AbsoluteX);
        ushort address = (ushort)(zpAddress + cpu.X);
        byte value = memory.Read(address);
        cpu.A = value;
        cpu.SetFlagsByValue(value);
        cpu.PC++;
    }

    private static void OpLDAZeroPageX(Cpu cpu, IMemory memory)
    {
        byte zpAddress = ReadMemory(cpu.PC++, memory, AddressingMode.ZeroPageX);
        ushort address = (ushort)(zpAddress + cpu.X);
        byte value = memory.Read(address);
        cpu.A = value;
        cpu.SetFlagsByValue(value);
        cpu.PC++;
    }

    private static void OpLDAAbsolute(Cpu cpu, IMemory memory)
    {
        byte value = ReadMemory(cpu.PC, memory, AddressingMode.Absolute);
        cpu.PC += 2; // Increment PC by 2 for absolute addressing
        cpu.A = value;
        cpu.SetFlagsByValue(value);
    }

    private static void OpLDAZeroPage(Cpu cpu, IMemory memory)
    {
        byte value = ReadMemory(cpu.PC++, memory, AddressingMode.ZeroPage);

        cpu.A = value;
        cpu.SetFlagsByValue(value);
    }

    private static void OpLDAImmediate(Cpu cpu, IMemory memory)
    {
        byte value = ReadMemory(cpu.PC++, memory, AddressingMode.Immediate);

        cpu.A = value;
        cpu.SetFlagsByValue(value);
    }
}
