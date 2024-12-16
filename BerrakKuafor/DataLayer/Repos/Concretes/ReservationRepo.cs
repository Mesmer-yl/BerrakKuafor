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
    public class ReservationRepo : GenericRepo<Reservation>, IReservationRepo
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<Reservation> _table;

        public ReservationRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
            _table = _appDbContext.Set<Reservation>();
        }
    }
}
