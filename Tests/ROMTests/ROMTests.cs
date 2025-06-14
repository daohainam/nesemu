
using mini_6502;

namespace ROMTests;
public class ROMTests
{
    [Theory]
    [InlineData("ram_after_reset.nes", 3000)]
    public async Task Load_ROM_And_Run_TestsAsync(string romName, int milliSeconds)
    {
        var path = Path.Combine("roms", romName);
        var romRoader = new rom_loader.NesRomLoader();
        
        var rom = await romRoader.LoadRomAsync(path);

        Assert.NotNull(rom);

        var nes = new NES();
        
        nes.LoadRom(rom);
        nes.Reset();

        var cancellationTokenSource = new CancellationTokenSource();// TimeSpan.FromMilliseconds(milliSeconds));
        await nes.RunAsync(cancellationTokenSource.Token);
    }
}
