namespace rom;

/// <summary>
/// Represents a NES ROM with header, PRG ROM, and CHR ROM data.
/// </summary>
public class NesRom(NesRomHeader header, byte[] prgRom, byte[] chrRom)
{
    /// <summary>
    /// Gets the ROM header containing metadata about the ROM.
    /// </summary>
    public NesRomHeader Header { get; private set; } = header;

    /// <summary>
    /// Gets the PRG ROM data (program code).
    /// </summary>
    public byte[] PrgRom { get; private set; } = prgRom;

    /// <summary>
    /// Gets the CHR ROM data (graphics data). Can be empty for CHR RAM.
    /// </summary>
    public byte[] ChrRom { get; private set; } = chrRom;
}
