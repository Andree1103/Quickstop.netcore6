USE master
GO
DROP DATABASE BDQUICKSTOP
GO
CREATE DATABASE BDQUICKSTOP
GO
IF db_id('BDQUICKSTOP') is not null
begin
	ALTER DATABASE BDQUICKSTOP
	SET SINGLE_USER WITH ROLLBACK IMMEDIATE

	DROP DATABASE BDQUICKSTOP
end
go

CREATE DATABASE BDQUICKSTOP
COLLATE Modern_Spanish_CI_AI
GO

SET LANGUAGE SPANISH
SET NOCOUNT ON
GO

USE BDQUICKSTOP
GO
-- Creacion de las Tablas
CREATE TABLE tb_Marca
(
	cod_marca int not null primary key,
	nombre varchar(50) not null,
	des_marca varchar(50) not null
)
GO
INSERT INTO tb_Marca VALUES(1, 'COCA COLA COMPANY','Marca de procedencia extranjera')
INSERT INTO tb_Marca VALUES(2, 'PEPSI CORPORATION','Marca de procedencia extranjera')
INSERT INTO tb_Marca VALUES(3, 'KARINTO','Empresa con 20 años de experiencia')
INSERT INTO tb_Marca VALUES(4, 'FRITOLAY','Empresa con 10 años de experiencia')
INSERT INTO tb_Marca VALUES(5, 'San Jorge','Marca peruana con los dulces mas sabrosos')
INSERT INTO tb_Marca VALUES(6, 'Costa','Empresa peruana lider en la venta de golosinas')
INSERT INTO tb_Marca VALUES(7, 'QUICKSTOP','Productos elaborados por la marca')
INSERT INTO tb_Marca VALUES(8, 'Donofrio','Empresa lider en la venta de helados')
INSERT INTO tb_Marca VALUES(9, 'ARTIKA','Helados cuzqueños con sabor nacional')
INSERT INTO tb_Marca VALUES(10, 'Otros','Otros Prooductos')
GO




CREATE TABLE tb_Categorias
(
	cod_cat char (5) NOT NULL Primary key,
	nombre varchar(50) not null,
	cod_marca int NOT NULL references tb_Marca,
	eli_cat	char(2) 
)
GO
INSERT INTO tb_Categorias VALUES('CA001','Bebidas COCA COLA CORP',1,'No')
INSERT INTO tb_Categorias VALUES('CA002','Bebidas PEPSI CORP',2,'No')
INSERT INTO tb_Categorias VALUES('CA003','Piqueos y Snacks KARINTO',3,'No')
INSERT INTO tb_Categorias VALUES('CA004','Piqueos y Snacks FRITOLAY',4,'No')
INSERT INTO tb_Categorias VALUES('CA005','Confiteria y Dulces San Jorge',5,'No')
INSERT INTO tb_Categorias VALUES('CA006','Confiteria y Dulces Costa',6,'No')
INSERT INTO tb_Categorias VALUES('CA007','Comidas QUICKSTOP',7,'No')
INSERT INTO tb_Categorias VALUES('CA008','Helados Donofrio',8,'No')
INSERT INTO tb_Categorias VALUES('CA009','Helados ARTIKA',9,'No')
INSERT INTO tb_Categorias VALUES('CA010','Otros',10,'No')
GO



CREATE TABLE tb_Productos (
	cod_prod char (5) NOT NULL Primary key,
	nom_prod varchar (50) NOT NULL ,
	cod_cat char (5) NOT NULL references tb_Categorias,
	pre_prod decimal(7,2) NULL ,
	stk_prod int NULL 
	)
GO

INSERT INTO tb_Productos VALUES ('P0001','Gaseosa Coca Cola 3L', 'CA001', 10.50, 200)
INSERT INTO tb_Productos VALUES ('P0002','Gaseosa Inca Cola 3L','CA001' , 10.50, 200)
INSERT INTO tb_Productos VALUES ('P0003','Botella de agua San Luis 1L', 'CA001', 3.50, 200)
INSERT INTO tb_Productos VALUES ('P0004','Gaseosa Sprite 500mL', 'CA001', 3.00, 300)
INSERT INTO tb_Productos VALUES ('P0005','Powerade MULTIFRUTAS 500mL', 'CA001', 3.50, 200)

