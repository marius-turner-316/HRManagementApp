using Application.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.HumanResources.Queries.GetOneHumanResource
{
    public class HumanResourceVM
    {
        public int HumanResourceId { get; set; }
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
}
