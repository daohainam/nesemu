using mini_6502.Components;

namespace UnitTests;
public class PpuTests
{
    [Theory]
    [InlineData(0x2000, 0x2000, 0xAF)] // PPUCTRL
    [InlineData(0x2001, 0x2001, 0xAF)] // PPUMASK
    [InlineData(0x2002, 0x2002, 0xAF)] // PPUSTATUS
    [InlineData(0x2003, 0x2003, 0xAF)] // OAMADDR
    [InlineData(0x2004, 0x2004, 0xAF)] // OAMDATA
    [InlineData(0x2005, 0x2005, 0xAF)] // PPUSCROLL
    [InlineData(0x2006, 0x2006, 0xAF)] // PPUADDR
    [InlineData(0x2007, 0x2007, 0xAF)] // PPUDATA
    public void ReadWrite_Success(ushort writeAddress, ushort readAddress, byte value)
    {
        var ppu = new Ppu();

        ppu.WriteRegister(writeAddress, value);
        var v = ppu.ReadRegister(readAddress);
        Assert.Equal(value, v);
    }
}
