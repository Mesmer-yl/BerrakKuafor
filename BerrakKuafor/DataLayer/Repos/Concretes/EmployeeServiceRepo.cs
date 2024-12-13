using DataLayer.Context;
using DataLayer.Repos.Abstracts;
using EntityLayer.Concretes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repos.Concretes
{
    public class EmployeeServiceRepo : GenericRepo<EmployeeService>, IEmployeeServiceRepo
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<EmployeeService> _table;

        public EmployeeServiceRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
            _table = _appDbContext.Set<EmployeeService>();
        }
    }
}
