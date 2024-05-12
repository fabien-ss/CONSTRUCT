create function insert_multiple_rows() returns void
  language plpgsql
as
$$
DECLARE
f RECORD;
BEGIN
FOR f IN SELECT * FROM facture LOOP
  FOR i IN 0..3 LOOP
         INSERT INTO details_facture (id, article, quantite, facture) VALUES
           (
           'DET' || nextval('details_seq'), getRandomArticle(), floor(random() * (200 - 0 + 1) + 0)::int, f.id
           );
END LOOP;
END LOOP;
END;
$$;

alter function insert_multiple_rows_2() owner to your_username;
----------------------
create function update_price() returns void
  language plpgsql
as
$$
DECLARE
f RECORD;
BEGIN
FOR f IN SELECT * FROM details_facture LOOP
update details_facture set prix = f.quantite * getArcticlePrice(f.article) where id = f.id;
END LOOP;
END;
$$;

alter function update_price() owner to your_username;
---------------------
create function concat_string_with_serial(prefix text, serial text, size integer) returns text
  language plpgsql
as
$$
BEGIN
RETURN prefix || LPAD(nextval(serial)::TEXT, size, '0');
END;
$$;

alter function concat_string_with_serial(text, text, integer) owner to your_username;

---------------------
create function getarcticleprice(id character varying) returns double precision
  language plpgsql
as
$$
DECLARE
price double precision;
BEGIN
SELECT tempPrice.prix / tempPrice.quantite into price from tempPrice where article = $1 and prix is not null ;
RETURN price;
END;
$$;

alter function getarcticleprice(varchar) owner to your_username;

-------------------




