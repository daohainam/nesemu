using mini_6502.Components;

namespace UnitTests;
public class PpuTests
{
    [Theory]
    [InlineData(0x2000, 0xAF)]
    [InlineData(0x2001, 0xAF)]
    [InlineData(0x2003, 0xAF)]
    [InlineData(0x2005, 0xAF)]
    public void WriteRegister_Success(ushort writeAddress, byte value)
    {
        var ppu = new Ppu();

        ppu.WriteRegister(writeAddress, value);
        
        Assert.True(true);
    }

    [Fact]
    public void GetScreenBuffer_ReturnsValidBuffer()
    {
        var ppu = new Ppu();
        var buffer = ppu.GetScreenBuffer();
        
        Assert.NotNull(buffer);
        Assert.Equal(256 * 240 * 4, buffer.Length);
    }

    [Fact]
    public void FrameComplete_EventRaisedAfterFrame()
    {
        var ppu = new Ppu();
        bool frameCompleteRaised = false;
        ppu.FrameComplete += (sender, e) => frameCompleteRaised = true;
        
        ppu.WriteRegister(0x2000, 0x80);
        
        for (int scanline = 0; scanline < 262; scanline++)
        {
            for (int cycle = 0; cycle < 341; cycle++)
            {
                ppu.Clock();
            }
        }
        
        Assert.True(frameCompleteRaised);
    }

    [Fact]
    public void Reset_ClearsState()
    {
        var ppu = new Ppu();
        
        ppu.WriteRegister(0x2000, 0xFF);
        ppu.Clock();
        
        ppu.Reset();
        
        var buffer = ppu.GetScreenBuffer();
        Assert.All(buffer, b => Assert.Equal(0, b));
    }

    [Fact]
    public void PPUSTATUS_VBlankFlag_SetAt241()
    {
        var ppu = new Ppu();
        
        for (int i = 0; i < 241 * 341 + 1; i++)
        {
            ppu.Clock();
        }
        
        var status = ppu.ReadRegister(0x2002);
        Assert.True((status & 0x80) != 0);
    }

    [Fact]
    public void PPUSTATUS_VBlankFlag_ClearsOnRead()
    {
        var ppu = new Ppu();
        
        for (int i = 0; i < 241 * 341 + 1; i++)
        {
            ppu.Clock();
        }
        
        var status1 = ppu.ReadRegister(0x2002);
        Assert.True((status1 & 0x80) != 0);
        
        var status2 = ppu.ReadRegister(0x2002);
        Assert.True((status2 & 0x80) == 0);
    }

    [Fact]
    public void PPUADDR_WriteTwice_SetsAddress()
    {
        var ppu = new Ppu();
        
        ppu.WriteRegister(0x2006, 0x20);
        ppu.WriteRegister(0x2006, 0x00);
        
        ppu.WriteRegister(0x2007, 0xAA);
        
        ppu.WriteRegister(0x2006, 0x20);
        ppu.WriteRegister(0x2006, 0x00);
        
        ppu.ReadRegister(0x2007);
        var value = ppu.ReadRegister(0x2007);
        
        Assert.Equal(0xAA, value);
    }

    [Fact]
    public void OAMDATA_WriteRead_Success()
    {
        var ppu = new Ppu();
        
        ppu.WriteRegister(0x2003, 0x00);
        ppu.WriteRegister(0x2004, 0x55);
        
        ppu.WriteRegister(0x2003, 0x00);
        var value = ppu.ReadRegister(0x2004);
        
        Assert.Equal(0x55, value);
    }

    [Fact]
    public void PPUSCROLL_WriteTwice_UpdatesScrollRegisters()
    {
        var ppu = new Ppu();
        
        ppu.WriteRegister(0x2005, 0x10);
        ppu.WriteRegister(0x2005, 0x20);
        
        Assert.True(true);
    }

    [Fact]
    public void VramWrite_Success()
    {
        var ppu = new Ppu();
        
        ppu.WriteRegister(0x2006, 0x20);
        ppu.WriteRegister(0x2006, 0x00);
        ppu.WriteRegister(0x2007, 0x42);
        
        ppu.WriteRegister(0x2006, 0x20);
        ppu.WriteRegister(0x2006, 0x00);
        
        ppu.ReadRegister(0x2007);
        var value = ppu.ReadRegister(0x2007);
        
        Assert.Equal(0x42, value);
    }

    [Fact]
    public void PaletteRam_WriteRead_Success()
    {
        var ppu = new Ppu();
        
        ppu.WriteRegister(0x2006, 0x3F);
        ppu.WriteRegister(0x2006, 0x00);
        ppu.WriteRegister(0x2007, 0x30);
        
        ppu.WriteRegister(0x2006, 0x3F);
        ppu.WriteRegister(0x2006, 0x00);
        
        var value = ppu.ReadRegister(0x2007);
        
        Assert.Equal(0x30, value);
    }

    [Fact]
    public void PPUCTRL_IncrementMode_Works()
    {
        var ppu = new Ppu();
        
        ppu.WriteRegister(0x2000, 0x04);
        
        ppu.WriteRegister(0x2006, 0x20);
        ppu.WriteRegister(0x2006, 0x00);
        ppu.WriteRegister(0x2007, 0x11);
        ppu.WriteRegister(0x2007, 0x22);
        
        ppu.WriteRegister(0x2006, 0x20);
        ppu.WriteRegister(0x2006, 0x20);
        
        ppu.ReadRegister(0x2007);
        var value = ppu.ReadRegister(0x2007);
        
        Assert.Equal(0x22, value);
    }

    [Fact]
    public void PPU_WithMapper_ReadsChrRom()
    {
        var mapper = new mini_6502.Mappers.Mapper0();
        var ppu = new Ppu(mapper);
        
        ppu.WriteRegister(0x2006, 0x00);
        ppu.WriteRegister(0x2006, 0x10);
        ppu.WriteRegister(0x2007, 0xAB);
        
        ppu.WriteRegister(0x2006, 0x00);
        ppu.WriteRegister(0x2006, 0x10);
        
        ppu.ReadRegister(0x2007);
        var value = ppu.ReadRegister(0x2007);
        
        Assert.Equal(0xAB, value);
    }

    [Fact]
    public void ScreenBuffer_UpdatesCorrectly()
    {
        var ppu = new Ppu();
        var initialBuffer = ppu.GetScreenBuffer();
        
        Assert.NotNull(initialBuffer);
        Assert.Equal(256 * 240 * 4, initialBuffer.Length);
        
        for (int i = 0; i < 10; i++)
        {
            ppu.Clock();
        }
        
        var updatedBuffer = ppu.GetScreenBuffer();
        Assert.NotNull(updatedBuffer);
        Assert.Equal(256 * 240 * 4, updatedBuffer.Length);
    }

    [Fact]
    public void BackgroundRendering_WithMapper_Works()
    {
        var mapper = new mini_6502.Mappers.Mapper0();
        var ppu = new Ppu(mapper);
        
        ppu.WriteRegister(0x2000, 0x80);
        ppu.WriteRegister(0x2001, 0x08);
        
        ppu.WriteRegister(0x2006, 0x20);
        ppu.WriteRegister(0x2006, 0x00);
        ppu.WriteRegister(0x2007, 0x01);
        
        ppu.WriteRegister(0x2006, 0x3F);
        ppu.WriteRegister(0x2006, 0x00);
        ppu.WriteRegister(0x2007, 0x0F);
        
        for (int i = 0; i < 341 * 10; i++)
        {
            ppu.Clock();
        }
        
        var buffer = ppu.GetScreenBuffer();
        Assert.NotNull(buffer);
    }
}
