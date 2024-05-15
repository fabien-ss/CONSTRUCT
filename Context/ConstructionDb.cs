using System;
using System.Collections.Generic;
using AspnetCoreMvcFull.Models.Entities;
using AspnetCoreMvcFull.Models.maisonTravaux;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvcFull.Context;

public partial class ConstructionDb : DbContext
{
    public ConstructionDb()
    {
    }

    public ConstructionDb(DbContextOptions<ConstructionDb> options)
        : base(options)
    {
    }

    public virtual DbSet<Lieu> Lieus { get; set; }
    public virtual DbSet<PaiementTemp> PaiementTemps { get; set; }

    public virtual DbSet<DevisTemp> DevisTemps { get; set; }
    public virtual DbSet<VSommePaiement> VSommePaiements { get; set; }
    public virtual DbSet<DetailsDevi> DetailsDevis { get; set; }

    public virtual DbSet<Devi> Devis { get; set; }

    public virtual DbSet<DevisParMaison> DevisParMaisons { get; set; }

    public virtual DbSet<Finition> Finitions { get; set; }

    public virtual DbSet<Paiement> Paiements { get; set; }

    public virtual DbSet<Prestation> Prestations { get; set; }

    public virtual DbSet<Quantite> Quantites { get; set; }

    public virtual DbSet<TypeMaison> TypeMaisons { get; set; }

    public virtual DbSet<TypeTravaux> TypeTravauxes { get; set; }

