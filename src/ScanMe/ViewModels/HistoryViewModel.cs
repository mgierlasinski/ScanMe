using CommunityToolkit.Mvvm.ComponentModel;
using ScanMe.Services;

namespace ScanMe.ViewModels;

[ObservableObject]
public partial class HistoryViewModel
{
    private readonly IBarcodeService _barcodeService;

    [ObservableProperty]
    private List<BarcodeItem> _history;

    public HistoryViewModel(IBarcodeService barcodeService)
    {
        _barcodeService = barcodeService;
    }

    public void LoadData()
    {
        History = _barcodeService.GetHistory().Select(x => new BarcodeItem
        {
            Barcode = x.Text
        }).ToList();
    }
}
