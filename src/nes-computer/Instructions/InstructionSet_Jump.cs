using mini_6502.Components;

namespace mini_6502.Instructions;
internal class InstructionSet_Jump
{
    internal static void OpJMP(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        ushort address = InstructionHelpers.ReadMemory(cpu, memory, mode);
        cpu.PC = address;
    }

    internal static void OpJSR(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        ushort returnAddress = (ushort)(cpu.PC + 2); // JSR instruction length is 3 bytes
        memory.Write((ushort)(0x0100 + cpu.SP--), (byte)((returnAddress >> 8) & 0xFF)); 
        memory.Write((ushort)(0x0100 + cpu.SP--), (byte)(returnAddress & 0xFF)); 
        
        ushort address = InstructionHelpers.ReadMemory(cpu, memory, mode);
        cpu.PC = address;
    }

    internal static void OpRTS(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        ushort pcLow = memory.Read((ushort)(0x0100 + ++cpu.SP)); 
        ushort pcHigh = memory.Read((ushort)(0x0100 + ++cpu.SP)); 
        cpu.PC = (ushort)((pcHigh << 8) | pcLow); 
        
        cpu.PC++; 
    }
}