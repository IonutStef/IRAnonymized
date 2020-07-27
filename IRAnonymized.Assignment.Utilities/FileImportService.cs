using AutoMapper;
using IRAnonymized.Assignment.Data.Dtos;
using IRAnonymized.Assignment.Data.Repositories;
using IRAnonymized.Assignment.Common.Models;
using IRAnonymized.Assignment.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("IRAnonymized.Assignment.Services.UnitTests")]
namespace IRAnonymized.Assignment.Services
{
    public class FileImportService : IFileImportService
    {
        private readonly ILogger<FileImportService> _logger;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IStoreRepository> _repositories;

        public FileImportService(ILogger<FileImportService> logger, IMapper mapper, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _mapper = mapper;
            _repositories = serviceProvider.GetServices<IStoreRepository>();
        }

        /// <summary>
        /// Import a file from a specified <paramref name="path"/> location.
        /// </summary>
        /// <param name="path">Location at which the file exists.</param>
        /// <returns>Number of entries found in the file.</returns>
        public async Task<int> Import(string path) 
        {
            if(string.IsNullOrWhiteSpace(path))
            {
                throw ArgumentNullException(nameof(path));
            }

            var header = File.ReadLines(path)
                .FirstOrDefault();

            if(string.IsNullOrWhiteSpace(header))
            {
                throw new InvalidOperationException("File has no header.");
            }

            var sumOfLines = File.ReadLines(path)
                .Skip(1)
                .Select(async line => await SaveItem(header, line));

            await Task.WhenAll(sumOfLines);

            return sumOfLines.Count();
        }

        private Exception ArgumentNullException(string v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Save an entry in the database.
        /// </summary>
        /// <param name="header">List of keys.</param>
        /// <param name="line">Values associated to <paramref name="header"/>.</param>
        /// <returns></returns>
        internal async Task<StoreItemDto> SaveItem(string header, string line)
        {
            if(string.IsNullOrWhiteSpace(line))
            {
                return null;
            }

            try
            {
                var item = CreateStoreItem(header, line);

                var itemDto = _mapper.Map<StoreItemDto>(item);
                foreach(var repo in _repositories)
                {
                    await repo.ReplaceAsync(itemDto);
                }

                return itemDto;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Could not deserialize / save item: {header} => {line}.");
            }

            return null;
        }

        internal StoreItem CreateStoreItem(string header, string values)
        {
            var headerTitles = header.Split(',');
            var valuesLine = values.Split(',');

            var itemAsDictionary = headerTitles.Zip(valuesLine, (k, v) => new { Key = k, Value = v })
                     .ToDictionary(x => x.Key, x => x.Value);
            var itemString = JsonConvert.SerializeObject(itemAsDictionary);

            return JsonConvert.DeserializeObject<StoreItem>(itemString);
        }
    }
}