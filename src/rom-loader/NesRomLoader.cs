using rom;

namespace rom_loader;

/// <summary>
/// Provides functionality to load NES ROM files.
/// </summary>
public class NesRomLoader: IRomLoader
{
    // https://www.nesdev.org/wiki/INES

    /// <summary>
    /// Asynchronously loads a NES ROM from a file path.
    /// </summary>
    /// <param name="filePath">The path to the ROM file.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the loaded NES ROM.</returns>
    /// <exception cref="ArgumentException">Thrown when file path is null or empty.</exception>
    public async Task<NesRom> LoadRomAsync(string filePath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
        byte[] romData = await File.ReadAllBytesAsync(filePath, cancellationToken);
     
        return LoadRom(romData);
    }

    /// <summary>
    /// Loads a NES ROM from a byte array.
    /// </summary>
    /// <param name="romData">The ROM data as a byte array.</param>
    /// <returns>The loaded NES ROM.</returns>
    public NesRom LoadRom(byte[] romData)
    {
        return LoadRom(romData.AsSpan());
    }

    /// <summary>
    /// Loads a NES ROM from a read-only span of bytes.
    /// </summary>
    /// <param name="bytes">The ROM data as a read-only span.</param>
    /// <returns>The loaded NES ROM.</returns>
    /// <exception cref="ArgumentException">Thrown when ROM data is too short.</exception>
    /// <exception cref="InvalidDataException">Thrown when ROM data is invalid or corrupted.</exception>
    public NesRom LoadRom(ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length < NesRomHeader.HeaderSize)
            throw new ArgumentException("ROM data is too short to contain a valid NES header.");
        
        NesRomHeader header = new(bytes[.. NesRomHeader.HeaderSize]);
        if (header.MagicNumber[0] != 'N' || header.MagicNumber[1] != 'E' || header.MagicNumber[2] != 'S')
            throw new InvalidDataException("Invalid NES ROM: Magic number does not match.");
        if (header.PrgRomSize == 0)
            throw new InvalidDataException("Invalid NES ROM: PRG ROM size is zero.");
        
        int prgRomSize = header.PrgRomSize * 16384; // 16KB units
        int chrRomSize = header.ChrRomSize * 8192; // 8KB units (can be 0 for CHR RAM)
        
        if (bytes.Length < NesRomHeader.HeaderSize + prgRomSize + chrRomSize)
            throw new InvalidDataException("Invalid NES ROM: Data size does not match header sizes.");

        var rom = new NesRom(
            header,
            bytes.Slice(NesRomHeader.HeaderSize, prgRomSize).ToArray(),
            chrRomSize > 0 ? bytes.Slice(NesRomHeader.HeaderSize + prgRomSize, chrRomSize).ToArray() : []
        );

        return rom;

    }
}
