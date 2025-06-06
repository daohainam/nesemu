using mini_6502;

namespace UnitTests;
public partial class Instruction_Tests_System: Instruction_Tests
{
    [Fact]
    public void BRK_And_RTI_Tests()
    {
        cpu.PC = 0x8000;
        cpu.P = 0x00; 
        cpu.SP = 0xFD; 
        memory.Write(0xFFFE, 0xFF); // Low byte of interrupt vector
        memory.Write(0xFFFF, 0x80); // High byte of interrupt vector
        memory.Write(cpu.PC, 0x00); // BRK opcode

        memory.Write(0x80FF, 0x40); // RTI opcode

        cpu.Clock(); 

        Assert.Equal(0x80FF, cpu.PC); 
        Assert.Equal(0xFD - 3, cpu.SP);
        Assert.Equal(0x80, memory.Read(0x01FD)); // PC high byte pushed to stack
        Assert.Equal(0x02, memory.Read(0x01FC)); // PC low byte pushed to stack
        Assert.Equal(0b0001_0000, memory.Read(0x01FB)); // Status pushed with break flag set

        cpu.Cycles = 0; // Reset cycles for next instruction
        cpu.Clock(); // Execute RTI

        Assert.Equal(0x8002, cpu.PC);
        Assert.Equal(0xFD, cpu.SP);
        Assert.Equal(0b0001_0000, cpu.P);
    }
}
