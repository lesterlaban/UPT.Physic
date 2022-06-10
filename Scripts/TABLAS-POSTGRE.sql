/*
drop table registro_consulta;
drop table tratamiento_recurso;
drop table tratamiento;
drop table encuesta;
drop table usuario;
drop table rol;
drop table pregunta;
drop table nivel_dolor;
drop table zona_dolor;
drop table recurso;
*/

create table rol
(
	id int generated always as identity,
	codigo varchar(100) not null,
	estado boolean not null,
	primary key (id)
);

create table usuario 
(
	id int generated always as identity,
	nombre varchar(100) not null,
	contrasenia varchar(100) not null,
	idrol int ,
	estado boolean not null,
	primary key (id),
    foreign key (idrol) references rol(id)
);

create table pregunta
(
	id int generated always as identity,
	descripcion varchar(5000) not null,
	estado boolean not null,
	primary key (id)
);

create table nivel_dolor
(
	id int generated always as identity,
	descripcion varchar(100) not null,
	estado boolean not null,
	primary key (id)
);

create table zona_dolor
(
	id int generated always as identity,
	descripcion varchar(100) not null,
	estado boolean not null,
	primary key (id)
);

create table recurso
(
	id int generated always as identity,
	titulo varchar(100) not null,
	descripcion varchar(500) not null,
	url varchar(100) not null,
	estado boolean not null,
	primary key (id)
);

create table encuesta
(
	id int generated always as identity,
	idpregunta int not null,
	idusuario int not null,
	puntaje int not null,
	estado boolean not null,
	primary key (id),
	foreign key (idusuario) references usuario(id),
	foreign key (idpregunta) references pregunta(id)
);

create table tratamiento
(
	id int generated always as identity,
	idzona int not null,
	idniveldolor int not null,
	puntajeminimo int not null,
	puntajemaximo int not null,
	estado boolean not null,
	primary key (id),
	foreign key (idniveldolor) references nivel_dolor(id),
	foreign key (idzona) references zona_dolor(id)
);

create table tratamiento_recurso
(
	id int generated always as identity,
	idtratamiento int not null,
	idrecurso int not null,
	primary key (id),
	foreign key (idtratamiento) references tratamiento(id),
	foreign key (idrecurso) references recurso(id)
);

create table registro_consulta
(
	id int generated always as identity,
	idusuario int not null,
	idzona int not null,
	idniveldolor int not null,
	puntajeminimo int not null,
	fecha date not null,
	estado boolean not null,
	primary key (id),
	foreign key (idusuario) references usuario(id),
	foreign key (idniveldolor) references nivel_dolor(id),
	foreign key (idzona) references zona_dolor(id)
);

INSERT INTO rol (codigo, estado) VALUES ('ADMIN', true), ('PACIENTE', true);
INSERT INTO usuario (nombre, contrasenia, idrol, estado) VALUES 
('LESTER', '123456', 1, true), 
('ALLY', '123456', 2, true);

INSERT INTO PREGUNTA(pregunta, estado) VALUES ('¿Cuanto califica su nivel de entrenamiento?', true), ('¿Hace Ejercicio frecuentemente? considere 1 la minima opción.', true);



