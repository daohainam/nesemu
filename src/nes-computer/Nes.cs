using mini_6502.Components;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]

namespace mini_6502;
public class Nes
{
    private int clockFrequency = 1789773; // Default clock frequency
    private int milliSecondsPerClock = 1000000 / 1789773; // Convert clock frequency to milliseconds per clock cycle

    public int ClockFrequency
    {
        get => clockFrequency;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Clock frequency must be positive.");
            
            clockFrequency = value;
            milliSecondsPerClock = 1000000 / clockFrequency;
        }
    }

    private readonly Cpu cpu;
    private readonly Memory memory;
    private readonly Ppu ppu;
    public Nes()
    {
        ppu = new Ppu();
        memory = new Memory(ppu);
        cpu = new Cpu(memory);
    }
    public void Reset()
    {
        cpu.Reset();
        ppu.Reset();
    }
    public void Clock()
    {
        ppu.Clock();
        cpu.Clock();
    }

    public void LoadCartridge(byte[] romData)
    {
        if (romData.Length < 0x8000)
            throw new ArgumentException("ROM data must be at least 32KB long.");

        memory.LoadCartridge(romData);
        memory.Write(0xFFFC, 0x00); // Reset vector low byte
        memory.Write(0xFFFD, 0x80); // Reset vector high byte
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        Reset();
        while (!cancellationToken.IsCancellationRequested)
        {
            var startTime = DateTime.UtcNow;

            Console.WriteLine("Clock!");
            Clock();

            var timeElapsed = DateTime.UtcNow - startTime;
            if (timeElapsed.TotalMilliseconds < milliSecondsPerClock)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(milliSecondsPerClock - timeElapsed.TotalMilliseconds), cancellationToken);
            }
        }
    }
}
