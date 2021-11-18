using Application.Common.Models;
using Application.Domain;
using System.Linq;

namespace Application.HumanResources.Queries.GetOneHumanResource
{
    internal static class Mapper
    {
        public static HumanResourceVM MapToHumanResourceVM(HumanResource humanResource)
        {
            var model = new HumanResourceVM();
            model.FirstName = humanResource.FirstName;
            model.Surname = humanResource.Surname;
            model.Email = humanResource.Email;
            model.DOB = humanResource.DOB;
            model.Department = humanResource.Department;
            model.Status = humanResource.Status;
            model.EmployeeNumber = humanResource.EmployeeNumber;

            return model;
        }
    }
}

