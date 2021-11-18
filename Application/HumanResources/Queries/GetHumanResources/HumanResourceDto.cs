using Application.Domain.Enums;
using System;

namespace Application.HumanResources.Queries.GetHumanResources
{
    public class HumanResourceDto
    {
        public int HumanResourceId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        public string Department { get; set; }
        public Status Status { get; set; }
        public int EmployeeNumber { get; set; }
    }
}
