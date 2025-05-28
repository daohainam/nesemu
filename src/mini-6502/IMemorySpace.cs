namespace mini_6502; 
internal interface IMemorySpace
{
    byte Read(ushort address);
    void Write(ushort address, byte value);
    void MapMemory(ushort start, ushort end, IMemoryMappable memoryMappable);
}