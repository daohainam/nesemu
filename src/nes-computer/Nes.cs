using mini_6502.Components;
using rom;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]

namespace mini_6502;
public class NES
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

    private Cpu cpu;
    private Memory memory;
    private Ppu ppu;

    public NES()
    {
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

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            // var startTime = DateTime.UtcNow;

            Console.WriteLine("Clock!");
            Clock();

            //var timeElapsed = DateTime.UtcNow - startTime;
            //if (timeElapsed.TotalMilliseconds < milliSecondsPerClock)
            //{
            //    await Task.Delay(TimeSpan.FromMilliseconds(milliSecondsPerClock - timeElapsed.TotalMilliseconds), cancellationToken);
            //}
        }
    }

    public void LoadRom(NesRom rom)
    {
        ArgumentNullException.ThrowIfNull(rom, nameof(rom));

        var mapper = MapperFactory.CreateMapper(rom) ?? throw new InvalidOperationException("Unsupported mapper type in the ROM.");

        ppu = new Ppu();
        memory = new Memory(mapper, ppu);
        cpu = new Cpu(memory);
    }
}
