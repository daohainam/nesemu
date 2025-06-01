namespace mini_6502.Components;
internal class Cpu: IDebugable
{
    // Registers
    public byte A; // Accumulator
    public byte X; // Index register X
    public byte Y; // Index register Y
    public byte SP; // Stack Pointer (chỉ offset trong vùng 0x0100–0x01FF)
    public ushort PC; // Program Counter (địa chỉ lệnh tiếp theo)
    public byte P; // Processor Status Register


    // Flags
    private const byte FLAG_CARRY = 1 << 0;
    private const byte FLAG_ZERO = 1 << 1;
    private const byte FLAG_INTERRUPT = 1 << 2;
    private const byte FLAG_DECIMAL = 1 << 3;
    private const byte FLAG_BREAK = 1 << 4;
    private const byte FLAG_UNUSED = 1 << 5; // Unused
    private const byte FLAG_VOVERFLOW = 1 << 6;
    private const byte FLAG_NEGATIVE = 1 << 7;

    private readonly IMemory memory;
    private int cycles;

    public Cpu(IMemory memory)
    {
        this.memory = memory;
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
            instr.Execute(this, memory);
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
    public string Dump()
    {
        return $"A={A:X2} X={X:X2} Y={Y:X2} SP={SP:X2} PC={PC:X4} P=[N:{GetFlag(FLAG_NEGATIVE)} V:{GetFlag(FLAG_VOVERFLOW)} -:{(P & 0x20) != 0} B:{GetFlag(FLAG_BREAK)} D:{GetFlag(FLAG_DECIMAL)} I:{GetFlag(FLAG_INTERRUPT)} Z:{GetFlag(FLAG_ZERO)} C:{GetFlag(FLAG_CARRY)}]";
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

    public static void Panic(string message)
    {
        throw new PanicException(message);
    }

    private void OpLDA(Cpu cpu, AddressingMode mode)
    {
        ushort addr = GetAddress(mode);
        byte value = memory.Read(addr);
        A = value;
        SetFlag(FLAG_ZERO, A == 0);
        SetFlag(FLAG_NEGATIVE, (A & 0x80) != 0);
    }
}