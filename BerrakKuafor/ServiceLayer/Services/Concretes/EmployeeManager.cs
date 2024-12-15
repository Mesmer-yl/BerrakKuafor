using DataLayer.Repos.Abstracts;
using EntityLayer.Concretes;
using EntityLayer.ViewModels;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Concretes
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IEmployeeServiceRepo _employeeServiceRepo;
        private readonly IShiftRepo _shiftRepo;
        private readonly UserManager<AppUser> _userManager;

        public EmployeeManager(IEmployeeRepo employeeRepo, UserManager<AppUser> userManager, IEmployeeServiceRepo employeeServiceRepo, IShiftRepo shiftRepo)
        {
            _employeeRepo = employeeRepo;
            _userManager = userManager;
            _employeeServiceRepo = employeeServiceRepo;
            _shiftRepo = shiftRepo;
        }

        public async Task CreateEmployee(EmployeeAddVM _employeeAddVM, int hairdresserId)
        {
            var currentUser = await _userManager.FindByEmailAsync(_employeeAddVM.Email);
            var employee = new Employee()
            {
                EmployeeId = currentUser.Id,
                HairdresserId = hairdresserId
            };
            _employeeRepo.Add(employee);
            _employeeRepo.Save();
        }

        public void CreateEmployeeService(EmployeeServiceAddVM _employeeServiceAddVM)
        {
            var employeeService = new EmployeeService()
            {
                ServiceId= _employeeServiceAddVM.ServiceId,
                EmployeeId= _employeeServiceAddVM.EmployeeId,
                Money= _employeeServiceAddVM.Money,
                Duration    = _employeeServiceAddVM.Duration,
                IsPro= _employeeServiceAddVM.IsPro
            };
            _employeeServiceRepo.Add(employeeService);
            _employeeServiceRepo.Save();
        }

        public void AddOrUpdateShift(ShiftAddVM _shiftAddVM)
        {
            var shiftThere = _shiftRepo.GetAllByCondition(x=>x.EmployeeId==_shiftAddVM.EmployeeId && x.DayOfWeek==_shiftAddVM.DayOfWeek).SingleOrDefault();
            if (shiftThere != null)
            {
                shiftThere.StartTime = _shiftAddVM.StartTime;
                shiftThere.EndTime = _shiftAddVM.EndTime;
                _shiftRepo.Update(shiftThere);
            }
            else
            {

                var shift = new Shift()
                {
                    DayOfWeek = _shiftAddVM.DayOfWeek,
                    StartTime = _shiftAddVM.StartTime,
                    EndTime = _shiftAddVM.EndTime,
                    EmployeeId = _shiftAddVM.EmployeeId
                };
                _shiftRepo.Add(shift);
            }
            _shiftRepo.Save();
        }

        public List<EmployeeServiceUpdateVM> GetAllEmployeeService(int employeeId)
        {
            var employeeServices = _employeeServiceRepo.GetAllByCondition(x=>x.EmployeeId == employeeId,"Service");
            var employeServiceAddListVM = new List<EmployeeServiceUpdateVM>();
            foreach(var employeeService in employeeServices)
            {
                var employeeServiceAddVM = new EmployeeServiceUpdateVM()
                {
                    Id = employeeService.Id,
                    EmployeeId=employeeService.EmployeeId,
                    ServiceId=employeeService.ServiceId,
                    Money=employeeService.Money,
                    IsPro=employeeService.IsPro,
                    Duration=employeeService.Duration
                };
                employeServiceAddListVM.Add(employeeServiceAddVM);
            }
            return employeServiceAddListVM;
        }

        public List<EmployeeVM> GetAllEmployeesWithUser(int hairdresserId)
        {
            var employees = _employeeRepo.GetAllByCondition(x=>x.HairdresserId==hairdresserId,"AppUser");
            var employeeList = new List<EmployeeVM>();
            foreach (var employee in employees)
            {
                var employeeVM = new EmployeeVM()
                {
                    Id = employee.Id,
                    NameSurname = employee.AppUser.NameSurname,
                    PhoneNumber = employee.AppUser.PhoneNumber!
                };
                employeeList.Add(employeeVM);
            }
            return employeeList;
        }

        public List<ShiftsVM> GetAllShiftByEmployee(int employeeId)
        {
            var shifts = _shiftRepo.GetAllByCondition(x => x.EmployeeId == employeeId);
            var shiftListModel = new List<ShiftsVM>();
            foreach(var shift in shifts)
            {
                var shiftModel = new ShiftsVM()
                {
                    Id = shift.Id,
                    EmployeeId = shift.EmployeeId,
                    DayOfWeek = shift.DayOfWeek,
                    StartTime = shift.StartTime,
                    EndTime = shift.EndTime
                };
                shiftListModel.Add(shiftModel);
            }
            return shiftListModel;
        }

        public void UpdateEmployeeService(EmployeeServiceUpdateVM _employeeServiceUpdateVM)
        {
            var id = _employeeServiceUpdateVM.Id;
            var employeeService = _employeeServiceRepo.GetById(id);
            employeeService.Money = _employeeServiceUpdateVM.Money;
            employeeService.Duration = _employeeServiceUpdateVM.Duration;
            employeeService.IsPro= _employeeServiceUpdateVM.IsPro;
            _employeeServiceRepo.Update(employeeService);
            _employeeServiceRepo.Save();

        }

    }
}
