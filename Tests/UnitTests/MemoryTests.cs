using Microsoft.Extensions.Logging.Abstractions;
using mini_6502;
using mini_6502.Components;
using System;

namespace UnitTests;
public class MemoryTests
{
    [Fact]
    public void ReadWriteMemory_MainRam_Test()
    {
        // Arrange
        var ppu = new Ppu();
        var memory = new Memory(MapperFactory.CreateMapper0(), ppu);
        byte salt = 0x42;

        // Act
        for (ushort i = 0; i < 2048; i++)
        {
            memory.Write(i, (byte)((i + salt) % 256));
        }


        // Read back the value
        for (ushort i = 0; i < 2048; i++)
        {
            byte valueRead = memory.Read(i);
            Assert.Equal((byte)((i + salt) % 256), valueRead);
        }
    }

    [Fact]
    public void ReadWriteMemory_Cartridge_Test()
    {
        // Arrange
        var ppu = new Ppu();
        var memory = new Memory(MapperFactory.CreateMapper0(), ppu);
        byte salt = 0x77;

        // Act
        for (ushort i = 0; i < 0x8000; i++)
        {
            memory.Write((ushort)(i + 0x8000), (byte)((i + salt) % 256));
        }


        // Read back the value
        for (ushort i = 0; i < 0x8000; i++)
        {
            byte valueRead = memory.Read((ushort)(i + 0x8000));
            Assert.Equal((byte)((i + salt) % 256), valueRead);
        }
    }
}
