﻿using mini_6502.Components;

namespace mini_6502.Instructions;
internal class InstructionSet_RegisterTransfer
{
    internal static void OpTAX(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.X = cpu.A;
        cpu.SetZNFlagsByValue(cpu.X);
    }

    internal static void OpTAY(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.Y = cpu.A;
        cpu.SetZNFlagsByValue(cpu.Y);
    }

    internal static void OpTXA(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.A = cpu.X;
        cpu.SetZNFlagsByValue(cpu.A);
    }

    internal static void OpTYA(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.A = cpu.Y;
        cpu.SetZNFlagsByValue(cpu.A);
    }
    internal static void OpTSX(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.X = cpu.SP;
        cpu.SetZNFlagsByValue(cpu.X);
    }
    internal static void OpTXS(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.SP = cpu.X;
    }
}
