using Microsoft.EntityFrameworkCore;

namespace ContactlessOrder.DAL.EF
{
    public partial class ContactlessOrderContext : DbContext
    {
        public ContactlessOrderContext(DbContextOptions<ContactlessOrderContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
