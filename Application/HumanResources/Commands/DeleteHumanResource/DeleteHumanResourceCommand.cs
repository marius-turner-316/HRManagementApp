using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.HumanResources.Commands.DeleteHumanResource
{
    public class DeleteHumanResourceCommand : IRequest
    {
        public int HumanResourceId { get; set; }
    }

    public class DeleteHumanResourceCommandHandler : IRequestHandler<DeleteHumanResourceCommand>
    {
        private readonly IHumanResourceRepository _repository;

        public DeleteHumanResourceCommandHandler(IHumanResourceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteHumanResourceCommand request, CancellationToken cancellationToken)
        {
            _ = await _repository.DeleteAsync(request.HumanResourceId);

            return Unit.Value;
        }
    }
}
