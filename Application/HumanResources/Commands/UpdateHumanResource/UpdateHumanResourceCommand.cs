using Application.Common.Interfaces;
using Application.Domain.Enums;
using FluentValidation;
using MediatR;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.HumanResources.Commands.UpdateHumanResource
{
    public class UpdateHumanResourceCommand : IRequest
    {
        public int HumanResourceId { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }
        public string Department { get; set; }
        public Status Status { get; set; }

        [DisplayName("Employee Number")]
        public int EmployeeNumber { get; set; }
    }

    public class UpdateHumanResourceCommandHandler : IRequestHandler<UpdateHumanResourceCommand>
    {
        private readonly IHumanResourceRepository _repository;
        private readonly IValidator<UpdateHumanResourceCommand> _validator;

        public UpdateHumanResourceCommandHandler(
            IHumanResourceRepository repository,
            IValidator<UpdateHumanResourceCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateHumanResourceCommand request, CancellationToken cancellationToken)
        {
            request.Validate(_validator);
            var model = request.MapToHumanResource();
            _ = await _repository.UpdateAsync(model);

            return Unit.Value;
        }
    }
}
