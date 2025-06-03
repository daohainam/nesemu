using mini_6502.Components;

namespace mini_6502.Instructions;
internal class InstructionSet_Arithmetic
{
    internal static void OpADC(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, mode);
        ushort result = (ushort)(cpu.A + value + (cpu.GetFlag(Flags.FLAG_CARRY) ? 1 : 0));
        
        cpu.SetFlag(Flags.FLAG_CARRY, result > 0xFF);
        cpu.SetZNFlagsByValue((byte)(result & 0xFF));
        cpu.SetFlag(Flags.FLAG_OVERFLOW, ((cpu.A ^ value) & 0x80) == 0 && ((cpu.A ^ result) & 0x80) != 0);
        
        cpu.A = (byte)(result & 0xFF);
    }

    internal static void OpSBC(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, mode);
        ushort result = (ushort)(cpu.A - value - (cpu.GetFlag(Flags.FLAG_CARRY) ? 0 : 1));
        
        cpu.SetFlag(Flags.FLAG_CARRY, result < 0x100);
        cpu.SetZNFlagsByValue((byte)(result & 0xFF));
        cpu.SetFlag(Flags.FLAG_OVERFLOW, ((cpu.A ^ value) & 0x80) != 0 && ((cpu.A ^ result) & 0x80) != 0);
        
        cpu.A = (byte)(result & 0xFF);
    }

    internal static void OpCMP(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, mode);
        ushort result = (ushort)(cpu.A - value);
        
        cpu.SetFlag(Flags.FLAG_CARRY, cpu.A >= value);
        cpu.SetZNFlagsByValue((byte)(result & 0xFF));
        cpu.SetFlag(Flags.FLAG_OVERFLOW, ((cpu.A ^ value) & 0x80) != 0 && ((cpu.A ^ result) & 0x80) != 0);
    }

    internal static void OpCPX(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, mode);
        ushort result = (ushort)(cpu.X - value);
        
        cpu.SetFlag(Flags.FLAG_CARRY, cpu.X >= value);
        cpu.SetZNFlagsByValue((byte)(result & 0xFF));
        cpu.SetFlag(Flags.FLAG_OVERFLOW, ((cpu.X ^ value) & 0x80) != 0 && ((cpu.X ^ result) & 0x80) != 0);
    }

    internal static void OpCPY(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, mode);
        ushort result = (ushort)(cpu.Y - value);
        
        cpu.SetFlag(Flags.FLAG_CARRY, cpu.Y >= value);
        cpu.SetZNFlagsByValue((byte)(result & 0xFF));
        cpu.SetFlag(Flags.FLAG_OVERFLOW, ((cpu.Y ^ value) & 0x80) != 0 && ((cpu.Y ^ result) & 0x80) != 0);
    }

    internal static void OpINC(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, mode, out var address);
        value = (byte)((value + 1) & 0xFF);
        memory.Write(address, value);

        cpu.SetZNFlagsByValue(value);
    }

    internal static void OpDEC(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        byte value = InstructionHelpers.ReadMemory(cpu, memory, mode, out var address);
        value = (byte)((value - 1) & 0xFF);
        memory.Write(address, value);

        cpu.SetZNFlagsByValue(value);
    }

    internal static void OpINX(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.X = (byte)((cpu.X + 1) & 0xFF);
        cpu.SetZNFlagsByValue(cpu.X);
    }

    internal static void OpDEX(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.X = (byte)((cpu.X - 1) & 0xFF);
        cpu.SetZNFlagsByValue(cpu.X);
    }

    internal static void OpINY(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.Y = (byte)((cpu.Y + 1) & 0xFF);
        cpu.SetZNFlagsByValue(cpu.Y);
    }

    internal static void OpDEY(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.Y = (byte)((cpu.Y - 1) & 0xFF);
        cpu.SetZNFlagsByValue(cpu.Y);
    }
}
