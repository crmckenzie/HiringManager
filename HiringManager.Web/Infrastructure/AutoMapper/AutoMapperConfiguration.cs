using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiringManager.Web.Infrastructure.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            global::AutoMapper.Mapper.AddProfile<DomainProfile>();
        }
    }
}