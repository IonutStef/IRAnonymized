using AutoMapper;
using IRAnonymized.Assignment.Data.Repositories;
using IRAnonymized.Assignment.WebApi.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace IRAnonymized.Assignment.WebApi.Services
{
    /// <summary>
    /// <see cref="StoreItem"/> service that uses Mongo Type Repository.
    /// </summary>
    public class StoreItemMongoService : StoreItemBaseService, IStoreItemMongoService
    {
        public StoreItemMongoService(IServiceProvider serviceProvider, ILogger<StoreItemMongoService> logger, IMapper mapper)
            : base(serviceProvider.GetServices<IStoreRepository>().FirstOrDefault(s => s.Type == RepoType.Mongo), logger, mapper)
        {
        }
    }
}