-- creación de la base de datos
create database AdminProyectosAPI;
go

use AdminProyectosAPI;

-- creación de las tablas
create table Categorias(
Id int not null identity(1,1),
Nombre nvarchar(50) not null,
primary key(Id)
);
go

create table Proyectos(
Id int not null identity(1,1),
IdCategoria int not null,
Titulo nvarchar(100) not null,
Descripcion nvarchar(250) not null,
FechaInicio date not null,
FechaFin date not null,
primary key(Id),
foreign key(IdCategoria) references Categorias(Id)
);
go

create table Tareas(
Id int not null identity(1,1),
IdProyecto int not null,
Nombre nvarchar(100) not null,
Descripcion nvarchar(250) not null,
Duracion nvarchar(50) not null,
Estado tinyint not null,
primary key(Id),
foreign key(IdProyecto) references Proyectos(Id)
);
go

create table Roles(
Id int not null identity(1,1),
Nombre nvarchar(50) not null,
primary key(Id)
);
go

create table Usuarios(
Id int not null identity(1,1),
IdRol int not null,
Nombre nvarchar(50) not null,
Apellido nvarchar(50) not null,
Telefono nvarchar(20) not null,
[Login] nvarchar(25) not null,
Clave nvarchar(200) not null,
primary key(Id),
foreign key(IdRol) references Roles(Id)
);
go

-- inserción de datos iniciales
use AdminProyectosAPI;
insert into Roles(nombre) values('Administrador');
insert into Roles(nombre) values('Usuario');

-- clave: sysadmin, usar encriptador en línea
insert into Usuarios(idrol, nombre, apellido, telefono, login, clave) 
values(1, 'Julio César', 'Tula', '67335298', 'jc-tula', '48a365b4ce1e322a55ae9017f3daf0c0');
