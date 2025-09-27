﻿using Microsoft.EntityFrameworkCore;
using Proyecto_Aerolinea.Web.Models.Entities;

namespace Proyecto_Aerolinea.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<SeatAssignment> SeatAssigments { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            // Reservation <-> User
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
