using CommunityToolkit.Mvvm.ComponentModel;
using ScanMe.Views;

namespace ScanMe.ViewModels;

[ObservableObject]
public partial class QuickParseViewModel
{
    [ObservableProperty]
    private string _text;

    [ObservableProperty] 
    private List<BarcodeItem> _barcodes = new();

    [ObservableProperty]
    private BarcodeFormat _selectedFormat = BarcodeFormat.Code128;

    [ObservableProperty]
    private double _scale = 1;

    public IEnumerable<BarcodeFormat> Formats { get; } = Enum.GetValues<BarcodeFormat>();
    
    partial void OnTextChanged(string value)
    {
        var lines = value.Split('\r', StringSplitOptions.RemoveEmptyEntries);
        Barcodes = lines.Select(x => new BarcodeItem
        {
            Barcode = x,
            Format = SelectedFormat
        }).ToList();
    }

    partial void OnSelectedFormatChanged(BarcodeFormat value)
    {
        foreach (var barcode in Barcodes)
        {
            barcode.Format = value;
        }
    }

    partial void OnScaleChanged(double value)
    {
        foreach (var barcode in Barcodes)
        {
            barcode.Scale = value;
        }
    }
}

[ObservableObject]
public partial class BarcodeItem
{
    private const int BaseWidth = 300;
    private const int BaseHeight = 100;

    [ObservableProperty]
    private string _barcode;
    
    [ObservableProperty]
    private BarcodeFormat _format;

    [ObservableProperty]
    private double _scale = 1;

    public int ScaledWidth => (int)(BaseWidth * Scale);
    public int ScaledHeight => (int)(BaseHeight * Scale);

    partial void OnScaleChanged(double value)
    {
        OnPropertyChanged(nameof(ScaledWidth));
        OnPropertyChanged(nameof(ScaledHeight));
    }
}
