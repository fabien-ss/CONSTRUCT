create table utilisateur
(
  id_utilisateur   varchar(50) default pk('USR'::text, 'seq_utilisateur'::text, 8) not null
    primary key,
  nom              varchar(250) not null ,
  prenom           varchar(250),
  image            text,
  biographie       char(250),
  email            varchar(250)
    constraint unique_mail
      unique,
  mot_de_passe     varchar(250) check ( length(mot_de_passe) >8 ),
  privilege        smallint                                                                  default 0,
  date_naissance   timestamp,
  date_inscription timestamp   default now()                                                 not null
);

create table unite(
  id_unite varchar(50) default pk('UNT', 'seq_unite', 8) primary key ,
  unite varchar(250) unique,
  valeur_unitaire float default 1
);
