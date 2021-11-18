using Application.Domain;

namespace Application.HumanResources.Commands.UpdateHumanResource
{
    internal static class Mapper
    {
        public static HumanResource MapToHumanResource(UpdateHumanResourceCommand request)
        {
            var model = new HumanResource();
            model.HumanResourceId = request.HumanResourceId;
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
