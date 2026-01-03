# NES Emulator UI

A WPF desktop application providing a graphical user interface for the NES emulator.

## Features

- **ROM Loading**: Open and load NES ROM files (.nes format) through a file dialog
- **Emulator Display**: Real-time display of the NES screen output (256x240 pixels, scaled 2x)
- **Keyboard Input**: Map keyboard keys to NES controller buttons
- **Key Mapping Configuration**: Customize keyboard mappings through an intuitive dialog

## Default Key Mappings

- **A Button**: Z
- **B Button**: X
- **Select**: A
- **Start**: S
- **D-Pad Up**: Arrow Up
- **D-Pad Down**: Arrow Down
- **D-Pad Left**: Arrow Left
- **D-Pad Right**: Arrow Right

## Usage

1. Launch the application
2. Go to **File → Open ROM...** to load a NES ROM file
3. The emulator will start automatically once the ROM is loaded
4. Use the keyboard to control the game (see default mappings above)
5. Customize key mappings via **Options → Key Mapping...**

**Note**: Controller input handling is implemented in the UI but requires controller support to be added to the core emulator (Memory component, addresses 0x4016-0x4017) before keyboard input can be sent to games.

## Building

The project targets .NET 9 with Windows-specific features (WPF) and supports AOT compilation.

```bash
dotnet build src/nes-ui/nes-ui.csproj
```

## Running

```bash
dotnet run --project src/nes-ui/nes-ui.csproj
```

Note: This project requires Windows or a Windows-compatible environment to run due to WPF dependencies.
