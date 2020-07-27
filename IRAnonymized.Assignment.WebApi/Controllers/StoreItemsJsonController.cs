using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IRAnonymized.Assignment.WebApi.Models;
using IRAnonymized.Assignment.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IRAnonymized.Assignment.WebApi.Controllers
{
    /// <summary>
    /// Manages the <see cref="StoreItemModel "/> stored in the Json type Database.
    /// </summary>
    [Route("api/[controller]")]
    public class StoreItemsJsonController : ControllerBase
    {
        private readonly IStoreItemJsonService _service;
        private readonly ILogger<StoreItemsJsonController> _logger;
        private readonly IMapper _mapper;

        public StoreItemsJsonController(IStoreItemJsonService service, 
            ILogger<StoreItemsJsonController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a <see cref="StoreItem" /> with the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">Unique identifier of the item.</param>
        /// <returns><see cref="StoreItem" /> for the specified <paramref name="id" /></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var storeItem = await _service.GetItem(id);

            if(storeItem == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StoreItemModel>(storeItem));
        }

        /// <summary>
        /// Get all <see cref="StoreItemDto"/> items following the Pagination structure.
        /// </summary>
        /// <param name="pageNumber">Page number for the items.</param>
        /// <param name="numberOfItems">Number of items to be retrieved.</param>
        /// <returns>A <see cref="ICollection{T}"/> of <see cref="StoreItemDto"/>.</returns>
        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber, int numberOfItems)
        {
            var storeItems = await _service.GetPaginated(pageNumber, numberOfItems);

            if(storeItems == null || !storeItems.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ICollection<StoreItemModel>>(storeItems));
        }
    }
}