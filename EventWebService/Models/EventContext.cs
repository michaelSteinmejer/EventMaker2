namespace EventWebService.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EventContext : DbContext
    {
        public EventContext()
            : base("name=EventContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Event> Event { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .Property(e => e.Description)
                .IsFixedLength();
        }
    }
}
