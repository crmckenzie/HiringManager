using HiringManager.EntityModel;
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

        public IValidatedResponse Close(int positionId)
        {
            throw new System.NotImplementedException();
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

        public CandidateStatusResponse Hire(int candidateStatusId)
        {
            var request = new HireCandidateRequest()
                                       {
                                           CandidateStatusId = candidateStatusId
                                       };
            var result = base.Execute<HireCandidateRequest, CandidateStatusResponse>(request);
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

        public CandidateStatusResponse SetCandidateStatus(int candidateStatusId, string status)
        {
            var setCandidateStatusRequest = new SetCandidateStatusRequest()
                                            {
                                                CandidateStatusId = candidateStatusId,
                                                Status = status,
                                            };
            var result = base.Execute<SetCandidateStatusRequest, CandidateStatusResponse>(setCandidateStatusRequest);
            return result;
        }

        public CandidateStatusDetails GetCandidateStatusDetails(int candidateStatusId)
        {
            var candidateStatus = this._repository.Get<CandidateStatus>(candidateStatusId);
            var details = _mapper
                .Map<CandidateStatusDetails>()
                .From(candidateStatus)
                ;

            return details;
        }
    }
}