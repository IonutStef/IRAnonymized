using GivenFixture;
using GivenFixture.Extensions;
using IRAnonymized.Assignment.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Moq;
using IRAnonymized.Assignment.Data.Dtos;
using AutoMapper;
using IRAnonymized.Assignment.Common.Models;

namespace IRAnonymized.Assignment.Services.UnitTests
{
    public class FileImportServiceTests
    {
        [Fact(Skip = "Cannot mock static method IServiceProvider.GetServices<T>()")]
        public Task When_save_item() =>
            Given.Fixture
                .HavingModels<string>(out var keysCollection)
                .HavingModels<string>(out var valuesCollection)
                .HavingModel<StoreItemDto>(out var storeItem)
                .HavingFake(f => string.Join(",", keysCollection), out var keysString)
                .HavingFake(f => string.Join(",", valuesCollection), out var valuesString)
                .HavingMocked<IMapper, StoreItemDto>(m => m.Map<StoreItemDto>(It.IsAny<StoreItem>()), out _)
                .HavingMock<IServiceProvider>(m =>
                {
                    var repo1 = new Mock<IStoreRepository>();
                    repo1.Setup(r => r.ReplaceAsync(storeItem)).ReturnsAsync(storeItem).Verifiable();

                    var repo2 = new Mock<IStoreRepository>();
                    repo2.Setup(r => r.ReplaceAsync(storeItem)).ReturnsAsync(storeItem).Verifiable();

                    var repositories = new List<IStoreRepository> { repo1.Object, repo2.Object };
                    m.Setup(s => s.GetServices<IStoreRepository>()).Returns(repositories).Verifiable();
                })
                .When<FileImportService>(f => f.CreateStoreItem(keysString, valuesString))
                .ShouldReturnEquivalent(storeItem)
                .RunAsync();

        [Fact(Skip = "Cannot mock static method IServiceProvider.GetServices<T>()")]
        public void When_converting_to_store_item() =>
            Given.LooseFixture
                .HavingModel<StoreItem>(out var storeItem)
                .HavingFake<string>(f => $"{nameof(storeItem.Key)},ArtikelCode," +
                        $"{nameof(storeItem.ColorCode)},{nameof(storeItem.Description)}," +
                        $"{nameof(storeItem.DiscountPrice)},DeliveredIn,Q1,{nameof(storeItem.Size)}," +
                        $"{nameof(storeItem.Color)},{nameof(storeItem.Price)}", out var header)
                .HavingFake<string>(f => $"{storeItem.Key},{storeItem.ArticleCode}" +
                        $",{storeItem.ColorCode},{storeItem.Description}" +
                        $",{storeItem.DiscountPrice},{storeItem.DeliveredInAddress},{storeItem.SizeGroup},{storeItem.Size}" +
                        $",{storeItem.Color},{storeItem.Price}", out var values)
                .When<FileImportService>(f => f.CreateStoreItem(header, values))
                .ShouldReturnEquivalent(storeItem)
                .Run();
    }
}