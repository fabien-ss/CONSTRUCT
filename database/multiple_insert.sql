select alimenter_vente_billet();

create or replace function generer_billet_2()
  returns void
  language plpgsql
as
$$
DECLARE
  last_vente RECORD;
BEGIN
  insert into axe(id_axe, code)
  select btrim(axe_livraison), btrim(axe_livraison) code
  from import_billet
  on conflict do nothing;
  insert into produit(id_produit, date_creation, nom_produit)
  select btrim(code_pack), now(), btrim(code_pack) nom_produit
  from import_billet
  on conflict do nothing;
  insert into users(nom) select btrim(code_vendeur) from import_billet on conflict do nothing;
  --insert into vente () values ();
--insert into details_vente(id_produit, quantite) select code_pack, quantite from import_billet;
END;
$$;

select generer_billet_2();


create table table1
(
  table1 varchar(10)
);

alter table table1
  drop constraint min_length;
alter table table2
  add constraint min_length check ( length(table2) > 5 );

insert into table1 (table1)
values ('11111A'),
       ('BBB'),
       ('dqsdqsC'),
       ('B'),
       ('POPOPOPOPO'),
       ('dqsdqsE'),
       ('AAA'),
       ('BA');



create table table2
(
  table2 varchar(10) unique not null
);

create or replace function table1_to_table2()
  returns varchar[]
  language plpgsql
as
$$
declare
  table1_1 record;
  error    varchar[];
begin
  for table1_1 in select * from table1
    loop
          begin
            insert into table2 values (table1_1.table1);
          exception
            when others then
              error := array_append(error, SQLERRM);
          end;
    end loop;
  return error;
end;
$$;

select table1_to_table2();

create or replace trigger transfert after insert on table1 execute function table1_to_table2();

select length('11111A');

insert into table2 values ('PER'),('UUU') on conflict do nothing ;
