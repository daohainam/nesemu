using mini_6502.Components;

namespace mini_6502;
internal partial class InstructionSet
{
    private static void OpLDA(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        byte value = mode switch
        {
            AddressingMode.Immediate => memory.Read(cpu.PC++),
            AddressingMode.ZeroPage => memory.Read(memory.Read(cpu.PC++)),
            AddressingMode.ZeroPageX => memory.Read((byte)(memory.Read(cpu.PC++) + cpu.X)),
            AddressingMode.Absolute => memory.Read((ushort)(memory.Read(cpu.PC++) | (ushort)(memory.Read(cpu.PC++) << 8))),
            AddressingMode.AbsoluteX => memory.Read((ushort)(memory.Read(cpu.PC++) | (ushort)(memory.Read(cpu.PC++) << 8) + cpu.X)),
            _ => throw new InvalidOperationException($"Unsupported addressing mode: {mode}")
        };
        cpu.A = value;
        cpu.SetFlag(Flags.FLAG_ZERO, value == 0);
        cpu.SetFlag(Flags.FLAG_NEGATIVE, (value & 0x80) != 0);
    }

}
