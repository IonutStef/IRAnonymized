using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IRAnonymized.Assignment.Data.Contexts;
using IRAnonymized.Assignment.Data.Dtos;
using MongoDB.Driver;

namespace IRAnonymized.Assignment.Data.Repositories
{
    /// <summary>
    /// <see cref="StoreItemDto"/> Repository using Mongo Data Access Type context.
    /// </summary>
    public class StoreItemMongoRepository : IStoreRepository
    {
        public RepoType Type => RepoType.Mongo;

        private readonly IMongoContext _storeContext;

        public StoreItemMongoRepository(IMongoContext storeContext)
        {
            _storeContext = storeContext;
        }

        public Task<StoreItemDto> GetAsync(string id)
            => _storeContext.StoreItems
                    .Find(i => i.Key == id)
                    .FirstOrDefaultAsync();

        public async Task<ICollection<StoreItemDto>> GetPaginatedAsync(int pageNumber, int pageSize) =>
            _storeContext.StoreItems
                .AsQueryable()
                .Skip(pageNumber * pageSize - 1)
                .Take(pageSize).ToList();

        public Task<StoreItemDto> ReplaceAsync(StoreItemDto entity)
                => _storeContext.StoreItems.FindOneAndReplaceAsync<StoreItemDto>(
                    n => n.Key == entity.Key, 
                    entity, 
                    new FindOneAndReplaceOptions<StoreItemDto, StoreItemDto>
                    {
                        IsUpsert = true,
                        ReturnDocument = ReturnDocument.After
                    });
    }
}