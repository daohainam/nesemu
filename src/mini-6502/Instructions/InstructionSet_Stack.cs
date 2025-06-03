using mini_6502.Components;

namespace mini_6502.Instructions;
internal class InstructionSet_Stack
{
    internal static void OpPHA(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        memory.Write((ushort)(0x100 + cpu.SP--), cpu.A);
    }

    internal static void OpPHP(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        memory.Write((ushort)(0x100 + cpu.SP--), cpu.P);
    }

    internal static void OpPLA(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.A = memory.Read((ushort)(0x100 + ++cpu.SP));
        cpu.SetFlagsByValue(cpu.A);
    }

    internal static void OpPLP(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        cpu.P = memory.Read((ushort)(0x100 + ++cpu.SP));
    }
}
