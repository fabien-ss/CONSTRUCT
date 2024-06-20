
create table maison_travaux(
    type_maison varchar(100),
    description varchar(500),
  surface integer,
  code_travaux varchar(10),
  type_travaux varchar(500),
  unite varchar(10),
  prix_unitaire double precision,
  quantite double precision,
  duree_travaux int
);

create table devis_temp(
    client varchar(100),
  ref_devis varchar(100),
  type_maison varchar(400),
  finition varchar(100),
  taux_finition decimal,
  date_devis varchar(200),
  date_debut varchar(200),
  lieu varchar(500)
);


create table paiement_temp(
  ref_devis varchar(50),
  ref_paiement varchar(50),
  date_paiement varchar(50),
  montant double precision
);

create or replace function insert_maison()
returns varchar[]
language plpgsql
as
  $$
declare
  maison record;
  error    varchar[];
begin
  for maison in select * from maison_travaux
    loop
     begin
       insert into type_maison (type_maison, desciption, surface, duree) values (maison.type_maison, maison.description, maison.surface, maison.duree_travaux);
     exception
       when others then
         error := array_append(error, SQLERRM );
     end;
    end loop;
  return error;
end;
  $$;

select insert_maison();

create or replace function insert_unite() -- prestation
  returns varchar[]
  language plpgsql
as
$$
declare
  maison record;
  error    varchar[];
begin
  for maison in select * from maison_travaux
    loop
          begin
            insert into unite (prestation, code, prix_unitaire, duree) values (maison.type_maison, maison.description, maison.surface, maison.duree_travaux);
          exception
            when others then
              error := array_append(error, SQLERRM );
          end;
    end loop;
  return error;
end;
$$;


create or replace function insert_travaux() -- prestation
  returns varchar[]
  language plpgsql
as
$$
declare
  maison record;
  error    varchar[];
begin
  for maison in select * from maison_travaux
    loop
          begin
            insert into prestation (prestation, code, prix_unitaire, duree) values (maison.type_maison, maison.description, maison.surface, maison.duree_travaux);
          exception
            when others then
              error := array_append(error, SQLERRM );
          end;
    end loop;
  return error;
end;
$$;



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
