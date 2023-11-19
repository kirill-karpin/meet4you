CREATE ROLE meetforyou WITH LOGIN ENCRYPTED PASSWORD 'meetforyou';

GRANT ALL ON SCHEMA public TO meetforyou;
GRANT USAGE ON SCHEMA public TO meetforyou;
GRANT ALL ON ALL TABLES    IN SCHEMA public TO meetforyou;


create DATABASE meetforyou_db;
ALTER DATABASE meetforyou_db OWNER TO meetforyou;
create schema main;
grant all privileges on database meetforyou_db to meetforyou;
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA main TO meetforyou;
GRANT ALL PRIVILEGES ON SCHEMA main TO GROUP meetforyou;

