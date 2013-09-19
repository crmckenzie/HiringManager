using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using HiringManager.Domain;
using HiringManager.DomainServices;
using HiringManager.Web.Models.Positions;

namespace HiringManager.Web.Mappers.Positions
{
    public class CreatePositionViewModelMapper : IMapper<CreatePositionViewModel, CreatePositionRequest>
    {
        private readonly IRepository _repository;
        private readonly IPrincipal _principal;

        public CreatePositionViewModelMapper(IRepository repository, IPrincipal principal)
        {
            _repository = repository;
            _principal = principal;
        }

        public CreatePositionRequest Map(CreatePositionViewModel input)
        {
            var result = AutoMapper.Mapper.DynamicMap<CreatePositionViewModel, CreatePositionRequest>(input);
            result.HiringManagerId = GetUser().ManagerId;
            return result;
        }

        private Manager GetUser()
        {
            return _repository.Query<Manager>().Single(row => row.UserName == _principal.Identity.Name);
        }
    }
}