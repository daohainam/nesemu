namespace rom_loader;
public interface IRomLoader
{
    NesRom LoadRom(byte[] romData);
    NesRom LoadRom(ReadOnlySpan<byte> bytes);
    Task<NesRom> LoadRomAsync(string filePath, CancellationToken cancellationToken = default);

}
