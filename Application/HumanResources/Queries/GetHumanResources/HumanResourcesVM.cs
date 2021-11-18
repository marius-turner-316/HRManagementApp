using System.Collections.Generic;

namespace Application.HumanResources.Queries.GetHumanResources
{
    public class HumanResourcesVM
    {
        public IList<HumanResourceDto> Items { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public HumanResourcesVM()
        {
            Items = new List<HumanResourceDto>();
        }
    }
}
