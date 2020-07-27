using AutoMapper;
using IRAnonymized.Assignment.Common.Models;
using IRAnonymized.Assignment.Data.Repositories;
using IRAnonymized.Assignment.WebApi.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IRAnonymized.Assignment.WebApi.Services
{
    /// <summary>
    /// <see cref="StoreItem"/> service.
    /// </summary>
    public abstract class StoreItemBaseService : IStoreItemService
    {
        private readonly IStoreRepository _repository;
        private readonly ILogger<StoreItemBaseService> _logger;
        private readonly IMapper _mapper;

        public StoreItemBaseService(IStoreRepository repository, ILogger<StoreItemBaseService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a <see cref="StoreItemDto"/> for the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">Unique Identifier of the <see cref="StoreItemDto"/>.</param>
        /// <returns>Requested <see cref="StoreItemDto"/>.</returns>
        public async Task<StoreItem> GetItem(string id)
        {
            var storeItem = await _repository.GetAsync(id);
            if (storeItem == null)
            {
                return null;
            }

            return _mapper.Map<StoreItem>(storeItem);
        }

        /// <summary>
        /// Gets a collection of <paramref name="pageSize"/> elements of <see cref="StoreItemDto"/> 
        /// found at page <paramref name="pageNumber"/>.
        /// </summary>
        /// <param name="pageNumber">Number of the page.</param>
        /// <param name="pageSize">Number of elements to be retrieved.</param>
        /// <returns>Unordered <see cref="ICollection<StoreItemDto>"/>.</returns>
        public async Task<ICollection<StoreItem>> GetPaginated(int pageNumber, int pageSize)
        {
            var storeItemsDtos = await _repository.GetPaginatedAsync(pageNumber, pageSize);
            if (storeItemsDtos == null || storeItemsDtos.Count == 0)
            {
                return null;
            }

            return _mapper.Map<ICollection<StoreItem>>(storeItemsDtos);
        }
    }
}