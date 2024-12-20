using EntityLayer.Concretes;
using EntityLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Abstracts
{
    public interface IHairdresserService
    {
        Task<bool> CreateHairdresserAsync(HairdresserAddVM _hairdresserAddVM);
        void UpdateHairdresserAsync(HairdresserUpdateVM _hairdresserUpdVM);
        Task DeleteHairdresserAsync(int hairdresserId);
        HairdresserUpdateVM GetHairdresserById(int hairdresserId);
        List<HairdresserVM> GetAllHairdresser();
        //Service
        void CreateService(ServiceAddVM _serviceAddVM);
        void UpdateService(ServiceVM _serviceVM);
        List<ServiceVM> GetAllServices();
        //Hairdresser Management
        Task<HairdresserUpdateVM> GetHairdresserIdByUserAsync(string email);
    }
}
