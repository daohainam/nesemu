using mini_6502.Components;

namespace mini_6502;
internal class InstructionSet
{
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

        //instructions[0xA9] = new Instruction("LDA", AddressingMode.Immediate, 2, OpLDA);
        //instructions[0xA5] = new Instruction("LDA", AddressingMode.ZeroPage, 3, OpLDA);
    }

    private class InvalidOpcodeInstruction(int index) : Instruction("INVALID", AddressingMode.Implied, 1,
    (cpu, mode) => Cpu.Panic($"Invalid opcode({index:X4}) at {cpu.PC - 1:X4}."));

    public Instruction this[byte opcode] => instructions[opcode];

    public static InstructionSet Instance { get; } = new InstructionSet();
}
