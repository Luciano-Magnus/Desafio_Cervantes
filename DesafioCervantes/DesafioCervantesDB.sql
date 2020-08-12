-- Database: DesafioCervantesDB

-- DROP DATABASE "DesafioCervantesDB";

CREATE DATABASE "DesafioCervantesDB"
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Portuguese_Brazil.1252'
    LC_CTYPE = 'Portuguese_Brazil.1252'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;


    create TABLE cadastro (
        id_cadastro serial not null,
        campo_texto varchar(100) not null,
        campo_numero decimal(12,2) not null
    );

    create table logCadastro(
        idOperacao serial not null,
        operacao varchar(10) not null,
        dataOperacao timestamp default current_timestamp
    );

    create or replace function validaNumero() returns trigger as $validaNumero$
    declare
    numero decimal(12,2);
	begin
		numero = new.campo_numero;
		if (TG_OP = 'UPDATE') THEN
			if numero <> old.campo_numero then
				if EXISTS(SELECT * FROM cadastro WHERE campo_numero = numero) then
				raise exception 'Os numeros nao podem se repetir .';
				end if;
				if numero <= 0 then
				raise exception 'Numero deve ser maior que 0';
				end if;
			end if;
		end if;
		if (TG_OP = 'INSERT') THEN
			if EXISTS(SELECT * FROM cadastro WHERE campo_numero = numero) then
				raise exception 'Os numeros nao podem se repetir.';
			end if;
			if numero <= 0 then
				raise exception 'Numero deve ser maior que 0.';
			end if;
		end if;
	return new;
	end
	$validaNumero$language plpgsql;

    create trigger validaNumero before insert or update on cadastro
    for each row execute procedure validaNumero();