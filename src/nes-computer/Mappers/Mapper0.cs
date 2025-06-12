using rom;

namespace mini_6502.Mappers;

internal class Mapper0 : IMapper
{
    private readonly byte[] prgRom;
    private readonly byte[] chrRom;

    public Mapper0()
    {
        prgRom = new byte[0x4000]; 
        chrRom = new byte[0x2000]; 
    }

    public Mapper0(NesRom rom)
    {
        prgRom = rom.PrgRom;
        chrRom = rom.ChrRom.Length == 0 ? new byte[0x2000] : rom.ChrRom;
    }

    public byte CpuRead(ushort addr)
    {
        if (addr >= 0x8000 && addr <= 0xFFFF)
        {
            int offset = prgRom.Length == 0x4000
                ? (addr - 0x8000) % 0x4000
                : addr - 0x8000;
            return prgRom[offset];
        }
        return 0x00;
    }

    public void CpuWrite(ushort addr, byte value)
    {
        // Write to PRG RAM if the address is in the range 0x8000-0xFFFF, we assume PRG RAM is writable
        if (addr >= 0x8000 && addr <= 0xFFFF)
        {
            int offset = prgRom.Length == 0x4000
                ? (addr - 0x8000) % 0x4000
                : addr - 0x8000;
            prgRom[offset] = value;
        }
    }

    public byte PpuRead(ushort addr)
    {
        if (addr < 0x2000)
        {
            return chrRom[addr];
        }
        return 0x00;
    }

    public void PpuWrite(ushort addr, byte value)
    {
        // if CHR ROM is empty, we use PRG RAM (check flags8)
        if (chrRom.Length == 0x2000 && addr < 0x2000)
        {
            chrRom[addr] = value;
        }
    }
}
