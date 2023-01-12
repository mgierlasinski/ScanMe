using ZXing;
using ZXing.Common;

namespace ScanMe.Views;

public enum BarcodeFormat
{
    Code39,
    Code93,
    Code128,
    DataMatrix,
    QRCode
}

public class BarcodeView : GraphicsView, IDrawable
{
    public BarcodeView()
    {
        Drawable = this;
        Background = new SolidColorBrush(Colors.White);
        Foreground = new SolidColorBrush(Colors.Black);
    }

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(BarcodeView),
        propertyChanged: RedrawBarcode);

    public static readonly BindableProperty FormatProperty = BindableProperty.Create(
        nameof(Format),
        typeof(BarcodeFormat),
        typeof(BarcodeView),
        defaultValue: BarcodeFormat.QRCode,
        propertyChanged: RedrawBarcode);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public BarcodeFormat Format
    {
        get => (BarcodeFormat)GetValue(FormatProperty);
        set => SetValue(FormatProperty, value);
    }

    public Brush Foreground { get; set; }

    private static void RedrawBarcode(BindableObject bindable, object _, object __) => ((BarcodeView)bindable).Invalidate();

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        try
        {
            var hints = new Dictionary<EncodeHintType, object> { /*[EncodeHintType.MARGIN] = 0*/ };
            var matrix = new MultiFormatWriter().encode(Text, GetFormat(), (int)dirtyRect.Width, (int)dirtyRect.Height, hints);

            canvas.SetFillPaint(BrushToPaint(Background), dirtyRect);
            canvas.FillRectangle(dirtyRect);
            canvas.SetFillPaint(BrushToPaint(Foreground), dirtyRect);

            for (var x = 0; x < matrix.Width; x++)
            {
                for (var y = 0; y < matrix.Height; y++)
                {
                    //var paint = new SolidPaint(matrix[x, y] ? Colors.Black : Colors.White);
                    //canvas.SetFillPaint(paint, dirtyRect);
                    //canvas.FillRectangle(x, y, 1, 1);

                    if (matrix[x, y])
                    {
                        canvas.FillRectangle(x, y, 1, 1);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private Paint BrushToPaint(Brush brush)
    {
        if (brush is SolidColorBrush solidBrush)
        {
            return new SolidPaint(solidBrush.Color);
        }
        if (brush is LinearGradientBrush linearBrush)
        {
            var linearPaint = new LinearGradientPaint
            {
                StartPoint = linearBrush.StartPoint,
                EndPoint = linearBrush.EndPoint
            };

            foreach (var stop in linearBrush.GradientStops)
            {
                if (stop.Offset <= 0)
                    linearPaint.StartColor = stop.Color;
                else if (stop.Offset >= 1)
                    linearPaint.EndColor = stop.Color;
                else
                    linearPaint.AddOffset(stop.Offset, stop.Color);
            }

            return linearPaint;
        }
        throw new NotSupportedException("Brush not supported");
    }

    private ZXing.BarcodeFormat GetFormat()
    {
        return Format switch
        {
            BarcodeFormat.Code39 => ZXing.BarcodeFormat.CODE_39,
            BarcodeFormat.Code93 => ZXing.BarcodeFormat.CODE_93,
            BarcodeFormat.Code128 => ZXing.BarcodeFormat.CODE_128,
            BarcodeFormat.DataMatrix => ZXing.BarcodeFormat.DATA_MATRIX,
            _ => ZXing.BarcodeFormat.QR_CODE
        };
    }
}
