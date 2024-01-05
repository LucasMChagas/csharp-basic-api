using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Atore> Atores { get; set; }

    public virtual DbSet<ElencoFilme> ElencoFilmes { get; set; }

    public virtual DbSet<Filme> Filmes { get; set; }

    public virtual DbSet<FilmesGenero> FilmesGeneros { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer(@"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Filmes;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Atore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_actor");

            entity.Property(e => e.Genero)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.PrimeiroNome)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UltimoNome)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ElencoFilme>(entity =>
        {
            entity.ToTable("ElencoFilme");

            entity.Property(e => e.Papel)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAtorNavigation).WithMany(p => p.ElencoFilmes)
                .HasForeignKey(d => d.IdAtor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ElencoFil__IdAto__2C3393D0");

            entity.HasOne(d => d.IdFilmeNavigation).WithMany(p => p.ElencoFilmes)
                .HasForeignKey(d => d.IdFilme)
                .HasConstraintName("FK__ElencoFil__IdFil__2D27B809");
        });

        modelBuilder.Entity<Filme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_movie");

            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FilmesGenero>(entity =>
        {
            entity.ToTable("FilmesGenero");

            entity.HasOne(d => d.IdFilmeNavigation).WithMany(p => p.FilmesGeneros)
                .HasForeignKey(d => d.IdFilme)
                .HasConstraintName("FK__FilmesGen__IdFil__2F10007B");

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.FilmesGeneros)
                .HasForeignKey(d => d.IdGenero)
                .HasConstraintName("FK__FilmesGen__IdGen__2E1BDC42");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_genres");

            entity.Property(e => e.Genero1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Genero");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
