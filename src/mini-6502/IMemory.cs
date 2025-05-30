namespace mini_6502; 
internal interface IMemory
{
    byte Read(ushort address);
    void Write(ushort address, byte value);
}