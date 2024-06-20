create or replace view v_somme_paiement as
  select id_devis, sum(p.montant) montant from paiement p
group by id_devis;

create or replace view v_pourcentage_paiement
                  as
  select id_devis, sum(prix_total) total from devis group by id_devis;

drop view v_pourcentage_paiement_final;
create or replace view v_pourcentage_paiement_final
as select vpp.id_devis, coalesce(vsp.montant, 0) as montant_paye, vpp.total,
          (vsp.montant * 100 /  vpp.total) pourcentage
    from v_pourcentage_paiement vpp left join v_somme_paiement vsp
              on vpp.id_devis = vsp.id_devis;

create or replace view v_somme_12_mois as
select vpa.mois, vpa.numero, sum(vpa.montant) montant, vpa.annees from v_montant_par_12_moi_par_annee vpa
group by vpa.mois, vpa.numero, vpa.annees
