using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options) :base(options)
        {

        }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Temel (base) sınıfta (IdentityDbContext<User>)
            //tanımlı olan konfigürasyonların da uygulanması için bu çağrı yapılır.
            //Böylece, Identity’ye ait tablolar (Users, Roles, vb.) doğru şekilde yapılandırılır.
            base.OnModelCreating(modelBuilder);

            //configration models
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.ApplyConfiguration(new BookConfig());
        }
    }
}
