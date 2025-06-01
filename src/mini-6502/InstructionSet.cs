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

    private static byte ReadMemory(ushort address, IMemory memory, AddressingMode mode)
    {
        return mode switch
        {
            AddressingMode.Immediate => memory.Read(address),
            AddressingMode.ZeroPage => memory.Read(memory.Read(address)),
            AddressingMode.ZeroPageX => memory.Read((byte)(memory.Read(address) + 1)), // Assuming X is added to the zero page address
            AddressingMode.Absolute => memory.Read((ushort)(memory.Read(address) | (ushort)(memory.Read((ushort)(address + 1)) << 8))),
            AddressingMode.AbsoluteX => memory.Read((ushort)(memory.Read(address) | (ushort)(memory.Read((ushort)(address + 1)) << 8) + 1)), // Assuming X is added to the absolute address
            _ => throw new InvalidOperationException($"Unsupported addressing mode: {mode}")
        };
    }


    private class InvalidOpcodeInstruction(int index) : Instruction("INVALID", AddressingMode.Implied, 1, 1,
    (cpu, memory, mode) => Cpu.Panic($"Invalid opcode({index:X4}) at {cpu.PC - 1:X4}."));

    public Instruction this[byte opcode] => instructions[opcode];

    public static InstructionSet Instance { get; } = new InstructionSet();
}
