using EntityLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Abstracts
{
    public interface IReservationService
    {
        List<EmployeeWithServiceVM> GetEmployeesByHairdresserId(int hairdresserId);
        List<ServiceVM> GetServiceByHairdresserId(int hairdresserId);
        bool CheckReservation(int employeeId,DateTime date,int time,int duration);
        void CreateReservation(ReservationAddVM _reservationAddVM);
        List<ReservationsForModVM> GetAllReservationsByHairdresserId(int hairdresserId);
        List<ReservationsForEmpVM> GetAllReservationsByEmployeeId(int employeeId);
        Task<List<ReservationsForUserVM>> GetAllReservationsByUserId(string userName);
        void ChangeReservationStatus(int reservationId, bool status, string? reason);
    }
}
