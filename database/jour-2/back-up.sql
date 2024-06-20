--
-- PostgreSQL database dump
--

-- Dumped from database version 16.1 (Debian 16.1-1.pgdg120+1)
-- Dumped by pg_dump version 16.1 (Debian 16.1-1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: pk(text, text, integer); Type: FUNCTION; Schema: public; Owner: your_username
--

CREATE FUNCTION public.pk(prefix text, serial text, size integer) RETURNS text
    LANGUAGE plpgsql
    AS $$
BEGIN
  RETURN prefix || LPAD(nextval(serial)::TEXT, size, '0');
END;
$$;


ALTER FUNCTION public.pk(prefix text, serial text, size integer) OWNER TO your_username;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: asso_13; Type: TABLE; Schema: public; Owner: your_username
--

CREATE TABLE public.asso_13 (
    id_devis integer NOT NULL,
    id_paiement integer NOT NULL
);


ALTER TABLE public.asso_13 OWNER TO your_username;

--
-- Name: details_devis; Type: TABLE; Schema: public; Owner: your_username
--

CREATE TABLE public.details_devis (
    id_details_devis integer NOT NULL,
    id_prestation integer,
    id_type_maison integer,
    quantite numeric(15,3),
    prestation character varying(250),
    total numeric,
    code character varying(50),
    prix_unitaire numeric(15,3),
    unite character varying(50),
    id_unite integer,
    id_devis integer,
    id_type_travaux integer,
    duree integer
);


ALTER TABLE public.details_devis OWNER TO your_username;

--
-- Name: details_devis_id_details_devis_seq; Type: SEQUENCE; Schema: public; Owner: your_username
--

CREATE SEQUENCE public.details_devis_id_details_devis_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.details_devis_id_details_devis_seq OWNER TO your_username;

--
-- Name: details_devis_id_details_devis_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: your_username
--

ALTER SEQUENCE public.details_devis_id_details_devis_seq OWNED BY public.details_devis.id_details_devis;


--
-- Name: devis; Type: TABLE; Schema: public; Owner: your_username
--

CREATE TABLE public.devis (
    id_devis integer NOT NULL,
    prix_total numeric(15,2),
    date_devis date DEFAULT now(),
    date_construction date,
    id_finition integer NOT NULL,
    id_type_maison integer NOT NULL,
    id_utilisateur integer NOT NULL,
    est_paye boolean DEFAULT false,
    est_fini boolean DEFAULT false,
    date_fin date
);


ALTER TABLE public.devis OWNER TO your_username;

--
-- Name: devis_id_devis_seq; Type: SEQUENCE; Schema: public; Owner: your_username
--

CREATE SEQUENCE public.devis_id_devis_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.devis_id_devis_seq OWNER TO your_username;

--
-- Name: devis_id_devis_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: your_username
--

ALTER SEQUENCE public.devis_id_devis_seq OWNED BY public.devis.id_devis;


--
-- Name: prestation; Type: TABLE; Schema: public; Owner: your_username
--

CREATE TABLE public.prestation (
    id_prestation integer NOT NULL,
    prestation character varying(250),
    code character varying(50) NOT NULL,
    prix_unitaire numeric(15,3),
    duree integer,
    id_unite integer NOT NULL,
    id_prestation_1 integer,
    id_type_travaux integer NOT NULL
);


ALTER TABLE public.prestation OWNER TO your_username;

--
-- Name: quantite; Type: TABLE; Schema: public; Owner: your_username
--

CREATE TABLE public.quantite (
    id_type_maison integer NOT NULL,
    id_prestation integer NOT NULL,
    quantite numeric(15,3) DEFAULT 1 NOT NULL,
    duree integer
);


ALTER TABLE public.quantite OWNER TO your_username;

--
-- Name: type_maison; Type: TABLE; Schema: public; Owner: your_username
--

CREATE TABLE public.type_maison (
    id_type_maison integer NOT NULL,
    type_maison character varying(250) NOT NULL,
    duree integer NOT NULL,
    photo text,
    prix_total numeric(15,2),
    desciption text,
    CONSTRAINT type_maison_duree_check CHECK ((duree >= 0))
);


ALTER TABLE public.type_maison OWNER TO your_username;

--
-- Name: unite; Type: TABLE; Schema: public; Owner: your_username
--

CREATE TABLE public.unite (
    id_unite integer NOT NULL,
    unite character varying(250) NOT NULL,
    initial character varying(50)
);


ALTER TABLE public.unite OWNER TO your_username;

--
-- Name: devis_par_maison; Type: VIEW; Schema: public; Owner: your_username
--

CREATE VIEW public.devis_par_maison AS
 SELECT tm.id_type_maison,
    q.quantite,
    p.prestation,
    p.id_prestation,
    (p.prix_unitaire * q.quantite) AS total,
    p.code,
    p.prix_unitaire,
    u.unite,
    u.id_unite,
    p.id_type_travaux,
    q.duree
   FROM (((public.type_maison tm
     JOIN public.quantite q ON ((tm.id_type_maison = q.id_type_maison)))
     LEFT JOIN public.prestation p ON ((q.id_prestation = p.id_prestation)))
     JOIN public.unite u ON ((u.id_unite = p.id_unite)))
  ORDER BY p.code, tm.type_maison;


ALTER VIEW public.devis_par_maison OWNER TO your_username;

--
-- Name: finition; Type: TABLE; Schema: public; Owner: your_username
--

CREATE TABLE public.finition (
    id_finition integer NOT NULL,
    finition character varying(250) NOT NULL,
    majoration numeric(15,2) NOT NULL,
    photo text
);


ALTER TABLE public.finition OWNER TO your_username;

--
-- Name: finition_id_finition_seq; Type: SEQUENCE; Schema: public; Owner: your_username
--

CREATE SEQUENCE public.finition_id_finition_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.finition_id_finition_seq OWNER TO your_username;

--
-- Name: finition_id_finition_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: your_username
--

ALTER SEQUENCE public.finition_id_finition_seq OWNED BY public.finition.id_finition;


--
-- Name: mois; Type: TABLE; Schema: public; Owner: your_username
--

CREATE TABLE public.mois (
    id_mois integer NOT NULL,
    mois character varying(100),
    numero integer NOT NULL
);


ALTER TABLE public.mois OWNER TO your_username;

--
-- Name: mois_id_mois_seq; Type: SEQUENCE; Schema: public; Owner: your_username
--

CREATE SEQUENCE public.mois_id_mois_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.mois_id_mois_seq OWNER TO your_username;

--
-- Name: mois_id_mois_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: your_username
--

ALTER SEQUENCE public.mois_id_mois_seq OWNED BY public.mois.id_mois;


--
-- Name: paiement; Type: TABLE; Schema: public; Owner: your_username
--

CREATE TABLE public.paiement (
    id_paiement integer NOT NULL,
    date_paiement timestamp without time zone NOT NULL,
    id_devis integer,
    montant double precision DEFAULT 0
);


ALTER TABLE public.paiement OWNER TO your_username;

--
-- Name: paiement_id_paiement_seq; Type: SEQUENCE; Schema: public; Owner: your_username
--

CREATE SEQUENCE public.paiement_id_paiement_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.paiement_id_paiement_seq OWNER TO your_username;

--
-- Name: paiement_id_paiement_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: your_username
--

ALTER SEQUENCE public.paiement_id_paiement_seq OWNED BY public.paiement.id_paiement;


--
-- Name: prestation_id_prestation_seq; Type: SEQUENCE; Schema: public; Owner: your_username
--

CREATE SEQUENCE public.prestation_id_prestation_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.prestation_id_prestation_seq OWNER TO your_username;

--
-- Name: prestation_id_prestation_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: your_username
--

ALTER SEQUENCE public.prestation_id_prestation_seq OWNED BY public.prestation.id_prestation;


--
-- Name: seq_unite; Type: SEQUENCE; Schema: public; Owner: your_username
--

CREATE SEQUENCE public.seq_unite
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.seq_unite OWNER TO your_username;

--
-- Name: seq_utilisateur; Type: SEQUENCE; Schema: public; Owner: your_username
--

CREATE SEQUENCE public.seq_utilisateur
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.seq_utilisateur OWNER TO your_username;

--
-- Name: type_maison_id_type_maison_seq; Type: SEQUENCE; Schema: public; Owner: your_username
--

CREATE SEQUENCE public.type_maison_id_type_maison_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.type_maison_id_type_maison_seq OWNER TO your_username;

--
-- Name: type_maison_id_type_maison_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: your_username
--

ALTER SEQUENCE public.type_maison_id_type_maison_seq OWNED BY public.type_maison.id_type_maison;


--
-- Name: type_travaux; Type: TABLE; Schema: public; Owner: your_username
--

CREATE TABLE public.type_travaux (
    id_type_travaux integer NOT NULL,
    code character varying(50) NOT NULL,
    type_travaux character varying(250)
);


ALTER TABLE public.type_travaux OWNER TO your_username;

--
-- Name: type_travaux_id_type_travaux_seq; Type: SEQUENCE; Schema: public; Owner: your_username
--

CREATE SEQUENCE public.type_travaux_id_type_travaux_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.type_travaux_id_type_travaux_seq OWNER TO your_username;

--
-- Name: type_travaux_id_type_travaux_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: your_username
--

ALTER SEQUENCE public.type_travaux_id_type_travaux_seq OWNED BY public.type_travaux.id_type_travaux;


--
-- Name: unite_id_unite_seq; Type: SEQUENCE; Schema: public; Owner: your_username
--

CREATE SEQUENCE public.unite_id_unite_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.unite_id_unite_seq OWNER TO your_username;

--
-- Name: unite_id_unite_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: your_username
--

ALTER SEQUENCE public.unite_id_unite_seq OWNED BY public.unite.id_unite;


--
-- Name: utilisateur; Type: TABLE; Schema: public; Owner: your_username
--

CREATE TABLE public.utilisateur (
    id_utilisateur integer NOT NULL,
    nom character varying(500),
    prenom character varying(250),
    numero character varying(500),
    email character varying(500),
    privilege integer DEFAULT 0 NOT NULL,
    mot_de_passe text,
    photo text
);


ALTER TABLE public.utilisateur OWNER TO your_username;

--
-- Name: utilisateur_id_utilisateur_seq; Type: SEQUENCE; Schema: public; Owner: your_username
--

CREATE SEQUENCE public.utilisateur_id_utilisateur_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.utilisateur_id_utilisateur_seq OWNER TO your_username;

--
-- Name: utilisateur_id_utilisateur_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: your_username
--

ALTER SEQUENCE public.utilisateur_id_utilisateur_seq OWNED BY public.utilisateur.id_utilisateur;


--
-- Name: v_annee_devi; Type: VIEW; Schema: public; Owner: your_username
--

CREATE VIEW public.v_annee_devi AS
 SELECT min(EXTRACT(year FROM date_devis)) AS min,
    max(EXTRACT(year FROM date_devis)) AS max
   FROM public.devis;


ALTER VIEW public.v_annee_devi OWNER TO your_username;

--
-- Name: v_etat_paiement; Type: VIEW; Schema: public; Owner: your_username
--

CREATE VIEW public.v_etat_paiement AS
 SELECT id_devis,
    sum(montant) AS montant
   FROM public.paiement
  GROUP BY id_devis;


ALTER VIEW public.v_etat_paiement OWNER TO your_username;

--
-- Name: v_etat_devis_paiement; Type: VIEW; Schema: public; Owner: your_username
--

CREATE VIEW public.v_etat_devis_paiement AS
 SELECT d.id_devis,
    COALESCE(vdp.montant, (0)::double precision) AS paye,
    d.prix_total
   FROM (public.devis d
     LEFT JOIN public.v_etat_paiement vdp ON ((d.id_devis = vdp.id_devis)));


ALTER VIEW public.v_etat_devis_paiement OWNER TO your_username;

--
-- Name: v_mois_annee; Type: VIEW; Schema: public; Owner: your_username
--

CREATE VIEW public.v_mois_annee AS
 SELECT m.numero,
    m.mois,
    annee.annees
   FROM (public.mois m
     CROSS JOIN ( SELECT generate_series(min(EXTRACT(year FROM devis.date_devis)), max(EXTRACT(year FROM devis.date_devis)), (1)::numeric) AS annees
           FROM public.devis) annee);


ALTER VIEW public.v_mois_annee OWNER TO your_username;

--
-- Name: v_montant_par_moi_par_annee; Type: VIEW; Schema: public; Owner: your_username
--

CREATE VIEW public.v_montant_par_moi_par_annee AS
 SELECT sum(prix_total) AS montant,
    EXTRACT(year FROM date_devis) AS year,
    EXTRACT(month FROM date_devis) AS mounth
   FROM public.devis d
  GROUP BY date_devis;


ALTER VIEW public.v_montant_par_moi_par_annee OWNER TO your_username;

--
-- Name: v_montant_par_12_moi_par_annee; Type: VIEW; Schema: public; Owner: your_username
--

CREATE VIEW public.v_montant_par_12_moi_par_annee AS
 SELECT mois.mois,
    mois.numero,
    COALESCE(v_montant_par_moi_par_annee.montant, (0)::numeric) AS montant,
    (mois.annees)::character varying(20) AS annees
   FROM (public.v_mois_annee mois
     LEFT JOIN public.v_montant_par_moi_par_annee ON ((((mois.numero)::numeric = v_montant_par_moi_par_annee.mounth) AND (mois.annees = v_montant_par_moi_par_annee.year))));


ALTER VIEW public.v_montant_par_12_moi_par_annee OWNER TO your_username;

--
-- Name: details_devis id_details_devis; Type: DEFAULT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.details_devis ALTER COLUMN id_details_devis SET DEFAULT nextval('public.details_devis_id_details_devis_seq'::regclass);


--
-- Name: devis id_devis; Type: DEFAULT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.devis ALTER COLUMN id_devis SET DEFAULT nextval('public.devis_id_devis_seq'::regclass);


--
-- Name: finition id_finition; Type: DEFAULT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.finition ALTER COLUMN id_finition SET DEFAULT nextval('public.finition_id_finition_seq'::regclass);


--
-- Name: mois id_mois; Type: DEFAULT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.mois ALTER COLUMN id_mois SET DEFAULT nextval('public.mois_id_mois_seq'::regclass);


--
-- Name: paiement id_paiement; Type: DEFAULT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.paiement ALTER COLUMN id_paiement SET DEFAULT nextval('public.paiement_id_paiement_seq'::regclass);


--
-- Name: prestation id_prestation; Type: DEFAULT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.prestation ALTER COLUMN id_prestation SET DEFAULT nextval('public.prestation_id_prestation_seq'::regclass);


--
-- Name: type_maison id_type_maison; Type: DEFAULT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.type_maison ALTER COLUMN id_type_maison SET DEFAULT nextval('public.type_maison_id_type_maison_seq'::regclass);


--
-- Name: type_travaux id_type_travaux; Type: DEFAULT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.type_travaux ALTER COLUMN id_type_travaux SET DEFAULT nextval('public.type_travaux_id_type_travaux_seq'::regclass);


--
-- Name: unite id_unite; Type: DEFAULT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.unite ALTER COLUMN id_unite SET DEFAULT nextval('public.unite_id_unite_seq'::regclass);


--
-- Name: utilisateur id_utilisateur; Type: DEFAULT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.utilisateur ALTER COLUMN id_utilisateur SET DEFAULT nextval('public.utilisateur_id_utilisateur_seq'::regclass);


--
-- Data for Name: asso_13; Type: TABLE DATA; Schema: public; Owner: your_username
--

COPY public.asso_13 (id_devis, id_paiement) FROM stdin;
\.


--
-- Data for Name: details_devis; Type: TABLE DATA; Schema: public; Owner: your_username
--

COPY public.details_devis (id_details_devis, id_prestation, id_type_maison, quantite, prestation, total, code, prix_unitaire, unite, id_unite, id_devis, id_type_travaux, duree) FROM stdin;
\.


--
-- Data for Name: devis; Type: TABLE DATA; Schema: public; Owner: your_username
--

COPY public.devis (id_devis, prix_total, date_devis, date_construction, id_finition, id_type_maison, id_utilisateur, est_paye, est_fini, date_fin) FROM stdin;
\.


--
-- Data for Name: finition; Type: TABLE DATA; Schema: public; Owner: your_username
--

COPY public.finition (id_finition, finition, majoration, photo) FROM stdin;
1	Standart	0.00	/img/construction/finition/standard.jpeg
3	VIP	5.00	/img/construction/finition/vip.jpeg
2	Gold	2.00	/img/construction/finition/gold.jpeg
\.


--
-- Data for Name: mois; Type: TABLE DATA; Schema: public; Owner: your_username
--

COPY public.mois (id_mois, mois, numero) FROM stdin;
1	Janvier	1
2	Fevrier	2
3	Mars	3
4	Avril	4
5	Mais	5
6	Juin	6
7	Juillet	7
8	Aout	8
9	Septembre	9
10	Octobre	10
11	Novembre	11
12	Decembre	12
\.


--
-- Data for Name: paiement; Type: TABLE DATA; Schema: public; Owner: your_username
--

COPY public.paiement (id_paiement, date_paiement, id_devis, montant) FROM stdin;
\.


--
-- Data for Name: prestation; Type: TABLE DATA; Schema: public; Owner: your_username
--

COPY public.prestation (id_prestation, prestation, code, prix_unitaire, duree, id_unite, id_prestation_1, id_type_travaux) FROM stdin;
1	Mur de soutenement et demi cout	001	190000.000	\N	1	\N	1
2	Decapage des terrains meubles	101	9390.000	\N	2	\N	2
4	Beton armée dosée à 350kg/m3	202	\N	\N	3	\N	3
5	Semelles isolée	2021	573.215	\N	3	4	3
6	Amorces potaux	2022	573.215	\N	3	4	3
7	Chaine de base de 20x20	2023	573.215	\N	3	4	3
\.


--
-- Data for Name: quantite; Type: TABLE DATA; Schema: public; Owner: your_username
--

COPY public.quantite (id_type_maison, id_prestation, quantite, duree) FROM stdin;
2	1	1.000	1
4	1	1.000	1
4	6	1.000	1
2	7	1.000	1
3	1	1.000	1
3	6	1.000	1
3	5	1.000	1
\.


--
-- Data for Name: type_maison; Type: TABLE DATA; Schema: public; Owner: your_username
--

COPY public.type_maison (id_type_maison, type_maison, duree, photo, prix_total, desciption) FROM stdin;
2	VILLA VITREE	100	/img/construction/villa_vitre.png	\N	Maison vitée, luxueux idéale pour les vacances. 8 chambres avec salle de bain. Garage 2 voitures plaisirs. Conçu pour les terrains hors de la ville. Jardin, Garage, Salle de jeux, Salle de musculation, Salle de repos, Jaccouzi.
4	ECOLOGIQUE	100	/img/construction/ecologique.png	\N	Maison adoptant les couleurs de la nature, vers très séduisant. Terrain de basket, terrain de foot, terrain de volley, salle de repos, salle de bain, salle à manger, salle pour animaux.
3	MITOYENNE	100	/img/construction/mitonienne.png	\N	Maison idéale en plein centre ville, garage, wifi, salle de bain, salle à manger, air de jeux, terrain de foot, terrain de basket, piscine 50m2. Balançoire, equipement dernier cri pou assuerer votre sécurité.
\.


--
-- Data for Name: type_travaux; Type: TABLE DATA; Schema: public; Owner: your_username
--

COPY public.type_travaux (id_type_travaux, code, type_travaux) FROM stdin;
1	000	TRAVAUX PREPARATOIRE
2	100	TRAVAUX DE TERRASSEMENT
3	200	TRAVAUX EN INFRASTRUCTURE
\.


--
-- Data for Name: unite; Type: TABLE DATA; Schema: public; Owner: your_username
--

COPY public.unite (id_unite, unite, initial) FROM stdin;
1	Metre cube	m3
2	Metre carré	m2
3	FFT	fft
\.


--
-- Data for Name: utilisateur; Type: TABLE DATA; Schema: public; Owner: your_username
--

COPY public.utilisateur (id_utilisateur, nom, prenom, numero, email, privilege, mot_de_passe, photo) FROM stdin;
1	\N	\N	0333333333	\N	0	\N	\N
3	\N	\N	0332021252	\N	0	\N	\N
4	\N	\N	03333333	\N	0	\N	\N
5	\N	\N	03333333333	\N	0	\N	\N
2	\N	\N	0322021225	admin@gmail.com	10	adminadmin	\N
6	\N	\N	033333333334	\N	0	\N	\N
\.


--
-- Name: details_devis_id_details_devis_seq; Type: SEQUENCE SET; Schema: public; Owner: your_username
--

SELECT pg_catalog.setval('public.details_devis_id_details_devis_seq', 63, true);


--
-- Name: devis_id_devis_seq; Type: SEQUENCE SET; Schema: public; Owner: your_username
--

SELECT pg_catalog.setval('public.devis_id_devis_seq', 31, true);


--
-- Name: finition_id_finition_seq; Type: SEQUENCE SET; Schema: public; Owner: your_username
--

SELECT pg_catalog.setval('public.finition_id_finition_seq', 3, true);


--
-- Name: mois_id_mois_seq; Type: SEQUENCE SET; Schema: public; Owner: your_username
--

SELECT pg_catalog.setval('public.mois_id_mois_seq', 12, true);


--
-- Name: paiement_id_paiement_seq; Type: SEQUENCE SET; Schema: public; Owner: your_username
--

SELECT pg_catalog.setval('public.paiement_id_paiement_seq', 27, true);


--
-- Name: prestation_id_prestation_seq; Type: SEQUENCE SET; Schema: public; Owner: your_username
--

SELECT pg_catalog.setval('public.prestation_id_prestation_seq', 7, true);


--
-- Name: seq_unite; Type: SEQUENCE SET; Schema: public; Owner: your_username
--

SELECT pg_catalog.setval('public.seq_unite', 3, true);


--
-- Name: seq_utilisateur; Type: SEQUENCE SET; Schema: public; Owner: your_username
--

SELECT pg_catalog.setval('public.seq_utilisateur', 3, true);


--
-- Name: type_maison_id_type_maison_seq; Type: SEQUENCE SET; Schema: public; Owner: your_username
--

SELECT pg_catalog.setval('public.type_maison_id_type_maison_seq', 4, true);


--
-- Name: type_travaux_id_type_travaux_seq; Type: SEQUENCE SET; Schema: public; Owner: your_username
--

SELECT pg_catalog.setval('public.type_travaux_id_type_travaux_seq', 3, true);


--
-- Name: unite_id_unite_seq; Type: SEQUENCE SET; Schema: public; Owner: your_username
--

SELECT pg_catalog.setval('public.unite_id_unite_seq', 3, true);


--
-- Name: utilisateur_id_utilisateur_seq; Type: SEQUENCE SET; Schema: public; Owner: your_username
--

SELECT pg_catalog.setval('public.utilisateur_id_utilisateur_seq', 6, true);


--
-- Name: asso_13 asso_13_pkey; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.asso_13
    ADD CONSTRAINT asso_13_pkey PRIMARY KEY (id_devis, id_paiement);


--
-- Name: details_devis details_devis_pkey; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.details_devis
    ADD CONSTRAINT details_devis_pkey PRIMARY KEY (id_details_devis);


--
-- Name: devis devis_pkey; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.devis
    ADD CONSTRAINT devis_pkey PRIMARY KEY (id_devis);


--
-- Name: finition finition_finition_key; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.finition
    ADD CONSTRAINT finition_finition_key UNIQUE (finition);


--
-- Name: finition finition_pkey; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.finition
    ADD CONSTRAINT finition_pkey PRIMARY KEY (id_finition);


--
-- Name: mois mois_pkey; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.mois
    ADD CONSTRAINT mois_pkey PRIMARY KEY (id_mois);


--
-- Name: paiement paiement_pkey; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.paiement
    ADD CONSTRAINT paiement_pkey PRIMARY KEY (id_paiement);


--
-- Name: prestation prestation_code_key; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.prestation
    ADD CONSTRAINT prestation_code_key UNIQUE (code);


--
-- Name: prestation prestation_pkey; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.prestation
    ADD CONSTRAINT prestation_pkey PRIMARY KEY (id_prestation);


--
-- Name: prestation prestation_prestation_key; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.prestation
    ADD CONSTRAINT prestation_prestation_key UNIQUE (prestation);


--
-- Name: quantite quantite_pkey; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.quantite
    ADD CONSTRAINT quantite_pkey PRIMARY KEY (id_type_maison, id_prestation);


--
-- Name: type_maison type_maison_pkey; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.type_maison
    ADD CONSTRAINT type_maison_pkey PRIMARY KEY (id_type_maison);


--
-- Name: type_maison type_maison_type_maison_key; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.type_maison
    ADD CONSTRAINT type_maison_type_maison_key UNIQUE (type_maison);


--
-- Name: type_travaux type_travaux_code_key; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.type_travaux
    ADD CONSTRAINT type_travaux_code_key UNIQUE (code);


--
-- Name: type_travaux type_travaux_pkey; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.type_travaux
    ADD CONSTRAINT type_travaux_pkey PRIMARY KEY (id_type_travaux);


--
-- Name: type_travaux type_travaux_type_travaux_key; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.type_travaux
    ADD CONSTRAINT type_travaux_type_travaux_key UNIQUE (type_travaux);


--
-- Name: unite unite_initial_key; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.unite
    ADD CONSTRAINT unite_initial_key UNIQUE (initial);


--
-- Name: unite unite_pkey; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.unite
    ADD CONSTRAINT unite_pkey PRIMARY KEY (id_unite);


--
-- Name: unite unite_unite_key; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.unite
    ADD CONSTRAINT unite_unite_key UNIQUE (unite);


--
-- Name: utilisateur utilisateur_email_key; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.utilisateur
    ADD CONSTRAINT utilisateur_email_key UNIQUE (email);


--
-- Name: utilisateur utilisateur_numero_key; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.utilisateur
    ADD CONSTRAINT utilisateur_numero_key UNIQUE (numero);


--
-- Name: utilisateur utilisateur_pkey; Type: CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.utilisateur
    ADD CONSTRAINT utilisateur_pkey PRIMARY KEY (id_utilisateur);


--
-- Name: asso_13 asso_13_id_devis_fkey; Type: FK CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.asso_13
    ADD CONSTRAINT asso_13_id_devis_fkey FOREIGN KEY (id_devis) REFERENCES public.devis(id_devis);


--
-- Name: asso_13 asso_13_id_paiement_fkey; Type: FK CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.asso_13
    ADD CONSTRAINT asso_13_id_paiement_fkey FOREIGN KEY (id_paiement) REFERENCES public.paiement(id_paiement);


--
-- Name: details_devis details_devis_id_devis_fkey; Type: FK CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.details_devis
    ADD CONSTRAINT details_devis_id_devis_fkey FOREIGN KEY (id_devis) REFERENCES public.devis(id_devis);


--
-- Name: details_devis details_devis_id_prestation_fkey; Type: FK CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.details_devis
    ADD CONSTRAINT details_devis_id_prestation_fkey FOREIGN KEY (id_prestation) REFERENCES public.prestation(id_prestation);


--
-- Name: details_devis details_devis_id_type_maison_fkey; Type: FK CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.details_devis
    ADD CONSTRAINT details_devis_id_type_maison_fkey FOREIGN KEY (id_type_maison) REFERENCES public.type_maison(id_type_maison);


--
-- Name: details_devis details_devis_id_type_travaux_fkey; Type: FK CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.details_devis
    ADD CONSTRAINT details_devis_id_type_travaux_fkey FOREIGN KEY (id_type_travaux) REFERENCES public.type_travaux(id_type_travaux);


--
-- Name: details_devis details_devis_id_unite_fkey; Type: FK CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.details_devis
    ADD CONSTRAINT details_devis_id_unite_fkey FOREIGN KEY (id_unite) REFERENCES public.unite(id_unite);


--
-- Name: devis devis_id_finition_fkey; Type: FK CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.devis
    ADD CONSTRAINT devis_id_finition_fkey FOREIGN KEY (id_finition) REFERENCES public.finition(id_finition);


--
-- Name: devis devis_id_type_maison_fkey; Type: FK CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.devis
    ADD CONSTRAINT devis_id_type_maison_fkey FOREIGN KEY (id_type_maison) REFERENCES public.type_maison(id_type_maison);


--
-- Name: devis devis_id_utilisateur_fkey; Type: FK CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.devis
    ADD CONSTRAINT devis_id_utilisateur_fkey FOREIGN KEY (id_utilisateur) REFERENCES public.utilisateur(id_utilisateur);


--
-- Name: paiement paiement_id_devis_fkey; Type: FK CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.paiement
    ADD CONSTRAINT paiement_id_devis_fkey FOREIGN KEY (id_devis) REFERENCES public.devis(id_devis);


--
-- Name: prestation prestation_id_prestation_1_fkey; Type: FK CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.prestation
    ADD CONSTRAINT prestation_id_prestation_1_fkey FOREIGN KEY (id_prestation_1) REFERENCES public.prestation(id_prestation);


--
-- Name: prestation prestation_id_type_travaux_fkey; Type: FK CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.prestation
    ADD CONSTRAINT prestation_id_type_travaux_fkey FOREIGN KEY (id_type_travaux) REFERENCES public.type_travaux(id_type_travaux);


--
-- Name: prestation prestation_id_unite_fkey; Type: FK CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.prestation
    ADD CONSTRAINT prestation_id_unite_fkey FOREIGN KEY (id_unite) REFERENCES public.unite(id_unite);


--
-- Name: quantite quantite_id_prestation_fkey; Type: FK CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.quantite
    ADD CONSTRAINT quantite_id_prestation_fkey FOREIGN KEY (id_prestation) REFERENCES public.prestation(id_prestation);

--
-- Name: quantite quantite_id_type_maison_fkey; Type: FK CONSTRAINT; Schema: public; Owner: your_username
--

ALTER TABLE ONLY public.quantite
    ADD CONSTRAINT quantite_id_type_maison_fkey FOREIGN KEY (id_type_maison) REFERENCES public.type_maison(id_type_maison);


--
-- PostgreSQL database dump complete
--

