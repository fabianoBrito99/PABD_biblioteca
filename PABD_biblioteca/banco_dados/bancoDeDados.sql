CREATE DATABASE bibliotecaPABD;
USE  bibliotecaPABD;

create table usuario(
id_usuario INT PRIMARY KEY AUTO_INCREMENT,
nome_usuario VARCHAR(100),
email VARCHAR(100),
senha VARCHAR(255),
telefone VARCHAR(15),
cep VARCHAR(10),
rua VARCHAR(45),
bairro VARCHAR(45),
numero INT
);

CREATE TABLE emprestimo(
id_emprestimo INT PRIMARY KEY AUTO_INCREMENT,
data_emprestimo DATE,
data_prevista_devolucao DATE,
data_devlucao DATE,
fk_id_livro INT
);

create table Livro(
id_livro INT PRIMARY KEY AUTO_INCREMENT,
nome_livro VARCHAR(100),
qtd_paginas VARCHAR(100)
);

select * from Livro;
INSERT INTO Livro(nome_livro, qtd_paginas)  values("livro1", "10");


