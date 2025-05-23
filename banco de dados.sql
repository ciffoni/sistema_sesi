-- criar o projeto
create database sistema;
-- ativar o banco
use sistema;
-- criar a tabela
create table usuario(
id int primary key auto_increment,
nome varchar(60),
email varchar(60),
senha varchar(50)
);
select * from usuario;