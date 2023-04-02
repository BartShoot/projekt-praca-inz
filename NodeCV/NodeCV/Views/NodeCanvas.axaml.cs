using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;

namespace NodeCV.Views;

public partial class NodeCanvas : UserControl
{
    Point lastRightClickPosition;
    double scale;
    public NodeCanvas()
    {
        InitializeComponent();
        MainCanvas.PointerPressed += new EventHandler<PointerPressedEventArgs>(RightClickOnCanvas);
        MainCanvas.PointerWheelChanged += new EventHandler<PointerWheelEventArgs>(ScrollWheelOnCanvas);
        this.ContextMenu = new ContextMenu();
        var menuObj = new MenuItem();
        menuObj.Header = "Add node";
        menuObj.Click += new EventHandler<RoutedEventArgs>(AddNode); ;
        this.ContextMenu.Items.Add(menuObj);
    }

    private void ScrollWheelOnCanvas(object? sender, PointerWheelEventArgs e)
    {
        if (e.Delta.Y>0)
        {
            scale += e.Delta.Y;
            MainCanvas.SetValue(ScaleTransform.ScaleXProperty, scale);
            MainCanvas.SetValue(ScaleTransform.ScaleYProperty, scale);
        }
        if (e.Delta.Y < 0)
        {
            scale -= e.Delta.Y;
            MainCanvas.SetValue(ScaleTransform.ScaleXProperty, scale);
            MainCanvas.SetValue(ScaleTransform.ScaleYProperty, scale);
        }
    }

    public void RightClickOnCanvas(object sender, PointerPressedEventArgs e)
    {
        if (RightClickOnCanvas != null)
        {
            lastRightClickPosition = e.GetPosition(null);
        }
    }

    private void AddNode(object? sender, RoutedEventArgs e)
    {
        var test = e.Source as MenuItem;
        var r = new Random();
        var Custombrush = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255),
                (byte)r.Next(1, 255), (byte)r.Next(1, 233)));

        Rectangle newRec = new Rectangle
        {
            Width = 50,
            Height = 50,
            StrokeThickness = 3,
            Fill = Custombrush,
            Stroke = Brushes.Black
        };
        Point pos = lastRightClickPosition;
        Canvas.SetLeft(newRec, pos.X);
        Canvas.SetTop(newRec, pos.Y);
        MainCanvas.Children.Add(newRec);
    }
}