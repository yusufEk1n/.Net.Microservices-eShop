﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Excepitons
{
    /// <summary>
    /// Validation Exception class used for throwing custom exception
    /// </summary>
    public class ValidationException : ApplicationException
    {
        public ValidationException()
            : base("One or more validation failures have occured.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) 
            : this()
        {
            Errors = failures
                .GroupBy(p => p.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        //Dictionary of errors
        public Dictionary<string, string[]> Errors { get; }
    }
}
