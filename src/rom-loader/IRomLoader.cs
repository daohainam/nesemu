using rom;

namespace rom_loader;

/// <summary>
/// Defines a contract for loading NES ROM files.
/// </summary>
public interface IRomLoader
{
    /// <summary>
    /// Loads a NES ROM from a byte array.
    /// </summary>
    /// <param name="romData">The ROM data as a byte array.</param>
    /// <returns>The loaded NES ROM.</returns>
    NesRom LoadRom(byte[] romData);

    /// <summary>
    /// Loads a NES ROM from a read-only span of bytes.
    /// </summary>
    /// <param name="bytes">The ROM data as a read-only span.</param>
    /// <returns>The loaded NES ROM.</returns>
    NesRom LoadRom(ReadOnlySpan<byte> bytes);

    /// <summary>
    /// Asynchronously loads a NES ROM from a file path.
    /// </summary>
    /// <param name="filePath">The path to the ROM file.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the loaded NES ROM.</returns>
    Task<NesRom> LoadRomAsync(string filePath, CancellationToken cancellationToken = default);

}
