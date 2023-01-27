using LiteDB;

namespace ScanMe.Services;

public interface IDatabaseProvider
{
    LiteDatabase CreateDatabase();
}

public class DatabaseProvider : IDatabaseProvider
{
    public LiteDatabase CreateDatabase()
    {
        return new LiteDatabase($"Filename={GetDbPath()};Upgrade=true");
    }

    private string GetDbPath()
    {
        var dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(dir, "Barcodes.db");
    }
}
