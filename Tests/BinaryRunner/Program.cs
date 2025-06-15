using Microsoft.Extensions.Logging.Abstractions;
using mini_6502;
using rom;
using System.IO;

// Read ROM data from file
var romPath = @"roms\registers.nes";

if (!File.Exists(romPath))
{
    Console.WriteLine($"ROM file not found: {romPath}");
    return;
}

var romRoader = new rom_loader.NesRomLoader();

var rom = await romRoader.LoadRomAsync(romPath);

var nes = new NES(rom, NullLoggerFactory.Instance);

nes.Reset();

var cancellationTokenSource = new CancellationTokenSource();
var cancellationToken = new CancellationTokenSource();

Console.WriteLine("Press any key to stop the NES tests...");

var t = nes.RunAsync(cancellationToken.Token).ConfigureAwait(false);

Console.ReadKey();

cancellationTokenSource.Cancel();
await t; // Wait for the NES to finish running

