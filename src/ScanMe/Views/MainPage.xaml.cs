namespace ScanMe.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
	{
		InitializeComponent();

        Format.ItemsSource = Enum.GetValues<BarcodeFormat>();
        Format.SelectedItem = BarcodeFormat.QRCode;
    }
}

