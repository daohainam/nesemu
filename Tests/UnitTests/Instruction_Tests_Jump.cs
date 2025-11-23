using mini_6502;

namespace UnitTests;

public partial class Instruction_Tests_Jump : Instruction_Tests
{
    /// <summary>
    /// JSR must:
    /// - Push the address of the last byte of the instruction (PC_return = I+2) onto the stack (high then low)
    /// - Decrement SP by 2
    /// - Jump PC to the target
    /// </summary>
    [Fact]
    public void JSR_Instruction_PushesReturnAddressOnStack()
    {
        // Arrange
        cpu.SP = 0xFF;                    // stack "empty"
        cpu.PC = cartridgeAddress;        // I = cartridgeAddress (e.g. 0x8000)

        ushort targetAddress = 0x9000;

        // JSR $9000
        memory.Write(cartridgeAddress, 0x20);                        // JSR abs
        memory.Write((ushort)(cartridgeAddress + 1), (byte)(targetAddress & 0xFF));   // low
        memory.Write((ushort)(cartridgeAddress + 2), (byte)(targetAddress >> 8));     // high

        // Act
        cpu.Clock(); // execute JSR

        // Assert: PC has jumped to target
        Assert.Equal(targetAddress, cpu.PC);

        // SP decreased by 2 (from 0xFF -> 0xFD)
        Assert.Equal(0xFD, cpu.SP);

        // 6502 standard return address = address of the last byte of the instruction = I+2
        ushort expectedReturnAddress = (ushort)(cartridgeAddress + 2);
        byte expectedLow = (byte)(expectedReturnAddress & 0xFF);
        byte expectedHigh = (byte)(expectedReturnAddress >> 8);

        // After pushing high, then low:
        //  SP_before = 0xFF
        //  write high @ 0x01FF, SP=0xFE
        //  write low  @ 0x01FE, SP=0xFD
        //
        // => positions:
        //   [0x0100 + SP + 1] = low  (0x01FE)
        //   [0x0100 + SP + 2] = high (0x01FF)

        Assert.Equal(expectedLow, memory.Read((ushort)(0x0100 + cpu.SP + 1)));
        Assert.Equal(expectedHigh, memory.Read((ushort)(0x0100 + cpu.SP + 2)));
    }

    /// <summary>
    /// RTS must:
    /// - Read low, high from stack (increment SP before each read)
    /// - PC = returnAddress + 1 (to the correct byte after JSR)
    /// - SP increases back by 2
    /// </summary>
    [Fact]
    public void RTS_Instruction_PopsReturnAddressFromStack()
    {
        // Simulate state after a JSR instruction at I = cartridgeAddress
        // Return address assumed to be I+2
        ushort returnAddress = (ushort)(cartridgeAddress + 2);
        byte low = (byte)(returnAddress & 0xFF);
        byte high = (byte)(returnAddress >> 8);

        // After a correct JSR:
        //   SP = 0xFD
        //   [0x01FE] = low
        //   [0x01FF] = high
        cpu.SP = 0xFD;
        memory.Write(0x01FE, low);
        memory.Write(0x01FF, high);

        // Place RTS at cartridgeAddress
        cpu.PC = cartridgeAddress;
        memory.Write(cartridgeAddress, 0x60); // RTS

        // Act
        cpu.Clock(); // execute RTS

        // RTS will:
        //   ++SP -> 0xFE, read low (0x01FE)
        //   ++SP -> 0xFF, read high (0x01FF)
        //   PC = high:low = returnAddress
        //   PC++ -> returnAddress + 1 = I+3
        Assert.Equal((ushort)(returnAddress + 1), cpu.PC);

        // SP returns to 0xFF (as before JSR)
        Assert.Equal(0xFF, cpu.SP);
    }

    /// <summary>
    /// Sequence JSR -> (subroutine contains RTS) -> RTS must return to the correct instruction after JSR.
    /// </summary>
    [Fact]
    public void JSR_Then_RTS_JumpsBackToCaller()
    {
        cpu.SP = 0xFF;
        cpu.PC = cartridgeAddress; // I

        // Program:
        //   I:    JSR $8005
        //   I+3:  LDA #$42
        //   I+5:  RTS
        ushort I = cartridgeAddress;
        ushort subroutineAddress = (ushort)(I + 5);

        // JSR $8005 (subroutineAddress)
        memory.Write(I, 0x20);                               // JSR abs
        memory.Write((ushort)(I + 1), (byte)(subroutineAddress & 0xFF));  // low
        memory.Write((ushort)(I + 2), (byte)(subroutineAddress >> 8));    // high

        // LDA #$42 (instruction after JSR – we will not execute it in this test, only use its address)
        memory.Write((ushort)(I + 3), 0xA9); // opcode LDA imm
        memory.Write((ushort)(I + 4), 0x42); // immediate value

        // Subroutine: RTS
        memory.Write(subroutineAddress, 0x60); // RTS

        // Act 1: execute JSR
        StepInstruction();

        // After JSR:
        Assert.Equal(subroutineAddress, cpu.PC);
        Assert.Equal(0xFD, cpu.SP); // two bytes were pushed onto the stack

        // Act 2: execute RTS in the subroutine
        StepInstruction();

        // After RTS, PC must return to the instruction after JSR = I + 3
        Assert.Equal((ushort)(I + 3), cpu.PC);
        // SP returns to 0xFF
        Assert.Equal(0xFF, cpu.SP);
    }
}
