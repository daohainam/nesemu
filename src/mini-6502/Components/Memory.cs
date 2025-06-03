namespace mini_6502.Components;
internal class Memory : IMemory
{
    private readonly byte[] ram = new byte[2048];
    private readonly byte[] cartridge = new byte[0x8000];
    private readonly Ppu ppu; // a plugable model is more flexible but for simplicity we use a specific PPU instance

    public Memory(Ppu ppu)
    {
        this.ppu = ppu;

        Array.Clear(ram, 0, ram.Length);
        Array.Clear(cartridge, 0, cartridge.Length);
    }

    public byte Read(ushort address)
    {
        if (address < 0x2000)
        {
            // 2KB RAM + mirror 3 lần
            return ram[address % 0x0800];
        }
        else if (address >= 0x2000 && address <= 0x2007)
        {
            // PPU Registers
            return ppu.ReadRegister((ushort)(address - 0x2000));
        }
        //else if (address < 0x4018)
        //{
        //    // APU and I/O Registers
        //}
        else if (address >= 0x8000)
        {
            // PRG ROM từ cartridge
            return cartridge[address - 0x8000];
        }
        else
        {
            // TODO: xử lý APU, I/O, expansion
            return 0x00;
        }
    }

    public void Write(ushort address, byte value)
    {
        if (address < 0x2000)
        {
            ram[address % 0x0800] = value;
        }
        else if (address >= 0x2000 && address <= 0x2007)
        {
            // PPU Registers
            ppu.WriteRegister((ushort)(address - 0x2000), value);
        }
        else if (address >= 0x8000)
        {
            cartridge[address - 0x8000] = value;
        }
        else
        {
            // TODO: xử lý APU, cartridge RAM, v.v.
        }
    }

    public byte this[ushort i]
    {
        get => Read(i);
        set => Write(i, value);
    }

    public void LoadCartridge(byte[] data)
    {
        if (data.Length > cartridge.Length)
            throw new ArgumentException("Cartridge data exceeds maximum size.");
        Array.Copy(data, cartridge, data.Length);
    }
}
