using Application.Common.Interfaces;
using FluentValidation;
using System;

namespace Application.HumanResources.Commands.UpdateHumanResource
{
    public class UpdateHumanResourceCommandValidator : AbstractValidator<UpdateHumanResourceCommand>
    {
        private readonly IHumanResourceRepository _repository;
        private readonly ISystemClock _systemClock;

        public UpdateHumanResourceCommandValidator(
            IHumanResourceRepository repository,
            ISystemClock systemClock)
        {
            _repository = repository;
            _systemClock = systemClock;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 200 characters.");

            RuleFor(x => x)
                .Must(BeUniqueEmail).WithMessage("The specified email already exists.")
                .Must(BeUniqueEmployeeNumber).WithMessage("The specified employee number already exists.");

            RuleFor(x => x.DOB)
                .Must(BeValidAge).WithMessage("Invalid {PropertyName}");

            RuleFor(x => x.Department)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("{PropertyName} is not valid.");

            RuleFor(x => x.EmployeeNumber)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        }

        protected bool BeValidAge(DateTime? date)
        {
            if (date is null) return true;

            const int minAge = 18;
            const int maxAge = 100;

            if (date.Value.AddYears(minAge) > _systemClock.Now)
            {
                return false;
            }  

            return date.Value.AddYears(maxAge) > _systemClock.Now;
        }

        protected bool BeUniqueEmail(UpdateHumanResourceCommand command)
        {
            var result = _repository.CheckEmailExists(command.Email?.Trim().ToLower());

            return result is null || result == command.HumanResourceId;
        }

        protected bool BeUniqueEmployeeNumber(UpdateHumanResourceCommand command)
        {
            var result = _repository.CheckEmployeeNumberExists(command.EmployeeNumber);

            return result is null || result == command.HumanResourceId;
        }
    }
}
