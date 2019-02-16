use treino;

CREATE TABLE Perfil(
	id integer auto_increment primary key,
    nome varchar(30) NOT NULL
);

CREATE TABLE Usuario(
	id integer auto_increment primary key,
    nome varchar(30) NOT NULL,
    email varchar(30) NOT NULL,
	senha varchar(50) NOT NULL,
    perfilId integer NOT NULL
);

CREATE TABLE Livro(
	id integer auto_increment primary key,
    nome varchar(30) NOT NULL,
    autor varchar(30)  NOT NULL,
	ano integer  NOT NULL
);

ALTER TABLE USUARIO 
ADD CONSTRAINT FK_USUARIO_PERFIL FOREIGN KEY (PerfilId)
REFERENCES PERFIL(id)

