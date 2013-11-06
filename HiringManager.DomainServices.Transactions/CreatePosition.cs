using HiringManager.DomainServices.Impl;
using HiringManager.EntityModel;
using HiringManager.Mappers;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Transactions
{
    public class CreatePosition : ITransaction<CreatePositionRequest, CreatePositionResponse>
    {
        private readonly IRepository _repository;
        private readonly IFluentMapper _fluentMapper;
        private readonly IUserSession _userSession;

        public CreatePosition(IRepository repository, IFluentMapper fluentMapper, IUserSession userSession)
        {
            _repository = repository;
            _fluentMapper = fluentMapper;
            _userSession = userSession;
        }

        public CreatePositionResponse Execute(CreatePositionRequest request)
        {
            var position = _fluentMapper
                .Map<Position>()
                .From(request)
                ;

            position.CreatedBy = this._repository.Get<Manager>(_userSession.ManagerId.Value);

            _repository.Store(position);

            _repository.Commit();

            return new CreatePositionResponse()
                   {
                       PositionId = position.PositionId,
                   };
        }
    }
}