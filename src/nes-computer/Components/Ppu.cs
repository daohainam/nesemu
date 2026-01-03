namespace mini_6502.Components;

internal class Ppu: IPpu
{
    private const int ScreenWidth = 256;
    private const int ScreenHeight = 240;
    private const int TotalScanlines = 262;
    private const int CyclesPerScanline = 341;
    
    private readonly IMapper? mapper;
    private readonly byte[] vram = new byte[0x800];
    private readonly byte[] oam = new byte[256];
    private readonly byte[] paletteRam = new byte[32];
    private readonly byte[] screenBuffer = new byte[ScreenWidth * ScreenHeight * 4];
    private readonly byte[] registers = new byte[8];
    
    private int cycle;
    private int scanline;
    private ushort vramAddress;
    private ushort tempVramAddress;
    private byte fineXScroll;
    private bool writeToggle;
    private byte dataBuffer;
    
    private const int PPUCTRL = 0;
    private const int PPUMASK = 1;
    private const int PPUSTATUS = 2;
    private const int OAMADDR = 3;
    private const int OAMDATA = 4;
    private const int PPUSCROLL = 5;
    private const int PPUADDR = 6;
    private const int PPUDATA = 7;

    public event EventHandler? FrameComplete;

    public Ppu() : this(null)
    {
    }

    public Ppu(IMapper? mapper)
    {
        this.mapper = mapper;
    }

    public byte ReadRegister(ushort addr)
    {
        addr = (ushort)(addr % 8);
        
        switch (addr)
        {
            case PPUSTATUS:
                byte status = registers[PPUSTATUS];
                registers[PPUSTATUS] &= 0x7F;
                writeToggle = false;
                return status;
                
            case OAMDATA:
                return oam[registers[OAMADDR]];
                
            case PPUDATA:
                byte data = dataBuffer;
                dataBuffer = ReadVram(vramAddress);
                
                if (vramAddress >= 0x3F00)
                {
                    data = dataBuffer;
                }
                
                vramAddress += (ushort)((registers[PPUCTRL] & 0x04) != 0 ? 32 : 1);
                return data;
                
            default:
                return registers[addr];
        }
    }

    public void WriteRegister(ushort addr, byte value)
    {
        addr = (ushort)(addr % 8);
        registers[addr] = value;
        
        switch (addr)
        {
            case PPUCTRL:
                tempVramAddress = (ushort)((tempVramAddress & 0xF3FF) | ((value & 0x03) << 10));
                break;
                
            case OAMADDR:
                break;
                
            case OAMDATA:
                oam[registers[OAMADDR]] = value;
                registers[OAMADDR]++;
                break;
                
            case PPUSCROLL:
                if (!writeToggle)
                {
                    fineXScroll = (byte)(value & 0x07);
                    tempVramAddress = (ushort)((tempVramAddress & 0xFFE0) | (value >> 3));
                }
                else
                {
                    tempVramAddress = (ushort)((tempVramAddress & 0x8FFF) | ((value & 0x07) << 12));
                    tempVramAddress = (ushort)((tempVramAddress & 0xFC1F) | ((value & 0xF8) << 2));
                }
                writeToggle = !writeToggle;
                break;
                
            case PPUADDR:
                if (!writeToggle)
                {
                    tempVramAddress = (ushort)((tempVramAddress & 0x00FF) | ((value & 0x3F) << 8));
                }
                else
                {
                    tempVramAddress = (ushort)((tempVramAddress & 0xFF00) | value);
                    vramAddress = tempVramAddress;
                }
                writeToggle = !writeToggle;
                break;
                
            case PPUDATA:
                WriteVram(vramAddress, value);
                vramAddress += (ushort)((registers[PPUCTRL] & 0x04) != 0 ? 32 : 1);
                break;
        }
    }

    private byte ReadVram(ushort addr)
    {
        addr &= 0x3FFF;
        
        if (addr < 0x2000)
        {
            return mapper?.PpuRead(addr) ?? 0;
        }
        else if (addr < 0x3F00)
        {
            return vram[addr & 0x7FF];
        }
        else if (addr < 0x4000)
        {
            int paletteAddr = addr & 0x1F;
            if (paletteAddr >= 16 && (paletteAddr & 0x03) == 0)
            {
                paletteAddr -= 16;
            }
            return paletteRam[paletteAddr];
        }
        
        return 0;
    }

