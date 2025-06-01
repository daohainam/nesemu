using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rom_loader;
public class NesRomHeader
{
    public const int HeaderSize = 16; // NES file header size in bytes
    public byte[] MagicNumber { get; } = new byte[4]; // "NES" magic number
    public byte PrgRomSize { get; set; } // Size of PRG ROM in 16KB units
    public byte ChrRomSize { get; set; } // Size of CHR ROM in 8KB units
    public byte Flags6 { get; set; } // Flags 6
    public byte Flags7 { get; set; } // Flags 7
    public byte PrgRamSize { get; set; } // Size of PRG RAM in 8KB units (usually 0)
    public byte Flags9 { get; set; } // Flags 9
    public byte Flags10 { get; set; } // Flags 10
    public ushort MapperId => (ushort)((Flags6 >> 4) | (Flags7 & 0xF0));
    public NesRomHeader(byte[] headerData): this(headerData.AsSpan())
    {
    }

    public NesRomHeader(ReadOnlySpan<byte> readOnlySpan)
    {
        MagicNumber = new byte[4];
        if (readOnlySpan.Length < HeaderSize)
            throw new ArgumentException("Header data is too short.");

        readOnlySpan[..4].CopyTo(MagicNumber);
        PrgRomSize = readOnlySpan[4];
        ChrRomSize = readOnlySpan[5];
        Flags6 = readOnlySpan[6];
        Flags7 = readOnlySpan[7];
        PrgRamSize = readOnlySpan[8];
        Flags9 = readOnlySpan[9];
        Flags10 = readOnlySpan[10];
    }
}
