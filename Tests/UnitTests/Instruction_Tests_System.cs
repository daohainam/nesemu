using mini_6502;

namespace UnitTests;
public partial class Instruction_Tests_System: Instruction_Tests
{
    [Fact]
    public void OpBRK_ShouldPushPCAndStatusAndSetInterruptFlag()
    {
        cpu.PC = 0x8000;
        cpu.P = 0x00; 
        memory.Write(0xFFFE, 0xFF); // Low byte of interrupt vector
        memory.Write(0xFFFF, 0x80); // High byte of interrupt vector
        memory.Write(cpu.PC, 0x00); // BRK opcode

        cpu.Clock(); 

        Assert.Equal(0x80FF, cpu.PC); // PC should point to the next instruction after BRK
        Assert.Equal(0x10, memory.Read((ushort)(0x0100 + cpu.SP + 1))); // Status pushed to stack
        Assert.Equal(0x80, memory.Read((ushort)(0x0100 + cpu.SP + 2))); // PC high byte pushed to stack
        Assert.Equal(0x02, memory.Read((ushort)(0x0100 + cpu.SP + 3))); // PC low byte pushed to stack
        Assert.True(cpu.GetFlag(Flags.FLAG_INTERRUPT)); // Interrupt flag should be set
    }
}
