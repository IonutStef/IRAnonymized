using IRAnonymized.Assignment.Data.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IRAnonymized.Assignment.Data.Repositories
{
    /// <summary>
    /// <see cref="StoreItemDto"/> Repository.
    /// </summary>
    public interface IStoreRepository
    {
        /// <summary>
        /// Type of the repository.
        /// </summary>
        RepoType Type { get; }

        /// <summary>
        /// Gets a <see cref="StoreItemDto"/> for the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">Unique Identifier of the <see cref="StoreItemDto"/>.</param>
        /// <returns>Requested <see cref="StoreItemDto"/>.</returns>
        Task<StoreItemDto> GetAsync(string id);

        /// <summary>
        /// Gets a collection of <paramref name="pageSize"/> elements of <see cref="StoreItemDto"/> 
        /// found at page <paramref name="pageNumber"/>.
        /// </summary>
        /// <param name="pageNumber">Number of the page.</param>
        /// <param name="pageSize">Number of elements to be retrieved.</param>
        /// <returns>Unordered <see cref="ICollection<StoreItemDto>"/></returns>
        Task<ICollection<StoreItemDto>> GetPaginatedAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Replace a <see cref="StoreItemDto"/> in the database with the specifies <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="StoreItemDto"/> to be replace.</param>
        /// <returns>Replaced <see cref="StoreItemDto"/>.</returns>
        Task<StoreItemDto> ReplaceAsync(StoreItemDto entity);
    }
}