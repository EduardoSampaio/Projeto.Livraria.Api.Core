use treino;


INSERT INTO Perfil (nome) VALUES ('User');
INSERT INTO Perfil (nome) VALUES ('Admin');
INSERT INTO Perfil (nome) VALUES ('Owner');

INSERT INTO Usuario (nome,email,senha,perfilId) values ('root','root@root.com','1234',(select id from Perfil where nome = 'Owner'));