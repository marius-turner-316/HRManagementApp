using Application.Common.Interfaces;
using Application.Domain.Enums;
using FluentValidation;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.HumanResources.Commands.CreateHumanResource
{
    public class CreateHumanResourceCommand : IRequest
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }
        public string Department { get; set; }
        public Status Status { get; set; }
        public int EmployeeNumber { get; set; }
    }

    public class CreateHumanResourceCommandHandler : IRequestHandler<CreateHumanResourceCommand>
    {
        private readonly IHumanResourceRepository _repository;
        private readonly IValidator<CreateHumanResourceCommand> _validator;

        public CreateHumanResourceCommandHandler(
            IHumanResourceRepository repository,
            IValidator<CreateHumanResourceCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Unit> Handle(CreateHumanResourceCommand request, CancellationToken cancellationToken)
        {
            request.Validate(_validator);
            var model = request.MapToHumanResource();
            await _repository.AddAsync(model);

            return Unit.Value;
        }
    }
}