INSERT INTO tb_Productos VALUES ('P0006','Bebida Evervess Carbonatada 1.5L', 'CA002', 6.90, 200)
INSERT INTO tb_Productos VALUES ('P0007','Gaseosa Pepsi Black 3L', 'CA002', 6.90, 120)
INSERT INTO tb_Productos VALUES ('P0008','Gaseosa 7up Personal', 'CA002', 3.00, 200)
INSERT INTO tb_Productos VALUES ('P0009','Agua San Carlos Personal','CA002' , 2.50, 200)
INSERT INTO tb_Productos VALUES ('P0010','Gatorade sabor Tropical', 'CA002', 3.50, 200)

INSERT INTO tb_Productos VALUES ('P0011','Chifles Karinto 34g', 'CA003', 2.00, 300)
INSERT INTO tb_Productos VALUES ('P0012','Habas', 'CA003', 1.50, 200)
INSERT INTO tb_Productos VALUES ('P0013','Mani Crocante Picante 100g', 'CA003', 2.50, 200)
INSERT INTO tb_Productos VALUES ('P0014','Mani Crocante Salado 100g', 'CA003', 2.50, 120)
INSERT INTO tb_Productos VALUES ('P0015','Mani Crocante Confitado 100g', 'CA003', 2.50, 120)

INSERT INTO tb_Productos VALUES ('P0016','Cuates Picantes', 'CA004', 1.50, 200)
INSERT INTO tb_Productos VALUES ('P0017','Papas Lays', 'CA004', 2.50, 120)
INSERT INTO tb_Productos VALUES ('P0018','Cheese Tris', 'CA004', 2.50, 200)
INSERT INTO tb_Productos VALUES ('P0019','Cheetos Mega Queso','CA004' , 2.50, 200)
INSERT INTO tb_Productos VALUES ('P0020','Piqueo Snax Original', 'CA004', 2.50, 200)

INSERT INTO tb_Productos VALUES ('P0021','Galleta BlackOut', 'CA005', 1.20, 300)
INSERT INTO tb_Productos VALUES ('P0022','Galletas Animalitos', 'CA005', 2.50, 200)
INSERT INTO tb_Productos VALUES ('P0023','Fruta Mix', 'CA005', 1.50, 200)
INSERT INTO tb_Productos VALUES ('P0024','Municion 55g', 'CA005', 2.50, 120)
INSERT INTO tb_Productos VALUES ('P0025','Soda Familiar', 'CA005', 2.50, 120)

INSERT INTO tb_Productos VALUES ('P0026','Costa Obsesion Barquillos 21g', 'CA006', 1.50, 200)
INSERT INTO tb_Productos VALUES ('P0027','Chocman', 'CA006', 1.50, 120)
INSERT INTO tb_Productos VALUES ('P0028','Mecano', 'CA006', 1.50, 200)
INSERT INTO tb_Productos VALUES ('P0029','Chips','CA006' , 1.50, 200)
INSERT INTO tb_Productos VALUES ('P0030','Caritas', 'CA006', 1.50, 200)

INSERT INTO tb_Productos VALUES ('P0031','Enrollado de Jamón y Queso El Cedro 1 und', 'CA007', 9.00, 300)
INSERT INTO tb_Productos VALUES ('P0032','Enrollado de Lechón El Cedro', 'CA007', 8.50, 200)
INSERT INTO tb_Productos VALUES ('P0033','Combo 02 Pizzas Familiar Americana', 'CA007', 19.50, 200)
INSERT INTO tb_Productos VALUES ('P0034','Combo 02 Torta De Chocolate', 'CA007', 6.00, 120)
INSERT INTO tb_Productos VALUES ('P0035','Combo 02 Hamburguesa Parrillera', 'CA007', 12.00, 120)

INSERT INTO tb_Productos VALUES ('P0036','Helado Jet', 'CA008', 3.00, 200)
INSERT INTO tb_Productos VALUES ('P0037','Helado Huracan', 'CA008', 2.50, 120)
INSERT INTO tb_Productos VALUES ('P0038','Helado Frio Rico', 'CA008', 5.50, 200)
INSERT INTO tb_Productos VALUES ('P0039','Helado Sin Parar','CA008' , 6.00, 200)
INSERT INTO tb_Productos VALUES ('P0040','Helado Dolcetto', 'CA008', 5.50, 200)

