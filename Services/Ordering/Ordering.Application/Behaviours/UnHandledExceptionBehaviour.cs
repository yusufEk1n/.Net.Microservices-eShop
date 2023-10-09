using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviours
{
    /// <summary>
    /// UnHandled Exception Behaviour class used to handle unhandled exceptions
    /// </summary>
    public class UnHandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnHandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handle method to handle unhandled exceptions
        /// </summary>
        /// <param name="request">Request object</param>
        /// <param name="next">Next handler</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> The <see cref="TResponse"/>. </returns>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "Application request: Unhandled exception for request {Name} {@Request}", requestName, request);
                throw;
            }
        }
    }
}
