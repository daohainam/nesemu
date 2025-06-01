using mini_6502.Components;

namespace mini_6502;

internal class Instruction(string mnemonic, AddressingMode mode, int cycles, int numberOfBytes,
                   Action<Cpu, IMemory, AddressingMode> execute)
{
    public string Mnemonic => mnemonic;
    public AddressingMode Mode => mode;
    public int Cycles => cycles;
    public int NumberOfBytes => numberOfBytes;
    public Action<Cpu, IMemory, AddressingMode> Execute => execute;
}