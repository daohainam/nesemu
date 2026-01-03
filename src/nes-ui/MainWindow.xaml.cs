using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using mini_6502;
using rom;
using rom_loader;

namespace nes_ui;

public partial class MainWindow : Window
{
    private NES? _nes;
    private WriteableBitmap? _screenBitmap;
    private CancellationTokenSource? _emulationCts;
    private readonly Dictionary<Key, NesButton> _keyMapping;
    private readonly HashSet<NesButton> _pressedButtons;

    public MainWindow()
    {
        InitializeComponent();
        
        _keyMapping = new Dictionary<Key, NesButton>
        {
            { Key.Z, NesButton.A },
            { Key.X, NesButton.B },
            { Key.A, NesButton.Select },
            { Key.S, NesButton.Start },
            { Key.Up, NesButton.Up },
            { Key.Down, NesButton.Down },
            { Key.Left, NesButton.Left },
            { Key.Right, NesButton.Right }
        };
        
        _pressedButtons = new HashSet<NesButton>();
        
        _screenBitmap = new WriteableBitmap(256, 240, 96, 96, PixelFormats.Bgra32, null);
        ScreenImage.Source = _screenBitmap;
    }

    private async void OpenROM_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "NES ROM files (*.nes)|*.nes|All files (*.*)|*.*",
            Title = "Open NES ROM"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            try
            {
                await StopEmulation();
                
                var loader = new NesRomLoader();
                var rom = await loader.LoadRomAsync(openFileDialog.FileName);
                
                _nes = new NES(rom);
                _nes.FrameComplete += OnFrameComplete;
                _nes.Reset();
                
                await StartEmulation();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load ROM: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void KeyMapping_Click(object sender, RoutedEventArgs e)
    {
        var keyMappingWindow = new KeyMappingWindow(_keyMapping);
        keyMappingWindow.Owner = this;
        keyMappingWindow.ShowDialog();
    }

    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (_keyMapping.TryGetValue(e.Key, out var button))
        {
            _pressedButtons.Add(button);
            e.Handled = true;
        }
    }

    private void Window_KeyUp(object sender, KeyEventArgs e)
    {
        if (_keyMapping.TryGetValue(e.Key, out var button))
        {
            _pressedButtons.Remove(button);
            e.Handled = true;
        }
    }

    private async Task StartEmulation()
    {
        if (_nes == null) return;
        
        _emulationCts = new CancellationTokenSource();
        await Task.Run(() => _nes.RunAsync(_emulationCts.Token));
    }

    private async Task StopEmulation()
    {
        if (_emulationCts != null)
        {
            _emulationCts.Cancel();
            try
            {
                await Task.Delay(100);
            }
            catch { }
            _emulationCts.Dispose();
            _emulationCts = null;
        }
    }

    private void OnFrameComplete(object? sender, EventArgs e)
    {
        if (_nes == null || _screenBitmap == null) return;

        Dispatcher.Invoke(() =>
        {
            try
            {
                var buffer = _nes.ScreenBuffer;
                _screenBitmap.Lock();
                
                unsafe
                {
                    var backBuffer = (byte*)_screenBitmap.BackBuffer;
                    var stride = _screenBitmap.BackBufferStride;
                    
                    for (int y = 0; y < 240; y++)
                    {
                        for (int x = 0; x < 256; x++)
                        {
                            int srcIndex = (y * 256 + x) * 4;
                            int dstIndex = y * stride + x * 4;
                            
                            backBuffer[dstIndex] = buffer[srcIndex + 2];
                            backBuffer[dstIndex + 1] = buffer[srcIndex + 1];
                            backBuffer[dstIndex + 2] = buffer[srcIndex];
                            backBuffer[dstIndex + 3] = buffer[srcIndex + 3];
                        }
                    }
                }
                
                _screenBitmap.AddDirtyRect(new Int32Rect(0, 0, 256, 240));
                _screenBitmap.Unlock();
            }
            catch { }
        });
    }

    protected override async void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
        await StopEmulation();
        base.OnClosing(e);
    }
}