namespace mini_6502.Components;
internal static class CpuExtensions
{
    public static void SetZNFlagsByValue(this Cpu cpu, byte value)
    {
        cpu.SetFlag(Flags.FLAG_ZERO, value == 0);
        cpu.SetFlag(Flags.FLAG_NEGATIVE, (value & 0x80) != 0);
    }
}
