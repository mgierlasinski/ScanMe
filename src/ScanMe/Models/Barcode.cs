namespace ScanMe.Models;

public class Barcode
{
    public long Id { get; set; }
    public string Text { get; set; }
    public string[] Tags { get; set; }
    public DateTime LastScan { get; set; }
}
