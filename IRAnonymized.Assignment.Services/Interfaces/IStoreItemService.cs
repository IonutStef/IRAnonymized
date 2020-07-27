using IRAnonymized.Assignment.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IRAnonymized.Assignment.WebApi.Services.Interfaces
{
    /// <summary>
    /// <see cref="StoreItem"/> service.
    /// </summary>
    public interface IStoreItemService
    {
        /// <summary>
        /// Gets a <see cref="StoreItemDto"/> for the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">Unique Identifier of the <see cref="StoreItemDto"/>.</param>
        /// <returns>Requested <see cref="StoreItemDto"/>.</returns>
        Task<StoreItem> GetItem(string id);

        /// <summary>
        /// Gets a collection of <paramref name="pageSize"/> elements of <see cref="StoreItemDto"/> 
        /// found at page <paramref name="pageNumber"/>.
        /// </summary>
        /// <param name="pageNumber">Number of the page.</param>
        /// <param name="pageSize">Number of elements to be retrieved.</param>
        /// <returns>Unordered <see cref="ICollection<StoreItemDto>"/>.</returns>
        Task<ICollection<StoreItem>> GetPaginated(int pageNumber, int pageSize);
    }
}