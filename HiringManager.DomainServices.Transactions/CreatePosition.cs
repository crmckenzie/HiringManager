using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using HiringManager.Transactions;
using Simple.Validation;

namespace HiringManager.DomainServices.Transactions
{
    public class CreatePosition : ITransaction<CreatePositionRequest, CreatePositionResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IValidationEngine _validationEngine;

        public CreatePosition(IDbContext dbContext, IValidationEngine validationEngine)
        {
            _dbContext = dbContext;
            _validationEngine = validationEngine;
        }

        public CreatePositionResponse Execute(CreatePositionRequest request)
        {
            var results = _validationEngine.Validate(request);
            if (results.HasErrors())
            {
                return new CreatePositionResponse()
                       {
                           ValidationResults = results,
                       };
            }

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