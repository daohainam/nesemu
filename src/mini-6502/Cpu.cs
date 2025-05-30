namespace mini_6502;
internal class Cpu
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
    private readonly Instruction[] instructions = new Instruction[256];

    public Cpu(IMemory memory)
    {
        this.memory = memory;
        Reset();
        InitInstructions();
    }

    public void Reset()
    {
        A = X = Y = 0;
        SP = 0xFD;
        P = 0b0010_0100;
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
    public string DumpRegisters()
    {
        return $"A={A:X2} X={X:X2} Y={Y:X2} SP={SP:X2} PC={PC:X4} P=[N:{GetFlag(FLAG_NEGATIVE)} V:{GetFlag(FLAG_VOVERFLOW)} -:{(P & 0x20) != 0} B:{GetFlag(FLAG_BREAK)} D:{GetFlag(FLAG_DECIMAL)} I:{GetFlag(FLAG_INTERRUPT)} Z:{GetFlag(FLAG_ZERO)} C:{GetFlag(FLAG_CARRY)}]";
    }

    private void InitInstructions()
    {
        for (int i = 0; i < instructions.Length; i++)
        {
            instructions[i] = new InvalidOpcodeInstruction(i);
        }

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

    private static void Panic(string message)
    {
        throw new PanicException(message);
    }

    private class InvalidOpcodeInstruction(int index) : Instruction("INVALID", AddressingMode.Implied, 1, 
        (cpu, mode) => Panic($"Invalid opcode({index:X4}) at {cpu.PC - 1:X4}."));

    private void OpLDA(Cpu cpu, AddressingMode mode)
    {
        ushort addr = GetAddress(mode);
        byte value = memory.Read(addr);
        A = value;
        SetFlag(FLAG_ZERO, A == 0);
        SetFlag(FLAG_NEGATIVE, (A & 0x80) != 0);
    }
}