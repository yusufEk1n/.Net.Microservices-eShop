using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = Ordering.Application.Excepitons.ValidationException;

namespace Ordering.Application.Behaviours
{
    /// <summary>
    /// Validation behavior class used to validate the request for the mediatr pipeline
    /// </summary>
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        /// <summary>
        /// Handle method to validate the request
        /// </summary>
        /// <param name="request">Request object</param>
        /// <param name="next">Next handler</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> The <see cref="TResponse"/>.</returns>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select
                    (v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    throw new ValidationException(failures);
                }
            }
            return await next();
        }
    }
}
