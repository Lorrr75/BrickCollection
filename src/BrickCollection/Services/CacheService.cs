using SQLite;
using BrickCollection.Models;
using Microsoft.VisualBasic;

namespace BrickCollection.Services
{
    public class CachedSet
    {
        [PrimaryKey]
        public int SetID { get; set; }
        public string? Number { get; set; }
        public string? Name { get; set; }
        public int Year { get; set; }
        public string? Theme { get; set; }
        public string? Subtheme { get; set; }
        public int? Pieces { get; set; }
        public int? Minifigs { get; set; }
        public string? ThumbnailURL { get; set; }
        public string? ImageURL { get; set; }
        public bool Wanted { get; set; }
        public int QtyWanted { get; set; }
    }

    public class CacheService
    {
        private SQLiteAsyncConnection? _db;

        public async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            if (_db is not null)
                return _db;
            
            var dbPath = Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "brickcollection.db");
            _db = new SQLiteAsyncConnection(dbPath);
            await _db.CreateTableAsync<CachedSet>();
            return _db;
        }

        public async Task SaveSetsAsync(IEnumerable<BricksetSet> sets)
        {
            var db = await GetConnectionAsync();
            await db.DeleteAllAsync<CachedSet>();

            var cachedSets = sets.Select(s => new CachedSet
            {
                SetID = s.SetID,
                Number = s.Number,
                Name = s.Name,
                Year = s.Year,
                Theme = s.Theme,
                Subtheme = s.Subtheme,
                Pieces = s.Pieces,
                Minifigs = s.Minifigs,
                ThumbnailURL = s.Image?.ThumbnailURL,
                ImageURL = s.Image?.ImageURL,
                Wanted = s.Collection?.Wanted ?? false,
                QtyWanted = s.Collection?.QtyWanted ?? 0
            }).ToList();

            await   db.InsertAllAsync(cachedSets);
        }

        public async Task<List<BricksetSet>> GetCachedSetsAsync()
        {
            var db = await GetConnectionAsync();
            var cachedSets = await db.Table<CachedSet>().ToListAsync();

            return cachedSets.Select(c => new BricksetSet
            {
                SetID = c.SetID,
                Number = c.Number,
                Name = c.Name,
                Year = c.Year,
                Theme = c.Theme,
                Subtheme = c.Subtheme,
                Pieces = c.Pieces,
                Minifigs = c.Minifigs,
                Image = new BricksetImage
                {
                    ThumbnailURL = c.ThumbnailURL,
                    ImageURL = c.ImageURL
                },
                Collection = new BricksetCollectionInfo
                {
                    Owned = true, // per definizione: la cache contiene solo set posseduti
                    Wanted = c.Wanted,
                    QtyWanted = c.QtyWanted
                }
            }).ToList();
        }

        public async Task UpdateWantedAsync(int setId, bool wanted)
        {
            var db = await GetConnectionAsync();
            var row = await db.FindAsync<CachedSet>(setId);
            if (row is null)
                return;

            row.Wanted = wanted;
            await db.UpdateAsync(row);
        }
    }
}
