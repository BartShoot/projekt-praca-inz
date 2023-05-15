using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia;
using Avalonia.VisualTree;
using System.Globalization;

namespace NoodleCV.Models
{
	public class Node : Control
	{
		private bool isDragging;
		private Point dragOffset;

		public Node()
		{
			Width = 100;
			Height = 50;
			PointerPressed += OnPointerPressed;
			PointerMoved += OnPointerMoved;
			PointerReleased += OnPointerReleased;
			Render(new DrawingContext(PlatformImp);
		}

		private void OnPointerPressed(object sender, PointerPressedEventArgs e)
		{
			if (e.GetCurrentPoint(this).Properties.PointerUpdateKind == PointerUpdateKind.LeftButtonPressed)
			{
				isDragging = true;
				dragOffset = e.GetPosition(this) - Position;
				e.Pointer.Capture(this);
			}
		}

		private void OnPointerMoved(object sender, PointerEventArgs e)
		{
			if (isDragging)
			{
				var newPos = e.GetPosition(this.Parent as Visual) - dragOffset;
				Position = newPos;
			}
		}

		private void OnPointerReleased(object sender, PointerReleasedEventArgs e)
		{
			if (isDragging && e.GetCurrentPoint(this).Properties.PointerUpdateKind == PointerUpdateKind.LeftButtonPressed)
			{
				isDragging = false;
				e.Pointer.Capture(null);
			}
		}

		public static readonly StyledProperty<Point> PositionProperty =
			AvaloniaProperty.Register<Node, Point>(nameof(Position));

			public Point Position
		{
			get { return GetValue(PositionProperty); }
			set { SetValue(PositionProperty, value); }
		}

		public override void Render(DrawingContext context)
		{
			var formattedText = new FormattedText(
				"Node",
				CultureInfo.CurrentCulture,
				FlowDirection.LeftToRight,
				new Typeface("Arial"),
				16,
				Brushes.Black);

			context.DrawRectangle(Brushes.LightBlue, null, new Rect(Bounds.Size));
			context.DrawText(formattedText, new Point(10, 10));
		}
	}
}

