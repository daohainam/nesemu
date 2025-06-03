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
}
