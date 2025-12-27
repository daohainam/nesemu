using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using mini_6502.Components;
using rom;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]

namespace mini_6502;

/// <summary>
/// Represents a Nintendo Entertainment System (NES) emulator.
/// Manages the CPU, PPU, and memory components and provides methods to run the emulation.
/// </summary>
public class NES
{
    private int clockFrequency = 1789773; // Default clock frequency
    private int milliSecondsPerClock = 1000000 / 1789773; // Convert clock frequency to milliseconds per clock cycle

    /// <summary>
    /// Gets or sets the clock frequency in Hz. Default is 1789773 Hz (NTSC).
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when value is not positive.</exception>
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

    /// <summary>
    /// Initializes a new instance of the <see cref="NES"/> class with the specified ROM and optional logger factory.
    /// </summary>
    /// <param name="rom">The NES ROM to load.</param>
    /// <param name="loggerFactory">Optional logger factory for logging. If null, no logging will be performed.</param>
    /// <exception cref="ArgumentNullException">Thrown when rom is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the ROM's mapper type is not supported.</exception>
    public NES(NesRom rom, ILoggerFactory? loggerFactory = null)
    {
        ArgumentNullException.ThrowIfNull(rom, nameof(rom));

        loggerFactory ??= NullLoggerFactory.Instance;

        var mapper = MapperFactory.CreateMapper(rom) ?? throw new InvalidOperationException("Unsupported mapper type in the ROM.");

        ppu = new Ppu();
        memory = new Memory(mapper, ppu, loggerFactory.CreateLogger<Memory>());
        cpu = new Cpu(memory, loggerFactory.CreateLogger<Cpu>());
    }

    /// <summary>
    /// Resets the CPU and PPU to their initial states.
    /// </summary>
    public void Reset()
    {
        cpu.Reset();
        ppu.Reset();
    }

    /// <summary>
    /// Executes one clock cycle for both the PPU and CPU.
    /// </summary>
    public void Clock()
    {
        ppu.Clock();
        cpu.Clock();
    }

    /// <summary>
    /// Runs the emulator asynchronously until the cancellation token is triggered.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the execution.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            // var startTime = DateTime.UtcNow;

            Clock();

            //var timeElapsed = DateTime.UtcNow - startTime;
            //if (timeElapsed.TotalMilliseconds < milliSecondsPerClock)
            //{
            //    await Task.Delay(TimeSpan.FromMilliseconds(milliSecondsPerClock - timeElapsed.TotalMilliseconds), cancellationToken);
            //}
        }
    }
}
