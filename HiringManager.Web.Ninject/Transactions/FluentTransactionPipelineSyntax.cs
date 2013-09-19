using System;
using System.Collections.Generic;
using System.Security.Principal;
using HiringManager.Transactions;
using Ninject;
using Simple.Validation;

namespace HiringManager.Web.Ninject.Transactions
{
    public class FluentTransactionPipelineSyntax<TRequest, TResponse> : IFluentTransactionPipelineSyntax<TRequest, TResponse>
    {
        private ITransaction<TRequest, TResponse> _baseCommand;
        private readonly IKernel _kernel;

        private readonly List<Func<ITransaction<TRequest, TResponse>, ITransaction<TRequest, TResponse>>> _instructions =
            new List<Func<ITransaction<TRequest, TResponse>, ITransaction<TRequest, TResponse>>>();


        public IFluentTransactionPipelineSyntax<TRequest, TResponse> WithRequestValidation(params string[] ruleSets)
        {
            _instructions.Add(input =>
                              {
                                  var validationEngine = _kernel.Get<IValidationEngine>();
                                  var decorator = new RequestValidationStep<TRequest, TResponse>(input, validationEngine, ruleSets);
                                  return decorator;
                              });
            return this;
        }

        public IFluentTransactionPipelineSyntax<TRequest, TResponse> WithPerformanceLogging()
        {
            _instructions.Add(input =>
                              {
                                  var clock = _kernel.Get<IClock>();
                                  var decorator = new PerformanceStep<TRequest, TResponse>(input, clock);
                                  return decorator;
                              });
            return this;
        }

        public IFluentTransactionPipelineSyntax<TRequest, TResponse> UsesImplementation<TCommandType>() where TCommandType : ITransaction<TRequest, TResponse>
        {
            _baseCommand = _kernel.Get<TCommandType>();
            return this;
        }

        public IFluentTransactionPipelineSyntax<TRequest, TResponse> WithErrorLogging()
        {
            _instructions.Add(input =>
                              {
                                  var decorator = new ErrorLoggingStep<TRequest, TResponse>(input);
                                  return decorator;
                              });

            return this;
        }



        public IFluentTransactionPipelineSyntax<TRequest, TResponse> WithAuthorization(params string[] roles)
        {
            _instructions.Add(input =>
                              {
                                  var principal = _kernel.Get<IPrincipal>();
                                  var decorator = new AuthorizationStep<TRequest, TResponse>(input, principal, roles);
                                  return decorator;
                              });
            return this;
        }

        public ITransaction<TRequest, TResponse> Build()
        {
            if (_baseCommand == null)
                _baseCommand = _kernel.Get<ITransaction<TRequest, TResponse>>();

            var result = _baseCommand;
            foreach (var decorator in _instructions)
            {
                result = decorator.Invoke(result);
            }
            return result;

        }

        public FluentTransactionPipelineSyntax(IKernel kernel)
        {
            _kernel = kernel;
        }
    }
}