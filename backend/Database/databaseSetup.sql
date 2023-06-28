USE MASTER
go

if exists(select * from sys.databases where name = 'RedBosch')
	drop database RedBosch
go

create database RedBosch
go
Use RedBosch
go

create table ImageData(
	ID int identity primary key,
	Photo varbinary(MAX) not null
);
go

create table Location(
	ID int identity primary key,
	Nome varchar(60) not null,
	Photo int references ImageData(ID) null
);
go

create table Usuario(
	Id int primary key,
	Email varchar(100) not null,
	Nome varchar(50) not null,
	Senha varbinary(150) not null,
	Salt varchar(30) not null,
	Data_Nascimento Date not null,
	Location int references Location(ID)
)

create table Forum(
	Id int primary key,
	Titulo varchar(50) not null,
	Descricao varchar(255) not null,
	Inscritos int not null,
	Location int references Location(ID),
	IdUsuario int not null,
	foreign key(IdUsuario) references Usuario(Id),
)

create table UsuarioForum(
	Id int primary key,
	IdUsuario int not null,
	foreign key(IdUsuario) references Usuario(Id),
	IdForum int not null,
	foreign key(IdForum) references Forum(Id),
)

create table Permissao(
	Id int primary key,
	Nome varchar(50) not null,
	Descricao varchar(255),
)

create table Cargo(
	Id int primary key,
	Nome varchar(50) not null,
	IdPermissao int not null,
	foreign key(IdPermissao) references Permissao(Id),
	IdUsuario int not null,
	foreign key(IdUsuario) references Usuario(Id),
)

create table UsuarioCargo(
	Id int primary key,
	IdCargo int not null,
	foreign key(IdCargo) references Cargo(Id),
	IdUsuario int not null,
	foreign key(IdUsuario) references Usuario(Id),
	IdForum int not null,
	foreign key(IdForum) references Forum(Id),
)

create table Post(
	Id int primary key,
	Location int references Location(ID),
	Conteudo varchar(255) not null,
	DataPublicacao date not null,
	IdUsuario int not null,
	foreign key(IdUsuario) references Usuario(Id),
	IdForum int not null,
	foreign key(IdForum) references Forum(Id),
)

create table Vote(
	Id int primary key,
	State BIT not null,
	IdUsuario int not null,
	IdPost int not null,
	foreign key(IdUsuario) references Usuario(Id),
	foreign key (IdPost) references Post(Id),
)

create table Comentario(
	Id int primary key,
	Conteudo varchar(255) not null,
	DataPublicacao date not null,
	IdUsuario int not null,
	foreign key(IdUsuario) references Usuario(Id),
	IdPost int not null,
	foreign key(IdPost) references Post(Id),
)

select * from Usuario
drop table Usuario 
