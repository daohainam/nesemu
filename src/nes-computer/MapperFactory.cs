using rom;

namespace mini_6502;
internal class MapperFactory
{
    public static IMapper CreateMapper(NesRom rom)
    {
        if (rom.Header.MapperId == 0)
        {
            return new Mappers.Mapper0(rom);
        }

        throw new NotSupportedException($"Mapper type {rom.Header.MapperId} is not supported.");
    }

    public static IMapper CreateMapper0() // For testing purposes, create a default Mapper0 instance
    {
        return new Mappers.Mapper0();
    }
}
