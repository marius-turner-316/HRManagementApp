using Application.Common.Models;
using System.Collections.Generic;

namespace Application.HumanResources.Queries.GetHumanResources
{
    public class GetHumanResourcesQueryResult
    {
        public PaginatedList<HumanResourceDto> HumanResources { get; set; }
        public IEnumerable<DepartmentDto> Departments { get; set; }
    }
}
