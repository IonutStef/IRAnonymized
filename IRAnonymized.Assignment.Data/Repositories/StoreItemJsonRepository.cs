using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IRAnonymized.Assignment.Common.Settings;
using IRAnonymized.Assignment.Data.Dtos;
using JsonFlatFileDataStore;

namespace IRAnonymized.Assignment.Data.Repositories
{
    /// <summary>
    /// <see cref="StoreItemDto"/> Repository using Json Data Access Type context.
    /// </summary>
    public class StoreItemJsonRepository : IStoreRepository
    {
        public RepoType Type => RepoType.Json;

        private readonly IJsonDatabaseSettings _settings;

        public StoreItemJsonRepository(IJsonDatabaseSettings settings)
        {
            _settings = settings;
        }

        public async Task<StoreItemDto> GetAsync(string id)
        {
            using (var store = new DataStore(_settings.DatabaseFilePath))
            {
                var movieCollection = store.GetCollection<StoreItemDto>();

                var movie = movieCollection.AsQueryable()
                    .FirstOrDefault(m => m.Key == id);

                return movie;
            }
        }

        public async Task<ICollection<StoreItemDto>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            using (var store = new DataStore(_settings.DatabaseFilePath))
            {
                var movieCollection = store.GetCollection<StoreItemDto>();

                var movies = movieCollection.AsQueryable();

                return movies.Skip(pageNumber * pageSize - 1)
                    .Take(pageSize)
                    .ToList();
            }
        }

        public async Task<StoreItemDto> ReplaceAsync(StoreItemDto entity)
        {
            using (var store = new DataStore(_settings.DatabaseFilePath))
            {
                var movieCollection = store.GetCollection<StoreItemDto>();

                await movieCollection.ReplaceOneAsync(m => m.Key == entity.Key, entity, true);

                return movieCollection.Find(
                    i => i.Key.Equals(entity.Key, StringComparison.InvariantCultureIgnoreCase))
                    .FirstOrDefault();
            }
        }
    }
}