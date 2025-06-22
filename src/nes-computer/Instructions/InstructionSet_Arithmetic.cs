using mini_6502.Components;

namespace mini_6502.Instructions;
internal class InstructionSet_Arithmetic
{
    internal static void OpADC(InstructionContext context)
    {
        byte value = (byte)InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode);
        ushort result = (ushort)(context.Cpu.A + value + (context.Cpu.GetFlag(Flags.FLAG_CARRY) ? 1 : 0));

        context.Cpu.SetFlag(Flags.FLAG_CARRY, result > 0xFF);
        context.Cpu.SetZNFlagsByValue((byte)(result & 0xFF));
        context.Cpu.SetFlag(Flags.FLAG_OVERFLOW, ((context.Cpu.A ^ value) & 0x80) == 0 && ((context.Cpu.A ^ result) & 0x80) != 0);

        context.Cpu.A = (byte)(result & 0xFF);
    }

    internal static void OpSBC(InstructionContext context)
    {
        byte value = (byte)InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode);
        ushort result = (ushort)(context.Cpu.A - value - (context.Cpu.GetFlag(Flags.FLAG_CARRY) ? 0 : 1));

        context.Cpu.SetFlag(Flags.FLAG_CARRY, result < 0x100);
        context.Cpu.SetZNFlagsByValue((byte)(result & 0xFF));
        context.Cpu.SetFlag(Flags.FLAG_OVERFLOW, ((context.Cpu.A ^ value) & 0x80) != 0 && ((context.Cpu.A ^ result) & 0x80) != 0);

        context.Cpu.A = (byte)(result & 0xFF);
    }

    internal static void OpCMP(InstructionContext context)
    {
        byte value = (byte)InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode);
        ushort result = (ushort)(context.Cpu.A - value);

        context.Cpu.SetFlag(Flags.FLAG_CARRY, context.Cpu.A >= value);
        context.Cpu.SetZNFlagsByValue((byte)(result & 0xFF));
        context.Cpu.SetFlag(Flags.FLAG_OVERFLOW, ((context.Cpu.A ^ value) & 0x80) != 0 && ((context.Cpu.A ^ result) & 0x80) != 0);
    }

    internal static void OpCPX(InstructionContext context)
    {
        byte value = (byte)InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode);
        ushort result = (ushort)(context.Cpu.X - value);

        context.Cpu.SetFlag(Flags.FLAG_CARRY, context.Cpu.X >= value);
        context.Cpu.SetZNFlagsByValue((byte)(result & 0xFF));
        context.Cpu.SetFlag(Flags.FLAG_OVERFLOW, ((context.Cpu.X ^ value) & 0x80) != 0 && ((context.Cpu.X ^ result) & 0x80) != 0);
    }

    internal static void OpCPY(InstructionContext context)
    {
        byte value = (byte)InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode);
        ushort result = (ushort)(context.Cpu.Y - value);

        context.Cpu.SetFlag(Flags.FLAG_CARRY, context.Cpu.Y >= value);
        context.Cpu.SetZNFlagsByValue((byte)(result & 0xFF));
        context.Cpu.SetFlag(Flags.FLAG_OVERFLOW, ((context.Cpu.Y ^ value) & 0x80) != 0 && ((context.Cpu.Y ^ result) & 0x80) != 0);
    }

    internal static void OpINC(InstructionContext context)
    {
        byte value = (byte)InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode, out var address);
        value = (byte)((value + 1) & 0xFF);
        context.Memory.Write(address, value);

        context.Cpu.SetZNFlagsByValue(value);
    }

    internal static void OpDEC(InstructionContext context)
    {
        byte value = (byte)InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode, out var address);
        value = (byte)((value - 1) & 0xFF);
        context.Memory.Write(address, value);

        context.Cpu.SetZNFlagsByValue(value);
    }

    internal static void OpINX(InstructionContext context)
    {
        context.Cpu.X = (byte)((context.Cpu.X + 1) & 0xFF);
        context.Cpu.SetZNFlagsByValue(context.Cpu.X);
    }

    internal static void OpDEX(InstructionContext context)
    {
        context.Cpu.X = (byte)((context.Cpu.X - 1) & 0xFF);
        context.Cpu.SetZNFlagsByValue(context.Cpu.X);
    }

    internal static void OpINY(InstructionContext context)
    {
        context.Cpu.Y = (byte)((context.Cpu.Y + 1) & 0xFF);
        context.Cpu.SetZNFlagsByValue(context.Cpu.Y);
    }

    internal static void OpDEY(InstructionContext context)
    {
        context.Cpu.Y = (byte)((context.Cpu.Y - 1) & 0xFF);
        context.Cpu.SetZNFlagsByValue(context.Cpu.Y);
    }

    internal static void OpASL(InstructionContext context)
    {
        if (context.Mode == AddressingMode.Accumulator)
        {
            context.Cpu.SetFlag(Flags.FLAG_CARRY, (context.Cpu.A & 0x80) != 0);
            context.Cpu.A = (byte)((context.Cpu.A << 1) & 0xFF);
            context.Cpu.SetZNFlagsByValue(context.Cpu.A);
        }
        else
        {
            byte value = (byte)InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode, out var address);
            context.Cpu.SetFlag(Flags.FLAG_CARRY, (value & 0x80) != 0);
            value = (byte)((value << 1) & 0xFF);
            context.Memory.Write(address, value);

            context.Cpu.SetZNFlagsByValue(value);
        }
    }

    internal static void OpLSR(InstructionContext context)
    {
        if (context.Mode == AddressingMode.Accumulator)
        {
            context.Cpu.SetFlag(Flags.FLAG_CARRY, (context.Cpu.A & 0x01) != 0);
            context.Cpu.A = (byte)(context.Cpu.A >> 1);
            context.Cpu.SetZNFlagsByValue(context.Cpu.A);
        }
        else
        {
            byte value = (byte)InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode, out var address);
            context.Cpu.SetFlag(Flags.FLAG_CARRY, (value & 0x01) != 0);
            value = (byte)(value >> 1);
            context.Memory.Write(address, value);
            context.Cpu.SetZNFlagsByValue(value);
        }
    }

    internal static void OpROL(InstructionContext context)
    {
        if (context.Mode == AddressingMode.Accumulator)
        {
            byte carry = (byte)(context.Cpu.GetFlag(Flags.FLAG_CARRY) ? 1 : 0);
            context.Cpu.SetFlag(Flags.FLAG_CARRY, (context.Cpu.A & 0x80) != 0);
            context.Cpu.A = (byte)(((context.Cpu.A << 1) | carry) & 0xFF);
            context.Cpu.SetZNFlagsByValue(context.Cpu.A);
        }
        else
        {
            byte value = (byte)InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode, out var address);
            byte carry = (byte)(context.Cpu.GetFlag(Flags.FLAG_CARRY) ? 1 : 0);
            context.Cpu.SetFlag(Flags.FLAG_CARRY, (value & 0x80) != 0);
            value = (byte)(((value << 1) | carry) & 0xFF);
            context.Memory.Write(address, value);
            context.Cpu.SetZNFlagsByValue(value);
        }
    }

    internal static void OpROR(InstructionContext context)
    {
        if (context.Mode == AddressingMode.Accumulator)
        {
            byte carry = (byte)(context.Cpu.GetFlag(Flags.FLAG_CARRY) ? 0x80 : 0);
            context.Cpu.SetFlag(Flags.FLAG_CARRY, (context.Cpu.A & 0x01) != 0);
            context.Cpu.A = (byte)((context.Cpu.A >> 1) | carry);
            context.Cpu.SetZNFlagsByValue(context.Cpu.A);
        }
        else
        {
            byte value = (byte)InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode, out var address);
            byte carry = (byte)(context.Cpu.GetFlag(Flags.FLAG_CARRY) ? 0x80 : 0);
            context.Cpu.SetFlag(Flags.FLAG_CARRY, (value & 0x01) != 0);
            value = (byte)((value >> 1) | carry);
            context.Memory.Write(address, value);
            context.Cpu.SetZNFlagsByValue(value);
        }
    }
}
