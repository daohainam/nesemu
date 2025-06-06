using mini_6502;
using System.IO;

var nes = new Nes();

// Read ROM data from file
var romPath = @"roms\registers.nes";
byte[] romData;
if (File.Exists(romPath))
{
    romData = File.ReadAllBytes(romPath);
}
else
{
    Console.WriteLine($"ROM file not found: {romPath}");
    return;
}

nes.LoadCartridge(romData);

var cancellationTokenSource = new CancellationTokenSource();
var cancellationToken = new CancellationTokenSource();

Console.WriteLine("Press any key to stop the NES tests...");

var t = nes.RunAsync(cancellationToken.Token).ConfigureAwait(false);

Console.ReadKey();

cancellationTokenSource.Cancel();
await t; // Wait for the NES to finish running

