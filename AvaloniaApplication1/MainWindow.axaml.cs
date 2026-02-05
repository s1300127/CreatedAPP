using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using SharpPcap;
using System;
using SharpPcap.LibPcap;
using PacketDotNet;

namespace AvaloniaApplication1;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    void MyButton_Clicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        string output = Packetcap.ReadPcapFile(InputedFilePath.Text!);
        MyTextBox.Text = output;
    }
}