    public virtual DbSet<Unite> Unites { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    public virtual DbSet<VEtatPaiementDevis> VEtatPaiementDevisEnumerable { get; set; }

    public virtual DbSet<MontantParMois> MontantParMoisEnumerable { get; set; }

    public virtual DbSet<MaisonTravaux> MaisonTravauxes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=construction;Username=your_username;Password=your_password;Port=5432;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      /*
      modelBuilder.Entity<MaisonTravaux>(entity =>
      {
        entity.Property(e => e.)
      });*/
      modelBuilder.Entity<VSommePaiement>(entity =>
      {
        entity.HasNoKey().ToView("v_somme_paiement");
        entity.Property(e => e.IdDevis).HasColumnName("id_devis");
        entity.Property(e => e.Montant).HasColumnName("montant");
      });
        modelBuilder.Entity<DetailsDevi>(entity =>
        {
            entity.HasKey(e => e.IdDetailsDevis).HasName("details_devis_pkey");

            entity.ToTable("details_devis");

            entity.Property(e => e.IdDetailsDevis).HasColumnName("id_details_devis");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.IdPrestation).HasColumnName("id_prestation");
            entity.Property(e => e.idDevis).HasColumnName("id_devis");
            entity.Property(e => e.IdTypeMaison).HasColumnName("id_type_maison");
            entity.Property(e => e.IdUnite).HasColumnName("id_unite");
            entity.Property(e => e.Prestation)
                .HasMaxLength(250)
                .HasColumnName("prestation");
            entity.Property(e => e.PrixUnitaire)
                .HasPrecision(15, 3)
                .HasColumnName("prix_unitaire");
            entity.Property(e => e.Quantite)
                .HasPrecision(15, 3)
                .HasColumnName("quantite");

            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.Unite)
                .HasMaxLength(50)
                .HasColumnName("unite");

            entity.HasOne(d => d.IdPrestationNavigation).WithMany(p => p.DetailsDevis)
                .HasForeignKey(d => d.IdPrestation)
                .HasConstraintName("details_devis_id_prestation_fkey");

            entity.HasOne(d => d.IdTypeMaisonNavigation).WithMany(p => p.DetailsDevis)
                .HasForeignKey(d => d.IdTypeMaison)
                .HasConstraintName("details_devis_id_type_maison_fkey");

            entity.HasOne(d => d.IdUniteNavigation).WithMany(p => p.DetailsDevis)
                .HasForeignKey(d => d.IdUnite)
                .HasConstraintName("details_devis_id_unite_fkey");
          //  entity.Property(d => d.idTypeTravaux).HasColumnName("id_type_travaux");
            entity.Property(d => d.Duree).HasColumnName("duree");
        });

        modelBuilder.Entity<Devi>(entity =>
        {
          entity.Property(e => e.DateFin).HasColumnName("date_fin");
            entity.HasKey(e => e.IdDevis).HasName("devis_pkey");

            entity.ToTable("devis");
            entity.Property(e => e.Lieu).HasColumnName("lieu");
            entity.Property(e => e.IdDevis).HasColumnName("id_devis");
            entity.Property(e => e.DateConstruction).HasColumnName("date_construction");
            entity.Property(e => e.DateDevis)
                .HasDefaultValueSql("now()")
                .HasColumnName("date_devis");
            entity.Property(e => e.EstFini)
                .HasDefaultValue(false)
                .HasColumnName("est_fini");
            entity.Property(e => e.EstPaye)
                .HasDefaultValue(false)
                .HasColumnName("est_paye");
            entity.Property(e => e.RefDevis).HasColumnName("ref_devis");
            entity.Property(e => e.IdFinition).HasColumnName("id_finition");
            entity.Property(e => e.IdTypeMaison).HasColumnName("id_type_maison");
            entity.Property(e => e.IdUtilisateur).HasColumnName("id_utilisateur");
            entity.Property(e => e.PrixTotal)
                .HasPrecision(15, 2)
                .HasColumnName("prix_total");

            entity.HasOne(d => d.IdFinitionNavigation).WithMany(p => p.Devis)
                .HasForeignKey(d => d.IdFinition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("devis_id_finition_fkey");

            entity.HasOne(d => d.IdTypeMaisonNavigation).WithMany(p => p.Devis)
                .HasForeignKey(d => d.IdTypeMaison)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("devis_id_type_maison_fkey");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithMany(p => p.Devis)
                .HasForeignKey(d => d.IdUtilisateur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("devis_id_utilisateur_fkey");

            entity.HasMany(d => d.IdPaiements).WithMany(p => p.IdDevisNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "Asso13",
                    r => r.HasOne<Paiement>().WithMany()
                        .HasForeignKey("IdPaiement")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("asso_13_id_paiement_fkey"),
                    l => l.HasOne<Devi>().WithMany()
                        .HasForeignKey("IdDevis")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("asso_13_id_devis_fkey"),
                    j =>
                    {
                        j.HasKey("IdDevis", "IdPaiement").HasName("asso_13_pkey");
                        j.ToTable("asso_13");
                        j.IndexerProperty<int>("IdDevis").HasColumnName("id_devis");
                        j.IndexerProperty<int>("IdPaiement").HasColumnName("id_paiement");
                    });
        });

        modelBuilder.Entity<DevisParMaison>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("devis_par_maison");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.IdPrestation).HasColumnName("id_prestation");
            entity.Property(e => e.IdTypeMaison).HasColumnName("id_type_maison");
            entity.Property(e => e.IdUnite).HasColumnName("id_unite");
            entity.Property(e => e.Prestation)
                .HasMaxLength(250)
                .HasColumnName("prestation");
            entity.Property(e => e.PrixUnitaire)
                .HasPrecision(15, 3)
                .HasColumnName("prix_unitaire");
            entity.Property(e => e.Quantite)
                .HasPrecision(15, 3)
                .HasColumnName("quantite");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.Unite)
                .HasMaxLength(250)
                .HasColumnName("unite");
            entity.Property(e => e.idTypeTravaux).HasColumnName("id_type_travaux");
            entity.Property(e => e.Duree).HasColumnName("duree");
        });

        modelBuilder.Entity<Finition>(entity =>
        {
            entity.HasKey(e => e.IdFinition).HasName("finition_pkey");

            entity.ToTable("finition");

            entity.HasIndex(e => e.Finition1, "finition_finition_key").IsUnique();

            entity.Property(e => e.IdFinition).HasColumnName("id_finition");
            entity.Property(e => e.Finition1)
                .HasMaxLength(250)
                .HasColumnName("finition");
            entity.Property(e => e.Majoration)
                .HasPrecision(15, 2)
                .HasColumnName("majoration");
            entity.Property(e => e.Photo).HasColumnName("photo");
        });

        modelBuilder.Entity<Paiement>(entity =>
        {
            entity.HasKey(e => e.IdPaiement).HasName("paiement_pkey");

            entity.ToTable("paiement");

            entity.Property(e => e.IdPaiement).HasColumnName("id_paiement");
            entity.Property(e => e.DatePaiement)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_paiement");
            entity.Property(e => e.IdDevis).HasColumnName("id_devis");

            entity.HasOne(d => d.IdDevis1).WithMany(p => p.Paiements)
                .HasForeignKey(d => d.IdDevis)
                .HasConstraintName("paiement_id_devis_fkey");
            entity.Property(e => e.Montant).HasColumnName("montant");
            entity.Property(e => e.RefPaiement).HasColumnName("ref_paiement");
        });

        modelBuilder.Entity<Prestation>(entity =>
        {
            entity.HasKey(e => e.IdPrestation).HasName("prestation_pkey");

            entity.ToTable("prestation");

            entity.HasIndex(e => e.Code, "prestation_code_key").IsUnique();

            entity.HasIndex(e => e.Prestation1, "prestation_prestation_key").IsUnique();

            entity.Property(e => e.IdPrestation).HasColumnName("id_prestation");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Duree).HasColumnName("duree");
            entity.Property(e => e.IdPrestation1).HasColumnName("id_prestation_1");
            entity.Property(e => e.IdTypeTravaux).HasColumnName("id_type_travaux");
            entity.Property(e => e.IdUnite).HasColumnName("id_unite");
            entity.Property(e => e.Prestation1)
                .HasMaxLength(250)
                .HasColumnName("prestation");
            entity.Property(e => e.PrixUnitaire)
                .HasPrecision(15, 3)
                .HasColumnName("prix_unitaire");

            entity.HasOne(d => d.IdPrestation1Navigation).WithMany(p => p.InverseIdPrestation1Navigation)
                .HasForeignKey(d => d.IdPrestation1)
                .HasConstraintName("prestation_id_prestation_1_fkey");

            entity.HasOne(d => d.IdTypeTravauxNavigation).WithMany(p => p.Prestations)
                .HasForeignKey(d => d.IdTypeTravaux)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prestation_id_type_travaux_fkey");

            entity.HasOne(d => d.IdUniteNavigation).WithMany(p => p.Prestations)
                .HasForeignKey(d => d.IdUnite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prestation_id_unite_fkey");
        });

        modelBuilder.Entity<Quantite>(entity =>
        {
            entity.HasKey(e => new { e.IdTypeMaison, e.IdPrestation }).HasName("quantite_pkey");

            entity.ToTable("quantite");

            entity.Property(e => e.IdTypeMaison).HasColumnName("id_type_maison");
            entity.Property(e => e.IdPrestation).HasColumnName("id_prestation");
            entity.Property(e => e.Quantite1)
                .HasPrecision(15, 3)
                .HasDefaultValueSql("1")
                .HasColumnName("quantite");
            entity.Property(e => e.Duree).HasColumnName("duree");
            entity.Property(e => e.Surface).HasColumnName("surface");
            entity.HasOne(d => d.IdPrestationNavigation).WithMany(p => p.Quantites)
                .HasForeignKey(d => d.IdPrestation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("quantite_id_prestation_fkey");

            entity.HasOne(d => d.IdTypeMaisonNavigation).WithMany(p => p.Quantites)
                .HasForeignKey(d => d.IdTypeMaison)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("quantite_id_type_maison_fkey");
        });

        modelBuilder.Entity<TypeMaison>(entity =>
        {
            entity.HasKey(e => e.IdTypeMaison).HasName("type_maison_pkey");

            entity.ToTable("type_maison");

            entity.HasIndex(e => e.TypeMaison1, "type_maison_type_maison_key").IsUnique();

            entity.Property(e => e.IdTypeMaison).HasColumnName("id_type_maison");
            entity.Property(e => e.Desciption).HasColumnName("desciption");
            entity.Property(e => e.Duree).HasColumnName("duree");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.PrixTotal)
                .HasPrecision(15, 2)
                .HasColumnName("prix_total");
            entity.Property(e => e.TypeMaison1)
                .HasMaxLength(250)
                .HasColumnName("type_maison");
            entity.Property(e => e.Surface).HasColumnName("surface");
        });

        modelBuilder.Entity<TypeTravaux>(entity =>
        {
            entity.HasKey(e => e.IdTypeTravaux).HasName("type_travaux_pkey");

            entity.ToTable("type_travaux");

            entity.HasIndex(e => e.Code, "type_travaux_code_key").IsUnique();

            entity.HasIndex(e => e.TypeTravaux1, "type_travaux_type_travaux_key").IsUnique();

            entity.Property(e => e.IdTypeTravaux).HasColumnName("id_type_travaux");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.TypeTravaux1)
                .HasMaxLength(250)
                .HasColumnName("type_travaux");
        });

        modelBuilder.Entity<Unite>(entity =>
        {
            entity.HasKey(e => e.IdUnite).HasName("unite_pkey");

            entity.ToTable("unite");

            entity.HasIndex(e => e.Initial, "unite_initial_key").IsUnique();

            entity.HasIndex(e => e.Unite1, "unite_unite_key").IsUnique();

            entity.Property(e => e.IdUnite).HasColumnName("id_unite");
            entity.Property(e => e.Initial)
                .HasMaxLength(50)
                .HasColumnName("initial");
            entity.Property(e => e.Unite1)
                .HasMaxLength(250)
                .HasColumnName("unite");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.IdUtilisateur).HasName("utilisateur_pkey");

            entity.ToTable("utilisateur");

            entity.HasIndex(e => e.Email, "utilisateur_email_key").IsUnique();

            entity.HasIndex(e => e.Numero, "utilisateur_numero_key").IsUnique();

            entity.Property(e => e.IdUtilisateur).HasColumnName("id_utilisateur");
            entity.Property(e => e.Email)
                .HasMaxLength(500)
                .HasColumnName("email");
            entity.Property(e => e.MotDePasse).HasColumnName("mot_de_passe");
            entity.Property(e => e.Nom)
                .HasMaxLength(500)
                .HasColumnName("nom");
            entity.Property(e => e.Numero)
                .HasMaxLength(500)
                .HasColumnName("numero");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.Prenom)
                .HasMaxLength(250)
                .HasColumnName("prenom");
            entity.Property(e => e.Privilege)
                .HasDefaultValue(0)
                .HasColumnName("privilege");
        });

        modelBuilder.Entity<MontantParMois>(entity =>
        {
          entity.ToTable("v_somme_12_mois");
          entity.HasNoKey();
          entity.Property(e => e.Mois).HasColumnName("mois");
          entity.Property(e => e.Numero).HasColumnName("numero");
          entity.Property(e => e.Montant).HasColumnName("montant");
          entity.Property(e => e.Annees).HasColumnName("annees");
        });
        modelBuilder.HasSequence("seq_unite");
        modelBuilder.HasSequence("seq_utilisateur");


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
