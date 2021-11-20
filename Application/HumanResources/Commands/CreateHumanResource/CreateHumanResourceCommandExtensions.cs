using Application.Domain;
using FluentValidation;

namespace Application.HumanResources.Commands.CreateHumanResource
{
    internal static class CreateHumanResourceCommandExtensions
    {
        public static void Validate(
            this CreateHumanResourceCommand request,
            IValidator<CreateHumanResourceCommand> validator)
        {
            var results = validator.Validate(request);
            if (!results.IsValid) throw new Common.Exceptions.ValidationException(results.Errors);
        }

        public static HumanResource MapToHumanResource(
            this CreateHumanResourceCommand request)
        {
            var model = new HumanResource();
            model.FirstName = request.FirstName;
            model.Surname = request.Surname;
            model.Email = request.Email;
            model.DOB = request.DOB;
            model.Department = request.Department;
            model.Status = request.Status;
            model.EmployeeNumber = request.EmployeeNumber;

            return model;
        }
    }
}
