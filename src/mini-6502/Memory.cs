namespace mini_6502;
internal class Memory : IMemorySpace
{
    public void MapMemory(ushort start, ushort end, IMemoryMappable memoryMappable)
    {
        throw new NotImplementedException();
    }

    public byte Read(ushort address)
    {
        throw new NotImplementedException();
    }

    public void Write(ushort address, byte value)
    {
        throw new NotImplementedException();
    }
}
