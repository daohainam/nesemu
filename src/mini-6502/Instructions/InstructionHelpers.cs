using mini_6502.Components;

namespace mini_6502.Instructions;
internal class InstructionHelpers
{
    internal static byte ReadMemory(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        switch (mode)
        {
            case AddressingMode.Immediate:
                return memory.Read(cpu.PC++);
            case AddressingMode.ZeroPage:
                return memory.Read(memory.Read(cpu.PC++));
            case AddressingMode.ZeroPageX:
                return memory.Read((byte)(memory.Read(cpu.PC++) + cpu.X));
            case AddressingMode.ZeroPageY:
                return memory.Read((byte)(memory.Read(cpu.PC++) + cpu.Y));
            case AddressingMode.Absolute:
                ushort address = (ushort)(memory.Read(cpu.PC++) | (memory.Read(cpu.PC++) << 8));
                return memory.Read(address);
            case AddressingMode.AbsoluteX:
                address = (ushort)(memory.Read(cpu.PC++) | (memory.Read(cpu.PC++) << 8));
                return memory.Read((ushort)(address + cpu.X));
            case AddressingMode.AbsoluteY:
                address = (ushort)(memory.Read(cpu.PC++) | (memory.Read(cpu.PC++) << 8));
                return memory.Read((ushort)(address + cpu.Y));
            case AddressingMode.IndirectX:
                byte zpAddress = memory.Read(cpu.PC++);
                ushort indirectAddress = (ushort)(zpAddress + cpu.X);
                ushort addressLow = memory.Read(indirectAddress);
                ushort addressHigh = memory.Read((ushort)((indirectAddress + 1) & 0xFF)); // Wrap around for zero page
                return memory.Read((ushort)(addressHigh << 8 | addressLow));
            case AddressingMode.IndirectY:
                zpAddress = memory.Read(cpu.PC++);
                indirectAddress = (ushort)(zpAddress + cpu.Y);
                addressLow = memory.Read(indirectAddress);
                addressHigh = memory.Read((ushort)((indirectAddress + 1) & 0xFF)); // Wrap around for zero page
                return memory.Read((ushort)(addressHigh << 8 | addressLow));
            default:
                Cpu.Panic($"Unsupported addressing mode: {mode}");
                return 0; // Unreachable, but required to satisfy the compiler
        }
    }
    internal static void WriteMemory(Cpu cpu, IMemory memory, AddressingMode mode, byte value)
    {
        switch (mode)
        {
            case AddressingMode.Immediate:
                Cpu.Panic("Cannot write to Immediate mode.");
                break;
            case AddressingMode.ZeroPage:
                byte zpAddress = memory.Read(cpu.PC++);
                memory.Write(zpAddress, value);
                break;
            case AddressingMode.ZeroPageX:
                zpAddress = (byte)(memory.Read(cpu.PC++) + cpu.X);
                memory.Write(zpAddress, value);
                break;
            case AddressingMode.ZeroPageY:
                zpAddress = (byte)(memory.Read(cpu.PC++) + cpu.Y);
                memory.Write(zpAddress, value);
                break;
            case AddressingMode.Absolute:
                ushort address = (ushort)(memory.Read(cpu.PC++) | (memory.Read(cpu.PC++) << 8));
                memory.Write(address, value);
                break;
            case AddressingMode.AbsoluteX:
                address = (ushort)(memory.Read(cpu.PC++) | (memory.Read(cpu.PC++) << 8));
                memory.Write((ushort)(address + cpu.X), value);
                break;
            case AddressingMode.AbsoluteY:
                address = (ushort)(memory.Read(cpu.PC++) | (memory.Read(cpu.PC++) << 8));
                memory.Write((ushort)(address + cpu.Y), value);
                break;
            case AddressingMode.IndirectX:
                zpAddress = memory.Read(cpu.PC++);
                ushort indirectAddress = (ushort)(zpAddress + cpu.X);
                ushort addressLow = memory.Read(indirectAddress);
                ushort addressHigh = memory.Read((ushort)((indirectAddress + 1) & 0xFF)); // Wrap around for zero page
                memory.Write((ushort)(addressHigh << 8 | addressLow), value);
                break;
            case AddressingMode.IndirectY:
                zpAddress = memory.Read(cpu.PC++);
                indirectAddress = (ushort)(zpAddress + cpu.Y);
                addressLow = memory.Read(indirectAddress);
                addressHigh = memory.Read((ushort)((indirectAddress + 1) & 0xFF)); // Wrap around for zero page
                memory.Write((ushort)(addressHigh << 8 | addressLow), value);
                break; 
            default:
                Cpu.Panic($"Unsupported addressing mode: {mode}");
                break;
        }

    }

}
