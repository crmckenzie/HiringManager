using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Transactions
{
    public class PositionService : DomainServiceBase, IPositionService
    {
        private readonly IUnitOfWork _unitOfwork;

        public PositionService(IFluentTransactionBuilder builder, IUnitOfWork unitOfwork)
            : base(builder)
        {
            _unitOfwork = unitOfwork;
        }

        public QueryResponse<PositionSummary> Query(QueryPositionSummariesRequest request)
        {
            var result = base.Execute<QueryPositionSummariesRequest, QueryResponse<PositionSummary>>(request);
            return result;
        }

        public IValidatedResponse Close(int positionId)
        {
            return base.Execute<int, IValidatedResponse>(positionId);
        }

        public CreatePositionResponse CreatePosition(CreatePositionRequest request)
        {
            var result = base.Execute<CreatePositionRequest, CreatePositionResponse>(request);
            return result;
        }

        public NewCandidateResponse AddNewCandidate(NewCandidateRequest request)
        {
            var result = base.Execute<NewCandidateRequest, NewCandidateResponse>(request);
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
            using (var repository = _unitOfwork.NewDbContext())
            {
                var position = repository.Get<Position>(id);
                var details = AutoMapper.Mapper.Map<PositionDetails>(position);
                return details;
            }
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
            using (var repository = _unitOfwork.NewDbContext())
            {
                var candidateStatus = repository.Get<CandidateStatus>(candidateStatusId);
                var details = AutoMapper.Mapper.Map<CandidateStatusDetails>(candidateStatus);
                return details;
            }
        }
    }
}