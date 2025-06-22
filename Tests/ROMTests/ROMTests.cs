
using Microsoft.Extensions.Logging;
using mini_6502;
using Xunit.Abstractions;

namespace ROMTests;
public class ROMTests
{
    private readonly ITestOutputHelper output;
    private readonly ILoggerFactory loggerFactory;

    public ROMTests(ITestOutputHelper output)
    {
        this.output = output;

        this.loggerFactory = LoggerFactory.Create(l =>
        {
            l.AddConsole();
            l.AddDebug();
            l.SetMinimumLevel(LogLevel.Debug);
        });
    }

    [Theory]
    [InlineData("registers.nes", 3000)]
    [InlineData("ram_after_reset.nes", 3000)]
    public async Task Load_ROM_And_Run_TestsAsync(string romName, int milliSeconds)
    {
        var path = Path.Combine("roms", romName);
        var romRoader = new rom_loader.NesRomLoader();
        
        var rom = await romRoader.LoadRomAsync(path);

        Assert.NotNull(rom);

        var nes = new NES(rom, loggerFactory);
        
        nes.Reset();

        var cancellationTokenSource = new CancellationTokenSource();// TimeSpan.FromMilliseconds(milliSeconds));
        await nes.RunAsync(cancellationTokenSource.Token);
    }
}
