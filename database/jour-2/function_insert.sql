create or replace function mutlipleInsert() -- prestation
  returns varchar[]
  language plpgsql
as
$$
declare
  maison record;
  error  varchar[];
begin
  insert into type_maison(type_maison, desciption, surface, duree)
  select btrim(type_maison), btrim(description), (surface), (duree_travaux)
  from maison_travaux
  on conflict do nothing;

  insert into unite(unite, initial)
  select btrim(unite), btrim(unite)
  from maison_travaux
  on conflict do nothing;


  return error;
end;
$$;

select mutlipleInsert();

create or replace function insert_type_prestations()
  returns void
  language plpgsql
as
$$
begin
  insert into prestation (code, prestation, prix_unitaire, duree, id_unite, id_type_travaux)
  select code_travaux, type_travaux, prix_unitaire, duree_travaux, u.id_unite, 1
  from maison_travaux mt
         join unite u
              on mt.unite = u.initial
  on conflict do nothing;
end;
$$;

select insert_type_prestations();

create or replace function insert_type_quantite()
  returns void
  language plpgsql
as
$$
begin
  insert into quantite (id_type_maison, id_prestation, quantite, duree, surface)
  select tm.id_type_maison, p.id_prestation, mt.quantite, 1, 1
  from maison_travaux mt
         join type_maison tm
              on tm.type_maison = mt.type_maison
         join prestation p on p.prestation = mt.type_travaux
  on conflict do nothing;
end;
$$;

select insert_type_quantite();

-- function to call

select mutlipleInsert();
select insert_type_prestations();
select insert_type_quantite();

-- function insert devis

create or replace function insert_client()
  returns void
  language plpgsql
as
$$
begin
  insert into utilisateur(numero) select client from devis_temp on conflict do nothing ;
end;
$$;

create or replace function insert_finition()
  returns void
  language plpgsql
as
$$
begin
  insert into finition(finition, majoration)
  select devis_temp.finition, devis_temp.taux_finition::numeric
  from devis_temp
  on conflict do nothing;
end;
$$
;

create or replace function insert_devis()
  returns void
  language plpgsql
as
$$
begin
  insert into devis(id_utilisateur, ref_devis, date_devis, date_construction, id_finition, id_type_maison, lieu)
  select u.id_utilisateur,
         dt.ref_devis,
         to_date(dt.date_devis, 'DD/MM/YYYY'),
         to_date(dt.date_debut, 'DD/MM/YYYY'),
         f.id_finition,
         tm.id_type_maison,
         dt.lieu
  from devis_temp dt
         join utilisateur u on u.numero = dt.client
         join finition f on f.finition = dt.finition
         join type_maison tm on tm.type_maison = dt.type_maison
  on conflict do nothing;
  insert into lieu (lieu) select lieu from devis_temp on conflict do nothing ;
end;
$$;


select insert_client();

select insert_finition();

select insert_devis();

-- paiement


create or replace function insert_paiement()
  returns void
  language plpgsql
as
$$
begin
  insert into paiement(id_devis, date_paiement, montant, ref_paiement)
  select d.id_devis, to_date(pt.date_paiement, 'DD/MM/YYYY'), pt.montant::double precision, pt.ref_paiement
  from paiement_temp pt
         join devis d on d.ref_devis = pt.ref_devis
  on conflict do nothing
  ;
end;
$$;

select insert_paiement();