INSERT INTO tb_Productos VALUES ('P0041','Helado Chancaycito', 'CA009', 3.00, 300)
INSERT INTO tb_Productos VALUES ('P0042','Paletas de Sabores Artika', 'CA009', 2.50, 200)
INSERT INTO tb_Productos VALUES ('P0043','Helado Aguaje', 'CA009', 3.50, 200)
INSERT INTO tb_Productos VALUES ('P0044','Helado Kimmi', 'CA009', 3.00, 120)
INSERT INTO tb_Productos VALUES ('P0045','Helado Ron con pasas', 'CA009', 1.50, 120)
GO


CREATE TABLE tb_Provincia (
	cod_prov int primary key,
	nom_prov varchar(35) not null
)
GO


INSERT INTO tb_Provincia VALUES (1, 'Lima')
INSERT INTO tb_Provincia VALUES (2, 'Arequipa')
INSERT INTO tb_Provincia VALUES (3, 'Ica')
INSERT INTO tb_Provincia VALUES (4, 'Cajamarca')
INSERT INTO tb_Provincia VALUES (5, 'Trujillo')
INSERT INTO tb_Provincia VALUES (6, 'Cuzco')
INSERT INTO tb_Provincia VALUES (7, 'Callao')
INSERT INTO tb_Provincia VALUES (8, 'Tacna')
INSERT INTO tb_Provincia VALUES (9, 'Junin')
INSERT INTO tb_Provincia VALUES (10, 'Puno')
GO


CREATE TABLE tb_Distritos (
	cod_dist int primary key,
	nom_dist varchar(35) not null,
	cod_prov int NOT NULL references tb_Provincia
)
GO



INSERT INTO tb_Distritos VALUES (1, 'Cercado de Lima',1)
INSERT INTO tb_Distritos VALUES (2, 'San juan de Lurigancho',1)
INSERT INTO tb_Distritos VALUES (3, 'San juan de Miraflores',1)
INSERT INTO tb_Distritos VALUES (4, 'Surco',1)
INSERT INTO tb_Distritos VALUES (5, 'La Molina',1)
INSERT INTO tb_Distritos VALUES (6, 'San borja',1)
INSERT INTO tb_Distritos VALUES (7, 'San Isidro',1)
INSERT INTO tb_Distritos VALUES (8, 'San Miguel',1)
INSERT INTO tb_Distritos VALUES (9, 'San Luis',1)
INSERT INTO tb_Distritos VALUES (10, 'Rimac',1)
GO



CREATE TABLE tb_Clientes (
	cod_cli char (5) NOT NULL Primary Key check(cod_cli like 'C[0-9][0-9][0-9][0-9]'),
	nom_cli varchar(50) NOT NULL Unique,
	tel_cli varchar(10) default('Sin telefono'),
	cor_cli varchar(50) default('Sin correo electronico') ,
	dir_cli varchar (50) default('Sin direccion'),
    cred_cli int default(1000),
	fec_nac date default('01/01/2000'),
	cod_dist int references tb_Distritos)
GO
select * from tb_Clientes

