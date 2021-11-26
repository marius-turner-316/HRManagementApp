using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.HumanResources.Queries.GetHumanResources
{
    public class GetHumanResourcesQuery : IRequest<GetHumanResourcesQueryResult>
    {
        public int? StatusId { get; set; }
        public int? DepartmentId { get; set; }
        public int PageNumber { get; set; } = 1;
    }

    public class GetHumanResourcesQueryHandler : IRequestHandler<GetHumanResourcesQuery, GetHumanResourcesQueryResult>
    {
        private readonly IHumanResourceRepository _repository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IConfiguration _configuration;

        public GetHumanResourcesQueryHandler(
            IHumanResourceRepository repository,
            IDepartmentRepository departmentRepository,
            IConfiguration configuration)
        {
            _repository = repository;
            _departmentRepository = departmentRepository;
            _configuration = configuration;
        }

        public async Task<GetHumanResourcesQueryResult> Handle(GetHumanResourcesQuery request, CancellationToken cancellationToken)
        {
            var humanResources = await _repository.GetAllAsync(request.StatusId, request.DepartmentId, request.PageNumber);
            var departments = await _departmentRepository.GetAll();

            return new GetHumanResourcesQueryResult
            {
                HumanResources = humanResources.MapToPaginatedHumanResourceDto(_configuration.TotalResultsPerPage),
                Departments = departments.MapToDepartmentDtos()
            };
        }
    }
}


