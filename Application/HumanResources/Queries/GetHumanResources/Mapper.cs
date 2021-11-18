using Application.Common.Models;
using Application.Domain;
using System.Linq;

namespace Application.HumanResources.Queries.GetHumanResources
{
    internal static class Mapper
    {
        public static HumanResourcesVM MapToHumanResourceVM(PaginatedList<HumanResource> paginatedList)
        {
            var model = new HumanResourcesVM();
            model.Items = paginatedList.Items.Select(x => new HumanResourceDto
            {
                HumanResourceId = x.HumanResourceId,
                FirstName = x.FirstName,
                Surname = x.Surname,
                Email = x.Email,
                DOB = x.DOB,
                Department = x.Department,
                Status = x.Status,
                EmployeeNumber = x.EmployeeNumber
            }).ToList();
            model.PageNumber = paginatedList.PageNumber;
            model.TotalPages = paginatedList.TotalPages;
            model.TotalCount = paginatedList.TotalCount;

            return model;
        }
    }
}

