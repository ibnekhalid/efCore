using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Application.Company.Commands.ViewModel
{
    public class CompanyVm : BaseVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public State Status { get; set; }
        //public List<User> Users { get; set; }
        //public List<Project> Projects { get; set; }
        public static CompanyVm Map(Core.Model.Company comp)
        => new CompanyVm()
        {
            Id = comp.Id,
            Name = comp.Name,
            Status = comp.Status
        };
        public static List<CompanyVm> Map(List<Core.Model.Company> comps)
       => comps.Select(comp => new CompanyVm()
       {
           Id = comp.Id,
           Name = comp.Name,
           Status = comp.Status
       }).ToList();
    }
    public class CreateCompanyVm
    {
        [Required]
        public string Name { get; set; }
    }
    public class UpdateCompanyVm : CreateCompanyVm
    {
        [Required]
        public int Id { get; set; }
    }
}
