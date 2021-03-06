using Application.Domain;
using FluentValidation;

namespace Application.HumanResources.Commands.UpdateHumanResource
{
    internal static class UpdateHumanResourceCommandExtensions
    {
        public static void Validate(
            this UpdateHumanResourceCommand request,
            IValidator<UpdateHumanResourceCommand> validator)
        {
            var results = validator.Validate(request);
            if (!results.IsValid) throw new Common.Exceptions.ValidationException(results.Errors);
        }

        public static HumanResource MapToHumanResource(
            this UpdateHumanResourceCommand request)
        {
            var model = new HumanResource();
            model.HumanResourceId = request.HumanResourceId;
            model.FirstName = request.FirstName?.Trim();
            model.Surname = request.Surname?.Trim();
            model.Email = request.Email?.Trim().ToLower();
            model.DOB = request.DOB;
            model.Department = request.Department?.Trim();
            model.Status = request.Status;
            model.EmployeeNumber = request.EmployeeNumber;

            return model;
        }
    }
}
