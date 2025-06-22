using Microsoft.Extensions.Logging;
using mini_6502.Components;

namespace mini_6502;

internal class Instruction(string mnemonic, AddressingMode mode, int cycles, int numberOfBytes,
                   Action<InstructionContext> execute)
{
    public string Mnemonic => mnemonic;
    public AddressingMode Mode => mode;
    public int Cycles => cycles;
    public int NumberOfBytes => numberOfBytes;
    public Action<InstructionContext> Execute => execute;

    public override string ToString()
    {
        return $"{Mnemonic} ({Mode}) - {Cycles} cycles, {NumberOfBytes} bytes";
    }
}

internal class InstructionContext(Cpu cpu, IMemory memory, AddressingMode mode, ILogger<Cpu> logger)
{
    public Cpu Cpu { get; } = cpu ?? throw new ArgumentNullException(nameof(cpu));
    public IMemory Memory { get; } = memory ?? throw new ArgumentNullException(nameof(memory));
    public AddressingMode Mode { get; } = mode;
    public ILogger<Cpu> Logger { get; } = logger ?? throw new ArgumentNullException(nameof(logger));
}