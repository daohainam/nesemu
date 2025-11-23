using Microsoft.Extensions.Logging;
using mini_6502;
using mini_6502.Components;

namespace mini_6502.Instructions;

internal static class InstructionSet_Jump
{
    internal static void OpJMP(InstructionContext context)
    {
        Cpu cpu = context.Cpu;
        IMemory memory = context.Memory;

        ushort address = InstructionHelpers.ReadAddress(cpu, memory, context.Mode);
        cpu.PC = address;

        if (context.Logger.IsEnabled(LogLevel.Debug))
        {
            context.Logger.LogDebug("JMP to 0x{address:X4}", address);
        }
    }

    internal static void OpJSR(InstructionContext context)
    {
        Cpu cpu = context.Cpu;
        IMemory memory = context.Memory;

        // PC is currently pointing at the low-byte operand (I+1)
        // JSR pushes the address of the last byte of the instruction (I+2) = PC + 1
        ushort returnAddress = (ushort)(cpu.PC + 1);

        // Push high byte, then low byte
        memory.Write((ushort)(0x0100 + cpu.SP--), (byte)((returnAddress >> 8) & 0xFF));
        memory.Write((ushort)(0x0100 + cpu.SP--), (byte)(returnAddress & 0xFF));

        ushort address = InstructionHelpers.ReadAddress(cpu, memory, context.Mode);
        cpu.PC = address;
    }

    internal static void OpRTS(InstructionContext context)
    {
        Cpu cpu = context.Cpu;
        IMemory memory = context.Memory;

        ushort pcLow = memory.Read((ushort)(0x0100 + ++cpu.SP));
        ushort pcHigh = memory.Read((ushort)(0x0100 + ++cpu.SP));
        cpu.PC = (ushort)((pcHigh << 8) | pcLow);

        cpu.PC++;
    }
}
