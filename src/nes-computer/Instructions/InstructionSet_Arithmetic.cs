using mini_6502;
using mini_6502.Components;

namespace mini_6502.Instructions;

internal class InstructionSet_Arithmetic
{
    // ===== ADC / SBC =====

    internal static void OpADC(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;

        byte value = (byte)InstructionHelpers.ReadMemory(cpu, memory, context.Mode);

        int a = cpu.A;
        int v = value;
        int carryIn = cpu.GetFlag(Flags.FLAG_CARRY) ? 1 : 0;

        int sum = a + v + carryIn;
        byte result = (byte)sum;

        cpu.SetFlag(Flags.FLAG_CARRY, sum > 0xFF);
        cpu.SetZNFlagsByValue(result);
        cpu.SetFlag(Flags.FLAG_OVERFLOW, (~(a ^ v) & (a ^ result) & 0x80) != 0);

        cpu.A = result;
    }

    internal static void OpSBC(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;

        byte value = (byte)InstructionHelpers.ReadMemory(cpu, memory, context.Mode);
        byte inverted = (byte)~value;

        int a = cpu.A;
        int v = inverted;
        int carryIn = cpu.GetFlag(Flags.FLAG_CARRY) ? 1 : 0;

        int sum = a + v + carryIn;
        byte result = (byte)sum;

        cpu.SetFlag(Flags.FLAG_CARRY, sum > 0xFF);
        cpu.SetZNFlagsByValue(result);
        cpu.SetFlag(Flags.FLAG_OVERFLOW, (~(a ^ v) & (a ^ result) & 0x80) != 0);

        cpu.A = result;
    }

    // ===== INC / DEC (memory) =====

    internal static void OpINC(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;

        byte value = (byte)InstructionHelpers.ReadMemory(cpu, memory, context.Mode, out ushort address);
        value++;
        memory.Write(address, value);
        cpu.SetZNFlagsByValue(value);
    }

    internal static void OpDEC(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;

        byte value = (byte)InstructionHelpers.ReadMemory(cpu, memory, context.Mode, out ushort address);
        value--;
        memory.Write(address, value);
        cpu.SetZNFlagsByValue(value);
    }

    // ===== INX / INY / DEX / DEY (register) =====

    internal static void OpINX(InstructionContext context)
    {
        var cpu = context.Cpu;
        cpu.X++;
        cpu.SetZNFlagsByValue(cpu.X);
    }

    internal static void OpINY(InstructionContext context)
    {
        var cpu = context.Cpu;
        cpu.Y++;
        cpu.SetZNFlagsByValue(cpu.Y);
    }

    internal static void OpDEX(InstructionContext context)
    {
        var cpu = context.Cpu;
        cpu.X--;
        cpu.SetZNFlagsByValue(cpu.X);
    }

    internal static void OpDEY(InstructionContext context)
    {
        var cpu = context.Cpu;
        cpu.Y--;
        cpu.SetZNFlagsByValue(cpu.Y);
    }

    // ===== ASL / LSR / ROL / ROR =====

    internal static void OpASL(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;

        if (context.Mode == AddressingMode.Accumulator)
        {
            byte value = cpu.A;
            cpu.SetFlag(Flags.FLAG_CARRY, (value & 0x80) != 0);
            value = (byte)(value << 1);
            cpu.A = value;
            cpu.SetZNFlagsByValue(value);
        }
        else
        {
            byte value = (byte)InstructionHelpers.ReadMemory(cpu, memory, context.Mode, out ushort address);
            cpu.SetFlag(Flags.FLAG_CARRY, (value & 0x80) != 0);
            value = (byte)(value << 1);
            memory.Write(address, value);
            cpu.SetZNFlagsByValue(value);
        }
    }

    internal static void OpLSR(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;

        if (context.Mode == AddressingMode.Accumulator)
        {
            byte value = cpu.A;
            cpu.SetFlag(Flags.FLAG_CARRY, (value & 0x01) != 0);
            value = (byte)(value >> 1);
            cpu.A = value;
            cpu.SetZNFlagsByValue(value);
        }
        else
        {
            byte value = (byte)InstructionHelpers.ReadMemory(cpu, memory, context.Mode, out ushort address);
            cpu.SetFlag(Flags.FLAG_CARRY, (value & 0x01) != 0);
            value = (byte)(value >> 1);
            memory.Write(address, value);
            cpu.SetZNFlagsByValue(value);
        }
    }

    internal static void OpROL(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;
        bool oldCarry = cpu.GetFlag(Flags.FLAG_CARRY);

        if (context.Mode == AddressingMode.Accumulator)
        {
            byte value = cpu.A;
            cpu.SetFlag(Flags.FLAG_CARRY, (value & 0x80) != 0);
            value = (byte)(value << 1);
            if (oldCarry)
                value |= 0x01;
            cpu.A = value;
            cpu.SetZNFlagsByValue(value);
        }
        else
        {
            byte value = (byte)InstructionHelpers.ReadMemory(cpu, memory, context.Mode, out ushort address);
            cpu.SetFlag(Flags.FLAG_CARRY, (value & 0x80) != 0);
            value = (byte)(value << 1);
            if (oldCarry)
                value |= 0x01;
            memory.Write(address, value);
            cpu.SetZNFlagsByValue(value);
        }
    }

    internal static void OpROR(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;
        bool oldCarry = cpu.GetFlag(Flags.FLAG_CARRY);

        if (context.Mode == AddressingMode.Accumulator)
        {
            byte value = cpu.A;
            cpu.SetFlag(Flags.FLAG_CARRY, (value & 0x01) != 0);
            value = (byte)(value >> 1);
            if (oldCarry)
                value |= 0x80;
            cpu.A = value;
            cpu.SetZNFlagsByValue(value);
        }
        else
        {
            byte value = (byte)InstructionHelpers.ReadMemory(cpu, memory, context.Mode, out ushort address);
            cpu.SetFlag(Flags.FLAG_CARRY, (value & 0x01) != 0);
            value = (byte)(value >> 1);
            if (oldCarry)
                value |= 0x80;
            memory.Write(address, value);
            cpu.SetZNFlagsByValue(value);
        }
    }

    // ===== CMP / CPX / CPY =====

    internal static void OpCMP(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;

        byte value = (byte)InstructionHelpers.ReadMemory(cpu, memory, context.Mode);
        ushort result = (ushort)(cpu.A - value);

        cpu.SetFlag(Flags.FLAG_CARRY, cpu.A >= value);
        cpu.SetZNFlagsByValue((byte)(result & 0xFF));
        // V không bị ảnh hưởng
    }

    internal static void OpCPX(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;

        byte value = (byte)InstructionHelpers.ReadMemory(cpu, memory, context.Mode);
        ushort result = (ushort)(cpu.X - value);

        cpu.SetFlag(Flags.FLAG_CARRY, cpu.X >= value);
        cpu.SetZNFlagsByValue((byte)(result & 0xFF));
        // V không bị ảnh hưởng
    }

    internal static void OpCPY(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;

        byte value = (byte)InstructionHelpers.ReadMemory(cpu, memory, context.Mode);
        ushort result = (ushort)(cpu.Y - value);

        cpu.SetFlag(Flags.FLAG_CARRY, cpu.Y >= value);
        cpu.SetZNFlagsByValue((byte)(result & 0xFF));
        // V không bị ảnh hưởng
    }
}
