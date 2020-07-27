using IRAnonymized.Assignment.Common.Settings;
using IRAnonymized.Assignment.Data.Dtos;
using MongoDB.Driver;

namespace IRAnonymized.Assignment.Data.Contexts
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoContext(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            if(client != null)
            {
                _database = client.GetDatabase(settings.DatabaseName);
            }
        }

        public IMongoCollection<StoreItemDto> StoreItems 
            => _database.GetCollection<StoreItemDto>(nameof(StoreItems));
    }
}