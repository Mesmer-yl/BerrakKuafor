using DataLayer.Repos.Abstracts;
using EntityLayer.Concretes;
using EntityLayer.ViewModels;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Concretes
{
    public class ReservationService : IReservationService
    {
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IEmployeeServiceRepo _employeeServiceRepo;
        private readonly IReservationRepo _reservationRepo;
        private readonly IShiftRepo _shiftRepo;
        private readonly UserManager<AppUser> _userManager;
        public ReservationService(IEmployeeRepo employeeRepo, IEmployeeServiceRepo employeeServiceRepo, IReservationRepo reservationRepo, IShiftRepo shiftRepo, UserManager<AppUser> userManager)
        {
            _employeeRepo = employeeRepo;
            _employeeServiceRepo = employeeServiceRepo;
            _reservationRepo = reservationRepo;
            _shiftRepo = shiftRepo;
            _userManager = userManager;
        }

        public void ChangeReservationStatus(int reservationId, bool status, string? reason)
        {
            var reservation = _reservationRepo.GetById(reservationId);
            reservation.IsStatus = status;
            reservation.Reason = reason;
            _reservationRepo.Update(reservation);
            _reservationRepo.Save();
        }

        public bool CheckReservation(int employeeId, DateTime date, int time, int duration)
        {
            var shift = _shiftRepo.GetAllByCondition(c=>c.EmployeeId==employeeId && c.DayOfWeek==date.DayOfWeek).SingleOrDefault();
            if(shift==null) return false;
            int openTime = (int)shift.StartTime.TotalMinutes;
            int closeTime = (int)shift.EndTime.TotalMinutes;
            if (time<=openTime || (time + duration) >= closeTime)
            {
                return false;
            }
            var reservations = _reservationRepo.GetAllByCondition(x=>x.EmployeeId == employeeId && x.Date==date && x.IsStatus!=false);
            foreach (var reservation in reservations) {
                int startTime = (int)reservation.StartTime.TotalMinutes;
                int endTime = (int)reservation.EndTime.TotalMinutes;
                if ((startTime <= time && time <= endTime) || (startTime <= (time + duration) && (time + duration) <= endTime)) {
                    return false;
                }
            }
            return true;
        }

        public void CreateReservation(ReservationAddVM _reservationAddVM)
        {
            var reservation = new Reservation()
            {
                HairdresserId = _reservationAddVM.HairdresserId,
                EmployeeId = _reservationAddVM.EmployeeId,
                UserId = _reservationAddVM.UserId,
                Money = _reservationAddVM.Money,
                StartTime = _reservationAddVM.StartTime,
                EndTime = _reservationAddVM.EndTime,
                Services = _reservationAddVM.Services,
                Date = _reservationAddVM.Date
            };
            _reservationRepo.Add(reservation);
            _reservationRepo.Save();
        }

        public List<ReservationsForEmpVM> GetAllReservationsByEmployeeId(int employeeId)
        {
            var reservations = _reservationRepo.GetAllByCondition(x => x.EmployeeId == employeeId, "AppUser");
            var resList = new List<ReservationsForEmpVM>();
            foreach (var reservation in reservations)
            {
                var res = new ReservationsForEmpVM()
                {
                    Id = reservation.Id,
                    UserId = reservation.UserId,
                    UserNameSurname = reservation.AppUser.NameSurname,
                    PhoneNumber = reservation.AppUser.PhoneNumber,
                    StartTime = reservation.StartTime,
                    EndTime = reservation.EndTime,
                    Date = reservation.Date,
                    Money = reservation.Money,
                    Services = reservation.Services,
                    IsStatus = reservation.IsStatus,
                    Reason = reservation.Reason
                };
                resList.Add(res);
            }
            return resList;
        }

        public List<ReservationsForModVM> GetAllReservationsByHairdresserId(int hairdresserId)
        {
            var reservations = _reservationRepo.GetAllByCondition(x=>x.HairdresserId == hairdresserId,"AppUser,Employee,Employee.AppUser");
            var resList  = new List<ReservationsForModVM>();
            foreach (var reservation in reservations)
            {
                var res = new ReservationsForModVM()
                {
                    Id = reservation.Id,
                    EmployeeNameSurname = reservation.Employee.AppUser.NameSurname,
                    UserId = reservation.UserId,
                    UserNameSurname = reservation.AppUser.NameSurname,
                    PhoneNumber = reservation.AppUser.PhoneNumber,
                    StartTime = reservation.StartTime,
                    EndTime = reservation.EndTime,
                    Date = reservation.Date,
                    Money = reservation.Money,
                    Services = reservation.Services,
                    IsStatus = reservation.IsStatus,
                    Reason  = reservation.Reason
                };
                resList.Add(res);
            }
            return resList;
        }

        public async Task<List<ReservationsForUserVM>> GetAllReservationsByUserId(string userName)
        {
            var currenUser = await _userManager.FindByNameAsync(userName);  
            var reservations = _reservationRepo.GetAllByCondition(x => x.UserId == currenUser.Id, "Hairdresser,Employee,Employee.AppUser");
            var resList = new List<ReservationsForUserVM>();
            foreach (var reservation in reservations)
            {
                var res = new ReservationsForUserVM()
                {
                    HairdresserName = reservation.Hairdresser.Name,
                    EmployeeName = reservation.Employee.AppUser.NameSurname,
                    Services = reservation.Services,
                    Date = reservation.Date,
                    Time = reservation.StartTime,
                    Money = reservation.Money,
                    Reason = reservation.Reason
                };

                if (reservation.IsStatus == true) { res.Status = "Onaylandı"; } else if (reservation.IsStatus == false) { res.Status = "Reddedildi"; } else { res.Status = "Beklemede"; }
                resList.Add(res);
            }
            return resList;
        }

        public List<EmployeeWithServiceVM> GetEmployeesByHairdresserId(int hairdresserId)
        {
            var employees = _employeeRepo.GetAllByCondition(x=>x.HairdresserId == hairdresserId,"AppUser");
            var employeesWithService = _employeeRepo.GetEmployeesWithServicesByHairdresserId(hairdresserId);
            var esVMlist = new List<EmployeeWithServiceVM>();
            foreach (var employee in employees)
            {
                var esVM = new EmployeeWithServiceVM()
                {
                    EmployeeId = employee.Id,
                    EmployeeName = employee.AppUser.NameSurname,
                    Services = new List<EServiceModel>()
                };
                foreach (var es in employeesWithService)
                {
                    if(employee.Id == es.EmployeeId)
                    {
                        var eServiceModel = new EServiceModel()
                        {
                            Id = es.ServiceId,
                            Name = es.ServiceName,
                            IsPro = es.IsPro,
                            Money = es.Money,
                            Duration = es.Duration
                        };
                        esVM.Services.Add(eServiceModel);
                    }
                }
                esVMlist.Add(esVM);
            }
            return esVMlist;
        }

        public List<ServiceVM> GetServiceByHairdresserId(int hairdresserId)
        {
            var employeesWithService = _employeeRepo.GetEmployeesWithServicesByHairdresserId(hairdresserId);
            var result = employeesWithService.GroupBy(e => e.ServiceId).Select(g => g.First()).ToList();
            var services = new List<ServiceVM>();
            foreach (var es in result)
            {
                var service = new ServiceVM()
                {
                    Id = es.ServiceId,
                    Name = es.ServiceName
                };
                services.Add(service);
            }
            return services;
        }
    }
}