    private void WriteVram(ushort addr, byte value)
    {
        addr &= 0x3FFF;
        
        if (addr < 0x2000)
        {
            mapper?.PpuWrite(addr, value);
        }
        else if (addr < 0x3F00)
        {
            vram[addr & 0x7FF] = value;
        }
        else if (addr < 0x4000)
        {
            int paletteAddr = addr & 0x1F;
            if (paletteAddr >= 16 && (paletteAddr & 0x03) == 0)
            {
                paletteAddr -= 16;
            }
            paletteRam[paletteAddr] = value;
        }
    }

    public void Clock()
    {
        if (scanline < ScreenHeight)
        {
            if (cycle > 0 && cycle <= ScreenWidth)
            {
                int pixelIndex = (scanline * ScreenWidth + (cycle - 1)) * 4;
                
                if (pixelIndex >= 0 && pixelIndex + 3 < screenBuffer.Length)
                {
                    byte paletteIndex = 0;
                    
                    bool backgroundEnabled = (registers[PPUMASK] & 0x08) != 0;
                    
                    if (backgroundEnabled && mapper != null)
                    {
                        int x = cycle - 1;
                        int y = scanline;
                        
                        int tileX = x / 8;
                        int tileY = y / 8;
                        
                        if (tileX < 32 && tileY < 30)
                        {
                            int pixelX = x % 8;
                            int pixelY = y % 8;
                            
                            ushort nametableBase = (ushort)(0x2000 | ((registers[PPUCTRL] & 0x03) << 10));
                            int nametableAddr = nametableBase + tileY * 32 + tileX;
                            byte tileIndex = ReadVram((ushort)nametableAddr);
                            
                            ushort patternTableBase = (ushort)(((registers[PPUCTRL] & 0x10) != 0) ? 0x1000 : 0x0000);
                            int patternAddr = patternTableBase + tileIndex * 16 + pixelY;
                            
                            byte tileLow = ReadVram((ushort)patternAddr);
                            byte tileHigh = ReadVram((ushort)(patternAddr + 8));
                            
                            int bitShift = 7 - pixelX;
                            byte pixelLow = (byte)((tileLow >> bitShift) & 1);
                            byte pixelHigh = (byte)((tileHigh >> bitShift) & 1);
                            paletteIndex = (byte)((pixelHigh << 1) | pixelLow);
                            
                            if (paletteIndex > 0)
                            {
                                int attributeAddr = nametableBase + 0x3C0 + (tileY / 4) * 8 + (tileX / 4);
                                byte attribute = ReadVram((ushort)attributeAddr);
                                
                                int attributeShift = ((tileY % 4) / 2) * 4 + ((tileX % 4) / 2) * 2;
                                byte paletteNum = (byte)((attribute >> attributeShift) & 0x03);
                                
                                paletteIndex = (byte)(paletteNum * 4 + paletteIndex);
                            }
                        }
                    }
                    
                    byte colorValue = paletteRam[paletteIndex & 0x1F];
                    
                    screenBuffer[pixelIndex] = colorValue;
                    screenBuffer[pixelIndex + 1] = colorValue;
                    screenBuffer[pixelIndex + 2] = colorValue;
                    screenBuffer[pixelIndex + 3] = 255;
                }
            }
        }
        
        cycle++;
        if (cycle >= CyclesPerScanline)
        {
            cycle = 0;
            scanline++;
            if (scanline >= TotalScanlines)
            {
                scanline = 0;
            }
        }
        
        if (scanline == 241 && cycle == 1)
        {
            registers[PPUSTATUS] |= 0x80;
            
            if ((registers[PPUCTRL] & 0x80) != 0)
            {
                OnFrameComplete();
            }
        }
        else if (scanline == 261 && cycle == 1)
        {
            registers[PPUSTATUS] &= 0x7F;
        }
    }

    public void Reset()
    {
        cycle = 0;
        scanline = 0;
        vramAddress = 0;
        tempVramAddress = 0;
        fineXScroll = 0;
        writeToggle = false;
        dataBuffer = 0;
        Array.Clear(registers);
        Array.Clear(screenBuffer);
    }

    public byte[] GetScreenBuffer()
    {
        return screenBuffer;
    }

    protected virtual void OnFrameComplete()
    {
        FrameComplete?.Invoke(this, EventArgs.Empty);
    }
}
