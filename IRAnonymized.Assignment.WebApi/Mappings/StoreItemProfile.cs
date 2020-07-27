using AutoMapper;
using IRAnonymized.Assignment.Common.Models;
using IRAnonymized.Assignment.WebApi.Models;

namespace IRAnonymized.Assignment.WebApi.Mappings
{
    public class StoreItemProfile : Profile
    {
        public StoreItemProfile()
        {
            CreateMap<StoreItem, StoreItemModel>()
                .ReverseMap();
        }
    }
}