INSERT INTO tb_Clientes VALUES ('C0001','Alvarez Peña, Angel','981234567','importa@hotmail.com','Av. La Marina 1234',3500,default,1)
INSERT INTO tb_Clientes VALUES ('C0002','Ponte Gomez, Alejandro','965845038','alex@yahoo.com','Av. Pardo 456  ',2800,'15/10/1999',1)
INSERT INTO tb_Clientes VALUES ('C0003', 'Lopez Fernandez, Maria','987445126', 'maria@gmail.com', 'Calle 123', 3200, '05/03/1985', 1)
INSERT INTO tb_Clientes VALUES ('C0004', 'Gonzalez Ramirez, Juan', '975236985', 'juan@hotmail.com', 'Avenida Principal 789', 1800, '12/07/1992', 1)
INSERT INTO tb_Clientes VALUES ('C0005', 'Martinez Rodriguez, Carolina', '965874141', 'carolina@gmail.com', 'Calle San Martin 234', 4100, '18/09/1980',3)
INSERT INTO tb_Clientes VALUES ('C0006', 'Hernandez Cruz, Luis', '955896314', 'luis@gmail.com', 'Avenida Libertad 567', 2400, '07/11/1991', 1)
INSERT INTO tb_Clientes VALUES ('C0007', 'Sanchez Morales, Ana', '957124598', 'ana@yahoo.com', 'Calle Primavera 789', 2900, '22/06/1988', 1)
INSERT INTO tb_Clientes VALUES ('C0008', 'Ramirez Gomez, Pedro', '988965412', 'pedro@hotmail.com', 'Avenida Central 456', 3400, '30/04/1987', 1)
INSERT INTO tb_Clientes VALUES ('C0009', 'Gomez Torres, Sofia', '926547892', 'sofia@gmail.com', 'Calle 456', 1800, '10/02/1995', 1)
INSERT INTO tb_Clientes VALUES ('C0010', 'Perez Herrera, Javier', '967458632', 'javier@yahoo.com', 'Avenida Sur 789', 3200, '17/11/1983', 1)
INSERT INTO tb_Clientes VALUES ('C0011', 'Lopez Ramirez, Gabriela', '969856214', 'gabriela@hotmail.com', 'Calle Principal 345', 2700, '28/05/1990', 1)
INSERT INTO tb_Clientes VALUES ('C0012', 'Garcia Fernandez, Ricardo', '957514896', 'ricardo@gmail.com', 'Avenida Norte 789', 3800, '14/08/1984', 1)
INSERT INTO tb_Clientes VALUES ('C0013', 'Martinez Hernandez, Laura', '936254789', 'laura@hotmail.com', 'Calle Central 567', 2100, '03/12/1993', 1)
INSERT INTO tb_Clientes VALUES ('C0014', 'Hernandez Lopez, Eduardo', '958547931', 'eduardo@gmail.com', 'Avenida Primavera 123', 2600, '11/09/1986', 1)
INSERT INTO tb_Clientes VALUES ('C0015', 'Sanchez Garcia, Carmen', '979654782', 'carmen@yahoo.com', 'Calle Libertad 789', 3100, '24/02/1982', 1)
INSERT INTO tb_Clientes VALUES ('C0016', 'Ramirez Martinez, Daniel', '947524168', 'daniel@gmail.com', 'Avenida San Martin 456', 2400, '01/07/1994', 1)
INSERT INTO tb_Clientes VALUES ('C0017', 'Gomez Sanchez, Andrea', '948965478', 'andrea@hotmail.com', 'Calle Principal 234', 2900, '08/04/1989', 1)
INSERT INTO tb_Clientes VALUES ('C0018', 'Perez Gonzalez, Luisa', '946548712', 'luisa@gmail.com', 'Avenida Libertad 789', 3400, '16/01/1998', 1)
GO


CREATE TABLE tb_Ventas_Cab (
	num_vta char (5) NOT NULL Primary Key,
	fec_vta date NULL ,
	cod_cli char (5) NULL ,
    cod_ven int	 )
GO

INSERT INTO tb_Ventas_Cab VALUES ('V0001','02/03/2023','C0001',1)
INSERT INTO tb_Ventas_Cab VALUES ('V0002','02/03/2023','C0002',2)
INSERT INTO tb_Ventas_Cab VALUES ('V0003','02/03/2023','C0003',3)
INSERT INTO tb_Ventas_Cab VALUES ('V0004','08/05/2023','C0004',4)
INSERT INTO tb_Ventas_Cab VALUES ('V0005','08/05/2023','C0005',5)
INSERT INTO tb_Ventas_Cab VALUES ('V0006','08/05/2023','C0006',6)
INSERT INTO tb_Ventas_Cab VALUES ('V0007','10/07/2023','C0007',7)


CREATE TABLE tb_Ventas_Deta (
	num_vta char (5) NOT NULL ,
	cod_prod char (5) NOT NULL,
	cantidad  int NULL , 
    Primary Key(num_vta,cod_prod))
GO
INSERT INTO tb_Ventas_Deta VALUES ('V0001','P0001',1)
INSERT INTO tb_Ventas_Deta VALUES ('V0002','P0002', 1)
INSERT INTO tb_Ventas_Deta VALUES ('V0003','P0003',1)
INSERT INTO tb_Ventas_Deta VALUES ('V0004','P0004', 1)
INSERT INTO tb_Ventas_Deta VALUES ('V0005','P0005', 1)
INSERT INTO tb_Ventas_Deta VALUES ('V0006','P0006', 1)
INSERT INTO tb_Ventas_Deta VALUES ('V0007','P0007',1)

