using HiringManager.DomainServices.Impl;
using HiringManager.EntityModel;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Transactions
{
    public class CreatePosition : ITransaction<CreatePositionRequest, CreatePositionResponse>
    {
        private readonly IRepository _repository;

        public CreatePosition(IRepository repository)
        {
            _repository = repository;
        }

        public CreatePositionResponse Execute(CreatePositionRequest request)
        {
            var position = global::AutoMapper.Mapper.Map<Position>(request);

            _repository.Store(position);

            _repository.Commit();

            return new CreatePositionResponse()
                   {
                       PositionId = position.PositionId,
                   };
        }
    }
}