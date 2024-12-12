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
    public class HairdresserService : IHairdresserService
    {
        private readonly IHairdresserRepo _hairdresserRepo;
        private readonly IServiceRepo _serviceRepo;
        private readonly UserManager<AppUser> _userManager;

        public HairdresserService(IHairdresserRepo hairdresserRepo, UserManager<AppUser> userManager, IServiceRepo serviceRepo)
        {
            _hairdresserRepo = hairdresserRepo;
            _userManager = userManager;
            _serviceRepo = serviceRepo;
        }

        public async Task<bool> CreateHairdresserAsync(HairdresserAddVM _hairdresserAddVM)
        {
            var manager = await _userManager.FindByEmailAsync(_hairdresserAddVM.ManagerMail);
            if(manager == null) return false;
            var newHairdresser = new Hairdresser()
            {
                Name = _hairdresserAddVM.Name,
                PhoneNumber = _hairdresserAddVM.PhoneNumber,
                Address = _hairdresserAddVM.Address,
                OpenTime = _hairdresserAddVM.OpenTime,
                CloseTime = _hairdresserAddVM.CloseTime,
                Field = _hairdresserAddVM.Field,
                ManagerId = manager!.Id
            };
            _hairdresserRepo.Add(newHairdresser);
            _hairdresserRepo.Save();
            return true;
        }

        public void CreateService(ServiceAddVM _serviceAddVM)
        {
            var service = new Service()
            {
                Name = _serviceAddVM.Name
            };
            _serviceRepo.Add(service);
            _serviceRepo.Save();
        }

        public async Task DeleteHairdresserAsync(int hairdresserId)
        {
            var currentHairdresser = _hairdresserRepo.GetById(hairdresserId);
            var managerId = currentHairdresser.ManagerId;
            _hairdresserRepo.Delete(currentHairdresser);
            _hairdresserRepo.Save();

        }

        public List<HairdresserVM> GetAllHairdresser()
        {
            var hairdresserList = _hairdresserRepo.GetAll("AppUser");
            var vmList = new List<HairdresserVM>();
            foreach(var item in hairdresserList)
            {
                var vm = new HairdresserVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    PhoneNumber = item.PhoneNumber,
                    Address = item.Address,
                    OpenTime = item.OpenTime,
                    CloseTime = item.CloseTime,
                    Field = item.Field,
                    ManagerName = item.AppUser.NameSurname
                };
                vmList.Add(vm);
            }
            return vmList;
        }

        public List<ServiceVM> GetAllServices()
        {
            var services = _serviceRepo.GetAll();
            var vmList = new List<ServiceVM>();
            foreach (var item in services)
            {
                var vm = new ServiceVM()
                {
                    Name = item.Name,
                    Id = item.Id
                };
                vmList.Add(vm);
            }
            return vmList;
        }

        public HairdresserUpdateVM GetHairdresserById(int hairdresserId)
        {
            var currentHairdresser = _hairdresserRepo.GetById(hairdresserId);
            return new HairdresserUpdateVM()
            {
                Name = currentHairdresser.Name,
                PhoneNumber = currentHairdresser.PhoneNumber,
                Address = currentHairdresser.Address,
                OpenTime    = currentHairdresser.OpenTime,
                CloseTime = currentHairdresser.CloseTime,
                Field = currentHairdresser.Field,
                Id = currentHairdresser.Id
            };
        }

        public void UpdateHairdresserAsync(HairdresserUpdateVM _hairdresserUpdVM)
        {
            var currentHairdresser = _hairdresserRepo.GetById(_hairdresserUpdVM.Id);
            currentHairdresser.Name= _hairdresserUpdVM.Name;
            currentHairdresser.PhoneNumber= _hairdresserUpdVM.PhoneNumber;
            currentHairdresser.Address= _hairdresserUpdVM.Address;
            currentHairdresser.OpenTime= _hairdresserUpdVM.OpenTime;
            currentHairdresser.CloseTime= _hairdresserUpdVM.CloseTime;
            currentHairdresser.Field= _hairdresserUpdVM.Field;
            _hairdresserRepo.Update(currentHairdresser);
            _hairdresserRepo.Save();
        }

        public void UpdateService(ServiceVM _serviceVM)
        {
            var service = _serviceRepo.GetById(_serviceVM.Id);
            service.Name= _serviceVM.Name;
            _serviceRepo.Update(service);
            _serviceRepo.Save();
        }
    }
}
