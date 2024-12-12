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
    public class ServiceRepo : GenericRepo<Service>, IServiceRepo
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<Service> _table;

        public ServiceRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
            _table = _appDbContext.Set<Service>();
        }
    }
}
