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

        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Grop> Grops { get; set; }
        public IDbConnection Connection => Database.GetDbConnection();
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=MOHAMADREZA\\MSSQLSERVER02;Initial Catalog=Phonebook;trusted_connection=true;");
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=185.120.222.106\\MSSQLSERVER2019,51019;Initial Catalog=PhoneBooke_Db ; user Id=PhoneBooke; Password=1W3xb67y_");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        }
    }
