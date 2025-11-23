using mini_6502.Components;

namespace mini_6502.Instructions;

internal static class InstructionHelpers
{
    internal static ushort ReadMemory(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        return ReadMemory(cpu, memory, mode, out _);
    }

    internal static ushort ReadMemory(Cpu cpu, IMemory memory, AddressingMode mode, out ushort address)
    {
        switch (mode)
        {
            case AddressingMode.Immediate:
                {
                    address = 0;
                    return memory.Read(cpu.PC++);
                }

            case AddressingMode.ZeroPage:
                {
                    byte zp = memory.Read(cpu.PC++);
                    address = zp;
                    return memory.Read(address);
                }

            case AddressingMode.ZeroPageX:
                {
                    byte zp = memory.Read(cpu.PC++);
                    address = (byte)(zp + cpu.X);
                    return memory.Read(address);
                }

            case AddressingMode.ZeroPageY:
                {
                    byte zp = memory.Read(cpu.PC++);
                    address = (byte)(zp + cpu.Y);
                    return memory.Read(address);
                }

            case AddressingMode.Absolute:
                {
                    ushort baseAddr = Read16(memory, ref cpu.PC);
                    address = baseAddr;
                    return memory.Read(address);
                }

            case AddressingMode.AbsoluteX:
                {
                    ushort baseAddr = Read16(memory, ref cpu.PC);
                    address = (ushort)(baseAddr + cpu.X);
                    return memory.Read(address);
                }

            case AddressingMode.AbsoluteY:
                {
                    ushort baseAddr = Read16(memory, ref cpu.PC);
                    address = (ushort)(baseAddr + cpu.Y);
                    return memory.Read(address);
                }

            case AddressingMode.IndirectX:
                {
                    byte zp = memory.Read(cpu.PC++);
                    byte ptr = (byte)(zp + cpu.X);
                    byte lo = memory.Read(ptr);
                    byte hi = memory.Read((byte)(ptr + 1));
                    address = (ushort)(lo | (hi << 8));
                    return memory.Read(address);
                }

            case AddressingMode.IndirectY:
                {
                    byte zp = memory.Read(cpu.PC++);
                    byte lo = memory.Read(zp);
                    byte hi = memory.Read((byte)(zp + 1));
                    ushort baseAddr = (ushort)(lo | (hi << 8));
                    address = (ushort)(baseAddr + cpu.Y);
                    return memory.Read(address);
                }

            case AddressingMode.Accumulator:
            case AddressingMode.Implied:
            case AddressingMode.Relative:
            default:
                Cpu.Panic($"ReadMemory: unsupported addressing mode {mode}");
                address = 0;
                return 0;
        }
    }

    internal static void WriteMemory(Cpu cpu, IMemory memory, AddressingMode mode, byte value)
    {
        switch (mode)
        {
            case AddressingMode.ZeroPage:
                {
                    byte zp = memory.Read(cpu.PC++);
                    memory.Write(zp, value);
                    break;
                }

            case AddressingMode.ZeroPageX:
                {
                    byte zp = memory.Read(cpu.PC++);
                    byte addr = (byte)(zp + cpu.X);
                    memory.Write(addr, value);
                    break;
                }

            case AddressingMode.ZeroPageY:
                {
                    byte zp = memory.Read(cpu.PC++);
                    byte addr = (byte)(zp + cpu.Y);
                    memory.Write(addr, value);
                    break;
                }

            case AddressingMode.Absolute:
                {
                    ushort addr = Read16(memory, ref cpu.PC);
                    memory.Write(addr, value);
                    break;
                }

            case AddressingMode.AbsoluteX:
                {
                    ushort baseAddr = Read16(memory, ref cpu.PC);
                    ushort addr = (ushort)(baseAddr + cpu.X);
                    memory.Write(addr, value);
                    break;
                }

            case AddressingMode.AbsoluteY:
                {
                    ushort baseAddr = Read16(memory, ref cpu.PC);
                    ushort addr = (ushort)(baseAddr + cpu.Y);
                    memory.Write(addr, value);
                    break;
                }

            case AddressingMode.IndirectX:
                {
                    byte zp = memory.Read(cpu.PC++);
                    byte ptr = (byte)(zp + cpu.X);
                    byte lo = memory.Read(ptr);
                    byte hi = memory.Read((byte)(ptr + 1));
                    ushort addr = (ushort)(lo | (hi << 8));
                    memory.Write(addr, value);
                    break;
                }

            case AddressingMode.IndirectY:
                {
                    byte zp = memory.Read(cpu.PC++);
                    byte lo = memory.Read(zp);
                    byte hi = memory.Read((byte)(zp + 1));
                    ushort baseAddr = (ushort)(lo | (hi << 8));
                    ushort addr = (ushort)(baseAddr + cpu.Y);
                    memory.Write(addr, value);
                    break;
                }

            case AddressingMode.Immediate:
            case AddressingMode.Accumulator:
            case AddressingMode.Implied:
            case AddressingMode.Relative:
            default:
                Cpu.Panic($"WriteMemory: unsupported addressing mode {mode}");
                break;
        }
    }

    internal static ushort ReadAddress(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        switch (mode)
        {
            case AddressingMode.Absolute:
                return Read16(memory, ref cpu.PC);

            case AddressingMode.Indirect:
                {
                    ushort ptr = Read16(memory, ref cpu.PC);
                    byte lo = memory.Read(ptr);
                    ushort hiAddr = (ushort)((ptr & 0xFF00) | ((ptr + 1) & 0x00FF));
                    byte hi = memory.Read(hiAddr);
                    return (ushort)(lo | (hi << 8));
                }

            default:
                Cpu.Panic($"ReadAddress: unsupported addressing mode {mode}");
                return 0;
        }
    }

    private static ushort Read16(IMemory memory, ref ushort pc)
    {
        byte lo = memory.Read(pc++);
        byte hi = memory.Read(pc++);
        return (ushort)(lo | (hi << 8));
    }
}
