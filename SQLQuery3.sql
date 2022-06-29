Create database Bd_Tienda
use Bd_Tienda

create table Tienda
(Id_Tienda int  not null,
Nom_Tienda varchar(30) not null,
Direccion varchar(40) not null,
Telefono varchar(9) not null)
GO


create table EMPLEADOS
(Id_Empleado int not null,
Nombre varchar(30) not null,
Apellido varchar(30) not null,
Edad int not null,
Direccion varchar(40)not null,
Telefono varchar(9) not null,
Cargo varchar(20) not null)
Go


create table Usuarios(
Id_Empleado int not null,
Usuario varchar(50) not null,
Contraseña varchar(50) not null
)

create table PROVEEDORES
(Id_proveedor int not null,
Nom_Prov varchar(30) not null,
Telefono varchar(9) not null,
DUI varchar(20) not null)
go

--creando tabla CATEGORIA
create table CATEGORIA
(Id_Categoria int  not null,
Nom_Categoria varchar(20) not null)
Go

insert into categoria 
values(1,'LACTEOS'),
(2,'ACEITES'),
(3,'HELADOS'),
(4,'FRUTAS'),
(5,'PANADERIA'),
(6,'BEBIDAS'),
(7,'CARNE'),
(8,'VERDURAS'),
(9,'CHURROS'),
(10,'SOPAS'),
(11,'PROD. PROTEINICOS')
GO

select *  from clientes WHERE Id_cliente=2

--creando tabla PRODUCTOS
create table PRODUCTOS
(Id_Productos int  not null,
Id_Categoria int not null,
Nom_Prod varchar(30) not null,
PrecioProd float not null)
Go

--procedimineto para ingresar nuevos productos
create PROCEDURE SP_Nuev_Produc
@Id_productos int,
@Id_categoria int,
@Nom_prod varchar(50),
@PrecioProd float
as
insert into PRODUCTOS(Id_Productos,Id_Categoria,Nom_Prod,PrecioProd)
values(@Id_productos,@Id_categoria,@Nom_prod,@PrecioProd)

exec SP_Nuev_Produc 3,4,'Fresas',1.75

--procedimiento para actualizar datos
create PROCEDURE sp_Actualizar_productos
@Id_productos int,
@Id_categoria int,
@Nom_prod varchar(50),
@PrecioProd float
as
begin 
update PRODUCTOS set
Id_Productos=@Id_productos,Id_Categoria=@Id_categoria,Nom_Prod=@Nom_prod,PrecioProd=@PrecioProd
where Id_Productos=@Id_productos
print 'Datos actualizados con exito'
end


--eliminar productos de tabla

create procedure EliminarProducto
@Id_productos int 
as

delete from PRODUCTOS WHERE Id_Productos=@Id_productos


create table Ventas
(Id_venta int constraint pk_Pedidos primary key not null,
Usuario varchar (50) not null,
Id_cliente int not null,
total float not null)
go
drop table ventas

--agregando id liente a ventas
alter table Ventas add Usuario varchar(50) null;



create table detalle_venta(
Id_detVentas int constraint pk_ventas primary key not null,
Id_venta int not null,
Nom_Prod varchar(30) not null,
cantidad float,
fecha date,
Nomb_cliente varchar (30),
Id_Empleado int not null)

--eliminado id cliente de detalleventas
alter table detalle_venta drop column Id_Empleado;

insert into Ventas values(1,1,)

create table CLIENTES
(Id_cliente int constraint pk_clientes primary key not null,
Nomb_cliente varchar(30) not null,
Ape_Cliente varchar(40) not null)
go


alter table EMPLEADOS add constraint pk_empleados primary key (Id_Empleado)
alter table Usuarios add constraint pk_usuarios primary key (Usuario)

alter table Productos add constraint pk_productos primary key (Id_Productos)
alter table Categoria add constraint pk_categoria primary key (Id_Categoria)

alter table PROVEEDORES add constraint pk_proveedores primary key (Id_proveedor)


--Estableciendo  las relaciones


alter table Usuarios add constraint fk_Usuarios foreign key (Id_Empleado) REFERENCES Empleados (Id_Empleado)
alter table	Productos add constraint fk_productos foreign key (Id_Categoria) REFERENCES Categoria (Id_Categoria)

alter table	Ventas add constraint fk_ventas foreign key (Id_producto) REFERENCES Productos (Id_productos)

alter table	Ventas add constraint fk_venta foreign key (Id_cliente) REFERENCES Clientes (Id_cliente)

alter table	detalle_venta add constraint fk_detalle_venta foreign key (Id_venta) REFERENCES Ventas (Id_venta)

alter table ventas add constraint fk_vent foreign key (Usuario) REFERENCES Usuarios (Usuario)
alter table ventas drop constraint fk_vent
select * from detalle_venta
select * from clientes
select * from ventas
select * from tienda
select * from Empleados
select * from Proveedores
select * from Usuarios
select * from Productos
select * from Categoria
delete from Usuarios where Id_Empleado=3
delete from EMPLEADOS where Id_Empleado=3
--elimnar relacion 
alter table ventas drop constraint fk_venta

insert into PRODUCTOS values(1,8,'Cebolla',0.25)
insert into PRODUCTOS values(2,4,'manzana',0.50)
insert into EMPLEADOS values('sandra beatriz','Gonzales ','23','colonia san luis','60120363','Cajera')
insert into Usuarios values(1,'sandra01','12345')
insert into clientes values(1,'Lorena noemy','Manrriques')
insert into clientes values(2,'Carla Estefany','Lopez')
insert into ventas values(1,2,2)
insert into detalle_venta values(1,1, 'Cebolla',2,'','Carla Estefany')

--agregando total a ventasss
alter table Ventas add total float

--eliminando id productos
alter table ventas drop column Id_productos;

Select * from Usuarios WHERE Usuario='carla01'

insert into Usuarios values(2,'carla01',123)

insert into EMPLEADOS values('Carla Stefany','Saldaña',22,'5t Avenida sur Cuscatlan','60120367','Cajera')


create PROCEDURE SP_Insert_ventas
@Id_venta int,
@Id_productos int,
@Id_cliente varchar(50),
@Usuario varchar(50),
@total float
as
insert into Ventas(Id_venta,Id_Productos,id_cliente,Usuario,total)
values(@Id_venta,@Id_Productos,@id_cliente,@Usuario,@total)

select *  from ventas

exec SP_Insert_ventas 2,2,'carla01',34
select * from ventas

create table ventas (
id_Ventas int identity (1,1) not null,
id_cliente int,
id_producto int,
nom_product varchar(50),
precio varchar(50),
cantidad int,
total varchar (9),
fecha varchar (50)
)


--procedimineto ventas de productos
alter  PROCEDURE SP_ventas_productos
@id_cliente int,
@id_prod int,
@nom_product varchar(50),
@precio varchar(3),
@cantidad int,
@total varchar (3),
@fecha varchar(50)


as
insert into ventas(id_cliente,id_producto,nom_product,precio,cantidad,total,fecha)
values(@id_cliente,@id_prod,@nom_product,@precio,@cantidad,@total,@fecha)

 
select* from  ventas

DELETE ventas where id_ventas=1

drop table ventas


CREATE TABLE DETALLES_VENTA(
Id_venta int,
Id_cliente int,
fecha date,


)
