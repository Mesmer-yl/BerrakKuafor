using DataLayer.Repos.Abstracts;
using EntityLayer.Concretes;
using EntityLayer.ViewModels;
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

        public ReservationService(IEmployeeRepo employeeRepo, IEmployeeServiceRepo employeeServiceRepo, IReservationRepo reservationRepo, IShiftRepo shiftRepo)
        {
            _employeeRepo = employeeRepo;
            _employeeServiceRepo = employeeServiceRepo;
            _reservationRepo = reservationRepo;
            _shiftRepo = shiftRepo;
        }

        public bool CheckReservation(int employeeId, DateTime date, int time, int duration)
        {
            var shift = _shiftRepo.GetAllByCondition(c=>c.EmployeeId==employeeId && c.DayOfWeek==date.DayOfWeek).SingleOrDefault();
            if(shift==null) return false;
            int openTime = (int)shift.StartTime.TotalMinutes;
            int closeTime = (int)shift.EndTime.TotalMinutes;
            if (time<openTime || (time + duration) > closeTime)
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
