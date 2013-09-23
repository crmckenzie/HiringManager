using HiringManager.Domain;
using HiringManager.Mappers;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Impl
{
    public class PositionService : DomainServiceBase, IPositionService
    {
        private readonly IRepository _repository;
        private readonly IFluentMapper _mapper;

        public PositionService(IFluentTransactionBuilder builder, IRepository repository, IFluentMapper mapper) : base(builder)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public QueryResponse<PositionSummary> Query(QueryPositionSummariesRequest request)
        {
            var result = base.Execute<QueryPositionSummariesRequest, QueryResponse<PositionSummary>>(request);
            return result;
        }

        public CreatePositionResponse CreatePosition(CreatePositionRequest request)
        {
            var result = base.Execute<CreatePositionRequest, CreatePositionResponse>(request);
            return result;
        }

        public AddCandidateResponse AddCandidate(AddCandidateRequest request)
        {
            var result = base.Execute<AddCandidateRequest, AddCandidateResponse>(request);
            return result;
        }

        public HireCandidateResponse Hire(HireCandidateRequest request)
        {
            var result = base.Execute<HireCandidateRequest, HireCandidateResponse>(request);
            return result;
        }

        public PositionDetails Details(int id)
        {
            var position = _repository.Get<Position>(id);
            var details = _mapper
                .Map<PositionDetails>()
                .From(position)
                ;
            return details;
        }
    }
}