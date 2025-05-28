namespace mini_6502;

internal class Instruction(string mnemonic, AddressingMode mode, int cycles,
                   Action<Cpu, AddressingMode> execute)
{
    public string Mnemonic => mnemonic;
    public AddressingMode Mode => mode;
    public int Cycles => cycles;
    public Action<Cpu, AddressingMode> Execute => execute;
}