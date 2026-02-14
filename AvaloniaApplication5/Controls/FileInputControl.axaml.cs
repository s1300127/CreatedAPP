using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace AvaloniaApplication5.Controls
{
    public partial class FileInputControl : UserControl
    {
        public event Action<string>? FileLoaded;
        public FileInputControl()
        {
            InitializeComponent();
        }
        private void MyButton_Clicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var path = InputedFilePath.Text;

            if(string.IsNullOrWhiteSpace(path))return;

            FileLoaded?.Invoke(path);
        }
    }
}