using IRAnonymized.Assignment.Data.Dtos;
using MongoDB.Driver;

namespace IRAnonymized.Assignment.Data.Contexts
{
    public interface IMongoContext
    {
        IMongoCollection<StoreItemDto> StoreItems { get; }
    }
}
