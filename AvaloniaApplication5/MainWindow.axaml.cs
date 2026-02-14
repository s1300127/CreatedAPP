using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Threading;
using AvaloniaApplication5.Controls;
using AvaloniaApplication5.Services;
using AvaloniaApplication5.Models;

namespace AvaloniaApplication5;

public partial class MainWindow : Window
{
    public MainWindow() 
    { 
        InitializeComponent();

        this.Opened += async (_,__) =>
        {
            await Dispatcher.UIThread.InvokeAsync(()=>
            {
                var fileInput = this.FindControl<FileInputControl>("FileInput");
                fileInput!.FileLoaded += OnPcapFileLoaded;
            }, DispatcherPriority.Background);
        };
    }

    private void InitializeGraph()
    {

        var renderer = new GraphRenderer(canvas);

        var (nodes, edges) = GraphInitializer.CreateTestGraph(
            canvas.Bounds.Width,
            canvas.Bounds.Height
        );

        renderer.AddNodes(nodes);
        renderer.AddEdges(edges);
    }

    private void OnPcapFileLoaded(string filePath)
    {
        var parsed = PcapParser.Parse(filePath);

        GraphRenderer renderer = new GraphRenderer(canvas);

        var (nodes, edges)= GraphInitializer.Initialize(
            canvas.Bounds.Width,
            canvas.Bounds.Height,
            parsed
        );

        renderer.AddNodes(parsed.Nodes);
        renderer.AddEdges(parsed.Edges);
    }
}