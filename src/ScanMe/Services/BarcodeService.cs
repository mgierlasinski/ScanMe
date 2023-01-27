using ScanMe.Models;

namespace ScanMe.Services;

public interface IBarcodeService
{
    List<Barcode> GetHistory();
    void AddToHistory(IEnumerable<Barcode> barcodes);
}

public class BarcodeService : IBarcodeService
{
    private readonly IDatabaseProvider _databaseProvider;

    public BarcodeService(IDatabaseProvider databaseProvider)
    {
        _databaseProvider = databaseProvider;
    }

    public List<Barcode> GetHistory()
    {
        using var db = _databaseProvider.CreateDatabase();

        var collection = db.GetCollection<Barcode>(nameof(Barcode));
        var result = collection.FindAll()
            .OrderBy(x => x.LastScan)
            .ToList();
            
        return result;
    }

    public void AddToHistory(IEnumerable<Barcode> barcodes)
    {
        using var db = _databaseProvider.CreateDatabase();
        var collection = db.GetCollection<Barcode>(nameof(Barcode));
        var lastScan = DateTime.Now;

        foreach (var barcode in barcodes)
        {
            var match = collection.FindOne(x => x.Text == barcode.Text);
            if (match != null)
            {
                match.Tags = barcode.Tags;
                match.LastScan = lastScan;
                collection.Update(match);
            }
            else
            {
                collection.Insert(barcode);
            }
        }
    }
}
