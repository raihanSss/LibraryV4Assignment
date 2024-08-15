using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using LibraryV4.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace LibraryV4.Infrastructure
{
    public class libraryContext : DbContext
    {

        public libraryContext(DbContextOptions<libraryContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Peminjaman> Peminjamans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Peminjaman>()
                .HasOne(p => p.Book)
                .WithMany()
                .HasForeignKey(p => p.id_buku);


            modelBuilder.Entity<Peminjaman>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.id_user);
        }

    }
}
