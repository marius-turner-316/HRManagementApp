using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public IReadOnlyDictionary<string, string> Errors { get; }
        public ValidationException(IEnumerable<ValidationFailure> failures)
        {
            Errors = failures.ToDictionary(x => x.PropertyName, y => y.ErrorMessage);
        }
    }
}
