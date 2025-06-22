using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace mini_6502.Components;
internal class Cpu : IDebugable
{
    // Registers
    public byte A; // Accumulator
    public byte X; // Index register X
    public byte Y; // Index register Y
    public byte SP; // Stack Pointer (chỉ offset trong vùng 0x0100–0x01FF)
    public ushort PC; // Program Counter (địa chỉ lệnh tiếp theo)
    public byte P; // Processor Status Register

    private readonly IMemory memory;
    private readonly ILogger<Cpu> logger;
    private int cycles;

    public Cpu(IMemory memory, ILogger<Cpu>? logger = null)
    {
        this.memory = memory ?? throw new ArgumentNullException(nameof(memory));
        this.logger = logger ?? NullLogger<Cpu>.Instance;

        Reset();
    }

    public void Reset()
    {
        A = X = Y = 0;
        SP = 0xFD;
        P = 0b0011_0100;
        PC = (ushort)(memory.Read(0xFFFC) | memory.Read(0xFFFD) << 8);
        cycles = 7;
    }

    public void Clock()
    {
        if (cycles == 0)
        {
            byte opcode = memory.Read(PC++);
            Instruction instr = InstructionSet.Instance[opcode];

            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("Executing instruction: {mnemonic} at PC={opc:X4}, A={a:X2}, X={x:X2}, Y={y:X2}, SP={sp:X2}, P={p:X2}",
                    instr.Mnemonic, PC - 1, A, X, Y, SP, P);
            }

            var context = new InstructionContext(this, memory, instr.Mode, logger);
            instr.Execute(context);
            cycles = instr.Cycles;
        }
        cycles--;
    }

    public void SetFlag(byte flag, bool value)
    {
        if (value)
            P |= flag;
        else
            P &= (byte)~flag;
    }

    public bool GetFlag(byte flag) => (P & flag) != 0;
    internal int Cycles
    {
        get => cycles;
        set => cycles = value;
    }
    public string Dump()
    {
        return $"A={A:X2} X={X:X2} Y={Y:X2} SP={SP:X2} PC={PC:X4} P=[N:{GetFlag(Flags.FLAG_NEGATIVE)} V:{GetFlag(Flags.FLAG_OVERFLOW)} -:{(P & 0x20) != 0} B:{GetFlag(Flags.FLAG_BREAK)} D:{GetFlag(Flags.FLAG_DECIMAL)} I:{GetFlag(Flags.FLAG_INTERRUPT)} Z:{GetFlag(Flags.FLAG_ZERO)} C:{GetFlag(Flags.FLAG_CARRY)}]";
    }

    public static void Panic(string message)
    {
        throw new PanicException(message);
    }
}