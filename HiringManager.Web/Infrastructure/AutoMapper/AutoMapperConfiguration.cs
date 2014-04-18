using HiringManager.DomainServices.AutoMapperProfiles;

namespace HiringManager.Web.Infrastructure.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            global::AutoMapper.Mapper.Reset();
            global::AutoMapper.Mapper.AddProfile<DomainProfile>();
            global::AutoMapper.Mapper.AddProfile<PresentationProfile>();
        }
    }
}