CREATE DATABASE bibliotecaPABD;
USE  bibliotecaPABD;

SELECT * from livro;


-- Tabela usuario
CREATE TABLE IF NOT EXISTS usuario (
  id_usuario INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  nome_usuario VARCHAR(100) NOT NULL,
  email VARCHAR(100) NOT NULL,
  senha VARCHAR(255) NOT NULL,
  telefone VARCHAR(15) NULL,
  cep VARCHAR(15) NULL,
  rua VARCHAR(100) NULL,
  bairro VARCHAR(100) NULL,
  numero VARCHAR(10) NULL
);

-- Tabela livro
CREATE TABLE IF NOT EXISTS livro (
  id_livro INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  nome_livro VARCHAR(100) NOT NULL,
  quantidade_paginas INT NULL,
  descricao TEXT NULL,
  foto_capa MEDIUMBLOB,
  ano_publicacao YEAR NULL
);

-- Tabela categoria
CREATE TABLE IF NOT EXISTS categoria (
  id_categoria INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  nome_categoria VARCHAR(100) NOT NULL
);

-- Tabela autor
CREATE TABLE IF NOT EXISTS autor (
  id_autor INT PRIMARY KEY AUTO_INCREMENT,
  nome VARCHAR(45) NOT NULL
);

-- Tabela estoque
CREATE TABLE IF NOT EXISTS estoque (
  id_estoque INT PRIMARY KEY AUTO_INCREMENT,
  quantidade_estoque INT NOT NULL,
  fk_id_livro INT NOT NULL,  -- Adiciona a chave estrangeira
  FOREIGN KEY (fk_id_livro) REFERENCES livro(id_livro) ON DELETE CASCADE
);


-- Tabela emprestimos
CREATE TABLE IF NOT EXISTS emprestimos (
  id_emprestimo INT PRIMARY KEY AUTO_INCREMENT,
  data_emprestimo DATE NOT NULL,
  data_prevista_devolucao DATE NOT NULL,
  data_devolucao DATE NULL,
  fk_id_livro INT NOT NULL,
  FOREIGN KEY (fk_id_livro) REFERENCES livro(id_livro) ON DELETE CASCADE
);

-- Tabela Usuario_Emprestimos (Relacionamento entre Usuario e Emprestimos)
CREATE TABLE IF NOT EXISTS usuario_emprestimos (
  fk_id_usuario INT NOT NULL,
  fk_id_emprestimo INT NOT NULL,
  PRIMARY KEY (fk_id_usuario, fk_id_emprestimo),
  FOREIGN KEY (fk_id_usuario) REFERENCES usuario(id_usuario) ON DELETE CASCADE,
  FOREIGN KEY (fk_id_emprestimo) REFERENCES emprestimos(id_emprestimo) ON DELETE CASCADE
);

-- Tabela Livro_Categoria (Relacionamento entre Livro e Categoria)
CREATE TABLE IF NOT EXISTS livro_categoria (
  id_livro_categoria INT PRIMARY KEY AUTO_INCREMENT,
  fk_id_livros INT NOT NULL,
  fk_id_categoria INT NOT NULL,
  FOREIGN KEY (fk_id_livros) REFERENCES livro(id_livro) ON DELETE CASCADE,
  FOREIGN KEY (fk_id_categoria) REFERENCES categoria(id_categoria) ON DELETE CASCADE
);


-- Tabela Autor_Livro (Relacionamento entre Autor e Livro)
CREATE TABLE IF NOT EXISTS autor_livro (
  id_autor_livros INT PRIMARY KEY AUTO_INCREMENT,
  fk_id_autor INT NOT NULL,
  fk_id_livro INT NOT NULL,
  FOREIGN KEY (fk_id_autor) REFERENCES autor(id_autor) ON DELETE CASCADE,
  FOREIGN KEY (fk_id_livro) REFERENCES livro(id_livro) ON DELETE CASCADE
);

-- inserção de dados para testar os gets e deltes

-- Inserindo dados na tabela usuario
INSERT INTO usuario (nome_usuario, email, senha, telefone, cep, rua, bairro, numero) VALUES
('João Silva', 'joao@email.com', 'senha123', '11987654321', '01001-000', 'Rua das Flores', 'Centro', '123'),
('Maria Oliveira', 'maria@email.com', 'senha456', '11987654322', '02002-000', 'Av. Paulista', 'Bela Vista', '456');

-- Inserindo dados na tabela livro
INSERT INTO livro (nome_livro, quantidade_paginas, descricao, foto_capa, ano_publicacao) VALUES
('O Senhor dos Anéis', 1178, 'Um clássico da fantasia', NULL, 1954),
('1984', 328, 'Um romance distópico', NULL, 1949);

-- Inserindo dados na tabela categoria
INSERT INTO categoria (nome_categoria) VALUES
('Fantasia'),
('Ficção Científica'),
('Distopia');

-- Inserindo dados na tabela autor
INSERT INTO autor (nome) VALUES
('J.R.R. Tolkien'),
('George Orwell');

-- Inserindo dados na tabela estoque
INSERT INTO estoque (quantidade_estoque, fk_id_livro) VALUES
(10, 1),
(5, 2);

-- Inserindo dados na tabela emprestimos
INSERT INTO emprestimos (data_emprestimo, data_prevista_devolucao, data_devolucao, fk_id_livro) VALUES
('2025-02-01', '2025-02-15', NULL, 1),
('2025-01-20', '2025-02-03', '2025-02-02', 2);

-- Inserindo dados na tabela usuario_emprestimos
INSERT INTO usuario_emprestimos (fk_id_usuario, fk_id_emprestimo) VALUES
(1, 1),
(2, 2);

-- Inserindo dados na tabela livro_categoria
INSERT INTO livro_categoria (fk_id_livros, fk_id_categoria) VALUES
(1, 1), -- O Senhor dos Anéis é Fantasia
(2, 3); -- 1984 é Distopia

-- Inserindo dados na tabela autor_livro
INSERT INTO autor_livro (fk_id_autor, fk_id_livro) VALUES
(1, 1), -- J.R.R. Tolkien escreveu O Senhor dos Anéis
(2, 2); -- George Orwell escreveu 1984




