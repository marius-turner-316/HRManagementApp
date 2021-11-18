using Application.HumanResources.Commands.UpdateHumanResource;
using Application.HumanResources.Queries.GetOneHumanResource;

namespace MVC.Extensions
{
    public static class HumanResourceVMExtensions
    {
        public static UpdateHumanResourceCommand ToUpdateHumanResourceCommand(this HumanResourceVM model)
        {
            var result = new UpdateHumanResourceCommand();
            result.HumanResourceId = model.HumanResourceId;
            result.Department = model.Department;
            result.DOB = model.DOB;
            result.Email = model.Email;
            result.EmployeeNumber = model.EmployeeNumber;
            result.FirstName = model.FirstName;
            result.Surname = model.Surname;
            result.Status = model.Status;

            return result;
        }
    }
}