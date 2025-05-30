namespace mini_6502;
internal interface IPpu
{
    void Clock();
    byte ReadRegister(ushort addr);
    void Reset();
    void WriteRegister(ushort addr, byte value);
}