namespace mini_6502;
internal class Flags
{
    public const byte FLAG_CARRY = 1 << 0;
    public const byte FLAG_ZERO = 1 << 1;
    public const byte FLAG_INTERRUPT = 1 << 2;
    public const byte FLAG_DECIMAL = 1 << 3;
    public const byte FLAG_BREAK = 1 << 4;
    public const byte FLAG_UNUSED = 1 << 5; // Unused
    public const byte FLAG_OVERFLOW = 1 << 6;
    public const byte FLAG_NEGATIVE = 1 << 7;
}
