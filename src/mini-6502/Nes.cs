using mini_6502.Components;

namespace mini_6502;
public class Nes
{
    private int clockFrequency = 1789773; // Default clock frequency
    private int milliSecondsPerClock = 1000000 / 1789773; // Convert clock frequency to nanoseconds per clock cycle

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

    public void LoadRom(byte[] romData)
    {
        if (romData.Length < 0x8000)
            throw new ArgumentException("ROM data must be at least 32KB long.");
        for (int i = 0; i < 0x8000; i++)
        {
            memory.Write((ushort)(0x8000 + i), romData[i]);
        }
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        Reset();
        while (!cancellationToken.IsCancellationRequested)
        {
            var startTime = DateTime.UtcNow;
            Clock();
            
            var timeElapsed = DateTime.UtcNow - startTime;
            if (timeElapsed.TotalMilliseconds < milliSecondsPerClock)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(milliSecondsPerClock - timeElapsed.TotalMilliseconds), cancellationToken);
            }
        }
    }
}
