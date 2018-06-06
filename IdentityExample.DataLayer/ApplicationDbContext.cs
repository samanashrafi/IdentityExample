using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using IdentityExample.DomainClasses;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityExample.DataLayer
{
    public class ApplicationDbContext :
        IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>,
        IUnitOfWork
    {
        public DbSet<Address> Addresses { set; get; }
        public DbSet<FreeContent> FreeContents { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<SubFreeContent> SubFreeContents { get; set; }

        public DbSet<SubItem> SubItems { get; set; }
        public ApplicationDbContext()
            : base("MyIdentityDb")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<CustomRole>().ToTable("Roles");
            modelBuilder.Entity<CustomUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<CustomUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<CustomUserRole>().ToTable("UserRoles");
        }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public int SaveAllChanges()
        {
            return base.SaveChanges();
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {

            return ((DbSet<TEntity>)this.Set<TEntity>()).AddRange(entities);
        }

        public void ForceDatabaseInitialize()
        {
            Database.Initialize(force: true);
        }
    }
}
