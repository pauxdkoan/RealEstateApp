
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Domain.Entities;


namespace RealEstateApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> opt):base(opt) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties  { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<SalesType> SalesTypes { get; set; }
        public DbSet<Improvement> Improvements { get; set; }
        public DbSet<PropertyImprovement>PropertyImprovements { get; set; }
        public DbSet<FavoriteProperty> FavoriteProperties { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tables
            modelBuilder.Entity<User>().ToTable("Usuarios");
            modelBuilder.Entity<Property>().ToTable("Propiedades");
            modelBuilder.Entity<PropertyType>().ToTable("TiposDePropiedades");
            modelBuilder.Entity<PropertyImage>().ToTable("ImagenesDePropiedades");
            modelBuilder.Entity<SalesType>().ToTable("Tipos de Ventas");
            modelBuilder.Entity<Improvement>().ToTable("Mejoras");
            modelBuilder.Entity<PropertyImprovement>().ToTable("MejorasDePropiedades");
            modelBuilder.Entity<FavoriteProperty>().ToTable("PropiedadesFavoritas");     
            modelBuilder.Entity<Offer>().ToTable("Ofertas");
            modelBuilder.Entity<Chat>().ToTable("Chats");
            modelBuilder.Entity<Message>().ToTable("Mensajes");
            #endregion

            #region Pk
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Property>().HasKey(p => p.Id);
            modelBuilder.Entity<PropertyType>().HasKey(p => p.Id);
            modelBuilder.Entity<PropertyImage>().HasKey(p => p.Id);
            modelBuilder.Entity<SalesType>().HasKey(s => s.Id);
            modelBuilder.Entity<Improvement>().HasKey(i => i.Id);
            modelBuilder.Entity<PropertyImprovement>().HasKey(p => p.Id);
            modelBuilder.Entity<FavoriteProperty>().HasKey(fp => fp.Id);
            modelBuilder.Entity<Offer>().HasKey(o => o.Id);
            modelBuilder.Entity<Chat>().HasKey(c => c.Id);
            modelBuilder.Entity<Message>().HasKey(m => m.Id);




            #endregion

            #region RelationShips

            //Relacion Agent(User) y Property 1-N
            modelBuilder.Entity<User>()
                .HasMany(a=>a.Properties)
                .WithOne(p=>p.Agent)
                .HasForeignKey(p=>p.AgentId)
                .OnDelete(DeleteBehavior.Cascade);

            //Relacion PropertyType y Property 1-N
            modelBuilder.Entity<PropertyType>()
                .HasMany(pt=>pt.Properties)
                .WithOne(p=>p.PropertyType)
                .HasForeignKey(p=>p.PropertyTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            //Relacion SalesType y Property 1-N
            modelBuilder.Entity<SalesType>()
               .HasMany(st => st.Properties)
               .WithOne(p => p.SalesType)
               .HasForeignKey(p => p.SalesTypeId)
               .OnDelete(DeleteBehavior.Cascade);

            //Relacion Property y PropertyImage 1-N
            modelBuilder.Entity<Property>()
               .HasMany(p => p.PropertyImages)
               .WithOne(pi => pi.Property)
               .HasForeignKey(pi => pi.PropertyId)
               .OnDelete(DeleteBehavior.Cascade);

            //Relacion Property e Improvement N-M

            modelBuilder.Entity<Property>()
               .HasMany(p => p.PropertyImprovements)
               .WithOne(pi => pi.Property)
               .HasForeignKey(pi => pi.PropertyId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Improvement>()
                .HasMany(i => i.PropertyImprovements)
                .WithOne(pi => pi.Improvement)
                .HasForeignKey(pi => pi.ImprovementId)
                .OnDelete(DeleteBehavior.Cascade);

            //Relacion Property[Favoritas] y Cliente[User] N-M

            modelBuilder.Entity<Property>()
               .HasMany(p => p.FavoriteProperties)
               .WithOne(fp => fp.Property)
               .HasForeignKey(fp => fp.PropertyId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
              .HasMany(c => c.FavoriteProperties)
              .WithOne(fp => fp.Client)
              .HasForeignKey(fp => fp.ClientId)
              .OnDelete(DeleteBehavior.Restrict);

            //Relacion Property y Offer 1-N

            modelBuilder.Entity<Property>()
               .HasMany(p => p.Offers)
               .WithOne(o=> o.Property)
               .HasForeignKey(o => o.PropertyId)
               .OnDelete(DeleteBehavior.Restrict);

            //Relacion User y Offer 1-N

            modelBuilder.Entity<User>()
              .HasMany(c => c.Offers)
              .WithOne(o => o.Cliente)
              .HasForeignKey(o => o.ClientId)
              .OnDelete(DeleteBehavior.Cascade);


            //Relacion Property y Chat 1-N

            modelBuilder.Entity<Property>()
               .HasMany(p => p.Chats)
               .WithOne(c => c.Property)
               .HasForeignKey(c => c.PropertyId)
               .OnDelete(DeleteBehavior.Cascade);

            //Relacion  Chat y Message 1-N

            modelBuilder.Entity<Chat>()
               .HasMany(c => c.Messages)
               .WithOne(m => m.Chat)
               .HasForeignKey(m => m.ChatId)
               .OnDelete(DeleteBehavior.Cascade);

            //Relacion Agent[User] y Chat  1-N

            modelBuilder.Entity<User>()
               .HasMany(a => a.AgentChats)
               .WithOne(c => c.Agent)
               .HasForeignKey(c => c.AgentId)
               .OnDelete(DeleteBehavior.Restrict);

            //Relacion Client[User] y Chat  1-N

            modelBuilder.Entity<User>()
               .HasMany(c => c.ClientChats)
               .WithOne(chat => chat.Client)
               .HasForeignKey(chat => chat.ClientId)
               .OnDelete(DeleteBehavior.Restrict);

            //Relacion  User y Message 1-N
            modelBuilder.Entity<User>()
               .HasMany(u => u.Messages)
               .WithOne(m => m.Sender)
               .HasForeignKey(m => m.SenderId)
               .OnDelete(DeleteBehavior.Restrict);





            #endregion

            #region Property Configurations

            //Offer
            modelBuilder.Entity<Offer>()
                .Property(o => o.Amount)
                .HasColumnType("decimal(18,2)");

            //Property
            modelBuilder.Entity<Property>()
               .Property(o => o.Price)
               .HasColumnType("decimal(18,2)");
            #endregion
        }
    }
}
