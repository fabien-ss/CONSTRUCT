drop view devis_par_maison;

CREATE OR REPLACE VIEW devis_par_maison AS
  SELECT
      tm.id_type_maison, q.quantite, p.prestation, p.id_prestation, p.prix_unitaire * q.quantite total, p.code, p.prix_unitaire,
      u.unite, u.id_unite, p.id_type_travaux, q.duree
  FROM type_maison tm
  JOIN quantite q
  on tm.id_type_maison = q.id_type_maison
  left join prestation p
  on q.id_prestation = p.id_prestation
  join unite u
  on u.id_unite = p.id_unite
order by p.code,tm.type_maison
;

-- view prix
create or replace view v_etat_paiement as
  select id_devis, sum(montant) montant from paiement
  group by id_devis;

drop view v_etat_devis_paiement;
create or replace view v_etat_devis_paiement as
  select d.id_devis, coalesce(vdp.montant, 0) paye, d.prix_total  from devis d left join v_etat_paiement vdp
  on d.id_devis = vdp.id_devis;

create or replace view v_montant_par_moi_par_annee as
select
    sum(d.prix_total) montant, extract(year from d.date_devis) as year, extract(month from d.date_devis) mounth
from devis d
group by d.date_devis;


create or replace view v_annee_devi as
select min(extract(year from devis.date_devis)) min, max(extract(year from devis.date_devis)) max
from devis;

SELECT generate_series(min(extract(year from date_devis)) , max(extract(year from date_devis)), 1) as annees from devis;

create or replace view v_mois_annee as
  select
      m.numero, m.mois, annee.annees from mois m cross join
          (SELECT generate_series(min(extract(year from date_devis)) , max(extract(year from date_devis)), 1) as annees from devis) as annee;

drop view v_montant_par_12_moi_par_annee;
create or replace view v_montant_par_12_moi_par_annee as
select
    mois.mois,
    mois.numero,
    coalesce( v_montant_par_moi_par_annee.montant, 0) montant ,
    (mois.annees)::varchar(20)
from v_mois_annee mois left join v_montant_par_moi_par_annee on mois.numero = v_montant_par_moi_par_annee.mounth and mois.annees = v_montant_par_moi_par_annee.year;
