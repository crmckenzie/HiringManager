using HiringManager.EntityModel;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Transactions
{
    public class CreatePosition : ITransaction<CreatePositionRequest, CreatePositionResponse>
    {
        private readonly IDbContext _dbContext;

        public CreatePosition(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CreatePositionResponse Execute(CreatePositionRequest request)
        {
            var position = global::AutoMapper.Mapper.Map<Position>(request);

            _dbContext.Add(position);

            _dbContext.SaveChanges();

            return new CreatePositionResponse()
                   {
                       PositionId = position.PositionId,
                   };
        }
    }
}