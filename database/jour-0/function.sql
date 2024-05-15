create function pk(prefix text, serial text, size integer) returns text
  language plpgsql
as
$$
BEGIN
  RETURN prefix || LPAD(nextval(serial)::TEXT, size, '0');
END;
$$;

