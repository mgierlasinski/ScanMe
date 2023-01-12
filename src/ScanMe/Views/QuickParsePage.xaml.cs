using ScanMe.ViewModels;

namespace ScanMe.Views;

public partial class QuickParsePage : ContentPage
{
	public QuickParsePage()
	{
		InitializeComponent();

        BindingContext = new QuickParseViewModel();
    }
}