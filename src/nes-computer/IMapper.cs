namespace mini_6502;

internal interface IMapper
{
    byte CpuRead(ushort addr);
    void CpuWrite(ushort addr, byte value);

    byte PpuRead(ushort addr);
    void PpuWrite(ushort addr, byte value);
}
