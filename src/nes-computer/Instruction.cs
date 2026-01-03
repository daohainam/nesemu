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

internal class InstructionContext
{
    public Cpu Cpu { get; internal set; } = default!;
    public IMemory Memory { get; internal set; } = default!;
    public AddressingMode Mode { get; internal set; } = default!;
    public ILogger<Cpu> Logger { get; internal set; } = default!;

    internal InstructionContext() { }

    internal InstructionContext(Cpu cpu, IMemory memory, AddressingMode mode, ILogger logger)
    {
        Initialize(cpu, memory, mode, logger);
    }

    public void Initialize(Cpu cpu, IMemory memory, AddressingMode mode, ILogger logger)
    {
        Cpu = cpu ?? throw new ArgumentNullException(nameof(cpu));
        Memory = memory ?? throw new ArgumentNullException(nameof(memory));
        Mode = mode;
        Logger = logger as ILogger<Cpu> ?? throw new ArgumentNullException(nameof(logger));
    }

    public void Reset()
    {
        Cpu = default!;
        Memory = default!;
        Mode = AddressingMode.Implied;
        Logger = default!;
    }
}