CREATE TABLE tb_Vendedor(
	cod_ven int identity(1,1) not null primary key,
	nom_ven varchar (50) NULL ,
	fecing_ven date NULL --fecha de ingreso 
)
GO
INSERT INTO tb_Vendedor VALUES ('Diaz Vera, Paola Isabel','01/03/2008')
INSERT INTO tb_Vendedor VALUES ('Pardo Campos, Carlos','11/05/2008')
INSERT INTO tb_Vendedor VALUES ('Linares Moreno, Susana Claudia','12/06/2008')
INSERT INTO tb_Vendedor VALUES ('Mendoza Obando, Maria del Carmen','21/11/2008')
INSERT INTO tb_Vendedor VALUES ('Narvaez Gomez, Mario Dario','15/12/2008')
INSERT INTO tb_Vendedor VALUES ('Murillo Mancini, Juan Carlos','02/05/2009')
INSERT INTO tb_Vendedor VALUES ('Gonzales Vera, Erlinda','12/07/2009')


CREATE TABLE tb_Favoritos(
	cod_fav char (5) NOT NULL Primary key,
	cod_cli char(5) NOT NULL references tb_clientes,
	cod_prod char (5) NOT NULL references tb_Productos,
	descripcion char(200),
	eliminado	char(2)
)
GO
select * from  tb_Favoritos go

INSERT INTO tb_Favoritos VALUES ('F0001','C0001','P0001','LA MEJOR BEBIDA DEL MUNDO','NO')
INSERT INTO tb_Favoritos VALUES ('F0002','C0002','P0003','LA MEJOR BEBIDA PERUANA','NO')
INSERT INTO tb_Favoritos VALUES ('F0003','C0003','P0003','LO MEJOR ES LO SALUDABLE','NO')
INSERT INTO tb_Favoritos VALUES ('F0004','C0004','P0018','ES MUCHO MEJOR QUE LOS DORITOS','NO')

CREATE TABLE tb_comentarios(
	cod_res char (5) NOT NULL Primary key,
	cod_cli char(5) NOT NULL references tb_clientes,
	comentarios char(200),
	eliminado	char(2)
)


INSERT INTO tb_comentarios VALUES ('CM001','C0001','LA TIENDA ES MUY BUENA Y AMIGABLE','NO')
INSERT INTO tb_comentarios VALUES ('CM002','C0002','LA MEJOR TIENDA DEL MUNDO','NO')
INSERT INTO tb_comentarios VALUES ('CM003','C0003','LO MEJOR DE LO MEJOR','NO')
INSERT INTO tb_comentarios VALUES ('CM004','C0004','AHI COSAS QUE MEJORAR PERO ESTOY SATISFECHO','NO')
GO
------------------------------------------------------------------------------------------------
set dateformat dmy
set language spanish
go
Alter Table tb_Ventas_Deta
Add precio decimal(7,2)
GO
Update D 
  Set D.precio=A.pre_prod
  From tb_Ventas_Deta D,tb_Productos A 
Where D.cod_prod=A.cod_prod
GO
ALTER TABLE tb_Productos
ADD eli_prod char(2) default 'No' with values
GO
ALTER TABLE tb_CLIENTES
ADD eli_cli	char(2) default 'No' with values
GO
ALTER TABLE tb_VENDEDOR
ADD eli_ven	char(2) default 'No' with values
GO
ALTER TABLE tb_Ventas_Deta
ADD tb_anulado char(2) default 'No' with values
GO
ALTER TABLE tb_Ventas_Cab
ADD tot_vta decimal(8,2) default 0 with values
GO
UPDATE VC
	SET tot_vta = VD.Total
FROM tb_Ventas_Cab VC INNER JOIN
  (SELECT num_vta, SUM(Precio*Cantidad) as Total FROM tb_Ventas_Deta
     GROUP BY num_vta) VD
	on VC.num_vta=VD.num_vta
GO		
ALTER TABLE tb_Ventas_Cab
ADD anulado char(2) default 'No' with values
GO
-------------------------------------------------------------
alter table tb_Ventas_Cab
	add constraint fk_Ventas_Cab_cod_cli foreign key(cod_cli)
		references tb_clientes(cod_cli)
go
-------------------------------------------------------------

alter table tb_Ventas_Cab
	add constraint fk_Ventas_Cab_cod_ven foreign key(cod_ven)
		references tb_vendedor(cod_ven)
go
-------------------------------------------------------------

alter table tb_Ventas_Deta
	add constraint fk_Ventas_Deta_num_vta foreign key(num_vta)
		references tb_Ventas_Cab(num_vta)
go
-------------------------------------------------------------

alter table tb_Ventas_Deta
	add constraint fk_Ventas_Deta_cod_prod foreign key(cod_prod)
		references tb_Productos(cod_prod)
