namespace mini_6502;
internal class Cpu
{
    // Registers
    public byte A, X, Y, SP;
    public ushort PC;
    public byte P; // Status

    // Flags
    private const byte FLAG_C = 1 << 0;
    private const byte FLAG_Z = 1 << 1;
    private const byte FLAG_I = 1 << 2;
    private const byte FLAG_D = 1 << 3;
    private const byte FLAG_B = 1 << 4;
    private const byte FLAG_U = 1 << 5; // Unused
    private const byte FLAG_V = 1 << 6;
    private const byte FLAG_N = 1 << 7;

    private readonly IMemorySpace memory;
    private int cycles;
    private readonly Instruction[] instructions = new Instruction[256];

    public Cpu(IMemorySpace memory)
    {
        this.memory = memory;
        Reset();
        InitInstructions();
    }

    public void Reset()
    {
        A = X = Y = 0;
        SP = 0xFD;
        P = 0x24;
        PC = (ushort)(memory.Read(0xFFFC) | (memory.Read(0xFFFD) << 8));
        cycles = 0;
    }

    public void Clock()
    {
        if (cycles == 0)
        {
            byte opcode = memory.Read(PC++);
            Instruction instr = instructions[opcode];
            instr.Execute(this, instr.Mode);
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

    private void InitInstructions()
    {
        instructions[0xA9] = new Instruction("LDA", AddressingMode.Immediate, 2, OpLDA);
        instructions[0xA5] = new Instruction("LDA", AddressingMode.ZeroPage, 3, OpLDA);
    }

    private ushort GetAddress(AddressingMode mode)
    {
        switch (mode)
        {
            case AddressingMode.Immediate:
                return PC++;
            case AddressingMode.ZeroPage:
                return memory.Read(PC++);
            default:
                throw new NotImplementedException($"Addressing mode {mode} is not supported.");
        }
    }

    private void OpLDA(Cpu cpu, AddressingMode mode)
    {
        ushort addr = GetAddress(mode);
        byte value = memory.Read(addr);
        A = value;
        SetFlag(FLAG_Z, A == 0);
        SetFlag(FLAG_N, (A & 0x80) != 0);
    }
}