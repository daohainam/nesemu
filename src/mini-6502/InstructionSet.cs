using mini_6502.Components;

namespace mini_6502;
internal partial class InstructionSet
{
    // http://www.6502.org/users/obelisk/6502/instructions.html
    private static readonly Instruction[] instructions = new Instruction[256];
    static InstructionSet()
    {
        InitInstructions();
    }

    private static void InitInstructions()
    {
        for (int i = 0; i < instructions.Length; i++)
        {
            instructions[i] = new InvalidOpcodeInstruction(i);
        }

        // Load/Store Instructions
        instructions[0xA9] = new Instruction("LDA", AddressingMode.Immediate, 2, 2, OpLDA);
        instructions[0xA5] = new Instruction("LDA", AddressingMode.ZeroPage, 3, 2, OpLDA);
        instructions[0xB5] = new Instruction("LDA", AddressingMode.ZeroPageX, 4, 2, OpLDA);
        instructions[0xAD] = new Instruction("LDA", AddressingMode.Absolute, 4, 3, OpLDA);
        instructions[0xBD] = new Instruction("LDA", AddressingMode.AbsoluteX, 4, 3, OpLDA);
        instructions[0xB9] = new Instruction("LDA", AddressingMode.AbsoluteY, 4, 3, OpLDA);
        instructions[0xA1] = new Instruction("LDA", AddressingMode.IndirectX, 6, 2, OpLDA);
        instructions[0xB1] = new Instruction("LDA", AddressingMode.IndirectY, 5, 2, OpLDA);
    }

    private static byte ReadMemory(Cpu cpu, IMemory memory, AddressingMode mode)
    {
        switch (mode)
        {
            case AddressingMode.Immediate:
                return memory.Read(cpu.PC++);
            case AddressingMode.ZeroPage:
                return memory.Read(memory.Read(cpu.PC++));
            case AddressingMode.ZeroPageX:
                return memory.Read((byte)(memory.Read(cpu.PC++) + cpu.X));
            case AddressingMode.Absolute:
                ushort address = (ushort)(memory.Read(cpu.PC++) | (memory.Read(cpu.PC++) << 8));
                return memory.Read(address);
            case AddressingMode.AbsoluteX:
                address = (ushort)(memory.Read(cpu.PC++) | (memory.Read(cpu.PC++) << 8));
                return memory.Read((ushort)(address + cpu.X));
            case AddressingMode.AbsoluteY:
                address = (ushort)(memory.Read(cpu.PC++) | (memory.Read(cpu.PC++) << 8));
                return memory.Read((ushort)(address + cpu.Y));
            case AddressingMode.IndirectX:
                byte zpAddress = memory.Read(cpu.PC++);
                ushort indirectAddress = (ushort)(zpAddress + cpu.X);
                ushort addressLow = memory.Read(indirectAddress);
                ushort addressHigh = memory.Read((ushort)((indirectAddress + 1) & 0xFF)); // Wrap around for zero page
                return memory.Read((ushort)(addressHigh << 8 | addressLow));
            case AddressingMode.IndirectY:
                zpAddress = memory.Read(cpu.PC++);
                indirectAddress = (ushort)(zpAddress + cpu.Y);
                addressLow = memory.Read(indirectAddress);
                addressHigh = memory.Read((ushort)((indirectAddress + 1) & 0xFF)); // Wrap around for zero page
                return memory.Read((ushort)(addressHigh << 8 | addressLow));
            default:
                Cpu.Panic($"Unsupported addressing mode: {mode}");
                return 0; // Unreachable, but required to satisfy the compiler
        }
    }

    private class InvalidOpcodeInstruction(int index) : Instruction("INVALID", AddressingMode.Implied, 1, 1,
    (cpu, memory, mode) => Cpu.Panic($"Invalid opcode({index:X4}) at {cpu.PC - 1:X4}."));

    public Instruction this[byte opcode] => instructions[opcode];

    public static InstructionSet Instance { get; } = new InstructionSet();
}
