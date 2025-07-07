using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.DAL
{
    public class SalingDBContext:DbContext
    {
        public SalingDBContext(DbContextOptions<SalingDBContext> options) : base(options)
        {

        }
          public DbSet<Present> Present { get; set; }
        public DbSet<Donor> Donor { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<Order> Order { get; set; }


    }

}
