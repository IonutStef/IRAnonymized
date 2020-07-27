using AutoMapper;
using IRAnonymized.Assignment.Common.Models;
using IRAnonymized.Assignment.Data.Dtos;

namespace IRAnonymized.Assignment.Services.Mappings
{
    public class StoreItemProfile : Profile
    {
        public StoreItemProfile()
        {
            CreateMap<StoreItemDto, StoreItem>()
                .ReverseMap();
        }
    }
}