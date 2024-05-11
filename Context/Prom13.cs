using System;
using System.Collections.Generic;
using AspnetCoreMvcFull.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvcFull.Context;

public partial class Prom13 : DbContext
{
    public Prom13()
    {
    }

    public Prom13(DbContextOptions<Prom13> options)
        : base(options)
    {
    }

    public virtual DbSet<Chauffeur> Chauffeur { get; set; }

    public virtual DbSet<Echeance> Echeance { get; set; }

    public virtual DbSet<Prestation> Prestation { get; set; }

    public virtual DbSet<Trajet> Trajet { get; set; }

    public virtual DbSet<VTrajetChauffeurNotOver> VTrajetChauffeurNotOver { get; set; }

    public virtual DbSet<Vehicule> Vehicule { get; set; }

    public virtual DbSet<VehiculeEcheance> VehiculeEcheance { get; set; }

    public virtual DbSet<VehiculePrestation> VehiculePrestation { get; set; }

    public virtual DbSet<VehiculeType> VehiculeType { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=vehicule;Username=your_username;Password=your_password;Port=5432;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chauffeur>(entity =>
        {
            entity.HasKey(e => e.IdChauffeur).HasName("chauffeur_pkey");

            entity.ToTable("chauffeur");

            entity.HasIndex(e => e.Email, "chauffeur_email_key").IsUnique();

            entity.Property(e => e.IdChauffeur)
                .HasMaxLength(20)
                .HasDefaultValueSql("get_prefix('CHF'::text, 'seq_chauffeur'::text, 10)")
                .HasColumnName("id_chauffeur");
            entity.Property(e => e.DateNaissance).HasColumnName("date_naissance");
            entity.Property(e => e.Email)
                .HasMaxLength(500)
                .HasColumnName("email");
            entity.Property(e => e.MotDePasse)
                .HasMaxLength(500)
                .HasColumnName("mot_de_passe");
            entity.Property(e => e.NomPrenom)
                .HasMaxLength(500)
                .HasColumnName("nom_prenom");
            entity.Property(e => e.Numero)
                .HasMaxLength(30)
                .HasColumnName("numero");
            entity.Property(e => e.Sexe)
                .HasMaxLength(2)
                .HasColumnName("sexe");
        });

        modelBuilder.Entity<Echeance>(entity =>
        {
            entity.HasKey(e => e.IdEcheance).HasName("echeance_pkey");

            entity.ToTable("echeance");

            entity.HasIndex(e => e.Echeance1, "echeance_echeance_key").IsUnique();

            entity.Property(e => e.IdEcheance)
                .HasMaxLength(10)
                .HasDefaultValueSql("get_prefix('ECHC'::text, 'seq_echeance'::text, 10)")
                .HasColumnName("id_echeance");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.Echeance1)
                .HasMaxLength(250)
                .HasColumnName("echeance");
        });

