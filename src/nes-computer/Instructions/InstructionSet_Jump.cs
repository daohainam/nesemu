using mini_6502.Components;

namespace mini_6502.Instructions;
internal class InstructionSet_Jump
{
    internal static void OpJMP(InstructionContext context)
    {
        ushort address = InstructionHelpers.ReadMemory(context.Cpu, context.Memory, context.Mode);
        context.Cpu.PC = address;
    }

    internal static void OpJSR(InstructionContext context)
    {
        Cpu cpu = context.Cpu;
        IMemory memory = context.Memory;
        AddressingMode mode = context.Mode;

        ushort returnAddress = (ushort)(cpu.PC + 2); // JSR instruction length is 3 bytes
        memory.Write((ushort)(0x0100 + cpu.SP--), (byte)((returnAddress >> 8) & 0xFF));
        memory.Write((ushort)(0x0100 + cpu.SP--), (byte)(returnAddress & 0xFF));

        ushort address = InstructionHelpers.ReadMemory(cpu, memory, mode);
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