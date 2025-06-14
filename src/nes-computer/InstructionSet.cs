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
        // Load Instructions
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

        // Transfer Instructions
        instructions[0xAA] = new Instruction("TAX", AddressingMode.Implied, 2, 2, InstructionSet_RegisterTransfer.OpTAX);
        instructions[0xA8] = new Instruction("TAY", AddressingMode.Implied, 2, 2, InstructionSet_RegisterTransfer.OpTAY);
        instructions[0x8A] = new Instruction("TXA", AddressingMode.Implied, 2, 2, InstructionSet_RegisterTransfer.OpTXA);
        instructions[0x98] = new Instruction("TYA", AddressingMode.Implied, 2, 2, InstructionSet_RegisterTransfer.OpTYA);
        instructions[0xBA] = new Instruction("TSX", AddressingMode.Implied, 2, 2, InstructionSet_RegisterTransfer.OpTSX);
        instructions[0x9A] = new Instruction("TXS", AddressingMode.Implied, 2, 2, InstructionSet_RegisterTransfer.OpTXS);

        // Stack Instructions
        instructions[0x48] = new Instruction("PHA", AddressingMode.Implied, 3, 1, InstructionSet_Stack.OpPHA);
        instructions[0x08] = new Instruction("PHP", AddressingMode.Implied, 3, 1, InstructionSet_Stack.OpPHP);
        instructions[0x68] = new Instruction("PLA", AddressingMode.Implied, 4, 1, InstructionSet_Stack.OpPLA);
        instructions[0x28] = new Instruction("PLP", AddressingMode.Implied, 4, 1, InstructionSet_Stack.OpPLP);

        // Logic Instructions
        instructions[0x29] = new Instruction("AND", AddressingMode.Immediate, 2, 2, InstructionSet_Logical.OpAND);
        instructions[0x25] = new Instruction("AND", AddressingMode.ZeroPage, 3, 2, InstructionSet_Logical.OpAND);
        instructions[0x35] = new Instruction("AND", AddressingMode.ZeroPageX, 4, 2, InstructionSet_Logical.OpAND);
        instructions[0x2D] = new Instruction("AND", AddressingMode.Absolute, 4, 3, InstructionSet_Logical.OpAND);
        instructions[0x3D] = new Instruction("AND", AddressingMode.AbsoluteX, 4, 3, InstructionSet_Logical.OpAND);
        instructions[0x39] = new Instruction("AND", AddressingMode.AbsoluteY, 4, 3, InstructionSet_Logical.OpAND);
        instructions[0x21] = new Instruction("AND", AddressingMode.IndirectX, 6, 2, InstructionSet_Logical.OpAND);
        instructions[0x31] = new Instruction("AND", AddressingMode.IndirectY, 5, 2, InstructionSet_Logical.OpAND);
        instructions[0x49] = new Instruction("EOR", AddressingMode.Immediate, 5, 2, InstructionSet_Logical.OpEOR);
        instructions[0x45] = new Instruction("EOR", AddressingMode.ZeroPage, 3, 2, InstructionSet_Logical.OpEOR);
        instructions[0x55] = new Instruction("EOR", AddressingMode.ZeroPageX, 4, 2, InstructionSet_Logical.OpEOR);
        instructions[0x4D] = new Instruction("EOR", AddressingMode.Absolute, 4, 3, InstructionSet_Logical.OpEOR);
        instructions[0x5D] = new Instruction("EOR", AddressingMode.AbsoluteX, 4, 3, InstructionSet_Logical.OpEOR);
        instructions[0x59] = new Instruction("EOR", AddressingMode.AbsoluteY, 4, 3, InstructionSet_Logical.OpEOR);
        instructions[0x41] = new Instruction("EOR", AddressingMode.IndirectX, 6, 2, InstructionSet_Logical.OpEOR);
        instructions[0x51] = new Instruction("EOR", AddressingMode.IndirectY, 5, 2, InstructionSet_Logical.OpEOR);
        instructions[0x09] = new Instruction("ORA", AddressingMode.Immediate, 2, 2, InstructionSet_Logical.OpORA);
        instructions[0x05] = new Instruction("ORA", AddressingMode.ZeroPage, 3, 2, InstructionSet_Logical.OpORA);
        instructions[0x15] = new Instruction("ORA", AddressingMode.ZeroPageX, 4, 2, InstructionSet_Logical.OpORA);
        instructions[0x0D] = new Instruction("ORA", AddressingMode.Absolute, 4, 3, InstructionSet_Logical.OpORA);
        instructions[0x1D] = new Instruction("ORA", AddressingMode.AbsoluteX, 4, 3, InstructionSet_Logical.OpORA);
        instructions[0x19] = new Instruction("ORA", AddressingMode.AbsoluteY, 4, 3, InstructionSet_Logical.OpORA);
        instructions[0x01] = new Instruction("ORA", AddressingMode.IndirectX, 6, 2, InstructionSet_Logical.OpORA);
        instructions[0x11] = new Instruction("ORA", AddressingMode.IndirectY, 5, 2, InstructionSet_Logical.OpORA);
        instructions[0x24] = new Instruction("BIT", AddressingMode.ZeroPage, 3, 2, InstructionSet_Logical.OpBIT);
        instructions[0x2C] = new Instruction("BIT", AddressingMode.Absolute, 4, 3, InstructionSet_Logical.OpBIT);

        // Arithmetic Instructions
        instructions[0x69] = new Instruction("ADC", AddressingMode.Immediate, 2, 2, InstructionSet_Arithmetic.OpADC);
        instructions[0x65] = new Instruction("ADC", AddressingMode.ZeroPage, 3, 2, InstructionSet_Arithmetic.OpADC);
        instructions[0x75] = new Instruction("ADC", AddressingMode.ZeroPageX, 4, 2, InstructionSet_Arithmetic.OpADC);
        instructions[0x6D] = new Instruction("ADC", AddressingMode.Absolute, 4, 3, InstructionSet_Arithmetic.OpADC);
        instructions[0x7D] = new Instruction("ADC", AddressingMode.AbsoluteX, 4, 3, InstructionSet_Arithmetic.OpADC);
        instructions[0x79] = new Instruction("ADC", AddressingMode.AbsoluteY, 4, 3, InstructionSet_Arithmetic.OpADC);
        instructions[0x61] = new Instruction("ADC", AddressingMode.IndirectX, 6, 2, InstructionSet_Arithmetic.OpADC);
        instructions[0x71] = new Instruction("ADC", AddressingMode.IndirectY, 5, 2, InstructionSet_Arithmetic.OpADC);
        instructions[0xE9] = new Instruction("SBC", AddressingMode.Immediate, 2, 2, InstructionSet_Arithmetic.OpSBC);
        instructions[0xE5] = new Instruction("SBC", AddressingMode.ZeroPage, 3, 2, InstructionSet_Arithmetic.OpSBC);
        instructions[0xF5] = new Instruction("SBC", AddressingMode.ZeroPageX, 4, 2, InstructionSet_Arithmetic.OpSBC);
        instructions[0xED] = new Instruction("SBC", AddressingMode.Absolute, 4, 3, InstructionSet_Arithmetic.OpSBC);
        instructions[0xFD] = new Instruction("SBC", AddressingMode.AbsoluteX, 4, 3, InstructionSet_Arithmetic.OpSBC);
        instructions[0xF9] = new Instruction("SBC", AddressingMode.AbsoluteY, 4, 3, InstructionSet_Arithmetic.OpSBC);
        instructions[0xE1] = new Instruction("SBC", AddressingMode.IndirectX, 6, 2, InstructionSet_Arithmetic.OpSBC);
        instructions[0xF1] = new Instruction("SBC", AddressingMode.IndirectY, 5, 2, InstructionSet_Arithmetic.OpSBC);
        instructions[0xC9] = new Instruction("CMP", AddressingMode.Immediate, 2, 2, InstructionSet_Arithmetic.OpCMP);
        instructions[0xC5] = new Instruction("CMP", AddressingMode.ZeroPage, 3, 2, InstructionSet_Arithmetic.OpCMP);
        instructions[0xD5] = new Instruction("CMP", AddressingMode.ZeroPageX, 4, 2, InstructionSet_Arithmetic.OpCMP);
        instructions[0xCD] = new Instruction("CMP", AddressingMode.Absolute, 4, 3, InstructionSet_Arithmetic.OpCMP);
        instructions[0xDD] = new Instruction("CMP", AddressingMode.AbsoluteX, 4, 3, InstructionSet_Arithmetic.OpCMP);
        instructions[0xD9] = new Instruction("CMP", AddressingMode.AbsoluteY, 4, 3, InstructionSet_Arithmetic.OpCMP);
        instructions[0xC1] = new Instruction("CMP", AddressingMode.IndirectX, 6, 2, InstructionSet_Arithmetic.OpCMP);
        instructions[0xD1] = new Instruction("CMP", AddressingMode.IndirectY, 5, 2, InstructionSet_Arithmetic.OpCMP);
        instructions[0xE0] = new Instruction("CPX", AddressingMode.Immediate, 2, 2, InstructionSet_Arithmetic.OpCPX);
        instructions[0xE4] = new Instruction("CPX", AddressingMode.ZeroPage, 3, 2, InstructionSet_Arithmetic.OpCPX);
        instructions[0xEC] = new Instruction("CPX", AddressingMode.Absolute, 4, 3, InstructionSet_Arithmetic.OpCPX);
        instructions[0xC0] = new Instruction("CPY", AddressingMode.Immediate, 2, 2, InstructionSet_Arithmetic.OpCPY);
        instructions[0xC4] = new Instruction("CPY", AddressingMode.ZeroPage, 3, 2, InstructionSet_Arithmetic.OpCPY);
        instructions[0xCC] = new Instruction("CPY", AddressingMode.Absolute, 4, 3, InstructionSet_Arithmetic.OpCPY);

        instructions[0xE6] = new Instruction("INC", AddressingMode.ZeroPage, 5, 2, InstructionSet_Arithmetic.OpINC);
        instructions[0xF6] = new Instruction("INC", AddressingMode.ZeroPageX, 6, 2, InstructionSet_Arithmetic.OpINC);
        instructions[0xEE] = new Instruction("INC", AddressingMode.Absolute, 6, 3, InstructionSet_Arithmetic.OpINC);
        instructions[0xFE] = new Instruction("INC", AddressingMode.AbsoluteX, 7, 3, InstructionSet_Arithmetic.OpINC);
        instructions[0xC6] = new Instruction("DEC", AddressingMode.ZeroPage, 5, 2, InstructionSet_Arithmetic.OpDEC);
        instructions[0xD6] = new Instruction("DEC", AddressingMode.ZeroPageX, 6, 2, InstructionSet_Arithmetic.OpDEC);
        instructions[0xCE] = new Instruction("DEC", AddressingMode.Absolute, 6, 3, InstructionSet_Arithmetic.OpDEC);
        instructions[0xDE] = new Instruction("DEC", AddressingMode.AbsoluteX, 7, 3, InstructionSet_Arithmetic.OpDEC);

        instructions[0xE8] = new Instruction("INX", AddressingMode.Implied, 2, 2, InstructionSet_Arithmetic.OpINX);
        instructions[0xC8] = new Instruction("INY", AddressingMode.Implied, 2, 2, InstructionSet_Arithmetic.OpINY);
        instructions[0xCA] = new Instruction("DEX", AddressingMode.Implied, 2, 1, InstructionSet_Arithmetic.OpDEX);
        instructions[0x88] = new Instruction("DEY", AddressingMode.Implied, 2, 1, InstructionSet_Arithmetic.OpDEY);

        instructions[0x0A] = new Instruction("ASL", AddressingMode.Accumulator, 2, 1, InstructionSet_Arithmetic.OpASL);
        instructions[0x06] = new Instruction("ASL", AddressingMode.ZeroPage, 5, 2, InstructionSet_Arithmetic.OpASL);
        instructions[0x16] = new Instruction("ASL", AddressingMode.ZeroPageX, 6, 2, InstructionSet_Arithmetic.OpASL);
        instructions[0x0E] = new Instruction("ASL", AddressingMode.Absolute, 6, 3, InstructionSet_Arithmetic.OpASL);
        instructions[0x1E] = new Instruction("ASL", AddressingMode.AbsoluteX, 7, 3, InstructionSet_Arithmetic.OpASL);

        instructions[0x4A] = new Instruction("LSR", AddressingMode.Accumulator, 2, 1, InstructionSet_Arithmetic.OpLSR);
        instructions[0x46] = new Instruction("LSR", AddressingMode.ZeroPage, 5, 2, InstructionSet_Arithmetic.OpLSR);
        instructions[0x56] = new Instruction("LSR", AddressingMode.ZeroPageX, 6, 2, InstructionSet_Arithmetic.OpLSR);
        instructions[0x4E] = new Instruction("LSR", AddressingMode.Absolute, 6, 3, InstructionSet_Arithmetic.OpLSR);
        instructions[0x5E] = new Instruction("LSR", AddressingMode.AbsoluteX, 7, 3, InstructionSet_Arithmetic.OpLSR);

        instructions[0x2A] = new Instruction("ROL", AddressingMode.Accumulator, 2, 1, InstructionSet_Arithmetic.OpROL);
        instructions[0x26] = new Instruction("ROL", AddressingMode.ZeroPage, 5, 2, InstructionSet_Arithmetic.OpROL);
        instructions[0x36] = new Instruction("ROL", AddressingMode.ZeroPageX, 6, 2, InstructionSet_Arithmetic.OpROL);
        instructions[0x2E] = new Instruction("ROL", AddressingMode.Absolute, 6, 3, InstructionSet_Arithmetic.OpROL);
        instructions[0x3E] = new Instruction("ROL", AddressingMode.AbsoluteX, 7, 3, InstructionSet_Arithmetic.OpROL);

        instructions[0x6A] = new Instruction("ROR", AddressingMode.Accumulator, 2, 1, InstructionSet_Arithmetic.OpROR);
        instructions[0x66] = new Instruction("ROR", AddressingMode.ZeroPage, 5, 2, InstructionSet_Arithmetic.OpROR);
        instructions[0x76] = new Instruction("ROR", AddressingMode.ZeroPageX, 6, 2, InstructionSet_Arithmetic.OpROR);
        instructions[0x6E] = new Instruction("ROR", AddressingMode.Absolute, 6, 3, InstructionSet_Arithmetic.OpROR);
        instructions[0x7E] = new Instruction("ROR", AddressingMode.AbsoluteX, 7, 3, InstructionSet_Arithmetic.OpROR);

        // Jumps & Calls
        instructions[0x4C] = new Instruction("JMP", AddressingMode.Absolute, 3, 3, InstructionSet_Jump.OpJMP);
        instructions[0x6C] = new Instruction("JMP", AddressingMode.Indirect, 5, 5, InstructionSet_Jump.OpJMP);
        instructions[0x20] = new Instruction("JSR", AddressingMode.Absolute, 6, 3, InstructionSet_Jump.OpJSR);
        instructions[0x60] = new Instruction("RTS", AddressingMode.Implied, 6, 1, InstructionSet_Jump.OpRTS);

        // Branching Instructions
        instructions[0x90] = new Instruction("BCC", AddressingMode.Relative, 2, 2, InstructionSet_Branching.OpBCC);
        instructions[0xB0] = new Instruction("BCS", AddressingMode.Relative, 2, 2, InstructionSet_Branching.OpBCS);
        instructions[0xF0] = new Instruction("BEQ", AddressingMode.Relative, 2, 2, InstructionSet_Branching.OpBEQ);
        instructions[0xD0] = new Instruction("BNE", AddressingMode.Relative, 2, 2, InstructionSet_Branching.OpBNE);
        instructions[0x10] = new Instruction("BPL", AddressingMode.Relative, 2, 2, InstructionSet_Branching.OpBPL);
        instructions[0x30] = new Instruction("BMI", AddressingMode.Relative, 2, 2, InstructionSet_Branching.OpBMI);
        instructions[0x50] = new Instruction("BVC", AddressingMode.Relative, 2, 2, InstructionSet_Branching.OpBVC);
        instructions[0x70] = new Instruction("BVS", AddressingMode.Relative, 2, 2, InstructionSet_Branching.OpBVS);

        // Status Flag Instructions
        instructions[0x18] = new Instruction("CLC", AddressingMode.Implied, 2, 1, InstructionSet_StatusFlag.OpCLC);
        instructions[0x38] = new Instruction("SEC", AddressingMode.Implied, 2, 1, InstructionSet_StatusFlag.OpSEC);
        instructions[0x58] = new Instruction("CLI", AddressingMode.Implied, 2, 1, InstructionSet_StatusFlag.OpCLI);
        instructions[0x78] = new Instruction("SEI", AddressingMode.Implied, 2, 1, InstructionSet_StatusFlag.OpSEI);
        instructions[0xB8] = new Instruction("CLV", AddressingMode.Implied, 2, 1, InstructionSet_StatusFlag.OpCLV);
        instructions[0xD8] = new Instruction("CLD", AddressingMode.Implied, 2, 1, InstructionSet_StatusFlag.OpCLD);
        instructions[0xF8] = new Instruction("SED", AddressingMode.Implied, 2, 1, InstructionSet_StatusFlag.OpSED);

        // System Instructions
        instructions[0x00] = new Instruction("BRK", AddressingMode.Implied, 7, 1, InstructionSet_System.OpBRK);
        instructions[0x40] = new Instruction("RTI", AddressingMode.Implied, 6, 1, InstructionSet_System.OpRTI);

        // No Operation (NOP) Instructions
        instructions[0xEA] = new Instruction("RTS", AddressingMode.Implied, 1, 1, InstructionSet_System.OpNOP);


        for (int i = 0; i < instructions.Length; i++)
        {
            if (instructions[i] is null) 
                instructions[i] = new InvalidOpcodeInstruction(i);
        }

    }

    private class InvalidOpcodeInstruction(int index) : Instruction("INVALID", AddressingMode.Implied, 1, 1,
    (cpu, memory, mode) => Cpu.Panic($"Invalid opcode({index:X4}) at {cpu.PC - 1:X4}."));

    public Instruction this[byte opcode] => instructions[opcode];

    public static InstructionSet Instance { get; } = new InstructionSet();
}
