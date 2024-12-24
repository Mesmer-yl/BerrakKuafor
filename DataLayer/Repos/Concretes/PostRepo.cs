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
    public class PostRepo : GenericRepo<Post>, IPostRepo
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<Post> _table;

        public PostRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
            _table = _appDbContext.Set<Post>();
        }
    }
}
