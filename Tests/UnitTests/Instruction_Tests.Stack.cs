namespace UnitTests;
public partial class Instruction_Tests
{
    [Fact]
    public void PHA_Instruction_Test()
    {
        cpu.A = 0x77;
        cpu.SP = 0x7F; 
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x48);

        cpu.Clock();
        
        Assert.Equal(0x7E, cpu.SP);
        Assert.Equal(0x77, memory.Read((ushort)(0x0100 + cpu.SP + 1))); // Stack starts at 0x0100
        Assert.Equal(cartridgeAddress + 1, cpu.PC);
    }

    [Fact]
    public void PLA_Instruction_Test()
    {
        cpu.SP = 0x7F;
        memory.Write((ushort)(0x0100 + cpu.SP + 1), 0x55); 
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x68); 
        
        cpu.Clock();
        
        Assert.Equal(0x80, cpu.SP); 
        Assert.Equal(0x55, cpu.A); 
        Assert.Equal(cartridgeAddress + 1, cpu.PC);
    }

    [Fact]
    public void PHP_Instruction_Test() {
        cpu.P = 0x77; 
        cpu.SP = 0x7F; 
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x08); 

        cpu.Clock();
        
        Assert.Equal(0x7E, cpu.SP);
        Assert.Equal(0x77, memory.Read((ushort)(0x0100 + cpu.SP + 1))); 
        Assert.Equal(cartridgeAddress + 1, cpu.PC);
    }

    [Fact]
    public void PLP_Instruction_Test() {
        cpu.SP = 0x7F; 
        memory.Write((ushort)(0x0100 + cpu.SP + 1), 0x55); 
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x28); 

        cpu.Clock();
        
        Assert.Equal(0x80, cpu.SP); 
        Assert.Equal(0x55, cpu.P); 
        Assert.Equal(cartridgeAddress + 1, cpu.PC);
    }

}
