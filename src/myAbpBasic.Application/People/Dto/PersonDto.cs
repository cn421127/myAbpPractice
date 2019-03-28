using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;


namespace myAbpBasic.People.Dto
{
    class PersonDto
    {
    }

    [AutoMapFrom(typeof(Person))]
    public class PersonListDto :  AuditedEntity<Guid>
    {

        public string Name { get; set; }
    }

    public class GetAllPersonInput
    {
        public string Name { get; set; }
    }

    [AutoMapTo(typeof(Person))]
    public class CreatePersonInput
    {
        [Required]
        [StringLength(Person.MaxNameLength)]
        public string Name { get; set; }
        
    }
}
