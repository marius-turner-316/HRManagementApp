using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.HumanResources.Queries.GetOneHumanResource
{
    public class GetOneHumanResourceQuery : IRequest<HumanResourceVM>
    {
        public int HumanResourceId { get; set; }
    }

    public class GetHumanResourceQueryHandler : IRequestHandler<GetOneHumanResourceQuery, HumanResourceVM>
    {
        private readonly IHumanResourceRepository _repository;

        public GetHumanResourceQueryHandler(IHumanResourceRepository repository)
        {
            _repository = repository;
        }

        public async Task<HumanResourceVM> Handle(GetOneHumanResourceQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.HumanResourceId);
            return Mapper.MapToHumanResourceVM(result);
        }
    }
}


