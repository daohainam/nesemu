﻿using mini_6502.Components;

namespace mini_6502.Instructions;
internal class InstructionSet_System
{
    internal static void OpBRK(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;
        cpu.SetFlag(Flags.FLAG_BREAK, true);
        cpu.PC++;
        memory.Write((ushort)(0x0100 + cpu.SP--), (byte)((cpu.PC >> 8) & 0xFF)); // Push PC high byte
        memory.Write((ushort)(0x0100 + cpu.SP--), (byte)(cpu.PC & 0xFF)); // Push PC low byte
        memory.Write((ushort)(0x0100 + cpu.SP--), cpu.P); // Push processor status

        cpu.PC = (ushort)(memory.Read(0xFFFE) | (memory.Read(0xFFFF) << 8)); // Jump to interrupt vector
    }
    internal static void OpRTI(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;
        cpu.P = memory.Read((ushort)(0x0100 + ++cpu.SP)); // Pull processor status
        ushort pcLow = memory.Read((ushort)(0x0100 + ++cpu.SP));
        ushort pcHigh = memory.Read((ushort)(0x0100 + ++cpu.SP));
        cpu.PC = (ushort)((pcHigh << 8) | pcLow); // Pull PC
    }
    internal static void OpNOP(InstructionContext context)
    {
        // No operation, just consume a cycle
    }
}