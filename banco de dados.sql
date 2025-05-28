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

create table produto(
id int primary key auto_increment,
descricao varchar(100),
quantidade int,
preco decimal(10.2),
datacadastro date,
foto varchar(60),
promocao tinyint
);
select * from produto;