go
------------------------------------------
-- CREANDO PROCEDIMIENTOS ALMACENADOS
-- CREANDO PROCEDIMIENTOS ALMACENADOS
-- CREANDO PROCEDIMIENTOS ALMACENADOS
-- CREANDO PROCEDIMIENTOS ALMACENADOS
-- CREANDO PROCEDIMIENTOS ALMACENADOS
-- CREANDO PROCEDIMIENTOS ALMACENADOS
----------------------------------------

CREATE OR ALTER PROC PA_LISTAR_CLIENTES_CC
AS
SELECT C.cod_cli, C.nom_cli, C.tel_cli, C.cred_cli
FROM tb_Clientes C
WHERE eli_cli = 'No'
GO
-------------------------------------------------------------

CREATE OR ALTER PROC PA_LISTAR_PRODUCTOS_CC
AS
SELECT P.cod_prod, nom_prod, cod_cat, 
       P.pre_prod, P.stk_prod, eli_prod
FROM tb_Productos P
	WHERE P.stk_prod>=0
GO
-------------------------------------------------------------
--------------------------------
-- ULTIMA VENTA REGISTRADA
--------------------------------
SELECT TOP(1) VC.*, C.nom_cli 
   FROM tb_Ventas_Cab VC INNER JOIN tb_CLIENTES C
   ON VC.cod_cli=C.cod_cli
   ORDER BY VC.num_vta DESC
GO

SELECT TOP(1) WITH TIES VD.*, P.nom_prod
FROM tb_Ventas_Deta VD INNER JOIN tb_PRODUCTOS p
	ON VD.cod_prod=P.cod_prod
	ORDER BY VD.num_vta DESC
GO

----------------- PARTE 02 ---------------------

CREATE OR ALTER PROC PA_GRABAR_WEB_VENTAS_CAB
@COD_CLI CHAR(5),@TOT_VTA DECIMAL(8,2)
AS
	DECLARE @NUMERO VARCHAR(5) 
	SELECT @NUMERO=RIGHT(MAX(NUM_VTA),4)+1 FROM tb_Ventas_Cab
	SELECT @NUMERO='V'+RIGHT('000'+@NUMERO,4)
	-----------------------------------------------
	INSERT INTO tb_Ventas_Cab VALUES(@NUMERO,GETDATE(),
	@COD_CLI, 1, @TOT_VTA, 'No')
	SELECT @NUMERO AS NUMERO
GO
select 10024 as Numero
GO
-------------------------------------------------------------
-------------------------------------------------------------
CREATE OR ALTER PROC PA_GRABAR_WEB_VENTAS_DETA
@NUM_VTA CHAR(5), @COD_PROD CHAR(5), 
@CANTIDAD INT, @PRECIO DECIMAL(7,2)
AS
	INSERT INTO tb_Ventas_Deta 
	  VALUES(@NUM_VTA, @COD_PROD, @CANTIDAD, @PRECIO, 'No')
	UPDATE tb_Productos SET stk_prod=stk_prod - @CANTIDAD 
	WHERE cod_prod = @COD_PROD
GO
-------------------------------------------------------------
-------------------------------------------------------------

-- LISTADOS.
create or alter procedure pa_listar_productos_nombre
@nom varchar(50)='%'
as
   select P.*
   from tb_Productos as P
   where P.nom_prod like @nom + '%' 
   and  P.eli_prod='No' 
go
execute pa_listar_productos_nombre 'c'
go
---------------------------------------
create or alter procedure PA_LISTAR_clientes_nombre
@nom varchar(50)='%'
as
   select C.*
   from tb_Clientes as C where C.nom_cli like @nom + '%' 
   and  C.eli_cli='No' 
go
execute PA_LISTAR_clientes_nombre 'a'
go
----------------------------------------

create or alter procedure PA_LISTAR_categoria_nombre
@nom varchar(50)='%'
as
   select C.cod_cat,C.nombre,C.cod_marca
   from tb_Categorias as C where C.nombre like @nom + '%' 
   and  C.eli_cat='No' 
go
execute PA_LISTAR_categoria_nombre 'a'
go
------------------------------------

CREATE OR ALTER PROCEDURE PA_GRABAR_PRODUCTO
@COD_PROD CHAR(5),
@NOM_PROD VARCHAR(50), 
@COD_CAT CHAR(5),
@PRE_PROD decimal(7,2),
@STK_PROD int

