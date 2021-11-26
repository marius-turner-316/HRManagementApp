using Application.Common.Models;
using Application.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Application.HumanResources.Queries.GetHumanResources
{
    public static class Extensions
    {
        public static PaginatedList<HumanResourceDto> MapToPaginatedHumanResourceDto(
            this PaginatedList<HumanResource> paginatedList, int pageSize)
        {
            var model = new PaginatedList<HumanResourceDto>(
                paginatedList.Items.Select(x => new HumanResourceDto
                {
                    HumanResourceId = x.HumanResourceId,
                    FirstName = x.FirstName,
                    Surname = x.Surname,
                    Email = x.Email,
                    DOB = x.DOB,
                    Department = x.Department,
                    Status = x.Status,
                    EmployeeNumber = x.EmployeeNumber
                }).ToList(),
                paginatedList.TotalCount,
                paginatedList.PageNumber,
                pageSize);

            return model;
        }

        public static IEnumerable<DepartmentDto> MapToDepartmentDtos(
            this IEnumerable<Department> departments)
        {
            var model = departments.Select(x => new DepartmentDto
            {
                DepartmentId = x.DepartmentId,
                Name = x.Name
            }).ToList();

            return model;
        }
    }
}
