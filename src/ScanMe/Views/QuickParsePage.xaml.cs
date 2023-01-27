using ScanMe.ViewModels;

namespace ScanMe.Views;

public partial class QuickParsePage : ContentPage
{
	public QuickParsePage(QuickParseViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}