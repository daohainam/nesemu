using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace nes_ui;

public partial class KeyMappingWindow : Window
{
    private readonly Dictionary<Key, NesButton> _keyMapping;
    private Button? _currentMappingButton;
    private readonly Dictionary<NesButton, Key> _defaultMapping = new()
    {
        { NesButton.A, Key.Z },
        { NesButton.B, Key.X },
        { NesButton.Select, Key.A },
        { NesButton.Start, Key.S },
        { NesButton.Up, Key.Up },
        { NesButton.Down, Key.Down },
        { NesButton.Left, Key.Left },
        { NesButton.Right, Key.Right }
    };

    public KeyMappingWindow(Dictionary<Key, NesButton> keyMapping)
    {
        InitializeComponent();
        _keyMapping = keyMapping;
        UpdateButtonLabels();
    }

    private void UpdateButtonLabels()
    {
        foreach (var kvp in _keyMapping)
        {
            var button = kvp.Value switch
            {
                NesButton.A => ButtonA,
                NesButton.B => ButtonB,
                NesButton.Select => ButtonSelect,
                NesButton.Start => ButtonStart,
                NesButton.Up => ButtonUp,
                NesButton.Down => ButtonDown,
                NesButton.Left => ButtonLeft,
                NesButton.Right => ButtonRight,
                _ => null
            };

            if (button != null)
            {
                button.Content = kvp.Key.ToString();
            }
        }
    }

    private void MapButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            _currentMappingButton = button;
            button.Content = "Press a key...";
            button.Focus();
        }
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (_currentMappingButton != null && e.Key != Key.Escape)
        {
            var buttonTag = _currentMappingButton.Tag.ToString();
            if (Enum.TryParse<NesButton>(buttonTag, out var nesButton))
            {
                var existingMapping = _keyMapping.FirstOrDefault(x => x.Value == nesButton);
                if (existingMapping.Key != default)
                {
                    _keyMapping.Remove(existingMapping.Key);
                }

                var duplicateKey = _keyMapping.FirstOrDefault(x => x.Key == e.Key);
                if (duplicateKey.Key != default)
                {
                    _keyMapping.Remove(duplicateKey.Key);
                }

                _keyMapping[e.Key] = nesButton;
                _currentMappingButton.Content = e.Key.ToString();
            }

            _currentMappingButton = null;
            e.Handled = true;
        }
    }

    private void ResetToDefault_Click(object sender, RoutedEventArgs e)
    {
        _keyMapping.Clear();
        foreach (var kvp in _defaultMapping)
        {
            _keyMapping[kvp.Value] = kvp.Key;
        }
        UpdateButtonLabels();
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
