namespace mini_6502;
internal static class MemoryExtensions
{
    public static ushort ReadAsUshort(this IMemory memory, ushort address)
    {
        ushort s = memory.Read(address);
        return s;
    }
}
