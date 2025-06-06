namespace mini_6502; 
internal interface IMemory
{
    byte this[ushort i] { get; set; }

    byte Read(ushort address);
    void Write(ushort address, byte value);    
}