using PhonBook.Core.Domain.Contacts;
using PhonBook.Core.Domain.Grops;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace PhonBook.infra.Data.sqlserver.Common
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext()
        {

        }
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options)
            : base(options)
        { }
        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        //{
        //}

        public  DbSet<Contact> Contacts { get; set; }
        public  DbSet<Grop> Grops { get; set; }
        public IDbConnection Connection => Database.GetDbConnection();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=PhoneBooke_Db;trusted_connection=true;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        }
    }
