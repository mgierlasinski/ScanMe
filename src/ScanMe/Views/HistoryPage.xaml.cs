using ScanMe.ViewModels;

namespace ScanMe.Views;

public partial class HistoryPage : ContentPage
{
    private readonly HistoryViewModel _viewModel;
	public HistoryPage(HistoryViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _viewModel.LoadData();
    }
}