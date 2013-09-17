using HiringManager.Mappers;
using HiringManager.Transactions;

namespace HiringManager.DomainServices.Impl
{
    public class PositionService : IPositionService
    {
        private readonly IFluentTransactionBuilder _builder;
        private readonly IFluentMapper _mapper;

        public PositionService(IFluentTransactionBuilder builder, IFluentMapper mapper)
        {
            _builder = builder;
            _mapper = mapper;
        }

        public QueryResponse<PositionSummary> Query(QueryPositionSummariesRequest request)
        {
            var transaction = _builder
                .Receives<QueryPositionSummariesRequest>()
                .Returns<QueryResponse<PositionSummary>>()
                .WithAuthorization()
                .WithRequestValidation()
                .WithPerformanceLogging()
                .Build()
                ;

            var response = transaction.Execute(request);

            return response;
        }

        public CreatePositionResponse CreatePosition(CreatePositionRequest request)
        {
            var transaction = this._builder
                .Receives<CreatePositionRequest>()
                .Returns<CreatePositionResponse>()
                .WithRequestValidation()
                .WithAuthorization()
                .WithPerformanceLogging()
                .Build()
                ;

            var response = transaction.Execute(request);
            return response;
        }

        public HireCandidateResponse Hire(HireCandidateRequest request)
        {
            var transaction = this._builder
                .Receives<HireCandidateRequest>()
                .Returns<HireCandidateResponse>()
                .WithRequestValidation()
                .WithAuthorization()
                .WithPerformanceLogging()
                .Build()
                ;

            var response = transaction.Execute(request);
            return response;
        }
    }
}