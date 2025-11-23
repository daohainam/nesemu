/*
Pseudocode / Plan:
- For each stack-related opcode implementation:
  - Obtain `cpu` and `memory` from the provided `context`.
- `OpPHA`:
  - Write `cpu.A` to address (0x0100 + cpu.SP).
  - Decrement `cpu.SP` after writing (post-decrement).
- `OpPHP`:
  - Build `statusToPush` by OR'ing `cpu.P` with the BREAK flag and ensuring bit 5 is set (0x20).
  - Write `statusToPush` to address (0x0100 + cpu.SP).
  - Decrement `cpu.SP` after writing (post-decrement).
- `OpPLA`:
  - Increment `cpu.SP` first (pre-increment).
  - Read value from address (0x0100 + cpu.SP) into `cpu.A`.
  - Update Z and N flags based on the loaded `cpu.A`.
- `OpPLP`:
  - Increment `cpu.SP` first (pre-increment).
  - Read status byte from address (0x0100 + cpu.SP).
  - Ensure bit 5 is always set (OR with 0x20) and store into `cpu.P`.
*/

using mini_6502;
using mini_6502.Components;

namespace mini_6502.Instructions;

internal static class InstructionSet_Stack
{
    internal static void OpPHA(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;
        memory.Write((ushort)(0x0100 + cpu.SP--), cpu.A);
    }

    internal static void OpPHP(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;
        // Push P with B=1 and bit 5 set as on the 6502
        byte statusToPush = (byte)(cpu.P | Flags.FLAG_BREAK | 0x20);
        memory.Write((ushort)(0x0100 + cpu.SP--), statusToPush);
    }

    internal static void OpPLA(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;
        cpu.A = memory.Read((ushort)(0x0100 + ++cpu.SP));
        cpu.SetZNFlagsByValue(cpu.A);
    }

    internal static void OpPLP(InstructionContext context)
    {
        var cpu = context.Cpu;
        var memory = context.Memory;
        byte status = memory.Read((ushort)(0x0100 + ++cpu.SP));
        // bit 5 is always set
        status = (byte)(status | 0x20);
        cpu.P = status;
    }
}
