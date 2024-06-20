create or replace view  devis_par_maison
    (id_type_maison, quantite, prestation, id_prestation, total, code, prix_unitaire, unite, id_unite,
     id_type_travaux, duree)
as
SELECT tm.id_type_maison,
       q.quantite,
       p.prestation,
       p.id_prestation,
       p.prix_unitaire * q.quantite AS total,
       p.code,
       p.prix_unitaire,
       u.unite,
       u.id_unite,
       p.id_type_travaux,
       q.duree;
FROM type_maison tm
       JOIN quantite q ON tm.id_type_maison = q.id_type_maison
       LEFT JOIN prestation p ON q.id_prestation = p.id_prestation
       JOIN unite u ON u.id_unite = p.id_unite
ORDER BY p.code, tm.type_maison;

alter table devis_par_maison
  owner to your_username;

create view v_etat_paiement(id_devis, montant) as
SELECT id_devis,
       sum(montant) AS montant
FROM paiement
GROUP BY id_devis;

alter table v_etat_paiement
  owner to your_username;

create view v_etat_devis_paiement(id_devis, paye, prix_total) as
SELECT d.id_devis,
       COALESCE(vdp.montant, 0::double precision) AS paye,
       d.prix_total
FROM devis d
       LEFT JOIN v_etat_paiement vdp ON d.id_devis = vdp.id_devis;

alter table v_etat_devis_paiement
  owner to your_username;

create view v_montant_par_moi_par_annee(montant, year, mounth) as
SELECT sum(prix_total)                AS montant,
       EXTRACT(year FROM date_devis)  AS year,
       EXTRACT(month FROM date_devis) AS mounth
FROM devis d
GROUP BY date_devis;

alter table v_montant_par_moi_par_annee
  owner to your_username;

create view v_annee_devi(min, max) as
SELECT min(EXTRACT(year FROM date_devis)) AS min,
       max(EXTRACT(year FROM date_devis)) AS max
FROM devis;

alter table v_annee_devi
  owner to your_username;

create view v_mois_annee(numero, mois, annees) as
SELECT m.numero,
       m.mois,
       annee.annees
FROM mois m
       CROSS JOIN (SELECT generate_series(min(EXTRACT(year FROM devis.date_devis)),
                                          max(EXTRACT(year FROM devis.date_devis)), 1::numeric) AS annees
                   FROM devis) annee;

alter table v_mois_annee
  owner to your_username;

create view v_montant_par_12_moi_par_annee(mois, numero, montant, annees) as
SELECT mois.mois,
       mois.numero,
       COALESCE(v_montant_par_moi_par_annee.montant, 0::numeric) AS montant,
       mois.annees::character varying(20)                        AS annees
FROM v_mois_annee mois
       LEFT JOIN v_montant_par_moi_par_annee ON mois.numero::numeric = v_montant_par_moi_par_annee.mounth AND
                                                mois.annees = v_montant_par_moi_par_annee.year;

alter table v_montant_par_12_moi_par_annee
  owner to your_username;

