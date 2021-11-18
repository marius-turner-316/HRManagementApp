using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.HumanResources.Queries.GetHumanResources
{
    public class GetHumanResourcesQuery : IRequest<HumanResourcesVM>
    {
        public int? Status { get; set; }
        public string Department { get; set; }
        public int PageNumber { get; set; } = 1;
    }

    public class GetHumanResourcesQueryHandler : IRequestHandler<GetHumanResourcesQuery, HumanResourcesVM>
    {
        private readonly IHumanResourceRepository _repository;

        public GetHumanResourcesQueryHandler(IHumanResourceRepository repository)
        {
            _repository = repository;
        }

        public async Task<HumanResourcesVM> Handle(GetHumanResourcesQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(request.Status, request.Department, request.PageNumber);
            return Mapper.MapToHumanResourceVM(result);
        }
    }
}


