namespace mini_6502.Components;

internal class Ppu: IPpu
{
    //private readonly byte[] vram = new byte[0x4000];
    //private readonly byte[] oam = new byte[256];
    private readonly byte[] registers = new byte[8];
    private int cycle, scanline;

    public byte ReadRegister(ushort addr)
    {
        addr = (ushort)(addr % 8);
        return registers[addr];
    }

    public void WriteRegister(ushort addr, byte value)
    {
        addr = (ushort)(addr % 8);
        registers[addr] = value;
    }

    public void Clock()
    {
        cycle++;
        if (cycle >= 341)
        {
            cycle = 0;
            scanline++;
            if (scanline >= 261)
            {
                scanline = 0;
            }
        }
    }

    public void Reset()
    {
        cycle = 0;
        scanline = 0;
    }
}
