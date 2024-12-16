using DataLayer.Context;
using DataLayer.Repos.Abstracts;
using EntityLayer.Concretes;
using EntityLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repos.Concretes
{
    public class EmployeeRepo : GenericRepo<Employee>, IEmployeeRepo
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<Employee> _table;

        public EmployeeRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
            _table = _appDbContext.Set<Employee>();
        }

        public List<EmployeeWithDetailsViewModel> GetEmployeesWithServicesByHairdresserId(int hairdresserId)
        {
            var result = _table
            .Where(e => e.HairdresserId == hairdresserId)
            .Include(e => e.AppUser)
            .Include(e => e.EmployeeServices)
                .ThenInclude(es => es.Service)
            .SelectMany(e => e.EmployeeServices, (e, es) => new EmployeeWithDetailsViewModel
            {
                EmployeeId = e.Id,
                EmployeeName = e.AppUser.NameSurname,
                IsPro = es.IsPro,
                Money = es.Money,
                Duration = es.Duration,
                ServiceId = es.ServiceId,
                ServiceName = es.Service.Name
            })
            .ToList();
            return result;
        }
    }
}
