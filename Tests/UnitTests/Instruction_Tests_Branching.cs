using mini_6502;

namespace UnitTests;
public partial class Instruction_Tests_Branching: Instruction_Tests
{
    private const byte OP_CODE_BCC = 0x90;
    private const byte OP_CODE_BCS = 0xB0;
    private const byte OP_CODE_BEQ = 0xF0;
    private const byte OP_CODE_BNE = 0xD0;
    private const byte OP_CODE_BPL = 0x10;
    private const byte OP_CODE_BMI = 0x30;
    private const byte OP_CODE_BVC = 0x50;
    private const byte OP_CODE_BVS = 0x70;

    public static readonly TheoryData<string, byte, ushort, int, bool, bool, bool, bool, ushort> Branching_TestData = new()
    {
        { "BCC", OP_CODE_BCC, 0x8000, 0x0F, false, false, false, false, 0x8011 },
        { "BCC", OP_CODE_BCC, 0x8000, 0x0F, true, false, false, false, 0x8002 },
        { "BCS", OP_CODE_BCS, 0x8000, 0x0F, true, false, false, false, 0x8011 },
        { "BCS", OP_CODE_BCS, 0x8000, 0x0F, false, false, false, false, 0x8002 },
        { "BEQ", OP_CODE_BEQ, 0x8000, 0x0F, false, true, false, false, 0x8011 },
        { "BEQ", OP_CODE_BEQ, 0x8000, 0x0F, false, false, false, false, 0x8002 },
        { "BNE", OP_CODE_BNE, 0x8000, 0x0F, true, false, false, false, 0x8011 },
        { "BNE", OP_CODE_BNE, 0x8000, 0x0F, true, true, false, false, 0x8002 },
        { "BPL", OP_CODE_BPL, 0x8000, 0x0F, false, false, true, false, 0x8011 },
        { "BPL", OP_CODE_BPL, 0x8000, 0x0F, false, false, false, false, 0x8002 },
        { "BMI", OP_CODE_BMI, 0x8000, 0xF1 /* -15 */, true ,false ,true ,false , 0x8002 - 15 },
        { "BMI", OP_CODE_BMI , 0x8001 , 15 ,false ,false ,true ,false , 0x8003 + 15  },
        { "BVC", OP_CODE_BVC , 0x8001 , -15 ,false ,false ,false ,false , 0x8003 - 15 },
        { "BVC", OP_CODE_BVC , 0x8001 , -15 ,false ,false ,true ,true , 0x8003 },
        { "BVS", OP_CODE_BVS , 0x8001 ,-15,false,false,false,true , 0x8003 - 15 },
        { "BVS", OP_CODE_BVS , 0x8001 ,-15,false,false,true,false , 0x8003 }
    }
;

    [Theory]
    [MemberData(nameof(Branching_TestData))]
    public void Branching_OpCode_Test(string _, byte opCode, ushort pc, int offset, bool carryFlag, bool zeroFlag, bool negativeFlag, bool overflowFlag, ushort expectedPC)
    {
        cpu.PC = pc;
        cpu.SetFlag(Flags.FLAG_CARRY, carryFlag);
        cpu.SetFlag(Flags.FLAG_ZERO, zeroFlag);
        cpu.SetFlag(Flags.FLAG_NEGATIVE, negativeFlag);
        cpu.SetFlag(Flags.FLAG_OVERFLOW, overflowFlag);
        memory.Write(pc, opCode);
        memory.Write((ushort)(cpu.PC + 1), (byte)offset);
        cpu.Clock();
        Assert.Equal(expectedPC, cpu.PC);
    }

}
