-- mise à jour de la colonne devis
alter table devis
  add ref_devis varchar(100) default nextval('ref_devis')::varchar;

