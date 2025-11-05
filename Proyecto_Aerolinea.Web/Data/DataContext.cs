using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proyecto_Aerolinea.Web.Data.Entities;
using static System.Collections.Specialized.BitVector32;

namespace Proyecto_Aerolinea.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<SeatAssignment> SeatAssigments { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureIndexes(modelBuilder);

            ConfigureKeys(modelBuilder);

            base.OnModelCreating(modelBuilder);

            // Airport <-> Flight
            // Evitar cascadas múltiples usando Restrict
            modelBuilder.Entity<Flight>()
                .HasOne(f => f.OriginAirport)
                .WithMany(a => a.OriginFlights)
                .HasForeignKey(f => f.OriginAirportId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Flight>()
                .HasOne(f => f.DestinationAirport)
                .WithMany(a => a.DestinationFlights)
                .HasForeignKey(f => f.DestinationAirportId)
                .OnDelete(DeleteBehavior.Restrict);
            // SeatAssignment <-> Ticket (uno a uno)
            modelBuilder.Entity<SeatAssignment>()
                .HasOne(sa => sa.Ticket)
                .WithOne(t => t.SeatAssignment)
                .HasForeignKey<SeatAssignment>(sa => sa.TicketId)
                .OnDelete(DeleteBehavior.Cascade); // un ticket tiene solo un seat assignment
            // SeatAssignment <-> Flight
            // Evitar cascada múltiple
            modelBuilder.Entity<SeatAssignment>()
                .HasOne(sa => sa.Flight)
                .WithMany(f => f.SeatAssignments)
                .HasForeignKey(sa => sa.FlightId)
                .OnDelete(DeleteBehavior.Restrict);
            // SeatAssignment <-> Seat (opcional)
            modelBuilder.Entity<SeatAssignment>()
                .HasOne(sa => sa.Seat)
                .WithMany(s => s.SeatAssignments)
                .HasForeignKey(sa => sa.SeatId)
                .OnDelete(DeleteBehavior.SetNull);
            // Flight <-> Aircraft
            // Evitar cascadas
            modelBuilder.Entity<Flight>()
                .HasOne(f => f.Aircraft)
                .WithMany(a => a.Flights)
                .HasForeignKey(f => f.AircraftId)
                .OnDelete(DeleteBehavior.Restrict);
            // Ticket <-> Flight
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Flight)
                .WithMany(f => f.Tickets)
                .HasForeignKey(t => t.FlightId)
                .OnDelete(DeleteBehavior.Restrict);
            // Ticket <-> Passenger
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Passenger)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.PassengerId)
                .OnDelete(DeleteBehavior.Restrict);
            // Ticket <-> Reservation
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Reservation)
                .WithMany(r => r.Tickets)
                .HasForeignKey(t => t.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);
            // Reservation <-> Payment
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Reservation)
                .WithMany(r => r.Payments)
                .HasForeignKey(p => p.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

        }

        private void ConfigureKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolePermission>().HasKey(rp => new { rp.PermissionId, rp.RoleId });

            modelBuilder.Entity<RolePermission>().HasOne(rp => rp.Role)
                                            .WithMany(r => r.RolePermissions)
                                            .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>().HasOne(rp => rp.Permission)
                                            .WithMany(p => p.RolePermissions)
                                            .HasForeignKey(rp => rp.PermissionId);
        }

        private void ConfigureIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectRole>().HasIndex(r => r.Name)
                                             .IsUnique();

            modelBuilder.Entity<User>().HasIndex(u => u.Document)
                                  .IsUnique();

            modelBuilder.Entity<Permission>().HasIndex(p => p.Name)
                                        .IsUnique();
        }
    }    
}
