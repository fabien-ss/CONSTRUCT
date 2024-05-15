CREATE TABLE Type_maison(
                          id_type_maison SERIAL,
                          type_maison VARCHAR(250)  NOT NULL,
                          duree INTEGER NOT NULL check(duree >= 0),
                          photo TEXT,
                          prix_total NUMERIC(15,2),
                          desciption TEXT,
                          PRIMARY KEY(id_type_maison),
                          UNIQUE(type_maison)
);

CREATE TABLE Type_travaux(
                           id_type_travaux SERIAL,
                           code VARCHAR(50)  NOT NULL,
                           type_travaux VARCHAR(250) ,
                           PRIMARY KEY(id_type_travaux),
                           UNIQUE(code),
                           UNIQUE(type_travaux)
);

CREATE TABLE unite(
                    id_unite SERIAL,
                    unite VARCHAR(250)  NOT NULL,
                    initial VARCHAR(50) ,
                    PRIMARY KEY(id_unite),
                    UNIQUE(unite),
                    UNIQUE(initial)
);

CREATE TABLE Utilisateur(
                          id_utilisateur SERIAL,
                          nom VARCHAR(500),
                          prenom VARCHAR(250) ,
                          numero VARCHAR(500) ,
                          email VARCHAR(500) ,
                          privilege INTEGER NOT NULL default 0,
                          mot_de_passe TEXT,
                          photo TEXT,
                          PRIMARY KEY(id_utilisateur),
                          UNIQUE(numero),
                          UNIQUE(email)
);


CREATE TABLE finition(
                       id_finition SERIAL,
                       finition VARCHAR(250)  NOT NULL,
                       majoration NUMERIC(15,2)   NOT NULL,
                       photo Text ,
                       PRIMARY KEY(id_finition),
                       UNIQUE(finition)
);

CREATE TABLE Prestation(
                         id_prestation SERIAL,
                         prestation VARCHAR(250) ,
                         code VARCHAR(50)  NOT NULL,
                         prix_unitaire NUMERIC(15,3),
                         duree INTEGER,
                         id_unite INTEGER NOT NULL,
                         id_prestation_1 INTEGER,
                         id_type_travaux INTEGER NOT NULL,
                         PRIMARY KEY(id_prestation),
                         UNIQUE(prestation),
                         UNIQUE(code),
                         FOREIGN KEY(id_unite) REFERENCES unite(id_unite),
                         FOREIGN KEY(id_prestation_1) REFERENCES Prestation(id_prestation),
                         FOREIGN KEY(id_type_travaux) REFERENCES Type_travaux(id_type_travaux)
);

CREATE TABLE devis(
                    id_devis SERIAL,
                    prix_total NUMERIC(15,2)  ,
                    date_devis DATE default now(),
                    date_construction DATE,
                    id_finition INTEGER NOT NULL,
                    id_type_maison INTEGER NOT NULL,
                    id_utilisateur INTEGER NOT NULL,
                    est_paye boolean default false,
                    est_fini boolean default false,
                    PRIMARY KEY(id_devis),
                    FOREIGN KEY(id_finition) REFERENCES finition(id_finition),
                    FOREIGN KEY(id_type_maison) REFERENCES Type_maison(id_type_maison),
                    FOREIGN KEY(id_utilisateur) REFERENCES Utilisateur(id_utilisateur)
);


CREATE TABLE paiement(
                       id_paiement SERIAL,
                       date_paiement TIMESTAMP NOT NULL,
                       id_devis INTEGER references devis(id_devis),
                       PRIMARY KEY(id_paiement)
);

CREATE TABLE quantite(
                       id_type_maison INTEGER,
                       id_prestation INTEGER,
                       quantite NUMERIC(15,3)   NOT NULL default 1,
                       PRIMARY KEY(id_type_maison, id_prestation),
                       FOREIGN KEY(id_type_maison) REFERENCES Type_maison(id_type_maison),
                       FOREIGN KEY(id_prestation) REFERENCES Prestation(id_prestation)
);

CREATE TABLE Asso_13(
                      id_devis INTEGER,
                      id_paiement INTEGER,
                      PRIMARY KEY(id_devis, id_paiement),
                      FOREIGN KEY(id_devis) REFERENCES devis(id_devis),
                      FOREIGN KEY(id_paiement) REFERENCES paiement(id_paiement)
);


create table details_devis(
    id_details_devis serial primary key ,
    id_prestation integer references Prestation(id_prestation),
    id_type_maison integer references Type_maison(id_type_maison),
    quantite numeric(15,3),
    prestation varchar(250),
    total numeric,
    code varchar(50),
    prix_unitaire numeric(15, 3),
    unite varchar(50),
    id_unite integer references unite(id_unite)
);

create table mois (
    id_mois serial primary key ,
    mois varchar(100),
    numero int not null
);
