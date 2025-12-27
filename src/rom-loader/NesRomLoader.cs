using rom;

namespace rom_loader;
public class NesRomLoader: IRomLoader
{
    // https://www.nesdev.org/wiki/INES
    public async Task<NesRom> LoadRomAsync(string filePath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
        byte[] romData = await File.ReadAllBytesAsync(filePath, cancellationToken);
     
        return LoadRom(romData);
    }

    public NesRom LoadRom(byte[] romData)
    {
        return LoadRom(romData.AsSpan());
    }

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
