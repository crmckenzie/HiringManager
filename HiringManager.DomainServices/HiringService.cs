using System;
using HiringManager.Transactions;

namespace HiringManager.DomainServices
{
    public class HiringService : IHiringService
    {
        private readonly IFluentTransactionBuilder _transactionBuilder;

        public HiringService(IFluentTransactionBuilder transactionBuilder)
        {
            _transactionBuilder = transactionBuilder;
        }

        public CreatePositionResponse CreatePosition(CreatePositionRequest request)
        {
            var transaction = this._transactionBuilder
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
            var transaction = this._transactionBuilder
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