namespace rom_loader;
public class NesRom(NesRomHeader header, byte[] prgRom, byte[] chrRom)
{
    public NesRomHeader Header { get; private set; } = header;
    public byte[] PrgRom { get; private set; } = prgRom;
    public byte[] ChrRom { get; private set; } = chrRom;
}
