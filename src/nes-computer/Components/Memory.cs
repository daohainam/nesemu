using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace mini_6502.Components;
internal class Memory(IMapper mapper, Ppu ppu, ILogger<Memory>? logger = null) : IMemory
{
    private readonly ILogger<Memory> logger = logger ?? NullLogger<Memory>.Instance;
    private readonly byte[] ram = new byte[0x0800]; // 2KB RAM (mirrored)
    private readonly IMapper mapper = mapper;
    private readonly Ppu ppu = ppu;

    public byte Read(ushort addr)
    {
        if (addr < 0x2000)
            return ram[addr % 0x0800];
        else if (addr >= 0x2000 && addr < 0x4000)
            return ppu.ReadRegister((ushort)(0x2000 + (addr % 8)));
        else if (addr >= 0x8000)
            return mapper.CpuRead(addr);
        else
            return 0;
    }

    public void Write(ushort addr, byte value)
    {
        if (addr < 0x2000)
            ram[addr % 0x0800] = value;
        else if (addr >= 0x2000 && addr < 0x4000)
            ppu.WriteRegister((ushort)(0x2000 + (addr % 8)), value);
        else if (addr >= 0x8000)
            mapper.CpuWrite(addr, value); // có thể bỏ qua nếu ROM không ghi
    }

    public byte this[ushort i]
    {
        get => Read(i);
        set => Write(i, value);
    }
}
