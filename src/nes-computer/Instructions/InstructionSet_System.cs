namespace mini_6502.Instructions;

internal static class InstructionSet_System
{
    internal static void OpBRK(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;

        // Mark the break flag in P
        cpu.SetFlag(Flags.FLAG_BREAK, true);

        // BRK is a 2-byte instruction: increment PC to point to the next instruction
        cpu.PC++;

        // P is pushed with B=1, bit5=1
        byte statusToPush = (byte)(cpu.P | 0x20); // bit5 is always = 1

        // Push PC high, low, then status
        memory.Write((ushort)(0x0100 + cpu.SP--), (byte)((cpu.PC >> 8) & 0xFF));
        memory.Write((ushort)(0x0100 + cpu.SP--), (byte)(cpu.PC & 0xFF));
        memory.Write((ushort)(0x0100 + cpu.SP--), statusToPush);

        // Disable maskable interrupts
        cpu.SetFlag(Flags.FLAG_INTERRUPT, true);

        // Jump to IRQ/BRK vector
        cpu.PC = (ushort)(memory.Read(0xFFFE) | (memory.Read(0xFFFF) << 8));
    }

    internal static void OpRTI(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;

        byte status = memory.Read((ushort)(0x0100 + ++cpu.SP));
        // bit5 is always = 1
        status = (byte)(status | 0x20);
        cpu.P = status;

        ushort pcLow = memory.Read((ushort)(0x0100 + ++cpu.SP));
        ushort pcHigh = memory.Read((ushort)(0x0100 + ++cpu.SP));
        cpu.PC = (ushort)((pcHigh << 8) | pcLow);
    }

    internal static void OpNOP(InstructionContext context)
    {
        // Do nothing
    }
}
