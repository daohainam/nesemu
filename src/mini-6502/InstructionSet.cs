using mini_6502.Components;
using mini_6502.Instructions;

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
        instructions[0xA9] = new Instruction("LDA", AddressingMode.Immediate, 2, 2, InstructionSet_LoadStore.OpLDA);
        instructions[0xA5] = new Instruction("LDA", AddressingMode.ZeroPage, 3, 2, InstructionSet_LoadStore.OpLDA);
        instructions[0xB5] = new Instruction("LDA", AddressingMode.ZeroPageX, 4, 2, InstructionSet_LoadStore.OpLDA);
        instructions[0xAD] = new Instruction("LDA", AddressingMode.Absolute, 4, 3, InstructionSet_LoadStore.OpLDA);
        instructions[0xBD] = new Instruction("LDA", AddressingMode.AbsoluteX, 4, 3, InstructionSet_LoadStore.OpLDA);
        instructions[0xB9] = new Instruction("LDA", AddressingMode.AbsoluteY, 4, 3, InstructionSet_LoadStore.OpLDA);
        instructions[0xA1] = new Instruction("LDA", AddressingMode.IndirectX, 6, 2, InstructionSet_LoadStore.OpLDA);
        instructions[0xB1] = new Instruction("LDA", AddressingMode.IndirectY, 5, 2, InstructionSet_LoadStore.OpLDA);

        instructions[0xA2] = new Instruction("LDX", AddressingMode.Immediate, 2, 2, InstructionSet_LoadStore.OpLDX);
        instructions[0xA6] = new Instruction("LDX", AddressingMode.ZeroPage, 3, 2, InstructionSet_LoadStore.OpLDX);
        instructions[0xB6] = new Instruction("LDX", AddressingMode.ZeroPageY, 4, 2, InstructionSet_LoadStore.OpLDX);
        instructions[0xAE] = new Instruction("LDX", AddressingMode.Absolute, 4, 3, InstructionSet_LoadStore.OpLDX);
        instructions[0xBE] = new Instruction("LDX", AddressingMode.AbsoluteY, 4, 3, InstructionSet_LoadStore.OpLDX);

        instructions[0xA0] = new Instruction("LDY", AddressingMode.Immediate, 2, 2, InstructionSet_LoadStore.OpLDY);
        instructions[0xA4] = new Instruction("LDY", AddressingMode.ZeroPage, 3, 2, InstructionSet_LoadStore.OpLDY);
        instructions[0xB4] = new Instruction("LDY", AddressingMode.ZeroPageX, 4, 2, InstructionSet_LoadStore.OpLDY);
        instructions[0xAC] = new Instruction("LDY", AddressingMode.Absolute, 4, 3, InstructionSet_LoadStore.OpLDY);
        instructions[0xBC] = new Instruction("LDY", AddressingMode.AbsoluteX, 4, 3, InstructionSet_LoadStore.OpLDY);

        // Store Instructions
        instructions[0x85] = new Instruction("STA", AddressingMode.ZeroPage, 3, 2, InstructionSet_LoadStore.OpSTA);
        instructions[0x95] = new Instruction("STA", AddressingMode.ZeroPageX, 4, 2, InstructionSet_LoadStore.OpSTA);
        instructions[0x8D] = new Instruction("STA", AddressingMode.Absolute, 4, 3, InstructionSet_LoadStore.OpSTA);
        instructions[0x9D] = new Instruction("STA", AddressingMode.AbsoluteX, 5, 3, InstructionSet_LoadStore.OpSTA);
        instructions[0x99] = new Instruction("STA", AddressingMode.AbsoluteY, 5, 3, InstructionSet_LoadStore.OpSTA);
        instructions[0x81] = new Instruction("STA", AddressingMode.IndirectX, 6, 2, InstructionSet_LoadStore.OpSTA);
        instructions[0x91] = new Instruction("STA", AddressingMode.IndirectY, 6, 2, InstructionSet_LoadStore.OpSTA);

        instructions[0x86] = new Instruction("STX", AddressingMode.ZeroPage, 3, 2, InstructionSet_LoadStore.OpSTX);
        instructions[0x96] = new Instruction("STX", AddressingMode.ZeroPageY, 4, 2, InstructionSet_LoadStore.OpSTX);
        instructions[0x8E] = new Instruction("STX", AddressingMode.Absolute, 4, 3, InstructionSet_LoadStore.OpSTX);

        instructions[0x84] = new Instruction("STY", AddressingMode.ZeroPage, 3, 2, InstructionSet_LoadStore.OpSTY);
        instructions[0x94] = new Instruction("STY", AddressingMode.ZeroPageX, 4, 2, InstructionSet_LoadStore.OpSTY);
        instructions[0x8C] = new Instruction("STY", AddressingMode.Absolute, 4, 3, InstructionSet_LoadStore.OpSTY);
    }

    private class InvalidOpcodeInstruction(int index) : Instruction("INVALID", AddressingMode.Implied, 1, 1,
    (cpu, memory, mode) => Cpu.Panic($"Invalid opcode({index:X4}) at {cpu.PC - 1:X4}."));

    public Instruction this[byte opcode] => instructions[opcode];

    public static InstructionSet Instance { get; } = new InstructionSet();
}
