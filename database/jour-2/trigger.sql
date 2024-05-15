CREATE OR REPLACE TRIGGER updatePourcentageDevis
  AFTER INSERT
  ON paiement
EXECUTE function updatePaiement();

drop function updatePaiement();
create or replace function updatePaiement()
  returns trigger
  language plpgsql
as
$$
begin
  update devis
  set paiement_effetue = (select pourcentage from v_pourcentage_paiement_final where id_devis = NEW.id_devis)
  where id_devis = NEW.id_devis;
  return NEW;
end;
$$
;

insert into paiement (date_paiement, id_devis, montant, ref_paiement) values (now(), 54, 4000, 'PP');

create or replace trigger mouvement_insertion_devis
  after insert
  on devis
execute function mouvement_devis();

create or replace function mouvement_devis()
  returns trigger
  language plpgsql
as
$$
begin
  insert into details_devis( id_prestation, id_type_maison, quantite, prestation, total, code, prix_unitaire, unite
                           , id_unite, id_devis, duree)
  select dpm.id_prestation
       , dpm.id_type_maison
       , dpm.quantite
       , dpm.prestation
       , dpm.total
       , dpm.code
       , dpm.prix_unitaire
       , dpm.unite
       , dpm.id_unite
       , NEW.id_devis
       , dpm.duree
  from devis_par_maison dpm
  where dpm.id_type_maison = NEW.id_type_maison
  on conflict do nothing;
  update devis
  set prix_total = (montantMaison(new.id_type_maison) * majorationFinition(new.id_finition) / 100) +
                   montantMaison(new.id_type_maison)
  where id_devis = NEW.id_devis;
  return NEW;
end;
$$;

create or replace function montantMaison(id_type_maison integer) returns double precision
  language plpgsql as
$$
declare
  montant double precision;
begin
  select sum(devis_par_maison.prix_unitaire * devis_par_maison.quantite)::double precision
  into montant
  from devis_par_maison
  where devis_par_maison.id_type_maison = $1;
  return montant;
end;
$$;

select montantMaison(2);

create or replace function majorationFinition(id_finition integer) returns double precision
  language plpgsql as
$$
declare
  majoration double precision;
begin
  select finition.majoration into majoration from finition where finition.id_finition = $1;
  return majoration;
end;
$$;

select majorationFinition(5);