AS
	MERGE tb_Productos AS TARGET
	USING (SELECT @COD_PROD,@NOM_PROD,@COD_CAT,@PRE_PROD,
	@STK_PROD,'No') AS SOURCE(cod_prod,nom_prod,cod_cat,pre_prod,
	stk_prod,eli_prod)
	ON TARGET.cod_prod = SOURCE.cod_prod
	WHEN MATCHED THEN
		UPDATE SET TARGET.nom_prod=SOURCE.nom_prod,
		           TARGET.cod_cat=SOURCE.cod_cat,
				   TARGET.pre_prod=SOURCE.pre_prod,
				   TARGET.stk_prod=SOURCE.stk_prod,
				   TARGET.eli_prod=SOURCE.eli_prod
	WHEN NOT MATCHED THEN 
		INSERT VALUES(SOURCE.cod_prod,SOURCE.nom_prod,
		SOURCE.cod_cat,SOURCE.pre_prod,SOURCE.stk_prod,
		SOURCE.eli_prod);
GO


------------------------------------------------------------------
CREATE OR ALTER PROCEDURE PA_GRABAR_CLIENTE
@COD_CLI CHAR(5),
@NOM_CLI VARCHAR(50), 
@TEL_CLI VARCHAR(10),
@COR_CLI VARCHAR(50),
@DIR_CLI VARCHAR(50),
@CRED_CLI int,
@FEC_NAC date,
@COD_DIST int
AS
	MERGE tb_Clientes AS TARGET
	USING (SELECT @COD_CLI,@NOM_CLI,@TEL_CLI,@COR_CLI,@DIR_CLI,
	@CRED_CLI,@FEC_NAC ,@COD_DIST ,'No') AS SOURCE(cod_cli,nom_cli,tel_cli,cor_cli,
	dir_cli,cred_cli,fec_nac,cod_dist,eli_cli)
	ON TARGET.cod_cli = SOURCE.cod_cli
	WHEN MATCHED THEN
		UPDATE SET TARGET.nom_cli=SOURCE.nom_cli,
		           TARGET.tel_cli=SOURCE.tel_cli,
				   TARGET.cor_cli=SOURCE.cor_cli,
				   TARGET.dir_cli=SOURCE.dir_cli,
				   TARGET.cred_cli=SOURCE.cred_cli,
				   TARGET.fec_nac=SOURCE.fec_nac,
				   TARGET.cod_dist=SOURCE.cod_dist,
				   TARGET.eli_cli=SOURCE.eli_cli
	WHEN NOT MATCHED THEN 
		INSERT VALUES(SOURCE.cod_cli,SOURCE.nom_cli,
		SOURCE.tel_cli,SOURCE.cor_cli,SOURCE.dir_cli,
		SOURCE.cred_cli,SOURCE.fec_nac,SOURCE.cod_dist,SOURCE.eli_cli);
GO

--------------------------------------------------------
CREATE OR ALTER PROCEDURE PA_GRABAR_CATEGORIA
@COD_CAT CHAR(5),
@NOM_CAT VARCHAR(50), 
@COD_MARCA INT
AS
	MERGE dbo.tb_Categorias AS TARGET
	USING (SELECT @COD_CAT,@NOM_CAT,@COD_MARCA,'No') AS SOURCE(cod_cat,nombre,cod_marca,eli_cat)
	ON TARGET.cod_cat = SOURCE.cod_cat
	WHEN MATCHED THEN
		UPDATE SET TARGET.nombre=SOURCE.nombre,
		           TARGET.cod_marca=SOURCE.cod_marca,
				   TARGET.eli_cat=SOURCE.eli_cat
	WHEN NOT MATCHED THEN 
		INSERT VALUES(SOURCE.cod_cat,SOURCE.nombre,
		SOURCE.cod_marca,SOURCE.eli_cat);
GO
-----------------------------------------------------
CREATE OR ALTER PROCEDURE PA_LISTAR_CATEGORIA
AS
	SELECT cod_cat,nombre FROM tb_Categorias
GO
----------------------------------------------------
CREATE OR ALTER PROCEDURE PA_LISTAR_DISTRITO
AS
	SELECT cod_dist,nom_dist FROM tb_Distritos
GO
---------------------------------------------------
CREATE OR ALTER PROCEDURE PA_LISTAR_MARCA
AS
	SELECT cod_marca,nombre FROM tb_Marca
