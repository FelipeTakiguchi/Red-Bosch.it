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


create table Usuario(
	Id int primary key identity,
	Email varchar(100) not null,
	Nome varchar(50) not null,
	Descricao varchar(255) null,
	Senha varbinary(150) not null,
	Salt varchar(30) not null,
	Data_Nascimento Date not null,
	ImageId int References ImageData(Id),
)

create table Forum(
	Id int primary key identity,
	Titulo varchar(50) not null,
	Descricao varchar(255) not null,
	Inscritos int not null,
	ImageId int References ImageData(Id),
	IdUsuario int not null,
	foreign key(IdUsuario) references Usuario(Id),
)

create table UsuarioForum(
	Id int primary key identity,
	IdUsuario int not null,
	foreign key(IdUsuario) references Usuario(Id),
	IdForum int not null,
	foreign key(IdForum) references Forum(Id),
)

create table Permissao(
	Id int primary key identity,
	Nome varchar(50) not null,
	Descricao varchar(255),
)

create table Cargo(
	Id int primary key identity,
	Nome varchar(50) not null,
	IdPermissao int not null,
	foreign key(IdPermissao) references Permissao(Id),
	IdUsuario int not null,
	foreign key(IdUsuario) references Usuario(Id),
)

create table UsuarioCargo(
	Id int primary key identity,
	IdCargo int not null,
	foreign key(IdCargo) references Cargo(Id),
	IdUsuario int not null,
	foreign key(IdUsuario) references Usuario(Id),
	IdForum int not null,
	foreign key(IdForum) references Forum(Id),
)

create table Post(
	Id int primary key identity,
	ImageId int References ImageData(Id),
	Conteudo varchar(255) not null,                                        
	DataPublicacao date not null,
	IdUsuario int not null,
	Votes int not null,
	foreign key(IdUsuario) references Usuario(Id),
	IdForum int not null,
	foreign key(IdForum) references Forum(Id),
)

create table Vote(
	Id int primary key identity,
	State BIT not null,
	IdUsuario int not null,
	IdPost int not null,
	foreign key(IdUsuario) references Usuario(Id),
	foreign key (IdPost) references Post(Id),
)

create table Comentario(
	Id int primary key identity,
	Conteudo varchar(255) not null,
	DataPublicacao date not null,
	IdUsuario int not null,
	foreign key(IdUsuario) references Usuario(Id),
	IdPost int not null,
	foreign key(IdPost) references Post(Id),
)


select * from Usuario
drop table Usuario 
