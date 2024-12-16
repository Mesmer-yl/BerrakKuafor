using EntityLayer.Concretes;
using EntityLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repos.Abstracts
{
    public interface IEmployeeRepo : IGenericRepo<Employee>
    {
        List<EmployeeWithDetailsViewModel> GetEmployeesWithServicesByHairdresserId(int hairdresserId);
    }
}