GO
------------------------------------------------------
CREATE OR ALTER PROCEDURE PA_ELIMINAR_PRODUCTO
@COD_PROD CHAR(5)
AS
	UPDATE tb_Productos
		SET eli_prod='Si'
	WHERE cod_prod = @COD_PROD
GO
------------------------------------------------------
CREATE OR ALTER PROCEDURE PA_ELIMINAR_CLIENTE
@COD_CLI CHAR(5)
AS
	UPDATE tb_Clientes
		SET eli_cli='Si'
	WHERE cod_cli = @COD_CLI
GO
--------------------------------------------------------
CREATE OR ALTER PROCEDURE PA_ELIMINAR_CATEGORIA
@COD_CAT CHAR(5)
AS
	UPDATE tb_Categorias
		SET eli_cat='Si'
	WHERE cod_cat = @COD_CAT
GO
---------------------------------------------------------
---
SET NOCOUNT OFF
SELECT MENSAJE='BASE DE DATOS CREADA CORRECTAMENTE'
GO
-------------------------------------------------------------
--------------------------API--------------------------
CREATE OR ALTER PROCEDURE PA_FAVORITOS_API
AS
	SELECT * FROM tb_Favoritos where eliminado = 'NO'
GO

CREATE OR ALTER PROCEDURE PA_COMENTARIOS_API
AS
	SELECT * FROM tb_comentarios where eliminado = 'NO'
GO

CREATE OR ALTER PROCEDURE PA_PRODUCTOS_API
AS
	SELECT * FROM tb_Productos where eli_prod = 'NO'
GO

CREATE OR ALTER PROCEDURE PA_CLIENTE_API
AS
	SELECT * FROM tb_Clientes  where eli_cli = 'NO'
GO

-----------------------------------------------

CREATE OR ALTER PROCEDURE PA_INSERTAR_FAVORITO_API
@CODFAV CHAR(5), @CODCLI CHAR(5), @CODPROD CHAR(5),
@DESC CHAR(200)
AS
	INSERT INTO tb_Favoritos
		VALUES(@CODFAV, @CODCLI, @CODPROD, @DESC,'NO')
GO

exec PA_INSERTAR_FAVORITO_API 'F0005','C0010','P0001','BEBIDA INCREIBLE Y REFRESCANTE'
GO

CREATE OR ALTER PROCEDURE PA_ACTUALIZAR_FAVORITO_API
@CODFAV CHAR(5), @CODCLI CHAR(5), @CODPROD CHAR(5),
@DESC CHAR(200)
AS
	UPDATE tb_Favoritos
		SET cod_cli = @CODCLI, cod_prod = @CODPROD, 
		    descripcion = @DESC
	WHERE cod_fav = @CODFAV
GO


CREATE OR ALTER PROCEDURE PA_ELIMINAR_FAVORITO_LOG
@CODFAV CHAR(5)
AS
	UPDATE tb_Favoritos
		SET eliminado = 'Si'
	WHERE cod_fav = @CODFAV
GO

---------------------------------------------------------------

CREATE OR ALTER PROCEDURE PA_INSERTAR_COMENTARIOS_API
@CODRES CHAR(5), @CODCLI CHAR(5), @COMENTARIOS CHAR(200)
AS
	INSERT INTO tb_comentarios
		VALUES(@CODRES, @CODCLI, @COMENTARIOS,'NO')
GO



CREATE OR ALTER PROCEDURE PA_ACTUALIZAR_COMENTARIOS_API
@CODRES CHAR(5), @CODCLI CHAR(5), @COMENTARIOS CHAR(200)

AS
	UPDATE tb_comentarios
		SET  cod_cli= @CODCLI, 
		    comentarios = @COMENTARIOS
	WHERE cod_res = @CODRES
GO

CREATE OR ALTER PROCEDURE PA_ELIMINAR_COMENTARIOS_LOG
@CODRES CHAR(5)
AS
	UPDATE tb_comentarios
		SET eliminado = 'Si'
	WHERE cod_res = @CODRES
GO

----------------
-------------------------------------------------------------

-- Crear la tabla tbl_login
CREATE TABLE tbl_login (
  username VARCHAR(100),
  password VARCHAR(100)
);

-- Insertar el usuario 'admin'
INSERT INTO tbl_login (username, password)
VALUES ('admin', '123');

-- Insertar el usuario 'victor'
INSERT INTO tbl_login (username, password)
VALUES ('victor', '123');
-------------------------------------------------------------
---------------------------


