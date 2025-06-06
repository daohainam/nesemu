﻿using mini_6502.Components;

namespace mini_6502.Instructions;
internal class InstructionSet_LoadStore
{
    // https://www.masswerk.at/6502/6502_instruction_set.html
    internal static void OpLDA(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        switch (mode)
        {
            case AddressingMode.Immediate:
            case AddressingMode.ZeroPage:
            case AddressingMode.ZeroPageX:
            case AddressingMode.Absolute:
            case AddressingMode.AbsoluteX:
            case AddressingMode.AbsoluteY:
            case AddressingMode.IndirectX:
            case AddressingMode.IndirectY:
                byte value = InstructionHelpers.ReadMemory(cpu, memory, mode);
                cpu.A = value;
                cpu.SetZNFlagsByValue(value);
                break;
            default:
                Cpu.Panic($"LDA: Unsupported addressing mode: {mode}");
                break;
        }
    }

    internal static void OpLDX(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        switch (mode)
        {
            case AddressingMode.Immediate:
            case AddressingMode.ZeroPage:
            case AddressingMode.ZeroPageY:
            case AddressingMode.Absolute:
            case AddressingMode.AbsoluteY:
                byte value = InstructionHelpers.ReadMemory(cpu, memory, mode);
                cpu.X = value;
                cpu.SetZNFlagsByValue(value);
                break;
            default:
                Cpu.Panic($"LDX: Unsupported addressing mode: {mode}");
                break;
        }
    }

    internal static void OpLDY(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        switch (mode)
        {
            case AddressingMode.Immediate:
            case AddressingMode.ZeroPage:
            case AddressingMode.ZeroPageX:
            case AddressingMode.Absolute:
            case AddressingMode.AbsoluteX:
                byte value = InstructionHelpers.ReadMemory(cpu, memory, mode);
                cpu.Y = value;
                cpu.SetZNFlagsByValue(value);
                break;
            default:
                Cpu.Panic($"LDY: Unsupported addressing mode: {mode}");
                break;
        }
    }
    internal static void OpSTA(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        switch (mode)
        {
            case AddressingMode.ZeroPage:
            case AddressingMode.ZeroPageX:
            case AddressingMode.Absolute:
            case AddressingMode.AbsoluteX:
            case AddressingMode.AbsoluteY:
            case AddressingMode.IndirectX:
            case AddressingMode.IndirectY:
                InstructionHelpers.WriteMemory(cpu, memory, mode, cpu.A);
                break;
            default:
                Cpu.Panic($"STA: Unsupported addressing mode: {mode}");
                break;
        }
    }

    internal static void OpSTX(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        switch (mode)
        {
            case AddressingMode.ZeroPage:
            case AddressingMode.ZeroPageY:
            case AddressingMode.Absolute:
            case AddressingMode.AbsoluteY:
                InstructionHelpers.WriteMemory(cpu, memory, mode, cpu.X);
                break;
            default:
                Cpu.Panic($"STX: Unsupported addressing mode: {mode}");
                break;
        }
    }

    internal static void OpSTY(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        switch (mode)
        {
            case AddressingMode.ZeroPage:
            case AddressingMode.ZeroPageX:
            case AddressingMode.Absolute:
            case AddressingMode.AbsoluteX:
                InstructionHelpers.WriteMemory(cpu, memory, mode, cpu.Y);
                break;
            default:
                Cpu.Panic($"STY: Unsupported addressing mode: {mode}");
                break;
        }
    }


}
