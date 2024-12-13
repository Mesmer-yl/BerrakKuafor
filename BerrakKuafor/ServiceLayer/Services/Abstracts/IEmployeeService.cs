using EntityLayer.Concretes;
using EntityLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Abstracts
{
    public interface IEmployeeService
    {
        Task CreateEmployee(EmployeeAddVM _employeeAddVM, int hairdresserId);
        List<EmployeeVM> GetAllEmployeesWithUser(int hairdresserId);
        List<EmployeeServiceUpdateVM> GetAllEmployeeService(int employeeId);
        void CreateEmployeeService(EmployeeServiceAddVM _employeeServiceAddVM);
        void UpdateEmployeeService(EmployeeServiceUpdateVM _employeeServiceUpdateVM);
    }
}
