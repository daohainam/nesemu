using rom;
using rom_loader;

namespace UnitTests;

public class RomLoaderTests
{
    [Fact]
    public void LoadRom_ValidRom_Success()
    {
        byte[] romData = new byte[16 + 16384 + 8192];
        
        romData[0] = (byte)'N';
        romData[1] = (byte)'E';
        romData[2] = (byte)'S';
        romData[3] = 0x1A;
        romData[4] = 1;
        romData[5] = 1;
        romData[6] = 0;
        romData[7] = 0;
        
        var loader = new NesRomLoader();
        var rom = loader.LoadRom(romData);
        
        Assert.NotNull(rom);
        Assert.Equal(1, rom.Header.PrgRomSize);
        Assert.Equal(1, rom.Header.ChrRomSize);
        Assert.Equal(16384, rom.PrgRom.Length);
        Assert.Equal(8192, rom.ChrRom.Length);
    }

    [Fact]
    public void LoadRom_WithTrainer_Success()
    {
        byte[] romData = new byte[16 + 512 + 16384 + 8192];
        
        romData[0] = (byte)'N';
        romData[1] = (byte)'E';
        romData[2] = (byte)'S';
        romData[3] = 0x1A;
        romData[4] = 1;
        romData[5] = 1;
        romData[6] = 0x04;
        romData[7] = 0;
        
        for (int i = 0; i < 512; i++)
        {
            romData[16 + i] = 0xFF;
        }
        
        for (int i = 0; i < 16384; i++)
        {
            romData[16 + 512 + i] = 0xAA;
        }
        
        var loader = new NesRomLoader();
        var rom = loader.LoadRom(romData);
        
        Assert.NotNull(rom);
        Assert.True(rom.Header.HasTrainer);
        Assert.Equal(16384, rom.PrgRom.Length);
        Assert.Equal(0xAA, rom.PrgRom[0]);
    }

    [Fact]
    public void LoadRom_NoChrRom_Success()
    {
        byte[] romData = new byte[16 + 16384];
        
        romData[0] = (byte)'N';
        romData[1] = (byte)'E';
        romData[2] = (byte)'S';
        romData[3] = 0x1A;
        romData[4] = 1;
        romData[5] = 0;
        romData[6] = 0;
        romData[7] = 0;
        
        var loader = new NesRomLoader();
        var rom = loader.LoadRom(romData);
        
        Assert.NotNull(rom);
        Assert.Equal(0, rom.Header.ChrRomSize);
        Assert.Empty(rom.ChrRom);
    }

    [Fact]
    public void LoadRom_InvalidMagicNumber_ThrowsException()
    {
        byte[] romData = new byte[16 + 16384];
        
        romData[0] = (byte)'X';
        romData[1] = (byte)'Y';
        romData[2] = (byte)'Z';
        romData[3] = 0x1A;
        romData[4] = 1;
        
        var loader = new NesRomLoader();
        
        Assert.Throws<InvalidDataException>(() => loader.LoadRom(romData));
    }

    [Fact]
    public void LoadRom_ZeroPrgRomSize_ThrowsException()
    {
        byte[] romData = new byte[16];
        
        romData[0] = (byte)'N';
        romData[1] = (byte)'E';
        romData[2] = (byte)'S';
        romData[3] = 0x1A;
        romData[4] = 0;
        romData[5] = 0;
        
        var loader = new NesRomLoader();
        
        Assert.Throws<InvalidDataException>(() => loader.LoadRom(romData));
    }

    [Fact]
    public void LoadRom_TooShortData_ThrowsException()
    {
        byte[] romData = new byte[10];
        
        var loader = new NesRomLoader();
        
        Assert.Throws<ArgumentException>(() => loader.LoadRom(romData));
    }

    [Fact]
    public void LoadRom_DataSizeMismatch_ThrowsException()
    {
        byte[] romData = new byte[16 + 1000];
        
        romData[0] = (byte)'N';
        romData[1] = (byte)'E';
        romData[2] = (byte)'S';
        romData[3] = 0x1A;
        romData[4] = 1;
        romData[5] = 1;
        
        var loader = new NesRomLoader();
        
        Assert.Throws<InvalidDataException>(() => loader.LoadRom(romData));
    }
}
