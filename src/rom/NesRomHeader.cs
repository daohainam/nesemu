using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rom;

/// <summary>
/// Represents the header of a NES ROM file in iNES format.
/// </summary>
public class NesRomHeader
{
    // http://fms.komkon.org/EMUL8/NES.html#LABM
    // https://www.nesdev.org/wiki/INES

    /// <summary>
    /// The size of the NES ROM header in bytes.
    /// </summary>
    public const int HeaderSize = 16;

    /// <summary>
    /// Gets the magic number (should be "NES" followed by MS-DOS EOF).
    /// </summary>
    public byte[] MagicNumber { get; } = new byte[4];

    /// <summary>
    /// Gets or sets the size of PRG ROM in 16KB units.
    /// </summary>
    public byte PrgRomSize { get; set; }

    /// <summary>
    /// Gets or sets the size of CHR ROM in 8KB units. Can be 0 for CHR RAM.
    /// </summary>
    public byte ChrRomSize { get; set; }

    /// <summary>
    /// Gets or sets flags 6 containing mapper, mirroring, battery, and trainer bits.
    /// </summary>
    public byte Flags6 { get; set; }

    /// <summary>
    /// Gets or sets flags 7 containing mapper and console type bits.
    /// </summary>
    public byte Flags7 { get; set; }

    /// <summary>
    /// Gets or sets the size of PRG RAM in 8KB units (usually 0).
    /// </summary>
    public byte PrgRamSize { get; set; }

    /// <summary>
    /// Gets or sets flags 9. Bit 0: 1 for PAL cartridges, otherwise assume NTSC.
    /// </summary>
    public byte Flags9 { get; set; }

    /// <summary>
    /// Gets or sets flags 10.
    /// </summary>
    public byte Flags10 { get; set; }

    /// <summary>
    /// Gets the mapper ID extracted from flags 6 and 7.
    /// </summary>
    public ushort MapperId => (ushort)(Flags6 >> 4 | Flags7 & 0xF0);

    /// <summary>
    /// Initializes a new instance of the <see cref="NesRomHeader"/> class from a byte array.
    /// </summary>
    /// <param name="headerData">The header data bytes.</param>
    public NesRomHeader(byte[] headerData): this(headerData.AsSpan())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NesRomHeader"/> class from a read-only span.
    /// </summary>
    /// <param name="readOnlySpan">The header data as a read-only span.</param>
    /// <exception cref="ArgumentException">Thrown when header data is too short.</exception>
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
