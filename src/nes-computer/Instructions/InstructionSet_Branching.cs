using mini_6502.Components;

namespace mini_6502.Instructions;
internal class InstructionSet_Branching
{
    internal static void OpBCC(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        if (!cpu.GetFlag(Flags.FLAG_CARRY))
        {
            byte offset = memory.Read(cpu.PC++);
            cpu.PC = (ushort)(cpu.PC + (sbyte)offset); // Signed offset
        }
        else
        {
            cpu.PC++;
        }
    }
    internal static void OpBCS(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        if (cpu.GetFlag(Flags.FLAG_CARRY))
        {
            byte offset = memory.Read(cpu.PC++);
            cpu.PC = (ushort)(cpu.PC + (sbyte)offset); // Signed offset
        }
        else
        {
            cpu.PC++;
        }
    }
    internal static void OpBEQ(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        if (cpu.GetFlag(Flags.FLAG_ZERO))
        {
            byte offset = memory.Read(cpu.PC++);
            cpu.PC = (ushort)(cpu.PC + (sbyte)offset); // Signed offset
        }
        else
        {
            cpu.PC++;
        }
    }
    internal static void OpBNE(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        if (!cpu.GetFlag(Flags.FLAG_ZERO))
        {
            byte offset = memory.Read(cpu.PC++);
            cpu.PC = (ushort)(cpu.PC + (sbyte)offset); // Signed offset
        }
        else
        {
            cpu.PC++;
        }
    }
    internal static void OpBPL(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        if (cpu.GetFlag(Flags.FLAG_NEGATIVE))
        {
            byte offset = memory.Read(cpu.PC++);
            cpu.PC = (ushort)(cpu.PC + (sbyte)offset); // Signed offset
        }
        else
        {
            cpu.PC++;
        }
    }
    internal static void OpBVS(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        if (cpu.GetFlag(Flags.FLAG_OVERFLOW))
        {
            byte offset = memory.Read(cpu.PC++);
            cpu.PC = (ushort)(cpu.PC + (sbyte)offset); // Signed offset
        }
        else
        {
            cpu.PC++;
        }
    }
    internal static void OpBVC(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        if (!cpu.GetFlag(Flags.FLAG_OVERFLOW))
        {
            byte offset = memory.Read(cpu.PC++);
            cpu.PC = (ushort)(cpu.PC + (sbyte)offset); // Signed offset
        }
        else
        {
            cpu.PC++;
        }
    }
    internal static void OpBMI(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        if (cpu.GetFlag(Flags.FLAG_NEGATIVE))
        {
            byte offset = memory.Read(cpu.PC++);
            cpu.PC = (ushort)(cpu.PC + (sbyte)offset); // Signed offset
        }
        else
        {
            cpu.PC++;
        }
    }
}