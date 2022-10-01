/*
drop table registro_consulta;
drop table tratamiento_recurso;
drop table tratamiento;
drop table nivel_dolor;
drop table zona_dolor;
drop table recurso;
drop table encuesta_seccion_usuario;
drop table pregunta_usuario;
drop table pregunta;
drop table rango_seccion;
drop table encuesta_seccion;
drop table encuesta;
drop table usuario;
drop table rol;
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


create table registro_consulta
(
	id int generated always as identity,
	idusuario int not null,
	idzona int not null,
	idniveldolor int not null,
	puntajeminimo int not null,
	fecha timestamp without time zone default now(),
	estado boolean not null,
	primary key (id),
	foreign key (idusuario) references usuario(id),
	foreign key (idniveldolor) references nivel_dolor(id),
	foreign key (idzona) references zona_dolor(id)
);

INSERT INTO rol (codigo, estado) VALUES ('ADMIN', true), ('PACIENTE', true);
INSERT INTO usuario (nombre, contrasenia, idrol, estado) VALUES 
('ADMIN', '123456-a', 1, true);
INSERT INTO usuario (nombre, contrasenia, idrol, estado) VALUES 
('FISICO', '123456-a', 1, true);
INSERT INTO usuario (nombre, contrasenia, idrol, estado) VALUES 
('LESTER', '123456', 2, true);

create table encuesta
(
	id int generated always as identity,
	nombre varchar(500) not null,
	estado boolean default('t') not null,
	primary key (id)
);

create table encuesta_seccion
(
	id int generated always as identity,
	idencuesta int not null,
	nombre varchar(500) not null,
	indicadores varchar(1000) not null,
	estado boolean default('t') not null,
	primary key (id),
	foreign key (idencuesta) references encuesta(id)
);

create table rango_seccion
(
	id int generated always as identity,
	nombre varchar(500) not null,
	idencuestaseccion int not null,
	valorminimo int not null,
	valormmaximo int not null,
	estado boolean default('t') not null,
	primary key (id),
	foreign key (idencuestaseccion) references encuesta_seccion(id)
);

create table pregunta
(
	id int generated always as identity,
	idencuestaseccion int not null,
	descripcion varchar(5000) not null,
	estado boolean default('t') not null,
	primary key (id),
	foreign key (idencuestaseccion) references encuesta_seccion(id)
);

create table pregunta_usuario
(
	id int generated always as identity,
	idpregunta int not null,
	idusuario int not null,
	puntaje int not null,
	estado boolean default('t') not null,
	primary key (id),
	foreign key (idusuario) references usuario(id),
	foreign key (idpregunta) references pregunta(id)
);

create table encuesta_seccion_usuario
(
	id int generated always as identity,
	idencuestaseccion int not null,
	idusuario int not null,
	puntaje int not null,
	estado boolean default('t') not null,
	primary key (id),
	foreign key (idusuario) references usuario(id),
	foreign key (idencuestaseccion) references encuesta_seccion(id)
);

create table tratamiento
(
	id int generated always as identity,
	idzona int not null,
	idniveldolor int not null,
	idencuestaseccion int not null,
	puntajeminimo int not null,
	puntajemaximo int not null,
	estado boolean not null,
	primary key (id),
	foreign key (idniveldolor) references nivel_dolor(id),
	foreign key (idzona) references zona_dolor(id),
	foreign key (idencuestaseccion) references encuesta_seccion(id)
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


INSERT INTO encuesta(nombre) values
('Escala de Tampa'),
('Escala de Catastrofización'),
('DASS - 21');

INSERT INTO encuesta_seccion(idencuesta, nombre, indicadores) values
(1, 'Kinesiofobia', ''),
(2, 'Dolor', ''),
(3, 'Depresion', 'Desesperanza, Auto depreciación, Falta de interes/motivación'),
(3, 'Ansiedad', 'Activación vegetativa, Efectos del musculo esquelético, Ansiedad situacional, Experiencias subjetiva de efecto ansioso'),
(3, 'Estrés', 'Dificultad para relajarse, Excitación nerviosa, Puede ser fácilmente alterado, Momentos irritables, Impaciente')
;
INSERT INTO rango_seccion(nombre, idencuestaseccion, valorminimo, valormmaximo) values
('Bajo', 1, 11, 27),
('Alto', 1, 28, 1000),

('Bajo', 2, 0, 26),
('Alto', 2, 27, 1000),

('Normal', 3, 0, 4),
('Leve', 3, 5, 6),
('Moderado', 3, 7, 10),
('Severo', 3, 11, 13),
('Extremadamente Severo', 3, 14, 1000),
('Normal', 4, 0, 3),
('Leve', 4, 4, 5),
('Moderado', 4, 6, 7),
('Severo', 4, 8, 9),
('Extremadamente Severo', 4, 10, 1000),
('Normal', 5, 0, 7),
('Leve', 5, 8, 9),
('Moderado', 5, 10, 12),
('Severo', 5, 13, 16),
('Extremadamente Severo', 5, 17, 1000)
;

INSERT INTO pregunta(idencuestaseccion, descripcion) values
(1, 'Tengo miedo de lesionarme si hago ejercicio físico.'),
(1, 'Si me dejara vencer por el dolor, el dolor aumentaría.'),
(1, 'Mi cuerpo me está diciendo que tengo algo serio.'),
(1, 'Tener dolor siempre quiere decir que en el cuerpo hay una lesión.'),
(1, 'Tengo miedo de lesionarme sin querer.'),
(1, 'Lo más seguro para evitar que aumente el dolor es tener cuidado y no hacer movimientos innecesarios.'),
(1, 'No me dolería tanto si no tuviese algo serio en mi cuerpo.'),
(1, 'El dolor me dice cuándo debo parar la actividad para no lesionarme.'),
(1, 'No es seguro para una persona con mi enfermedad hacer actividades físicas. '),
(1, 'No puedo hacer todo lo que la gente normal hace porque me podría lesionar con facilidad.'),
(1, 'Nadie debería hacer actividad física cuando tiene dolor.'),
(2, 'Estoy preocupado todo el tiempo pensando si el dolor desaparecerá.'),
(2, 'Siento que ya no puedo continuar debido al dolor.'),
(2, 'El dolor es muy fuerte y creo que nunca va a mejorar.'),
(2, 'El dolor es muy desagradable y siento que es más fuerte que yo.'),
(2, 'Siento que no aguanto más el dolor.'),
(2, 'Tengo miedo de que el dolor pueda empeorar.'),
(2, 'Me vienen a la memoria experiencias dolorosas anteriores.'),
(2, 'Deseo desesperadamente que desaparezca el dolor.'),
(2, 'No paro de pensar en el dolor.'),
(2, 'No dejo de pensar en lo mucho que me duele.'),
(2, 'No dejo de pensar en lo mucho que deseo que desaparezca el dolor.'),
(2, 'No hay nada que pueda hacer para aliviar la intensidad del dolor.'),
(2, 'Me pregunto si me puede pasar algo grave.'),
(5, 'Me resulta difícil relajarme.'),
(4, 'Noté resequedad en mi boca.'),
(3, 'Pareciera que no puedo experimentar ningún sentimiento positivo.'),
(4, 'Tuve dificultades al respirar (por ejemplo, respiración excesivamente rápida dificultad para respirar sin ningún esfuerzo físico).'),
(3, 'Me resultó difícil tener iniciativa para hacer cosas.'),
(5, 'Tendía a reaccionar en exceso ante las situaciones.'),
(4, 'Tuve temblores (por ejemplo, en las manos).'),
(5, 'Sentí que estaba usando mucha energía nerviosa.'),
(4, 'Estuve preocupado por situaciones en las que podría entrar en pánico y parecer un tonto.'),
(3, 'Sentí que no tenía nada que esperar.'),
(5, 'Me encontré agitado.'),
(5, 'Tuve dificultades para relajarme.'),
(3, 'Me sentí abatido y triste.'),
(5, 'No toleraba nada que me impidiera continuar con lo que estaba haciendo.'),
(4, 'Sentí que estaba cerca del pánico.'),
(3, 'No pude entusiasmarme con nada.'),
(3, 'Sentí que no valía mucho como persona.'),
(5, 'No pude entusiasmarme con nada.'),
(4, 'Fui consciente del trabajo de mi corazón e ausencia de esfuerzo físico (por ejemplo, sensación de aumento de la frecuencia cardiaca, falta de latido del corazón).'),
(4, 'Sentí miedo sin ninguna razón.'),
(3, 'Sentí que la vida no valía nada.')
;
