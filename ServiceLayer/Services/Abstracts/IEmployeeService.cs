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
        Task<bool> CreateEmployee(EmployeeAddVM _employeeAddVM, int hairdresserId);
        List<EmployeeVM> GetAllEmployeesWithUser(int hairdresserId);
        //EmployeService OP
        List<EmployeeServiceUpdateVM> GetAllEmployeeService(int employeeId);
        void CreateEmployeeService(EmployeeServiceAddVM _employeeServiceAddVM);
        void UpdateEmployeeService(EmployeeServiceUpdateVM _employeeServiceUpdateVM);
        //Shift OP
        List<ShiftsVM> GetAllShiftByEmployee(int employeeId);
        void AddOrUpdateShift(ShiftAddVM _shiftAddVM);
    }
}