        modelBuilder.Entity<Prestation>(entity =>
        {
            entity.HasKey(e => e.IdPrestation).HasName("prestation_pkey");

            entity.ToTable("prestation");

            entity.HasIndex(e => e.Maintenance, "prestation_maintenance_key").IsUnique();

            entity.Property(e => e.IdPrestation)
                .HasMaxLength(10)
                .HasDefaultValueSql("get_prefix('PRST'::text, 'seq_prestation'::text, 10)")
                .HasColumnName("id_prestation");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.Maintenance)
                .HasMaxLength(250)
                .HasColumnName("maintenance");
        });

        modelBuilder.Entity<Trajet>(entity =>
        {
            entity.HasKey(e => e.IdTrajet).HasName("trajet_pkey");

            entity.ToTable("trajet");

            entity.Property(e => e.IdTrajet)
                .HasMaxLength(10)
                .HasColumnName("id_trajet");
            entity.Property(e => e.Arrive)
                .HasMaxLength(100)
                .HasColumnName("arrive");
            entity.Property(e => e.DateTrajet)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_trajet");
            entity.Property(e => e.Depart)
                .HasMaxLength(100)
                .HasColumnName("depart");
            entity.Property(e => e.Details)
                .HasMaxLength(500)
                .HasColumnName("details");
            entity.Property(e => e.IdChauffeur)
                .HasMaxLength(20)
                .HasColumnName("id_chauffeur");
            entity.Property(e => e.Kilometrage).HasColumnName("kilometrage");
            entity.Property(e => e.MontantCarburant).HasColumnName("montant_carburant");
            entity.Property(e => e.Plaque)
                .HasMaxLength(10)
                .HasColumnName("plaque");

            entity.HasOne(d => d.IdChauffeurNavigation).WithMany(p => p.Trajet)
                .HasForeignKey(d => d.IdChauffeur)
                .HasConstraintName("trajet_id_chauffeur_fkey");

            entity.HasOne(d => d.PlaqueNavigation).WithMany(p => p.Trajet)
                .HasForeignKey(d => d.Plaque)
                .HasConstraintName("trajet_plaque_fkey");
        });

        modelBuilder.Entity<VTrajetChauffeurNotOver>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_trajet_chauffeur_not_over");

            entity.Property(e => e.Arrive)
                .HasMaxLength(100)
                .HasColumnName("arrive");
            entity.Property(e => e.DateTrajet)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_trajet");
            entity.Property(e => e.Depart)
                .HasMaxLength(100)
                .HasColumnName("depart");
            entity.Property(e => e.Details)
                .HasMaxLength(500)
                .HasColumnName("details");
            entity.Property(e => e.IdChauffeur)
                .HasMaxLength(20)
                .HasColumnName("id_chauffeur");
            entity.Property(e => e.IdTrajet)
                .HasMaxLength(10)
                .HasColumnName("id_trajet");
            entity.Property(e => e.Kilometrage).HasColumnName("kilometrage");
            entity.Property(e => e.MontantCarburant).HasColumnName("montant_carburant");
            entity.Property(e => e.NomPrenom)
                .HasMaxLength(500)
                .HasColumnName("nom_prenom");
            entity.Property(e => e.Plaque)
                .HasMaxLength(10)
                .HasColumnName("plaque");
        });

        modelBuilder.Entity<Vehicule>(entity =>
        {
            entity.HasKey(e => e.Plaque).HasName("vehicule_pkey");

            entity.ToTable("vehicule");

            entity.Property(e => e.Plaque)
                .HasMaxLength(20)
                .HasColumnName("plaque");
            entity.Property(e => e.DateMiseEnRoute).HasColumnName("date_mise_en_route");
            entity.Property(e => e.IdVehiculeType)
                .HasMaxLength(10)
                .HasColumnName("id_vehicule_type");

            entity.HasOne(d => d.IdVehiculeTypeNavigation).WithMany(p => p.Vehicule)
                .HasForeignKey(d => d.IdVehiculeType)
                .HasConstraintName("vehicule_id_vehicule_type_fkey");
        });

        modelBuilder.Entity<VehiculeEcheance>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("vehicule_echeance");

            entity.Property(e => e.DateDebut).HasColumnName("date_debut");
            entity.Property(e => e.DateFin).HasColumnName("date_fin");
            entity.Property(e => e.IdEcheance)
                .HasMaxLength(10)
                .HasColumnName("id_echeance");
            entity.Property(e => e.MontantRecu).HasColumnName("montant_recu");
            entity.Property(e => e.Plaque)
                .HasMaxLength(10)
                .HasColumnName("plaque");

            entity.HasOne(d => d.IdEcheanceNavigation).WithMany()
                .HasForeignKey(d => d.IdEcheance)
                .HasConstraintName("vehicule_echeance_id_echeance_fkey");

            entity.HasOne(d => d.PlaqueNavigation).WithMany()
                .HasForeignKey(d => d.Plaque)
                .HasConstraintName("vehicule_echeance_plaque_fkey");
        });

        modelBuilder.Entity<VehiculePrestation>(entity =>
        {
            entity.HasKey(e => e.IdPrestation).HasName("vehicule_prestation_pkey");

            entity.ToTable("vehicule_prestation");

            entity.Property(e => e.IdPrestation)
                .HasMaxLength(10)
                .HasColumnName("id_prestation");
            entity.Property(e => e.DateHeure)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_heure");
            entity.Property(e => e.Details)
                .HasMaxLength(500)
                .HasColumnName("details");
            entity.Property(e => e.Montant).HasColumnName("montant");
            entity.Property(e => e.Plaque)
                .HasMaxLength(10)
                .HasColumnName("plaque");

            entity.HasOne(d => d.PlaqueNavigation).WithMany(p => p.VehiculePrestation)
                .HasForeignKey(d => d.Plaque)
                .HasConstraintName("vehicule_prestation_plaque_fkey");
        });

        modelBuilder.Entity<VehiculeType>(entity =>
        {
            entity.HasKey(e => e.IdVehiculeType).HasName("vehicule_type_pkey");

            entity.ToTable("vehicule_type");

            entity.HasIndex(e => e.VehiculeType1, "vehicule_type_vehicule_type_key").IsUnique();

            entity.Property(e => e.IdVehiculeType)
                .HasMaxLength(10)
                .HasColumnName("id_vehicule_type");
            entity.Property(e => e.VehiculeType1)
                .HasMaxLength(250)
                .HasColumnName("vehicule_type");
        });
        modelBuilder.HasSequence("seq_chauffeur");
        modelBuilder.HasSequence("seq_echeance");
        modelBuilder.HasSequence("seq_prestation");
        modelBuilder.HasSequence("seq_trajet");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
